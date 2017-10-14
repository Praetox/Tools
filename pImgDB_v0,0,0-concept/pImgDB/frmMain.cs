using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pImgDB
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        bool bViewingImage = false;
        Point ptThumbLoc = new Point(0, 0);
        Size szThumbSize = new Size(0, 0);

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            if (!bViewingImage)
            {
                bViewingImage = true;
                ptThumbLoc = panel3.Location;
                szThumbSize = panel3.Size;
                panel3.BringToFront();
                int iStep = 1;
                Point p1 = panel3.Location;
                Size s1 = panel3.Size;
                Size s2 = pThumbs.Size;
                double dIX1 = -p1.X + 3;
                double dIX2 = (pThumbs.Width - s1.Width) - 6;
                double dIY1 = -p1.Y + 3;
                double dIY2 = (pThumbs.Height - s1.Height) - 6;
                double dX1 = p1.X;
                double dX2 = s1.Width;
                double dY1 = p1.Y;
                double dY2 = s1.Height;
                dIX1 = dIX1 / iStep; dIX2 = dIX2 / iStep; dIY1 = dIY1 / iStep; dIY2 = dIY2 / iStep;

                for (int a = 0; a < iStep; a++)
                {
                    dX1 += dIX1; dX2 += dIX2; dY1 += dIY1; dY2 += dIY2;
                    panel3.Location = new Point((int)dX1, (int)dY1);
                    pictureBox2.Size = new Size(((int)dX2) - (7 * 2), ((int)dY2) - (3 * 2));
                    panel3.Size = new Size((int)dX2, (int)dY2);
                    Application.DoEvents(); //System.Threading.Thread.Sleep(10);
                }
            }
            else
            {
                panel3.BringToFront();
                int iStep = 1;
                Point p1 = panel3.Location;
                Size s1 = panel3.Size;
                Size s2 = pThumbs.Size;
                double dIX1 = ptThumbLoc.X- p1.X;
                double dIX2 = szThumbSize.Width-s1.Width;
                double dIY1 = ptThumbLoc.Y-p1.Y;
                double dIY2 = szThumbSize.Height-s1.Height;
                double dX1 = p1.X;
                double dX2 = s1.Width;
                double dY1 = p1.Y;
                double dY2 = s1.Height;
                dIX1 = dIX1 / iStep; dIX2 = dIX2 / iStep; dIY1 = dIY1 / iStep; dIY2 = dIY2 / iStep;

                for (int a = 0; a < iStep; a++)
                {
                    dX1 += dIX1; dX2 += dIX2; dY1 += dIY1; dY2 += dIY2;
                    panel3.Size = new Size((int)dX2, (int)dY2);
                    pictureBox2.Size = new Size(((int)dX2) - (7 * 2), ((int)dY2) - (3 * 2));
                    panel3.Location = new Point((int)dX1, (int)dY1);
                    Application.DoEvents(); //System.Threading.Thread.Sleep(10);
                }
                bViewingImage = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
