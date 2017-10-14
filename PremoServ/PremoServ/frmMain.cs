using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PremoServ {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
        }

        Server srv;
        string log = "";

        private void guToggleServ_Click(object sender, EventArgs e) {
            int port = Convert.ToInt32(guPort.Text);
            srv = new Server(port);
            srv.user = guUser.Text;
            srv.pass = guPass.Text;
            srv.Start();
            guToggleServ.Enabled = false;
        }

        private void tServerLog_Tick(object sender, EventArgs e) {
            if (srv == null) return;
            string apn = srv.getLog();
            if (apn != "") {
                log = apn + log;
                string[] alog = log.Split('\n');
                
                log = "";
                for (int a = 0; a < 10; a++) {
                    if (a >= alog.Length) break;
                    log += alog[a] + "\n";
                }
                guLog.Text = log.Replace("\n", "\r\n");
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
