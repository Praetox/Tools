using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Preenshot
{
    public partial class frmPostCropF : Form
    {
        public frmPostCropF()
        {
            InitializeComponent();
        }

        private bool bTimer = false;
        private bool bShowing = true;
        public bool bShow = false;
        public string sLabel = "";
        public Point ptOffs = new Point(0, 0);
        public Point ptLoc1 = new Point(0, 0);
        public Point ptLoc2 = new Point(1, 1);

        private void tMove_Tick(object sender, EventArgs e)
        {
            if (bTimer) return; bTimer = true;
            if (bShow != bShowing)
            {
                if (bShow) this.Opacity = 0.5;
                if (bShow) this.Show();
                if (!bShow) fHide();
                bShowing = bShow;
            }

            int iX1 = (int)Math.Min(ptLoc1.X, ptLoc2.X) + ptOffs.X;
            int iX2 = (int)Math.Max(ptLoc1.X, ptLoc2.X) + ptOffs.X;
            int iY1 = (int)Math.Min(ptLoc1.Y, ptLoc2.Y) + ptOffs.Y;
            int iY2 = (int)Math.Max(ptLoc1.Y, ptLoc2.Y) + ptOffs.Y;
            this.Location = new Point(iX1, iY1);
            this.Size = new Size(iX2 - iX1, iY2 - iY1);
            Application.DoEvents();
            bTimer = false;
        }

        private void fHide()
        {
            for (int a = 15; a > 0; a--)
            {
                this.Opacity = (double)a / 20;
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
            this.Hide();
        }

        private void frmPostCropF_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}
