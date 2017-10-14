using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Preenshot
{
    public partial class frmPostCrop : Form
    {
        public frmPostCrop(Bitmap b)
        {
            InitializeComponent();
            this.BackgroundImage = b;
            ptScrn = new Point(
                Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height);
        }

        Point ptScrn; frmPostCropF frm;
        public Rectangle ret;

        private void frmPostCrop_Load(object sender, EventArgs e)
        {
            this.Location = Point.Empty;
            this.Size = Screen.PrimaryScreen.Bounds.Size;
            this.TopMost = true; frm = new frmPostCropF();
            Application.DoEvents(); this.Show();
        }

        private void frmPostCrop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                frm.ptLoc1 = new Point(e.X + 0, e.Y + 0);
                frm.ptLoc2 = new Point(e.X + 0, e.Y + 0);
                frm.ptOffs = new Point(
                    Cursor.Position.X - e.X,
                    Cursor.Position.Y - e.Y);
            }
        }

        private void frmPostCrop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point pt = frm.ptLoc1;
                int iX = e.X; int iY = e.Y;
                int iXD = (int)Math.Abs(pt.X - iX);
                int iYD = (int)Math.Abs(pt.Y - iY);
                if (iXD > 4 || iYD > 4)
                {
                    frm.ptLoc2 = new Point(iX, iY);
                    frm.bShow = true;
                }
            }
        }

        private void frmPostCrop_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (frm.bShow)
                {
                    frm.bShow = false;
                    Point pt1 = frm.ptLoc1;
                    Point pt2 = frm.ptLoc2;
                    ret = new Rectangle(
                        Math.Min(pt1.X, pt2.X),
                        Math.Min(pt1.Y, pt2.Y),
                        Math.Max(pt1.X, pt2.X),
                        Math.Max(pt1.Y, pt2.Y));
                    ret.Width -= ret.Left;
                    ret.Height -= ret.Top;
                    this.Close();
                }
            }
        }
    }
}
