using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Batchify
{
    public partial class frmHelp : Form
    {
        public frmHelp()
        {
            InitializeComponent();
        }

        bool Changetext = true;
        string Defaultval = "";

        private void frmHelp_Load(object sender, EventArgs e)
        {
            Defaultval = txtHelp.Text;
            txtHelp.SelectionStart = 0;
            txtHelp.SelectionLength = 0;
            txtHelp.ScrollToCaret();
        }

        private void txtHelp_TextChanged(object sender, EventArgs e)
        {
            if (!Changetext) return;
            Changetext = false;
            txtHelp.Text = Defaultval;
            Changetext = true;
        }

        private void frmHelp_Resize(object sender, EventArgs e)
        {
            this.Width = 790;
            txtHelp.Height = this.Height - 90;
        }
    }
}
