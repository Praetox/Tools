using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pImgDB
{
    public partial class frmDebug : Form
    {
        public frmDebug()
        {
            InitializeComponent();
        }

        private void tRefresh_Tick(object sender, EventArgs e)
        {
            string s = "";
            s += frmMain.dDPI_Original[0] + "/" + frmMain.dDPI_Original[1] + " / ";
            s += frmMain.dDPI_Current[0] + "/" + frmMain.dDPI_Current[1] + "\r\n";
            s += frmMain.bRunning.ToString().Substring(0, 1) +
                "??" + frmTip.iCurView +
                frmWait.bActive.ToString().Substring(0, 1) +
                frmZoom.sLabel.Substring(0, 1) + "\r\n";
        }
    }
}
