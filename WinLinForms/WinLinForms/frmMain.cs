using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WinLinForms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern short GetKeyState(System.Windows.Forms.Keys vKey);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT { public int X; public int Y; public int X2; public int Y2; }

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOZORDER = 0x0004;
        const UInt32 SWP_NOREDRAW = 0x0008;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        const UInt32 SWP_FRAMECHANGED = 0x0020;
        const UInt32 SWP_SHOWWINDOW = 0x0040;
        const UInt32 SWP_HIDEWINDOW = 0x0080;
        const UInt32 SWP_NOCOPYBITS = 0x0100;
        const UInt32 SWP_NOOWNERZORDER = 0x0200;
        const UInt32 SWP_NOSENDCHANGING = 0x0400;

        RECT rofs = new RECT();
        Point cofs = new Point(0, 0);
        NotifyIcon ni = new NotifyIcon();
        IntPtr lolwindow = (IntPtr)0;
        bool bTop, bLft, bMid;

        private void dongs_Tick(object sender, EventArgs e)
        {
            bool LAlt = (GetAsyncKeyState(Keys.LMenu)<0);
            if (!LAlt) return; //GTFO MY METHOD BITCH
            bool LBtn = (GetAsyncKeyState(Keys.LButton)<0);
            bool RBtn = (GetAsyncKeyState(Keys.RButton)<0);
            IntPtr nig = GetForegroundWindow();
            
            if (!LBtn && !RBtn) lolwindow = (IntPtr)0;
            if ((LBtn || RBtn) && lolwindow != nig)
            {
                lolwindow = nig;
                RECT um = new RECT();
                cofs = Cursor.Position;
                GetWindowRect(nig, out um);
                um.X2 -= um.X; um.Y2 -= um.Y;
                cofs.X -= um.X; cofs.Y -= um.Y;
                bLft = false; if (cofs.X < um.X2 / 2) bLft = true;
                bTop = false; if (cofs.Y < um.Y2 / 2) bTop = true;
                bMid = false; rofs = um; um.X2 /= 4; um.Y2 /= 4;
                if (cofs.X > um.X2 * 1 && cofs.X < um.X2 * 3 &&
                    cofs.Y > um.Y2 * 1 && cofs.Y < um.Y2 * 3)
                    bMid = true;
            }
            if (LAlt && LBtn)
            {
                Point loc = Cursor.Position;
                loc.X -= cofs.X; loc.Y -= cofs.Y;
                SetWindowPos(nig, 0, loc.X, loc.Y,
                    0, 0, SWP_NOZORDER | SWP_NOSIZE);
                Application.DoEvents();
            }
            if (LAlt && RBtn)
            {
                RECT um = rofs;
                Point nofs = Cursor.Position;
                nofs.X -= rofs.X + cofs.X;
                nofs.Y -= rofs.Y + cofs.Y;
                if (bMid)
                {
                    um.X -= nofs.X; um.X2 += nofs.X * 2;
                    um.Y -= nofs.Y; um.Y2 += nofs.Y * 2;
                }
                else
                {
                    if (!bLft) um.X2 += nofs.X; else
                    { um.X += nofs.X; um.X2 -= nofs.X; }
                    if (!bTop) um.Y2 += nofs.Y; else
                    { um.Y += nofs.Y; um.Y2 -= nofs.Y; }
                }
                SetWindowPos(nig, 0, um.X, um.Y,
                    um.X2, um.Y2, SWP_NOZORDER);
                Application.DoEvents();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ni.Icon = this.Icon;
            ni.Visible = true;
            ni.DoubleClick += new EventHandler(ni_DoubleClick);
            ni.ContextMenu = new ContextMenu(new MenuItem[] { new MenuItem("Exit", ni_MenuExit) });
        }
        void ni_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = !this.Visible;
        }
        void ni_MenuExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void derp_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ni.Visible = false;
        }
    }
}
