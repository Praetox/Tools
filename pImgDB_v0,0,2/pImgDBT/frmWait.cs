using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pImgDBT
{
    public partial class frmWait : Form
    {
        public frmWait()
        {
            InitializeComponent();
        }

        public static bool bActive;
        public static bool bInstant;
        public static bool bVisible;
        public static string sHeader;
        public static string sMain;
        public static string sFooter;
        private static bool bWasVisible;
        private static bool bIntMain;
        private static bool bIntInstant;
        
        private void ShowForm()
        {
            this.Opacity = (double)0;
            this.Show(); Application.DoEvents();
            for (int a = 0; a < 20; a++)
            {
                this.Opacity = (double)a / 22;
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
            bWasVisible = true; bActive = true;
        }
        private void HideForm()
        {
            for (int a = 10; a >= 0; a--)
            {
                this.Opacity = (double)a / 11;
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
            this.Hide(); Application.DoEvents();
            this.Opacity = (double)1; Application.DoEvents();
            sHeader = ""; sMain = ""; sFooter = "";
            bWasVisible = false; bActive = false;
        }

        private void tMain_Tick(object sender, EventArgs e)
        {
            if (bIntMain) return; bIntMain = true;
            lbHeader.Text = sHeader;
            lbMain.Text = sMain;
            lbFooter.Text = sFooter;
            if (bVisible != bWasVisible)
            {
                bWasVisible = bVisible;
                if (bVisible) ShowForm();
                if (!bVisible) HideForm();
                Application.DoEvents();
            }
            bIntMain = false;
        }
        private void tInstant_Tick(object sender, EventArgs e)
        {
            if (bIntInstant) return; bIntInstant = true;
            if (bInstant)
            {
                bInstant = false; tMain.Stop(); tMain.Start();
                tMain_Tick(new object(), new EventArgs());
            }
            bIntInstant = false;
        }
        private void frmWait_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
        }
        private void frmWait_FormClosing(object sender, FormClosingEventArgs e)
        {
            HideForm();
        }
    }
}
