using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PremoClie {
    public partial class frmLogin : Form {
        public frmLogin() {
            InitializeComponent();
        }

        public Client cli;
        private void frmLogin_Load(object sender, EventArgs e) {
            int iLast = -1;
            guProfile.SelectedIndex = -1;
            string[] profiles = System.IO
                .Directory.GetFiles(".", "*.cfg");
            for (int a = 0; a < profiles.Length; a++) {
                string s = profiles[a].Substring(2);
                s = s.Substring(1, s.Length - 5);
                if (s == "last") iLast = a;
                guProfile.Items.Add(s);
            }
            if (iLast >= 0) {
                guProfile.SelectedIndex = iLast;
            }
        }
        private void guConnect_Click(object sender, EventArgs e) {
            status("Connecting to server");
            Client cli = new Client(guServer.Text,
                Convert.ToInt32(guPort.Text));
            status("Authenticating");
            if (cli.Auth(guUser.Text, guPass.Text)) {
                status("Auth OK!");
                this.cli = cli;
                guProfile.Text = "last";
                saveProfile();
                this.Close();
            }
            else {
                status("Wrong user/pwd");
                MessageBox.Show("Invalid credentials!", "Oh snap",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void saveProfile() {
            string name = guProfile.Text;
            string path = "_" + name + ".cfg";
            System.IO.File.WriteAllText(path,
                guServer.Text + "\r\n" +
                guPort.Text + "\r\n" +
                guUser.Text + "\r\n" +
                guPass.Text, Encoding.UTF8);
        }
        private void guSave_Click(object sender, EventArgs e) {
            saveProfile();
        }
        private void guProfile_SelectedIndexChanged(object sender, EventArgs e) {
            loadProfile();
        }
        private void loadProfile() {
            string name = guProfile.Text;
            string path = "_" + name + ".cfg";
            if (System.IO.File.Exists(path)) {
                string[] s = System.IO.File.ReadAllText(path,
                    Encoding.UTF8).Replace("\r", "").Split('\n');
                guServer.Text = s[0];
                guPort.Text = s[1];
                guUser.Text = s[2];
                guPass.Text = s[3];
            }
        }
        private void status(string str) {
            guStatus.Text = str;
            Application.DoEvents();
        }
    }
}
