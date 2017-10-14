using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace NowPlaying {
    public partial class frmSelectPlayer : Form {
        public frmSelectPlayer() {
            InitializeComponent();
        }

        public string appname = "";
        public string appmask = "";
        private void frmSelectPlayer_Load(object sender, EventArgs e) {
            this.Icon = frmMain.ico;
            if (System.IO.File.Exists("player.txt")) {
                string[] vars = System.IO.File
                    .ReadAllText("player.txt")
                    .Replace("\r", "").Split('\n');
                appname = vars[0]; appmask = vars[1];
                this.Close();
            }
        }
        private void label1_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void iTunes_Click(object sender, EventArgs e) {
            SetMemory("itunes");
        }
        private void SpotifyMem_Click(object sender, EventArgs e) {
            SetMemory("spotify");
        }
        private void SetMemory(string sName) {
            if (Process.GetProcessesByName(sName).Length > 0) {
                MemProc.Text = sName;
            }
        }
        private void memContinue_Click(object sender, EventArgs e) {
            appname = MemProc.Text; appmask = ""; this.Close();
        }

        private void Foobar_Click(object sender, EventArgs e) {
            SetParse("foobar2000");
        }
        private void Winamp_Click(object sender, EventArgs e) {
            SetParse("winamp");
        }
        private void Spotify_Click(object sender, EventArgs e) {
            SetParse("spotify");
        }
        private void SetParse(string var) {
            if (Process.GetProcessesByName(var).Length > 0) {
                txtProc.Text = var;
                if (var == "foobar2000") txtMask.Text = "%filename% - [%album% #%trackno%] %title% // %artist%   [foobar2000";
                if (var == "winamp") txtMask.Text = "%trackno%. %artist% - %title% - Winamp";
                if (var == "spotify") txtMask.Text = "Spotify - %artist% – %title%";
                if (var == "xmplay") txtMask.Text = "%artist% - %title%";
            }
        }
        private void cmdContinue_Click(object sender, EventArgs e) {
            appname = txtProc.Text;
            appmask = txtMask.Text;
            if (chkRemember.Checked) {
                System.IO.File.WriteAllText("player.txt",
                    appname + "\r\n" + appmask);
            }
            this.Close();
        }
    }
}
