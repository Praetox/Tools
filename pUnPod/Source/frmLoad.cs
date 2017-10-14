using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pUnPod
{
    public partial class frmLoad : Form
    {
        bool bDriveSelected = false;
        public string podPath = "";
        public frmLoad()
        {
            InitializeComponent();
        }

        private void frmLoad_Load(object sender, EventArgs e) {
            this.Show(); Application.DoEvents();
            cbDrives.Items.Clear();
            for (int a = 0; a < 27; a++) {
                try {
                    System.IO.DriveInfo di = new System.IO.DriveInfo(((char)(64 + a)).ToString());
                    cbDrives.Items.Add(((char)(64 + a)).ToString() + ": " + di.VolumeLabel);
                }
                catch { }
            }
            cbDrives.DroppedDown = true;
        }
        private void frmLoad_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!bDriveSelected) Program.Kill();
        }
        private void cbDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            Timer t = new Timer(); t.Interval = 500;
            t.Tick += delegate(object lol, EventArgs wut) {
                bDriveSelected = true; Application.DoEvents();
                podPath = cbDrives.SelectedItem.ToString().Substring(0, 1);
                this.Close();
            }; t.Start();
        }

        private void label2_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("GPL.txt");
        }
    }
}
