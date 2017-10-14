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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ShittySort {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
        }
        private imInfo[] inf;
        private iTagCollection itc;
        iTagCollection getTags(string fn, string mask,
            string rpl1, string rpl2, string splitBy) {
            string tagsUnk = "";
            string tagsGen = "";
            string tagsSrc = "";
            string tagsChr = "";
            string tagsArt = "";
            //remove extension
            fn = fn.Substring(0,
                fn.LastIndexOf("."));
            while (true) {
                string metaType = "";
                Point nextMeta = GetNextMeta(mask);
                if (nextMeta.Y == 0) {
                    //no more meta
                    break;
                }
                if (nextMeta.X > 0) {
                    //Non-meta information at start
                    if (mask.Substring(0, nextMeta.X) !=
                        fn.Substring(0, nextMeta.X)) {
                        throw new Exception("Mask syntax error");
                    }
                }
                metaType = mask.Substring(nextMeta.X, nextMeta.Y);
                mask = mask.Substring(nextMeta.X + nextMeta.Y);
                fn = fn.Substring(nextMeta.X);
                //We're at some meta, find end offset
                nextMeta = GetNextMeta(mask);
                string meta = fn; //assume rest of filename
                if (mask.Length > 0) {
                    //naw dude, bad assumption
                    string delim = mask;
                    if (nextMeta.X > 0) {
                        //more meta tags in mask
                        delim = mask.Substring(0, nextMeta.X);
                    }
                    int iMetaDelim = fn.IndexOf(delim);
                    if (iMetaDelim == -1) throw new
                        Exception("Mask syntax error");
                    meta = fn.Substring(0, iMetaDelim);
                }
                //Now we remove meta from fn
                fn = fn.Substring(meta.Length);
                metaType = metaType.Trim('{', '}');
                if (metaType == "tags") tagsUnk += meta + "\n";
                if (metaType == "tagsGen") tagsGen += meta + "\n";
                if (metaType == "tagsSrc") tagsSrc += meta + "\n";
                if (metaType == "tagsChr") tagsChr += meta + "\n";
                if (metaType == "tagsArt") tagsArt += meta + "\n";
            }
            iTagCollection ret = new iTagCollection();
            //This isn't pretty, you might want to close your eyes.
            ret.Add(SplitTags(tagsUnk, rpl1, rpl2, splitBy), iTag.tUnk);
            ret.Add(SplitTags(tagsGen, rpl1, rpl2, splitBy), iTag.tGen);
            ret.Add(SplitTags(tagsSrc, rpl1, rpl2, splitBy), iTag.tSrc);
            ret.Add(SplitTags(tagsChr, rpl1, rpl2, splitBy), iTag.tChr);
            ret.Add(SplitTags(tagsArt, rpl1, rpl2, splitBy), iTag.tArt);
            return ret;
        }
        private Point GetNextMeta(string str) {
            return GetNextMeta(str, Point.Empty);
        }
        private Point GetNextMeta(string str, Point fromMeta) {
            int i = -1;
            int i1 = str.IndexOf("{junk", fromMeta.X + fromMeta.Y);
            int i2 = str.IndexOf("{tags", fromMeta.X + fromMeta.Y);

            //...if neither junk or tags
            if (i1 == -1 && i2 == -1) return Point.Empty;
            //...if either junk or tags
            if (i1 == -1) i = i2;
            if (i2 == -1) i = i1;
            //...if both junk and tags
            if (i == -1) i = Math.Min(i1, i2);

            int j = str.IndexOf("}", i + 1);
            return new Point(i, j + 1 - i);
        }
        private string[] SplitTags(string tags,
            string rpl1, string rpl2, string splitBy) {
            tags = tags.Replace(splitBy, "\n");
            while (tags.Contains("\n\n"))
                tags = tags.Replace("\n\n", "\n");
            string[] aTags = tags.Split('\n');
            for (int a = 0; a < aTags.Length; a++)
                for (int b = 0; b < rpl1.Length; b++)
                    aTags[a] = aTags[a].Replace(
                        rpl1[b] + "", rpl2[b] + "");
            return aTags;
        }

        private void guRootB_Click(object sender, EventArgs e) {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog(); string path = fbd.SelectedPath;
            if (path != "") guRoot.Text = path;
        }
        private void guRootOK_Click(object sender, EventArgs e) {
            gugRoot.Enabled = false;
            gugMask.Enabled = true;
        }
        private void guMaskOK_Click(object sender, EventArgs e) {
            MessageBox.Show("If you have categorized tags earlier, remember\r\n" +
                "to click \"Load tag cat.\" once the cat window\r\n" +
                "has finished loading.", "Tip");
            iTagCollection tc = new
                iTagCollection();
            gugMask.Enabled = false;
            string mask = guMask.Text;
            string rpl1 = guReplace.Text;
            string rpl2 = guReplace2.Text;
            string splitBy = guSplit.Text;
            string[] files;
            if (guRoot.Text != "list.txt") {
                files = cb.getFiles(guRoot.Text);
            } else {
                files = System.IO.File.ReadAllText("list.txt",
                    Encoding.UTF8).Replace("\r", "").
                    Trim('\n').Split('\n');
            }
            string[] tagFn = files;
            for (int a = 0; a < tagFn.Length; a++) {
                int ofs = tagFn[a].LastIndexOf("\\");
                tagFn[a] = tagFn[a].Substring(ofs + 1);
            }
            proa.Maximum = tagFn.Length;
            inf = new imInfo[files.Length];
            
            for (int a = 0; a < tagFn.Length; a++) {
                if (a % 250 == 0) {
                    proa.Value = a;
                    prob.Text = "Parsing file " +
                        (a + 1) + " of " + tagFn.Length;
                    Application.DoEvents();
                }
                iTag[] it = getTags(tagFn[a], mask,
                    rpl1, rpl2, splitBy).Get();
                tc.Add(it);
                inf[a] = new imInfo();
                inf[a].path = files[a];
                inf[a].tags = new iTag[it.Length];
                for (int b = 0; b < it.Length; b++) {
                    inf[a].tags[b] = new iTag(
                        it[b].tag, iTag.tUnk, 0);
                }
            }
            int proas = proa.Maximum / 3;

            prob.Text = "Reading categories";
            proa.Value = proas * 1;
            Application.DoEvents();
            tc.LoadAppend(); //Read db

            prob.Text = "Saving new tags.db";
            proa.Value = proas * 2;
            Application.DoEvents();
            tc.Save(); //Store tags.db

            prob.Text = "Preparing GUI";
            proa.Value = proa.Maximum;
            Application.DoEvents();
            doCat(tc, false);
        }

        private void guResume_Click(object sender, EventArgs e) {
            gugRoot.Enabled = false;
            prob.Text = "Validating data...";
            proa.Value = 33;
            Application.DoEvents();

            //Load stored tag counts
            iTagCollection tc = new iTagCollection();
            tc.Load();

            prob.Text = "Preparing GUI...";
            proa.Value = 66;
            Application.DoEvents();

            doCat(tc, true);
        }

        private void doCat(iTagCollection tc, bool ragequit) {
            frmTagtype fTags = new frmTagtype(tc, ragequit);
            fTags.ShowDialog();

            prob.Text = "Ready to organize images";
            proa.Value = 0;
            gugMask.Enabled = false;
            gugOrganize.Enabled = true;
            itc = tc;
        }

        private void frmMain_Load(object sender, EventArgs e) {
            MessageBox.Show("This application is still in beta.\r\n\r\n" +
                "Remember to keep several backups of your tags.db\r\n" +
                "in case I fucked up somewhere. Enjoy sortan gaems.");
        }
        private void button2_Click(object sender, EventArgs e) {
            new frmReorder(itc, inf).ShowDialog();
        }
    }
}
