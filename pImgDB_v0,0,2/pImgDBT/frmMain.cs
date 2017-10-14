using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace pImgDBT
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        #region APIs
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;
        #endregion
        #region Conversions
        /// <summary>
        /// Returns character from ascii
        /// </summary>
        private string Chr(int id)
        {
            return Convert.ToChar(id).ToString();
        }
        /// <summary>
        /// Returns ascii from character
        /// </summary>
        private int Asc(char id)
        {
            return Convert.ToInt16(id);
        }
        /// <summary>
        /// Converts integer to byte
        /// </summary>
        public byte[] Int2Byte(int Value)
        {
            byte[] rawbuf = BitConverter.GetBytes(Value);
            int a = 0; for (a = rawbuf.Length; a > 0; a--) if (rawbuf[a - 1] != 0) break;
            byte[] buf = new byte[a]; for (a = 0; a < buf.Length; a++) buf[a] = rawbuf[a];
            return buf;
        }
        /// <summary>
        /// Converts byte to string
        /// </summary>
        public String Byte2Str(byte[] Value)
        {
            int len = 0; for (len = Value.Length; len > 0; len--) if (Value[len - 1] != 0) break;
            string ret = ""; for (int a = 0; a < len; a++) ret += (char)Value[a];
            //byte[] buf = new byte[a]; for (a = 0; a < buf.Length; a++) buf[a] = Value[a];
            return ret; //System.Text.Encoding.ASCII.GetString(buf);
        }
        /// <summary>
        /// Converts string to byte
        /// </summary>
        public byte[] Str2Byte(string Value)
        {
            byte[] ret = new byte[Value.Length];
            for (int a = 0; a < Value.Length; a++) ret[a] = (byte)Value[a];
            return ret;
        }
        /// <summary>
        /// Converts string into unicode byte array
        /// </summary>
        private byte[] CnvToAByt(string src)
        {
            int iRet = 0;
            Encoder enc = System.Text.Encoding.UTF8.GetEncoder();
            char[] buf1 = src.ToCharArray();
            byte[] buf2 = new byte[src.Length * 4];
            enc.GetBytes(buf1, 0, buf1.Length, buf2, 0, true);
            for (iRet = 0; iRet < buf2.Length; iRet++)
            {
                if (buf2[iRet] == 0) break;
            }
            byte[] ret = new byte[iRet];
            for (int a = 0; a < ret.Length; a++)
            {
                ret[a] = buf2[a];
            }
            return ret;
        }
        /// <summary>
        /// Converts unicode byte array to string
        /// </summary>
        private string CnvToStr(byte[] src)
        {
            int buf2l = 0;
            Decoder dec = System.Text.Encoding.UTF8.GetDecoder();
            char[] buf1 = new char[src.Length];
            dec.GetChars(src, 0, src.Length, buf1, 0);
            for (buf2l = 0; buf2l < buf1.Length; buf2l++)
            {
                if (buf1[buf2l] == 0) break;
            }
            char[] buf2 = new char[buf2l];
            for (int a = 0; a < buf2.Length; a++)
            {
                buf2[a] = buf1[a];
            }
            string lol = new string(buf2);
            return lol;
        }
        #endregion
        #region "Standard" functions
        ///<summary>
        /// Splits msg by delim, returns bit part
        /// Chops returned string at next instance of delim
        ///</summary>
        public static string Split(string msg, string delim, int part)
        {
            try
            {
                string ret;
                string repd = "" + (char)2449 + (char)3983 + (char)5422 + (char)7882 + (char)9003;
                msg = msg.Replace("" + (char)9376, repd);
                ret = msg.Replace(delim, "" + (char)9376).Split((char)9376)[part];
                ret = ret.Replace(repd, "" + (char)9376);
                return ret;
            }
            catch { return ""; }
        }
        ///<summary>
        /// Splits msg by delim, returns string array of all results
        ///</summary>
        public static string[] aSplit(string msg, string delim)
        {
            try
            {
                string[] ret;
                string repd = "" + (char)2449 + (char)3983 + (char)5422 + (char)7882 + (char)9003;
                msg = msg.Replace("" + (char)9376, repd);
                ret = msg.Replace(delim, "" + (char)9376).Split((char)9376);
                for (int a = 0; a < ret.Length; a++)
                {
                    ret[a] = ret[a].Replace(repd, "" + (char)9376);
                }
                return ret;
            }
            catch { return new string[1]; }
        }
        ///<summary>
        /// Splits msg by delim, returns bit part
        /// Does not chop at next instance of delim
        ///</summary>
        public static string sSplit(string msg, string delim, int part)
        {
            try
            {
                int loc = -1;
                for (int a = 0; a < part; a++)
                {
                    loc = msg.IndexOf(delim, loc + 1);
                }
                return msg.Substring(loc + delim.Length);
            }
            catch { return ""; }
        }
        /// <summary>
        /// Returns true if str only contains chars specified in vl
        /// </summary>
        public static bool OnlyContains(string str, string vl)
        {
            for (int a = 0; a < str.Length; a++)
                if (!vl.Contains("" + str[a])) return false;
            return true;
        }
        ///<summary>
        /// Converts a number (vl) into a digit-grouped string
        ///</summary>
        public static string Decimize(string vl)
        {
            string ret = ""; int spt = 0;
            while (vl.Length != 0)
            {
                if (spt == 3)
                {
                    ret = "," + ret; spt = 0;
                }
                ret = vl.Substring(vl.Length - 1, 1) + ret;
                vl = vl.Substring(0, vl.Length - 1);
                spt++;
            }
            return ret;
        }
        ///<summary>
        /// Creates a string containing vl number of spaces
        ///</summary>
        public static string Space(int vl)
        {
            string ret = new string(' ', vl); //"";
            //for (int a = 0; a < vl; a++)
            //{
            //    ret += " ";
            //}
            return ret;
        }
        ///<summary>
        /// Removes all spaces at start and end of vl
        ///</summary>
        public static string unSpace(string vl)
        {
            string ret = vl;
            try
            {
                while (ret.Substring(0, 1) == " ")
                {
                    ret = ret.Substring(1);
                }
                while (ret.Substring(ret.Length - 1) == " ")
                {
                    ret = ret.Substring(0, ret.Length - 1);
                }
                return ret;
            }
            catch
            {
                return "";
            }
        }
        ///<summary>
        /// Removes all newlines at start and end of vl
        /// </summary>
        public static string unNewline(string vl)
        {
            try
            {
                while ((vl.Substring(0, 1) == "\r") || (vl.Substring(0, 1) == "\n"))
                {
                    vl = vl.Substring(1);
                }
                while ((vl.Substring(vl.Length - 1) == "\r") || (vl.Substring(vl.Length - 1) == "\n"))
                {
                    vl = vl.Substring(0, vl.Length - 1);
                }
                return vl;
            }
            catch
            {
                return "";
            }
        }
        ///<summary>
        /// Returns system clock in seconds
        ///</summary>
        public int sTick()
        {
            return (System.DateTime.Now.Hour * 60 * 60) +
                   (System.DateTime.Now.Minute * 60) +
                   (System.DateTime.Now.Second);
        }
        ///<summary>
        /// Sleeps for vl seconds
        ///</summary>
        public void iSlp(int vl)
        {
            long lol = Tick();
            while (Tick() < lol + vl)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
        }
        ///<summary>
        /// Returns system clock in miliseconds
        ///</summary>
        public long Tick()
        {
            return System.DateTime.Now.Ticks / 10000;
        }
        ///<summary>
        /// Returns hh:mm:ss of vl (seconds)
        ///</summary>
        public string s2ms(int vl)
        {
            int iHour = 0; int iMins = 0; int iSecs = vl;
            while (iSecs >= 60)
            {
                iSecs -= 60; iMins++;
            }
            while (iMins >= 60)
            {
                iMins -= 60; iHour++;
            }
            string sHour = Convert.ToString(iHour); sHour = Space(2 - sHour.Length).Replace(" ", "0") + sHour;
            string sMins = Convert.ToString(iMins); sMins = Space(2 - sMins.Length).Replace(" ", "0") + sMins;
            string sSecs = Convert.ToString(iSecs); sSecs = Space(2 - sSecs.Length).Replace(" ", "0") + sSecs;
            return sHour + ":" + sMins + ":" + sSecs;
        }
        ///<summary>
        /// Returns current date and time
        ///</summary>
        public string TDNow()
        {
            return System.DateTime.Now.ToLongDateString() + " .::. " +
                   System.DateTime.Now.ToLongTimeString();
        }
        ///<summary>
        /// Returns MD5 of vl
        ///</summary>
        public string MD5(string vl)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(vl);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
        /// <summary>
        /// Makes sure the process ends completely
        /// </summary>
        public static void pKillMe()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        #endregion
        #region File and array manipulation
        /// <summary>
        /// Reads sFile, works with norwegian letters ae oo aa
        /// </summary>
        private string FileRead(string sFile)
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream
                    (sFile, System.IO.FileMode.Open);
                byte[] buf = new byte[fs.Length];
                fs.Read(buf, 0, Convert.ToInt32(fs.Length));
                fs.Flush(); fs.Close(); fs.Dispose();
                return CnvToStr(buf);
            }
            catch { return ""; }
        }
        /// <summary>
        /// Writes sVal to sFile, works with norwegian letters ae oo aa
        /// </summary>
        /// <param name="sFile">Target file</param>
        /// <param name="sVal">The string to write</param>
        /// <param name="bAppend">Append instead of overwrite</param>
        private void FileWrite(string sFile, string Data, bool bAppend)
        {
            byte[] buf = CnvToAByt(Data);
            System.IO.FileMode AccessType = System.IO.FileMode.Create;
            if (bAppend) AccessType = System.IO.FileMode.Append;
            System.IO.FileStream fs = new System.IO.FileStream(sFile, AccessType);
            if (bAppend) fs.Seek(0, System.IO.SeekOrigin.End);
            fs.Write(buf, 0, buf.Length);
            fs.Flush(); fs.Close(); fs.Dispose();
        }
        public void FileWrite(string sFile, string sVal)
        {
            FileWrite(sFile, sVal, false);
        }
        /// <summary>
        /// Sorts vl[] alphabetically, ignores letters other than 0-9 a-z
        /// </summary>
        public string[] SortStringArrayAlphabetically(string[] vl)
        {
            for (int a = vl.GetUpperBound(0); a >= 0; a--)
            {
                for (int b = 0; b < a; b++) //changed "b <= a" to "b < a"
                {
                    if (string.Compare(vl[b], vl[b + 1], true) > 0)
                    {
                        //Swap values
                        string tmp = vl[b].ToString();
                        vl[b] = vl[b + 1];
                        vl[b + 1] = tmp;
                    }
                }
            }
            return vl;
        }
        #endregion
        #region Global variables
        double[] dDPI_Original = new double[] { 96, 96 };
        double[] dDPI_Current = new double[] { 96, 96 };
        double[] dDPIf = new double[] { 1, 1 };
        Random rnd = new Random();
        Form wfWait = new frmWait();
        Form wfSplash = new frmSplash();
        DateTime dtLastWPChange = DateTime.Now.AddDays(-1);
        private Color[] clr_pnThumb = new Color[] { Color.FromArgb(32, 32, 32),
                                                    Color.FromArgb(200, 220, 230) };

        public static string sAppPath = "";
        public static IntPtr ipMyHandle = (IntPtr)0;
        public static string sAppVer = "";
        public static FormWindowState fwsMyState = FormWindowState.Normal;
        private static bool bInitialRun = true;
        private static bool bInHotkeys = false;
        private static bool bInPFClicks = false;
        public static string PrgDomain = "http://tox.awardspace.us/div/";
        public static string ToxDomain = "http://www.praetox.com/";
        public static bool bRunning = false;

        private static string QEdit_sName = "";
        private static string QEdit_sRating = "";
        private static string QEdit_sTGeneral = "";
        private static string QEdit_sTSource = "";
        private static string QEdit_sTChars = "";
        private static string QEdit_sTArtists = "";

        private int Upload_iDone = 0;
        private int Upload_iError_HiRes = 0;
        private int Upload_iError_HiSize = 0;
        private int Upload_iError_Exists = 0;
        private int Upload_iError_iLimit = 0;
        private int Upload_iError_Other = 0;

        private static SearchPrm LastSearch;
        double dStats_SelCount = 0, dStats_SelSize = 0, dStats_thPage = 0;
        double dStats_TotCount = 0, dStats_TotSize = 0, dStats_thPages = 0;

        public static Int32 DispImageIndex = -9001;
        public static Bitmap[] DispImage = new Bitmap[3];
        public static Bitmap bDummy = new Bitmap(1, 1);
        public static Bitmap bDenied = new Bitmap(1, 1);
        public static Bitmap btDenied = new Bitmap(1, 1);
        public static BackgroundWorker bwPrevDisp;
        public static BackgroundWorker bwNextDisp;
        public static bool bwPrevVoid = false;
        public static bool bwNextVoid = false;

        public static string sGSep = "   ~   ";
        public static string sFirstImportRoot = "";
        public static string[] saFullPaths = new string[0];
        
        public static Point ptThumbSize;
        public static int iThumbPage = 0;
        public static ImageData[] idImages = new ImageData[0];
        public static thPageData[] thPages = new thPageData[3];
        public static string[] saAllowedTypes = new string[0];
        
        public static PanelArray pnThumb;
        public static PBoxArray pThumb;
        private static Panel[] pnaPanelsMain = new Panel[0];
        private static Panel[] pnaPanelsControl = new Panel[0];
        private static Panel pnActiveMain = new Panel();
        private static Panel pnActiveControl = new Panel();
        #endregion

        private void frmMain_Load(object sender, EventArgs e)
        {
            sAppVer = Application.ProductVersion.Substring(
                0, Application.ProductVersion.LastIndexOf("."));
            this.Text += sAppVer;

            frmWait.bVisible = true;
            frmWait.sHeader = "pImgDB - Loading";
            frmWait.sMain = "Loading...";
            frmWait.sFooter = ToxDomain;
            frmWait.bInstant = true;
            wfWait.Show();
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            frmWait.sMain = "Configuring GUI (pt.1)"; frmWait.bInstant = true;
            while (frmWait.bInstant) Application.DoEvents();
            for (int a = 0; a < thPages.Length; a++)
            {
                thPages[a] = new thPageData();
            }
            saAllowedTypes = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".tiff" };
            pnaPanelsMain = new Panel[] { pnThumbs, pnDisplay, pnImport, pnDBStats, pnSearchInit, pnWPChanger, pnMassEdit, pnUpload };
            pnaPanelsControl = new Panel[] { pnGeneral, pnQEdit };
            pnActiveMain = pnThumbs;
            pnActiveControl = pnGeneral;
            for (int a = 0; a < pnaPanelsMain.Length; a++)
            {
                CSet_ddMainSet.Items.Add((a + 1) + " - " + pnaPanelsMain[a].Tag);
            }
            for (int a = 0; a < pnaPanelsControl.Length; a++)
            {
                CSet_ddControlSet.Items.Add((a + 1) + " - " + pnaPanelsControl[a].Tag);
            }
            CSet_ddMainSet.SelectedIndex = 0;
            CSet_ddControlSet.SelectedIndex = 0;

            frmWait.sMain = "Configuring GUI (pt.2)"; frmWait.bInstant = true;
            while (frmWait.bInstant) Application.DoEvents();
            sAppPath = Application.StartupPath.Replace("\\", "/");
            if (!sAppPath.EndsWith("/")) sAppPath += "/";
            string[] saFolders = System.IO.Directory.GetDirectories(sAppPath, "db_*");
            for (int a = 0; a < saFolders.Length; a++)
            {
                General_cbDatabase.Items.Add((a + 1) + " - " + saFolders[a].Substring(sAppPath.Length + 3));
            }
            string sLastDB_Name = Program.Reg_Access("Last DB - Name", "");
            if (sLastDB_Name != "")
            {
                General_cbDatabase.SelectedIndex = -1;
                for (int a = 0; a < saFolders.Length; a++)
                {
                    if ((saFolders[a].Substring(sAppPath.Length + 3)) == sLastDB_Name)
                        General_cbDatabase.SelectedIndex = a;
                }
            }

            frmWait.sMain = "Loading resources"; frmWait.bInstant = true;
            while (frmWait.bInstant) Application.DoEvents();
            System.IO.Stream streamBMP = System.Reflection.Assembly.
                GetExecutingAssembly().GetManifestResourceStream(
                "pImgDBT.Graphics.denied.png");
            Bitmap bTmp = (Bitmap)Bitmap.FromStream(streamBMP);
            bDenied = (Bitmap)bTmp.Clone(); bTmp.Dispose();
            streamBMP.Close(); streamBMP.Dispose();
            WPChanger_cmdLoad_Click(new object(), new EventArgs());
            Upload_cmdPass_Click(new object(), new EventArgs());

            frmWait.sMain = "Polling for SQLite"; frmWait.bInstant = true;
            while (frmWait.bInstant) Application.DoEvents();
            if (!System.IO.File.Exists("System.Data.SQLite.dll"))
            {
                DialogResult OSType = MessageBox.Show(
                    "Do you have a 32bit-based OS?" + "\r\n\r\n" +
                    "Press YES if you have a 32bit OS (common)" + "\r\n" +
                    "Press NO if you have a 64bit OS (rare)" + "\r\n\r\n" +
                    "This does not relate to your CPU." + "\r\n" +
                    "If you pick the wrong choice, delete" + "\r\n" +
                    "System.Data.SQLite.dll and restart pImgDB.",
                    "Determining OS", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Asterisk);
                if (OSType == DialogResult.Cancel) { this.Dispose(); return; }
                if (OSType == DialogResult.Yes) System.IO.File.Copy(
                    "SQLite_32.dll", "System.Data.SQLite.dll");
                if (OSType == DialogResult.No) System.IO.File.Copy(
                    "SQLite_64.dll", "System.Data.SQLite.dll");
            }

            frmWait.sMain = "Creating helpers"; frmWait.bInstant = true;
            while (frmWait.bInstant) Application.DoEvents();
            ipMyHandle = this.Handle;
            bwPrevDisp = new BackgroundWorker();
            bwNextDisp = new BackgroundWorker();
            bwPrevDisp.DoWork += new DoWorkEventHandler(bwPrevDisp_DoWork);
            bwNextDisp.DoWork += new DoWorkEventHandler(bwNextDisp_DoWork);
            if (!System.IO.Directory.Exists("_tmp"))
                System.IO.Directory.CreateDirectory("_tmp");

            frmWait.sMain = "Checking for updates"; frmWait.bInstant = true;
            while (frmWait.bInstant) Application.DoEvents();
            try
            {
                bool bUpdateCheckOK = true;
                WebReq WR = new WebReq();
                WR.Request(PrgDomain + "pImgDB_version.php?cv=" + sAppVer);
                long lUpdateStart = Tick();
                while (!WR.isReady && bUpdateCheckOK)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(10);
                    if (Tick() > lUpdateStart + 5000)
                        bUpdateCheckOK = false;
                }
                string wrh = WR.Response;
                if (!bUpdateCheckOK) throw new Exception("wat");
                if (wrh.Contains("<WebReq_Error>")) throw new Exception("wat");

                if (!wrh.Contains("<VERSION>" + sAppVer + "</VERSION>"))
                {
                    string sNewVer = Split(Split(wrh, "<VERSION>", 1), "</VERSION>", 0);
                    bool GetUpdate = (DialogResult.Yes == MessageBox.Show(
                        "A new version (" + sNewVer + ") is available. Update?",
                        "pImgDBT Updater", MessageBoxButtons.YesNo));
                    if (GetUpdate)
                    {
                        string UpdateLink = new System.Net.WebClient().DownloadString(
                            ToxDomain + "inf/pImgDB_link.html").Split('%')[1];
                        System.Diagnostics.Process.Start(UpdateLink + "?cv=" + sAppVer);
                        Application.Exit();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Couldn't check for updates.", "pImgDBT Updater");
            }

            frmWait.sMain = "Finalizing"; frmWait.bInstant = true;
            while (frmWait.bInstant) Application.DoEvents();
            pThumb = new PBoxArray(this);
            pnThumb = new PanelArray(this);
            frmWait.bVisible = false;
            while (frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            ShowForm();
            bRunning = true;
            frmMain_Resize(new object(), new EventArgs());
            //tResizeThumbs.Start();
        }
        private void ShowForm()
        {
            this.Opacity = (double)0;
            this.Show(); this.BringToFront();
            Application.DoEvents();
            for (int a = 0; a <= 10; a++)
            {
                this.Opacity = (double)a / 10;
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
        }
        private void HideForm()
        {
            for (int a = 7; a >= 0; a--)
            {
                this.Opacity = (double)a / 7;
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
            this.Hide(); Application.DoEvents();
            this.Opacity = (double)1;
        }

        public Bitmap ResizeBitmap(Bitmap bPic, int iX, int iY, bool bKeepAspect, int iScaleMode)
        {
            double dRaX = (double)bPic.Width / (double)iX;
            double dRaY = (double)bPic.Height / (double)iY;
            if (bKeepAspect)
            {
                if (dRaX > dRaY) iY = (int)Math.Round((double)iX / ((double)bPic.Width / (double)bPic.Height));
                if (dRaY > dRaX) iX = (int)Math.Round((double)iY * ((double)bPic.Width / (double)bPic.Height));
            }
            Bitmap bOut = new Bitmap(iX, iY);
            using (Graphics gOut = Graphics.FromImage((Image)bOut))
            {
                gOut.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                if (iScaleMode==2) gOut.InterpolationMode =
                    System.Drawing.Drawing2D.InterpolationMode.High;
                if (iScaleMode==3) gOut.InterpolationMode =
                    System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                gOut.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gOut.DrawImage(bPic, 0, 0, iX, iY);
            }
            return bOut;
        }
        public string MD5File(string sFile)
        {
            System.IO.FileStream fs = new System.IO.FileStream(sFile, System.IO.FileMode.Open);
            byte[] bFile = new byte[fs.Length];
            fs.Read(bFile, 0, (int)fs.Length);
            fs.Close(); fs.Dispose();
            System.Security.Cryptography.MD5CryptoServiceProvider crypt =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bRet = crypt.ComputeHash(bFile);
            string ret = "";
            for (int a = 0; a < bRet.Length; a++)
                ret += bRet[a].ToString("X");
            crypt.Clear(); return ret;
        }
        private Point CalcThumbSize(int ThumbX, int ThumbY)
        {
            if (ThumbX == 0) ThumbX = (int)Math.Round((((double)ThumbY / 10) * 16), 0); //Thumb W (16/10)
            if (ThumbY == 0) ThumbY = (int)Math.Round((((double)ThumbX / 16) * 10), 0); //Thumb H (16/10)
            return new Point(ThumbX, ThumbY);
        }
        private Point CalcThumbCountXY(Point szCont, int ThumbX, int ThumbY)
        {
            if (ThumbX == 0 && ThumbY == 0) return new Point(0, 0); //O SHI-
            Point ptThumb = CalcThumbSize(ThumbX, ThumbY); //Get thumb size
            int ThPanX = ptThumb.X + 4 + 2; int ThPanY = ptThumb.Y + 4 + 2; //Panel W/H
            int ThTotX = ThPanX + 2; int ThTotY = ThPanY + 2; //Panel+Dist W/H
            szCont.X -= 4 + 4; szCont.Y -= 4 + 4; //Offsets W/H
            //szCont.X += 2; szCont.Y += 2; //Remove last dist
            //szCont.X -= 2; szCont.Y -= 2; //Subtract frame
            return new Point(
                (int)Math.Floor((double)szCont.X / (double)ThTotX),
                (int)Math.Floor((double)szCont.Y / (double)ThTotY));
        }
        private Point CalcThumbMaxSize(Point szCont, int ThumbsY)
        {
            int ThumbX = 0, ThumbY = 0;
            for (int x = 1; x < 32768; x++)
            {
                Point ptThSize = CalcThumbSize(x, 0); //Get thumb size
                Point ptThCount = CalcThumbCountXY(szCont, ptThSize.X, ptThSize.Y); //Get thumb count
                if (ptThCount.X < 1) break; //No thumbs at all
                if (ptThCount.Y < ThumbsY) break; //Below threshold
                ThumbX = ptThSize.X; ThumbY = ptThSize.Y; //Good to go
            }
            return new Point(ThumbX, ThumbY);
        }
        private void RedrawThumbnails(bool bRecreateControls)
        {
            if (bRecreateControls)
            {
                for (int a = 0; a < pThumb.Count; a++)
                {
                    pThumb[a].Visible = false;
                    pnThumb[a].Visible = false;
                    this.Controls.Add(pThumb[a]);
                    this.Controls.Add(pnThumb[a]);
                }
                while (pThumb.Count > 0) pThumb.Remove();
                while (pnThumb.Count > 0) pnThumb.Remove();
            }
            int iThumbs = Convert.ToInt32(General_txtRows.Text);
            ptThumbSize = CalcThumbMaxSize((Point)pnThumbs.Size, Convert.ToInt32(iThumbs));
            Point ptThumbCount = CalcThumbCountXY((Point)pnThumbs.Size, ptThumbSize.X, ptThumbSize.Y);
            Point ptThumbPSize = new Point(ptThumbSize.X + 4 + 2, ptThumbSize.Y + 4 + 2);
            btDenied.Dispose(); btDenied = ResizeBitmap(bDenied, ptThumbSize.X, ptThumbSize.Y, true, 3);
            for (int y = 0; y < ptThumbCount.Y; y++)
            {
                for (int x = 0; x < ptThumbCount.X; x++)
                {
                    int iThis = (y * (ptThumbCount.X)) + x;
                    if (bRecreateControls) pnThumb.NewPanel();
                    pnThumbs.Controls.Add(pnThumb[iThis]);
                    pnThumb[iThis].Size = (Size)ptThumbPSize;
                    pnThumb[iThis].BorderStyle = BorderStyle.FixedSingle;
                    pnThumb[iThis].Location = new Point(
                        4 + (x * (ptThumbPSize.X + 2)),
                        4 + (y * (ptThumbPSize.Y + 2)));
                    pnThumb[iThis].BackColor = clr_pnThumb[0];
                    if (bRecreateControls) pThumb.NewPBox();
                    pnThumb[iThis].Controls.Add(pThumb[iThis]);
                    pThumb[iThis].Size = (Size)ptThumbSize;
                    pThumb[iThis].Location = new Point(2, 2);
                    pThumb[iThis].SizeMode = PictureBoxSizeMode.Zoom;
                    pThumb[iThis].BackColor = clr_pnThumb[0];
                    pThumb[iThis].BorderStyle = BorderStyle.FixedSingle;
                }
            }
            if (thPages[1].bImages != null && thPages[1].idRange.X != -1)
            {
                for (int a = 0; a < pThumb.Count; a++)
                {
                    if (a >= thPages[1].bImages.Length) break;
                    int iThisID = thPages[1].idRange.X + a;
                    if (iThisID >= idImages.Length) break;
                    if (!idImages[iThisID].bDeleted)
                        pThumb[a].Image = (Image)thPages[1].bImages[a];
                    else pThumb[a].Image = btDenied as Image;
                    if (idImages[iThisID].bSelected)
                        pnThumb[a].BackColor = clr_pnThumb[1];
                    else pnThumb[a].BackColor = clr_pnThumb[0];
                }
            }
            RecountStatistics();
        }
        private void ReloadThumbnails(int iPage, bool bAll)
        {
            if (bAll)
            {
                ReloadThumbnails(1, false);
                ReloadThumbnails(2, false);
                ReloadThumbnails(0, false);
            }
            else
            {
                if (iPage == 1)
                    for (int a = 0; a < pThumb.Count; a++)
                        pThumb[a].Image = bDummy as Image;

                if (thPages[iPage].bImages != null)
                    for (int a = 0; a < thPages[iPage].bImages.Length; a++)
                        if (thPages[iPage].bImages[a] != null)
                            thPages[iPage].bImages[a].Dispose();
                thPages[iPage].bImages = new Bitmap[pThumb.Count];
                double dMul = Convert.ToDouble(General_txtHRes.Text);
                int iIDCnt = thPages[iPage].idRange.Y - thPages[iPage].idRange.X;
                for (int a = 0; a < pThumb.Count; a++)
                {
                    if (a <= iIDCnt && thPages[iPage].idRange.Y != -1)
                    {
                        int iThisID = thPages[iPage].idRange.X + a;
                        if (!idImages[iThisID].bDeleted)
                        {
                            Bitmap wat = (Bitmap)Bitmap.FromFile(sAppPath + DB.Path +
                                idImages[iThisID].sHash + "." + idImages[iThisID].sType);
                            int dsX = (int)Math.Round((double)ptThumbSize.X * dMul);
                            int dsY = (int)Math.Round((double)ptThumbSize.Y * dMul);
                            thPages[iPage].bImages[a] = ResizeBitmap(wat, dsX, dsY, true, 2);
                            if (iPage == 1)
                            {
                                pThumb[a].Image = thPages[iPage].bImages[a] as Image;
                                Application.DoEvents();
                            }
                            wat.Dispose();
                        }
                        else
                        {
                            pThumb[a].Image = btDenied as Image;
                        }
                    }
                }
                RecountStatistics();
            }
        }
        private void ShowPanel(Panel pnShow)
        {
            bool bIsMainPanel = false;
            bool bIsControlPanel = false;
            for (int a = 0; a < pnaPanelsMain.Length; a++)
                if (pnaPanelsMain[a] == pnShow) bIsMainPanel = true;
            for (int a = 0; a < pnaPanelsControl.Length; a++)
                if (pnaPanelsControl[a] == pnShow) bIsControlPanel = true;
            if (bIsMainPanel)
            {
                int iPanelIndex = -1;
                for (int a = 0; a < pnaPanelsMain.Length; a++)
                {
                    if (pnaPanelsMain[a] == pnShow) iPanelIndex = a;
                    pnaPanelsMain[a].Visible = false;
                }
                pnShow.Visible = true;
                pnActiveMain = pnShow;
                CSet_ddMainSet.SelectedIndex = iPanelIndex;
            }
            if (bIsControlPanel)
            {
                int iPanelIndex = -1;
                for (int a = 0; a < pnaPanelsControl.Length; a++)
                {
                    if (pnaPanelsControl[a] == pnShow) iPanelIndex = a;
                    pnaPanelsControl[a].Visible = false;
                }
                pnShow.Visible = true;
                pnActiveControl = pnShow;
                CSet_ddControlSet.SelectedIndex = iPanelIndex;
            }
        }
        public void DispLoad(int iIndex)
        {
            int iDist = iIndex - DispImageIndex;
            if (iIndex < 0 || iIndex > idImages.Length) return;
            if ((int)Math.Abs(iDist) <= 1) { DispSkip(iDist); return; }
            DispImageIndex = iIndex;
            if (idImages[DispImageIndex].bDeleted)
            {
                int iPrev = DispImageIndex;
                int iNext = DispImageIndex;
                DispImageIndex = -1;
                while (true)
                {
                    if (!idImages[iPrev].bDeleted) break;
                    iPrev--; if (iPrev < 0)
                    {
                        iPrev = -1; break;
                    }
                }
                while (true)
                {
                    if (!idImages[iNext].bDeleted) break;
                    iNext++; if (iNext >= idImages.Length)
                    {
                        iNext = -1; break;
                    }
                }
                if (iPrev != -1) DispImageIndex = iPrev;
                if (iNext != -1) DispImageIndex = iNext;
            }
            if (DispImageIndex != -1)
                DispLoad(sAppPath + DB.Path +
                idImages[DispImageIndex].sHash + "." +
                idImages[DispImageIndex].sType);
            else DispLoad(ResizeBitmap(
                bDenied, Display_pbDisplay.Width,
                Display_pbDisplay.Height, true, 3));
            if (DispImage[0] != null) DispImage[0].Dispose();
            if (DispImage[2] != null) DispImage[2].Dispose();
            while (bwPrevDisp.IsBusy || bwNextDisp.IsBusy)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            bwPrevDisp.RunWorkerAsync();
            bwNextDisp.RunWorkerAsync();
            ShowPanel(pnQEdit);
            ViewImageInfo(DispImageIndex, QEdit_chkAutorefresh.Checked);
        }
        public void DispLoad(string sFilename)
        {
            Bitmap bTmp = (Bitmap)Bitmap.FromFile(sFilename);
            DispLoad(bTmp); bTmp.Dispose();
        }
        public void DispLoad(Bitmap bImage)
        {
            Display_pbDisplay.Image = bDummy as Image;
            if (DispImage[1] != null) DispImage[1].Dispose();
            //DispImage[1] = (Bitmap)bImage.Clone();
            DispImage[1] = (Bitmap)ResizeBitmap(
                bImage, (int)((double)Display_pbDisplay.Width*1.5),
                (int)((double)Display_pbDisplay.Height * 1.5), true, 2).Clone();
            Display_pbDisplay.Image = DispImage[1] as Image; bImage.Dispose();
        }
        public void DispSkip(int iSteps)
        {
            int iIndex = DispImageIndex + iSteps;
            if (iIndex < 0 || iIndex >= idImages.Length) return;
            if ((int)Math.Abs(iSteps) > 1) { DispLoad(iIndex); return; }
            if (iSteps == -1)
            {
                int iTestDel = DispImageIndex;
                while (true)
                {
                    iTestDel--; if (iTestDel < 0) return;
                    if (!idImages[iTestDel].bDeleted) break;
                }
                while (bwPrevDisp.IsBusy)
                {
                    System.Threading.Thread.Sleep(1);
                    Application.DoEvents();
                    if (bwPrevVoid) return;
                }
                if (bwPrevVoid) return;
                Display_pbDisplay.Image = bDummy as Image;
                if (DispImage[2] != null) DispImage[2].Dispose(); DispImage[2] = DispImage[1].Clone() as Bitmap;
                if (DispImage[1] != null) DispImage[1].Dispose(); DispImage[1] = DispImage[0].Clone() as Bitmap;
                if (DispImage[0] != null) DispImage[0].Dispose();
                Display_pbDisplay.Image = DispImage[1] as Image;
                while (true)
                {
                    DispImageIndex--; if (DispImageIndex < 0) return;
                    if (!idImages[DispImageIndex].bDeleted) break;
                }
                bwPrevDisp.RunWorkerAsync();
            }
            if (iSteps == 1)
            {
                int iTestDel = DispImageIndex;
                while (true)
                {
                    iTestDel++; if (iTestDel >= idImages.Length) return;
                    if (!idImages[iTestDel].bDeleted) break;
                }
                while (bwNextDisp.IsBusy)
                {
                    System.Threading.Thread.Sleep(1);
                    Application.DoEvents();
                    if (bwNextVoid) return;
                }
                if (bwNextVoid) return;
                Display_pbDisplay.Image = bDummy as Image;
                if (DispImage[0] != null) DispImage[0].Dispose(); DispImage[0] = DispImage[1].Clone() as Bitmap;
                if (DispImage[1] != null) DispImage[1].Dispose(); DispImage[1] = DispImage[2].Clone() as Bitmap;
                if (DispImage[2] != null) DispImage[2].Dispose();
                Display_pbDisplay.Image = DispImage[1] as Image;
                while (true)
                {
                    DispImageIndex++; if (DispImageIndex >= idImages.Length) return;
                    if (!idImages[DispImageIndex].bDeleted) break;
                }
                bwNextDisp.RunWorkerAsync();
            }
            ViewImageInfo(DispImageIndex, QEdit_chkAutorefresh.Checked);
        }
        public void DispUnload()
        {
            Display_pbDisplay.Image = bDummy as Image;
            if (DispImage[0] != null) DispImage[0].Dispose();
            if (DispImage[1] != null) DispImage[1].Dispose();
            if (DispImage[2] != null) DispImage[2].Dispose();
            DispImageIndex = -9001;
        }
        void bwPrevDisp_DoWork(object sender, DoWorkEventArgs e)
        {
            int iDII = DispImageIndex;
            int iPrevImg = DispImageIndex;
            while (true)
            {
                iPrevImg--; if (iPrevImg < 0) return;
                if (!idImages[iPrevImg].bDeleted) break;
            }
            if (DispImage[0] != null) DispImage[0].Dispose();
            /*DispImage[0] = (Bitmap)Bitmap.FromFile(sAppPath + DB.Path +
                idImages[iPrevImg].sHash + "." +
                idImages[iPrevImg].sType);*/
            Bitmap bTmp1 = (Bitmap)Bitmap.FromFile(sAppPath + DB.Path +
                idImages[iPrevImg].sHash + "." + idImages[iPrevImg].sType);
            Bitmap bTmp2 = ResizeBitmap(
                bTmp1, (int)((double)Display_pbDisplay.Width * 1.5),
                (int)((double)Display_pbDisplay.Height * 1.5), true, 2);
            if (DispImageIndex == iDII)
            {
                DispImage[0] = (Bitmap)bTmp2.Clone();
                bwPrevVoid = false;
            }
            else bwPrevVoid = true;
            bTmp1.Dispose(); bTmp2.Dispose();
        }
        void bwNextDisp_DoWork(object sender, DoWorkEventArgs e)
        {
            int iDII = DispImageIndex;
            int iNextImg = DispImageIndex;
            while (true)
            {
                iNextImg++; if (iNextImg >= idImages.Length) return;
                if (!idImages[iNextImg].bDeleted) break;
            }
            if (DispImage[2] != null) DispImage[2].Dispose();
            /*DispImage[2] = (Bitmap)Bitmap.FromFile(sAppPath + DB.Path +
                idImages[iNextImg].sHash + "." +
                idImages[iNextImg].sType);*/
            Bitmap bTmp1 = (Bitmap)Bitmap.FromFile(sAppPath + DB.Path +
                idImages[iNextImg].sHash + "." + idImages[iNextImg].sType);
            Bitmap bTmp2 = ResizeBitmap(
                bTmp1, (int)((double)Display_pbDisplay.Width * 1.5),
                (int)((double)Display_pbDisplay.Height * 1.5), true, 2);
            if (DispImageIndex == iDII)
            {
                DispImage[2] = (Bitmap)bTmp2.Clone();
                bwNextVoid = false;
            }
            else bwNextVoid = true;
            bTmp1.Dispose(); bTmp2.Dispose();
        }
        private bool ToggleSelect(int iIndex)
        {
            if (iIndex == -1) return false;
            if (idImages == null) return false;
            if (iIndex >= idImages.Length) return false;
            if (idImages[iIndex].bSelected)
            {
                idImages[iIndex].bSelected = false;
                pnThumb[iIndex - thPages[1].idRange.X].BackColor = clr_pnThumb[0];
            }
            else
            {
                idImages[iIndex].bSelected = true;
                pnThumb[iIndex - thPages[1].idRange.X].BackColor = clr_pnThumb[1];
            }
            RecountStatistics();
            return idImages[iIndex].bSelected;
        }
        private void tPollForClicks_Tick(object sender, EventArgs e)
        {
            //this.Text = pThumb.Ci.iCount + "";
            if (bRunning && !bInPFClicks)
            {
                bInPFClicks = true;
                if (pThumb.Ci.bPoll)
                {
                    pThumb.Ci.bPoll = false;
                    if (!DB.IsOpen)
                    {
                        MessageBox.Show("Please select or create a database first.",
                            "You are doing it wrong.", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        int iIndex = pThumb.Ci.iIndex;
                        if (pThumb.Ci.iKey == 1)
                        {
                            if (pThumb.Ci.iCount == 1)
                            {
                                if (!pnThumbs.Controls.Contains(pThumb[iIndex]))
                                {
                                    /*long tStart = Tick();
                                    while (Tick() - tStart < 200)
                                    {
                                        Application.DoEvents();
                                        System.Threading.Thread.Sleep(1);
                                        this.Text = pThumb.Ci.iCount + "";
                                        if (pThumb.Ci.iCount != 1 ||
                                            pThumb.Ci.iIndex != iIndex)
                                        {
                                            bInPFClicks = false; return;
                                        }
                                    }*/
                                    //pThumb.Ci = new ClickInfo();
                                    ToggleSelect(thPages[1].idRange.X + iIndex);
                                }
                            }
                            else if (pThumb.Ci.iCount == -1)
                            {
                                int iIDI = thPages[1].idRange.X + iIndex;
                                ToggleSelect(iIDI); DispLoad(iIDI);
                                DispImageIndex = iIDI; ShowPanel(pnDisplay);
                            }
                        }
                        else
                        {
                            rcmThumbs.Show(pThumb.Ci.ptLoc);
                        }
                    }
                }
                bInPFClicks = false;
            }
        }

        private void General_cmdRedraw_Click(object sender, EventArgs e)
        {
            if (!DB.IsOpen)
            {
                MessageBox.Show("Please select or create a database first.",
                    "You are doing it wrong.", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation); return;
            }
            RedrawThumbnails(true);
        }
        private void General_cmdReload_Click(object sender, EventArgs e)
        {
            if (!DB.IsOpen)
            {
                MessageBox.Show("Please select or create a database first.",
                    "You are doing it wrong.", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation); return;
            }
            ReloadThumbnails(1, false); //true);
        }
        private void View_cmdNext_Click(object sender, EventArgs e)
        {
            if (pnDisplay.Visible) DispSkip(1);
            if (pnThumbs.Visible) ChangeThumbPage(1);
        }
        private void View_cmdPrev_Click(object sender, EventArgs e)
        {
            if (pnDisplay.Visible) DispSkip(-1);
            if (pnThumbs.Visible) ChangeThumbPage(-1);
        }
        private void General_cmdAllImages_Click(object sender, EventArgs e)
        {
            if (GUI_CheckForChanges()) return;
            if (!DB.IsOpen)
            {
                MessageBox.Show("Please select or create a database first.",
                    "You are doing it wrong.", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation); return;
            }
            int iCnt = DB.Entries();
            idImages = new ImageData[iCnt];
            int iIndex = -1;
            using (SQLiteCommand DBc = DB.Data.CreateCommand())
            {
                DBc.CommandText = "SELECT * FROM 'images'";
                using (SQLiteDataReader rd = DBc.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        iIndex++; idImages[iIndex] = new ImageData();
                        idImages[iIndex].sHash = rd.GetString(0);
                        idImages[iIndex].sType = rd.GetString(1);
                        idImages[iIndex].iLength = rd.GetInt32(2);
                        idImages[iIndex].ptRes.X = rd.GetInt32(3);
                        idImages[iIndex].ptRes.Y = rd.GetInt32(4);
                        idImages[iIndex].sName = rd.GetString(5);
                        idImages[iIndex].iRating = rd.GetInt32(6);
                        idImages[iIndex].sTGeneral = rd.GetString(7);
                        idImages[iIndex].sTSource = rd.GetString(8);
                        idImages[iIndex].sTChars = rd.GetString(9);
                        idImages[iIndex].sTArtists = rd.GetString(10);
                    }
                }
            }
            SetThumbPageRanges(0);
            RedrawThumbnails(false);
            ReloadThumbnails(1, false);
        }
        private void Display_pbDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            int iIndex = DispImageIndex;
            if (e.Button == MouseButtons.Left)
                ShowPanel(pnThumbs); DispUnload();
            if (iIndex < thPages[1].idRange.X ||
                iIndex > thPages[1].idRange.Y)
            {
                int iThumbPage = 0;
                while (true)
                {
                    iThumbPage++;
                    int iThumbsEnd = (iThumbPage * pnThumb.Count) - 1;
                    int iThumbsStart = iThumbsEnd - (pnThumb.Count - 1);
                    if (iIndex >= iThumbsStart &&
                        iIndex <= iThumbsEnd)
                    {
                        SetThumbPageRanges(iThumbsStart);
                        RedrawThumbnails(false);
                        ReloadThumbnails(1, false);
                        break;
                    }
                }
            }
        }
        private void tHotkeys_Tick(object sender, EventArgs e)
        {
            if (bInHotkeys) return; bInHotkeys = true;
            bool VKCtrl = (GetAsyncKeyState(Keys.ControlKey) != 0);
            bool VKAlt = (GetAsyncKeyState(Keys.Menu) != 0);
            bool VKLeft = (GetAsyncKeyState(Keys.Left) == -32767);
            bool VKRight = (GetAsyncKeyState(Keys.Right) == -32767);
            bool VKHome = (GetAsyncKeyState(Keys.Home) == -32767);
            bool VKIns = (GetAsyncKeyState(Keys.Insert) == -32767);
            bool VKDel = (GetAsyncKeyState(Keys.Delete) == -32767);
            bool VKA = (GetAsyncKeyState(Keys.A) == -32767);
            bool VKE = (GetAsyncKeyState(Keys.E) == -32767);
            bool VKG = (GetAsyncKeyState(Keys.G) == -32767);
            if (GetForegroundWindow() == ipMyHandle)
            {
                if (VKCtrl && !VKAlt)
                {
                    if (VKLeft || VKRight)
                    {
                        if (pnDisplay.Visible)
                        {
                            int iDist = 0;
                            if (VKLeft) iDist--;
                            if (VKRight) iDist++;
                            DispSkip(iDist);
                        }
                        if (pnThumbs.Visible)
                        {
                            int iDist = 0;
                            if (VKLeft) iDist--;
                            if (VKRight) iDist++;
                            ChangeThumbPage(iDist);
                        }
                    }
                    if (VKHome)
                    {
                        if (pnGeneral.Visible && pnThumbs.Visible)
                        {
                            SetThumbPageRanges(0); ReloadThumbnails(1, false); //true);
                        }
                        else
                        {
                            if (pnDisplay.Visible)
                                Display_pbDisplay_MouseClick
                                    (new object(), new MouseEventArgs(
                                        MouseButtons.Left, 1, 1, 1, 0));
                            ShowPanel(pnGeneral); ShowPanel(pnThumbs);
                        }
                    }
                    if (VKDel)
                    {
                        if (pnDisplay.Visible)
                        {
                            if (MessageBox.Show(
                                "Do you wish to tag this image for deletion?",
                                "O SHI-", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation)
                                == DialogResult.No) return;
                            idImages[DispImageIndex].bModified = true;
                            idImages[DispImageIndex].bDeleted = true;
                            idImages[DispImageIndex].bSelected = false;
                            int iThumb = DispImageIndex - thPages[1].idRange.X;
                            DispSkip(1); bwPrevVoid = true;
                            if (iThumb < pThumb.Count && iThumb >= 0)
                                RedrawThumbnails(false);
                        }
                        if (pnThumbs.Visible)
                        {
                            if (MessageBox.Show(
                                "Do you wish to tag these images for deletion?",
                                "O SHI-", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation)
                                == DialogResult.No) return;
                            for (int a = 0; a < idImages.Length; a++)
                            {
                                if (idImages[a].bSelected)
                                {
                                    idImages[a].bModified = true;
                                    idImages[a].bDeleted = true;
                                    idImages[a].bSelected = false;
                                }
                            }
                            RedrawThumbnails(false);
                        }
                    }
                    if (VKA)
                    {
                        if (pnThumbs.Visible)
                        {
                            bool bAllSelected = true;
                            for (int a = 0; a < idImages.Length; a++)
                            {
                                if (!idImages[a].bSelected)
                                    bAllSelected = false;
                            }
                            if (bAllSelected)
                            {
                                for (int a = 0; a < idImages.Length; a++)
                                {
                                    idImages[a].bSelected = false;
                                }
                            }
                            else
                            {
                                for (int a = 0; a < idImages.Length; a++)
                                {
                                    idImages[a].bSelected = true;
                                }
                            }
                            RedrawThumbnails(false);
                        }
                    }
                    if (VKE)
                    {

                    }
                    if (VKG)
                    {
                        if (pnThumbs.Visible)
                        {
                            int iPage = Convert.ToInt32(InputBox.Show(
                                "Where do you want to go today?",
                                "Select a page", "1").Text);
                            int iIndex = (iPage - 1) * pnThumb.Count;
                            if (idImages.Length <= iIndex)
                            {
                                MessageBox.Show("You don't have that many images.",
                                    "You are doing it wrong.", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information); return;
                            }
                            SetThumbPageRanges(iIndex);
                            RedrawThumbnails(false);
                            ReloadThumbnails(1, false);
                        }
                    }
                    if (VKIns)
                    {
                        if (pnQEdit.Visible)
                        {
                            ViewImageInfo(DispImageIndex, true);
                        }
                    }
                }
            }
            bInHotkeys = false;
        }
        private void RecountStatistics()
        {
            Double dSelCount = 0; Double dSelSize = 0; string SelUnit = "B";
            Double dTotCount = 0; Double dTotSize = 0; string TotUnit = "B";
            for (int a = 0; a < idImages.Length; a++)
            {
                dTotCount++; dTotSize += (Double)idImages[a].iLength;
                if (idImages[a].bSelected)
                {
                    dSelCount++; dSelSize += (Double)idImages[a].iLength;
                }
            }
            if (dTotSize > 1024) { dTotSize = dTotSize / 1024; TotUnit = "KB"; }
            if (dTotSize > 1024) { dTotSize = dTotSize / 1024; TotUnit = "MB"; }
            if (dTotSize > 1024) { dTotSize = dTotSize / 1024; TotUnit = "GB"; }
            if (dTotSize > 1024) { dTotSize = dTotSize / 1024; TotUnit = "TB"; }
            if (dSelSize > 1024) { dSelSize = dSelSize / 1024; SelUnit = "KB"; }
            if (dSelSize > 1024) { dSelSize = dSelSize / 1024; SelUnit = "MB"; }
            if (dSelSize > 1024) { dSelSize = dSelSize / 1024; SelUnit = "GB"; }
            if (dSelSize > 1024) { dSelSize = dSelSize / 1024; SelUnit = "TB"; }

            int iSelDec = 3; int iTotDec = 3;
            if (dSelSize >= 1) iSelDec = 2; if (dTotSize >= 1) iTotDec = 2;
            if (dSelSize >= 10) iSelDec = 1; if (dTotSize >= 10) iTotDec = 1;
            if (dSelSize >= 100) iSelDec = 0; if (dTotSize >= 100) iTotDec = 0;
            dSelSize = Math.Round(dSelSize, iSelDec);
            dTotSize = Math.Round(dTotSize, iTotDec);

            double dThPage = Math.Ceiling(thPages[1].idRange.X / (double)pnThumb.Count) + 1;
            double dThPages = Math.Ceiling(dTotCount / (double)pnThumb.Count);
            dStats_SelCount = dSelCount; dStats_SelSize = dSelSize; dStats_thPage = dThPage;
            dStats_TotCount = dTotCount; dStats_TotSize = dTotSize; dStats_thPages = dThPages;

            View_Stats.Text = " " +
                "Page " + dThPage + " of " + dThPages + "   ~   " +
                "Selected: " + dSelCount + " (" + dSelSize + SelUnit + ")   ~   " +
                "Total: " + dTotCount + " (" + dTotSize + TotUnit + ") ";
        }
        private void ViewImageInfo(int iIndex, bool bQEdit)
        {
            string sInfo = " ";
            if (iIndex < idImages.Length)
            {
                if (idImages[iIndex].bSelected) sInfo += "[X]" + sGSep;
                else sInfo += "[  ]" + sGSep;
                sInfo += idImages[iIndex].iRating + sGSep;
                sInfo += idImages[iIndex].ptRes.X + "x" +
                    idImages[iIndex].ptRes.Y + sGSep;

                double dSize = (double)idImages[iIndex].iLength;
                string sSize = "B";
                if (dSize > 1024) { dSize = dSize / 1024; sSize = "KB"; }
                if (dSize > 1024) { dSize = dSize / 1024; sSize = "MB"; }
                int iSize = 3;
                if (dSize >= 1) iSize = 2;
                if (dSize >= 10) iSize = 1;
                if (dSize >= 100) iSize = 0;
                dSize = Math.Round(dSize, iSize);
                sInfo += dSize + sSize + sGSep;

                sInfo += idImages[iIndex].sName + sGSep;
                sInfo += idImages[iIndex].sHash + " . " +
                    idImages[iIndex].sType + " ";

                if (bQEdit)
                {
                    QEdit_sName = idImages[iIndex].sName;
                    QEdit_txtImagName.Text = QEdit_sName;
                    QEdit_sRating = idImages[iIndex].iRating + "";
                    QEdit_txtImagRating.Text = QEdit_sRating;
                    QEdit_sTGeneral = idImages[iIndex].sTGeneral;
                    QEdit_txtTagsGeneral.Text = QEdit_sTGeneral;
                    QEdit_sTSource = idImages[iIndex].sTSource;
                    QEdit_txtTagsSource.Text = QEdit_sTSource;
                    QEdit_sTChars = idImages[iIndex].sTChars;
                    QEdit_txtTagsChars.Text = QEdit_sTChars;
                    QEdit_sTArtists = idImages[iIndex].sTArtists;
                    QEdit_txtTagsArtists.Text = QEdit_sTArtists;
                }
            }
            Info_lblInfo.Text = sInfo;
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            dDPI_Current[0] = g.DpiX; dDPI_Current[1] = g.DpiY;
            dDPIf[0] = (dDPI_Current[0] / dDPI_Original[0]);
            dDPIf[1] = (dDPI_Current[1] / dDPI_Original[1]);
        }
        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) return;
            int ThisW = this.Width, ThisH = this.Height;
            foreach (Panel p in pnaPanelsControl)
                p.Height = ThisH - (int)Math.Round((95) * dDPIf[1]);
            foreach (Panel p in pnaPanelsMain)
                p.Size = new Size(
                    ThisW - (int)Math.Round((212 + 40) * dDPIf[0]),
                    ThisH - (int)Math.Round((132) * dDPIf[1]));
            pnInfo.Width = ThisW - (int)Math.Round((212 + 40) * dDPIf[0]);
            pnView.Width = ThisW - (int)Math.Round((212 + 40) * dDPIf[0]);
            pnView.Top = ThisH - (int)Math.Round((74) * dDPIf[1]);
            lbRes.Text = pnThumbs.Width + "x" + pnThumbs.Height + " - 16x" +
                Math.Round((16 / (double)pnThumbs.Width) * (double)pnThumbs.Height, 1);
            tResizeThumbs.Stop(); tResizeThumbs.Start();
        }
        private void frmMain_ResizeEnd(object sender, EventArgs e)
        {
            if (bRunning) RedrawThumbnails(true);
        }
        private void pnInfo_Resize(object sender, EventArgs e)
        {
            int ThisW = pnInfo.Width;
            Info_lblInfo.Width = ThisW - (int)Math.Round((8) * dDPIf[0]);
        }
        private void pnStats_Resize(object sender, EventArgs e)
        {
            int ThisW = pnView.Width;
            View_Stats.Width = ThisW - (int)Math.Round((170) * dDPIf[0]);
            View_cmdPrev.Left = ThisW - (int)Math.Round((161) * dDPIf[0]);
            View_cmdNext.Left = ThisW - (int)Math.Round((80) * dDPIf[0]);
        }
        private void pnDisplay_Resize(object sender, EventArgs e)
        {
            Display_pbDisplay.Size = new Size(
                pnDisplay.Width - (int)Math.Round((3 + 3 + 2) * dDPIf[0]),
                pnDisplay.Height - (int)Math.Round((3 + 3 + 2) * dDPIf[1]));
        }
        private void pnImport_Resize(object sender, EventArgs e)
        {
            int ThisW = pnImport.Width; int ThisH = pnImport.Height;
            Import_txtImageName.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_txtImageRating.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_txtTagsGeneral.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_txtTagsSource.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_txtTagsChars.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_txtTagsArtists.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_txtAction.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_txtCurrentProgress.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_txtTotalProgress.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_pbPreview.Top = ThisH - (int)Math.Round((233) * dDPIf[1]);
            Import_dmySeparator.Width = ThisW - (int)Math.Round((8) * dDPIf[0]);
            Import_cmdTestTags.Location = new Point(ThisW - (int)Math.Round((242) * dDPIf[0]), ThisH - (int)Math.Round((28) * dDPIf[1]));
            Import_cmdCancel.Location = new Point(ThisW - (int)Math.Round((161) * dDPIf[0]), ThisH - (int)Math.Round((28) * dDPIf[1]));
            Import_cmdStart.Location = new Point(ThisW - (int)Math.Round((80) * dDPIf[0]), ThisH - (int)Math.Round((28) * dDPIf[1]));
        }
        private void pnSearchInit_Resize(object sender, EventArgs e)
        {
            int ThisW = pnSearchInit.Width; int ThisH = pnSearchInit.Height;
            SearchInit_txtTagsGlobal.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            SearchInit_txtImageName.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            SearchInit_txtImageFormat.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            SearchInit_txtImageRes.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            SearchInit_txtImageRating.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            SearchInit_txtTagsGeneral.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            SearchInit_txtTagsSource.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            SearchInit_txtTagsChars.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            SearchInit_txtTagsArtists.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            SearchInit_txtProgress.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            SearchInit_txtResults.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            SearchInit_dmySeparator.Width = ThisW - (int)Math.Round((8) * dDPIf[0]);
            SearchInit_cmdCancel.Location = new Point(ThisW - (int)Math.Round((161) * dDPIf[0]), ThisH - (int)Math.Round((28) * dDPIf[1]));
            SearchInit_cmdStart.Location = new Point(ThisW - (int)Math.Round((80) * dDPIf[0]), ThisH - (int)Math.Round((28) * dDPIf[1]));
        }
        private void pnWPChanger_Resize(object sender, EventArgs e)
        {
            int ThisW = pnWPChanger.Width; int ThisH = pnWPChanger.Height;
            WPChanger_lstImages.Height = ThisH - (int)Math.Round((8) * dDPIf[1]);
            WPChanger_lstImages.Width = ThisW - (int)Math.Round((378) * dDPIf[0]);
            WPChanger_pbPreview.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            WPChanger_cmdAddThumbs.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            WPChanger_cmdSave.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            WPChanger_Label1.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            WPChanger_txtFrequency.Left = ThisW - (int)Math.Round((290) * dDPIf[0]);
            WPChanger_Label2.Left = ThisW - (int)Math.Round((234) * dDPIf[0]);
            WPChanger_cmdRemThumbs.Left = ThisW - (int)Math.Round((184) * dDPIf[0]);
            WPChanger_cmdRemFromList.Left = ThisW - (int)Math.Round((184) * dDPIf[0]);
            WPChanger_cmdChange.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            WPChanger_cmdLoad.Left = ThisW - (int)Math.Round((184) * dDPIf[0]);
            WPChanger_cmdSave.Left = ThisW - (int)Math.Round((91) * dDPIf[0]);
        }
        private void pnMassEdit_Resize(object sender, EventArgs e)
        {
            int ThisW = pnMassEdit.Width; int ThisH = pnMassEdit.Height;
            MassEdit_ddMainAction.Width = ThisW - (int)Math.Round((139) * dDPIf[0]);
            MassEdit_txtModifyValue.Width = ThisW - (int)Math.Round((139) * dDPIf[0]);
            MassEdit_txtInsertValue.Width = ThisW - (int)Math.Round((139) * dDPIf[0]);
            MassEdit_ddInsertTo.Width = ThisW - (int)Math.Round((139) * dDPIf[0]);
            MassEdit_ddTargetGroup.Width = ThisW - (int)Math.Round((139) * dDPIf[0]);
            MassEdit_ddCondTarget.Width = ThisW - (int)Math.Round((139) * dDPIf[0]);
            MassEdit_ddCondCType.Width = ThisW - (int)Math.Round((139) * dDPIf[0]);
            MassEdit_txtCondValue.Width = ThisW - (int)Math.Round((139) * dDPIf[0]);
            MassEdit_cmdCancel.Location = new Point(ThisW - (int)Math.Round((161) * dDPIf[0]), ThisH - (int)Math.Round((28) * dDPIf[1]));
            MassEdit_cmdStart.Location = new Point(ThisW - (int)Math.Round((80) * dDPIf[0]), ThisH - (int)Math.Round((28) * dDPIf[1]));
        }
        private void pnUpload_Resize(object sender, EventArgs e)
        {
            int ThisW = pnUpload.Width; int ThisH = pnUpload.Height;
            Upload_lstImages.Height = ThisH - (int)Math.Round((8) * dDPIf[1]);
            Upload_lstImages.Width = ThisW - (int)Math.Round((378) * dDPIf[0]);
            Upload_txtStatus.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_txtComment.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_lblURL.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_lblDelay.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_lblPass.Left = ThisW - (int)Math.Round((60) * dDPIf[0]);
            Upload_ddURL.Left = ThisW - (int)Math.Round((308) * dDPIf[0]);
            Upload_txtDelay.Left = ThisW - (int)Math.Round((308) * dDPIf[0]);
            Upload_txtPass.Left = ThisW - (int)Math.Round((184) * dDPIf[0]);
            Upload_cmdAddThumbs.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_cmdRemThumbs.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_cmdRemList.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_cmdPass.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_cmdStart.Left = ThisW - (int)Math.Round((184) * dDPIf[0]);
            Upload_cmdPause.Left = ThisW - (int)Math.Round((184) * dDPIf[0]);
            Upload_cmdStop.Left = ThisW - (int)Math.Round((184) * dDPIf[0]);
        }
        private void tResizeThumbs_Tick(object sender, EventArgs e)
        {
            tResizeThumbs.Stop();
            if (bRunning)
            {
                RedrawThumbnails(true);
                if (bInitialRun)
                {
                    bInitialRun = false;
                    wfSplash.Show();
                }
            }
        }

        private void frmMain_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            if (!DB.IsOpen)
            {
                MessageBox.Show("Please select or create a database first.",
                    "You are doing it wrong.", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation); return;
            }
            ShowPanel(pnImport);
            Import_txtAction.Text = "Scanning directories";
            Import_txtCurrentProgress.Text = "n/a";
            Import_txtTotalProgress.Text = "1/2";
            Application.DoEvents();

            string[] saPaths = new string[0];
            string[] saDrops = (string[])e.Data.GetData(DataFormats.FileDrop);
            sFirstImportRoot = saDrops[0];
            for (int a = 0; a < saDrops.Length; a++)
            {
                string[] saAppend = GetPaths(saDrops[a], true);
                string[] saOldRet = saPaths;
                saPaths = new string[saOldRet.Length + saAppend.Length];
                saOldRet.CopyTo(saPaths, 0);
                saAppend.CopyTo(saPaths, saOldRet.Length);
            }

            Import_txtAction.Text = "Filtering results";
            Import_txtTotalProgress.Text = "2/2";
            Application.DoEvents();

            saFullPaths = FilterArray(saPaths, saAllowedTypes,
                asIntArray(2, saAllowedTypes.Length));

            Import_txtAction.Text = "Identified " + saFullPaths.Length + " images.";
            Import_txtCurrentProgress.Text = ""; Import_txtTotalProgress.Text = "";
            Application.DoEvents();

            Import_cmdTestTags.Enabled = true;
            Import_cmdCancel.Enabled = true;
            Import_cmdStart.Enabled = true;
        }
        private int[] asIntArray(int iVal, int iLen)
        {
            int[] iaRet = new int[iLen];
            for (int a = 0; a < iaRet.Length; a++)
            {
                iaRet[a] = iVal;
            }
            return iaRet;
        }
        private string[] FilterArray(string[] saVal, string[] saPrm, int[] iaPrL)
        {
            int iCnt = 0; bool[] baInclude = new bool[saVal.Length];
            for (int a = 0; a < saVal.Length; a++)
            {
                for (int b = 0; b < saPrm.Length; b++)
                {
                    if (!baInclude[a])
                    {
                        if (iaPrL[b] == 1) if (saVal[a].StartsWith(saPrm[b])) baInclude[a] = true;
                        if (iaPrL[b] == 2) if (saVal[a].EndsWith(saPrm[b])) baInclude[a] = true;
                        if (iaPrL[b] == 3) if (saVal[a].Contains(saPrm[b])) baInclude[a] = true;
                        if (baInclude[a]) iCnt++;
                    }
                }
            }
            int iLoc = -1; string[] saRet = new string[iCnt];
            for (int a = 0; a < saVal.Length; a++)
            {
                if (baInclude[a])
                {
                    iLoc++;
                    saRet[iLoc] = saVal[a];
                }
            }
            return saRet;
        }
        private string[] GetPaths(string sRoot, bool bRecursive)
        {
            Import_txtCurrentProgress.Text = sRoot; Application.DoEvents();
            sRoot = sRoot.Replace("\\", "/");
            if (!sRoot.EndsWith("/")) sRoot += "/";
            string[] ret = new string[0];
            string[] saFolders, saFiles;
            try
            {
                saFolders = System.IO.Directory.GetDirectories(sRoot);
                saFiles = System.IO.Directory.GetFiles(sRoot);
            }
            catch
            {
                saFiles = new string[] { "#ERROR#" };
                saFolders = new string[] { };
            }
            ret = saFiles;
            //for (int a = 0; a < ret.Length; a++)
            //{
            //    ret[a] = ret[a].Replace("\\", "/");
            //}
            for (int a = 0; a < saFolders.Length; a++)
            {
                if (bRecursive)
                {
                    string[] saAppend = GetPaths(saFolders[a], bRecursive);
                    string[] saOldRet = ret;
                    ret = new string[saOldRet.Length + saAppend.Length];
                    saOldRet.CopyTo(ret, 0);
                    saAppend.CopyTo(ret, saOldRet.Length);
                }
            }
            return ret;
        }
        private void CSet_ddControlSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!pnaPanelsControl[CSet_ddControlSet.SelectedIndex].Visible)
                ShowPanel(pnaPanelsControl[CSet_ddControlSet.SelectedIndex]);
        }
        private void CSet_ddMainSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!pnaPanelsMain[CSet_ddMainSet.SelectedIndex].Visible)
                ShowPanel(pnaPanelsMain[CSet_ddMainSet.SelectedIndex]);
        }

        private void Import_cmdTestTags_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = sFirstImportRoot; fd.ShowDialog();
            string sFName = fd.FileName;
            if (sFName != "")
            {
                ImageData tID = GenerateImageData(sFName.Replace("\\", "/"),
                    Import_txtImageName.Text, Import_txtImageRating.Text,
                    Import_txtTagsGeneral.Text, Import_txtTagsSource.Text,
                    Import_txtTagsChars.Text, Import_txtTagsArtists.Text);
                MessageBox.Show(".:: Resulting properties for the selected file ::." + "\r\n" +
                    "(" + sFName + ")" + "\r\n" +
                    "\r\n" +
                    //"bSelected: " + tID.bSelected + "\r\n" +
                    "MD5 checksum  ...  " + tID.sHash + "\r\n" +
                    "File size (B)  ...  " + tID.iLength + "\r\n" +
                    "Image name  ...  " + tID.sName + "\r\n" +
                    "Image type  ...  " + tID.sType + "\r\n" +
                    "Image rating  ...  " + tID.iRating + "\r\n" +
                    "Resolution  ...  " + tID.ptRes + "\r\n" +
                    "Tags General  ...  " + tID.sTGeneral + "\r\n" +
                    "Tags Soure  ...  " + tID.sTSource + "\r\n" +
                    "Tags Chars  ...  " + tID.sTChars + "\r\n" +
                    "Tags Artists  ...  " + tID.sTArtists);
            }
        }
        private void Import_cmdCancel_Click(object sender, EventArgs e)
        {
            ShowPanel(pnThumbs);
        }
        private void Import_cmdStart_Click(object sender, EventArgs e)
        {
            if (!DB.IsOpen)
            {
                MessageBox.Show("Please select or create a database first.",
                    "You are doing it wrong.", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation); return;
            }
            string sImageName = Import_txtImageName.Text;
            string sImageRating = Import_txtImageRating.Text;
            string sTagsGeneral = Import_txtTagsGeneral.Text;
            string sTagsSource = Import_txtTagsSource.Text;
            string sTagsChars = Import_txtTagsChars.Text;
            string sTagsArtists = Import_txtTagsArtists.Text;
            using (SQLiteTransaction dbTrs = DB.Data.BeginTransaction())
            {
                using (SQLiteCommand DBc = DB.Data.CreateCommand())
                {
                    DBc.CommandText = "INSERT INTO 'images' (" +
                        "hash, type, size, xres, yres, name, rating, " +
                        "t_general, t_source, t_chars, t_artists) " +
                        "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    SQLiteParameter[] dbParam = new SQLiteParameter[11];
                    for (int a = 0; a < dbParam.Length; a++)
                    {
                        dbParam[a] = DBc.CreateParameter();
                        DBc.Parameters.Add(dbParam[a]);
                    }
                    for (int a = 0; a < saFullPaths.Length; a++)
                    {
                        saFullPaths[a] = saFullPaths[a].Replace("\\", "/");
                        Import_txtAction.Text = "Importing " + saFullPaths[a];
                        Import_txtCurrentProgress.Text = (a + 1) + " of " + saFullPaths.Length;
                        ImageData tID = GenerateImageData(saFullPaths[a],
                            sImageName, sImageRating, sTagsGeneral,
                            sTagsSource, sTagsChars, sTagsArtists);
                        if (tID.sType != "nil")
                        {
                            if (!System.IO.File.Exists(sAppPath +
                                DB.Path + tID.sHash + "." + tID.sType))
                            {
                                System.IO.File.Copy(saFullPaths[a], sAppPath +
                                    DB.Path + tID.sHash + "." + tID.sType);
                                dbParam[0].Value = tID.sHash;
                                dbParam[1].Value = tID.sType;
                                dbParam[2].Value = tID.iLength;
                                dbParam[3].Value = tID.ptRes.X;
                                dbParam[4].Value = tID.ptRes.Y;
                                dbParam[5].Value = tID.sName;
                                dbParam[6].Value = tID.iRating;
                                dbParam[7].Value = tID.sTGeneral;
                                dbParam[8].Value = tID.sTSource;
                                dbParam[9].Value = tID.sTChars;
                                dbParam[10].Value = tID.sTArtists;
                                DBc.ExecuteNonQuery();
                            }
                            else
                            {
                                //Shitty temporary solution.
                                //TODO: Needs moar appending options
                            }
                        }
                    }
                    frmWait.sMain = "Writing to database";
                    frmWait.bInstant = true; frmWait.bVisible = false;
                }
                dbTrs.Commit();
            }
            Import_txtAction.Text = "";
            Import_txtCurrentProgress.Text = "";
        }
        private ImageData GenerateImageData(string sPath, string sName, string sRating,
            string sTGeneral, string sTSource, string sTChars, string sTArtists)
        {
            sRating = sRating.Trim(' ');
            string[] saName = sName.Split(','); sName = "";
            for (int i = 0; i < saName.Length; i++)
            {
                saName[i] = saName[i].Trim(' ');
                sName += saName[i] + ",";
            }
            sName = sName.Trim(',');
            string[] saTGeneral = sTGeneral.Split(','); sTGeneral = "";
            for (int i = 0; i < saTGeneral.Length; i++)
            {
                saTGeneral[i] = saTGeneral[i].Trim(' ');
                sTGeneral += saTGeneral[i] + ",";
            }
            sTGeneral = sTGeneral.Trim(',');
            string[] saTSource = sTSource.Split(','); sTSource = "";
            for (int i = 0; i < saTSource.Length; i++)
            {
                saTSource[i] = saTSource[i].Trim(' ');
                sTSource += saTSource[i] + ",";
            }
            sTSource = sTSource.Trim(',');
            string[] saTChars = sTChars.Split(','); sTChars = "";
            for (int i = 0; i < saTChars.Length; i++)
            {
                saTChars[i] = saTChars[i].Trim(' ');
                sTChars += saTChars[i] + ",";
            }
            sTChars = sTChars.Trim(',');
            string[] saTArtists = sTArtists.Split(','); sTArtists = "";
            for (int i = 0; i < saTArtists.Length; i++)
            {
                saTArtists[i] = saTArtists[i].Trim(' ');
                sTArtists += saTArtists[i] + ",";
            }
            sTArtists = sTArtists.Trim(',');

            if (Import_pbPreview.Image != null) Import_pbPreview.Image.Dispose();
            ImageData ret = new ImageData();
            string[] saE = sPath.Split('/');
            string sFName = "";
            sFName = saE[saE.Length - 1];
            sFName = sFName.Substring(0, sFName.LastIndexOf("."));

            ret.bSelected = false;
            ret.sType = saE[saE.Length - 1].Substring(sFName.Length + 1);
            ret.sHash = MD5File(sPath);
            System.IO.FileStream fs = new System.IO.FileStream(sPath, System.IO.FileMode.Open);
            ret.iLength = (int)fs.Length; fs.Close(); fs.Dispose();
            //Image img = Bitmap.FromFile(sPath);
            //ret.ptRes = (Point)img.Size;
            try
            {
                Import_pbPreview.Image = Bitmap.FromFile(sPath); Application.DoEvents();
                ret.ptRes = (Point)Import_pbPreview.Image.Size;
            }
            catch
            {
                ret.sType = "nil";
                ret.ptRes = new Point(0, 0); //(Point)Import_pbPreview.Image.Size;
            }
            for (int a = 1; a < saE.Length; a++)
            {
                sName = sName.Replace("{" + a + "}", saE[a]);
                sRating = sRating.Replace("{" + a + "}", saE[a]);
                sTGeneral = sTGeneral.Replace("{" + a + "}", saE[a]);
                sTSource = sTSource.Replace("{" + a + "}", saE[a]);
                sTArtists = sTArtists.Replace("{" + a + "}", saE[a]);
                sTChars = sTChars.Replace("{" + a + "}", saE[a]);
            }
            sName = sName.Replace("{fname}", sFName);
            sRating = sRating.Replace("{fname}", sFName);
            sTGeneral = sTGeneral.Replace("{fname}", sFName);
            sTSource = sTSource.Replace("{fname}", sFName);
            sTArtists = sTArtists.Replace("{fname}", sFName);
            sTChars = sTChars.Replace("{fname}", sFName);

            int iRating = -1;
            if (sRating != "" && OnlyContains(sRating, "0123456789"))
                iRating = Convert.ToInt32(sRating);
            ret.sName = sName;
            ret.iRating = iRating;
            ret.sTGeneral = sTGeneral;
            ret.sTSource = sTSource;
            ret.sTChars = sTChars;
            ret.sTArtists = sTArtists;
            return ret;
        }

        private void General_cmdDBCreate_Click(object sender, EventArgs e)
        {
            if (GUI_CheckForChanges()) return;
            bool bDBCreated = false;
            string sDBName = InputBox.Show(
                "Please enter a name for the new database.",
                "Creating a new database", "pImgDB").Text;

            if (!OnlyContains(sDBName,
                "abcdefghijklmnopqrstuvwxyz" +
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "0123456789"))
            {
                MessageBox.Show(
                    "That name is invalid." + "\r\n\r\n" +
                    "Please choose a name consisting only of a-z, A-Z and 0-9.",
                    "You are doing it wrong.", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation); return;
            }

            if (!DB.Create(sDBName, false))
            {
                if (MessageBox.Show(
                    "Database already exists." + "\r\n" +
                    "If you proceed, the database will be deleted." + "\r\n" +
                    "THIS INCLUDES ALL ITS IMAGES." + "\r\n" +
                    "\r\n" + "Do you want to continue?", "O SHI-",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                    if (DB.Create(sDBName, true)) bDBCreated = true;
            }
            else bDBCreated = true;

            if (bDBCreated)
            {
                MessageBox.Show(
                    "Your database was created!" + "\r\n\r\n" +
                    "Please restart pImgDB.", "You are doing it right.",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Dispose();
            }
            else
            {
                MessageBox.Show(
                    "Your database could not be created!" + "\r\n\r\n" +
                    "Please make sure that you are using an extracted copy of pImgDB.",
                    "You are doing it wrong.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void General_cmdDBStatistics_Click(object sender, EventArgs e)
        {
            if (!DB.IsOpen)
            {
                MessageBox.Show("Please select or create a database first.",
                    "You are doing it wrong.", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation); return;
            }
            ShowPanel(pnDBStats);
            DBStats_lblDBName.Text = DB.Name + "";
            DBStats_lblDBImages.Text = DB.Entries() + "";
            if (DB.CanClose) DB.Close();
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(
                    DB.Path + "_pImgDB.db", System.IO.FileMode.Open);
                DBStats_lblDBSize.Text = Math.Round((double)fs.Length / 1024, 1) + " kB";
                fs.Close(); fs.Dispose();
            }
            catch { }
            if (!DB.IsOpen) DB.Open();
        }
        private void General_cbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (General_cbDatabase.SelectedIndex == -1) return;
            string tsDBName = General_cbDatabase.Items[General_cbDatabase.SelectedIndex].ToString();
            tsDBName = tsDBName.Substring(tsDBName.IndexOf(" - ") + 3);
            DB.Open(tsDBName);
        }

        private void General_cmdSearch_Click(object sender, EventArgs e)
        {
            if (!DB.IsOpen)
            {
                MessageBox.Show("Please select or create a database first.",
                    "You are doing it wrong.", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation); return;
            }
            ShowPanel(pnSearchInit);
        }
        private void SearchInit_cmdCancel_Click(object sender, EventArgs e)
        {
            ShowPanel(pnThumbs);
        }
        private void SearchInit_cmdStart_Click(object sender, EventArgs e)
        {
            if (GUI_CheckForChanges()) return;
            SearchInit_txtProgress.Text = "Preparing search parameters";
            SearchInit_txtResults.Text = ""; Application.DoEvents();
            SearchPrm sp = new SearchPrm();
            sp.sTagsGlobal = SearchInit_txtTagsGlobal.Text;
            sp.sImagName = SearchInit_txtImageName.Text;
            sp.sImagFormat = SearchInit_txtImageFormat.Text;
            sp.sImagRes = SearchInit_txtImageRes.Text;
            sp.sImagRating = SearchInit_txtImageRating.Text;
            sp.sTagsGeneral = SearchInit_txtTagsGeneral.Text;
            sp.sTagsSource = SearchInit_txtTagsSource.Text;
            sp.sTagsChars = SearchInit_txtTagsChars.Text;
            sp.sTagsArtists = SearchInit_txtTagsArtists.Text;
            Search(sp);

            SetThumbPageRanges(0);
            ShowPanel(pnThumbs); Application.DoEvents();
            RedrawThumbnails(true); ReloadThumbnails(1, false);
        }
        private void Search(SearchPrm sp)
        {
            LastSearch = sp;
            if (sp.sImagRes != "")
            if (!sp.sImagRes.StartsWith("<") &&
                !sp.sImagRes.StartsWith(">"))
            {
                sp.sImagRes = sp.sImagRes.Replace(" ", "").
                    Replace("<", "").Replace(">", "");
                string[] sImagResAry = sp.sImagRes.Split(',');
                sp.sImagRes = "";
                for (int a = 0; a < sImagResAry.Length; a++)
                {
                    sp.sImagRes +=
                        "<" + sImagResAry[a] + "," +
                        ">" + sImagResAry[a] + ",";
                }
                sp.sImagRes = sp.sImagRes.Substring(
                    0, sp.sImagRes.Length - 1);
            }

            string[] tagsGlobal = sp.sTagsGlobal.Split(',');
            string[] imagName = sp.sImagName.Split(',');
            string[] imagFormat = sp.sImagFormat.Split(',');
            string[] imagRes = sp.sImagRes.Split(',');
            string[] imagRating = sp.sImagRating.Split(',');
            string[] tagsGeneral = sp.sTagsGeneral.Split(',');
            string[] tagsSource = sp.sTagsSource.Split(',');
            string[] tagsChars = sp.sTagsChars.Split(',');
            string[] tagsArtists = sp.sTagsArtists.Split(',');
            for (int a = 0; a < 1024; a++)
            {
                if (a < tagsGlobal.Length) tagsGlobal[a] = tagsGlobal[a].Trim(' ');
                if (a < imagName.Length) imagName[a] = imagName[a].Trim(' ');
                if (a < imagFormat.Length) imagFormat[a] = imagFormat[a].Trim(' ');
                if (a < imagRes.Length) imagRes[a] = imagRes[a].Trim(' ');
                if (a < imagRating.Length) imagRating[a] = imagRating[a].Trim(' ');
                if (a < tagsGeneral.Length) tagsGeneral[a] = tagsGeneral[a].Trim(' ');
                if (a < tagsSource.Length) tagsSource[a] = tagsSource[a].Trim(' ');
                if (a < tagsChars.Length) tagsChars[a] = tagsChars[a].Trim(' ');
                if (a < tagsArtists.Length) tagsArtists[a] = tagsArtists[a].Trim(' ');
            }

            // Tags - Global
            int[] tagsGlobalP = new int[tagsGlobal.Length];
            string[] tagsGlobalV = new string[tagsGlobal.Length];
            if (tagsGlobal[0] != "")
                for (int a = 0; a < tagsGlobal.Length; a++)
                {
                    if (!tagsGlobal[a].StartsWith("+") &&
                        !tagsGlobal[a].StartsWith("-") &&
                        tagsGlobal[a] != "")
                        tagsGlobal[a] = "+" + tagsGlobal[a];
                    if (tagsGlobal[a].StartsWith("+")) tagsGlobalP[a] = 1;
                    if (tagsGlobal[a].StartsWith("-")) tagsGlobalP[a] = 2;
                    tagsGlobalV[a] = tagsGlobal[a].Substring(1);
                }

            // Image Name
            int[] imagNameP = new int[imagName.Length];
            string[] imagNameV = new string[imagName.Length];
            if (imagName[0] != "")
                for (int a = 0; a < imagName.Length; a++)
                {
                    if (!imagName[a].StartsWith("+") &&
                        !imagName[a].StartsWith("-") &&
                        imagName[a] != "")
                        imagName[a] = "+" + imagName[a];
                    if (imagName[a].StartsWith("+")) imagNameP[a] = 1;
                    if (imagName[a].StartsWith("-")) imagNameP[a] = 2;
                    imagNameV[a] = imagName[a].Substring(1);
                }

            // Image Format
            int[] imagFormatP = new int[imagFormat.Length];
            string[] imagFormatV = new string[imagFormat.Length];
            if (imagFormat[0] != "")
                for (int a = 0; a < imagFormat.Length; a++)
                {
                    if (!imagFormat[a].StartsWith("+") &&
                        !imagFormat[a].StartsWith("-") &&
                        imagFormat[a] != "")
                        imagFormat[a] = "+" + imagFormat[a];
                    if (imagFormat[a].StartsWith("+")) imagFormatP[a] = 1;
                    if (imagFormat[a].StartsWith("-")) imagFormatP[a] = 2;
                    imagFormatV[a] = imagFormat[a].Substring(1);
                }

            // Image Resolution
            int[] imagResP = new int[imagRes.Length];
            Point[] imagResV = new Point[imagRes.Length];
            if (imagRes[0] != "")
                for (int a = 0; a < imagRes.Length; a++)
                {
                    if (imagRes[a].StartsWith("<")) imagResP[a] = 1;
                    if (imagRes[a].StartsWith(">")) imagResP[a] = 2;
                    string[] imagResT = imagRes[a].Substring(1).Split('x');
                    imagResV[a] = new Point(
                        Convert.ToInt32(imagResT[0]),
                        Convert.ToInt32(imagResT[1]));
                }

            // Image Rating
            int[] imagRatingP = new int[imagRating.Length];
            int[] imagRatingV = new int[imagRating.Length];
            if (imagRating[0] != "")
                for (int a = 0; a < imagRating.Length; a++)
                {
                    if (imagRating[a].StartsWith("<")) imagRatingP[a] = 1;
                    if (imagRating[a].StartsWith(">")) imagRatingP[a] = 2;
                    imagRatingV[a] = Convert.ToInt32(imagRating[a].Substring(1));
                }

            // Tags - General
            int[] tagsGeneralP = new int[tagsGeneral.Length];
            string[] tagsGeneralV = new string[tagsGeneral.Length];
            if (tagsGeneral[0] != "")
                for (int a = 0; a < tagsGeneral.Length; a++)
                {
                    if (!tagsGeneral[a].StartsWith("+") &&
                        !tagsGeneral[a].StartsWith("-") &&
                        tagsGeneral[a] != "")
                        tagsGeneral[a] = "+" + tagsGeneral[a];
                    if (tagsGeneral[a].StartsWith("+")) tagsGeneralP[a] = 1;
                    if (tagsGeneral[a].StartsWith("-")) tagsGeneralP[a] = 2;
                    tagsGeneralV[a] = tagsGeneral[a].Substring(1);
                }

            // Tags - Source
            int[] tagsSourceP = new int[tagsSource.Length];
            string[] tagsSourceV = new string[tagsSource.Length];
            if (tagsSource[0] != "")
                for (int a = 0; a < tagsSource.Length; a++)
                {
                    if (!tagsSource[a].StartsWith("+") &&
                        !tagsSource[a].StartsWith("-") &&
                        tagsSource[a] != "")
                        tagsSource[a] = "+" + tagsSource[a];
                    if (tagsSource[a].StartsWith("+")) tagsSourceP[a] = 1;
                    if (tagsSource[a].StartsWith("-")) tagsSourceP[a] = 2;
                    tagsSourceV[a] = tagsSource[a].Substring(1);
                }

            // Tags - Chars
            int[] tagsCharsP = new int[tagsChars.Length];
            string[] tagsCharsV = new string[tagsChars.Length];
            if (tagsChars[0] != "")
                for (int a = 0; a < tagsChars.Length; a++)
                {
                    if (!tagsChars[a].StartsWith("+") &&
                        !tagsChars[a].StartsWith("-") &&
                        tagsChars[a] != "")
                        tagsChars[a] = "+" + tagsChars[a];
                    if (tagsChars[a].StartsWith("+")) tagsCharsP[a] = 1;
                    if (tagsChars[a].StartsWith("-")) tagsCharsP[a] = 2;
                    tagsCharsV[a] = tagsChars[a].Substring(1);
                }

            // Tags - Artists
            int[] tagsArtistsP = new int[tagsArtists.Length];
            string[] tagsArtistsV = new string[tagsArtists.Length];
            if (tagsArtists[0] != "")
                for (int a = 0; a < tagsArtists.Length; a++)
                {
                    if (!tagsArtists[a].StartsWith("+") &&
                        !tagsArtists[a].StartsWith("-") &&
                        tagsArtists[a] != "")
                        tagsArtists[a] = "+" + tagsArtists[a];
                    if (tagsArtists[a].StartsWith("+")) tagsArtistsP[a] = 1;
                    if (tagsArtists[a].StartsWith("-")) tagsArtistsP[a] = 2;
                    tagsArtistsV[a] = tagsArtists[a].Substring(1);
                }

            SearchInit_txtProgress.Text = "Search started";
            SearchInit_txtResults.Text = ""; Application.DoEvents();

            int iCnt = DB.Entries(); int i = -1;
            int[] iRet = new int[iCnt];
            using (SQLiteCommand DBc = DB.Data.CreateCommand())
            {
                DBc.CommandText = "SELECT * FROM 'images'";
                using (SQLiteDataReader rd = DBc.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        i++;
                        string sHash = rd.GetString(0);
                        string sType = rd.GetString(1);
                        int iLength = rd.GetInt32(2);
                        int iResX = rd.GetInt32(3);
                        int iResY = rd.GetInt32(4);
                        string sName = rd.GetString(5);
                        int iRating = rd.GetInt32(6);
                        string[] sTGeneral = rd.GetString(7).Split(',');
                        string[] sTSource = rd.GetString(8).Split(',');
                        string[] sTChars = rd.GetString(9).Split(',');
                        string[] sTArtists = rd.GetString(10).Split(',');

                        if (i.ToString().EndsWith("00"))
                        {
                            SearchInit_txtProgress.Text = "Searching (" + i + " / " + iCnt + ")";
                            SearchInit_txtResults.Text = "n/a"; Application.DoEvents();
                        }

                        // Image Name
                        if (iRet[i] != 2)
                            if (imagName[0] != "")
                                for (int a = 0; a < imagName.Length; a++)
                                    if (iRet[i] != 2)
                                        if (sName.Contains(imagNameV[a]))
                                        {
                                            //if (imagNameP[a] == 1) iRet[i] = 1;
                                            //if (imagNameP[a] == 2) iRet[i] = 2;
                                            iRet[i] = imagNameP[a];
                                        }

                        // Image Format
                        if (iRet[i] != 2)
                            if (imagFormat[0] != "")
                                for (int a = 0; a < imagFormat.Length; a++)
                                    if (iRet[i] != 2)
                                        if (sType == imagFormatV[a])
                                        {
                                            //if (imagFormatP[a] == 1) iRet[i] = 1;
                                            //if (imagFormatP[a] == 2) iRet[i] = 2;
                                            iRet[i] = imagFormatP[a];
                                        }

                        // Image Resolution
                        if (iRet[i] != 2)
                            if (imagRes[0] != "")
                                for (int a = 0; a < imagRes.Length; a++)
                                    if (iRet[i] != 2)
                                    {
                                        if (imagResP[a] == 1)
                                        {
                                            if (iResX > imagResV[a].X) iRet[i] = 2;
                                            if (iResY > imagResV[a].Y) iRet[i] = 2;
                                        }
                                        if (imagResP[a] == 2)
                                        {
                                            if (iResX < imagResV[a].X) iRet[i] = 2;
                                            if (iResY < imagResV[a].Y) iRet[i] = 2;
                                        }
                                    }

                        // Image Rating
                        if (iRet[i] != 2)
                            if (imagRating[0] != "")
                                for (int a = 0; a < imagRating.Length; a++)
                                    if (iRet[i] != 2)
                                    {
                                        if (imagRatingP[a] == 1)
                                        {
                                            if (iRating > imagRatingV[a]) iRet[i] = 2;
                                        }
                                        if (imagRatingP[a] == 2)
                                        {
                                            if (iRating < imagRatingV[a]) iRet[i] = 2;
                                        }
                                    }

                        // Tags - General
                        if (iRet[i] != 2)
                            if (tagsGeneral[0] != "")
                                for (int a = 0; a < tagsGeneral.Length; a++)
                                    if (iRet[i] != 2)
                                        for (int b = 0; b < sTGeneral.Length; b++)
                                            if (iRet[i] != 2)
                                                if (sTGeneral[b] == tagsGeneralV[a])
                                                {
                                                    //if (tagsGeneralP[a] == 1) iRet[i] = 1;
                                                    //if (tagsGeneralP[a] == 2) iRet[i] = 2;
                                                    iRet[i] = tagsGeneralP[a];
                                                }

                        // Tags - Source
                        if (iRet[i] != 2)
                            if (tagsSource[0] != "")
                                for (int a = 0; a < tagsSource.Length; a++)
                                    if (iRet[i] != 2)
                                        for (int b = 0; b < sTSource.Length; b++)
                                            if (iRet[i] != 2)
                                                if (sTSource[b] == tagsSourceV[a])
                                                {
                                                    //if (tagsSourceP[a] == 1) iRet[i] = 1;
                                                    //if (tagsSourceP[a] == 2) iRet[i] = 2;
                                                    iRet[i] = tagsSourceP[a];
                                                }

                        // Tags - Chars
                        if (iRet[i] != 2)
                            if (tagsChars[0] != "")
                                for (int a = 0; a < tagsChars.Length; a++)
                                    if (iRet[i] != 2)
                                        for (int b = 0; b < sTChars.Length; b++)
                                            if (iRet[i] != 2)
                                                if (sTChars[b] == tagsCharsV[a])
                                                {
                                                    //if (tagsCharsP[a] == 1) iRet[i] = 1;
                                                    //if (tagsCharsP[a] == 2) iRet[i] = 2;
                                                    iRet[i] = tagsCharsP[a];
                                                }

                        // Tags - Artists
                        if (iRet[i] != 2)
                            if (tagsArtists[0] != "")
                                for (int a = 0; a < tagsArtists.Length; a++)
                                    if (iRet[i] != 2)
                                        for (int b = 0; b < sTArtists.Length; b++)
                                            if (iRet[i] != 2)
                                                if (sTArtists[b] == tagsArtistsV[a])
                                                {
                                                    //if (tagsArtistsP[a] == 1) iRet[i] = 1;
                                                    //if (tagsArtistsP[a] == 2) iRet[i] = 2;
                                                    iRet[i] = tagsArtistsP[a];
                                                }

                        // Tags - Global
                        if (iRet[i] != 2)
                            if (tagsGlobal[0] != "")
                                for (int a = 0; a < tagsGlobal.Length; a++)
                                {
                                    if (iRet[i] != 2)
                                        if (sName.Contains(tagsGlobalV[a]))
                                        {
                                            //if (tagsGlobalP[a] == 1) iRet[i] = 1;
                                            //if (tagsGlobalP[a] == 2) iRet[i] = 2;
                                            iRet[i] = imagNameP[a];
                                        }
                                    if (iRet[i] != 2)
                                        for (int b = 0; b < sTGeneral.Length; b++)
                                            if (iRet[i] != 2)
                                                if (sTGeneral[b] == tagsGlobalV[a])
                                                {
                                                    //if (tagsGlobalP[a] == 1) iRet[i] = 1;
                                                    //if (tagsGlobalP[a] == 2) iRet[i] = 2;
                                                    iRet[i] = tagsGeneralP[a];
                                                }
                                    if (iRet[i] != 2)
                                        for (int b = 0; b < sTSource.Length; b++)
                                            if (iRet[i] != 2)
                                                if (sTSource[b] == tagsGlobalV[a])
                                                {
                                                    //if (tagsGlobalP[a] == 1) iRet[i] = 1;
                                                    //if (tagsGlobalP[a] == 2) iRet[i] = 2;
                                                    iRet[i] = tagsSourceP[a];
                                                }
                                    if (iRet[i] != 2)
                                        for (int b = 0; b < sTChars.Length; b++)
                                            if (iRet[i] != 2)
                                                if (sTChars[b] == tagsGlobalV[a])
                                                {
                                                    //if (tagsGlobalP[a] == 1) iRet[i] = 1;
                                                    //if (tagsGlobalP[a] == 2) iRet[i] = 2;
                                                    iRet[i] = tagsCharsP[a];
                                                }
                                    if (iRet[i] != 2)
                                        for (int b = 0; b < sTArtists.Length; b++)
                                            if (iRet[i] != 2)
                                                if (sTArtists[b] == tagsGlobalV[a])
                                                {
                                                    //if (tagsGlobalP[a] == 1) iRet[i] = 1;
                                                    //if (tagsGlobalP[a] == 2) iRet[i] = 2;
                                                    iRet[i] = tagsArtistsP[a];
                                                }
                                }
                    }
                }
            }

            SearchInit_txtProgress.Text = "Gathering results";
            SearchInit_txtResults.Text = "n/a"; Application.DoEvents();

            i = -1; iCnt = 0;
            for (int a = 0; a < iRet.Length; a++)
            {
                if (iRet[a] == 1) iCnt++;
            }
            idImages = new ImageData[iCnt];
            SearchInit_txtResults.Text = iCnt + "";
            Application.DoEvents();

            i = -1; iCnt = -1;
            using (SQLiteCommand DBc = DB.Data.CreateCommand())
            {
                DBc.CommandText = "SELECT * FROM 'images'";
                using (SQLiteDataReader rd = DBc.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        iCnt++;
                        if (iRet[iCnt] == 1)
                        {
                            i++; idImages[i] = new ImageData();
                            idImages[i].sHash = rd.GetString(0);
                            idImages[i].sType = rd.GetString(1);
                            idImages[i].iLength = rd.GetInt32(2);
                            idImages[i].ptRes.X = rd.GetInt32(3);
                            idImages[i].ptRes.Y = rd.GetInt32(4);
                            idImages[i].sName = rd.GetString(5);
                            idImages[i].iRating = rd.GetInt32(6);
                            idImages[i].sTGeneral = rd.GetString(7);
                            idImages[i].sTSource = rd.GetString(8);
                            idImages[i].sTChars = rd.GetString(9);
                            idImages[i].sTArtists = rd.GetString(10);
                        }
                    }
                }
            }
        }
        private void SetThumbPageRanges(int iMainPageStart)
        {
            int[] iStart = new int[3];
            int[] iEnd = new int[3];
            if (idImages.Length == 0)
            {
                iStart[0] = -1; iEnd[0] = -1;
                iStart[1] = -1; iEnd[1] = -1;
                iStart[2] = -1; iEnd[2] = -1;
            }
            else
            {
                iStart[1] = iMainPageStart;
                iStart[0] = iStart[1] - pThumb.Count;
                iStart[2] = iStart[1] + pThumb.Count;
                if (iStart[0] < 0) iStart[0] = 0;
                if (iStart[1] < 0) iStart[1] = 0;
                if (iStart[2] < 0) iStart[2] = 0;
                if (iStart[0] > idImages.Length) iStart[0] = -1;
                if (iStart[1] > idImages.Length) iStart[1] = -1;
                if (iStart[2] > idImages.Length) iStart[2] = -1;
                iEnd[0] = iStart[0] + pThumb.Count - 1;
                iEnd[1] = iStart[1] + pThumb.Count - 1;
                iEnd[2] = iStart[2] + pThumb.Count - 1;
                if (iEnd[0] >= idImages.Length) iEnd[0] = idImages.Length - 1;
                if (iEnd[1] >= idImages.Length) iEnd[1] = idImages.Length - 1;
                if (iEnd[2] >= idImages.Length) iEnd[2] = idImages.Length - 1;
                if (iStart[0] == -1) iEnd[0] = -1;
                if (iStart[1] == -1) iEnd[1] = -1;
                if (iStart[2] == -1) iEnd[2] = -1;
            }
            thPages[0].idRange = new Point(iStart[0], iEnd[0]);
            thPages[1].idRange = new Point(iStart[1], iEnd[1]);
            thPages[2].idRange = new Point(iStart[2], iEnd[2]);
        }
        private void ChangeThumbPage(int iSteps)
        {
            /*if ((int)Math.Abs((double)iSteps) <= 1)
            {
                if (iSteps == -1)
                {
                    int iIndex = DispImageIndex + iSteps;
                    if (iIndex < 0 || iIndex >= idImages.Length) return;
                    if ((int)Math.Abs(iSteps) > 1) { DispLoad(iIndex); return; }
                    if (iSteps == -1)
                    {
                        while (bwPrevDisp.IsBusy)
                        {
                            System.Threading.Thread.Sleep(1);
                            Application.DoEvents();
                        }
                        Display_pbDisplay.Image = bDummy;
                        DispImage[2] = DispImage[1];
                        DispImage[1] = DispImage[0];
                        Display_pbDisplay.Image = DispImage[1];
                        DispImageIndex += iSteps;
                        bwPrevDisp.RunWorkerAsync();
                    }
                    if (iSteps == 1)
                    {
                        while (bwNextDisp.IsBusy)
                        {
                            System.Threading.Thread.Sleep(1);
                            Application.DoEvents();
                        }
                        Display_pbDisplay.Image = bDummy;
                        DispImage[0] = DispImage[1];
                        DispImage[1] = DispImage[2];
                        Display_pbDisplay.Image = DispImage[1];
                        DispImageIndex += iSteps;
                        bwNextDisp.RunWorkerAsync();
                    }
                }
            }
            else
            {*/
            SetThumbPageRanges(thPages[1].idRange.X + (iSteps * pnThumb.Count));
            iThumbPage += iSteps;
            RedrawThumbnails(false);
            ReloadThumbnails(1, false); //true);
            //}
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            HideForm();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private void tBwVoid_Tick(object sender, EventArgs e)
        {
            if (bwPrevVoid)
            {
                while (bwPrevDisp.IsBusy)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(1);
                }
                bwPrevDisp.RunWorkerAsync();
                bwPrevVoid = false;
            }
            if (bwNextVoid)
            {
                while (bwNextDisp.IsBusy)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(1);
                }
                bwNextDisp.RunWorkerAsync();
                bwNextVoid = false;
            }
        }

        private bool CheckForChanges()
        {
            bool bChanges = false;
            for (int a = 0; a < idImages.Length; a++)
            {
                if (idImages[a].bModified) bChanges = true;
            }
            return bChanges;
        }
        private bool GUI_CheckForChanges()
        {
            if (CheckForChanges())
            {
                if (MessageBox.Show("You have changes that needs to be saved." + "\r\n" +
                    "If you perform this action, the queued changes will be lost." + "\r\n" +
                    "To store all changes, press the \"Save changes\" button." + "\r\n\r\n" +
                    "Do you wish to continue (and thereby lose all changes)?", "Hello",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return true;
            }
            return false;
        }
        private void General_cmdSaveChanges_Click(object sender, EventArgs e)
        {
            if (!DB.IsOpen)
            {
                MessageBox.Show("Please select or create a database first.",
                    "You are doing it wrong.", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation); return;
            }
            frmWait.sHeader = "Executing changes";
            frmWait.sFooter = "Constructing todo-list";
            frmWait.bVisible = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            int iDelCnt = 0;
            for (int a = 0; a < idImages.Length; a++)
            {
                if (idImages[a].bDeleted) iDelCnt++;
            }
            if (iDelCnt >= 1)
            {
                frmWait.sMain = "Deleting " + iDelCnt + " images";
                frmWait.bInstant = true; while (frmWait.bInstant) Application.DoEvents();
                using (SQLiteTransaction dbTrs = DB.Data.BeginTransaction())
                {
                    using (SQLiteCommand DBc = DB.Data.CreateCommand())
                    {
                        DBc.CommandText = "DELETE FROM 'images' WHERE hash = ?";
                        SQLiteParameter dbParam = DBc.CreateParameter();
                        DBc.Parameters.Add(dbParam);

                        for (int a = 0; a < idImages.Length; a++)
                        {
                            if (idImages[a].bDeleted)
                            {
                                frmWait.sFooter = "Processing " + a + " of " + iDelCnt;
                                System.IO.File.Delete(sAppPath + DB.Path +
                                    idImages[a].sHash + "." + idImages[a].sType);
                                dbParam.Value = idImages[a].sHash;
                                DBc.ExecuteNonQuery();
                            }
                        }
                    }
                    frmWait.sFooter = "Finalizing deletion";
                    dbTrs.Commit();
                }
            }

            frmWait.sMain = "Looking for changes";
            frmWait.bInstant = true; while (frmWait.bInstant) Application.DoEvents();
            int iModCnt = 0;
            string[] saDBFields = new string[]
            {
                "name", "rating", "t_general", "t_source", "t_chars", "t_artists"
            };
            using (SQLiteTransaction dbTrs = DB.Data.BeginTransaction())
            {
                SQLiteCommand[] DBcW = new SQLiteCommand[6];
                SQLiteParameter[] dbP_Value = new SQLiteParameter[DBcW.Length];
                SQLiteParameter[] dbP_Hash = new SQLiteParameter[DBcW.Length];
                for (int a = 0; a < DBcW.Length; a++)
                {
                    DBcW[a] = DB.Data.CreateCommand();
                    DBcW[a].CommandText = "UPDATE 'images' SET " + saDBFields[a] + " = ? WHERE hash = ?";
                    dbP_Value[a] = DBcW[a].CreateParameter();
                    dbP_Hash[a] = DBcW[a].CreateParameter();
                    DBcW[a].Parameters.Add(dbP_Value[a]);
                    DBcW[a].Parameters.Add(dbP_Hash[a]);
                }
                using (SQLiteCommand DBcR = DB.Data.CreateCommand())
                {
                    DBcR.CommandText = "SELECT * FROM 'images'";
                    using (SQLiteDataReader rd = DBcR.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            for (int a = 0; a < idImages.Length; a++)
                            {
                                bool bThisChanged = false;
                                bool[] baThisChanged = new bool[DBcW.Length];
                                idImages[a].bModified = false;
                                if (!idImages[a].bDeleted &&
                                    idImages[a].sHash == rd.GetString(0))
                                {
                                    if (idImages[a].sName != rd.GetString(5))
                                    {
                                        bThisChanged = true;
                                        baThisChanged[0] = true;
                                        dbP_Value[0].Value = idImages[a].sName;
                                    }
                                    if (idImages[a].iRating != rd.GetInt32(6))
                                    {
                                        bThisChanged = true;
                                        baThisChanged[1] = true;
                                        dbP_Value[1].Value = idImages[a].iRating;
                                    }
                                    if (idImages[a].sTGeneral != rd.GetString(7))
                                    {
                                        bThisChanged = true;
                                        baThisChanged[2] = true;
                                        dbP_Value[2].Value = idImages[a].sTGeneral;
                                    }
                                    if (idImages[a].sTSource != rd.GetString(8))
                                    {
                                        bThisChanged = true;
                                        baThisChanged[3] = true;
                                        dbP_Value[3].Value = idImages[a].sTSource;
                                    }
                                    if (idImages[a].sTChars != rd.GetString(9))
                                    {
                                        bThisChanged = true;
                                        baThisChanged[4] = true;
                                        dbP_Value[4].Value = idImages[a].sTChars;
                                    }
                                    if (idImages[a].sTArtists != rd.GetString(10))
                                    {
                                        bThisChanged = true;
                                        baThisChanged[5] = true;
                                        dbP_Value[5].Value = idImages[a].sTArtists;
                                    }
                                    if (bThisChanged)
                                    {
                                        for (int b = 0; b < baThisChanged.Length; b++)
                                        {
                                            if (baThisChanged[b])
                                            {
                                                dbP_Hash[b].Value = idImages[a].sHash;
                                                DBcW[b].ExecuteNonQuery(); iModCnt++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                frmWait.sHeader = "Saving " + iModCnt + " changes";
                frmWait.sFooter = iModCnt + " changes identified";
                dbTrs.Commit();
                for (int a = 0; a < DBcW.Length; a++)
                {
                    DBcW[a].Dispose();
                }
            }

            if (iDelCnt != 0 || iModCnt != 0)
            {
                frmWait.sMain = "Reconstructing thumb view";
                frmWait.bInstant = true; while (frmWait.bInstant)
                    Application.DoEvents();
                if (LastSearch != null)
                {
                    Search(LastSearch);
                    SetThumbPageRanges(0);
                    ShowPanel(pnThumbs); Application.DoEvents();
                    RedrawThumbnails(true); ReloadThumbnails(1, false);
                }
                else
                {
                    General_cmdAllImages_Click(
                        new object(), new EventArgs());
                }
            }
            frmWait.sHeader = "";
            frmWait.sMain = "All done";
            frmWait.sFooter = "";
            frmWait.bInstant = true; while (frmWait.bInstant)
                Application.DoEvents();
            System.Threading.Thread.Sleep(1000);
            frmWait.bVisible = false;
            frmWait.bInstant = true;
        }

        private void General_cmdMassEdit_Click(object sender, EventArgs e)
        {
            ShowPanel(pnMassEdit);
        }
        private void MassEdit_cmdCancel_Click(object sender, EventArgs e)
        {
            ShowPanel(pnThumbs);
        }
        private void MassEdit_cmdStart_Click(object sender, EventArgs e)
        {
            bool bRedoGUI = false;
            frmWait.sHeader = "This is a beta function.";
            frmWait.sMain = "Preparing mass edit";
            frmWait.sFooter = "";
            frmWait.bVisible = true;
            frmWait.bInstant = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            
            int iMainAction = MassEdit_ddMainAction.SelectedIndex+1;
            string sModifyValue = MassEdit_txtModifyValue.Text;
            string sInsertValue = MassEdit_txtInsertValue.Text;
            int iInsertTo = MassEdit_ddInsertTo.SelectedIndex + 1;
            int iTargetGroup = MassEdit_ddTargetGroup.SelectedIndex + 1;
            int iCondTarget = MassEdit_ddCondTarget.SelectedIndex + 1;
            int iCondCType = MassEdit_ddCondCType.SelectedIndex + 1;
            string sCondValue = MassEdit_txtCondValue.Text;
            int iChangeCnt = 0;

            ImageData[] idEdit = new ImageData[0];
            if (iTargetGroup == 1) //Selected images
            {
                int i = 0;
                for (int a = 0; a < idImages.Length; a++)
                {
                    if (idImages[a].bSelected) i++;
                }
                idEdit = new ImageData[i]; i = -1;
                for (int a = 0; a < idImages.Length; a++)
                {
                    if (idImages[a].bSelected)
                    {
                        i++; idEdit[i] = idImages[a];
                    }
                }
            }
            if (iTargetGroup == 2) //Last search results
            {
                ImageData[] old_idImages = idImages;
                Search(LastSearch);
                idEdit = idImages;
                idImages = old_idImages;
            }
            if (iTargetGroup == 3) //All images in database
            {
                ImageData[] old_idImages = idImages;
                string sFormats = "";
                foreach (string sFormat in saAllowedTypes)
                    sFormats += sFormat.Substring(1) + ",";
                sFormats = sFormats.Substring(0, sFormats.Length - 1);

                SearchPrm sp_all = new SearchPrm();
                sp_all.sImagFormat = sFormats;
                SearchPrm old_sp = LastSearch;
                Search(sp_all);
                
                idEdit = idImages;
                LastSearch = old_sp;
                idImages = old_idImages;
            }

            using (SQLiteTransaction dbTrs = DB.Data.BeginTransaction())
            {
                using (SQLiteCommand DBc = DB.Data.CreateCommand())
                {
                    string sInsertTo = "";
                    if (iInsertTo == 1) sInsertTo = "name"; // Image Name
                    if (iInsertTo == 2) sInsertTo = "rating"; // Image Rating
                    if (iInsertTo == 3) sInsertTo = "t_general"; // Tags General
                    if (iInsertTo == 4) sInsertTo = "t_source"; // Tags Source
                    if (iInsertTo == 5) sInsertTo = "t_chars"; // Tags Chars
                    if (iInsertTo == 6) sInsertTo = "t_artists"; // Tags Artists

                    if (iInsertTo == 2)
                    {
                        sInsertValue = sInsertValue.Replace(",", "").Replace(" ", "");
                        try
                        {
                            if (iMainAction == 2) throw new Exception("wat");
                            Convert.ToInt32(sInsertValue);
                        }
                        catch
                        {
                            MessageBox.Show("Don't do that with the rating field!",
                                "You are doing it wrong.", MessageBoxButtons.OK,
                                MessageBoxIcon.Error); return;
                        }
                    }

                    if (iMainAction == 1) //Set value
                        DBc.CommandText = "UPDATE 'images' SET " + sInsertTo + " = ? WHERE hash = ?";
                    if (iMainAction == 2) //Append value
                        DBc.CommandText = "UPDATE 'images' SET " + sInsertTo + " = ? WHERE hash = ?";
                    if (iMainAction == 3) //Replace value
                        DBc.CommandText = "UPDATE 'images' SET " + sInsertTo + " = ? WHERE hash = ?";
                    if (iMainAction == 4) //Delete image
                        DBc.CommandText = "DELETE FROM 'images' WHERE hash = ?";
                    if (iMainAction == 5) //Select image
                        DBc.CommandText = "SELECT * FROM 'images'";

                    SQLiteParameter dbParam1 = DBc.CreateParameter();
                    SQLiteParameter dbParam2 = DBc.CreateParameter();
                    DBc.Parameters.Add(dbParam1);
                    DBc.Parameters.Add(dbParam2);

                    frmWait.sMain = "Checking " + idEdit.Length + " images";
                    for (int a = 0; a < idEdit.Length; a++)
                    {
                        frmWait.sHeader = "Checking " + a + " of " + idEdit.Length;
                        bool bFulfillsCondition = false;
                        string sConditionTarget = "";
                        if (iCondTarget == 1) sConditionTarget = "," + idEdit[a].sName + ","; //Image Name
                        if (iCondTarget == 2) sConditionTarget = "," + idEdit[a].iRating + ","; //Image Rating
                        if (iCondTarget == 3) sConditionTarget = "," + idEdit[a].sTGeneral + ","; //Tags General
                        if (iCondTarget == 4) sConditionTarget = "," + idEdit[a].sTSource + ","; //Tags Source
                        if (iCondTarget == 5) sConditionTarget = "," + idEdit[a].sTChars + ","; //Tags Chars
                        if (iCondTarget == 6) sConditionTarget = "," + idEdit[a].sTArtists + ","; //Tags Artists
                        if (sCondValue == "")
                        {
                            bFulfillsCondition = true;
                        }
                        else
                        {
                            bool bContainsAllOf = true;
                            bool bContainsAnyOf = false;
                            bool bIsBlank = (sCondValue == "");
                            string[] saCondValue = sCondValue.Split(',');
                            for (int b = 0; b < saCondValue.Length; b++)
                            {
                                saCondValue[b] = saCondValue[b].Trim(' ');
                                if (sConditionTarget.Contains("," + saCondValue[b] + ","))
                                    bContainsAnyOf = true;
                                else bContainsAllOf = false;
                            }
                            if (iCondCType == 1) if (bContainsAllOf) bFulfillsCondition = true; //Contains all of
                            if (iCondCType == 2) if (bContainsAnyOf) bFulfillsCondition = true; //Contains any of
                            if (iCondCType == 3) if (!bContainsAllOf) bFulfillsCondition = true; //Does not contain all of
                            if (iCondCType == 4) if (!bContainsAnyOf) bFulfillsCondition = true; //Does not contain any of
                            if (iCondCType == 5) if (bIsBlank) bFulfillsCondition = true; //Is blank
                            if (iCondCType == 6) if (!bIsBlank) bFulfillsCondition = true; //Is not blank
                        }
                        if (bFulfillsCondition)
                        {
                            iChangeCnt++; frmWait.sFooter = "Modified " + iChangeCnt + " images";
                            string sOriginalField = "";
                            if (iInsertTo == 1) sOriginalField = "" + idEdit[a].sName; // Image Name
                            if (iInsertTo == 2) sOriginalField = "" + idEdit[a].iRating; // Image Rating
                            if (iInsertTo == 3) sOriginalField = "" + idEdit[a].sTGeneral; // Tags General
                            if (iInsertTo == 4) sOriginalField = "" + idEdit[a].sTSource; // Tags Source
                            if (iInsertTo == 5) sOriginalField = "" + idEdit[a].sTChars; // Tags Chars
                            if (iInsertTo == 6) sOriginalField = "" + idEdit[a].sTArtists; // Tags Artists

                            string sVal = "";
                            if (iMainAction == 1) //Set value
                            {
                                sVal = sInsertValue;
                                while (sVal.Contains(" ,") || sVal.Contains(", "))
                                    sVal = sVal.Replace(" ,", ",").Replace(", ", ",");
                                while (sVal.Contains(",,")) sVal = sVal.Replace(",,", ",");
                                sVal = sVal.Trim(',');

                                dbParam1.Value = sVal;
                                dbParam2.Value = idEdit[a].sHash;
                                DBc.ExecuteNonQuery();
                            }
                            if (iMainAction == 2) //Append value
                            {
                                sVal = sOriginalField + "," + sInsertValue;
                                while (sVal.Contains(" ,") || sVal.Contains(", "))
                                    sVal = sVal.Replace(" ,", ",").Replace(", ", ",");
                                while (sVal.Contains(",,")) sVal = sVal.Replace(",,", ",");
                                sVal = sVal.Trim(',');

                                dbParam1.Value = sVal;
                                dbParam2.Value = idEdit[a].sHash;
                                DBc.ExecuteNonQuery();
                            }
                            if (iMainAction == 3) //Replace value
                            {
                                sVal = sOriginalField.Replace
                                    (sModifyValue, sInsertValue);
                                while (sVal.Contains(" ,") || sVal.Contains(", "))
                                    sVal = sVal.Replace(" ,", ",").Replace(", ", ",");
                                while (sVal.Contains(",,")) sVal = sVal.Replace(",,", ",");
                                sVal = sVal.Trim(',');

                                dbParam1.Value = sVal;
                                dbParam2.Value = idEdit[a].sHash;
                                DBc.ExecuteNonQuery();
                            }
                            if (iMainAction == 4) //Delete image
                            {
                                System.IO.File.Delete(sAppPath + DB.Path +
                                    idImages[a].sHash + "." + idImages[a].sType);
                                dbParam1.Value = idImages[a].sHash;
                                DBc.ExecuteNonQuery();
                            }

                            if (iMainAction != 4)
                            {
                                if (iInsertTo == 1) idEdit[a].sName = sVal; // Image Name
                                if (iInsertTo == 2) idEdit[a].iRating = 
                                    Convert.ToInt32(sVal); // Image Rating
                                if (iInsertTo == 3) idEdit[a].sTGeneral = sVal; // Tags General
                                if (iInsertTo == 4) idEdit[a].sTSource = sVal; // Tags Source
                                if (iInsertTo == 5) idEdit[a].sTChars = sVal; // Tags Chars
                                if (iInsertTo == 6) idEdit[a].sTArtists = sVal; // Tags Artists
                            }
                        }
                    }
                }
                frmWait.sMain = "Saving changes";
                dbTrs.Commit();
            }
            if (iTargetGroup == 3) bRedoGUI = true; // All
            if (iMainAction == 4 && iTargetGroup == 2) bRedoGUI = true; // Delete & Searchresults
            if (iMainAction == 4 && iTargetGroup == 1) bRedoGUI = true; // Delete & Selected
            if (bRedoGUI)
            {
                frmWait.sMain = "Reconstructing thumb view"; frmWait.bInstant = true;
                while (frmWait.bInstant) Application.DoEvents();
                if (LastSearch != null)
                {
                    Search(LastSearch);
                    SetThumbPageRanges(0);
                    ShowPanel(pnThumbs); Application.DoEvents();
                    RedrawThumbnails(true); ReloadThumbnails(1, false);
                }
                else
                {
                    General_cmdAllImages_Click(
                        new object(), new EventArgs());
                }
            }
            frmWait.sMain = "All done";
            frmWait.bInstant = true;
            frmWait.bVisible = false;
        }
        private void EditSingleParameter(int i, int iParam, string sValue, int iAction)
        {
            if (i >= idImages.Length)
            {
                MessageBox.Show("I was asked to do something, but the target was out of range.\r\n" +
                    "I have no fucking clue how this happened. Please contact Praetox.",
                    "You are doing it wrong.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (iAction <= 0)
            {
                MessageBox.Show("I was asked to do something, but not what.\r\n" +
                    "Maybe you forgot to set the \"Action at [Enter]\" dropdown again?",
                    "You are doing it wrong.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sOldVal = ""; string sVal = "";
            idImages[i].bModified = true;
            if (iParam == 1) sOldVal = "" + idImages[i].sName;
            if (iParam == 2) sOldVal = "" + idImages[i].iRating;
            if (iParam == 3) sOldVal = "" + idImages[i].sTGeneral;
            if (iParam == 4) sOldVal = "" + idImages[i].sTSource;
            if (iParam == 5) sOldVal = "" + idImages[i].sTChars;
            if (iParam == 6) sOldVal = "" + idImages[i].sTArtists;
            
            if (iAction == 1) //Overwrite
            {
                sVal = sValue;
            }
            if (iAction == 2) //Append
            {
                sVal = sValue + "," + sValue;
            }
            if (iAction == 3) //Remove
            {
                sVal = sOldVal.Replace(sValue, "");
            }
            while (sVal.Contains(" ,") || sVal.Contains(", "))
                sVal = sVal.Replace(" ,", ",").Replace(", ", ",");
            while (sVal.Contains(",,")) sVal = sVal.Replace(",,", ",");
            sVal = sVal.Trim(',');

            if (iParam == 1) idImages[i].sName = sVal;
            if (iParam == 2) idImages[i].iRating = Convert.ToInt32(sVal);
            if (iParam == 3) idImages[i].sTGeneral = sVal;
            if (iParam == 4) idImages[i].sTSource = sVal;
            if (iParam == 5) idImages[i].sTChars = sVal;
            if (iParam == 6) idImages[i].sTArtists = sVal;
        }
        private void QEdit_txtImagName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                QEdit_sName = QEdit_txtImagName.Text;
            QEdit_txtImagName.Text = QEdit_sName;
            if (e.KeyCode == Keys.Enter)
            {
                EditSingleParameter(DispImageIndex, 1, QEdit_txtImagName.Text, 
                    QEdit_ddEnterAction.SelectedIndex + 1);
                QEdit_txtImagRating.Focus();
                QEdit_txtImagRating.SelectAll();
            }
        }
        private void QEdit_txtImagRating_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                QEdit_sRating = QEdit_txtImagRating.Text;
            QEdit_txtImagRating.Text = QEdit_sRating;
            if (e.KeyCode == Keys.Enter)
            {
                EditSingleParameter(DispImageIndex, 2, QEdit_txtImagRating.Text,
                    QEdit_ddEnterAction.SelectedIndex + 1);
                QEdit_txtTagsGeneral.Focus();
                QEdit_txtTagsGeneral.SelectAll();
            }
        }
        private void QEdit_txtTagsGeneral_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                QEdit_sTGeneral = QEdit_txtTagsGeneral.Text;
            QEdit_txtTagsGeneral.Text = QEdit_sTGeneral;
            if (e.KeyCode == Keys.Enter)
            {
                EditSingleParameter(DispImageIndex, 3, QEdit_txtTagsGeneral.Text,
                    QEdit_ddEnterAction.SelectedIndex + 1);
                QEdit_txtTagsSource.Focus();
                QEdit_txtTagsSource.SelectAll();
            }
        }
        private void QEdit_txtTagsSource_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                QEdit_sTSource = QEdit_txtTagsSource.Text;
            QEdit_txtTagsSource.Text = QEdit_sTSource;
            if (e.KeyCode == Keys.Enter)
            {
                EditSingleParameter(DispImageIndex, 4, QEdit_txtTagsSource.Text,
                    QEdit_ddEnterAction.SelectedIndex + 1);
                QEdit_txtTagsChars.Focus();
                QEdit_txtTagsChars.SelectAll();
            }
        }
        private void QEdit_txtTagsChars_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                QEdit_sTChars = QEdit_txtTagsChars.Text;
            QEdit_txtTagsChars.Text = QEdit_sTChars;
            if (e.KeyCode == Keys.Enter)
            {
                EditSingleParameter(DispImageIndex, 5, QEdit_txtTagsChars.Text,
                    QEdit_ddEnterAction.SelectedIndex + 1);
                QEdit_txtTagsArtists.Focus();
                QEdit_txtTagsArtists.SelectAll();
            }
        }
        private void QEdit_txtTagsArtists_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                QEdit_sTArtists = QEdit_txtTagsArtists.Text;
            QEdit_txtTagsArtists.Text = QEdit_sTArtists;
            if (e.KeyCode == Keys.Enter)
            {
                EditSingleParameter(DispImageIndex, 6, QEdit_txtTagsArtists.Text,
                    QEdit_ddEnterAction.SelectedIndex + 1);
                DispSkip(1);
                QEdit_txtImagName.Focus();
                QEdit_txtImagName.SelectAll();
            }
        }

        private void General_cmdReadme_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.praetox.com/site/other/pimgdb_readme.html");
        }

        private void General_cmdWPChanger_Click(object sender, EventArgs e)
        {
            ShowPanel(pnWPChanger);
        }
        private void WPChanger_cmdAddThumbs_Click(object sender, EventArgs e)
        {
            for (int a = 0; a < idImages.Length; a++)
            {
                if (idImages[a].bSelected)
                {
                    WPChanger_lstImages.Items.Add(
                        DB.Path +
                        idImages[a].sHash + "." +
                        idImages[a].sType);
                }
            }
        }
        private void WPChanger_cmdRemThumbs_Click(object sender, EventArgs e)
        {
            for (int a = 0; a < idImages.Length; a++)
            {
                if (idImages[a].bSelected)
                {
                    for (int b = 0; b < WPChanger_lstImages.Items.Count; b++)
                    {
                        if (WPChanger_lstImages.Items[b].ToString() ==
                            DB.Path + idImages[a].sHash + "." + idImages[a].sType)
                        {
                            WPChanger_lstImages.Items.RemoveAt(b); b--;
                        }
                    }
                }
            }
        }
        private void WPChanger_cmdRemFromList_Click(object sender, EventArgs e)
        {
            while (WPChanger_lstImages.SelectedItems.Count > 0)
                WPChanger_lstImages.Items.Remove(
                    WPChanger_lstImages.SelectedItems[0]);
        }
        private void WPChanger_cmdLoad_Click(object sender, EventArgs e)
        {
            string sVal = FileRead("wpchanger.txt").Replace("\r", "");
            if (sVal == "") return;
            sVal = sVal.Substring(0, sVal.Length - 1);
            WPChanger_lstImages.Items.Clear();
            string[] saVal = sVal.Split('\n');
            foreach (string sWall in saVal)
                WPChanger_lstImages.Items.Add(sWall);
        }
        private void WPChanger_cmdSave_Click(object sender, EventArgs e)
        {
            string sVal = "";
            for (int a = 0; a < WPChanger_lstImages.Items.Count; a++)
            {
                sVal += WPChanger_lstImages.Items[a].ToString() + "\r\n";
            }
            FileWrite("wpchanger.txt", sVal);
        }
        private void WPChanger_cmdChange_Click(object sender, EventArgs e)
        {
            ChangeWP(-1);
        }
        private void ChangeWP(string sPath)
        {
            string[] sOldWPs = System.IO.Directory.GetFiles(sAppPath, "_wp_*");
            foreach (string sOldWP in sOldWPs) System.IO.File.Delete(sOldWP);

            string sFName = sPath;
            Bitmap bWP = (Bitmap)Bitmap.FromFile(sPath);
            sFName = sFName.Substring(sFName.LastIndexOf("/") + 1);
            sFName = sFName.Substring(0, sFName.IndexOf(".") - 1);
            sFName = "_wp_" + sFName + ".bmp";
            bWP.Save(sAppPath + sFName, System.Drawing.Imaging.ImageFormat.Bmp);

            Microsoft.Win32.RegistryKey key =
                Microsoft.Win32.Registry.CurrentUser.
                OpenSubKey("Control Panel\\Desktop", true);
            key.SetValue(@"WallpaperStyle", "2");
            key.SetValue(@"TileWallpaper", "0");
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0,
                sFName, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
        private void ChangeWP(int iPath)
        {
            int iWP = iPath; if (WPChanger_lstImages.Items.Count == 0) return;
            if (iWP == -1) iWP = rnd.Next(0, WPChanger_lstImages.Items.Count);
            string sFName = WPChanger_lstImages.Items[iWP].ToString();
            ChangeWP(sAppPath + sFName);
        }
        private void tWPChanger_Tick(object sender, EventArgs e)
        {
            if (!bRunning) return;
            if (WPChanger_txtFrequency.Text != "" &&
                OnlyContains(WPChanger_txtFrequency.Text, "0123456789"))
            {
                int iFreq = Convert.ToInt32(WPChanger_txtFrequency.Text);
                if ((DateTime.Now.Ticks/10000000) >
                    (dtLastWPChange.Ticks/10000000) + (iFreq*60))
                {
                    dtLastWPChange = DateTime.Now;
                    ChangeWP(-1);
                }
            }
        }

        private void General_cmdUpload_Click(object sender, EventArgs e)
        {
            ShowPanel(pnUpload);
        }
        private void Upload_cmdAddThumbs_Click(object sender, EventArgs e)
        {
            bool[] bAppend = new bool[idImages.Length];
            for (int a = 0; a < idImages.Length; a++)
            {
                if (idImages[a].bSelected) bAppend[a] = true;
            }
            for (int a = 0; a < Upload_lstImages.Items.Count; a++)
            {
                string sTmp = Upload_lstImages.Items[a].ToString();
                for (int b = 0; b < idImages.Length; b++)
                {
                    if (idImages[b].bSelected)
                    {
                        if (sTmp == DB.Path +
                            idImages[b].sHash + "." +
                            idImages[b].sType)
                            bAppend[b] = false;
                    }
                }
            }
            for (int a = 0; a < idImages.Length; a++)
            {
                if (bAppend[a])
                {
                    Upload_lstImages.Items.Add(
                        DB.Path +
                        idImages[a].sHash + "." +
                        idImages[a].sType);
                }
            }
        }
        private void Upload_cmdRemThumbs_Click(object sender, EventArgs e)
        {
            for (int a = 0; a < idImages.Length; a++)
            {
                if (idImages[a].bSelected)
                {
                    for (int b = 0; b < Upload_lstImages.Items.Count; b++)
                    {
                        if (Upload_lstImages.Items[b].ToString() ==
                            DB.Path + idImages[a].sHash + "." + idImages[a].sType)
                        {
                            Upload_lstImages.Items.RemoveAt(b); b--;
                        }
                    }
                }
            }
        }
        private void Upload_cmdRemList_Click(object sender, EventArgs e)
        {
            while (Upload_lstImages.SelectedItems.Count > 0)
                Upload_lstImages.Items.Remove(
                    Upload_lstImages.SelectedItems[0]);
        }
        private void Upload_cmdStart_Click(object sender, EventArgs e)
        {
            Upload_txtStatus.Text = "Fetching parameters from website"; Application.DoEvents();
            WebReq WPrep = new WebReq();
            string sRefer = Upload_ddURL.Text;
            WPrep.Request(sRefer);
            while (!WPrep.isReady)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            string sRaw = WPrep.Response;
            string sPostTo = Split(Split(sRaw, "<form name=\"post\" action=\"", 1), "\"", 0);
            string sComment = Upload_txtComment.Text;
            int iSizeLim = Convert.ToInt32(Split(Split(sRaw, "name=\"MAX_FILE_SIZE\" value=\"", 1), "\"", 0));
            int iThreadID = Convert.ToInt32(Split(Split(sRaw, "name=resto value=\"", 1), "\"", 0));
            int iPass = Convert.ToInt32(Upload_txtPass.Text);

            Upload_txtStatus.Text = "Preparing"; Application.DoEvents();
            string sName = Upload_lstImages.Items[0].ToString().Substring(DB.Path.Length);
            string sPath = sAppPath + Upload_lstImages.Items[0].ToString();

            System.IO.File.Copy(sPath, sAppPath + "upload", true);
            System.IO.FileStream fs = System.IO.File.Open(
                sAppPath + "upload", System.IO.FileMode.Open);
            byte[] bImg = new byte[fs.Length];
            fs.Read(bImg, 0, (int)fs.Length);
            fs.Close(); fs.Dispose();
            System.IO.File.Delete(sAppPath + "upload");

            // if (bImg.Length > iSizeLim)
            
            WebReq WReq = new WebReq();
            string sMPBound = "----------" + WReq.RandomChars(22);
            string sPD1 = "--" + sMPBound + "\r\n" +
                "Content-Disposition: form-data; name=\"MAX_FILE_SIZE\"" + "\r\n" +
                "\r\n" + iSizeLim + "\r\n" + "--" + sMPBound + "\r\n" +
                "Content-Disposition: form-data; name=\"resto\"" + "\r\n" +
                "\r\n" + iThreadID + "\r\n" + "--" + sMPBound + "\r\n" +
                "Content-Disposition: form-data; name=\"name\"" + "\r\n" +
                "\r\n" + "" + "\r\n" + "--" + sMPBound + "\r\n" +
                "Content-Disposition: form-data; name=\"sub\"" + "\r\n" +
                "\r\n" + "" + "\r\n" + "--" + sMPBound + "\r\n" +
                "Content-Disposition: form-data; name=\"email\"" + "\r\n" +
                "\r\n" + "pimgdb_user@praetox.com" + "\r\n" + "--" + sMPBound + "\r\n" +
                "Content-Disposition: form-data; name=\"com\"" + "\r\n" +
                "\r\n" + sComment + "\r\n" + "--" + sMPBound + "\r\n" +
                "Content-Disposition: form-data; name=\"upfile\"; filename=\"pImgDB v" + sAppVer + ".jpg\"" +
                "\r\n" + "Content-Type: image/jpeg" + "\r\n" +
                "\r\n";
            string sPD2 = "\r\n" + "--" + sMPBound + "\r\n" +
                "Content-Disposition: form-data; name=\"pwd\"" + "\r\n" +
                "\r\n" + iPass + "\r\n" + "--" + sMPBound + "\r\n" +
                "Content-Disposition: form-data; name=\"mode\"" + "\r\n" +
                "\r\n" + "regist" + "\r\n" + "--" + sMPBound + "--" + "\r\n";
            byte[] bPD1 = new byte[sPD1.Length];
            byte[] bPD2 = new byte[sPD2.Length];
            for (int a = 0; a < sPD1.Length; a++) bPD1[a] = (byte)sPD1[a];
            for (int a = 0; a < sPD2.Length; a++) bPD2[a] = (byte)sPD2[a];
            byte[] bPD = new byte[bPD1.Length + bImg.Length + bPD2.Length];
            bPD1.CopyTo(bPD, 0);
            bImg.CopyTo(bPD, 0 + bPD1.Length);
            bPD2.CopyTo(bPD, 0 + bPD1.Length + bImg.Length);
            System.Net.WebHeaderCollection whc = new System.Net.WebHeaderCollection();
            whc.Add("Expect: 100-continue");
            whc.Add("Referer: " + sRefer);

            Upload_txtStatus.Text = "Uploading image " + "1" + " of " + "7"; Application.DoEvents();
            WReq.Request(sPostTo, whc, bPD, sMPBound, 3, "", true);
            while (!WReq.isReady)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            Upload_txtStatus.Text = "Next: " + "1" + " of " + "7" + " (" + "0" + " errors)";
            Clipboard.Clear(); Clipboard.SetText(WReq.Response);
        }
        private void Upload_cmdPause_Click(object sender, EventArgs e)
        {

        }
        private void Upload_cmdStop_Click(object sender, EventArgs e)
        {

        }
        private void tUpload_Tick(object sender, EventArgs e)
        {

        }
        private void Upload_cmdPass_Click(object sender, EventArgs e)
        {
            string ret = "";
            for (int a = 0; a < 8; a++)
            {
                ret += rnd.Next(0, 10) + "";
            }
            Upload_txtPass.Text = ret;
        }
    }

    public class thPageData
    {
        public Bitmap[] bImages = new Bitmap[0];
        public Point idRange = new Point(-1, -1);
    }
    public class ImageData
    {
        public bool bModified;
        public bool bSelected;
        public bool bDeleted;
        public string sType;
        public string sHash;
        public Point ptRes;
        public int iLength;
        public string sName;
        public int iRating;
        public string sTGeneral;
        public string sTSource;
        public string sTArtists;
        public string sTChars;
    }
    public class SearchPrm
    {
        public string sTagsGlobal = "";
        public string sImagName = "";
        public string sImagFormat = "";
        public string sImagRes = "";
        public string sImagRating = "";
        public string sTagsGeneral = "";
        public string sTagsSource = "";
        public string sTagsChars = "";
        public string sTagsArtists = "";
    }
    public class DB
    {
        public static SQLiteConnection Data;
        public static string Name = "";
        public static string Path = "";
        public static bool CanClose = true;
        public static bool IsOpen = false;

        public static bool Create(string sDBName, bool bOverwrite)
        {
            try
            {
                if (Data != null) { Data.Close(); Data.Dispose(); }
                if (System.IO.Directory.Exists("db_" + sDBName))
                {
                    if (bOverwrite)
                        System.IO.Directory.Delete("db_" + sDBName, true);
                    else return false;
                }
                System.IO.Directory.CreateDirectory("db_" + sDBName);
                SQLiteConnection.CreateFile("db_" + sDBName + "/_pImgDB.db");
                SQLiteConnection.CompressFile("db_" + sDBName + "/_pImgDB.db");
                Data = new SQLiteConnection("Data source=db_" + sDBName + "/_pImgDB.db");
                Data.Open(); Name = sDBName; Path = "db_" + sDBName + "/";

                using (SQLiteCommand DBc = Data.CreateCommand())
                {
                    DBc.CommandText = "CREATE TABLE 'images' (" +
                        "'hash' varchar(32), " +
                        "'type' varchar(4), " +
                        "'size' unsigned int, " +
                        "'xres' int, " +
                        "'yres' int, " +
                        "'name' text, " +
                        "'rating' int, " +
                        "'t_general' text, " +
                        "'t_source' text, " +
                        "'t_chars' text, " +
                        "'t_artists' text)";
                    DBc.ExecuteNonQuery();
                }
                IsOpen = true;
                return true;
            }
            catch
            {
                IsOpen = false;
                return false;
            }
        }
        public static bool Open(string sDBName)
        {
            try
            {
                if (Data != null) { Data.Close(); Data.Dispose(); }
                Data = new SQLiteConnection("Data source=db_" + sDBName + "/_pImgDB.db");
                Data.Open();
            }
            catch
            {
                IsOpen = false;
                return false;
            }
            if (Data.State == ConnectionState.Open)
            {
                Name = sDBName; Path = "db_" + Name + "/";
                Program.Reg_Access("Last DB - Name", sDBName);
                IsOpen = true; return true;
            }
            return false;
        }
        public static bool Open()
        {
            try
            {
                Close();
                Data.Open();
                IsOpen = true;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Close()
        {
            try
            {
                Data.Close();
                IsOpen = false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static int Entries()
        {
            if (!IsOpen) return -1; int iCnt = 0;
            using (SQLiteCommand DBc = Data.CreateCommand())
            {
                DBc.CommandText = "SELECT * FROM 'images'";
                using (SQLiteDataReader rd = DBc.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        iCnt++;
                    }
                }
            }
            return iCnt;
        }
    }
}
