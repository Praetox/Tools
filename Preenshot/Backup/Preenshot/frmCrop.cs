using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Preenshot
{
    public partial class frmCrop : Form
    {
        public frmCrop()
        {
            init();
            this.Location = new Point(
                (ptScrn.X / 2) - (ptScrn.X / 8),
                (ptScrn.Y / 2) - (ptScrn.Y / 8));
            this.Size = new Size(
                ptScrn.X / 4, ptScrn.Y / 4);
        }
        public frmCrop(Point pt, Size sz)
        {
            init();
            this.Location = pt;
            this.Size = sz;
        }
        private void init()
        {
            InitializeComponent();
            this.TopMost = true;
            ptRelOfs = ptInvalid;
            ptMySize = ptInvalid;
            ptScrn = new Point(
                Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height);
        }
        public Rectangle ret;
        enum MDMode { None, Move, Resize }
        enum RSMode { None, Up, Right, Down, Left, }
        private Point ptRelOfs; private Point ptMySize;
        private Point ptInvalid = new Point(-1, -1);
        private MDMode mdMouseAction = MDMode.None;
        private RSMode rsMouseX = RSMode.None;
        private RSMode rsMouseY = RSMode.None;
        bool bIsDrawing = false;
        private Point ptScrn;

        private void bg_MouseDown(object sender, MouseEventArgs e)
        {
            double dX = this.Width / 10.0;
            double dY = this.Height / 10.0;
            if (e.X < dX * 1) rsMouseX = RSMode.Left;
            if (e.X > dX * 9) rsMouseX = RSMode.Right;
            if (e.Y < dY * 1) rsMouseY = RSMode.Up;
            if (e.Y > dY * 9) rsMouseY = RSMode.Down;
            if (rsMouseX != RSMode.None ||
                rsMouseY != RSMode.None)
                mdMouseAction = MDMode.Resize;
            else mdMouseAction = MDMode.Move;
            ptMySize = (Point)this.Size;
            ptRelOfs = e.Location;
        }
        private void bg_MouseMove(object sender, MouseEventArgs e)
        {
            if (bIsDrawing) return;
            bIsDrawing = true;
            if (mdMouseAction == MDMode.Move)
            {
                Point ptNewPos = this.Location;
                Point ptNewSize = (Point)this.Size;
                ptNewPos.X += e.X - ptRelOfs.X;
                ptNewPos.Y += e.Y - ptRelOfs.Y;
                ptNewPos = FixLocation(ptNewPos);
                ptNewPos = FixLocation(ptNewPos, ptNewSize);
                this.Location = ptNewPos;
                Application.DoEvents();
            }
            if (mdMouseAction == MDMode.Resize)
            {
                Point ptNewPos = this.Location;
                Point ptNewSize = (Point)this.Size;
                if (rsMouseX == RSMode.Left)
                {
                    int xDiff = e.X - ptRelOfs.X;
                    int xNull = ptNewPos.X + xDiff;
                    if (xNull < 0) xDiff -= xNull;
                    ptNewPos.X += xDiff;
                    ptNewSize.X -= xDiff;
                }
                if (rsMouseY == RSMode.Up)
                {
                    int yDiff = e.Y - ptRelOfs.Y;
                    int yNull = ptNewPos.Y + yDiff;
                    if (yNull < 0) yDiff -= yNull;
                    ptNewPos.Y += yDiff;
                    ptNewSize.Y -= yDiff;
                }
                if (rsMouseX == RSMode.Right)
                {
                    int xDiff = e.X - ptRelOfs.X;
                    int xOver = ptScrn.X - (ptNewPos.X + ptNewSize.X + xDiff);
                    if (xOver < 0) xDiff += xOver;
                    ptNewSize.X += xDiff;
                    ptRelOfs.X += xDiff;
                }
                if (rsMouseY == RSMode.Down)
                {
                    int yDiff = e.Y - ptRelOfs.Y;
                    int yOver = ptScrn.Y - (ptNewPos.Y + ptNewSize.Y + yDiff);
                    if (yOver < 0) yDiff += yOver;
                    ptNewSize.Y += yDiff;
                    ptRelOfs.Y += yDiff;
                }
                ptMySize = ptNewSize;
                ptNewPos = FixLocation(ptNewPos);
                ptNewPos = FixLocation(ptNewPos, ptNewSize);
                this.Size = (Size)ptNewSize;
                this.Location = ptNewPos;
                Application.DoEvents();
            }
            bIsDrawing = false;
        }
        private Point FixLocation(Point ptNewPos)
        {
            if (ptNewPos.X < 0) ptNewPos.X = 0;
            if (ptNewPos.Y < 0) ptNewPos.Y = 0;
            return ptNewPos;
        }
        private Point FixLocation(Point ptNewPos, Point ptNewSize)
        {
            if (ptNewPos.X + ptNewSize.X > ptScrn.X) ptNewPos.X = ptScrn.X - ptNewSize.X;
            if (ptNewPos.Y + ptNewSize.Y > ptScrn.Y) ptNewPos.Y = ptScrn.Y - ptNewSize.Y;
            return ptNewPos;
        }
        private void bg_MouseUp(object sender, MouseEventArgs e)
        {
            ptRelOfs = ptInvalid;
            ptMySize = ptInvalid;
            rsMouseX = RSMode.None;
            rsMouseY = RSMode.None;
            mdMouseAction = MDMode.None;
        }

        private void bg_DoubleClick(object sender, EventArgs e)
        {
            bg_MouseUp(new object(), new MouseEventArgs
                (MouseButtons.Left, 0, 0, 0, 0));
            for (int a = 15; a > 0; a--)
            {
                this.Opacity = (double)a / 20;
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
            this.Close();
        }
        private void frmCrop_FormClosing(object sender, FormClosingEventArgs e)
        {
            ret = new Rectangle(
                this.Location,
                this.Size);
        }
    }
}
