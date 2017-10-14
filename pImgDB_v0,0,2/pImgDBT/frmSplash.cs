using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pImgDBT
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
    }
}
