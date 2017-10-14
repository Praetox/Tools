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
using System.Collections;

namespace ShittySort {
    public partial class frmTagtype : Form {
        public frmTagtype(iTagCollection tc, bool ragequit) {
            InitializeComponent();
            this.ragequit = ragequit;
            this.tc = tc;
        }
        private bool ragequit;
        private iTagCollection tc;
        private ListBox[] lb;
        private History undo;

        private void frmTagtype_Load(object sender, EventArgs e) {
            lb = new ListBox[6];
            lb[iTag.tUnk] = gtUnk; //Unknown
            lb[iTag.tGen] = gtGen; //General
            lb[iTag.tSrc] = gtSrc; //Source
            lb[iTag.tChr] = gtChr; //Chara
            lb[iTag.tArt] = gtArt; //Artist
            lb[iTag.tJnk] = gtJnk; //Junk
            LoadFromTC();
        }
        private void LoadFromTC() {
            for (int a = 0; a < lb.Length; a++) {
                lb[a].Visible = false;
                Application.DoEvents();
                tc.Load(lb[a], a); //Load to LB
                lb[a].Visible = true;
            }
            undo = new History();
            RefreshCounts();
        }
        private void RefreshCounts() {
            for (int a = 0; a < lb.Length; a++) {
                string txt = lb[a].Parent.Text;
                txt = txt.Substring(0, txt.IndexOf("("));
                lb[a].Parent.Text = txt + "(" +
                    lb[a].Items.Count + ")";
            }
        }
        private void stat(string str) {
            gStatus.Text = ":: " + str;
            Application.DoEvents();
        }

        private void MoveTag(int from, Keys to) {
            int iMoveTo = -1;
            string tag = (string)lb
                [from].SelectedItem;
            if (to == Keys.Enter) iMoveTo = 0;
            if (to == Keys.F1) iMoveTo = 1;
            if (to == Keys.F2) iMoveTo = 2;
            if (to == Keys.F3) iMoveTo = 3;
            if (to == Keys.F4) iMoveTo = 4;
            if (to == Keys.F5) iMoveTo = 5;
            if (iMoveTo != -1) {
                MoveTag(tag, from, iMoveTo);
            }
            if (to == Keys.F9) {
                gSave_Click(new object(),
                    new EventArgs());
            }
            if (to == Keys.Escape) {
                HEvent he = undo.Undo();
                if (he.tag == "") return;
                MoveTag(he.tag, he.to, he.from, he.ofs);
                for (int a = 0; a < lb.Length; a++) {
                    lb[a].SelectedIndex = -1;
                }
                lb[he.from].SelectedIndex = he.ofs;
                lb[he.from].Select();
            }
        }
        private void MoveTag(string tag, int from, int to) {
            MoveTag(tag, from, to, -1);
        }
        private void MoveTag(string tag, int from, int to, int toOfs) {
            if (from == to) return;
            if (!tc.SetCat(new iTag(tag, to, 0), from)) {
                MessageBox.Show("Integrity issue in iTagCollection",
                    "FUCK FUCK FUCK OH SHIT FUCK", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            int iMoved = 0;
            for (int a = 0; a < lb[from].Items.Count; a++) {
                if (tag == (string)lb[from].Items[a]) {
                    lb[from].Items.RemoveAt(a); iMoved++;
                    lb[to].Items.Insert(Math.
                        Max(toOfs, 0), tag);

                    if (a < lb[from].Items.Count)
                        lb[from].SelectedIndex = a;
                    else lb[from].SelectedIndex =
                        lb[from].Items.Count - 1;

                    if (toOfs == -1) undo.Add(new
                        HEvent(a, tag, from, to));
                    a--;
                }
            }
            if (iMoved != 1) {
                MessageBox.Show("Did " + iMoved + " moves, 1 expected",
                    "FUCK FUCK FUCK OH SHIT FUCK", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            RefreshCounts();
        }

        private void gSave_Click(object sender, EventArgs e) {
            stat("Saving ..."); this.Enabled = false;
            tc.Save(); //Save tags database
            stat("Ready."); this.Enabled = true;
        }
        private void gContinue_Click(object sender, EventArgs e) {
            if (!ragequit) this.Close();
            else {
                MessageBox.Show("SS can't continue from this state.\r\n\r\n" +
                    "When you chose \"Resume sorting\" in the main window,\r\n" +
                    "SS only loaded the tags and not the filenames. Thus,\r\n" +
                    "SS doesn't know which tags belong to which image.\r\n\r\n" +
                    "To start organizing images,\r\n" +
                    "- Click \"Save to disk\" and \"Quit\"\r\n" +
                    "- Start SS, select directory with images\r\n" +
                    "- Click \"Select this folder\"\r\n" +
                    "- Fill in the name mask etc, click \"Start\"\r\n" +
                    "- Click the \"Load tag cat.\" button\r\n" +
                    "- Click \"Close and Continue\"", "tl;dr lol");
            }
        }
        private void gLoad_Click(object sender, EventArgs e) {
            this.Enabled = false;
            stat("Loading tags ...");
            tc.Load();
            stat("Loading GUI ...");
            this.Enabled = true;
            LoadFromTC();
            stat("Ready.");
        }
        private void gCancel_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void gtUnk_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control || e.Alt || e.Shift) return;
            MoveTag(iTag.tUnk, e.KeyCode);
        }
        private void gtGen_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control || e.Alt || e.Shift) return;
            MoveTag(iTag.tGen, e.KeyCode);
        }
        private void gtSrc_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control || e.Alt || e.Shift) return;
            MoveTag(iTag.tSrc, e.KeyCode);
        }
        private void gtChr_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control || e.Alt || e.Shift) return;
            MoveTag(iTag.tChr, e.KeyCode);
        }
        private void gtArt_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control || e.Alt || e.Shift) return;
            MoveTag(iTag.tArt, e.KeyCode);
        }
        private void gtJnk_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control || e.Alt || e.Shift) return;
            MoveTag(iTag.tJnk, e.KeyCode);
        }
        private void gGen_Click(object sender, EventArgs e) {
            gtUnk_KeyDown(sender, new KeyEventArgs(Keys.F1));
        }
        private void gSrc_Click(object sender, EventArgs e) {
            gtUnk_KeyDown(sender, new KeyEventArgs(Keys.F2));
        }
        private void gChr_Click(object sender, EventArgs e) {
            gtUnk_KeyDown(sender, new KeyEventArgs(Keys.F3));
        }
        private void gArt_Click(object sender, EventArgs e) {
            gtUnk_KeyDown(sender, new KeyEventArgs(Keys.F4));
        }
        private void gJnk_Click(object sender, EventArgs e) {
            gtUnk_KeyDown(sender, new KeyEventArgs(Keys.F5));
        }
    }
    public class History {
        ArrayList al;
        public History() {
            al = new ArrayList();
        }
        public void Add(HEvent he) {
            al.Add(he);
        }
        public HEvent Undo() {
            if (al.Count == 0)
                return new HEvent(
                    0, "", 0, 0);

            HEvent ret = (HEvent)
                al[al.Count - 1];
            al.RemoveAt(al.Count - 1);
            return ret;
        }
    }
    public class HEvent {
        public int ofs;
        public string tag;
        public int from;
        public int to;
        public HEvent(int ofs, string tag, int from, int to) {
            this.ofs = ofs;
            this.tag = tag;
            this.from = from;
            this.to = to;
        }
    }
}
