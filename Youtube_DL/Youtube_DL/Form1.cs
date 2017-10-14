using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Youtube_DL
{
    public partial class Form1 : Form
    {
        [DllImport("User32")] private static extern int SetForegroundWindow(int hwnd);
        [DllImport("User32")] private static extern int ShowWindow(int hwnd, int nCmdShow);
        private bool BatchJob = false;
        private string ytID_Prefix = "youtube.com/watch?v=";
        private string ytDL_Template = "http://cache.googlevideo.com/get_video?video_id=%ytID%";
        private string ptAppInfo = "http://nordic.awardspace.com/YouTubeDL.php";

        public Form1()
        {
            InitializeComponent();
        }

        private void cDownload_Click(object sender, EventArgs e)
        {
            try
            {
                BatchJob = false; cDownload.Enabled = false; string ytDL = "";
                #region Check if file already exists
                try
                {
                    if (System.IO.File.Exists(tFile.Text + ".flv"))
                    {
                        if (MessageBox.Show(tFile.Text + " already exists.\r\n\r\nDo you want to replace it?",
                                            "Praetox YouTube Downloader", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            System.IO.File.Delete(tFile.Text + ".flv");
                        }
                        else
                        {
                            cDownload.Enabled = true; return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Media.SystemSounds.Hand.Play();
                    MessageBox.Show("Houston, we have a problem.\r\n\r\n" +
                                    "Occurred in: CheckFileExist\r\n" + ex.Message);
                }
                #endregion
                #region Create the download link (ytDL)
                try
                {
                    string ytID = tLink.Text;
                    ytID = ytID.Substring(ytID.IndexOf(ytID_Prefix) + ytID_Prefix.Length);
                    ytID = ytID.Substring(0, 11);
                    ytDL = ytDL_Template.Replace("%ytID%", ytID);
                }
                catch (Exception ex)
                {
                    System.Media.SystemSounds.Hand.Play();
                    MessageBox.Show("Houston, we have a problem.\r\n\r\n" +
                                    "Occurred in: ExtractYoutubeID\r\n" + ex.Message);
                    return;
                }
                #endregion
                #region Start async download
                try
                {
                    System.Net.WebClient wc = new System.Net.WebClient();
                    wc.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(wc_DPC);
                    wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DFC);
                    wc.DownloadFileAsync(new Uri(ytDL), tFile.Text + ".flv");
                    //Clipboard.Clear(); Clipboard.SetText(ytDL);
                }
                catch (Exception ex)
                {
                    System.Media.SystemSounds.Hand.Play();
                    MessageBox.Show("Houston, we have a problem.\r\n\r\n" +
                                    "Occurred in: StartAsyncDownload\r\n" + ex.Message);
                }
                #endregion
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show("Houston, we have a problem.\r\n\r\n" +
                                "Occurred in: cmdStartAsyncVoid\r\n" + ex.Message);
            }
        }
        private void cGetname_Click(object sender, EventArgs e)
        {
            try
            {
                string Link = tLink.Text;
                if (Link.Length > Link.IndexOf(ytID_Prefix) + ytID_Prefix.Length + 11)
                    Link = Link.Substring(0, Link.IndexOf(ytID_Prefix) + ytID_Prefix.Length + 11);
                string YT_Html = new System.Net.WebClient().DownloadString(Link);
                string ytPrefix = "<meta name=\"title\" content=\"";
                YT_Html = YT_Html.Substring(YT_Html.IndexOf(ytPrefix) + ytPrefix.Length);
                YT_Html = YT_Html.Substring(0, YT_Html.IndexOf("\">"));
                YT_Html = YT_Html.Replace("/", "_").Replace("\\", "_").Replace(":", "_").Replace("?", "_").Replace
                                            ("<", "_").Replace("\"", "_").Replace(">", "_").Replace("|", "_");
                if (YT_Html.Length > 250) YT_Html = YT_Html.Substring(0, 250);
                tFile.Text = YT_Html;
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show("Houston, we have a problem.\r\n\r\n" +
                                "Occurred in: cmdIdentifyVoid\r\n" + ex.Message);
            }
        }

        void wc_DPC(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            try
            {
                cDownload.Text = (int)((float)((float)100 / (float)e.TotalBytesToReceive) * (float)e.BytesReceived) + "%";
            }
            catch { cDownload.Text = "Downloading..."; }
            Application.DoEvents();
        }
        void wc_DFC(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                cDownload.Text = "Download";
                cDownload.Enabled = true;
                if (!BatchJob)
                    MessageBox.Show("Download complete!\r\n" +
                                    "\r\n" +
                                    "To play this movie, you need a movie player that supports\r\n" +
                                    ".flv media files. I would like to recommend the CCCP codec\r\n" +
                                    "package - it can play nearly everything. It comes with two\r\n" +
                                    "different media players - personally I prefer Media Player\r\n" +
                                    "Classic, although the other one has more eyecandy.\r\n" +
                                    "\r\n" +
                                    "Enjoy your offline YouTube movies!               ~Praetox");
                if (BatchJob) NextBatch();
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show("Houston, we have a problem.\r\n\r\n" +
                                "Occurred in: DownloadCompleteVoid\r\n" + ex.Message);
            }
        }
        
        private string GetFromBrowser(string Browser)
        {
            try
            {
                string oldclip = Clipboard.GetText();
                System.Diagnostics.Process[] prcOpera = System.Diagnostics.Process.GetProcessesByName(Browser);
                if (prcOpera.Length == 0)
                {
                    MessageBox.Show(Browser + " is not running!");
                    return "";
                }
                int Handle = (int)prcOpera[0].MainWindowHandle;
                SetForegroundWindow(Handle);
                Application.DoEvents();
                if (Browser == "Opera") SendKeys.SendWait("{f8}^c{tab}{tab}");
                if (Browser == "Firefox") SendKeys.SendWait("{f6}^c{tab}{tab}");
                if (Browser == "iExplore") SendKeys.SendWait("{f6}^c{tab}{tab}");
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
                SetForegroundWindow((int)this.Handle);
                string ret = Clipboard.GetText(); Application.DoEvents();
                Clipboard.Clear(); if (oldclip != null && oldclip != "") Clipboard.SetText(oldclip);
                if ((ret.IndexOf("youtube.com/watch?v=") == -1) || (ret.EndsWith("watch?v=")))
                {
                    MessageBox.Show("The open tab in " + Browser + " is not a youtube movie!");
                    return "";
                }
                if (ret.Length > ret.IndexOf(ytID_Prefix) + ytID_Prefix.Length + 11)
                    ret = ret.Substring(0, ret.IndexOf(ytID_Prefix) + ytID_Prefix.Length + 11);
                return ret;
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show("Houston, we have a problem.\r\n\r\n" +
                                "Occurred in: UrlFromWcVoid\r\n" + ex.Message);
                return "";
            }
        }
        private void cmdOpera_Click(object sender, EventArgs e)
        {
            string vl = GetFromBrowser("Opera");
            if (vl != "") tLink.Text = vl;
        }
        private void cmdFirefox_Click(object sender, EventArgs e)
        {
            string vl = GetFromBrowser("Firefox");
            if (vl != "") tLink.Text = vl;
        }
        private void cmdIExplore_Click(object sender, EventArgs e)
        {
            string vl = GetFromBrowser("iExplore");
            if (vl != "") tLink.Text = vl;
        }
        
        private void cQueue_Click(object sender, EventArgs e)
        {
            NextBatch();
        }
        private void NextBatch()
        {
            try
            {
                string sLinks = tQueue.Text; sLinks = sLinks.Replace("\r", "");
                if (sLinks.IndexOf(ytID_Prefix) == -1)
                {
                    MessageBox.Show("Batch download completed!");
                    return;
                }
                while (sLinks.StartsWith("\n")) sLinks = sLinks.Substring(1);
                while (sLinks.EndsWith("\n")) sLinks = sLinks.Substring(0, sLinks.Length - 1);

                int NextNewline = sLinks.IndexOf("\n");
                if (NextNewline != -1) sLinks = sLinks.Substring(0, NextNewline);
                tQueue.Text = tQueue.Text.Substring(tQueue.Text.IndexOf(sLinks) + sLinks.Length);
                if (tQueue.Text.StartsWith("\r\n")) tQueue.Text = tQueue.Text.Substring(2);
                tLink.Text = sLinks; Application.DoEvents();

                cGetname_Click(new object(), new EventArgs());
                while (System.IO.File.Exists(tFile.Text + ".flv"))
                    tFile.Text += "_";
                cDownload_Click(new object(), new EventArgs());
                Application.DoEvents(); BatchJob = true;
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show("Houston, we have a problem.\r\n\r\n" +
                                "Occurred in: NextInBatchVoid\r\n" + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text += Application.ProductVersion;
            string strError = "Warning!\r\n\r\nCould not verify the download template.\r\nProgram may not work!";
            #region Update checker and template downloader
            try
            {
                string wcRaw = new System.Net.WebClient().DownloadString(ptAppInfo);
                if (wcRaw.IndexOf("<ytDL_Template>") == -1)
                {
                    MessageBox.Show(strError);
                }
                else
                {
                    string Tpl = wcRaw;
                    Tpl = Tpl.Substring(Tpl.IndexOf("<ytDL_Template>") + "<ytDL_Template>".Length);
                    Tpl = Tpl.Substring(0, Tpl.IndexOf("</ytDL_Template>"));
                    ytDL_Template = Tpl;

                    //string Ver = wcRaw; string myVer = ProductVersion;
                    //Ver = Ver.Substring(Ver.IndexOf("<version>") + "<version>".Length);
                    //Ver = Ver.Substring(0, Ver.IndexOf("</version>"));
                    if (wcRaw.IndexOf("<version>" + Application.ProductVersion + "</version>") == -1)
                    {
                        MessageBox.Show("There is a new version available on my website.\r\n" +
                                        "Please re-download PYTD if you want to update.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strError + "\r\n\r\n" + "Specific error:\r\n" + ex.Message);
            }
            #endregion
        }
        private void tFile_Enter(object sender, EventArgs e)
        {
            if (tFile.Text == "PraetoxYTD_Movie") tFile.Text = "";
        }
        private void tLink_Enter(object sender, EventArgs e)
        {
            if (tLink.Text == "http://www.youtube.com/watch?v=P_N0KYb6GOU") tLink.Text = "";
        }
    }
}