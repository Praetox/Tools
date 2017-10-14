using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PremoClie {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
        }

        Keybd kb;
        Mouse mo;
        Client cli;
        public static string head = "";
        private void frmMain_Load(object sender, EventArgs e) {
            Timer tt = new Timer(); tt.Interval = 250;
            tt.Tick += delegate(object oo, EventArgs oe) {
                label2.Text = head;
            }; tt.Start();

            frmLogin fl = new frmLogin();
            fl.ShowDialog();
            if (fl.cli != null) {
                this.cli = fl.cli;
                kb = new Keybd(cli);
                mo = new Mouse(cli);
                mo.setCenter(new Point(
                    this.Location.X + this.Width / 2,
                    this.Location.Y + this.Height / 2));

                Timer t = new Timer(); t.Interval = 250;
                t.Tick += delegate(object oo, EventArgs oe) {
                    if (kb != null) {
                        bool hasFocus = this.Focused;
                        if (kb.active != hasFocus) {
                            kb.active = this.Focused;
                            if (hasFocus) mo.start();
                            if (!hasFocus) mo.stop();
                        }
                    }
                }; t.Start();
            }
            else this.Close();
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
            if (kb != null) kb.Dispose();
        }

        private void frmMain_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) cli.SendLine("MD:L");
            if (e.Button == MouseButtons.Right) cli.SendLine("MD:R");
        }
        private void frmMain_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) cli.SendLine("MU:L");
            if (e.Button == MouseButtons.Right) cli.SendLine("MU:R");
        }
    }
}
