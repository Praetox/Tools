using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Preenshot
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);
        System.Media.SoundPlayer spShot = new System.Media.SoundPlayer("grab.wav");
        System.Media.SoundPlayer spDone = new System.Media.SoundPlayer("done.wav");
        System.Collections.ArrayList links = new System.Collections.ArrayList(8);

        private bool inHotkeys;
        private string sAppVer = "";
        private string PrgDomain = "http://tox.awardspace.us/div/";
        private string ToxDomain = "http://praetox.com/";
        Random rnd = new Random();

        private void frmMain_Load(object sender, EventArgs e)
        {
            sAppVer = Application.ProductVersion;
            sAppVer = sAppVer.Substring(0, sAppVer.LastIndexOf('.'));
            txtPath.Text = Application.StartupPath;
            if (System.IO.File.Exists("grab.wav")) spShot.Load();
            if (System.IO.File.Exists("done.wav")) spDone.Load();
            if (System.IO.File.Exists("config.ini"))
            {
                string sConf = System.IO.File.ReadAllText("config.ini");
                string sPath = txtPath.Text;
                string sFormat = "png";
                string sCrop = txtCrop.Text;
                string sResize = txtResize.Text;
                string sUpload = "";
                if (sConf.Contains("</path>")) sPath = Split(Split(sConf, "<path>")[1], "</path>")[0];
                if (sConf.Contains("</format>")) sFormat = Split(Split(sConf, "<format>")[1], "</format>")[0];
                if (sConf.Contains("</crop>")) sCrop = Split(Split(sConf, "<crop>")[1], "</crop>")[0];
                if (sConf.Contains("<crop-explicit />")) chkCrop.Checked = true; else chkCrop.Checked = false;
                if (sConf.Contains("</resize>")) sResize = Split(Split(sConf, "<resize>")[1], "</resize>")[0];
                if (sConf.Contains("<keep-aspect />")) chkResize.Checked = true; else chkResize.Checked = false;
                if (sConf.Contains("</upload>")) sUpload = Split(Split(sConf, "<upload>")[1], "</upload>")[0];
                if (sConf.Contains("<upload-clip />")) chkUploadClip.Checked = true; else chkUploadClip.Checked = false;
                if (sConf.Contains("<upload-notify />")) chkUploadNotify.Checked = true; else chkUploadNotify.Checked = false;
                txtPath.Text = sPath; txtCrop.Text = sCrop; txtResize.Text = sResize;
                if (sFormat == "bmp") rBMP.Checked = true;
                if (sFormat == "jpg") rJPG.Checked = true;
                if (sFormat == "png") rPNG.Checked = true;
                if (sFormat == "gif") rGIF.Checked = true;
                if (sFormat == "tif") rTIF.Checked = true;
                if (sUpload == "Disabled") rUP_NA.Checked = true;
                if (sUpload == "Tinypic") rUP_Tinypic.Checked = true;
                if (sUpload == "Imagehost") rUP_Imagehost.Checked = true;
            }

            this.Opacity = 0; this.Show();
            for (int a = 1; a <= 10; a++)
            {
                Application.DoEvents();
                this.Opacity = (double)a / 10;
                System.Threading.Thread.Sleep(20);
            }
            nico.Icon = this.Icon;
            nico.Text = "Preenshot";
            nico.Visible = true;

            try
            {
                bool bUpdateCheckOK = true;
                WebReq WR = new WebReq();
                WR.Request(PrgDomain + "Preenshot_version.php?cv=" + sAppVer);
                long lUpdateStart = Tick();
                while (!WR.isReady && bUpdateCheckOK)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(10);
                    if (Tick() > lUpdateStart + 5000)
                        bUpdateCheckOK = false;
                }
                string wrh = WR.sResponse;
                if (wrh.Contains("<WebReq_Error>")) throw new Exception("wat");
                if (!bUpdateCheckOK) throw new Exception("wat");

                if (!wrh.Contains("<VERSION>" + sAppVer + "</VERSION>"))
                {
                    string sNewVer = Split(Split(wrh, "<VERSION>")[1], "</VERSION>")[0];
                    bool GetUpdate = (DialogResult.Yes == MessageBox.Show(
                        "A new version (" + sNewVer + ") is available. Update?",
                        "Preenshot Updater", MessageBoxButtons.YesNo));
                    if (GetUpdate)
                    {
                        string UpdateLink = new System.Net.WebClient().DownloadString(
                            ToxDomain + "inf/Preenshot_link.html").Split('%')[1];
                        System.Diagnostics.Process.Start(UpdateLink + "&cv=" + sAppVer);
                        Application.Exit();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Couldn't check for updates.", "Preenshot Updater");
            }
            log("Finished.");
        }

        private void cmdPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd =
                new FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != "")
                txtPath.Text = fbd.SelectedPath;
        }
        private void log(string s)
        {
            state.Text = DateTime.Now.ToLongTimeString() + " - " + s;
            Application.DoEvents();
        }

        private void tHotkeys_Tick(object sender, EventArgs e)
        {
            if (inHotkeys) return; inHotkeys = true;
            if (GetAsyncKeyState(Keys.PrintScreen) == -32767)
            {
                Point ptScrn = new Point(
                    Screen.PrimaryScreen.Bounds.Width,
                    Screen.PrimaryScreen.Bounds.Height);
                Bitmap img = new Bitmap(ptScrn.X, ptScrn.Y);
                Graphics scr = Graphics.FromImage(img);
                scr.CopyFromScreen(0, 0, 0, 0,
                    new Size(ptScrn.X, ptScrn.Y),
                    CopyPixelOperation.SourceCopy);
                scr.Dispose();

                log("Grabbing screenshot");
                if (spShot.IsLoadCompleted) spShot.Play();
                string scrop = txtCrop.Text;
                string ssize = txtResize.Text;
                if (chkCrop.Checked)
                {
                    //scrop = getCrop();
                    frmPostCrop fpc = new frmPostCrop(img);
                    fpc.ShowDialog(); Rectangle rc = fpc.ret;
                    scrop = rc.Left + "x" + rc.Top + " | " +
                        rc.Width + "x" + rc.Height;
                }

                if (scrop != "" || ssize != "")
                {
                    Rectangle rc = new Rectangle(Point.Empty, img.Size);
                    string[] sacrop = ParseGeo(scrop);
                    if (sacrop.Length == 4)
                        rc = new Rectangle(
                            Convert.ToInt32(sacrop[0]),
                            Convert.ToInt32(sacrop[1]),
                            Convert.ToInt32(sacrop[2]),
                            Convert.ToInt32(sacrop[3]));

                    Rectangle sz = new Rectangle(
                        0, 0, rc.Width, rc.Height);
                    string[] sasize = ParseGeo(ssize);
                    if (sasize.Length == 2)
                        sz = new Rectangle(0, 0, 
                            Convert.ToInt32(sasize[0]),
                            Convert.ToInt32(sasize[1]));

                    if (chkResize.Checked)
                    {
                        double aspecta = (double)rc.Width / (double)rc.Height;
                        double aspectb = (double)sz.Width / (double)sz.Height;
                        if (Math.Abs(aspecta - aspectb) >= 0.05)
                        {
                            if (aspectb > aspecta) sz.Width = (int)Math.Round(sz.Height * aspecta);
                            if (aspectb < aspecta) sz.Height = (int)Math.Round(sz.Width / aspecta);
                        }
                    }

                    Bitmap imaeg = new Bitmap(sz.Width, sz.Height);
                    Graphics geami = Graphics.FromImage(imaeg);
                    geami.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    geami.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    geami.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    geami.DrawImage(img, sz, rc, GraphicsUnit.Pixel);
                    geami.Dispose(); img.Dispose(); img = imaeg;
                }

                string sfExt = ".wat";
                if (rBMP.Checked) sfExt = ".bmp";
                if (rJPG.Checked) sfExt = ".jpg";
                if (rPNG.Checked) sfExt = ".png";
                if (rGIF.Checked) sfExt = ".gif";
                if (rTIF.Checked) sfExt = ".tif";
                string sPath = txtPath.Text + "/ps_";

                int ifNum = 0;
                do ifNum++;
                while (System.IO.File.Exists(sPath + ifNum + sfExt));
                sPath += ifNum + sfExt;

                if (sfExt == ".bmp") img.Save(sPath, System.Drawing.Imaging.ImageFormat.Bmp);
                if (sfExt == ".jpg") img.Save(sPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                if (sfExt == ".png") img.Save(sPath, System.Drawing.Imaging.ImageFormat.Png);
                if (sfExt == ".gif") img.Save(sPath, System.Drawing.Imaging.ImageFormat.Gif);
                if (sfExt == ".tif") img.Save(sPath, System.Drawing.Imaging.ImageFormat.Tiff);
                img.Dispose();

                if (rUP_Imagehost.Checked)
                {
                    log("Uploading screenshot");
                    string sUrl = Upload(sPath);
                    if (sUrl != "")
                    {
                        links.Add(sUrl);
                        if (chkUploadClip.Checked)
                        {
                            Clipboard.Clear();
                            Clipboard.SetText(sUrl);
                        }
                    }
                }
                log("Finished.");
                if (chkUploadNotify.Checked &&
                    spDone.IsLoadCompleted)
                    spDone.Play();
            }
            inHotkeys = false;
        }
        private string[] ParseGeo(string s)
        {
            string[] d = new string[] { " ", "|", "X", ".",
                ",", ":", ";", "-", "!", "/", "\\", "'" };
            for (int a = 0; a < d.Length; a++) s = s.Replace(d[a], "x");
            while (s.Contains("xx")) s = s.Replace("xx", "x");
            return s.Split('x');
        }

        #region GUI-crap
        private void SetMyForeColor(Control c)
        {
            if (c is RadioButton) if (((RadioButton)c).Checked) c.ForeColor = Color.White; else c.ForeColor = Color.Silver;
            if (c is CheckBox) if (((CheckBox)c).Checked) c.ForeColor = Color.White; else c.ForeColor = Color.Silver;
        }
        private void rBMP_CheckedChanged(object sender, EventArgs e)
        {
            SetMyForeColor(rBMP);
        }
        private void rJPG_CheckedChanged(object sender, EventArgs e)
        {
            SetMyForeColor (rJPG);
        }
        private void rPNG_CheckedChanged(object sender, EventArgs e)
        {
            SetMyForeColor (rPNG);
        }
        private void rGIF_CheckedChanged(object sender, EventArgs e)
        {
            SetMyForeColor (rGIF);
        }
        private void rTIF_CheckedChanged(object sender, EventArgs e)
        {
            SetMyForeColor (rTIF);
        }
        private void rUP_NA_CheckedChanged(object sender, EventArgs e)
        {
            SetMyForeColor (rUP_NA);
        }
        private void rUP_Tinypic_CheckedChanged(object sender, EventArgs e)
        {
            SetMyForeColor (rUP_Tinypic);
        }
        private void rUP_Imagehost_CheckedChanged(object sender, EventArgs e)
        {
            SetMyForeColor (rUP_Imagehost);
        }
        private void chkCrop_CheckedChanged(object sender, EventArgs e)
        {
            SetMyForeColor(chkCrop);
        }
        private void chkResize_CheckedChanged(object sender, EventArgs e)
        {
            SetMyForeColor(chkResize);
        }
        private void chkUploadClip_CheckedChanged(object sender, EventArgs e)
        {
            SetMyForeColor(chkUploadClip);
        }
        private void chkUploadNotify_CheckedChanged(object sender, EventArgs e)
        {
            SetMyForeColor(chkUploadNotify);
        }
        #endregion

        private void cmdCrop_Click(object sender, EventArgs e)
        {
            txtCrop.Text = getCrop();
        }
        private string getCrop()
        {
            frmCrop frm;
            string[] sarc = ParseGeo(txtCrop.Text);
            if (sarc.Length == 4)
            {
                Point ptLoc = new Point(
                    Convert.ToInt32(sarc[0]),
                    Convert.ToInt32(sarc[1]));
                Size szSize = new Size(
                    Convert.ToInt32(sarc[2]),
                    Convert.ToInt32(sarc[3]));
                frm = new frmCrop(ptLoc, szSize);
            }
            else frm = new frmCrop();
            this.Hide(); frm.ShowDialog();
            this.Show(); Rectangle rc = frm.ret;
            return rc.Left + "x" + rc.Top + " | " +
                rc.Width + "x" + rc.Height;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string sConfig =
                "////////////////////////////////\r\n" +
                "//                            //\r\n" +
                "//  Preenshot configuration   //\r\n" +
                "//----------------------------//\r\n" +
                "//    http://praetox.com/     //\r\n" +
                "//                            //\r\n" +
                "////////////////////////////////\r\n" +
                "\r\n";
            if (txtPath.Text != "") sConfig += "<path>" + txtPath.Text + "</path>\r\n";
            if (rBMP.Checked) sConfig += "<format>bmp</format>\r\n";
            if (rJPG.Checked) sConfig += "<format>jpg</format>\r\n";
            if (rPNG.Checked) sConfig += "<format>png</format>\r\n";
            if (rGIF.Checked) sConfig += "<format>gif</format>\r\n";
            if (rTIF.Checked) sConfig += "<format>tif</format>\r\n";
            if (txtCrop.Text != "") sConfig += "<crop>" + txtCrop.Text + "</crop>\r\n";
            if (chkCrop.Checked) sConfig += "<crop-explicit />\r\n";
            if (txtResize.Text != "") sConfig += "<resize>" + txtResize.Text + "</resize>\r\n";
            if (chkResize.Checked) sConfig += "<keep-aspect />\r\n";
            if (rUP_NA.Checked) sConfig += "<upload>Disabled</upload>\r\n";
            if (rUP_Tinypic.Checked) sConfig += "<upload>Tinypic</upload>\r\n";
            if (rUP_Imagehost.Checked) sConfig += "<upload>Imagehost</upload>\r\n";
            if (chkUploadClip.Checked) sConfig += "<upload-clip />\r\n";
            if (chkUploadNotify.Checked) sConfig += "<upload-notify />\r\n";
            System.IO.File.WriteAllText("config.ini", sConfig);
        }
        private string[] Split(string a, string b)
        {
            return a.Split(new string[] { b }, StringSplitOptions.None);
        }
        private long Tick()
        {
            return DateTime.Now.Ticks / 10000;
        }

        private void pbHead_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://praetox.com/");
        }

        private string Upload(string sPath)
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(
                    sPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] bImg = new byte[fs.Length]; fs.Read(bImg, 0, bImg.Length);
                fs.Close(); fs.Dispose(); string fname = MakeFName();

                string sPostTo = "http://www.imagehost.org/";
                string sRefer = "http://www.imagehost.org/";

                WebReq WReq = new WebReq();
                string sMPBound = "----------" + WReq.RandomChars(22);
                string sPD1 = "--" + sMPBound + "\r\n" +
                    "Content-Disposition: form-data; name=\"a\"" + "\r\n" +
                    "\r\n" + "upload" + "\r\n" + "--" + sMPBound + "\r\n" +
                    "Content-Disposition: form-data; name=\"file[]\"; " +
                    "filename=\"" + fname + ".png\r\n" +
                    "Content-Type: image/png" + "\r\n" +
                    "\r\n";
                string sPD2 = "\r\n" + "--" + sMPBound + "\r\n" +
                    "Content-Disposition: form-data; name=\"file[]\"; filename=\"\"" + "\r\n" +
                    "\r\n" + "\r\n" + "--" + sMPBound + "\r\n" +
                    "Content-Disposition: form-data; name=\"file[]\"; filename=\"\"" + "\r\n" +
                    "\r\n" + "\r\n" + "--" + sMPBound + "\r\n" +
                    "Content-Disposition: form-data; name=\"file[]\"; filename=\"\"" + "\r\n" +
                    "\r\n" + "\r\n" + "--" + sMPBound + "\r\n" +
                    "Content-Disposition: form-data; name=\"file[]\"; filename=\"\"" + "\r\n" +
                    "\r\n" + "\r\n" + "--" + sMPBound + "\r\n" +
                    "Content-Disposition: form-data; name=\"file[]\"; filename=\"\"" + "\r\n" +
                    "\r\n" + "\r\n" + "--" + sMPBound + "\r\n" +
                    "Content-Disposition: form-data; name=\"file[]\"; filename=\"\"" + "\r\n" +
                    "\r\n" + "\r\n" + "--" + sMPBound + "\r\n" +
                    "Content-Disposition: form-data; name=\"file[]\"; filename=\"\"" + "\r\n" +
                    "\r\n" + "\r\n" + "--" + sMPBound + "--" + "\r\n";
                byte[] bPD1 = new byte[sPD1.Length];
                byte[] bPD2 = new byte[sPD2.Length];
                for (int a = 0; a < sPD1.Length; a++) bPD1[a] = (byte)sPD1[a];
                for (int a = 0; a < sPD2.Length; a++) bPD2[a] = (byte)sPD2[a];
                byte[] bPD = new byte[bPD1.Length + bImg.Length + bPD2.Length];
                bPD1.CopyTo(bPD, 0);
                bImg.CopyTo(bPD, 0 + bPD1.Length);
                bPD2.CopyTo(bPD, 0 + bPD1.Length + bImg.Length);
                System.Net.WebHeaderCollection whc = new System.Net.WebHeaderCollection();
                //whc.Add("Expect: 100-continue");
                whc.Add("Referer: " + sRefer);

                WReq.Request(sPostTo, whc, bPD, sMPBound, 3, "", true);
                while (!WReq.isReady)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(1);
                }
                if (WReq.sResponse.Contains(fname))
                {
                    if (WReq.sResponse.Contains("nowrap\">Hotlink</td>"))
                        return Split(Split(Split(WReq.sResponse,
                            "nowrap\">Hotlink</td>")[1],
                            " size=\"50\" value=\"")[1],
                            "\" />")[0];
                    else return Split(Split(Split(WReq.sResponse,
                            "nowrap\">Text Link</td>")[1],
                            " size=\"50\" value=\"")[1],
                            "\" />")[0];
                }
                else return "";
            }
            catch { return ""; }
        }
        string MakeFName()
        {
            string ret = "";
            for (int a = 0; a < 4; a++)
            {
                ret += rnd.Next(0xffff).ToString("x4");
            }
            return ret;
        }

        private void lbClipAll_Click(object sender, EventArgs e)
        {
            string sLinks = "";
            for (int a = 0; a < links.Count; a++)
                sLinks += (string)links[a] + "\r\n";
            Clipboard.Clear(); Clipboard.SetText(sLinks);
        }

        private void lbClrMem_Click(object sender, EventArgs e)
        {
            links.Clear();
        }

        private void nico_MouseClick(object sender, MouseEventArgs e)
        {
            this.Visible = !this.Visible;
        }
    }
}
