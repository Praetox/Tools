using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NowPlaying {
    public partial class frmSelectAccount : Form {
        public frmSelectAccount() {
            InitializeComponent();
        }
        public string usr, pwd;
        string dom = "http://yuki.praetox.com/sig/index.php";
        //string dom = "http://192.168.0.100/sig/index.php";
        private void frmSelectAccount_Load(object sender, EventArgs e) {
            this.Icon = frmMain.ico;
            this.Show(); Application.DoEvents();
            if (System.IO.File.Exists("login.txt")) {
                string[] vars = System.IO.File
                    .ReadAllText("login.txt")
                    .Replace("\r", "").Split('\n');
                txtUser.Text = vars[0];
                txtPass.Text = vars[1];
                cmdGo_Click(sender, e);
            }
        }
        private void label3_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        string mask = "abcdefghijklmnopqrstuvwxyz" +
                      "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                      "0123456789";
        private void cmdGo_Click(object sender, EventArgs e) {
            this.Enabled = false;
            usr = Filter(txtUser.Text,mask);
            pwd = Filter(txtPass.Text,mask);
            try {
                System.Net.WebClient wc = new System.Net.WebClient();
                string ret = wc.DownloadString(dom + "?" +
                    "usr=" + usr + "&pwd=" + pwd);
                if (ret.Contains("<!-- VER") &&
                    !ret.Contains("<!-- VER " +
                    Application.ProductVersion + " -->"))
                    MessageBox.Show("There's a new version available." +
                        "\r\n\r\n" + "Get it at http://praetox.com/");

                if (ret.Contains("Logged in")) {
                    if (chkRemember.Checked) System.IO.File.WriteAllText
                        ("login.txt", usr + "\r\n" + pwd); this.Close();
                }
                else MessageBox.Show("Incorrect username or password!");
            }
            catch {
                MessageBox.Show("Couldn't connect to server!" + "\r\n\r\n" +
                    "Possible reasons:" + "\r\n" +
                    "- Your internet connection is dead" + "\r\n" +
                    "- Praetox's internet connection is dead" + "\r\n" +
                    "- Praetox has given up on the project" + "\r\n" +
                    "- There's a new version available." + "\r\n\r\n" +
                    "Anyhow, check http://praetox.com/");
            }
            this.Enabled = true;
        }
        private string Filter(string str, string mask) {
            string ret = "";
            for (int a = 0; a < str.Length; a++)
                if (mask.Contains(str[a] + ""))
                    ret += str[a] + "";
            return ret;
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) cmdGo_Click(sender, e);
        }
        private void txtPass_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) cmdGo_Click(sender, e);
        }
        private void register_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://yuki.praetox.com/sig/reg.php");
        }
    }
}
