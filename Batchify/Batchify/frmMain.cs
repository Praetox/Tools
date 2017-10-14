using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

/*  Batchify -- automate monotonous file operations
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

namespace Batchify {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
        }
        string sAppVer = Application.ProductVersion;
        public static string ToxDomain = "http://praetox.com/";
        public static string PrgDomain = "http://tox.awardspace.us/div/";

        public static string[] Split(string a, string b) {
            return a.Split(new string[] { b },
                StringSplitOptions.None);
        }
        public static bool OnlyContains(string str, string vl) {
            for (int a = 0; a < str.Length; a++)
                if (!vl.Contains("" + str[a])) return false;
            return true;
        }
        public static string ReadFile(string path) {
            if (System.IO.File.Exists(path)) return
                System.IO.File.ReadAllText(path, Encoding.UTF8);
            return "";
        }
        private void pbLogo_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start(ToxDomain);
        }
        private void frmMain_Load(object sender, EventArgs e) {
            sAppVer = sAppVer.Substring(0, sAppVer.LastIndexOf("."));
            this.Text += sAppVer; this.Show(); Application.DoEvents();
            string lodscr = ReadFile("script.txt")
                .Replace("\r\n", "\n");

            if (lodscr.StartsWith("INDIR=")) {
                int ofs = lodscr.IndexOf("\n");
                string tmp = lodscr.Substring(0, ofs);
                tmp = tmp.Substring(6);
                tmp = tmp.Replace("[mydir]",
                    Application.StartupPath);
                txtSource.Text = tmp;
                lodscr = lodscr.Substring(ofs + 1);
            }
            if (lodscr.StartsWith("OUTDIR=")) {
                int ofs = lodscr.IndexOf("\n");
                string tmp = lodscr.Substring(0, ofs);
                tmp = tmp.Substring(7);
                tmp = tmp.Replace("[mydir]",
                    Application.StartupPath);
                txtDestination.Text = tmp;
                lodscr = lodscr.Substring(ofs + 1);
            }
            if (lodscr.StartsWith("INCLUDE=")) {
                int ofs = lodscr.IndexOf("\n");
                string tmp = lodscr.Substring(0, ofs);
                tmp = tmp.Substring(8);
                cbFilter.SelectedIndex = Convert
                    .ToInt32(tmp[0] + "") - 1;
                txtFilter.Text = tmp.Substring(1);
                lodscr = lodscr.Substring(ofs + 1);
            }
            lodscr = lodscr.Trim(' ', '\n').Replace("\n", "\r\n");
            if (lodscr != "") txtScript.Text = lodscr;
            txtScript.Select(0, 0);

            BackgroundWorker bwLoad = new BackgroundWorker();
            bwLoad.DoWork += new DoWorkEventHandler(bwLoad_DoWork);
            bwLoad.RunWorkerAsync();
        }
        void bwLoad_DoWork(object sender, DoWorkEventArgs e) {
            try {
                System.Net.WebClient wc = new System.Net.WebClient();
                string src = wc.DownloadString(PrgDomain +
                    "Batchify_version.php?cv=" + sAppVer);
                if (!src.Contains("<VERSION>" + sAppVer + "</VERSION>")) {
                    string sNewVer = Split(Split(src, "<VERSION>")[1], "</VERSION>")[0];
                    bool GetUpdate = (DialogResult.Yes == MessageBox.Show(
                        "A new version (" + sNewVer + ") is available. Update?",
                        "Updater", MessageBoxButtons.YesNo));
                    if (GetUpdate) {
                        string UpdateLink = new System.Net.WebClient().DownloadString(
                            ToxDomain + "inf/Batchify_link.html").Split('%')[1];
                        System.Diagnostics.Process.Start(UpdateLink + sAppVer);
                        Application.Exit();
                    }
                }
            }
            catch {
                MessageBox.Show("Couldn't check for updates.", "Oh snap");
            }
        }

        private void cmdSource_Click(object sender, EventArgs e) {
            FolderBrowserDialog b = new FolderBrowserDialog();
            b.SelectedPath = Application.StartupPath;
            if (txtSource.Text != "") b.SelectedPath = txtSource.Text;
            b.ShowDialog();
            if (b.SelectedPath != "") {
                txtSource.Text = b.SelectedPath;
                //txtDestination.Text = b.SelectedPath + "\\batch.bat";
                txtSource.SelectionLength = 0;
                txtSource.SelectionStart = txtSource.Text.Length;
                txtSource.ScrollToCaret();
                txtDestination.SelectionLength = 0;
                txtDestination.SelectionStart = txtDestination.Text.Length;
                txtDestination.ScrollToCaret();
            }
        }
        private void cmdDestination_Click(object sender, EventArgs e) {
            SaveFileDialog b = new SaveFileDialog();
            b.AddExtension = true;
            b.AutoUpgradeEnabled = true;
            b.FileName = "batch.bat";
            b.Filter = "Batch file (*.bat)|*.bat";
            b.InitialDirectory = txtSource.Text;
            b.OverwritePrompt = true;
            b.ShowDialog();
            if (b.FileName != "") {
                txtDestination.Text = b.FileName;
                txtDestination.SelectionLength = 0;
                txtDestination.SelectionStart = txtDestination.Text.Length;
                txtDestination.ScrollToCaret();
            }
        }
        private void cmdExecute_Click(object sender, EventArgs e) {
            cmdExecute.Enabled = false;
            Application.DoEvents();

            string ret = "@echo off\r\n\r\n";
            string cmd = txtScript.Text;
            string fltrCond = txtFilter.Text;
            string fltrType = cbFilter.Text;
            bool bCapsMatters = chkCapsMatters.Checked;
            string[] f0 = System.IO.Directory.GetFiles(txtSource.Text);
            string[] f1 = new string[f0.Length];
            string[] f2a = new string[f0.Length];
            string[] f3a = new string[f0.Length];
            string[] f3b = new string[f0.Length];

            if (!bCapsMatters) fltrCond = fltrCond.ToLower();
            for (int a = 0; a < f0.Length; a++) {
                f1[a] = f0[a].Substring(0, f0[a].LastIndexOf("\\"));
                f2a[a] = f0[a].Substring(f1[a].Length + 1);
                if (f2a[a].Contains(".")) {
                    f2a[a] = f2a[a].Substring(0, f2a[a].LastIndexOf("."));
                    f3a[a] = f0[a].Substring(f1[a].Length + 1 + f2a[a].Length);
                    f3b[a] = f3a[a].Substring(1);
                }
                else {
                    f3a[a] = "";
                    f3b[a] = "";
                }
            }

            string stList = ReadFile(txtSource.Text + "\\" + "_BSN_LST.txt");
            if (stList != "") stList = stList.Substring(0, stList.Length - 2);
            string[] sfList = stList.Replace("\r", "").Split('\n');
            string[] f2b = new string[f0.Length];
            for (int a = 0; a < f2a.Length; a++) {
                f2b[a] = f2a[a];
            }
            if (sfList[0] != "") {
                for (int a = 0; a < sfList.Length; a++) {
                    string stfName = sfList[a].Substring(0, 8);
                    for (int b = 0; b < f2a.Length; b++) {
                        if (stfName == f2a[b]) {
                            f2b[b] = sfList[a].Substring(9);
                        }
                    }
                    if (f2b[a] == null) f2b[a] = f2a[a];
                }
            }

            string sToDo = "";
            for (int a = 0; a < f0.Length; a++) {
                bool doThis = false;
                string sTmpCmp = f2a[a] + f3a[a];
                if (!bCapsMatters) sTmpCmp = sTmpCmp.ToLower();
                if (fltrType == "start with") if ((sTmpCmp).StartsWith(fltrCond)) doThis = true;
                if (fltrType == "end with") if ((sTmpCmp).EndsWith(fltrCond)) doThis = true;
                if (fltrType == "contain") if ((sTmpCmp).Contains(fltrCond)) doThis = true;
                if (doThis) sToDo += a + ",";
            }
            string[] saToDo = sToDo.Substring(0, sToDo.Length - 1).Split(',');
            int[] ToDo = new int[saToDo.Length];
            for (int a = 0; a < saToDo.Length; a++) {
                ToDo[a] = Convert.ToInt32(saToDo[a]);
            }

            int iCurrent = 0;
            for (int a = 0; a < f0.Length; a++) {
                bool doThis = false;
                for (int b = 0; b < ToDo.Length; b++)
                    if (a == ToDo[b]) doThis = true;
                if (doThis) {
                    iCurrent++;
                    string retCmd = cmd;
                    while (retCmd.Contains("§(")) {
                        string sTmpCmd = retCmd;
                        sTmpCmd = sTmpCmd.Substring(sTmpCmd.IndexOf("§(") + 2);
                        sTmpCmd = sTmpCmd.Substring(0, sTmpCmd.IndexOf(")§"));
                        string sTmpRpl = sTmpCmd;
                        sTmpRpl = sTmpRpl.Substring(sTmpRpl.IndexOf(",") + 1);
                        sTmpRpl = sTmpRpl.Substring(0, sTmpRpl.LastIndexOf(","));
                        int iTmpSrc = Convert.ToInt32(sTmpCmd.Substring(0, sTmpCmd.IndexOf(",")));
                        int iTmpRpl = Convert.ToInt32(sTmpCmd.Substring(sTmpCmd.LastIndexOf(",") + 1)) - 1;
                        if (iTmpSrc == 1) sTmpRpl = Split(f2a[a], sTmpRpl)[iTmpRpl];
                        if (iTmpSrc == 2) sTmpRpl = Split(f2b[a], sTmpRpl)[iTmpRpl];

                        sTmpCmd = retCmd.Substring(0, retCmd.IndexOf("§("));
                        sTmpCmd += sTmpRpl;
                        sTmpCmd += retCmd.Substring(retCmd.IndexOf(")§") + 2);
                        retCmd = sTmpCmd;
                    }
                    ret += retCmd.Replace("§1", f1[a]).
                                  Replace("§2a", f2a[a]).
                                  Replace("§2b", f2b[a]).
                                  Replace("§3a", f3a[a]).
                                  Replace("§3b", f3b[a]).
                                  Replace("§n1", "" + iCurrent).
                                  Replace("§n2", "" + ToDo.Length) + "\r\n";
                }
            }

            if (!chkCloseConsoleWhenDone.Checked)
                ret += "ECHO ==\\" + "\r\n" +
                       "ECHO ===\\" + "\r\n" +
                       "ECHO ====) All actions completed." + "\r\n" +
                       "ECHO ===/" + "\r\n" +
                       "ECHO ==/" + "\r\n" +
                       "pause";
            System.IO.File.WriteAllText(
                txtDestination.Text, ret,
                Encoding.Default);
            cmdExecute.Enabled = true;
        }
        private void cmdHelp_Click(object sender, EventArgs e) {
            new frmHelp().Show();
        }
        private void cmdUnsafeToSafe_Click(object sender, EventArgs e) {
            if (System.IO.File.Exists(txtSource.Text + "\\" + "~BS_LIST.txt")) {
                if (DialogResult.No == MessageBox.Show(
                    "_BSN_LST.txt (Batchify's safename / realname list) already exist in this folder.\r\n" +
                    "If you proceed, the previously safenamed files will have their real names LOST!\r\n\r\n" +
                    "Do you really want to continue?", "WARNING !", MessageBoxButtons.YesNo))
                    return;
            }

            cmdUnsafeToSafe.Enabled = false;
            Application.DoEvents();

            string ret = ""; int iUnsafe = 0;
            string fltrCond = txtFilter.Text;
            string fltrType = cbFilter.Text;
            bool bCapsMatters = chkCapsMatters.Checked;
            if (!bCapsMatters) fltrCond = fltrCond.ToLower();

            string[] f0 = System.IO.Directory.GetFiles(txtSource.Text);
            string[] f1 = new string[f0.Length];
            string[] f2 = new string[f0.Length];
            string[] f3a = new string[f0.Length];
            string[] f3b = new string[f0.Length];

            for (int a = 0; a < f0.Length; a++) {
                f1[a] = f0[a].Substring(0, f0[a].LastIndexOf("\\"));
                f2[a] = f0[a].Substring(f1[a].Length + 1);
                if (f2[a].Contains(".")) {
                    f2[a] = f2[a].Substring(0, f2[a].LastIndexOf("."));
                    f3a[a] = f0[a].Substring(f1[a].Length + 1 + f2[a].Length);
                    f3b[a] = f3a[a].Substring(1);
                }
                else {
                    f3a[a] = "";
                    f3b[a] = "";
                }

                bool doThis = false;
                string sTmpCmp = f2[a] + f3a[a];
                if (!bCapsMatters) sTmpCmp = sTmpCmp.ToLower();
                if (fltrType == "start with") if ((sTmpCmp).StartsWith(fltrCond)) doThis = true;
                if (fltrType == "end with") if ((sTmpCmp).EndsWith(fltrCond)) doThis = true;
                if (fltrType == "contain") if ((sTmpCmp).Contains(fltrCond)) doThis = true;

                if (doThis) {
                    if (!OnlyContains(f2[a],
                        "abcdefghijklmnopqrstuvwxyz" +
                        "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                        "1234567890_-+=()%¤$#£\"@!") || f2[a].Length > 8) {
                        iUnsafe++;
                        string sUnsafe = "" + iUnsafe;
                        while (sUnsafe.Length < 5) sUnsafe = "0" + sUnsafe;
                        ret += "~BS" + sUnsafe + " " + f2[a] + "\r\n";
                        System.IO.File.Move(
                            f1[a] + "\\" + f2[a] + f3a[a],
                            f1[a] + "\\" + "~BS" + sUnsafe + f3a[a]);
                    }
                }
            }

            System.IO.File.WriteAllText(txtSource.Text +
                "\\" + "~BS_LIST.txt", ret, Encoding.UTF8);
            cmdUnsafeToSafe.Enabled = true;
        }
        private void cmdSafeToUnsafe_Click(object sender, EventArgs e) {
            cmdSafeToUnsafe.Enabled = false;
            Application.DoEvents();

            string[] f0 = System.IO.Directory.GetFiles(txtSource.Text);
            string[] f1 = new string[f0.Length];
            string[] f2 = new string[f0.Length];
            string[] f3a = new string[f0.Length];
            string[] f3b = new string[f0.Length];

            string stList = ReadFile(txtSource.Text + "\\" + "~BS_LIST.txt");
            if (stList != "") stList = stList.Substring(0, stList.Length - 2);
            string[] f2b = stList.Replace("\r", "").Split('\n');

            for (int a = 0; a < f0.Length; a++) {
                f1[a] = f0[a].Substring(0, f0[a].LastIndexOf("\\"));
                f2[a] = f0[a].Substring(f1[a].Length + 1);
                if (f2[a].Contains(".")) {
                    f2[a] = f2[a].Substring(0, f2[a].LastIndexOf("."));
                    f3a[a] = f0[a].Substring(f1[a].Length + 1 + f2[a].Length);
                    f3b[a] = f3a[a].Substring(1);
                }
                else {
                    f3a[a] = "";
                    f3b[a] = "";
                }

                if (f2[a].Length == 8 && f2[a].StartsWith("~BS")) {
                    for (int b = 0; b < f2b.Length; b++) {
                        if (f2b[b].Substring(0, 8) == f2[a]) {
                            System.IO.File.Move(
                                f1[a] + "\\" + f2[a] + f3a[a],
                                f1[a] + "\\" + f2b[b].Substring(9) + f3a[a]);
                        }
                    }
                }
            }

            System.IO.File.Delete(txtSource.Text + "\\" + "~BS_LIST.txt");
            cmdSafeToUnsafe.Enabled = true;
        }
    }
}
