using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pImgDB
{
    public partial class frmTan : Form
    {
        public frmTan()
        {
            InitializeComponent();
        }

        private void tMove_Tick(object sender, EventArgs e)
        {
            if (frmMain.bCanHasFocus)
            {
                this.Show();
                this.Size = new Size(71, 81);
                this.Top = frmMain.rcForm.Top + frmMain.rcForm.Height - 1;
                this.Left = frmMain.rcForm.Left + frmMain.rcForm.Width - 100;
            }
            else this.Hide();
        }

        private void frmTan_Click(object sender, EventArgs e)
        {
            tMove.Stop();
            this.Hide();
        }
    }
}
