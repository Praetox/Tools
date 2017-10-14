using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pImgResize
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private string sVer = Application.ProductVersion;
        private Image[] imLogo = new Image[0];
        private int iLogo = 0;

        private void cmdGo_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void cmdGo_DragDrop(object sender, DragEventArgs e)
        {
            Bitmap bPic;
            string sPath = ((string[])e.Data.GetData(DataFormats.FileDrop, false))[0];
            try { bPic = Bitmap.FromFile(sPath) as Bitmap; ProcessImage(bPic); }
            catch { MessageBox.Show("That's not an image!"); return; }
        }

        private void ProcessImage(Bitmap bPic)
        {
            int iX = -1; int iY = -1;
            try
            {
                string[] sTmp = txtRes.Text
                    .Replace("-", "x").Replace(";", "x")
                    .Replace("X", "x").Replace(" ", "")
                    .Split('x');
                iX = Convert.ToInt32(sTmp[0]);
                iY = Convert.ToInt32(sTmp[1]);
            }
            catch
            {
                MessageBox.Show("Error while parsing new resolution." + "\r\n\r\n" +
                    "Proper format: Horizontal x Vertical"); return;
            }

            //Stop if bPic is invalid
            if (bPic.Width < 0) return;

            string sCGo = cmdGo.Text;
            cmdGo.Text = "P r o c e s s i n g . . .";
            Application.DoEvents();

            int iScaleMode = 0;
            if (R11.Checked) iScaleMode = 1;
            if (R12.Checked) iScaleMode = 2;
            int iSaveTo = 0;
            if (R21.Checked) iSaveTo = 1;
            if (R22.Checked) iSaveTo = 2;
            int iFormat = 0;
            if (R31.Checked) iFormat = 1;
            if (R32.Checked) iFormat = 2;
            if (R33.Checked) iFormat = 3;

            if (iX < 1) iX = 1; if (iY < 1) iY = 1;
            double dRaX = (double)bPic.Width / (double)iX;
            double dRaY = (double)bPic.Height / (double)iY;
            if (chkAspect.Checked)
            {
                if (dRaX > dRaY) iY = (int)Math.Round((double)iX / ((double)bPic.Width / (double)bPic.Height));
                if (dRaY > dRaX) iX = (int)Math.Round((double)iY * ((double)bPic.Width / (double)bPic.Height));
            }
            Bitmap bOut;
            int iXD = iX; int iYD = iY;
            if (iScaleMode == 2)
            {
                iXD = (int)Math.Round((double)iX * 2.0);
                iYD = (int)Math.Round((double)iY * 2.0);
            }
            bOut = new Bitmap(iXD, iYD);

            using (Graphics gOut = Graphics.FromImage((Image)bOut))
            {
                if (iScaleMode == 0) gOut.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                if (iScaleMode != 0) gOut.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                if (iScaleMode == 0) gOut.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                if (iScaleMode != 0) gOut.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                gOut.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gOut.DrawImage(bPic, 0, 0, iXD, iYD);
            }
            if (iScaleMode == 2)
            {
                bPic.Dispose(); bPic = (Bitmap)bOut.Clone();
                bOut.Dispose(); bOut = new Bitmap(iX, iY);
                using (Graphics gOut = Graphics.FromImage((Image)bOut))
                {
                    gOut.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    gOut.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    gOut.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    gOut.DrawImage(bPic, 0, 0, iX, iY);
                }
            }

            if (iSaveTo != 2)
            {
                string sFormat = "";
                System.Drawing.Imaging.ImageFormat imFormat = System.Drawing.Imaging.ImageFormat.Png;
                if (iFormat == 1) { sFormat = "jpg"; imFormat = System.Drawing.Imaging.ImageFormat.Jpeg; }
                if (iFormat == 2) { sFormat = "png"; imFormat = System.Drawing.Imaging.ImageFormat.Png; }
                if (iFormat == 3) { sFormat = "gif"; imFormat = System.Drawing.Imaging.ImageFormat.Gif; }
                bOut.Save("output." + sFormat, imFormat);
            }
            if (iSaveTo != 1)
            {
                Clipboard.Clear();
                Clipboard.SetImage((Image)bOut);
            }

            cmdGo.Text = sCGo;
            Application.DoEvents();
        }

        private void cmdGo_Click(object sender, EventArgs e)
        {
            Bitmap bPic;
            try { bPic = Clipboard.GetImage() as Bitmap; ProcessImage(bPic); }
            catch { MessageBox.Show("That's not an image!"); return; }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            sVer = sVer.Substring(0, sVer.LastIndexOf("."));
            this.Text += " v" + sVer;
        }
    }
}
