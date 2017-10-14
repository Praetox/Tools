using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pImgDB
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

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
            this.Opacity = (double)1;
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            lbP.Visible = false;
            lbH1.Visible = false;
            lbH2.Visible = false;
            ShowForm();
        }
        private void frmSplash_MouseClick(object sender, MouseEventArgs e)
        {
            HideForm();
        }
        private void frmSplash_KeyDown(object sender, KeyEventArgs e)
        {
            HideForm();
        }
        private void lbBody_Click(object sender, EventArgs e)
        {
            HideForm();
        }
        private void lbH1_Click(object sender, EventArgs e)
        {
            HideForm();
        }
        private void lbH2_Click(object sender, EventArgs e)
        {
            HideForm();
        }

        private void tNews_Tick(object sender, EventArgs e)
        {
            if (!frmMain.sNews_B) return;
            if (frmMain.sNews_Al)
            {
                lbP.TextAlign = ContentAlignment.TopLeft;
                frmMain.sNews_Al = false;
            }
            lbH1.Text = frmMain.sNews_H1;
            lbH2.Text = frmMain.sNews_H2;
            lbP.Text = frmMain.sNews_P;
        }
    }
}
