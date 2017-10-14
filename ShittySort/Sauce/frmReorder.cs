using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ShittySort {
    public partial class frmReorder : Form {
        public frmReorder(iTagCollection itc, imInfo[] inf) {
            InitializeComponent();
            this.itc = itc;
            this.inf = inf;
        }
        iTagCollection itc;
        imInfo[] inf;

        private void stat(string str) {
            state.Text = str;
            Application.DoEvents();
        }
        private void frmReorder_Load(object sender, EventArgs e) {
            string msg = "Merging tag categories and images - ";
            this.Show(); this.Enabled = false; stat(msg + "0%");
            double dProc = 100.0 / inf.Length;
            iTag[] db = itc.Get();
            for (int a = 0; a < inf.Length; a++) {
                if (a % 100 == 0) stat(msg + 
                    Math.Round(dProc * a, 1) + "%");
                inf[a].Upgrade(db);
            }
            int b = 7; b *= 3;
        }

        private void gPriority_KeyDown(object sender, KeyEventArgs e) {
            if (!e.Control) return;
            int dir = 0;
            if (e.KeyCode == Keys.Up) dir--;
            if (e.KeyCode == Keys.Down) dir++;
            int ofs = gPriority.SelectedIndex;
            if (dir == 0 || dir + ofs < 0 ||
                dir + ofs >= gPriority.Items.Count)
                return;
            object o = gPriority.Items[ofs];
            gPriority.Items.RemoveAt(ofs);
            gPriority.Items.Insert(ofs + dir, o);
            gPriority.SelectedIndex = ofs;
        }

        private void gTreeR_Click(object sender, EventArgs e) {

        }
    }
}
