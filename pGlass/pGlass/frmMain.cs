using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace pGlass
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        #region API
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);
        [DllImportAttribute("User32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("User32")]
        private static extern int GetWindowLong(IntPtr hWnd, int Index);
        [DllImport("User32")]
        private static extern int SetWindowLong(IntPtr hWnd, int Index, int Value);
        [DllImport("User32", CharSet = CharSet.Auto)]
        private static extern int SetLayeredWindowAttributes(IntPtr hWnd, int clrKey, Byte bAlpha, int dwFlags);

        private const int LWA_COLORKEY = 1;
        private const int LWA_ALPHA = 2;
        private const int GWL_STYLE = -16;
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x00080000;
        private int k1 = 38, k2 = 40, kW = 17, vl = 255;
        #endregion
        #region Controls
        private void kOpaque_KeyUp(object sender, KeyEventArgs e)
        {
            k1 = k2 = Convert.ToInt16(e.KeyCode);
            kOpaque.Text = "" + k1;
        }
        private void kTransparent_KeyUp(object sender, KeyEventArgs e)
        {
            k2 = Convert.ToInt16(e.KeyCode);
            kTransparent.Text = "" + k2;
        }
        private void kWith_KeyUp(object sender, KeyEventArgs e)
        {
            kW = Convert.ToInt16(e.KeyCode);
            kWith.Text = "" + kW;
        }
        private void kWith_MouseClick(object sender, MouseEventArgs e)
        {
            kW = 0; kWith.Text = "" + kW;
        }
        #endregion
        
        private void tMain_Tick(object sender, EventArgs e)
        {
            bool bk1 = false, bk2 = false, bkW = false;
            if (GetAsyncKeyState(k1) != 0) bk1 = true;
            if (GetAsyncKeyState(k2) != 0) bk2 = true;
            if (GetAsyncKeyState(kW) != 0) bkW = true;
            if (kW != 0 && !bkW) return;
            if (bk1 || bk2)
            {
                int llRet; IntPtr llVnd;
                if (bk1) vl += 10; if (vl > 255) vl = 255;
                if (bk2) vl -= 10; if (vl < 0) vl = 0;
                llVnd = GetForegroundWindow();
                llRet = GetWindowLong(llVnd, GWL_EXSTYLE);
                llRet = llRet |= WS_EX_LAYERED;
                SetWindowLong(llVnd, GWL_EXSTYLE, llRet);
                SetLayeredWindowAttributes(llVnd, 0, (byte)vl, LWA_ALPHA);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //new frmBlendedBG().Show();
            this.Text = "pGlass v" + Application.ProductVersion;
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void frmMain_Enter(object sender, EventArgs e)
        {
            /*MessageBox.Show("Hoy!");
            frmBlendedBG.bRegainFocus = true;
            bRegainFocus = 1;*/
        }
        private void frmMain_Leave(object sender, EventArgs e)
        {
            //bRegainFocus = 0;
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            //frmBlendedBG.bRegainFocus = true;
        }
    }
}