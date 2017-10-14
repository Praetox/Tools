using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pImgDB
{
    public partial class frmTip : Form
    {
        public frmTip()
        {
            InitializeComponent();
        }

        public static int iQueMode = 0;
        public static int iSetMode = 0; // 0=quo 1=reView 2=exView 3=fcHide
        public static int iCurView = 0; // 0=hidden 1=visible
        public static string sTip;
        public static string sPrevTip = "";
        private static bool bIntMain;
        
        private void ShowForm()
        {
            int iMid = frmMain.rcForm.Left + (frmMain.rcForm.Width / 2);
            Point ptPos = new Point(iMid - (this.Width / 2), frmMain.rcForm.Top);
            this.Location = ptPos; this.Opacity = 0;

            this.Show(); Application.DoEvents();
            frmMain.bFocusMe = true; Application.DoEvents();
            for (int a = 0; a < 20; a++)
            {
                this.Opacity = (double)a / 22;
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
        }
        private void HideForm()
        {
            for (int a = 10; a >= 0; a--)
            {
                this.Opacity = (double)a / 11;
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
            this.Opacity = 0; Application.DoEvents();
            this.Hide(); Application.DoEvents();
        }

        private void frmTip_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
        }
        private void frmTip_FormClosing(object sender, FormClosingEventArgs e)
        {
            HideForm(); e.Cancel = true;
        }

        private void tMain_Tick(object sender, EventArgs e)
        {
            if (bIntMain) return; bIntMain = true;
            if (sTip != sPrevTip)
            {
                lbTip.Text = sTip;
                sPrevTip = sTip;
            }

            if (iSetMode == 0) iSetMode = iQueMode;
            if (iSetMode != 0)
            {
                tHide.Stop();
                iQueMode = 0;
                //MessageBox.Show("Enter");
                if (iSetMode == 1) //reView
                {
                    if (iCurView != 0) //visible
                    {
                        HideForm();
                        iCurView = 0; //hidden
                    }
                    iSetMode = 2; //exView
                }
                if (iSetMode == 2) //exView
                {
                    if (iCurView == 0) //hidden
                    {
                        ShowForm();
                        iCurView = 1; //visible
                    }
                    tHide.Start();
                }
                if (iSetMode == 3) //hide
                {
                    if (iCurView != 0) //visible
                    {
                        HideForm();
                        iCurView = 0; //hidden
                    }
                }
                Application.DoEvents();
                //MessageBox.Show("Done");
                iSetMode = 0; //done
            }
            bIntMain = false;
            /*if (bVisible || bReTip)
            {
                tHide.Stop();
                if (bVisible && this.Opacity != 0)
                    HideForm();
                lbTip.Text = sTip;
                if (this.Opacity == 0)
                    ShowForm();
                tHide.Start();

                bVisible = false;
                bReTip = false;
                Application.DoEvents();
            }
            bIntMain = false;*/
        }
        private void tHide_Tick(object sender, EventArgs e)
        {
            if (bIntMain) return; bIntMain = true;
            tHide.Stop(); iSetMode = 3; //hidden
            bIntMain = false;
        }
        private void lbTip_Click(object sender, EventArgs e)
        {
            if (bIntMain) return; bIntMain = true;
            tHide.Stop(); iSetMode = 3; //hidden
            bIntMain = false;
        }
        public static void ShowMsg(bool bReView, string sMsg)
        {
            sTip = sMsg;
            int iSet = 2; if (bReView) iSet = 1;
            if (iSetMode == 3) iQueMode = iSet;
            if (iSetMode == 0) iSetMode = iSet;
            Application.DoEvents();
        }
    }
}
