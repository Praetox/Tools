using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pImgDB
{
    public partial class frmZoom : Form
    {
        public frmZoom()
        {
            InitializeComponent();
        }

        private static bool bTimer = false;
        private static bool bShowing = true;
        public static bool bShow = false;
        public static string sLabel = "";
        public static Point ptOffs = new Point(0, 0);
        public static Point ptLoc1 = new Point(0, 0);
        public static Point ptLoc2 = new Point(1, 1);

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

            if (sLabel != "")
            {
                lbZoom.Text = sLabel;
                Application.DoEvents();
                sLabel = "";
            }

            int iX1 = (int)Math.Min(ptLoc1.X, ptLoc2.X) + ptOffs.X;
            int iX2 = (int)Math.Max(ptLoc1.X, ptLoc2.X) + ptOffs.X;
            int iY1 = (int)Math.Min(ptLoc1.Y, ptLoc2.Y) + ptOffs.Y;
            int iY2 = (int)Math.Max(ptLoc1.Y, ptLoc2.Y) + ptOffs.Y;
            this.Location = new Point(iX1, iY1);
            this.Size = new Size(iX2 - iX1, iY2 - iY1);
            bTimer = false;
        }

        private void fHide()
        {
            for (int a = 20; a > 0; a--)
            {
                this.Opacity = (double)a / 40;
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
            this.Hide(); lbZoom.Text = "zoom";
        }
    }
}
