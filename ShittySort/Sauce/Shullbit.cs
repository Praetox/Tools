/*  ShittySort -- helps organize tagged images by filename.
 *  Copyright (C) 2008,2009  Praetox (http://praetox.com/)
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Windows.Forms;
using System.Collections;
using System.IO;
using System;
namespace ShittySort {
    public class cb {
        public static string[] Split(string a, string b) {
            return a.Split(new string[] { b },
                StringSplitOptions.None);
        }
        public static string[] getFiles(string root) {
            string[] ret = new string[0];
            string[] dirs = Directory.GetDirectories(root);
            for (int a = 0; a < dirs.Length; a++) {
                ret = merge(ret, getFiles(dirs[a]));
            }
            string[] files = Directory.GetFiles(root);
            return merge(files, ret);
        }
        public static string[] merge(string[] a, string[] b) {
            string[] ret = new string[a.Length + b.Length];
            a.CopyTo(ret, 0); b.CopyTo(ret, a.Length);
            return ret;
        }
    }
    public class imInfo {
        public string path;
        public iTag[] tags;
        public void Upgrade(iTag[] db) {
            for (int a = 0; a < tags.Length; a++) {
                for (int b = 0; b < db.Length; b++) {
                    if (tags[a].tag == db[b].tag) {
                        tags[a].count = db[b].count;
                        tags[a].type = db[b].type;
                        break;
                    }
                }
            }
        }
        public void Regression() {
            for (int a = 0; a < tags.Length; a++) {
                tags[a].type = iTag.tUnk;
                tags[a].count = 0;
            }
        }
    }
    public class iTag {
        public class cmpTag : IComparer {
            int IComparer.Compare(object x, object y) {
                iTag a = (iTag)x; iTag b = (iTag)y;
                CaseInsensitiveComparer cic =
                    new CaseInsensitiveComparer();
                return cic.Compare(a.tag, b.tag);
            }
        }
        public const int tUnk = 0;
        public const int tGen = 1;
        public const int tSrc = 2;
        public const int tChr = 3;
        public const int tArt = 4;
        public const int tJnk = 5;
        public int type;
        public int count;
        public string tag;
        public iTag(string tag, int type, int count) {
            this.count = count;
            this.type = type;
            this.tag = tag;
        }
    }
    public class iTagCollection {
        ArrayList tags;
        public iTagCollection() {
            Wipe();
        }
        void Wipe() {
            tags = new ArrayList();
        }
        public bool Add(iTag ntag) {
            ntag.tag = ntag.tag.Trim();
            if (ntag.tag == "") return false;
            int ofs = tags.BinarySearch(ntag,
                new iTag.cmpTag());
            if (ofs > -1) {
                iTag ctag = (iTag)tags[ofs];
                ctag.count += ntag.count;
            } else {
                tags.Insert(0 - 1 - ofs, ntag);
            }
            return true;
        }
        public iTag[] Get() {
            iTag[] ret = new iTag[tags.Count];
            for (int a = 0; a < ret.Length; a++) {
                ret[a] = (iTag)tags[a];
            }
            return ret;
        }
        public iTag[] Get(int type) {
            iTag[] all = Get(); int cnt = 0;
            for (int a = 0; a < all.Length; a++)
                if (all[a].type == type) cnt++;
            iTag[] ret = new iTag[cnt]; cnt = 0;
            for (int a = 0; a < all.Length; a++)
                if (all[a].type == type) {
                    ret[cnt] = all[a]; cnt++;
                }
            return ret;
        }
        public bool Add(iTag[] tags) {
            for (int a = 0; a < tags.Length; a++) {
                Add(tags[a]);
            }
            return true;
        }
        public bool Add(string[] tags, int type) {
            //lol in-place mergesort imitation
            iTagCollection tc = new iTagCollection();
            for (int a = 0; a < tags.Length; a++)
                tc.Add(new iTag(tags[a], type, 1));
            Add(tc); //...doubles the speed \o/
            return true;
        }
        public bool Add(iTagCollection tags) {
            return Add(tags.Get());
        }
        public bool Load() {
            Wipe();
            if (!File.Exists("tags.db")) return false;
            string[] saTags = File.ReadAllText("tags.db",
                System.Text.Encoding.UTF8).Replace(
                "\r", "").Trim('\n').Split('\n');
            for (int a = 0; a < saTags.Length; a++) {

                int ofs = saTags[a].IndexOf(".");
                int type = Convert.ToInt32(
                    saTags[a].Substring(0, ofs));
                saTags[a] = saTags[a].Substring(ofs + 1);

                ofs = saTags[a].IndexOf(".");
                int cnt = Convert.ToInt32(
                    saTags[a].Substring(0, ofs));
                saTags[a] = saTags[a].Substring(ofs + 1);

                saTags[a] = saTags[a];
                Add(new iTag(saTags[a], type, cnt));
            }
            return true;
        }
        public bool LoadAppend() {
            iTagCollection iTC = new iTagCollection();
            iTC.Load();
            iTag[] iT = iTC.Get();
            for (int a = 0; a < iT.Length; a++) {
                SetCat(iT[a], -1);
            }
            return false;
        }
        public bool Save() {
            System.Text.StringBuilder sb = new
                System.Text.StringBuilder();
            for (int a = 0; a < tags.Count; a++) {
                iTag iTag = (iTag)tags[a];
                sb.Append(iTag.type + "." +
                    iTag.count + "." +
                    iTag.tag + "\r\n");
            }
            File.WriteAllText("tags.db", sb.ToString(),
                System.Text.Encoding.UTF8);
            return true;
        }
        public bool Load(ListBox lb, int type) {
            lb.Items.Clear();
            for (int a = 0; a < tags.Count; a++) {
                iTag iTag = (iTag)tags[a];
                if (iTag.count > 0 &&
                    iTag.type == type)
                    lb.Items.Add(iTag.tag);
            }
            return true;
        }
        public bool Save(ListBox lb, int type) {
            iTag[] lbTags = new iTag[lb.Items.Count];
            for (int a = 0; a < lbTags.Length; a++)
                lbTags[a] = new iTag((string)
                    lb.Items[a], type, 1);
            return Add(lbTags);
        }
        public bool SetCat(iTag ncat, int from) {
            int ofs = tags.BinarySearch(
                ncat, new iTag.cmpTag());
            if (ofs > -1) {
                iTag itag = (iTag)tags[ofs];
                if (from > -1 &&
                    itag.type != from)
                    return false;
                if (itag.type == ncat.type)
                    return false;
                tags.Remove(itag);
                itag.type = ncat.type;
                Add(itag); return true;
            } else {
                ncat.count = 0;
                Add(ncat);
                return false;
            }
        }
    }
}