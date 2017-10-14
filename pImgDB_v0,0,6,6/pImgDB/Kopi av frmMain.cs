using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Data.SQLite;

namespace pImgDB
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
        public static extern short GetKeyState(System.Windows.Forms.Keys vKey);
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
        private static string[] Split(string sSrc, string sDlm)
        {
            return sSrc.Split(new string[] { sDlm }, StringSplitOptions.None);
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
        /// <summary>
        /// Replaces all occurrences of vl1 in str by vl2. Case invariant, hence its name.
        /// </summary>
        public static string ReplaceCI(string str, string vl1, string vl2)
        {
            string ret = str;
            while (ret.ToLower().Contains(vl1.ToLower()))
            {
                string ret1 = ret.Substring(0, ret.ToLower().IndexOf(vl1.ToLower()));
                string ret2 = ret.Substring(ret1.Length + vl1.Length);
                ret = ret1 + vl2 + ret2;
            }
            return ret;
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
        private string oFileRead(string sFile)
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream
                    (sFile, System.IO.FileMode.Open, System.IO.FileAccess.Read);
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
        private void oFileWrite(string sFile, string Data, bool bAppend)
        {
            byte[] buf = CnvToAByt(Data);
            System.IO.FileMode AccessType = System.IO.FileMode.Create;
            if (bAppend) AccessType = System.IO.FileMode.Append;
            System.IO.FileStream fs = new System.IO.FileStream(sFile, AccessType);
            if (bAppend) fs.Seek(0, System.IO.SeekOrigin.End);
            fs.Write(buf, 0, buf.Length);
            fs.Flush(); fs.Close(); fs.Dispose();
        }
        public void oFileWrite(string sFile, string sVal)
        {
            oFileWrite(sFile, sVal, false);
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
        Form wfZoom = new frmZoom();
        Form wfTip = new frmTip();
        DateTime dtLastWPChange = DateTime.Now.AddDays(-1);
        DateTime dtLastUpload = DateTime.Now.AddDays(-1);
        private Color[] clr_pnThumb = new Color[] { Color.FromArgb(32, 32, 32),
                                                    Color.FromArgb(200, 220, 230) };
        private Color clr_BtnN = Color.FromArgb(64, 64, 64);
        private Color clr_BtnOn = Color.FromArgb(56, 72, 64);
        private Color clr_BtnOff = Color.FromArgb(72, 56, 64);
        private Color clr_BtnHold = Color.FromArgb(72, 72, 56);

        public static IntPtr ipMyHandle = (IntPtr)0;
        private static bool bInitialRun = true;
        private static bool bInTHotkeys = false;
        private static bool bInTPFClicks = false;
        private static bool bInTUpload = false;
        public static string PrgDomain = "http://tox.awardspace.us/div/";
        public static string ToxDomain = "http://praetox.com/";
        public static bool bRunning = false;

        public static int iHandleEx_ImageNotFound;
        // 1_Ignore  2_Remove_from_db  3_Shutdown

        public static bool VK_Caps = false;
        public static bool VK_Alt = false;
        public static bool VK_Ctrl = false;
        public static bool VK_Shft = false;
        public static bool VK_Home = false;
        public static bool VK_End = false;
        public static bool VK_Ins = false;
        public static bool VK_Del = false;
        public static bool VK_PgUp = false;
        public static bool VK_PgDn = false;

        private static string QEdit_sName = "";
        private static string QEdit_sComment = "";
        private static string QEdit_sRating = "";
        private static string QEdit_sTGeneral = "";
        private static string QEdit_sTSource = "";
        private static string QEdit_sTChars = "";
        private static string QEdit_sTArtists = "";

        private bool Upload_InSession = false;
        private int Upload_iDone = 0;
        private int Upload_iCurrent = 0;
        private int Upload_iError_HiRes = 0;
        private int Upload_iError_HiSize = 0;
        private int Upload_iError_Exists = 0;
        private int Upload_iError_iFlood = 0;
        private int Upload_iError_iLimit = 0;
        private int Upload_iError_iEmbed = 0;
        private int Upload_iError_Img404 = 0;
        private int Upload_iError_Other = 0;

        private static int LastSearchFallback = 1;
        private static SearchPrm LastSearch = new SearchPrm();
        double dStats_SelCount = 0, dStats_SelSize = 0, dStats_thPage = 0;
        double dStats_TotCount = 0, dStats_TotSize = 0, dStats_thPages = 0;
        int iVMode = 0; //0=All, 1=Search
        int iAMode = 0; //0=Normal, 1=Dupes

        public static Int32 DispImageIndex = -9001;
        public static Bitmap[] DispImage = new Bitmap[3];
        public static Point[] DispSizes = new Point[3];
        public static Point DispClick = new Point(0, 0);
        public static int iDispSidepn = -1;
        public static Rectangle ZoomRect = new Rectangle(-1, -1, -1, -1);
        public static Bitmap bDummy = new Bitmap(1, 1);
        public static Bitmap bDenied = new Bitmap(1, 1);
        public static Bitmap btDenied = new Bitmap(1, 1);
        public static BackgroundWorker bwPrevDisp;
        public static BackgroundWorker bwNextDisp;
        public static bool bwPrevVoid = false;
        public static bool bwNextVoid = false;

        public static string sGSep = "   ~   ";
        public static string sFirstImportRoot = "";
        private static bool bImportIsLocal = false;
        public static string[] saFullPaths = new string[0];

        public static bool bFocusMe;
        public static Point ptFrmPos;
        public static Size szFrmSize;
        public static Rectangle rcForm;
        public static FormWindowState fwsFrmState;
        public static FormBorderStyle fbsFrmStyle;
        public static bool bEnableSidebars = true;
        public static Point ptPosSidebar = new Point(12, 52);
        public static Point ptPosMainpan = new Point(232, 52);
        public static int iWidthMainpnDf = 220;
        public static int iActiveSidePan = 0;

        public static bool bQTagovEnabled = false;
        public static string[] sQTagovFields = new string[0];
        public static Point pQTagovEnum = new Point(2, 5);

        public static bool bQThumbs = true;
        public static int iQThumbsDel = 300;
        public static Point pQThumbs = new Point(384, 240);
        public static Point ptThumbSize;
        public static double dResMul = 1.5;
        public static int iThumbCnt = 16;
        public static int iThClicked = -1;
        public static int iThumbPage = 0;
        public static int iThumbPageR = 0;
        public static int iThWrkCnt = 0;
        public static int iThWrkMax = 2;
        public static int iThWrkTrg = -1;
        public static BackgroundWorker bwLoadThumbs;
        public static ImageData[] idImages = new ImageData[0];
        public static ImageData[] idImagesR = new ImageData[0];
        public static thPageData[] thPages = new thPageData[3];
        public static thPageData[] thPagesR = new thPageData[0];
        public static ImageData idCopied = new ImageData();

        public static string[] sTagbar;
        public static LabelArray lbTagbar;
        public static PanelArray pnThumb;
        public static PBoxArray pThumb;
        public static TBoxArray tThumb;
        private static Panel[] pnaPanelsMain = new Panel[0];
        private static Panel[] pnaPanelsControl = new Panel[0];
        private static Panel pnActiveMain = new Panel();
        private static Panel pnActiveControl = new Panel();

        private string pFBind_Root = "";
        private bool bHotTagger = false;
        #endregion

        private void QuickBM_Test()
        {
            tmr t = new tmr();
            string s = "";

            double[] d1 = new double[10];
            for (int a = 0; a < d1.Length; a++)
            {
                t.Start(); byte[] qp = cb.qPicR(new Bitmap("c:\\test.png"));
                t.Stop(); d1[a] = Math.Round(t.Ret * 1000);
            }

            double[] d2 = new double[10];
            for (int a = 0; a < d2.Length; a++)
            {
                t.Start(); byte[, ,] qp = cb.qPicR2(new Bitmap("c:\\test.png"));
                t.Stop(); d2[a] = Math.Round(t.Ret * 1000);
            }

            double[] d3 = new double[10];
            for (int a = 0; a < d3.Length; a++)
            {
                t.Start(); byte[, ,] qp = cb.qPicZ(cb.qPicR(new Bitmap("c:\\test.png")));
                t.Stop(); d3[a] = Math.Round(t.Ret * 1000);
            }

            double[] d4 = new double[10];
            for (int a = 0; a < d4.Length; a++)
            {
                t.Start(); byte[] qp = cb.qPicZ(cb.qPicR2(new Bitmap("c:\\test.png")));
                t.Stop(); d4[a] = Math.Round(t.Ret * 1000);
            }

            double[] d5 = new double[10];
            for (int a = 0; a < d5.Length; a++)
            {
                t.Start(); byte[] qp = cb.qPicZ(cb.qPicZ(cb.qPicR(new Bitmap("c:\\test.png"))));
                t.Stop(); d5[a] = Math.Round(t.Ret * 1000);
            }

            double[] d6 = new double[10];
            for (int a = 0; a < d6.Length; a++)
            {
                t.Start(); byte[, ,] qp = cb.qPicZ(cb.qPicZ(cb.qPicR2(new Bitmap("c:\\test.png"))));
                t.Stop(); d6[a] = Math.Round(t.Ret * 1000);
            }

            s += "\r\n 1          "; for (int a = 0; a < d1.Length; a++) s += " | " + d1[a]; s += "   ~   ";
            double d1t = 0; for (int a = 0; a < d1.Length; a++) d1t += d1[a]; d1t /= d1.Length; s += d1t + "        ";
            s += "\r\n 2          "; for (int a = 0; a < d2.Length; a++) s += " | " + d2[a]; s += "   ~   ";
            double d2t = 0; for (int a = 0; a < d2.Length; a++) d2t += d2[a]; d2t /= d2.Length; s += d2t + "        ";
            s += "\r\n 1-2      "; for (int a = 0; a < d3.Length; a++) s += " | " + d3[a]; s += "   ~   ";
            double d3t = 0; for (int a = 0; a < d3.Length; a++) d3t += d3[a]; d3t /= d3.Length; s += d3t + "        ";
            s += "\r\n 2-1      "; for (int a = 0; a < d4.Length; a++) s += " | " + d4[a]; s += "   ~   ";
            double d4t = 0; for (int a = 0; a < d4.Length; a++) d4t += d4[a]; d4t /= d4.Length; s += d4t + "        ";
            s += "\r\n 1-2-1  "; for (int a = 0; a < d5.Length; a++) s += " | " + d5[a]; s += "   ~   ";
            double d5t = 0; for (int a = 0; a < d5.Length; a++) d5t += d5[a]; d5t /= d5.Length; s += d5t + "        ";
            s += "\r\n 2-1-2  "; for (int a = 0; a < d6.Length; a++) s += " | " + d6[a]; s += "   ~   ";
            double d6t = 0; for (int a = 0; a < d6.Length; a++) d6t += d6[a]; d6t /= d6.Length; s += d6t + "        ";
            MessageBox.Show(s);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //QuickBM_Test();
            //MessageBox.Show(DB.AppendNumToFN("c:\\windows\\system32\\mspaint.exe"));

            /*FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog(); if (fbd.SelectedPath != "")
            {
                string sPath = fbd.SelectedPath;
                string[] sFiles = GetPaths(sPath, true);
                sFiles = FilterArray(sFiles, DB.saAllowedTypes,
                    asIntArray(2, DB.saAllowedTypes.Length), false);

                string[] sRet = new string[DB.saAllowedTypes.Length];
                for (int a = 0; a < sFiles.Length; a++)
                {
                    byte[] bf = new byte[20];
                    string sLoc = sFiles[a].Substring(sFiles[a].IndexOf("\\") + 1);
                    System.IO.FileStream fs = new System.IO.FileStream(sFiles[a],
                        System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    //fs.Seek(-20, System.IO.SeekOrigin.End);
                    fs.Read(bf, 0, 20); fs.Close(); fs.Dispose();
                    string sInf = ""; string sByt = "";
                    foreach (byte bfe in bf)
                    {
                        if (bfe == 0) sInf += "/";
                        else sInf += (char)bfe + "";
                        sByt += bfe.ToString("X2") + " ";
                    }
                    sByt = sByt.Replace("\r", "").Replace("\n", "").Replace("\t", "");
                    while (sByt.Length < 20) sByt = sByt + " ";

                    for (int b = 0; b < sRet.Length; b++)
                        if (sFiles[a].EndsWith(DB.saAllowedTypes[b]))
                        {
                            sRet[b] += sByt + "  -   " + sInf + "   -   " + sLoc + "\r\n";
                        }
                }

                string sRetV = "";
                for (int a = 0; a < sRet.Length; a++)
                    sRetV += sRet[a];
                Clipboard.Clear();
                Clipboard.SetText(sRetV);
                MessageBox.Show("LOLDONGS");
            }*/

            /*int iEmTagAction = Convert.ToInt32(InputBox.Show(
                "1. Read tags" + "\r\n" +
                "2. Remove tags" + "\r\n" +
                "3. Remove meta" + "\r\n" +
                "4. Write tags"
                ).Text);
            if (iEmTagAction == 1)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog(); string sPath = ofd.FileName;
                string[] sTags = emTags.TagsRead(sPath);
                MessageBox.Show(
                    "Hash: -" + sTags[0] + "-\r\n" +
                    "Name: -" + sTags[1] + "-\r\n" +
                    "Cmnt: -" + sTags[2] + "-\r\n" +
                    "tGen: -" + sTags[3] + "-\r\n" +
                    "tSrc: -" + sTags[4] + "-\r\n" +
                    "tChr: -" + sTags[5] + "-\r\n" +
                    "tArt: -" + sTags[6] + "-");
            }
            if (iEmTagAction == 2)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog(); string sPath = ofd.FileName;
                emTags.TagsClear(sPath, false);
                MessageBox.Show("Done.");
            }
            if (iEmTagAction == 3)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog(); string sPath = ofd.FileName;
                emTags.TagsClear(sPath, true);
                MessageBox.Show("Done.");
            }
            if (iEmTagAction == 4)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog(); string sPath = ofd.FileName;
                emTags.TagsWrite(sPath, true, new string[6]);
                MessageBox.Show("Done.");
            }*/

            cb.sAppVer = Application.ProductVersion.Substring(
                0, Application.ProductVersion.LastIndexOf("."));
            this.Text += cb.sAppVer;

            frmWait.bVisible = true;
            frmWait.sHeader = "pImgDB - Loading";
            frmWait.sMain = "Loading...";
            frmWait.sFooter = "www.praetox.com";
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
            pnaPanelsMain = new Panel[] { pnThumbs, pnDisplay, pnSearchInit, pnMassEdit, pnImport, pnUpload, pnWPChanger, pnFBindsEx, pnWebServ, pnColors };
            pnaPanelsControl = new Panel[] { pnGeneral, pnQEdit, pnHotTagger, pnTagbar, pnQTagovCP, pnTagBase, pnQFBind };
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
            cb.sAppPath = Application.StartupPath.Replace("\\", "/");
            if (!cb.sAppPath.EndsWith("/")) cb.sAppPath += "/";
            QEdit_ddEnterAction.SelectedIndex = 1;
            HotTagger_ddAction.SelectedIndex = 1;

            frmWait.sMain = "Polling for SQLite"; frmWait.bInstant = true;
            while (frmWait.bInstant) Application.DoEvents();
            if (!System.IO.File.Exists(cb.sAppPath + "System.Data.SQLite.dll"))
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

            frmWait.sMain = "Configuring GUI (pt.2)"; frmWait.bInstant = true;
            while (frmWait.bInstant) Application.DoEvents();
            string[] saFolders = System.IO.Directory.GetDirectories(cb.sAppPath, "db_*");
            for (int a = 0; a < saFolders.Length; a++)
            {
                General_cbDatabase.Items.Add((a + 1) + " - " + saFolders[a].Substring(cb.sAppPath.Length + 3));
            }
            string sLastDB_Name = Program.Reg_Access("Last DB name", "");
            if (sLastDB_Name != "")
            {
                General_cbDatabase.SelectedIndex = -1;
                for (int a = 0; a < saFolders.Length; a++)
                {
                    if ((saFolders[a].Substring(cb.sAppPath.Length + 3)) == sLastDB_Name)
                        General_cbDatabase.SelectedIndex = a;
                }
            }
            string sThumbCount = Program.Reg_Access("Thumbnail rows", "");
            string sResMultip = Program.Reg_Access("Res multiplier", "");
            string sQuickThumbsRes = Program.Reg_Access("Quick thumbs res", "");
            string sQuickThumbsKill = Program.Reg_Access("Quick thumbs kill", "");
            if (sThumbCount != "") General_txtThumbs.Text = sThumbCount;
            if (sResMultip != "") General_txtResMul.Text = sResMultip;
            if (sQuickThumbsRes == "no") General_chkQThumbs.Checked = false;
            else if (sQuickThumbsRes != "")
            {
                General_txtQThumbs.Text = sQuickThumbsRes;
                General_txtQThumbs_TextChanged(new object(), new EventArgs());
            }
            if (sQuickThumbsKill != "")
            {
                General_txtQThumbsDel.Text = sQuickThumbsKill;
                General_txtQThumbsDel_TextChanged(new object(), new EventArgs());
            }
            General_chkQThumbs_CheckedChanged(new object(), new EventArgs());
            FBindEx_cmdFromImported.BackColor = clr_BtnOff;
            FBindEx_cmdFromFolder.BackColor = clr_BtnOff;

            try
            {
                string[] sDesigns = System.IO.Directory.GetFiles(cb.sAppPath + "z_colors");
                foreach (string sDesign in sDesigns) Design_ddPresets.Items.Add
                    (sDesign.Substring((cb.sAppPath + "z_colors/").Length));
            }
            catch { }

            frmWait.sMain = "Loading resources"; frmWait.bInstant = true;
            while (frmWait.bInstant) Application.DoEvents();
            System.IO.Stream streamBMP = System.Reflection.Assembly.
                GetExecutingAssembly().GetManifestResourceStream(
                "pImgDB.Graphics.Denied.png");
            Bitmap bTmp = (Bitmap)Bitmap.FromStream(streamBMP);
            bDenied = (Bitmap)bTmp.Clone(); bTmp.Dispose();
            streamBMP.Close(); streamBMP.Dispose();
            WPChanger_cmdLoad_Click(new object(), new EventArgs());
            Upload_cmdPass_Click(new object(), new EventArgs());

            frmWait.sMain = "Creating helpers"; frmWait.bInstant = true;
            while (frmWait.bInstant) Application.DoEvents();
            ipMyHandle = this.Handle;
            bwLoadThumbs = new BackgroundWorker();
            bwPrevDisp = new BackgroundWorker();
            bwNextDisp = new BackgroundWorker();
            bwLoadThumbs.DoWork += new DoWorkEventHandler(bwLoadThumbs_DoWork);
            bwPrevDisp.DoWork += new DoWorkEventHandler(bwPrevDisp_DoWork);
            bwNextDisp.DoWork += new DoWorkEventHandler(bwNextDisp_DoWork);
            bwLoadThumbs.WorkerSupportsCancellation = true;
            if (!System.IO.Directory.Exists(cb.sAppPath + "z_tmp"))
                System.IO.Directory.CreateDirectory(cb.sAppPath + "z_tmp");
            niTray.Icon = this.Icon;
            wfZoom.Show();
            wfTip.Show();

            frmWait.sMain = "Checking for updates"; frmWait.bInstant = true;
            while (frmWait.bInstant) Application.DoEvents();
            try
            {
                bool bUpdateCheckOK = true;
                WebReq WR = new WebReq();
                WR.Request(PrgDomain + "pImgDB_version.php?cv=" + cb.sAppVer);
                long lUpdateStart = Tick();
                while (!WR.isReady && bUpdateCheckOK)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(10);
                    if (Tick() > lUpdateStart + 5000)
                        bUpdateCheckOK = false;
                }
                string wrh = WR.sResponse;
                if (!bUpdateCheckOK) throw new Exception("wat");
                if (wrh.Contains("<WebReq_Error>")) throw new Exception("wat");

                if (!wrh.Contains("<VERSION>" + cb.sAppVer + "</VERSION>"))
                {
                    string sNewVer = Split(Split(wrh, "<VERSION>")[1], "</VERSION>")[0];
                    bool GetUpdate = (DialogResult.Yes == MessageBox.Show(
                        "A new version (" + sNewVer + ") is available. Update?",
                        "pImgDB Updater", MessageBoxButtons.YesNo));
                    if (GetUpdate)
                    {
                        string UpdateLink = new System.Net.WebClient().DownloadString(
                            ToxDomain + "inf/pImgDB_link.html").Split('%')[1];
                        System.Diagnostics.Process.Start(UpdateLink + "?cv=" + cb.sAppVer);
                        Application.Exit();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Couldn't check for updates.", "pImgDB Updater");
            }

            frmWait.sMain = "Finalizing"; frmWait.bInstant = true;
            while (frmWait.bInstant) Application.DoEvents();
            pThumb = new PBoxArray(this);
            pnThumb = new PanelArray(this);
            tThumb = new TBoxArray(this);
            lbTagbar = new LabelArray(this);
            tThumb.Remove(); lbTagbar.Remove();
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
            for (int a = 5; a >= 0; a--)
            {
                this.Opacity = (double)a / 5;
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
            this.Hide(); Application.DoEvents();
            this.Opacity = (double)1;
        }

        public void HandleEx_ImageNotFound(int iIndex)
        {
            if (iHandleEx_ImageNotFound == 0)
            {
                string ret = InputBox.Show(
                    "One of the images could not be found." + "\r\n\r\n" +
                    "This is likely the result of manual moving or" + "\r\n" +
                    "deletion of images within the database folders." + "\r\n" +
                    "While this is a horrible thing to do, I -should- be able to fix it." + "\r\n" +
                    "Let's see what happens, shall we? ^_^" + "\r\n\r\n" +
                    "Please choose one of the options below." + "\r\n" +
                    "Do so by typing its number and hitting enter." + "\r\n" +
                    "[1] Ignore these errors and continue as if nothing happened" + "\r\n" +
                    "[2] Remove all traces of images from database (in next save)" + "\r\n" +
                    "[3] Delete all missing images in entire database" + "\r\n" +
                    "[4] OMG WAT? EMERGENCY SHUTDOWN!", "Oh snap.", "2").Text;

                try { iHandleEx_ImageNotFound = Convert.ToInt32(ret); }
                catch
                {
                    MessageBox.Show("Could you repeat that, please?",
                        "You are doing it wrong.", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }

                HandleEx_ImageNotFound(iIndex); return;
            }
            if (iHandleEx_ImageNotFound == 1) // Ignore
            {
                return;
            }
            if (iHandleEx_ImageNotFound == 2) // Remove from db
            {
                idImages[iIndex].bMod = true;
                idImages[iIndex].bDel = true;
                idImages[iIndex].bSel = false;
            }
            if (iHandleEx_ImageNotFound == 3) // Deep scan
            {
                frmWait.sHeader = "Fixing database";
                frmWait.sMain = "Preparing";
                frmWait.sFooter = "Doing full scan";
                frmWait.bVisible = true;
                while (!frmWait.bActive)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(1);
                }

                idImages = DB.Search(new SearchPrm(), 1);
                frmWait.sMain = "Verifying images"; frmWait.sFooter = "Image 0 of " + idImages.Length;
                frmWait.bInstant = true; while (frmWait.bInstant) Application.DoEvents();

                int iDelCnt = 0;
                using (SQLiteTransaction dbTrs = DB.Data.BeginTransaction())
                {
                    using (SQLiteCommand DBc = DB.Data.CreateCommand())
                    {
                        DBc.CommandText = "DELETE FROM 'images' WHERE hash = ?";
                        SQLiteParameter dbParam = DBc.CreateParameter();
                        DBc.Parameters.Add(dbParam);

                        for (int a = 0; a < idImages.Length; a++)
                        {
                            frmWait.sFooter = "Image " + a + " of " + idImages.Length;
                            if (a.ToString().EndsWith("0")) Application.DoEvents();
                            if (!System.IO.File.Exists(cb.sAppPath + DB.Path + idImages[a].sPath))
                            {
                                dbParam.Value = idImages[a].sHash;
                                iDelCnt += DBc.ExecuteNonQuery();
                            }
                        }
                    }
                    dbTrs.Commit();
                }

                frmWait.sHeader = "Click this window or press space to close";
                frmWait.sMain = "All done";
                frmWait.sFooter = "Removed " + iDelCnt + " images";
                frmWait.bCloseAtClick = true;
                wfWait.Focus();
            }
            if (iHandleEx_ImageNotFound == 4) // Shutdown
            {
                niTray.Visible = false;
                System.Diagnostics.Process.
                    GetCurrentProcess().Kill();
            }
        }

        private Point CalcThumbSize(int ThumbX, int ThumbY)
        {
            if (ThumbX == 0) ThumbX = (int)Math.Round((((double)ThumbY / 10) * 16), 0); //Thumb W (16/10)
            if (ThumbY == 0) ThumbY = (int)Math.Round((((double)ThumbX / 16) * 10), 0); //Thumb H (16/10)
            return new Point(ThumbX, ThumbY);
        }
        private Rectangle CalcThumbCountXY(Point szCont, int ThumbX, int ThumbY)
        {
            if (ThumbX == 0 && ThumbY == 0) return new Rectangle(0, 0, 0, 0); //O SHI-
            Point ptThumb = CalcThumbSize(ThumbX, ThumbY); //Get thumb size
            if (bQTagovEnabled) //Tags overview
            {
                ptThumb.X += 100; //100px textf w/no spacing)
                if (ptThumb.Y < (17 * sQTagovFields.Length)) //26px textf (17px w/no spacing)
                    ptThumb.Y = (17 * sQTagovFields.Length);
            }
            int ThPanX = ptThumb.X + 4 + 2; int ThPanY = ptThumb.Y + 4 + 2; //Panel W/H
            int ThTotX = ThPanX + 2; int ThTotY = ThPanY + 2; //Panel+Dist W/H
            szCont.X -= 4 + 4; szCont.Y -= 4 + 4; //Offsets W/H
            //szCont.X += 2; szCont.Y += 2; //Remove last dist
            //szCont.X -= 2; szCont.Y -= 2; //Subtract frame

            Point pt = new Point(
                (int)Math.Floor((double)szCont.X / (double)ThTotX),
                (int)Math.Floor((double)szCont.Y / (double)ThTotY));
            Rectangle ret = new Rectangle(pt.X, pt.Y,
                (int)Math.Floor((double)pt.X * ThTotX),
                (int)Math.Floor((double)pt.Y * ThTotY));
            if (bQTagovEnabled) if (ret.X > pQTagovEnum.X) ret.X = pQTagovEnum.X;
            if (bQTagovEnabled) if (ret.Y > pQTagovEnum.Y) ret.Y = pQTagovEnum.Y;
            return ret;
        }
        private Rectangle CalcThumbMaxSize(Point szCont, int Thumbs, int ThumbsY)
        {
            int[,] iD = new int[
                Math.Max(szCont.X, szCont.Y), 6];
            // cntX, cntY, cntT, covT
            int iiD = 0;
            for (int a = 0; a < szCont.X; a++)
            {
                Point ptRes = CalcThumbSize(a, 0);
                iD[iiD, 0] = ptRes.X; iD[iiD, 1] = ptRes.Y;
                Rectangle rcPt = CalcThumbCountXY
                    (szCont, ptRes.X, ptRes.Y);
                if (ThumbsY > 0)
                    if (rcPt.Y > ThumbsY)
                        rcPt.Y = ThumbsY;

                if (rcPt.X != iD[iiD, 2] ||
                    rcPt.Y != iD[iiD, 3]) iiD++;
                iD[iiD, 2] = rcPt.X; iD[iiD, 3] = rcPt.Y;
                iD[iiD, 4] = rcPt.X * rcPt.Y;
                iD[iiD, 5] = rcPt.Width * rcPt.Height;
            }
            int iThCur = -1;
            int iThDst = 9001;
            for (int a = 0; a <= iiD; a++)
            {
                int iCrDst = Math.Abs(iD[a, 4] - Thumbs);
                if (iCrDst < iThDst) { iThCur = a; iThDst = iCrDst; }
            }

            if (iD[iThCur - 1, 5] > iD[iThCur, 5]) iThCur--;
            //else if (iD[iThCur + 1, 5] > iD[iThCur, 5]) iThCur++;
            return new Rectangle(iD[iThCur, 0], iD[iThCur, 1], iD[iThCur, 2], iD[iThCur, 3]);
        }
        private Point CalcThumbMaxSize(Point szCont, int ThumbsY, IntPtr wat)
        {
            int ThumbX = 0, ThumbY = 0;
            Point ptInit = new Point(1, 1);
            Rectangle rcTmp = CalcThumbCountXY(szCont, 1, 1);
            ptInit.X = rcTmp.X; ptInit.Y = rcTmp.Y;
            for (int x = 1; x < 32768; x++)
            {
                Point ptThSize = CalcThumbSize(x, 0); //Get thumb size
                Rectangle ptThCount = CalcThumbCountXY(szCont, ptThSize.X, ptThSize.Y); //Get thumb count
                if (ptThCount.X < 1) break; //No thumbs at all
                if (ptThCount.Y < ThumbsY) break; //Below threshold
                ThumbX = ptThSize.X; ThumbY = ptThSize.Y; //Good to go
            }
            return new Point(ThumbX, ThumbY);
        }
        private void RedrawThumbnails(bool bRecreateControls)
        {
            if (bwLoadThumbs.IsBusy)
            {
                bwLoadThumbs.CancelAsync();
                while (bwLoadThumbs.IsBusy)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(10);
                }
            }

            this.SuspendLayout();

            if (bRecreateControls)
            {
                for (int a = 0; a < pThumb.Count; a++)
                {
                    pThumb[a].Visible = false;
                    pnThumb[a].Visible = false;
                    this.Controls.Add(pThumb[a]);
                    this.Controls.Add(pnThumb[a]);
                }
                for (int a = 0; a < tThumb.Count; a++)
                {
                    tThumb[a].Visible = false;
                    this.Controls.Add(tThumb[a]);
                }
                while (pThumb.Count > 0) pThumb.Remove();
                while (pnThumb.Count > 0) pnThumb.Remove();
                while (tThumb.Count > 0) tThumb.Remove();
            }

            int iThumbs = iThumbCnt;
            if (iThumbs > idImages.Length && idImages.Length > 0)
                iThumbs = idImages.Length;

            Point szCont = (Point)pnThumbs.Size; //szCont.X -= (20);
            Rectangle rcThumbInfo = new Rectangle(-1, -1, -1, -1);
            if (!bQTagovEnabled) rcThumbInfo = CalcThumbMaxSize(szCont, iThumbs, 0);
            if (bQTagovEnabled) rcThumbInfo = CalcThumbMaxSize
                (szCont, pQTagovEnum.X * pQTagovEnum.Y, pQTagovEnum.Y);
            //Rectangle ptThumbCount = CalcThumbCountXY(szCont, ptThumbSize.X, ptThumbSize.Y);

            ptThumbSize = new Point(rcThumbInfo.X, rcThumbInfo.Y);
            Point ptThumbCount = new Point(rcThumbInfo.Width, rcThumbInfo.Height);
            Point ptThumbPSize = new Point(ptThumbSize.X + 4 + 2, ptThumbSize.Y + 4 + 2);
            Point ptInftxPSize = new Point(0, 0);
            if (bQTagovEnabled)
            {
                //ptThumbPSize.X += 2;
                ptInftxPSize = new Point
                    ((szCont.X / ptThumbCount.X) -
                    (ptThumbPSize.X + 4), 17);
            }
            int iSpentX = (ptThumbCount.X * (ptThumbPSize.X + ptInftxPSize.X + 2));
            int iSpentY = (ptThumbCount.Y * (ptThumbPSize.Y + 2));
            Point ptSkew = new Point(
                (szCont.X - iSpentX) / 2,
                (szCont.Y - iSpentY) / 2);

            btDenied.Dispose();
            btDenied = cb.ResizeBitmap(bDenied,
                ptThumbSize.X, ptThumbSize.Y, true, 3);

            if (!bRecreateControls)
                if (ptThumbCount.X * ptThumbCount.Y != pnThumb.Count)
                {
                    RedrawThumbnails(true);
                    SetThumbPageRanges(thPages[1].idRange.X);
                    return;
                }

            for (int y = 0; y < ptThumbCount.Y; y++)
            {
                for (int x = 0; x < ptThumbCount.X; x++)
                {
                    int iThis = (y * (ptThumbCount.X)) + x;
                    if (bRecreateControls) pnThumb.NewPanel();
                    pnThumbs.Controls.Add(pnThumb[iThis]);
                    pnThumb[iThis].Size = new Size(
                        ptThumbPSize.X + ptInftxPSize.X,
                        ptThumbPSize.Y);
                    pnThumb[iThis].Location = new Point(
                        ptSkew.X + (x * (ptThumbPSize.X + ptInftxPSize.X + 2)),
                        ptSkew.Y + (y * (ptThumbPSize.Y + 2)));
                    pnThumb[iThis].BorderStyle = BorderStyle.FixedSingle;
                    pnThumb[iThis].BackColor = clr_pnThumb[0];
                    if (bRecreateControls) pThumb.NewPBox();
                    pnThumb[iThis].Controls.Add(pThumb[iThis]);
                    pThumb[iThis].Size = (Size)ptThumbSize;
                    pThumb[iThis].Location = new Point(2, 2);
                    pThumb[iThis].SizeMode = PictureBoxSizeMode.Zoom;
                    pThumb[iThis].BackColor = clr_pnThumb[0];
                    pThumb[iThis].BorderStyle = BorderStyle.FixedSingle;
                    if (bQTagovEnabled)
                    {
                        //int iTTc = sQTagovFields.Length - 1;
                        for (int a = 0; a < sQTagovFields.Length; a++)
                        {
                            string sData = "";
                            int iThisID = thPages[1].idRange.X + iThis;
                            if (iThisID < idImages.Length)
                            {
                                if (sQTagovFields[a] == "iN") sData = idImages[iThisID].sName;
                                if (sQTagovFields[a] == "iC") sData = idImages[iThisID].sCmnt;
                                if (sQTagovFields[a] == "iP") sData = idImages[iThisID].sPath;
                                if (sQTagovFields[a] == "iR") sData = idImages[iThisID].iRate + "";
                                if (sQTagovFields[a] == "TG") sData = idImages[iThisID].sTGen.Replace(",", ", ");
                                if (sQTagovFields[a] == "TS") sData = idImages[iThisID].sTSrc.Replace(",", ", ");
                                if (sQTagovFields[a] == "TC") sData = idImages[iThisID].sTChr.Replace(",", ", ");
                                if (sQTagovFields[a] == "TA") sData = idImages[iThisID].sTArt.Replace(",", ", ");
                            }
                            int iTThis = (iThis * sQTagovFields.Length) + a;
                            if (bRecreateControls || tThumb.Count <= iTThis)
                            {
                                tThumb.NewTBox();
                                tThumb[iTThis].KeyUp += new KeyEventHandler(tThumb_KeyUp);
                                tThumb[iTThis].TextChanged += new EventHandler(tThumb_TextChanged);
                            }
                            pnThumb[iThis].Controls.Add(tThumb[iTThis]);
                            tThumb[iTThis].Size = new Size(
                                ptInftxPSize.X - 2,
                                ptInftxPSize.Y + 1);
                            tThumb[iTThis].Location = new Point(
                                ptThumbPSize.X - 2, 2 +
                                ((ptInftxPSize.Y) * a));
                            tThumb[iTThis].BackColor = clr_pnThumb[0];
                            tThumb[iTThis].ForeColor = Color.White;
                            tThumb[iTThis].BorderStyle = BorderStyle.None;
                            //tThumb[iTThis].Tag = iThisID + "." + sQTagovFields[a];
                            tThumb[iTThis].Text = sQTagovFields[a] + ":  " + sData;
                        }
                    }
                }
            }
            if (thPages[1].bImages != null && thPages[1].idRange.X != -1)
            {
                for (int a = 0; a < pThumb.Count; a++)
                {
                    if (a >= thPages[1].bImages.Length) break;
                    int iThisID = thPages[1].idRange.X + a;
                    if (iThisID >= idImages.Length) break;
                    if (!idImages[iThisID].bDel)
                        if (thPages[1].bImages[a] != null)
                            pThumb[a].Image = (Image)thPages[1].bImages[a];
                        else pThumb[a].Image = (Image)bDummy;
                    else pThumb[a].Image = btDenied as Image;
                    if (idImages[iThisID].bSel)
                        pnThumb[a].BackColor = clr_pnThumb[1];
                    else pnThumb[a].BackColor = clr_pnThumb[0];
                }
            }
            RecountStatistics();
            this.ResumeLayout();
        }

        private void tThumb_TextChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void tThumb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int sQLen = sQTagovFields.Length;
                int iTThis = tThumb.KUi.iIndex;
                int iThisIDL = iTThis / sQLen;
                int iThisIDT = iTThis % sQLen;
                int iThisID = iThisIDL +
                    thPages[1].idRange.X;

                int iParam = -1;
                int iAction = QTagovCP_ddAction.SelectedIndex + 1;
                if (sQTagovFields[iThisIDT] == "iN") iParam = 1;
                if (sQTagovFields[iThisIDT] == "iC") iParam = 2;
                if (sQTagovFields[iThisIDT] == "iR") iParam = 3;
                if (sQTagovFields[iThisIDT] == "TG") iParam = 4;
                if (sQTagovFields[iThisIDT] == "TS") iParam = 5;
                if (sQTagovFields[iThisIDT] == "TC") iParam = 6;
                if (sQTagovFields[iThisIDT] == "TA") iParam = 7;
                string sVar = tThumb[iTThis].Text;
                string sPref = sQTagovFields[iThisIDT] + ":  ";
                if (sVar.StartsWith(sPref))
                    sVar = sVar.Substring(sPref.Length);
                tThumb[iTThis].Text = sPref + sVar;

                if (iParam != -1)
                    EditSingleParameter(iThisID, iParam, sVar, iAction);

                if (iTThis < tThumb.Count - 1)
                {
                    if (tThumb[iTThis + 1].Text.StartsWith(sQTagovFields[(iTThis + 1) % sQLen] + ":  "))
                        tThumb[iTThis + 1].Text = tThumb[iTThis + 1].Text.Substring(5);
                    tThumb[iTThis + 1].Focus();
                    tThumb[iTThis + 1].SelectAll();
                }
            }
            //throw new NotImplementedException();
        }
        private void ReloadThumbnails(bool bFullReload)
        {
            //  Stop preloading images in the background, if applicable
            if (bwLoadThumbs.IsBusy)
            {
                bwLoadThumbs.CancelAsync();
                while (bwLoadThumbs.IsBusy)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(10);
                }
            }

            //  Jump ship if there's no images to display
            if (idImages.Length == 0) return;

            //  Determine (highest) amount of thumbs (normally even)
            int iLev1 = thPages.Length;
            int iLev2 = -1; int iTotC = 0;
            for (int a = 0; a < iLev1; a++)
            {
                iTotC += thPages[a].bImages.Length;
                if (iLev2 < thPages[a].bImages.Length)
                    iLev2 = thPages[a].bImages.Length;
            }

            //  Default to full reloading
            bool[,] bDoThis = new bool[iLev1, pThumb.Count];
            for (int a = 0; a < iLev1; a++)
                for (int b = 0; b < pThumb.Count; b++)
                    bDoThis[a, b] = true;

            //  Clear all displays (avoid runtime crash)
            for (int a = 0; a < pThumb.Count; a++)
                pThumb[a].Image = bDummy as Image;

            //  If any page is blank, force full reload
            //for (int iPage = 0; iPage < thPages.Length; iPage++)
            //    if (thPages[iPage].bImages == null)
            //        bFullReload = true;

            //  Create buffer with all previous images
            int iBufP = 0;
            Bitmap[] bImages = new Bitmap[iTotC];
            string[] sHashes = new string[iTotC];
            for (int iPage = 0; iPage < thPages.Length; iPage++)
                for (int a = 0; a < thPages[iPage].bImages.Length; a++)
                    if (thPages[iPage].bImages[a] != null)
                    {
                        bImages[iBufP] = (Bitmap)thPages[iPage].bImages[a].Clone();
                        sHashes[iBufP] = thPages[iPage].sHashes[a];
                        thPages[iPage].bImages[a].Dispose();
                        iBufP++;
                    }

            //  Create new bitmap and hashes arrays
            for (int iPage = 0; iPage < thPages.Length; iPage++)
            {
                thPages[iPage].bImages = new Bitmap[pThumb.Count];
                thPages[iPage].sHashes = new string[pThumb.Count];
            }

            if (!bFullReload)
            {
                //  Iterate temp. cache for usable images
                for (int a = 0; a < thPages.Length; a++)
                    for (int b = 0; b < thPages[a].bImages.Length; b++)
                    {
                        int iElm = thPages[a].idRange.X + b;
                        if (iElm <= thPages[a].idRange.Y)
                        {
                            string sHash = idImages[iElm].sHash;
                            for (int c = 0; c < bImages.Length; c++)
                                if (sHashes[c] == sHash)
                                {
                                    thPages[a].bImages[b] = bImages[c].Clone() as Bitmap;
                                    thPages[a].sHashes[b] = sHashes[c];
                                    bDoThis[a, b] = false;
                                }
                        }
                    }
            }

            //  Dispose of the buffer
            for (int a = 0; a < bImages.Length; a++)
                if (bImages[a] != null)
                    bImages[a].Dispose();

            /*/  Extend / create bitmap arrays (if necessary)
            for (int iPage = 0; iPage < thPages.Length; iPage++)
                if (thPages[iPage].bImages.Length < pThumb.Count)
                {
                    Bitmap[] bTmp = new Bitmap[pThumb.Count];
                    for (int a = 0; a < thPages[iPage].bImages.Length; a++)
                        if (thPages[iPage].bImages[a] != null)
                        {
                            bTmp[a] = (Bitmap)thPages[iPage].bImages[a].Clone();
                            thPages[iPage].bImages[a].Dispose();
                        }

                    thPages[iPage].bImages = new Bitmap[pThumb.Count];
                    for (int a = 0; a < thPages[iPage].bImages.Length; a++)
                        if (thPages[iPage].bImages[a] != null)
                        {
                            thPages[iPage].bImages[a] =
                                (Bitmap)bTmp[a].Clone();
                            bTmp[a].Dispose();
                        }
                }

            //  Extend / create hash arrays (if necessary)
            for (int iPage = 0; iPage < thPages.Length; iPage++)
                if (thPages[iPage].sHashes.Length < pThumb.Count)
                {
                    string[] sTmp = new string[pThumb.Count];
                    thPages[iPage].sHashes.CopyTo(sTmp, 0);
                    thPages[iPage].sHashes = new string[pThumb.Count];
                    sTmp.CopyTo(thPages[iPage].sHashes, 0);
//                    for (int a = 0; a < thPages[iPage].sHashes.Length; a++)
//                        if (thPages[iPage].sHashes[a] == null)
//                            thPages[iPage].sHashes[a] = "DEADBEEF";
                }*/

            /*/  Create new bitmap arrays
            for (int iPage = 0; iPage < thPages.Length; iPage++)
                for (int a = 0; a < thPages[iPage].bImages.Length; a++)
                    if (thPages[iPage].bImages[a] != null)
                        thPages[iPage].bImages[a].Dispose();

            //  Extend / create hash arrays (if necessary)
            int iHLoc = 0;
            for (int iPage = 0; iPage < thPages.Length; iPage++)
                iHLoc += thPages[iPage].sHashes.Length;
            string[] sHaStrm = new string[iHLoc];
            iHLoc = 0;
            for (int iPage = 0; iPage < thPages.Length; iPage++)
            {
                thPages[iPage].sHashes.CopyTo(sHaStrm, iHLoc);
                iHLoc += thPages[iPage].sHashes.Length;
            }
            iHLoc = 0;
            for (int iPage = 0; iPage < thPages.Length; iPage++)
            {
                thPages[iPage].sHashes = new string[pThumb.Count];
                for (int a = 0; a < thPages[iPage].sHashes.Length; a++)
                    if (iHLoc < sHaStrm.Length)
                        thPages[iPage].sHashes[a] = sHaStrm[iHLoc];
                    else thPages[iPage].sHashes[a] = "DEADBEEF";
            }*/

            /*if (!bFullReload)
            {
                int iLoc = 0;
                //  Create temp. cache with images and old hashes
                Bitmap[] bImages = new Bitmap[iLev1 * iLev2];
                string[] sHashes = new string[iLev1 * iLev2];

                for (int a = 0; a < thPages.Length; a++)
                    for (int b = 0; b < thPages[a].bImages.Length; b++)
                    {
                        if (thPages[a].bImages[b] != null)
                        {
                            //  Populate temp. cache
                            bImages[iLoc] = (Bitmap)thPages[a].bImages[b].Clone();
                            sHashes[iLoc] = (string)thPages[a].sHashes[b].Clone();
                            thPages[a].bImages[b].Dispose();
                            thPages[a].bImages[b] = null;

                            //  Assign new hashes to public cache
                            int iElm = thPages[a].idRange.X + b;
                            if (iElm <= thPages[a].idRange.Y)
                                thPages[a].sHashes[b] = idImages
                                    [thPages[a].idRange.X + b].sHash;
//                            else thPages[a].sHashes[b] = "DEADBEEF";
                            iLoc++;
                        }
                    }

                for (int iPage = 0; iPage < thPages.Length; iPage++)
                    if (thPages[iPage].sHashes.Length < pThumb.Count)
                    {
                        string[] sTmp = new string[pThumb.Count];
                        thPages[iPage].sHashes.CopyTo(sTmp, 0);
                        thPages[iPage].sHashes = new string[pThumb.Count];
                        sTmp.CopyTo(thPages[iPage].sHashes, 0);
//                        for (int a = 0; a < thPages[iPage].sHashes.Length; a++)
//                            if (thPages[iPage].sHashes[a] == null)
//                                thPages[iPage].sHashes[a] = "DEADBEEF";
                    }

                //  Iterate temp. cache for usable images
                for (int a = 0; a < thPages.Length; a++)
                    for (int b = 0; b < thPages[a].bImages.Length; b++)
                    {
                        int iElm = thPages[a].idRange.X + b;
                        if (iElm <= thPages[a].idRange.Y)
                        {
                            string sHash = idImages[iElm].sHash;
                            for (int c = 0; c < iLev1 * iLev2; c++)
                                if (sHashes[c] == sHash)
                                {
                                    thPages[a].bImages[b] = bImages[c].Clone() as Bitmap;
                                    thPages[a].sHashes[b] = sHashes[c].Clone() as string;
                                    bDoThis[a, b] = false;
                                }
                        }
                    }

                //  Remove thumbs from temp. cache
                for (int a = 0; a < iLev1 * iLev2; a++)
                    if (bImages[a] != null)
                        bImages[a].Dispose();

                iLev1++;
            }
            else
            {
                //  Dispose of the old images
                for (int iPage = 0; iPage < thPages.Length; iPage++)
                    if (thPages[iPage].bImages != null)
                        for (int a = 0; a < thPages[iPage].bImages.Length; a++)
                            if (thPages[iPage].bImages[a] != null)
                                thPages[iPage].bImages[a].Dispose();

                //  Create new bitmap arrays
                for (int iPage = 0; iPage < thPages.Length; iPage++)
                    thPages[iPage].bImages = new Bitmap[pThumb.Count];
            }*/

            //  Write correct hashes to global thumbnail cache
            for (int a = 0; a < thPages.Length; a++)
            {
                int iElmCnt = thPages[a].bImages.Length;
                thPages[a].sHashes = new string[iElmCnt];
                for (int b = 0; b < iElmCnt; b++)
                {
                    int iElm = thPages[a].idRange.X + b;
                    if (iElm <= thPages[a].idRange.Y)
                        thPages[a].sHashes[b] = idImages[iElm].sHash;
                }
            }

            //  Start preloading
            bwLoadThumbs.RunWorkerAsync(bDoThis);
        }
        private void bwLoadThumbs_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int iPage = -1;
                bool[,] bDoThis = (bool[,])e.Argument;
                while (true)
                {
                    if (iPage == -1) iPage = 1;
                    else if (iPage == 1) iPage = 2;
                    else if (iPage == 2) iPage = 0;
                    else if (iPage == 0) break;

                    int iIDCnt = thPages[iPage].idRange.Y - thPages[iPage].idRange.X;
                    for (int a = 0; a < thPages[iPage].bImages.Length; a++)
                    {
                        if (bwLoadThumbs.CancellationPending)
                        {
                            if (a > 0)
                                for (int iK = a; iK < thPages[iPage].bImages.Length; iK++)
                                    if (bDoThis[iPage, iK])
                                    {
                                        if (thPages[iPage].bImages != null)
                                            if (thPages[iPage].bImages[iK] != null)
                                            {
                                                thPages[iPage].bImages[iK].Dispose();
                                                thPages[iPage].bImages[iK] = null;
                                            }

                                        if (thPages[iPage].sHashes != null)
                                            if (thPages[iPage].sHashes[iK] != null)
                                                thPages[iPage].sHashes[iK] = "DEADFEED";
                                    }
                            break;
                        }
                        if (a <= iIDCnt && thPages[iPage].idRange.Y != -1)
                        {
                            int iThisID = thPages[iPage].idRange.X + a;
                            if (!idImages[iThisID].bDel)
                            {
                                if (bDoThis[iPage, a])
                                {

                                    while (iThWrkCnt >= iThWrkMax)
                                        System.Threading.Thread.Sleep(10);

                                    iThWrkTrg = iThisID;
                                    Thread thLoadThumb = new Thread
                                        (new ThreadStart(stLoadThumb));
                                }
                            }
                            else
                            {
                                thPages[iPage].bImages[a] = (Bitmap)btDenied.Clone();
                            }
                            if (iPage == 1)
                            {
                                pThumb[a].Image = thPages[iPage].bImages[a] as Image;
                                Application.DoEvents();
                            }
                        }
                    }
                }
            }
            catch
            {
                frmTip.ShowMsg(true, "Exception 0x1337");
            }
            //RecountStatistics();
        }
        private void stLoadThumb()
        {
            int iThisID = iThWrkTrg; iThWrkTrg = -1;
            if (iThisID == -1) throw new Exception();
            string sImPath = cb.sAppPath + DB.Path + idImages[iThisID].sPath;
            string sThPath = cb.sAppPath + DB.Path + "_th/";
            if (bQThumbs)
            {
                System.IO.Directory.CreateDirectory(sThPath);
                sThPath += idImages[iThisID].sHash + ".jpg";
                if (System.IO.File.Exists(sThPath))
                    sImPath = sThPath;
            }

            Bitmap wat;
            try { wat = (Bitmap)Bitmap.FromFile(sImPath); }
            catch { HandleEx_ImageNotFound(iThisID); wat = (Bitmap)bDummy.Clone(); }
            if (sImPath != sThPath)
            {
                if (bQThumbs)
                {
                    Bitmap bQTh = cb.ResizeBitmap(wat, pQThumbs.X, pQThumbs.Y, true, 2);

                    System.Drawing.Imaging.ImageCodecInfo iCodec = null;
                    foreach (System.Drawing.Imaging.ImageCodecInfo iCd in
                        System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders())
                        if (iCd.MimeType == "image/jpeg") iCodec = iCd;
                    System.Drawing.Imaging.EncoderParameters iEnc =
                        new System.Drawing.Imaging.EncoderParameters(1);
                    iEnc.Param[0] = new System.Drawing.Imaging.EncoderParameter(
                        System.Drawing.Imaging.Encoder.Compression, 50L);

                    bQTh.Save(sThPath, iCodec, iEnc);
                    thPages[iPage].bImages[a] = bQTh;
                }
                else
                {
                    int dsX = (int)Math.Round((double)ptThumbSize.X * dResMul);
                    int dsY = (int)Math.Round((double)ptThumbSize.Y * dResMul);
                    thPages[iPage].bImages[a] = cb.ResizeBitmap(wat, dsX, dsY, true, 2);
                }
                wat.Dispose();
            }
            else
            {
                thPages[iPage].bImages[a] = wat;
            }
        }
        private void ShiftThumbData(int iSteps)
        {
            if (iSteps == -1)
            {

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
                //pnShow.BringToFront();
            }
            if (bIsControlPanel)
            {
                if (!bEnableSidebars) return;
                int iPanelIndex = -1;
                for (int a = 0; a < pnaPanelsControl.Length; a++)
                {
                    if (pnaPanelsControl[a] == pnShow) iPanelIndex = a;
                    pnaPanelsControl[a].Visible = false;
                }
                pnShow.Visible = true;
                pnActiveControl = pnShow;
                CSet_ddControlSet.SelectedIndex = iPanelIndex;
                if (pnDisplay.Visible) iDispSidepn = iPanelIndex;
                //pnShow.SendToBack();
            }
        }
        public void DispLoad(int iIndex)
        {
            int iDist = iIndex - DispImageIndex;
            if (iIndex < 0 || iIndex >= idImages.Length) return;
            if ((int)Math.Abs(iDist) <= 1) { DispSkip(iDist); return; }
            DispImageIndex = iIndex;
            if (idImages[DispImageIndex].bDel)
            {
                int iPrev = DispImageIndex;
                int iNext = DispImageIndex;
                DispImageIndex = -1;
                while (true)
                {
                    if (!idImages[iPrev].bDel) break;
                    iPrev--; if (iPrev < 0)
                    {
                        iPrev = -1; break;
                    }
                }
                while (true)
                {
                    if (!idImages[iNext].bDel) break;
                    iNext++; if (iNext >= idImages.Length)
                    {
                        iNext = -1; break;
                    }
                }
                if (iPrev != -1) DispImageIndex = iPrev;
                if (iNext != -1) DispImageIndex = iNext;
            }
            if (DispImageIndex != -1)
                DispLoad(cb.sAppPath + DB.Path +
                idImages[DispImageIndex].sPath);
            else DispLoad(cb.ResizeBitmap(
                bDenied, Display_pbDisplay.Width,
                Display_pbDisplay.Height, true, 3),
                new Rectangle());
            if (DispImage[0] != null) DispImage[0].Dispose();
            if (DispImage[2] != null) DispImage[2].Dispose();
            while (bwPrevDisp.IsBusy || bwNextDisp.IsBusy)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            bwPrevDisp.RunWorkerAsync();
            bwNextDisp.RunWorkerAsync();
            if (iDispSidepn == -1) ShowPanel(pnQEdit);
            else ShowPanel(pnaPanelsControl[iDispSidepn]);
            ViewImageInfo(DispImageIndex, QEdit_chkAutorefresh.Checked);
            RecountStatistics();
        }
        public void DispLoad(string sFilename)
        {
            Bitmap bTmp;
            try { bTmp = (Bitmap)Bitmap.FromFile(sFilename); }
            catch { HandleEx_ImageNotFound(DispImageIndex); bTmp = (Bitmap)bDummy.Clone(); }
            DispLoad(bTmp, new Rectangle()); bTmp.Dispose();
        }
        public void DispLoad(Bitmap bImage, Rectangle rect)
        {
            Display_pbDisplay.Image = bDummy as Image;
            if (DispImage[1] != null) DispImage[1].Dispose();
            Bitmap bPic = null;
            if (rect != new Rectangle())
            {
                double dAW = (double)Display_pbDisplay.Width / (double)Display_pbDisplay.Height;
                double dAR = (double)rect.Width / (double)rect.Height;
                if (dAR < dAW) // Too wide
                {
                    bPic = new Bitmap((int)((double)Display_pbDisplay.Width * 1.5),
                        (int)(((double)Display_pbDisplay.Width * 1.5) / dAR));
                }
                if (dAR > dAW) // Too tall
                {
                    bPic = new Bitmap((int)(((double)Display_pbDisplay.Height * 1.5) * dAR),
                        (int)((double)Display_pbDisplay.Height * 1.5));
                }

                using (Graphics g = Graphics.FromImage((Image)bPic))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.DrawImage(bImage, new Rectangle(0, 0, bPic.Width, bPic.Height),
                        rect, GraphicsUnit.Pixel);
                }
                DispSizes[1] = new Point(-1, -1);
            }
            else
            {
                bPic = (Bitmap)cb.ResizeBitmap(
                    bImage, (int)((double)Display_pbDisplay.Width * 1.5),
                    (int)((double)Display_pbDisplay.Height * 1.5), true, 2);
                DispSizes[1] = new Point(bImage.Width, bImage.Height);
            }
            DispImage[1] = (Bitmap)bPic.Clone();
            Display_pbDisplay.Image = DispImage[1] as Image;
            bImage.Dispose(); bPic.Dispose();
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
                    if (!idImages[iTestDel].bDel) break;
                }
                while (bwPrevDisp.IsBusy)
                {
                    System.Threading.Thread.Sleep(1);
                    Application.DoEvents();
                    if (bwPrevVoid) return;
                }
                if (bwPrevVoid) return;
                Display_pbDisplay.Image = bDummy as Image;
                DispSizes[2] = DispSizes[1]; DispSizes[1] = DispSizes[0];
                if (DispImage[2] != null) DispImage[2].Dispose(); DispImage[2] = DispImage[1].Clone() as Bitmap;
                if (DispImage[1] != null) DispImage[1].Dispose(); DispImage[1] = DispImage[0].Clone() as Bitmap;
                if (DispImage[0] != null) DispImage[0].Dispose();
                Display_pbDisplay.Image = DispImage[1] as Image;
                while (true)
                {
                    DispImageIndex--; if (DispImageIndex < 0) return;
                    if (!idImages[DispImageIndex].bDel) break;
                }
                bwPrevDisp.RunWorkerAsync();
            }
            if (iSteps == 1)
            {
                int iTestDel = DispImageIndex;
                while (true)
                {
                    iTestDel++; if (iTestDel >= idImages.Length) return;
                    if (!idImages[iTestDel].bDel) break;
                }
                while (bwNextDisp.IsBusy)
                {
                    System.Threading.Thread.Sleep(1);
                    Application.DoEvents();
                    if (bwNextVoid) return;
                }
                if (bwNextVoid) return;
                Display_pbDisplay.Image = bDummy as Image;
                DispSizes[0] = DispSizes[1]; DispSizes[1] = DispSizes[2];
                if (DispImage[0] != null) DispImage[0].Dispose(); DispImage[0] = DispImage[1].Clone() as Bitmap;
                if (DispImage[1] != null) DispImage[1].Dispose(); DispImage[1] = DispImage[2].Clone() as Bitmap;
                if (DispImage[2] != null) DispImage[2].Dispose();
                Display_pbDisplay.Image = DispImage[1] as Image;
                while (true)
                {
                    DispImageIndex++; if (DispImageIndex >= idImages.Length) return;
                    if (!idImages[DispImageIndex].bDel) break;
                }
                bwNextDisp.RunWorkerAsync();
            }
            RecountStatistics();
            ViewImageInfo(DispImageIndex, QEdit_chkAutorefresh.Checked);
        }
        public void DispUnload()
        {
            Display_pbDisplay.Image = bDummy as Image;
            if (DispImage[0] != null) DispImage[0].Dispose();
            if (DispImage[1] != null) DispImage[1].Dispose();
            if (DispImage[2] != null) DispImage[2].Dispose();
            ZoomRect = new Rectangle(-1, -1, -1, -1);
            DispImageIndex = -9001;
        }
        public void DispClose()
        {
            int iIndex = DispImageIndex;
            ShowPanel(pnThumbs);
            ShowPanel(pnGeneral);
            DispUnload();
            SetThumbPageRanges(iIndex);
            RedrawThumbnails(false);
            ReloadThumbnails(false);
            RecountStatistics();
        }
        private void bwPrevDisp_DoWork(object sender, DoWorkEventArgs e)
        {
            if (DispImageIndex < 0) return;
            int iDII = DispImageIndex;
            int iPrevImg = DispImageIndex;
            while (true)
            {
                iPrevImg--; if (iPrevImg < 0) return;
                if (!idImages[iPrevImg].bDel) break;
            }
            if (DispImage[0] != null) DispImage[0].Dispose();
            /*DispImage[0] = (Bitmap)Bitmap.FromFile(cb.sAppPath + DB.Path +
                idImages[iPrevImg].sHash + "." +
                idImages[iPrevImg].sType);*/
            Bitmap bTmp1;
            try { bTmp1 = (Bitmap)Bitmap.FromFile(cb.sAppPath + DB.Path + idImages[iPrevImg].sPath); }
            catch { HandleEx_ImageNotFound(iPrevImg); bTmp1 = (Bitmap)bDummy.Clone(); }

            Bitmap bTmp2 = cb.ResizeBitmap(
                bTmp1, (int)((double)Display_pbDisplay.Width * 1.5),
                (int)((double)Display_pbDisplay.Height * 1.5), true, 2);
            if (DispImageIndex == iDII)
            {
                DispImage[0] = (Bitmap)bTmp2.Clone();
                DispSizes[0] = new Point(bTmp1.Width, bTmp1.Height);
                bwPrevVoid = false;
            }
            else bwPrevVoid = true;
            bTmp1.Dispose(); bTmp2.Dispose();
        }
        private void bwNextDisp_DoWork(object sender, DoWorkEventArgs e)
        {
            if (DispImageIndex < 0) return;
            int iDII = DispImageIndex;
            int iNextImg = DispImageIndex;
            while (true)
            {
                iNextImg++; if (iNextImg >= idImages.Length) return;
                if (!idImages[iNextImg].bDel) break;
            }
            if (DispImage[2] != null) DispImage[2].Dispose();
            /*DispImage[2] = (Bitmap)Bitmap.FromFile(cb.sAppPath + DB.Path +
                idImages[iNextImg].sHash + "." +
                idImages[iNextImg].sType);*/
            Bitmap bTmp1;
            try { bTmp1 = (Bitmap)Bitmap.FromFile(cb.sAppPath + DB.Path + idImages[iNextImg].sPath); }
            catch { HandleEx_ImageNotFound(iNextImg); bTmp1 = (Bitmap)bDummy.Clone(); }

            Bitmap bTmp2 = cb.ResizeBitmap(
                bTmp1, (int)((double)Display_pbDisplay.Width * 1.5),
                (int)((double)Display_pbDisplay.Height * 1.5), true, 2);
            if (DispImageIndex == iDII)
            {
                DispImage[2] = (Bitmap)bTmp2.Clone();
                DispSizes[2] = new Point(bTmp1.Width, bTmp1.Height);
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
            if (idImages[iIndex].bSel)
            {
                idImages[iIndex].bSel = false;
                if (iIndex >= thPages[1].idRange.X && iIndex <= thPages[1].idRange.Y)
                    pnThumb[iIndex - thPages[1].idRange.X].BackColor = clr_pnThumb[0];
            }
            else
            {
                idImages[iIndex].bSel = true;
                if (iIndex >= thPages[1].idRange.X && iIndex <= thPages[1].idRange.Y)
                    pnThumb[iIndex - thPages[1].idRange.X].BackColor = clr_pnThumb[1];
            }
            RecountStatistics();
            return idImages[iIndex].bSel;
        }
        private void tPollForClicks_Tick(object sender, EventArgs e)
        {
            if (bRunning && !bInTPFClicks)
            {
                bInTPFClicks = true;
                if (bFocusMe)
                {
                    bFocusMe = false;
                    this.Focus();
                    Application.DoEvents();
                }
                if (pThumb.Ci.bPoll)
                {
                    pThumb.Ci.bPoll = false;
                    cmdDummy.Focus();
                    if (!DB.IsOpen)
                    {
                        MessageBox.Show("Please select or create a database first.",
                            "You are doing it wrong.", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        iThClicked = pThumb.Ci.iIndex;
                        if (pThumb.Ci.iKey == 1)
                        {
                            if (pThumb.Ci.iCount == 1)
                            {
                                if (!pnThumbs.Controls.Contains(pThumb[iThClicked]))
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
                                    ToggleSelect(thPages[1].idRange.X + iThClicked);
                                }
                            }
                            else if (pThumb.Ci.iCount == -1)
                            {
                                int iIDI = thPages[1].idRange.X + iThClicked;
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
                bInTPFClicks = false;
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
            SetThumbPageRanges(thPages[1].idRange.X);
            ReloadThumbnails(false);
        }
        private void General_cmdReload_Click(object sender, EventArgs e)
        {
            if (!DB.IsOpen)
            {
                MessageBox.Show("Please select or create a database first.",
                    "You are doing it wrong.", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation); return;
            }
            ReloadThumbnails(true);
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
                        idImages[iIndex].sHash = rd.GetString(DB.diHash);
                        idImages[iIndex].sThmb = rd.GetString(DB.diThmb);
                        idImages[iIndex].sType = rd.GetString(DB.diType);
                        idImages[iIndex].iLen = rd.GetInt32(DB.diFLen);
                        idImages[iIndex].ptRes.X = rd.GetInt32(DB.diXRes);
                        idImages[iIndex].ptRes.Y = rd.GetInt32(DB.diYRes);
                        idImages[iIndex].sName = rd.GetString(DB.diName);
                        idImages[iIndex].sPath = rd.GetString(DB.diPath);
                        idImages[iIndex].cFlag = rd.GetString(DB.diFlag).ToCharArray(0, 32);
                        idImages[iIndex].sCmnt = rd.GetString(DB.diCmnt);
                        idImages[iIndex].iRate = rd.GetInt32(DB.diRate);
                        idImages[iIndex].sTGen = rd.GetString(DB.diTGen);
                        idImages[iIndex].sTSrc = rd.GetString(DB.diTSrc);
                        idImages[iIndex].sTChr = rd.GetString(DB.diTChr);
                        idImages[iIndex].sTArt = rd.GetString(DB.diTArt);
                    }
                }
            }
            iThumbPage = 1;
            if (!pnThumbs.Visible) ShowPanel(pnThumbs);
            if (!pnGeneral.Visible) ShowPanel(pnGeneral);
            LastSearch = new SearchPrm();
            LastSearchFallback = 1;
            SetThumbPageRanges(0);
            RedrawThumbnails(false);
            ReloadThumbnails(false);
            iVMode = 0; iAMode = 0;
        }
        private void tHotkeys_Tick(object sender, EventArgs e)
        {
            if (bInTHotkeys) return; bInTHotkeys = true;
            VK_Alt = (GetAsyncKeyState(Keys.Menu) != 0);
            VK_Ctrl = (GetAsyncKeyState(Keys.ControlKey) != 0);
            VK_Shft = (GetKeyState(Keys.LShiftKey) != 0);
            VK_Home = (GetAsyncKeyState(Keys.Home) == -32767);
            VK_Ins = (GetAsyncKeyState(Keys.Insert) == -32767);
            VK_Del = (GetAsyncKeyState(Keys.Delete) == -32767);
            VK_Caps = (GetKeyState(Keys.Capital) != 0);
            bool VKUp = (GetAsyncKeyState(Keys.Up) == -32767);
            bool VKDown = (GetAsyncKeyState(Keys.Down) == -32767);
            bool VKLeft = (GetAsyncKeyState(Keys.Left) == -32767);
            bool VKRight = (GetAsyncKeyState(Keys.Right) == -32767);
            bool VKSpace = (GetAsyncKeyState(Keys.Space) == -32767);
            bool VKEnter = (GetAsyncKeyState(Keys.Enter) == -32767);
            bool VKPlus = (
                GetAsyncKeyState(Keys.Add) == -32767 ||
                GetAsyncKeyState(Keys.Oemplus) == -32767);
            bool VKMinus = (
                GetAsyncKeyState(Keys.Subtract) == -32767 ||
                GetAsyncKeyState(Keys.OemMinus) == -32767);
            bool VKA = (GetAsyncKeyState(Keys.A) == -32767);
            bool VKC = (GetAsyncKeyState(Keys.C) == -32767);
            bool VKF = (GetAsyncKeyState(Keys.F) == -32767);
            bool VKG = (GetAsyncKeyState(Keys.G) == -32767);
            bool VKH = (GetAsyncKeyState(Keys.H) == -32767);
            bool VKU = (GetAsyncKeyState(Keys.U) == -32767);
            if (GetForegroundWindow() == ipMyHandle)
            {
                if (!VK_Ctrl && VK_Alt)
                {
                    if (VKEnter)
                    {
                        pnDisplay.BringToFront();
                        if (pnDisplay.Dock == DockStyle.None)
                        {
                            fbsFrmStyle = this.FormBorderStyle;
                            fwsFrmState = this.WindowState;
                            ptFrmPos = this.Location;
                            szFrmSize = this.Size;
                            pnDisplay.Dock = DockStyle.Fill;
                            this.WindowState = FormWindowState.Normal;
                            this.FormBorderStyle = FormBorderStyle.None;
                            this.Width = Screen.PrimaryScreen.Bounds.Width;
                            this.Height = Screen.PrimaryScreen.Bounds.Height;
                            this.Location = new Point(0, 0);
                            this.BringToFront();
                        }
                        else
                        {
                            pnDisplay.Dock = DockStyle.None;
                            this.FormBorderStyle = fbsFrmStyle;
                            this.WindowState = fwsFrmState;
                            this.Location = ptFrmPos;
                            this.Size = szFrmSize;
                            this.BringToFront();
                        }
                    }
                }
                if (ZoomRect != new Rectangle(-1, -1, -1, -1))
                {
                    if (VKLeft || VKRight || VKUp || VKDown)
                    {
                        cmdDummy.Focus();
                        double dMul = 1;
                        if (VK_Shft) dMul = 0.05;
                        if (VK_Ctrl) dMul = 2;

                        if (VKLeft) ZoomRect = new Rectangle(
                            ZoomRect.X - (int)((double)ZoomRect.Width * dMul), ZoomRect.Y,
                            ZoomRect.Width, ZoomRect.Height);

                        if (VKRight) ZoomRect = new Rectangle(
                            ZoomRect.X + (int)((double)ZoomRect.Width * dMul), ZoomRect.Y,
                            ZoomRect.Width, ZoomRect.Height);

                        if (VKUp) ZoomRect = new Rectangle(
                            ZoomRect.X, ZoomRect.Y - (int)((double)ZoomRect.Height * dMul),
                            ZoomRect.Width, ZoomRect.Height);

                        if (VKDown) ZoomRect = new Rectangle(
                            ZoomRect.X, ZoomRect.Y + (int)((double)ZoomRect.Height * dMul),
                            ZoomRect.Width, ZoomRect.Height);

                        DispLoad(new Bitmap(cb.sAppPath + DB.Path +
                            idImages[DispImageIndex].sPath), ZoomRect);
                    }
                    if (VKPlus || VKMinus)
                    {
                        cmdDummy.Focus();
                        double dMul = 1.5;
                        if (VK_Shft) dMul = 1.05;
                        if (VK_Ctrl) dMul = 2;

                        int iWidth = 0, iHeight = 0, iXs = 0, iYs = 0;
                        if (VKMinus)
                        {
                            iWidth = (int)Math.Round((double)ZoomRect.Width * dMul);
                            iHeight = (int)Math.Round((double)ZoomRect.Height * dMul);
                            iXs = (ZoomRect.Width - iWidth) / 2;
                            iYs = (ZoomRect.Height - iHeight) / 2;
                        }
                        if (VKPlus)
                        {
                            iWidth = (int)Math.Round((double)ZoomRect.Width / dMul);
                            iHeight = (int)Math.Round((double)ZoomRect.Height / dMul);
                            iXs = (ZoomRect.Width - iWidth) / 2;
                            iYs = (ZoomRect.Height - iHeight) / 2;
                        }

                        ZoomRect = new Rectangle(ZoomRect.X + iXs,
                            ZoomRect.Y + iYs, iWidth, iHeight);
                        DispLoad(new Bitmap(cb.sAppPath + DB.Path +
                            idImages[DispImageIndex].sPath), ZoomRect);
                    }
                }
                if ((VK_Ctrl || VK_Caps) && !VK_Alt)
                {
                    if (VKLeft || VKRight)
                    {
                        cmdDummy.Focus();
                        if (pnDisplay.Visible)
                        {
                            if (ZoomRect == new Rectangle(-1, -1, -1, -1))
                            {
                                int iDist = 0;
                                if (VKLeft) iDist--;
                                if (VKRight) iDist++;
                                DispSkip(iDist);
                            }
                        }
                        if (pnThumbs.Visible)
                        {
                            int iDist = 0;
                            if (VKLeft) iDist--;
                            if (VKRight) iDist++;
                            ChangeThumbPage(iDist);
                        }
                    }
                    if (VK_Home)
                    {
                        MoveHomewards();
                    }
                    if (VK_Del)
                    {
                        if (pnDisplay.Visible)
                        {
                            if (MessageBox.Show(
                                "Do you wish to tag this image for deletion?",
                                "O SHI-", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question)
                                == DialogResult.Yes)
                            {
                                idImages[DispImageIndex].bMod = true;
                                idImages[DispImageIndex].bDel = true;
                                idImages[DispImageIndex].bSel = false;
                                int iThumb = DispImageIndex - thPages[1].idRange.X;
                                DispSkip(1); bwPrevVoid = true;
                                if (iThumb < pThumb.Count && iThumb >= 0)
                                    RedrawThumbnails(false);
                            }
                        }
                        if (pnThumbs.Visible)
                        {
                            if (MessageBox.Show(
                                "Do you wish to tag these images for deletion?",
                                "O SHI-", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question)
                                == DialogResult.Yes)
                            {
                                for (int a = 0; a < idImages.Length; a++)
                                {
                                    if (idImages[a].bSel)
                                    {
                                        idImages[a].bMod = true;
                                        idImages[a].bDel = true;
                                        idImages[a].bSel = false;
                                    }
                                }
                                RedrawThumbnails(false);
                            }
                        }
                    }
                    if (VKA)
                    {
                        if (pnThumbs.Visible)
                        {
                            SelectAllToggle(true);
                        }
                    }
                    if (VKC)
                    {
                        int[] ii = GetSelected(false);
                        System.Collections.Specialized.StringCollection sc = new
                            System.Collections.Specialized.StringCollection();

                        for (int a = 0; a < ii.Length; a++)
                            if (idImages[ii[a]].bSel)
                                sc.Add(cb.sAppPath + DB.Path +
                                    idImages[ii[a]].sPath);

                        if (sc.Count > 0)
                        {
                            Clipboard.Clear();
                            Clipboard.SetFileDropList(sc);
                        }
                    }
                    if (VKF)
                    {
                        ShowPanel(pnSearchInit);
                        ShowPanel(pnGeneral);
                        SearchInit_txtAnyfield.Focus();
                    }
                    if (VKG)
                    {
                        if (pnThumbs.Visible)
                        {
                            int iPage = 1;
                            try
                            {
                                iPage = Convert.ToInt32(InputBox.Show(
                                    "Where do you want to go today?",
                                    "Select a page", "1").Text);
                            }
                            catch { }
                            int iIndex = (iPage - 1) * pnThumb.Count;
                            if (idImages.Length <= iIndex)
                            {
                                MessageBox.Show("You don't have that many images.",
                                    "You are doing it wrong.", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                                bInTHotkeys = false; return;
                            }
                            else GotoThumbPage(iPage);
                        }
                    }
                    if (VKH)
                    {
                        ShowPanel(pnHotTagger);
                    }
                    if (VKU)
                    {
                        ShowPanel(pnUpload);
                    }
                    if (VK_Ins)
                    {
                        if (pnQEdit.Visible)
                        {
                            ViewImageInfo(DispImageIndex, true);
                        }
                    }
                    if (VKSpace)
                    {
                        if (pnDisplay.Visible)
                        {
                            ToggleSelect(DispImageIndex);
                            ViewImageInfo(DispImageIndex,
                                QEdit_chkAutorefresh.Checked);
                        }
                    }
                }
                if (bHotTagger)
                {
                    if (GetAsyncKeyState(Keys.F1) == -32767) HotTagger_Exec(1);
                    if (GetAsyncKeyState(Keys.F2) == -32767) HotTagger_Exec(2);
                    if (GetAsyncKeyState(Keys.F3) == -32767) HotTagger_Exec(3);
                    if (GetAsyncKeyState(Keys.F4) == -32767) HotTagger_Exec(4);
                    if (GetAsyncKeyState(Keys.F5) == -32767) HotTagger_Exec(5);
                    if (GetAsyncKeyState(Keys.F6) == -32767) HotTagger_Exec(6);
                    if (GetAsyncKeyState(Keys.F7) == -32767) HotTagger_Exec(7);
                    if (GetAsyncKeyState(Keys.F8) == -32767) HotTagger_Exec(8);
                    if (GetAsyncKeyState(Keys.F9) == -32767) HotTagger_Exec(9);
                    if (GetAsyncKeyState(Keys.F10) == -32767) HotTagger_Exec(10);
                    if (GetAsyncKeyState(Keys.F11) == -32767) HotTagger_Exec(11);
                    if (GetAsyncKeyState(Keys.F12) == -32767) HotTagger_Exec(12);
                }
            }
            bInTHotkeys = false;
        }
        private void RecountStatistics()
        {
            Double dSelCount = 0; Double dSelSize = 0; string SelUnit = "B";
            Double dTotCount = 0; Double dTotSize = 0; string TotUnit = "B";
            for (int a = 0; a < idImages.Length; a++)
            {
                dTotCount++; dTotSize += (Double)idImages[a].iLen;
                if (idImages[a].bSel)
                {
                    dSelCount++; dSelSize += (Double)idImages[a].iLen;
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
            iThumbPage = (int)dThPage;

            string sStatsText = " ";
            if (DispImageIndex < 0) sStatsText += "Page " + dThPage + " of " + dThPages;
            else sStatsText += "Image " + (DispImageIndex + 1) + " of " + idImages.Length;

            View_Stats.Text = sStatsText + "   ~   " +
                "Selected: " + dSelCount + " (" + dSelSize + SelUnit + ")   ~   " +
                "Total: " + dTotCount + " (" + dTotSize + TotUnit + ") ";
        }
        private void ViewImageInfo(int iIndex, bool bQEdit)
        {
            if (iIndex < 0) return;
            string sInfo = " ";
            if (iIndex < idImages.Length)
            {
                if (idImages[iIndex].bSel) sInfo += "[X]" + sGSep;
                else sInfo += "[  ]" + sGSep;
                sInfo += idImages[iIndex].iRate + sGSep;
                sInfo += idImages[iIndex].ptRes.X + "x" +
                    idImages[iIndex].ptRes.Y + sGSep;

                double dSize = (double)idImages[iIndex].iLen;
                string sSize = "B";
                if (dSize > 1024) { dSize = dSize / 1024; sSize = "KB"; }
                if (dSize > 1024) { dSize = dSize / 1024; sSize = "MB"; }
                int iSize = 3;
                if (dSize >= 1) iSize = 2;
                if (dSize >= 10) iSize = 1;
                if (dSize >= 100) iSize = 0;
                dSize = Math.Round(dSize, iSize);
                sInfo += dSize + sSize + sGSep;

                sInfo += idImages[iIndex].sName + sGSep; //+ "." + idImages[iIndex].sThumb[189] + idImages[iIndex].sThumb[190] + idImages[iIndex].sThumb[191] + "." + sGSep;
                sInfo += idImages[iIndex].sPath + " ";
                if (idImages[iIndex].sCmnt != "")
                    sInfo += "\r\n" +
                    idImages[iIndex].sCmnt + " ";

                if (bQEdit)
                {
                    QEdit_sName = idImages[iIndex].sName;
                    QEdit_txtImagName.Text = QEdit_sName;
                    QEdit_sComment = idImages[iIndex].sCmnt;
                    QEdit_txtImagComment.Text = QEdit_sComment;
                    QEdit_sRating = idImages[iIndex].iRate + "";
                    QEdit_txtImagRating.Text = QEdit_sRating;
                    QEdit_sTGeneral = idImages[iIndex].sTGen.Replace(",", ", ");
                    QEdit_txtTagsGeneral.Text = QEdit_sTGeneral;
                    QEdit_sTSource = idImages[iIndex].sTSrc.Replace(",", ", ");
                    QEdit_txtTagsSource.Text = QEdit_sTSource;
                    QEdit_sTChars = idImages[iIndex].sTChr.Replace(",", ", ");
                    QEdit_txtTagsChars.Text = QEdit_sTChars;
                    QEdit_sTArtists = idImages[iIndex].sTArt.Replace(",", ", ");
                    QEdit_txtTagsArtists.Text = QEdit_sTArtists;
                }
            }
            Info_lblInfo.Text = sInfo;
        }
        private void MoveHomewards()
        {
            if (pnDisplay.Visible)
            {
                DispClose();
            }
            else if ((!pnGeneral.Visible && bEnableSidebars) || !pnThumbs.Visible)
            {
                ShowPanel(pnGeneral); ShowPanel(pnThumbs);
            }
            else if (iAMode == 1)
            {
                iThumbPage = 1;
                idImages = idImagesR;
                iThumbPage = iThumbPageR;
                thPages = new thPageData[thPagesR.Length];
                for (int a = 0; a < thPagesR.Length; a++)
                {
                    thPages[a] = new thPageData();
                    thPages[a].idRange = thPagesR[a].idRange;
                    thPages[a].sHashes = new string[thPagesR[a].sHashes.Length];
                    for (int b = 0; b < thPagesR[a].sHashes.Length; b++)
                        thPages[a].sHashes[b] = thPagesR[a].sHashes[b];

                    thPages[a].bImages = new Bitmap[thPagesR[a].bImages.Length];
                    for (int b = 0; b < thPagesR[a].bImages.Length; b++)
                    {
                        if (thPages[a].bImages[b] != null) thPages[a].bImages[b].Dispose();
                        if (thPagesR[a].bImages[b] != null)
                        {
                            thPages[a].bImages[b] = (Bitmap)
                                thPagesR[a].bImages[b].Clone();
                            thPagesR[a].bImages[b].Dispose();
                        }
                    }
                }
                //SetThumbPageRanges(0);
                //ChangeThumbPage(iThumbPageR - 1);
                RedrawThumbnails(false);
                iAMode = 0;
            }
            else if ((pnGeneral.Visible || !bEnableSidebars) && pnThumbs.Visible && thPages[1].idRange.X != 0)
            {
                SetThumbPageRanges(0);
                RedrawThumbnails(false);
                ReloadThumbnails(false);
            }
            else
            {
                General_cmdToggleSidebars_Click(new object(), new EventArgs());
            }
        }
        private bool SelectAllToggle(bool RedrawThumbs)
        {
            bool bAllSelected = true;
            for (int a = 0; a < idImages.Length; a++)
            {
                if (!idImages[a].bSel)
                    bAllSelected = false;
            }
            if (bAllSelected)
            {
                for (int a = 0; a < idImages.Length; a++)
                {
                    idImages[a].bSel = false;
                }
            }
            else
            {
                for (int a = 0; a < idImages.Length; a++)
                {
                    idImages[a].bSel = true;
                }
            }
            if (RedrawThumbs)
            {
                RedrawThumbnails(false);
                SetThumbPageRanges(thPages[1].idRange.X);
                ReloadThumbnails(false);
            }
            return !bAllSelected;
        }
        private void SelectInvToggle(bool RedrawThumbs)
        {
            for (int a = 0; a < idImages.Length; a++)
            {
                idImages[a].bSel = !idImages[a].bSel;
            }
            if (RedrawThumbs)
            {
                RedrawThumbnails(false);
                SetThumbPageRanges(thPages[1].idRange.X);
                ReloadThumbnails(false);
            }
        }
        private void SelectAsView(bool RedrawThumbs)
        {
            int iCnt = 0;
            for (int a = 0; a < idImages.Length; a++)
                if (idImages[a].bSel) iCnt++;

            int iLoc = 0;
            ImageData[] idOld = idImages;
            idImages = new ImageData[iCnt];
            for (int a = 0; a < idOld.Length; a++)
                if (idOld[a].bSel)
                {
                    idImages[iLoc] = idOld[a];
                    idImages[iLoc].bSel = false;
                    iLoc++;
                }
            if (RedrawThumbs)
            {
                RedrawThumbnails(false);
                SetThumbPageRanges(thPages[1].idRange.X);
                ReloadThumbnails(false);
            }
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
            int FrameW = 8, FrameH = 31;
            if (pnDisplay.Dock == DockStyle.Fill)
            {
                FrameW = 0; FrameH = 0;
            }

            int pnHSkew = 212 + 12 + 8;
            if (!bEnableSidebars) pnHSkew = 4 + 8;

            foreach (Panel p in pnaPanelsControl)
                p.Height = ThisH - (int)Math.Round((64 + FrameH) * dDPIf[1]);
            foreach (Panel p in pnaPanelsMain)
            {
                p.Left = (int)Math.Round((pnHSkew) * dDPIf[0]);
                p.Size = new Size(
                    ThisW - (int)Math.Round((pnHSkew + 12 + FrameW) * dDPIf[0]),
                    ThisH - (int)Math.Round((101 + FrameH) * dDPIf[1]));
            }

            pnInfo.Width = ThisW - (int)Math.Round((212 + 32 + FrameW) * dDPIf[0]);
            pnView.Left = (int)Math.Round((pnHSkew) * dDPIf[0]);
            pnView.Width = ThisW - (int)Math.Round((pnHSkew + 12 + FrameW) * dDPIf[0]);
            pnView.Top = ThisH - (int)Math.Round((43 + FrameH) * dDPIf[1]);
            lbRes.Text = pnThumbs.Width + "x" + pnThumbs.Height + " - 16x" +
                Math.Round((16 / (double)pnThumbs.Width) * (double)pnThumbs.Height, 1);
            rcForm = new Rectangle(this.Location, this.Size);
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
        private void pnView_Resize(object sender, EventArgs e)
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
            Import_txtImageComment.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_txtImageRating.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_txtTagsGeneral.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_txtTagsSource.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_txtTagsChars.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_txtTagsArtists.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_txtProgress.Width = ThisW - (int)Math.Round((93) * dDPIf[0]);
            Import_pbPreview.Top = ThisH - (int)Math.Round((233) * dDPIf[1]);
            Import_dmySeparator.Width = ThisW - (int)Math.Round((8) * dDPIf[0]);
            Import_cmdTestTags.Location = new Point(ThisW - (int)Math.Round((242) * dDPIf[0]), ThisH - (int)Math.Round((28) * dDPIf[1]));
            Import_cmdCancel.Location = new Point(ThisW - (int)Math.Round((161) * dDPIf[0]), ThisH - (int)Math.Round((28) * dDPIf[1]));
            Import_cmdStart.Location = new Point(ThisW - (int)Math.Round((80) * dDPIf[0]), ThisH - (int)Math.Round((28) * dDPIf[1]));
        }
        private void pnSearchInit_Resize(object sender, EventArgs e)
        {
            int ThisW = pnSearchInit.Width; int ThisH = pnSearchInit.Height;
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
            Upload_lstImages.Height = (ThisH - (int)Math.Round((8 + 4) * dDPIf[1])) / 2;
            Upload_lstImages.Width = ThisW - (int)Math.Round((378) * dDPIf[0]);
            Upload_lstCompleted.Height = (ThisH - (int)Math.Round((8 + 4) * dDPIf[1])) / 2;
            Upload_lstCompleted.Width = ThisW - (int)Math.Round((378) * dDPIf[0]);
            Upload_lstCompleted.Top = ((ThisH - (int)Math.Round((8 + 4) * dDPIf[1])) / 2) +
                                        (int)Math.Round((4 + 4) * dDPIf[1]);
            Upload_txtComment.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_txtComment.Height = (ThisH - (int)Math.Round((8 + 4) * dDPIf[1])) / 2;
            Upload_pnControls.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_pnControls.Top = ((ThisH - (int)Math.Round((8 + 4) * dDPIf[1])) / 2) +
                                        (int)Math.Round((4 + 4) * dDPIf[1]);
            /*Upload_lblDelay.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_lblPass.Left = ThisW - (int)Math.Round((60) * dDPIf[0]);
            Upload_ddURL.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_txtFrequency.Left = ThisW - (int)Math.Round((308) * dDPIf[0]);
            Upload_txtPass.Left = ThisW - (int)Math.Round((184) * dDPIf[0]);
            Upload_cmdAddThumbs.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_cmdRemThumbs.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_cmdRemList.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_cmdCopyURLs.Left = ThisW - (int)Math.Round((369) * dDPIf[0]);
            Upload_cmdPass.Left = ThisW - (int)Math.Round((184) * dDPIf[0]);
            Upload_cmdStart.Left = ThisW - (int)Math.Round((184) * dDPIf[0]);
            Upload_cmdPause.Left = ThisW - (int)Math.Round((184) * dDPIf[0]);
            Upload_cmdStop.Left = ThisW - (int)Math.Round((184) * dDPIf[0]);*/
        }
        private void pnQEdit_Resize(object sender, EventArgs e)
        {
            int ThisW = pnQEdit.Width; int ThisH = pnQEdit.Height;
            QEdit_pnControls.Top = ThisH - (int)Math.Round((130) * dDPIf[1]);
            int iTBVert = (int)Math.Round((ThisH - (2 + (7 * 19) + 3 + 130 + 3)) * dDPIf[1]) / 7;
            int iLbH = QEdit_lbImagName.Height;
            int iLocName = 3;
            QEdit_lbImagName.Top = iLocName;
            QEdit_txtImagName.Top = iLocName + iLbH + 3;
            QEdit_txtImagName.Height = iTBVert;
            int iLocCmnt = iLocName + iLbH + 3 + iTBVert + 3;
            QEdit_lbImagComment.Top = iLocCmnt;
            QEdit_txtImagComment.Top = iLocCmnt + iLbH + 3;
            QEdit_txtImagComment.Height = iTBVert;
            int iLocRate = iLocCmnt + iLbH + 3 + iTBVert + 3;
            QEdit_lbImagRating.Top = iLocRate;
            QEdit_txtImagRating.Top = iLocRate + iLbH + 3;
            QEdit_txtImagRating.Height = iTBVert;
            int iLocGenr = iLocRate + iLbH + 3 + iTBVert + 3;
            QEdit_lbTagsGeneral.Top = iLocGenr;
            QEdit_txtTagsGeneral.Top = iLocGenr + iLbH + 3;
            QEdit_txtTagsGeneral.Height = iTBVert;
            int iLocSrce = iLocGenr + iLbH + 3 + iTBVert + 3;
            QEdit_lbTagsSource.Top = iLocSrce;
            QEdit_txtTagsSource.Top = iLocSrce + iLbH + 3;
            QEdit_txtTagsSource.Height = iTBVert;
            int iLocChar = iLocSrce + iLbH + 3 + iTBVert + 3;
            QEdit_lbTagsChars.Top = iLocChar;
            QEdit_txtTagsChars.Top = iLocChar + iLbH + 3;
            QEdit_txtTagsChars.Height = iTBVert;
            int iLocArti = iLocChar + iLbH + 3 + iTBVert + 3;
            QEdit_lbTagsArtists.Top = iLocArti;
            QEdit_txtTagsArtists.Top = iLocArti + iLbH + 3;
            QEdit_txtTagsArtists.Height = iTBVert;
        }
        private void pnFBindsEx_Resize(object sender, EventArgs e)
        {
            FBindEx_pnControl.Left = pnFBindsEx.Width - 369;
            FBindEx_pnDisplay.Left = pnFBindsEx.Width - 369;
            FBindEx_pnList.Width = pnFBindsEx.Width - 378;
            FBindEx_pnList.Height = pnFBindsEx.Height - 8;
            FBindEx_lstFiles.Width = pnFBindsEx.Width - 386;
            FBindEx_lstFiles.Height = pnFBindsEx.Height - 75;
            FBindEx_cmdSelAll.Top = pnFBindsEx.Height - 36;
            FBindEx_cmdSelInv.Top = pnFBindsEx.Height - 36;
        }
        private void tResizeThumbs_Tick(object sender, EventArgs e)
        {
            tResizeThumbs.Stop();
            if (bRunning)
            {
                //RedrawThumbnails(true);
                RedrawThumbnails(true);
                SetThumbPageRanges(thPages[1].idRange.X);
                ReloadThumbnails(false);
                Program.Reg_Access("Thumbnail rows", iThumbCnt + "");
                Program.Reg_Access("Res multiplier", dResMul + "");
                if (bQThumbs) Program.Reg_Access("Quick thumbs res", pQThumbs.X + " x " + pQThumbs.Y);
                if (!bQThumbs) Program.Reg_Access("Quick thumbs res", "no");
                Program.Reg_Access("Quick thumbs kill", iQThumbsDel + "");
                if (bInitialRun)
                {
                    bInitialRun = false;
                    wfSplash.Show();
                }
            }
        }
        private void General_cmdToggleSidebars_Click(object sender, EventArgs e)
        {
            bEnableSidebars = !bEnableSidebars;

            if (!bEnableSidebars)
                for (int a = 0; a < pnaPanelsControl.Length; a++)
                    if (pnaPanelsControl[a].Visible)
                    {
                        iActiveSidePan = a;
                        pnaPanelsControl[a].Visible = false;
                    }
            if (bEnableSidebars)
                ShowPanel(pnaPanelsControl[iActiveSidePan]);

            frmMain_Resize(new object(), new EventArgs());
            tResizeThumbs.Stop();

            RedrawThumbnails(true);
            SetThumbPageRanges(thPages[1].idRange.X);
            ReloadThumbnails(false);
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
            Import_txtProgress.Text = "Scanning directories";
            Application.DoEvents();

            string[] saDrops = (string[])e.Data.GetData(DataFormats.FileDrop);
            for (int a = 0; a < saDrops.Length; a++)
                saDrops[a] = saDrops[a].Replace("\\", "/");
            sFirstImportRoot = saDrops[0];
            if (sFirstImportRoot.StartsWith(cb.sAppPath + DB.Path))
                bImportIsLocal = true;
            else bImportIsLocal = false;
            if (bImportIsLocal) Import_lblStructWarn.Visible = false;
            else Import_lblStructWarn.Visible = true;

            string[] saPaths = saDrops;
            for (int a = 0; a < saDrops.Length; a++)
            {
                string[] saAppend = cb.GetPaths(saDrops[a], true);
                string[] saOldRet = saPaths;
                saPaths = new string[saOldRet.Length + saAppend.Length];
                saOldRet.CopyTo(saPaths, 0);
                saAppend.CopyTo(saPaths, saOldRet.Length);
            }

            Import_txtProgress.Text = "Filtering results"; Application.DoEvents();
            saFullPaths = cb.FilterArray(saPaths, DB.saAllowedTypes,
                cb.asIntArray(2, DB.saAllowedTypes.Length), false);

            /*Import_txtAction.Text = "Processing images...";
            for (int a = 0; a < saFullPaths.Length; a++)
            {
                Import_txtProgress.Text = a + " / " + saFullPaths.Length;
                Application.DoEvents();

                string[] sRPrcT = saFullPaths[a].Replace("\\", "/").Split('/');
                string sRPrc = "c:\\jnk\\" + sRPrcT[sRPrcT.Length-1];
                System.IO.File.Copy(saFullPaths[a], sRPrc);
                
                /*Bitmap bmTmp = (Bitmap)Bitmap.FromFile(sRPrc);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                bmTmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] msb = ms.GetBuffer();
                
                byte[] bRes = new byte[4];
                string sResX = bmTmp.Width.ToString("X4");
                string sResY = bmTmp.Height.ToString("X4");
                string sRes = sResX + sResY;
                for (int a = 0; a < 4; a++)
                System.IO.FileStream fs = new System.IO.FileStream
                    (sRPrc + ".bmp", System.IO.FileMode.Create);
                fs.Write(msb, 54, msb.Length - 54);
                fs.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);* /

                byte[] bPic = qPicR(sRPrc);
                System.IO.FileStream fs = new System.IO.FileStream
                    (sRPrc + ".bmp", System.IO.FileMode.Create);
                fs.Write(bPic, 0, bPic.Length);
                fs.Flush(); fs.Close(); fs.Dispose();
            }*/

            Import_txtProgress.Text = "Identified " + saFullPaths.Length + " images.";
            Application.DoEvents();
            Import_cmdTestTags.Enabled = true;
            Import_cmdCancel.Enabled = true;
            Import_cmdStart.Enabled = true;
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

        private static string Decimate(string str, int cnt)
        {
            string ret = ""; int i = 0;
            for (int a = 0; a < str.Length; a++)
            {
                ret += str[a]; i++;
                if (i >= cnt)
                {
                    i = 0;
                    ret += "'";
                }
            }
            return ret;
        }
        private void Import_cmdTestTags_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = sFirstImportRoot; fd.ShowDialog();
            string sFName = fd.FileName;
            if (sFName != "")
            {
                ImageData tID = DB.GenerateImageData(sFName.Replace("\\", "/"),
                    new string[] { Import_txtImageName.Text,
                        Import_txtImageComment.Text, Import_txtImageRating.Text,
                        Import_txtTagsGeneral.Text, Import_txtTagsSource.Text,
                        Import_txtTagsChars.Text, Import_txtTagsArtists.Text },
                        bImportIsLocal, true);
                if (Import_pbPreview.Image != null) Import_pbPreview.Image.Dispose();
                Import_pbPreview.Image = DB.bmThumb;
                MessageBox.Show(".:: Resulting properties for the selected file ::." + "\r\n" +
                    "(" + sFName + ")" + "\r\n" +
                    "\r\n" +
                    //"bSelected: " + tID.bSelected + "\r\n" +
                    "MD5 checksum  ...  " + tID.sHash + "\r\n" +
                    "File size (B)  ...  " + tID.iLen + "\r\n" +
                    "Image name  ...  " + tID.sName + "\r\n" +
                    "Image localpath  ...  " + tID.sPath + "\r\n" +
                    "Image comment ... " + tID.sCmnt + "\r\n" +
                    "Image type  ...  " + tID.sType + "\r\n" +
                    "Image rating  ...  " + tID.iRate + "\r\n" +
                    "Resolution  ...  " + tID.ptRes + "\r\n" +
                    "Tags General  ...  " + tID.sTGen.Replace(",", ", ") + "\r\n" +
                    "Tags Soure  ...  " + tID.sTSrc.Replace(",", ", ") + "\r\n" +
                    "Tags Chars  ...  " + tID.sTChr.Replace(",", ", ") + "\r\n" +
                    "Tags Artists  ...  " + tID.sTArt.Replace(",", ", "));
            }
        }
        private void Import_cmdCancel_Click(object sender, EventArgs e)
        {
            if (sender.ToString() == Import_cmdCancel.ToString())
            {
                frmTip.ShowMsg(true, "Tip:   Press Ctrl-Home to move one step up.");
            }
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
            if (MessageBox.Show(
                "The images will be imported to this database:" + "\r\n\r\n" +
                DB.Name + "\r\n\r\n" + "Do you wish to continue?", "Confirming import",
                MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                return;

            int iemTags = 0;
            if (Import_emTags_Before.Checked) iemTags = 1;
            if (Import_emTags_After.Checked) iemTags = 2;
            if (Import_emTags_Substitute.Checked) iemTags = 3;
            if (Import_emTags_Override.Checked) iemTags = 4;

            string[] sInfo = new string[]{Import_txtImageName.Text,
                Import_txtImageComment.Text,Import_txtImageRating.Text,
                Import_txtTagsGeneral.Text,Import_txtTagsSource.Text,
                Import_txtTagsChars.Text,Import_txtTagsArtists.Text};
            object[] ImportArgs = new object[] { saFullPaths, sInfo, iemTags, bImportIsLocal };

            int iThID = DB.iThumbIndex;
            BackgroundWorker bwImport = new BackgroundWorker();
            bwImport.DoWork += new DoWorkEventHandler(bwImport_DoWork);
            bwImport.RunWorkerAsync(ImportArgs);
            while (bwImport.IsBusy)
            {
                Import_txtProgress.Text = DB.ImportProg;
                if (iThID != DB.iThumbIndex)
                {
                    iThID = DB.iThumbIndex;
                    if (Import_pbPreview.Image != null)
                        Import_pbPreview.Image.Dispose();
                    if (DB.iThumbBusy != 1)
                    {
                        DB.iThumbBusy = 2;
                        Import_pbPreview.Image =
                            (Bitmap)DB.bmThumb.Clone();
                        DB.iThumbBusy = 0;
                    }
                }
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
            }
            Import_txtProgress.Text = "";
        }
        void bwImport_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            DB.Import((string[])args[0], (string[])args[1], (int)args[2], (bool)args[3]);
            //saFullPaths, sInfo, bImportIsLocal
        }

        private void General_cmdDBCreate_Click(object sender, EventArgs e)
        {
            if (GUI_CheckForChanges()) return;
            bool bDBCreated = false;
            string sDBName = InputBox.Show(
                "Please enter a name for the new database.",
                "Creating a new database", "pImgDB").Text;

            if (sDBName == "") return;
            sDBName = sDBName.Replace("\\", "").Replace("/", "").Replace(":", "")
                             .Replace("\"", "").Replace("*", "").Replace("?", "")
                             .Replace("<", "").Replace(">", "").Replace("|", "");
            if (sDBName == "")
            {
                MessageBox.Show(
                    "That name is invalid." + "\r\n\r\n" +
                    "Please do not use thee following characters:" + "\r\n" +
                    "\\ / : \" * ? < > |",
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
            if (sender.ToString() == General_cmdSearch.ToString())
            {
                frmTip.ShowMsg(true, "Tip:   Press Ctrl-F to initiate a search.");
            }
            ShowPanel(pnSearchInit);
        }
        private void SearchInit_cmdCancel_Click(object sender, EventArgs e)
        {
            if (sender.ToString() == SearchInit_cmdCancel.ToString())
            {
                frmTip.ShowMsg(true, "Tip:   Press Ctrl-Home to move one step up.");
            }
            ShowPanel(pnThumbs);
        }
        private void SearchInit_cmdStart_Click(object sender, EventArgs e)
        {
            if (GUI_CheckForChanges()) return;
            if (sender.ToString() == SearchInit_cmdStart.ToString())
            {
                frmTip.ShowMsg(true, "Tip:   Press Enter to start the search.");
            }
            SearchInit_txtProgress.Text = "Preparing search parameters";
            SearchInit_txtResults.Text = ""; Application.DoEvents();
            SearchPrm sp = new SearchPrm();
            if (SearchInit_txtAnyfield.Text != "")
            {
                string sSrcP = SearchInit_txtAnyfield.Text;
                sp.sImagName = sSrcP;
                sp.sTagsGeneral = sSrcP;
                sp.sTagsSource = sSrcP;
                sp.sTagsChars = sSrcP;
                sp.sTagsArtists = sSrcP;
            }
            else
            {
                sp.sImagName = SearchInit_txtImageName.Text;
                sp.sTagsGeneral = SearchInit_txtTagsGeneral.Text;
                sp.sTagsSource = SearchInit_txtTagsSource.Text;
                sp.sTagsChars = SearchInit_txtTagsChars.Text;
                sp.sTagsArtists = SearchInit_txtTagsArtists.Text;
            }
            sp.sImagFormat = SearchInit_txtImageFormat.Text;
            sp.sImagRes = SearchInit_txtImageRes.Text;
            sp.sImagRating = SearchInit_txtImageRating.Text;
            if (SearchInit_Deny_FBinds.Checked) sp.cFlagDeny[DB.flFBind] = '1';
            if (SearchInit_Return_FBinds.Checked) sp.cFlagRet[DB.flFBind] = '1';

            LastSearchFallback = 2;
            if (SearchInit_rdoFallbackAnd.Checked) LastSearchFallback = 1;
            if (SearchInit_rdoFallbackOr.Checked) LastSearchFallback = 2;
            if (SearchInit_rdoFallbackNot.Checked) LastSearchFallback = 3;
            LastSearch = sp;

            BackgroundWorker bwSearch = new BackgroundWorker();
            bwSearch.DoWork += new DoWorkEventHandler(bwSearch_DoWork);
            bwSearch.RunWorkerAsync();
            while (bwSearch.IsBusy)
            {
                if (!DB.SearchProg.EndsWith(" results"))
                    SearchInit_txtProgress.Text = DB.SearchProg;
                else SearchInit_txtResults.Text = DB.SearchProg;
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
            }

            iThumbPage = 1;
            SetThumbPageRanges(0);
            ShowPanel(pnThumbs); Application.DoEvents();
            RedrawThumbnails(true); ReloadThumbnails(false);
            iVMode = 1; iAMode = 0;
        }
        void bwSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            idImages = DB.Search(LastSearch, LastSearchFallback);
        }

        private void SetThumbPageRanges(int iMainPageStart)
        {
            if (bwLoadThumbs.IsBusy)
            {
                bwLoadThumbs.CancelAsync();
                while (bwLoadThumbs.IsBusy)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(10);
                }
            }

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
                /*iStart[1] = iMainPageStart;
                iStart[0] = iStart[1] - pThumb.Count;
                iStart[2] = iStart[1] + pThumb.Count;
                if (iStart[0] > idImages.Length) iStart[0] = 0;
                if (iStart[1] > idImages.Length) iStart[1] = 0;
                if (iStart[2] > idImages.Length) iStart[2] = 0;
                if (iStart[0] < 0) iStart[0] = idImages.Length - pThumb.Count;
                if (iStart[1] < 0) { GotoThumbPageWith(idImages.Length - 2); return; }
                //if (iStart[1] < 0) iStart[1] = idImages.Length - pThumb.Count;
                if (iStart[2] < 0) iStart[2] = idImages.Length - pThumb.Count;
                iEnd[0] = iStart[0] + pThumb.Count - 1;
                iEnd[1] = iStart[1] + pThumb.Count - 1;
                iEnd[2] = iStart[2] + pThumb.Count - 1;
                if (iEnd[0] >= idImages.Length) iEnd[0] = idImages.Length - 1;
                if (iEnd[1] >= idImages.Length) iEnd[1] = idImages.Length - 1;
                if (iEnd[2] >= idImages.Length) iEnd[2] = idImages.Length - 1;*/
                int iLastPageStart = ((int)Math.Ceiling((double)idImages.Length / (double)pThumb.Count) - 1) * pThumb.Count;
                if (iMainPageStart >= idImages.Length) iMainPageStart = 0;
                if (iMainPageStart < 0) iMainPageStart = iLastPageStart;
                iMainPageStart -= (iMainPageStart % pThumb.Count);
                iStart[0] = iMainPageStart - pThumb.Count;
                iStart[1] = iMainPageStart;
                iStart[2] = iMainPageStart + pThumb.Count;
                if (iStart[0] >= idImages.Length) iStart[0] = 0;
                if (iStart[1] >= idImages.Length) iStart[1] = 0;
                if (iStart[2] >= idImages.Length) iStart[2] = 0;
                if (iStart[0] < 0) iStart[0] = iLastPageStart;
                if (iStart[1] < 0) iStart[1] = iLastPageStart;
                if (iStart[2] < 0) iStart[2] = iLastPageStart;
                iStart[0] -= (iStart[0] % pThumb.Count);
                iStart[1] -= (iStart[1] % pThumb.Count);
                iStart[2] -= (iStart[2] % pThumb.Count);
                iEnd[0] = iStart[0] + pThumb.Count - 1;
                iEnd[1] = iStart[1] + pThumb.Count - 1;
                iEnd[2] = iStart[2] + pThumb.Count - 1;
                if (iEnd[0] >= idImages.Length) iEnd[0] = idImages.Length - 1;
                if (iEnd[1] >= idImages.Length) iEnd[1] = idImages.Length - 1;
                if (iEnd[2] >= idImages.Length) iEnd[2] = idImages.Length - 1;
            }
            thPages[0].idRange = new Point(iStart[0], iEnd[0]);
            thPages[1].idRange = new Point(iStart[1], iEnd[1]);
            thPages[2].idRange = new Point(iStart[2], iEnd[2]);
            iThumbPage = (int)Math.Ceiling((double)(iStart[1] + 1) / (double)pThumb.Count) - 1;
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
            RedrawThumbnails(false);
            ReloadThumbnails(false);
            //}
        }
        private void GotoThumbPage(int iPage)
        {
            int iIndex = (iPage - 1) * pnThumb.Count;
            SetThumbPageRanges(iIndex);
            RedrawThumbnails(false);
            ReloadThumbnails(false);
            iThumbPage = iPage;
        }
        private void GotoThumbPageWith(int iImage)
        {
            int iIndex = 0; int iPage = 1;
            while (iIndex < iImage)
            {
                iPage++;
                iIndex += pnThumb.Count;
            }
            SetThumbPageRanges(iIndex);
            RedrawThumbnails(false);
            ReloadThumbnails(false);
            iThumbPage = iPage;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CheckForChanges())
            {
                DialogResult dRet = MessageBox.Show(
                    "You have unsaved changes. Do you wish to save before you exit?",
                    "Hello", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (dRet == DialogResult.Yes) GUI_SaveChanges(false);
                if (dRet == DialogResult.Cancel) { e.Cancel = true; return; }
            }
            niTray.Visible = false;
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
                if (idImages[a].bMod) bChanges = true;
            }
            return bChanges;
        }
        private bool GUI_CheckForChanges()
        {
            if (CheckForChanges())
            {
                DialogResult dr = MessageBox.Show("You have changes that needs to be saved." + "\r\n" +
                    "If you perform this action, the queued changes will be lost." + "\r\n" +
                    "Do you wish to save changes before you continue?" + "\r\n\r\n" +
                    "YES: Save changes, then continue" + "\r\n" +
                    "NO:  Continue without saving changes" + "\r\n" +
                    "CNC: Cancel the pending operation", "Hello",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel) return true;
                if (dr == DialogResult.Yes) GUI_SaveChanges(false);
                return false;
            }
            return false;
        }
        private void GUI_SaveChanges(bool bReloadDisplay)
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
                if (idImages[a].bDel) iDelCnt++;
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
                            if (idImages[a].bDel)
                            {
                                frmWait.sFooter = "Processing " + a + " of " + iDelCnt;
                                try { System.IO.File.Delete(cb.sAppPath + DB.Path + idImages[a].sPath); }
                                catch { }
                                dbParam.Value = idImages[a].sHash;
                                DBc.ExecuteNonQuery();
                            }
                        }
                    }
                    frmWait.sFooter = "Finalizing deletion";
                    dbTrs.Commit();
                }
            }

            frmWait.sMain = "Fixing errors (if any)";
            frmWait.bInstant = true; while (frmWait.bInstant) Application.DoEvents();
            for (int a = 0; a < idImages.Length; a++)
            {
                frmWait.sFooter = a + " of " + idImages.Length;
                idImages[a].sTGen = cb.RemoveDupes(idImages[a].sTGen.Replace("\r", "").Replace("\n", ""));
                idImages[a].sTSrc = cb.RemoveDupes(idImages[a].sTSrc.Replace("\r", "").Replace("\n", ""));
                idImages[a].sTChr = cb.RemoveDupes(idImages[a].sTChr.Replace("\r", "").Replace("\n", ""));
                idImages[a].sTArt = cb.RemoveDupes(idImages[a].sTArt.Replace("\r", "").Replace("\n", ""));
            }

            frmWait.sMain = "Looking for changes";
            frmWait.bInstant = true; while (frmWait.bInstant) Application.DoEvents();
            int iModCnt = 0;
            string[] saDBFields = new string[]
            {
                "name", "path", "flag", "cmnt", "rate", "tgen", "tsrc", "tchr", "tart"
            };
            using (SQLiteTransaction dbTrs = DB.Data.BeginTransaction())
            {
                SQLiteCommand[] DBcW = new SQLiteCommand[9];
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
                    int iImgCnt = 0;
                    DBcR.CommandText = "SELECT * FROM 'images'";
                    using (SQLiteDataReader rd = DBcR.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            string sHash = rd.GetString(DB.diHash);
                            for (int a = 0; a < idImages.Length; a++)
                            {
                                if (!idImages[a].bDel &&
                                    idImages[a].sHash == sHash)
                                {
                                    iImgCnt++; frmWait.sFooter = iImgCnt + " of " + idImages.Length;
                                    if (iImgCnt.ToString().EndsWith("00")) Application.DoEvents();

                                    bool bThisChanged = false;
                                    bool[] baThisChanged = new bool[DBcW.Length];
                                    idImages[a].bMod = false;

                                    string sOName = rd.GetString(DB.diName);
                                    if (idImages[a].sName != sOName)
                                    {
                                        bThisChanged = true;
                                        baThisChanged[0] = true;
                                        dbP_Value[0].Value = idImages[a].sName;
                                        //idImages[a].cFlag[DB.flEmTag] = '2';
                                    }
                                    string sOPath = rd.GetString(DB.diPath);
                                    if (idImages[a].sPath != sOPath)
                                    {
                                        bThisChanged = true;
                                        baThisChanged[1] = true;
                                        dbP_Value[1].Value = idImages[a].sPath;

                                        string sFilename = idImages[a].sPath.Substring(idImages[a].sPath.LastIndexOf('/') + 1);
                                        string sNewDir = idImages[a].sPath.Substring(0, idImages[a].sPath.Length - sFilename.Length);
                                        System.IO.Directory.CreateDirectory(cb.sAppPath + DB.Path + sNewDir);
                                        System.IO.File.Move(cb.sAppPath + DB.Path + sOPath, cb.sAppPath + DB.Path + sNewDir + sFilename);
                                    }
                                    string sOCmnt = rd.GetString(DB.diCmnt);
                                    if (idImages[a].sCmnt != sOCmnt)
                                    {
                                        bThisChanged = true;
                                        baThisChanged[3] = true;
                                        dbP_Value[3].Value = idImages[a].sCmnt;
                                        //idImages[a].cFlag[DB.flEmTag] = '2';
                                    }
                                    int iORate = rd.GetInt32(DB.diRate);
                                    if (idImages[a].iRate != iORate)
                                    {
                                        bThisChanged = true;
                                        baThisChanged[4] = true;
                                        dbP_Value[4].Value = idImages[a].iRate;
                                    }
                                    string sOTGen = rd.GetString(DB.diTGen);
                                    if (idImages[a].sTGen != sOTGen)
                                    {
                                        bThisChanged = true;
                                        baThisChanged[5] = true;
                                        dbP_Value[5].Value = idImages[a].sTGen;
                                        //idImages[a].cFlag[DB.flEmTag] = '2';
                                    }
                                    string sOTSrc = rd.GetString(DB.diTSrc);
                                    if (idImages[a].sTSrc != sOTSrc)
                                    {
                                        bThisChanged = true;
                                        baThisChanged[6] = true;
                                        dbP_Value[6].Value = idImages[a].sTSrc;
                                        //idImages[a].cFlag[DB.flEmTag] = '2';
                                    }
                                    string sOTChr = rd.GetString(DB.diTChr);
                                    if (idImages[a].sTChr != sOTChr)
                                    {
                                        bThisChanged = true;
                                        baThisChanged[7] = true;
                                        dbP_Value[7].Value = idImages[a].sTChr;
                                        //idImages[a].cFlag[DB.flEmTag] = '2';
                                    }
                                    string sOTArt = rd.GetString(DB.diTArt);
                                    if (idImages[a].sTArt != sOTArt)
                                    {
                                        bThisChanged = true;
                                        baThisChanged[8] = true;
                                        dbP_Value[8].Value = idImages[a].sTArt;
                                        //idImages[a].cFlag[DB.flEmTag] = '2';
                                    }
                                    string sOTFlag = rd.GetString(DB.diFlag);
                                    if (new string(idImages[a].cFlag) != sOTFlag)
                                    {
                                        bThisChanged = true;
                                        baThisChanged[2] = true;
                                        dbP_Value[2].Value = new string(idImages[a].cFlag);
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

            for (int a = 0; a < idImages.Length; a++)
            {
                idImages[a].bMod = false;
            }

            if (bReloadDisplay && (iDelCnt != 0 || iModCnt != 0))
            {
                frmWait.sMain = "Reconstructing thumb view";
                frmWait.bInstant = true; while (frmWait.bInstant)
                    Application.DoEvents();
                if (iVMode == 1)
                {
                    idImages = DB.Search(LastSearch, LastSearchFallback);
                    iThumbPage = 1;
                    SetThumbPageRanges(0);
                    ShowPanel(pnThumbs); Application.DoEvents();
                    RedrawThumbnails(true); ReloadThumbnails(false);
                    iAMode = 0;
                }
                else
                {
                    General_cmdAllImages_Click(
                        new object(), new EventArgs());
                }
            }
            frmWait.sHeader = "Click this window or press space to close";
            frmWait.sMain = "All done";
            frmWait.bCloseAtClick = true;
            wfWait.Focus();
        }
        private void General_cmdSaveChanges_Click(object sender, EventArgs e)
        {
            GUI_SaveChanges(true);
        }

        private void General_cmdMassEdit_Click(object sender, EventArgs e)
        {
            ShowPanel(pnMassEdit);
        }
        private void MassEdit_cmdCancel_Click(object sender, EventArgs e)
        {
            if (sender.ToString() == MassEdit_cmdCancel.ToString())
            {
                frmTip.ShowMsg(true, "Tip:   Press Ctrl-Home to move one step up.");
            }
            ShowPanel(pnThumbs);
        }
        private void MassEdit_cmdStart_Click(object sender, EventArgs e)
        {
            if (GUI_CheckForChanges()) return;
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

            int iMainAction = MassEdit_ddMainAction.SelectedIndex + 1;
            string sModifyValue = MassEdit_txtModifyValue.Text.ToLower();
            string sInsertValue = MassEdit_txtInsertValue.Text;
            int iInsertTo = MassEdit_ddInsertTo.SelectedIndex + 1;
            int iTargetGroup = MassEdit_ddTargetGroup.SelectedIndex + 1;
            int iCondTarget = MassEdit_ddCondTarget.SelectedIndex + 1;
            int iCondCType = MassEdit_ddCondCType.SelectedIndex + 1;
            string sCondValue = MassEdit_txtCondValue.Text.ToLower();
            int iChangeCnt = 0;

            ImageData[] idEdit = new ImageData[0];
            if (iTargetGroup == 1) //Selected images
            {
                int i = 0;
                for (int a = 0; a < idImages.Length; a++)
                {
                    if (idImages[a].bSel) i++;
                }
                idEdit = new ImageData[i]; i = -1;
                for (int a = 0; a < idImages.Length; a++)
                {
                    if (idImages[a].bSel)
                    {
                        i++; idEdit[i] = idImages[a];
                    }
                }
            }
            if (iTargetGroup == 2) //Last search results
            {
                idEdit = DB.Search(LastSearch, LastSearchFallback);
            }
            if (iTargetGroup == 3) //All images in database
            {
                idEdit = DB.Search(new SearchPrm(), 1);
            }

            using (SQLiteTransaction dbTrs = DB.Data.BeginTransaction())
            {
                using (SQLiteCommand DBc = DB.Data.CreateCommand())
                {
                    string sInsertTo = "";
                    if (iInsertTo == 1) sInsertTo = "name"; // Image Name
                    if (iInsertTo == 2) sInsertTo = "cmnt"; // Image comment
                    if (iInsertTo == 3) sInsertTo = "rate"; // Image Rating
                    if (iInsertTo == 4) sInsertTo = "tgen"; // Tags General
                    if (iInsertTo == 5) sInsertTo = "tsrc"; // Tags Source
                    if (iInsertTo == 6) sInsertTo = "tchr"; // Tags Chars
                    if (iInsertTo == 7) sInsertTo = "tart"; // Tags Artists

                    if (iInsertTo == 3)
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
                    frmWait.bInstant = true; while (frmWait.bInstant) Application.DoEvents();
                    for (int a = 0; a < idEdit.Length; a++)
                    {
                        frmWait.sHeader = "Checking " + a + " of " + idEdit.Length;
                        bool bFulfillsCondition = false;
                        string sConditionTarget = "";
                        if (iCondTarget == 1) sConditionTarget = "," + idEdit[a].sName + ","; //Image Name
                        if (iCondTarget == 2) sConditionTarget = "," + idEdit[a].sCmnt + ","; //Image Comment
                        if (iCondTarget == 3) sConditionTarget = "," + idEdit[a].iRate + ","; //Image Rating
                        if (iCondTarget == 4) sConditionTarget = "," + idEdit[a].sTGen + ","; //Tags General
                        if (iCondTarget == 5) sConditionTarget = "," + idEdit[a].sTSrc + ","; //Tags Source
                        if (iCondTarget == 6) sConditionTarget = "," + idEdit[a].sTChr + ","; //Tags Chars
                        if (iCondTarget == 7) sConditionTarget = "," + idEdit[a].sTArt + ","; //Tags Artists
                        sConditionTarget = sConditionTarget.ToLower();
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
                                saCondValue[b] = saCondValue[b].Trim(' ').ToLower();
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
                            if (iInsertTo == 2) sOriginalField = "" + idEdit[a].sCmnt; // Image Comment
                            if (iInsertTo == 3) sOriginalField = "" + idEdit[a].iRate; // Image Rating
                            if (iInsertTo == 4) sOriginalField = "" + idEdit[a].sTGen; // Tags General
                            if (iInsertTo == 5) sOriginalField = "" + idEdit[a].sTSrc; // Tags Source
                            if (iInsertTo == 6) sOriginalField = "" + idEdit[a].sTChr; // Tags Chars
                            if (iInsertTo == 7) sOriginalField = "" + idEdit[a].sTArt; // Tags Artists

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
                                sVal = ReplaceCI("," + sOriginalField + ",",
                                    "," + sModifyValue + ",", "," + sInsertValue + ",");
                                while (sVal.Contains(" ,") || sVal.Contains(", "))
                                    sVal = sVal.Replace(" ,", ",").Replace(", ", ",");
                                while (sVal.Contains(",,")) sVal = sVal.Replace(",,", ",");
                                sVal = sVal.Trim(',', ' ');

                                dbParam1.Value = sVal;
                                dbParam2.Value = idEdit[a].sHash;
                                DBc.ExecuteNonQuery();
                            }
                            if (iMainAction == 4) //Delete image
                            {
                                System.IO.File.Delete(
                                    cb.sAppPath + DB.Path + idEdit[a].sPath);
                                dbParam1.Value = idEdit[a].sHash;
                                DBc.ExecuteNonQuery();
                            }

                            if (iMainAction != 4)
                            {
                                if (iInsertTo == 1) idEdit[a].sName = sVal; // Image Name
                                if (iInsertTo == 2) idEdit[a].sCmnt = sVal; // Image Comment
                                if (iInsertTo == 3) idEdit[a].iRate =
                                    Convert.ToInt32(sVal); // Image Rating
                                if (iInsertTo == 4) idEdit[a].sTGen = sVal; // Tags General
                                if (iInsertTo == 5) idEdit[a].sTSrc = sVal; // Tags Source
                                if (iInsertTo == 6) idEdit[a].sTChr = sVal; // Tags Chars
                                if (iInsertTo == 7) idEdit[a].sTArt = sVal; // Tags Artists
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
                if (iVMode == 1)
                {
                    idImages = DB.Search(LastSearch, LastSearchFallback);
                    iThumbPage = 1;
                    SetThumbPageRanges(0);
                    ShowPanel(pnThumbs); Application.DoEvents();
                    RedrawThumbnails(true); ReloadThumbnails(false);
                    iAMode = 0;
                }
                else
                {
                    General_cmdAllImages_Click(
                        new object(), new EventArgs());
                }
            }
            frmWait.sHeader = "Click this window or press space to close";
            frmWait.sMain = "All done";
            frmWait.bCloseAtClick = true;
            wfWait.Focus();
        }

        private void General_cmdQuickEdit_Click(object sender, EventArgs e)
        {
            ShowPanel(pnQEdit);
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
            idImages[i].bMod = true;
            if (iParam == 1) sOldVal = "" + idImages[i].sName;
            if (iParam == 2) sOldVal = "" + idImages[i].sCmnt;
            if (iParam == 3) sOldVal = "" + idImages[i].iRate;
            if (iParam == 4) sOldVal = "" + idImages[i].sTGen;
            if (iParam == 5) sOldVal = "" + idImages[i].sTSrc;
            if (iParam == 6) sOldVal = "" + idImages[i].sTChr;
            if (iParam == 7) sOldVal = "" + idImages[i].sTArt;

            if (iAction == 1) //Overwrite
            {
                sVal = sValue;
            }
            if (iAction == 2) //Append
            {
                sVal = sOldVal + "," + sValue;
                if (iParam <= 3) sVal = sValue;
            }
            if (iAction == 3) //Remove
            {
                sVal = ReplaceCI(sOldVal, sValue, "");
            }
            while (sVal.Contains(" ,") || sVal.Contains(", "))
                sVal = sVal.Replace(" ,", ",").Replace(", ", ",");
            while (sVal.Contains(",,")) sVal = sVal.Replace(",,", ",");
            sVal = cb.RemoveDupes(sVal);

            if (iParam == 1) idImages[i].sName = sVal;
            if (iParam == 2) idImages[i].sCmnt = sVal;
            if (iParam == 3) if (OnlyContains(sVal, "-0123456789"))
                    idImages[i].iRate = Convert.ToInt32(sVal);
            if (iParam == 4) idImages[i].sTGen = sVal;
            if (iParam == 5) idImages[i].sTSrc = sVal;
            if (iParam == 6) idImages[i].sTChr = sVal;
            if (iParam == 7) idImages[i].sTArt = sVal;
            if (iParam != 3)
                if (idImages[i].cFlag[DB.flEmTag] == '1')
                    idImages[i].cFlag[DB.flEmTag] = '2';
        }
        private void EditSingleParameter(int iParam, string sValue, int iAction)
        {
            if (pnDisplay.Visible) EditSingleParameter(DispImageIndex, iParam, sValue, iAction);
            else
            {
                frmTip.ShowMsg(false, "PLEASE WAIT");

                int[] iImages = GetSelected(false);
                foreach (int i in iImages)
                    EditSingleParameter(i, iParam, sValue, iAction);

                frmTip.ShowMsg(false, "Done changing " + iImages.Length + " values");
            }
        }
        private void QEdit_txtImagName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                QEdit_sName = QEdit_txtImagName.Text
                    .Replace("\r", "").Replace("\n", "");
            QEdit_txtImagName.Text = QEdit_sName;
            if (e.KeyCode == Keys.Enter)
            {
                int iAction = QEdit_ddEnterAction.SelectedIndex + 1;
                if (VK_Ctrl)
                {
                    EditSingleParameter(1, QEdit_txtImagName.Text, iAction);
                    EditSingleParameter(2, QEdit_txtImagComment.Text, iAction);
                    EditSingleParameter(3, QEdit_txtImagRating.Text, iAction);
                    EditSingleParameter(4, QEdit_txtTagsGeneral.Text, iAction);
                    EditSingleParameter(5, QEdit_txtTagsSource.Text, iAction);
                    EditSingleParameter(6, QEdit_txtTagsChars.Text, iAction);
                    EditSingleParameter(7, QEdit_txtTagsArtists.Text, iAction);
                    DispSkip(1); QEdit_txtImagName.SelectAll();
                }
                else
                {
                    EditSingleParameter(1, QEdit_txtImagName.Text, iAction);
                    QEdit_txtImagComment.Focus();
                    QEdit_txtImagComment.SelectAll();
                }
            }
        }
        private void QEdit_txtImagComment_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                QEdit_sComment = QEdit_txtImagComment.Text
                    .Replace("\r", "").Replace("\n", "");
            QEdit_txtImagComment.Text = QEdit_sComment;
            if (e.KeyCode == Keys.Enter)
            {
                int iAction = QEdit_ddEnterAction.SelectedIndex + 1;
                if (VK_Ctrl)
                {
                    EditSingleParameter(1, QEdit_txtImagName.Text, iAction);
                    EditSingleParameter(2, QEdit_txtImagComment.Text, iAction);
                    EditSingleParameter(3, QEdit_txtImagRating.Text, iAction);
                    EditSingleParameter(4, QEdit_txtTagsGeneral.Text, iAction);
                    EditSingleParameter(5, QEdit_txtTagsSource.Text, iAction);
                    EditSingleParameter(6, QEdit_txtTagsChars.Text, iAction);
                    EditSingleParameter(7, QEdit_txtTagsArtists.Text, iAction);
                    DispSkip(1); QEdit_txtImagComment.SelectAll();
                }
                else
                {
                    EditSingleParameter(2, QEdit_txtImagComment.Text, iAction);
                    QEdit_txtImagRating.Focus();
                    QEdit_txtImagRating.SelectAll();
                }
            }
        }
        private void QEdit_txtImagRating_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                QEdit_sRating = QEdit_txtImagRating.Text
                    .Replace("\r", "").Replace("\n", "");
            QEdit_txtImagRating.Text = QEdit_sRating;
            if (e.KeyCode == Keys.Enter)
            {
                int iAction = QEdit_ddEnterAction.SelectedIndex + 1;
                if (VK_Ctrl)
                {
                    EditSingleParameter(1, QEdit_txtImagName.Text, iAction);
                    EditSingleParameter(2, QEdit_txtImagComment.Text, iAction);
                    EditSingleParameter(3, QEdit_txtImagRating.Text, iAction);
                    EditSingleParameter(4, QEdit_txtTagsGeneral.Text, iAction);
                    EditSingleParameter(5, QEdit_txtTagsSource.Text, iAction);
                    EditSingleParameter(6, QEdit_txtTagsChars.Text, iAction);
                    EditSingleParameter(7, QEdit_txtTagsArtists.Text, iAction);
                    DispSkip(1); QEdit_txtImagRating.SelectAll();
                }
                else
                {
                    EditSingleParameter(3, QEdit_txtImagRating.Text, iAction);
                    QEdit_txtTagsGeneral.Focus();
                    QEdit_txtTagsGeneral.SelectAll();
                }
            }
        }
        private void QEdit_txtTagsGeneral_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                QEdit_sTGeneral = QEdit_txtTagsGeneral.Text
                    .Replace("\r", "").Replace("\n", "");
            QEdit_txtTagsGeneral.Text = QEdit_sTGeneral;
            if (e.KeyCode == Keys.Enter)
            {
                int iAction = QEdit_ddEnterAction.SelectedIndex + 1;
                if (VK_Ctrl)
                {
                    EditSingleParameter(1, QEdit_txtImagName.Text, iAction);
                    EditSingleParameter(2, QEdit_txtImagComment.Text, iAction);
                    EditSingleParameter(3, QEdit_txtImagRating.Text, iAction);
                    EditSingleParameter(4, QEdit_txtTagsGeneral.Text, iAction);
                    EditSingleParameter(5, QEdit_txtTagsSource.Text, iAction);
                    EditSingleParameter(6, QEdit_txtTagsChars.Text, iAction);
                    EditSingleParameter(7, QEdit_txtTagsArtists.Text, iAction);
                    DispSkip(1); QEdit_txtTagsGeneral.SelectAll();
                }
                else
                {
                    EditSingleParameter(4, QEdit_txtTagsGeneral.Text, iAction);
                    QEdit_txtTagsSource.Focus();
                    QEdit_txtTagsSource.SelectAll();
                }
            }
        }
        private void QEdit_txtTagsSource_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                QEdit_sTSource = QEdit_txtTagsSource.Text
                    .Replace("\r", "").Replace("\n", "");
            QEdit_txtTagsSource.Text = QEdit_sTSource;
            if (e.KeyCode == Keys.Enter)
            {
                int iAction = QEdit_ddEnterAction.SelectedIndex + 1;
                if (VK_Ctrl)
                {
                    EditSingleParameter(1, QEdit_txtImagName.Text, iAction);
                    EditSingleParameter(2, QEdit_txtImagComment.Text, iAction);
                    EditSingleParameter(3, QEdit_txtImagRating.Text, iAction);
                    EditSingleParameter(4, QEdit_txtTagsGeneral.Text, iAction);
                    EditSingleParameter(5, QEdit_txtTagsSource.Text, iAction);
                    EditSingleParameter(6, QEdit_txtTagsChars.Text, iAction);
                    EditSingleParameter(7, QEdit_txtTagsArtists.Text, iAction);
                    DispSkip(1); QEdit_txtTagsSource.SelectAll();
                }
                else
                {
                    EditSingleParameter(5, QEdit_txtTagsSource.Text, iAction);
                    QEdit_txtTagsChars.Focus();
                    QEdit_txtTagsChars.SelectAll();
                }
            }
        }
        private void QEdit_txtTagsChars_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                QEdit_sTChars = QEdit_txtTagsChars.Text
                    .Replace("\r", "").Replace("\n", "");
            QEdit_txtTagsChars.Text = QEdit_sTChars;
            if (e.KeyCode == Keys.Enter)
            {
                int iAction = QEdit_ddEnterAction.SelectedIndex + 1;
                if (VK_Ctrl)
                {
                    EditSingleParameter(1, QEdit_txtImagName.Text, iAction);
                    EditSingleParameter(2, QEdit_txtImagComment.Text, iAction);
                    EditSingleParameter(3, QEdit_txtImagRating.Text, iAction);
                    EditSingleParameter(4, QEdit_txtTagsGeneral.Text, iAction);
                    EditSingleParameter(5, QEdit_txtTagsSource.Text, iAction);
                    EditSingleParameter(6, QEdit_txtTagsChars.Text, iAction);
                    EditSingleParameter(7, QEdit_txtTagsArtists.Text, iAction);
                    DispSkip(1); QEdit_txtTagsChars.SelectAll();
                }
                else
                {
                    EditSingleParameter(6, QEdit_txtTagsChars.Text, iAction);
                    QEdit_txtTagsArtists.Focus();
                    QEdit_txtTagsArtists.SelectAll();
                }
            }
        }
        private void QEdit_txtTagsArtists_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                QEdit_sTArtists = QEdit_txtTagsArtists.Text
                    .Replace("\r", "").Replace("\n", "");
            QEdit_txtTagsArtists.Text = QEdit_sTArtists;
            if (e.KeyCode == Keys.Enter)
            {
                int iAction = QEdit_ddEnterAction.SelectedIndex + 1;
                if (VK_Ctrl)
                {
                    EditSingleParameter(1, QEdit_txtImagName.Text, iAction);
                    EditSingleParameter(2, QEdit_txtImagComment.Text, iAction);
                    EditSingleParameter(3, QEdit_txtImagRating.Text, iAction);
                    EditSingleParameter(4, QEdit_txtTagsGeneral.Text, iAction);
                    EditSingleParameter(5, QEdit_txtTagsSource.Text, iAction);
                    EditSingleParameter(6, QEdit_txtTagsChars.Text, iAction);
                    EditSingleParameter(7, QEdit_txtTagsArtists.Text, iAction);
                    DispSkip(1); QEdit_txtTagsArtists.SelectAll();
                }
                else
                {
                    EditSingleParameter(7, QEdit_txtTagsArtists.Text, iAction);
                    DispSkip(1);
                    QEdit_txtImagName.Focus();
                    QEdit_txtImagName.SelectAll();
                }
            }
        }

        private void General_cmdWPChanger_Click(object sender, EventArgs e)
        {
            ShowPanel(pnWPChanger);
        }
        private void WPChanger_cmdAddThumbs_Click(object sender, EventArgs e)
        {
            for (int a = 0; a < idImages.Length; a++)
            {
                if (idImages[a].bSel)
                {
                    WPChanger_lstImages.Items.Add(
                        DB.Path + idImages[a].sPath);
                }
            }
        }
        private void WPChanger_cmdRemThumbs_Click(object sender, EventArgs e)
        {
            for (int a = 0; a < idImages.Length; a++)
            {
                if (idImages[a].bSel)
                {
                    for (int b = 0; b < WPChanger_lstImages.Items.Count; b++)
                    {
                        if (WPChanger_lstImages.Items[b].ToString() ==
                            DB.Path + idImages[a].sPath)
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
            string sVal = cb.FileRead("wpchanger.txt").Replace("\r", "");
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
            cb.FileWrite("wpchanger.txt", false, sVal);
        }
        private void WPChanger_cmdChange_Click(object sender, EventArgs e)
        {
            while (!ChangeWP(-1))
            {
                Application.DoEvents();
                if (WPChanger_lstImages.Items.Count == 0) break;
            }
        }
        private bool ChangeWP(string sPath)
        {
            if (!System.IO.File.Exists(sPath)) return false;
            string[] sOldWPs = System.IO.Directory.GetFiles(cb.sAppPath, "_wp_*");
            foreach (string sOldWP in sOldWPs) System.IO.File.Delete(sOldWP);

            Bitmap bWP = (Bitmap)Bitmap.FromFile(sPath);
            sPath = sPath.Substring(sPath.LastIndexOf("/") + 1);
            sPath = sPath.Substring(0, sPath.IndexOf(".") - 1);
            sPath = "_wp_" + sPath + ".bmp";
            bWP.Save(cb.sAppPath + sPath, System.Drawing.Imaging.ImageFormat.Bmp);

            Microsoft.Win32.RegistryKey key =
                Microsoft.Win32.Registry.CurrentUser.
                OpenSubKey("Control Panel\\Desktop", true);
            key.SetValue(@"WallpaperStyle", "2");
            key.SetValue(@"TileWallpaper", "0");
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0,
                sPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            return true;
        }
        private bool ChangeWP(int iPath)
        {
            int iWP = iPath; if (WPChanger_lstImages.Items.Count == 0) return false;
            if (iWP == -1) iWP = rnd.Next(0, WPChanger_lstImages.Items.Count);
            string sFName = WPChanger_lstImages.Items[iWP].ToString();
            if (!ChangeWP(cb.sAppPath + sFName))
            {
                WPChanger_lstImages.Items.RemoveAt(iWP);
                return false;
            }
            return true;
        }
        private void tWPChanger_Tick(object sender, EventArgs e)
        {
            if (!bRunning) return;
            if (WPChanger_txtFrequency.Text != "" &&
                OnlyContains(WPChanger_txtFrequency.Text, "0123456789"))
            {
                int iFreq = Convert.ToInt32(WPChanger_txtFrequency.Text);
                if ((DateTime.Now.Ticks / 10000000) >
                    (dtLastWPChange.Ticks / 10000000) + (iFreq * 60))
                {
                    dtLastWPChange = DateTime.Now;
                    while (!ChangeWP(-1))
                    {
                        Application.DoEvents();
                        if (WPChanger_lstImages.Items.Count == 0) break;
                    }
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
                if (idImages[a].bSel) bAppend[a] = true;
            }
            for (int a = 0; a < Upload_lstImages.Items.Count; a++)
            {
                string sTmp = Upload_lstImages.Items[a].ToString();
                for (int b = 0; b < idImages.Length; b++)
                {
                    if (idImages[b].bSel)
                    {
                        if (sTmp == DB.Path +
                            idImages[b].sPath)
                            bAppend[b] = false;
                    }
                }
            }
            for (int a = 0; a < idImages.Length; a++)
            {
                if (bAppend[a])
                {
                    Upload_lstImages.Items.Add(
                        DB.Path + idImages[a].sPath);
                }
            }
        }
        private void Upload_cmdRemThumbs_Click(object sender, EventArgs e)
        {
            for (int a = 0; a < idImages.Length; a++)
            {
                if (idImages[a].bSel)
                {
                    for (int b = 0; b < Upload_lstImages.Items.Count; b++)
                    {
                        if (Upload_lstImages.Items[b].ToString() ==
                            DB.Path + idImages[a].sPath)
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
        private void Upload_cmdRandomize_Click(object sender, EventArgs e)
        {
            int i = 0;
            string[] saImages =
                new string[Upload_lstImages.Items.Count];
            while (Upload_lstImages.Items.Count > 0)
            {
                int r = rnd.Next(0, Upload_lstImages.Items.Count);
                saImages[i] = Upload_lstImages.Items[r].ToString();
                Upload_lstImages.Items.RemoveAt(r); i++;
            }
            Upload_lstImages.Items.Clear();
            for (int a = 0; a < saImages.Length; a++)
                Upload_lstImages.Items.Add(saImages[a]);
        }
        private void Upload_cmdStart_Click(object sender, EventArgs e)
        {
            if (bInTUpload)
            {
                if (DialogResult.Yes == MessageBox.Show(
                    "I seem to be in the middle of uploading something." + "\r\n\r\n" +
                    "Do you want to force me to think otherwise?", "Hello",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    bInTUpload = false;
            }
            if (!Upload_InSession)
            {
                Upload_iCurrent = 0;
                Upload_iDone = 0;
                Upload_iError_Exists = 0;
                Upload_iError_HiRes = 0;
                Upload_iError_HiSize = 0;
                Upload_iError_iLimit = 0;
                Upload_iError_Other = 0;
            }
            Upload_InSession = true;
            tUpload.Start();
        }
        private void Upload_cmdPause_Click(object sender, EventArgs e)
        {
            tUpload.Stop();
        }
        private void Upload_cmdStop_Click(object sender, EventArgs e)
        {
            tUpload.Stop();
            Upload_InSession = false;
            Upload_txtStatus.Text = "Stopped";
        }
        private void tUpload_Tick(object sender, EventArgs e)
        {
            if (!bRunning) return;
            if (bInTUpload) return; bInTUpload = true;
            if (Upload_txtFrequency.Text != "" &&
                OnlyContains(Upload_txtFrequency.Text, "0123456789"))
            {
                int iFreq = Convert.ToInt32(Upload_txtFrequency.Text);
                if ((DateTime.Now.Ticks / 10000000) >=
                    (dtLastUpload.Ticks / 10000000) + iFreq)
                {
                    if (Upload_lstImages.Items.Count == 0)
                    {
                        bInTUpload = false; tUpload.Stop();
                        Upload_cmdStop_Click(new object(), new EventArgs());
                        return;
                    }

                    //  Prepare random variables
                    string sFPath, sFName, sIdent, sLPath;
                    sFPath = Upload_lstImages.Items[0].ToString();
                    sLPath = sFPath.Substring(sFPath.IndexOf("/") + 1);
                    sFName = sFPath.Substring(sFPath.LastIndexOf("/") + 1);
                    sIdent = sFName.Substring(0, sFName.LastIndexOf("."));
                    sFPath = cb.sAppPath + sFPath;

                    if (!System.IO.File.Exists(sFPath))
                    {
                        Upload_lstCompleted.Items.Add((Upload_iCurrent + 1) +
                            " [SKIP] ~ File not found ~ " + sFName);
                        Upload_iCurrent++; Upload_iError_Img404++;
                        Upload_lstImages.Items.RemoveAt(0);
                        bInTUpload = false; return;
                    }

                    //  Find image in database
                    ImageData idInfo = new ImageData();
                    using (SQLiteCommand DBc = DB.Data.CreateCommand())
                    {
                        DBc.CommandText = "SELECT * FROM 'images'";
                        using (SQLiteDataReader rd = DBc.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                if (rd.GetString(DB.diPath) == sLPath)
                                {
                                    idInfo.sHash = rd.GetString(DB.diHash);
                                    idInfo.sType = rd.GetString(DB.diType);
                                    idInfo.iLen = rd.GetInt32(DB.diFLen);
                                    idInfo.ptRes.X = rd.GetInt32(DB.diXRes);
                                    idInfo.ptRes.Y = rd.GetInt32(DB.diYRes);
                                    idInfo.sName = rd.GetString(DB.diName);
                                    idInfo.sPath = rd.GetString(DB.diPath);
                                    idInfo.cFlag = rd.GetString(DB.diFlag).ToCharArray(0, 32);
                                    idInfo.sCmnt = rd.GetString(DB.diCmnt);
                                    idInfo.iRate = rd.GetInt32(DB.diRate);
                                    idInfo.sTGen = rd.GetString(DB.diTGen);
                                    idInfo.sTSrc = rd.GetString(DB.diTSrc);
                                    idInfo.sTChr = rd.GetString(DB.diTChr);
                                    idInfo.sTArt = rd.GetString(DB.diTArt);
                                }
                            }
                        }
                    }

                    //  Guess the best tags for emTagging
                    System.IO.File.Copy(sFPath, cb.sAppPath + "z_tmp/upload", true);
                    string[] semTags = emTags.TagsRead(sFPath);
                    if (semTags.Length == 0) semTags = new string[6];
                    for (int b = 0; b < semTags.Length; b++)
                        if (semTags[b] == null) semTags[b] = "";

                    if (semTags[0].Length < 3) semTags[0] = idInfo.sName;
                    if (semTags[1].Length < 3) semTags[1] = idInfo.sCmnt;
                    semTags[2] = cb.RemoveDupes(idInfo.sTGen + ", " + semTags[2]);
                    semTags[3] = cb.RemoveDupes(idInfo.sTSrc + ", " + semTags[3]);
                    semTags[4] = cb.RemoveDupes(idInfo.sTChr + ", " + semTags[4]);
                    semTags[5] = cb.RemoveDupes(idInfo.sTArt + ", " + semTags[5]);
                    emTags.TagsWrite(cb.sAppPath + "z_tmp/upload", true, semTags);

                    //  Prepare file for upload
                    System.IO.FileStream fs = System.IO.File.Open(cb.sAppPath + "z_tmp/upload",
                        System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    byte[] bImg = new byte[fs.Length];
                    fs.Read(bImg, 0, bImg.Length); fs.Close(); fs.Dispose();
                    System.IO.File.Delete(cb.sAppPath + "z_tmp/upload");



                    if (Upload_ddURL.Text == "http://www.imagehost.org/")
                    {
                        try
                        {
                            Upload_txtStatus.Text = "Preparing..."; Application.DoEvents();
                            string sPostTo = "http://www.imagehost.org/";
                            string sRefer = "http://www.imagehost.org/";

                            WebReq WReq = new WebReq();
                            string sMPBound = "----------" + WReq.RandomChars(22);
                            string sPD1 = "--" + sMPBound + "\r\n" +
                                "Content-Disposition: form-data; name=\"a\"" + "\r\n" +
                                "\r\n" + "upload" + "\r\n" + "--" + sMPBound + "\r\n" +
                                "Content-Disposition: form-data; name=\"file[]\"; " +
                                "filename=\"" + sFName + "\r\n" +
                                "Content-Type: image/jpeg" + "\r\n" +
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

                            Upload_txtStatus.Text =
                                "Uploading image " + (Upload_iCurrent + 1) +
                                " of " + Upload_lstImages.Items.Count;
                            WReq.Request(sPostTo, whc, bPD, sMPBound, 3, "", true);
                            while (!WReq.isReady)
                            {
                                Application.DoEvents();
                                System.Threading.Thread.Sleep(1);
                            }
                            dtLastUpload = DateTime.Now;
                            if (WReq.sResponse.Contains(sIdent))
                            {
                                string sImgURL = Split(Split(Split(WReq.sResponse,
                                    "nowrap\">Text Link</td>")[1], " size=\"50\" value=\"")[1], "\" />")[0];
                                Upload_lstCompleted.Items.Add((Upload_iCurrent + 1) + " ~ " + sImgURL);
                                Upload_lstImages.Items.RemoveAt(0);
                                Upload_iCurrent++; Upload_iDone++;
                            }
                            else
                            {
                                Upload_iError_Other++;
                                Upload_lstCompleted.Items.Add((Upload_iCurrent + 1) + " [ERROR]");
                                //dtLastUpload = DateTime.Now.AddSeconds(
                                //    -(Convert.ToInt32(Upload_txtFrequency.Text) - 5));
                            }
                        }
                        catch { }
                    }
                    else //assume 4chan
                    {
                        try
                        {
                            int iPrmAttempt = 0; bool bPrmRetry = true, bPrmOK = true;
                            string sRaw = "", sPostTo = "", sRefer = "";
                            int iSizeLim = 0, iThreadID = 0, iPass = 0;
                            while (bPrmRetry)
                            {
                                bPrmRetry = false; iPrmAttempt++;
                                Upload_txtStatus.Text = "Fetching parameters (attempt " +
                                    iPrmAttempt + ")"; Application.DoEvents();
                                WebReq WPrep = new WebReq();
                                sRefer = Upload_ddURL.Text;
                                WPrep.Request(sRefer);
                                while (!WPrep.isReady)
                                {
                                    Application.DoEvents();
                                    System.Threading.Thread.Sleep(1);
                                }
                                sRaw = WPrep.sResponse;
                                if (sRaw.StartsWith("<WebReq_Error>"))
                                {
                                    bPrmRetry = true;
                                    if (sRaw == "<WebReq_Error>#02-0004_404</WebReq_Error>")
                                    {
                                        bPrmRetry = false;
                                        bPrmOK = false;
                                    }
                                }
                                sPostTo = Split(Split(sRaw, "<form name=\"post\" action=\"")[1], "\"")[0];
                                iSizeLim = Convert.ToInt32(Split(Split(sRaw, "name=\"MAX_FILE_SIZE\" value=\"")[1], "\"")[0]);
                                iThreadID = Convert.ToInt32(Split(Split(sRaw, "name=resto value=\"")[1], "\"")[0]);
                                iPass = Convert.ToInt32(Upload_txtPass.Text);
                            }

                            if (bPrmOK)
                            {
                                bool bResume = false;
                                while (!bResume)
                                {
                                    bResume = true;
                                    if (Upload_lstImages.Items.Count == 0)
                                    {
                                        bResume = false; break;
                                    }
                                    Upload_txtStatus.Text = "Preparing"; Application.DoEvents();

                                    if (bImg.Length > iSizeLim)
                                    {
                                        Upload_lstCompleted.Items.Add((Upload_iCurrent + 1) +
                                            " [SKIP] ~ Size above max ~ " + idInfo.sHash);
                                        bResume = false;
                                        Upload_lstImages.Items.RemoveAt(0);
                                        Upload_iError_HiSize++;
                                        Upload_iCurrent++;
                                        break;
                                    }
                                }
                                if (bResume)
                                {
                                    string sComment = Upload_txtComment.Text
                                        .Replace("[[appver]]", cb.sAppVer)
                                        .Replace("[[iname]]", idInfo.sName)
                                        .Replace("[[icomment]]", idInfo.sCmnt)
                                        .Replace("[[irating]]", idInfo.iRate + "")
                                        .Replace("[[tgeneral]]", idInfo.sTGen.Replace(",", ", "))
                                        .Replace("[[tsource]]", idInfo.sTSrc.Replace(",", ", "))
                                        .Replace("[[tchars]]", idInfo.sTChr.Replace(",", ", "))
                                        .Replace("[[tartists]]", idInfo.sTArt.Replace(",", ", "))
                                        .Replace("[[i1]]", (Upload_iCurrent + 1) + "")
                                        .Replace("[[i2]]", (Upload_lstImages.Items.Count - 1) + "")
                                        .Replace("[[hms]]", System.DateTime.Now.ToLongTimeString());

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
                                        "\r\n" + "pimgdb@gmail.com" + "\r\n" + "--" + sMPBound + "\r\n" +
                                        //"\r\n" + "" + "\r\n" + "--" + sMPBound + "\r\n" +
                                        "Content-Disposition: form-data; name=\"com\"" + "\r\n" +
                                        "\r\n" + sComment + "\r\n" + "--" + sMPBound + "\r\n" +
                                        "Content-Disposition: form-data; name=\"upfile\"; filename=\"pImgDB v" + cb.sAppVer + " .wat\"" +
                                        //"Content-Disposition: form-data; name=\"upfile\"; filename=\"You just lost the game\"" +
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
                                    //whc.Add("Expect: 100-continue");
                                    whc.Add("Referer: " + sRefer);

                                    Upload_txtStatus.Text =
                                        "Uploading image " + (Upload_iCurrent + 1) +
                                        " of " + Upload_lstImages.Items.Count;
                                    Application.DoEvents();
                                    WReq.Request(sPostTo, whc, bPD, sMPBound, 3, "", true);
                                    while (!WReq.isReady)
                                    {
                                        Application.DoEvents();
                                        System.Threading.Thread.Sleep(1);
                                    }
                                    dtLastUpload = DateTime.Now;
                                    string sEPre = "<font color=red size=5 style=\"\"><b>";
                                    if (WReq.sResponse.Contains(" uploaded! Updating page.<!-- thread:"))
                                    {
                                        Upload_lstCompleted.Items.Add((Upload_iCurrent + 1) +
                                            " [OK] ~ " + idInfo.sHash);
                                        Upload_lstImages.Items.RemoveAt(0);
                                        Upload_iCurrent++; Upload_iDone++;
                                    }
                                    else if (WReq.sResponse.Contains(sEPre + "Error: Flood detected"))
                                    {
                                        Upload_lstCompleted.Items.Add((Upload_iCurrent + 1) +
                                            " [REDO] ~ Flood detected");
                                        Upload_iError_iFlood++;
                                        dtLastUpload = DateTime.Now.AddSeconds(
                                            -(Convert.ToInt32(Upload_txtFrequency.Text) - 5));
                                    }
                                    else if (WReq.sResponse.Contains(sEPre + "<a href=\"http://") &&
                                        WReq.sResponse.Contains("\">Error: Duplicate file entry detected.</a><br><br>"))
                                    {
                                        Upload_lstCompleted.Items.Add((Upload_iCurrent + 1) +
                                            " [SKIP] ~ Duplicate file ~ " + idInfo.sHash);
                                        Upload_lstImages.Items.RemoveAt(0);
                                        Upload_iCurrent++; Upload_iError_Exists++;
                                        dtLastUpload = DateTime.Now.AddSeconds(
                                            -(Convert.ToInt32(Upload_txtFrequency.Text) - 5));
                                    }
                                    else if (WReq.sResponse.Contains(sEPre + "Max limit of "))
                                    {
                                        Upload_lstCompleted.Items.Add((Upload_iCurrent + 1) +
                                            " [STOP] ~ Thread full");
                                        Upload_cmdStop_Click(new object(), new EventArgs());
                                    }
                                    else if (WReq.sResponse.Contains(sEPre + "Error: Image resolution is too large.<br><br>"))
                                    {
                                        Upload_lstCompleted.Items.Add((Upload_iCurrent + 1) +
                                            " [SKIP] ~ Res above max ~ " + idInfo.sHash);
                                        Upload_lstImages.Items.RemoveAt(0);
                                        Upload_iCurrent++; Upload_iError_HiRes++;
                                        dtLastUpload = DateTime.Now.AddSeconds(
                                            -(Convert.ToInt32(Upload_txtFrequency.Text) - 5));
                                    }
                                    else if (WReq.sResponse.Contains(sEPre + "Error: File too large.<br><br>"))
                                    {
                                        Upload_lstCompleted.Items.Add((Upload_iCurrent + 1) +
                                            " [SKIP] ~ Size above max ~ " + idInfo.sHash);
                                        Upload_lstImages.Items.RemoveAt(0);
                                        Upload_iCurrent++; Upload_iError_HiSize++;
                                        dtLastUpload = DateTime.Now.AddSeconds(
                                            -(Convert.ToInt32(Upload_txtFrequency.Text) - 5));
                                    }
                                    if (WReq.sResponse.Contains(sEPre + "Image file contains embedded archive.<br><br>"))
                                    {
                                        Upload_lstCompleted.Items.Add((Upload_iCurrent + 1) +
                                            " [SKIP] ~ Contains archive ~ " + idInfo.sHash);
                                        Upload_lstImages.Items.RemoveAt(0);
                                        Upload_iCurrent++; Upload_iError_iEmbed++;
                                        dtLastUpload = DateTime.Now.AddSeconds(
                                            -(Convert.ToInt32(Upload_txtFrequency.Text) - 5));
                                    }
                                    if (WReq.sResponse != "")
                                    {
                                        Clipboard.SetText(WReq.sResponse);
                                        Clipboard.Clear();
                                    }
                                }
                            }
                            else
                            {
                                Upload_lstCompleted.Items.Add((Upload_iCurrent + 1) +
                                    " [STOP] ~ 404 (wat)");
                                Upload_cmdStop_Click(new object(), new EventArgs());
                            }
                        }
                        catch { }
                    }
                    Upload_txtStatus.Text = "Next: " + (Upload_iCurrent + 1) +
                        " of " + Upload_lstImages.Items.Count + " (" +
                        (Upload_iError_Exists + Upload_iError_iLimit +
                        Upload_iError_HiRes + Upload_iError_HiSize +
                        Upload_iError_iEmbed + Upload_iError_iFlood +
                        Upload_iError_Other) + " errors)";
                    Application.DoEvents();
                }
            }
            bInTUpload = false;
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
        private void Upload_cmdCopyURLs_Click(object sender, EventArgs e)
        {
            string sRet = "";
            for (int a = 0; a < Upload_lstCompleted.Items.Count; a++)
            {
                string sTmp = Upload_lstCompleted.Items[a].ToString();
                if (sTmp.Contains("http://"))
                {
                    sRet += sTmp.Substring(sTmp.IndexOf("http://")) + "\r\n";
                }
            }
            if (sRet != "")
            {
                Clipboard.Clear();
                Clipboard.SetText(sRet);
            }
        }
        private void rcmThumbs_Export_Upload_Click(object sender, EventArgs e)
        {
            int[] iImages = GetSelected();
            if (DialogResult.No == MessageBox.Show(
                "Do you wish to add the " + iImages.Length + " selected" + "\r\n" +
                "images to the uploader?", "Hello", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)) return;

            bool[] bSel = new bool[idImages.Length];
            for (int a = 0; a < idImages.Length; a++)
            {
                bSel[a] = idImages[a].bSel;
                idImages[a].bSel = false;
            }
            for (int a = 0; a < iImages.Length; a++)
                idImages[iImages[a]].bSel = true;

            Upload_cmdAddThumbs_Click(new object(), new EventArgs());
            for (int a = 0; a < idImages.Length; a++)
                idImages[a].bSel = bSel[a];
        }

        private void niTray_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.Visible) this.Visible = false;
            else this.Visible = true;
        }
        private void General_cmdReadme_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://praetox.com/site/software/pimgdb-manual.html");
        }

        private void SearchInit_cmdColor1_Click(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = pnSearchInit.BackColor;
        }
        private void SearchInit_cmdColor2_Click(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = pnSearchInit.BackColor;
        }
        private void SearchInit_cmdColor1_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog(); cd.ShowDialog();
            ((PictureBox)sender).BackColor = cd.Color;
        }
        private void SearchInit_cmdColor2_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog(); cd.ShowDialog();
            ((PictureBox)sender).BackColor = cd.Color;
        }
        private void SearchInit_txtAnyfield_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SearchInit_cmdStart_Click(new object(), new EventArgs());
        }
        private void SearchInit_txtImageName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SearchInit_cmdStart_Click(new object(), new EventArgs());
        }
        private void SearchInit_txtImageFormat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SearchInit_cmdStart_Click(new object(), new EventArgs());
        }
        private void SearchInit_txtImageRes_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SearchInit_cmdStart_Click(new object(), new EventArgs());
        }
        private void SearchInit_txtImageRating_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SearchInit_cmdStart_Click(new object(), new EventArgs());
        }
        private void SearchInit_txtTagsGeneral_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SearchInit_cmdStart_Click(new object(), new EventArgs());
        }
        private void SearchInit_txtTagsSource_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SearchInit_cmdStart_Click(new object(), new EventArgs());
        }
        private void SearchInit_txtTagsChars_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SearchInit_cmdStart_Click(new object(), new EventArgs());
        }
        private void SearchInit_txtTagsArtists_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SearchInit_cmdStart_Click(new object(), new EventArgs());
        }

        private void rcmThumbs_FindDupes_Click(object sender, EventArgs e)
        {
            frmWait.sHeader = "";
            frmWait.sMain = "Dupe identifier";
            frmWait.sFooter = "Initiating...";
            frmWait.bVisible = true;
            frmWait.bInstant = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            ImageData idO = new ImageData();
            idO.sHash = idImages[thPages[1].idRange.X + iThClicked].sHash;
            idO.sThmb = idImages[thPages[1].idRange.X + iThClicked].sThmb;
            iThumbPageR = iThumbPage;
            idImagesR = idImages;
            thPagesR = new thPageData[thPages.Length];
            for (int a = 0; a < thPages.Length; a++)
            {
                thPagesR[a] = new thPageData();
                thPagesR[a].idRange = thPages[a].idRange;
                thPagesR[a].sHashes = new string[thPages[a].sHashes.Length];
                for (int b = 0; b < thPages[a].sHashes.Length; b++)
                    thPagesR[a].sHashes[b] = thPages[a].sHashes[b];

                thPagesR[a].bImages = new Bitmap[thPages[a].bImages.Length];
                for (int b = 0; b < thPages[a].bImages.Length; b++)
                {
                    if (thPagesR[a].bImages[b] != null) thPagesR[a].bImages[b].Dispose();
                    if (thPages[a].bImages[b] != null) thPagesR[a].bImages[b] =
                       (Bitmap)thPages[a].bImages[b].Clone();
                }
            }

            frmWait.sFooter = "Caching images";
            ImageData[] idC = DB.Search(new SearchPrm(), 1);

            string sClones = "";
            for (int a = 0; a < idC.Length; a++)
            {
                int iDiff = 0; int iThreshold = 20;
                frmWait.sFooter = a + " of " + idC.Length;
                for (int i = 0; i < 192; i += 1)
                {
                    int iThDiff = (int)Math.Abs(
                        idO.sThmb[i] - idC[a].sThmb[i]);
                    if (iThDiff < iThreshold) iDiff += iThDiff;
                    if (iThDiff >= iThreshold) iDiff += iThDiff * 4;
                }
                iDiff = (int)Math.Round(((double)iDiff * 2) / 192);
                //if (idC[a].sName.StartsWith(idO.sName))
                if (iDiff < iThreshold)
                    //MessageBox.Show(iDiff + "\r\n\r\n" +
                    //    idO.sName + "\r\n" +
                    //    idC[a].sName);
                    sClones += a + ",";
            }

            if (sClones != "")
            {
                sClones = sClones.Substring(0, sClones.Length - 1);
                string[] saClones = sClones.Split(',');
                idImages = new ImageData[saClones.Length];
                for (int a = 0; a < saClones.Length; a++)
                {
                    int iClone = Convert.ToInt32(saClones[a]);
                    idImages[a] = idC[iClone];
                }

                iThumbPage = 1;
                SetThumbPageRanges(0);
                RedrawThumbnails(false);
                ReloadThumbnails(false);
                iAMode = 1;
            }
            frmWait.bVisible = false;
        }
        private void rcmThumbs_View_Home_Click(object sender, EventArgs e)
        {
            MoveHomewards();
        }
        private void rcmThumbs_Edit_Rate_Clr_Click(object sender, EventArgs e)
        {
            EditSingleParameter(thPages[1].idRange.X + iThClicked, 3, "-1", 1);
        }
        private void rcmThumbs_Edit_Rate_1_Click(object sender, EventArgs e)
        {
            EditSingleParameter(thPages[1].idRange.X + iThClicked, 3, "1", 1);
        }
        private void rcmThumbs_Edit_Rate_2_Click(object sender, EventArgs e)
        {
            EditSingleParameter(thPages[1].idRange.X + iThClicked, 3, "2", 1);
        }
        private void rcmThumbs_Edit_Rate_3_Click(object sender, EventArgs e)
        {
            EditSingleParameter(thPages[1].idRange.X + iThClicked, 3, "3", 1);
        }
        private void rcmThumbs_Edit_Rate_4_Click(object sender, EventArgs e)
        {
            EditSingleParameter(thPages[1].idRange.X + iThClicked, 3, "4", 1);
        }
        private void rcmThumbs_Edit_Rate_5_Click(object sender, EventArgs e)
        {
            EditSingleParameter(thPages[1].idRange.X + iThClicked, 3, "5", 1);
        }
        private void rcmThumbs_Edit_Rate_6_Click(object sender, EventArgs e)
        {
            EditSingleParameter(thPages[1].idRange.X + iThClicked, 3, "6", 1);
        }
        private void rcmThumbs_Selext_All_Click(object sender, EventArgs e)
        {
            SelectAllToggle(true);
        }
        private void rcmThumbs_Select_Invert_Click(object sender, EventArgs e)
        {
            SelectInvToggle(true);
        }
        private void rcmThumbs_Selext_SetAsView_Click(object sender, EventArgs e)
        {
            SelectAsView(true);
        }
        private void rcmThumbs_View_inExplorer_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = cb.sAppPath + DB.Path +
                idImages[thPages[1].idRange.X + iThClicked].sPath;
            prc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
            prc.Start();
        }
        private void rcmThumbs_ShowDetails_Click(object sender, EventArgs e)
        {
            if (idImages == null) return;
            if (iThClicked == -1) return;
            int iThisID = thPages[1].idRange.X + iThClicked;
            if (iThisID > idImages.Length) return;
            string sRet =
                "Detailed information for image #" + iThisID.ToString("X4") + "\r\n" +
                "--------------------------------" + "\r\n" +
                "\r\n" +
                "isModified: " + idImages[iThisID].bMod + "\r\n" +
                "isSelected: " + idImages[iThisID].bSel + "\r\n" +
                "isDeleted: " + idImages[iThisID].bDel + "\r\n" +
                "imgFormat: " + idImages[iThisID].sType + "\r\n" +
                "imgHash: " + idImages[iThisID].sHash + "\r\n" +
                "imgFlags: " + new string(idImages[iThisID].cFlag) + "\r\n" +
                "imgPath: " + idImages[iThisID].sPath + "\r\n" +
                "imgThumb: " + "[[UNPRINTABLE]]" + "\r\n" +
                "imgRes: " + idImages[iThisID].ptRes + "\r\n" +
                "imgLen: " + (idImages[iThisID].iLen / 1024) + " kB" + "\r\n" +
                "imgName: " + idImages[iThisID].sName + "\r\n" +
                "imgCmnt: " + idImages[iThisID].sCmnt + "\r\n" +
                "imgRate: " + idImages[iThisID].iRate + "\r\n" +
                "tagGen: " + idImages[iThisID].sTGen + "\r\n" +
                "tagSrc: " + idImages[iThisID].sTSrc + "\r\n" +
                "tagArt: " + idImages[iThisID].sTArt + "\r\n" +
                "tagChr: " + idImages[iThisID].sTChr;
            MessageBox.Show(sRet, "Hello", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void rcmThumbs_Edit_Move_Click(object sender, EventArgs e)
        {
            MessageBox.Show("In the following file selector, enter a random" + "\r\n" +
                "filename once you're in the correct folder." + "\r\n" +
                "Confirm your choice by hitting Enter." + "\r\n\r\n" +
                "Note that you can only copy to other" + "\r\n" +
                "folders within the database folder!", "Hello",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            string sRoot = cb.sAppPath + DB.Path;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.CheckFileExists = false; sfd.CheckPathExists = false;
            sfd.CreatePrompt = false; sfd.OverwritePrompt = false;
            sfd.InitialDirectory = sRoot.Replace("/", "\\");
            sfd.ShowDialog();

            string sSelPath = sfd.FileName.Replace("\\", "/");
            sSelPath = sSelPath.Substring(0, sSelPath.LastIndexOf('/') + 1);
            if (!sSelPath.StartsWith(sRoot)) return;
            sSelPath = sSelPath.Substring(sRoot.Length);

            int[] iImages = GetSelected();
            if (DialogResult.No == MessageBox.Show(
                "Move the " + iImages.Length + " selected images to this directory?" + "\r\n" +
                sSelPath, "Hello", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) return;

            for (int a = 0; a < iImages.Length; a++)
            {
                int i = iImages[a];
                string sOriginal = idImages[i].sPath;
                string sFilename = sOriginal.Substring(sOriginal.LastIndexOf('/') + 1);
                idImages[i].sPath = sSelPath + sFilename;
            }
            GUI_SaveChanges(true);
        }
        private void rcmThumbs_Edit_Delete_Click(object sender, EventArgs e)
        {
            int iThisID = thPages[1].idRange.X + iThClicked;
            ChangeDeleteState(iThisID, true, true);
        }
        private void rcmThumbs_Edit_Undelete_Click(object sender, EventArgs e)
        {
            int iThisID = thPages[1].idRange.X + iThClicked;
            ChangeDeleteState(iThisID, false, true);
        }
        private void ChangeDeleteState(int iThisID, bool bDeleted, bool bSpreadThroughSelection)
        {
            string sPrompt = "";
            if (bDeleted) sPrompt = "Delete";
            if (!bDeleted) sPrompt = "Undelete";

            if (!idImages[iThisID].bSel || !bSpreadThroughSelection)
            {
                if (DialogResult.Yes == MessageBox.Show(sPrompt + " this single image?",
                    "O SHI-", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    idImages[thPages[1].idRange.X + iThClicked].bMod = true;
                    idImages[thPages[1].idRange.X + iThClicked].bDel = bDeleted;
                }
            }
            else
            {
                int iDelCnt = 0;
                for (int a = 0; a < idImages.Length; a++)
                    if (idImages[a].bSel) iDelCnt++;

                if (DialogResult.Yes == MessageBox.Show(sPrompt + " the " + iDelCnt + " selected images?",
                    "O SHI-", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    for (int a = 0; a < idImages.Length; a++)
                        if (idImages[a].bSel)
                        {
                            idImages[a].bMod = true;
                            idImages[a].bDel = bDeleted;
                        }
                }
            }
            RedrawThumbnails(false);
        }

        private void tTenSec_Tick(object sender, EventArgs e)
        {
            if (bQThumbs)
            {
                try
                {
                    long lTck = DateTime.Now.Ticks / 10000000;
                    string[] sFiles = System.IO.Directory.GetFiles(cb.sAppPath + DB.Path + "_th");
                    for (int a = 0; a < sFiles.Length; a++)
                    {
                        long iFTck = System.IO.File.GetLastAccessTime(sFiles[a]).Ticks / 10000000;
                        if (lTck - iFTck > iQThumbsDel)
                            try { System.IO.File.Delete(sFiles[a]); }
                            catch { }
                    }
                }
                catch { }
            }
        }

        private void General_txtQThumbsDel_TextChanged(object sender, EventArgs e)
        {
            try { iQThumbsDel = Convert.ToInt32(General_txtQThumbsDel.Text); }
            catch { }
        }
        private void General_txtQThumbs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sRes = General_txtQThumbs.Text
                    .Replace(" ", "x").Replace("-", "x").Replace(",", "x").Replace(".", "x")
                    .Replace("/", "x").Replace(":", "x").Replace(";", "x").Replace("*", "x");
                while (sRes.Contains("xx")) sRes = sRes.Replace("xx", "x");
                string[] saRes = sRes.Split('x');
                pQThumbs = new Point(
                    Convert.ToInt16(saRes[0]),
                    Convert.ToInt16(saRes[1]));
            }
            catch { }
        }
        private void General_chkQThumbs_CheckedChanged(object sender, EventArgs e)
        {
            bQThumbs = General_chkQThumbs.Checked;
        }
        private void General_txtThumbs_KeyUp(object sender, KeyEventArgs e)
        {
            int iDist = 0;
            if (e.KeyCode == Keys.Up) iDist++;
            if (e.KeyCode == Keys.Down) iDist--;
            if (iDist != 0)
            {
                General_txtThumbs.Text = "" + (Convert.ToInt32(General_txtThumbs.Text) + iDist);
                RedrawThumbnails(true); SetThumbPageRanges(thPages[1].idRange.X); ReloadThumbnails(false);
                //General_cmdRedraw_Click(new object(), new EventArgs());
            }
        }
        private void General_txtThumbs_TextChanged(object sender, EventArgs e)
        {
            try { iThumbCnt = Convert.ToInt32(General_txtThumbs.Text); }
            catch { }
        }
        private void General_txtResMul_TextChanged(object sender, EventArgs e)
        {
            try { dResMul = Convert.ToDouble(General_txtResMul.Text); }
            catch { }
        }

        private void Display_pbDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DispClick = e.Location;
                frmZoom.ptLoc1 = new Point(e.X + 0, e.Y + 0);
                frmZoom.ptLoc2 = new Point(e.X + 0, e.Y + 0);
                frmZoom.ptOffs = new Point(
                    Cursor.Position.X - e.X,
                    Cursor.Position.Y - e.Y);
            }
        }
        private void Display_pbDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int iX = e.X; int iY = e.Y;
                int iX1 = Display_pbDisplay.Left;
                int iX2 = Display_pbDisplay.Right;
                int iY1 = Display_pbDisplay.Top;
                int iY2 = Display_pbDisplay.Bottom;
                if (iX < iX1) iX = iX1; if (iX > iX2) iX = iX2;
                if (iY < iY1) iY = iY1; if (iY > iY2) iY = iY2;

                int iXD = (int)Math.Abs(DispClick.X - iX);
                int iYD = (int)Math.Abs(DispClick.Y - iY);
                if (iXD > 4 || iYD > 4)
                {
                    frmZoom.ptLoc2 = new Point(iX, iY);
                    frmZoom.bShow = true;
                }
            }
        }
        private void Display_pbDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!frmZoom.bShow) DispClose();
                if (frmZoom.bShow)
                {
                    cmdDummy.Focus();
                    frmZoom.sLabel = ":: Please wait ::";
                    while (frmZoom.sLabel != "") Application.DoEvents();
                    frmZoom.bShow = false;

                    if (DispSizes[1].X == -1)
                    {
                        ZoomRect = new Rectangle(-1, -1, -1, -1);
                        DispLoad(new Bitmap(cb.sAppPath + DB.Path +
                            idImages[DispImageIndex].sPath),
                            new Rectangle());
                    }
                    else
                    {
                        double dPW = DispSizes[1].X; double dDW = Display_pbDisplay.Width;
                        double dPH = DispSizes[1].Y; double dDH = Display_pbDisplay.Height;
                        double dDX1 = Math.Min(DispClick.X, e.X);
                        double dDX2 = Math.Max(DispClick.X, e.X);
                        double dDY1 = Math.Min(DispClick.Y, e.Y);
                        double dDY2 = Math.Max(DispClick.Y, e.Y);

                        double dAW = dDW / dDH; double dAR = dPW / dPH;
                        if (dAW > dAR) // Screen too wide
                        {
                            double dSDW = (dDW - (dDH * dAR)) / 2;
                            dDW -= dSDW * 2; dDX1 -= dSDW; dDX2 -= dSDW;
                        }
                        if (dAW < dAR) // Screen too tall
                        {
                            double dSDH = (dDH - (dDW / dAR)) / 2;
                            dDH -= dSDH * 2; dDY1 -= dSDH; dDY2 -= dSDH;
                        }

                        dDX1 *= (dPW / dDW); dDY1 *= (dPH / dDH);
                        dDX2 *= (dPW / dDW); dDY2 *= (dPH / dDH);
                        if (dDX1 < 0) dDX1 = 0; if (dDX2 > dPW) dDX2 = dPW - 1;
                        if (dDY1 < 0) dDY1 = 0; if (dDY2 > dPH) dDY2 = dPH - 1;
                        ZoomRect = new Rectangle((int)dDX1, (int)dDY1,
                            (int)(dDX2 - dDX1), (int)(dDY2 - dDY1));

                        View_cmdNext.Focus();
                        DispLoad(new Bitmap(cb.sAppPath + DB.Path +
                            idImages[DispImageIndex].sPath), ZoomRect);
                    }
                }
            }
        }

        private void General_cmdTagsOV_Click(object sender, EventArgs e)
        {
            ShowPanel(pnQTagovCP);
        }
        private void QTagovCP_cmdActivateConfig_Click(object sender, EventArgs e)
        {
            try
            {
                string sRes = QTagovCP_txtThRowsCols.Text
                    .Replace(" ", "x").Replace("-", "x").Replace(",", "x").Replace(".", "x")
                    .Replace("/", "x").Replace(":", "x").Replace(";", "x").Replace("*", "x");
                while (sRes.Contains("xx")) sRes = sRes.Replace("xx", "x");
                string[] saRes = sRes.Split('x');
                pQTagovEnum = new Point(
                    Convert.ToInt16(saRes[0]),
                    Convert.ToInt16(saRes[1]));
            }
            catch { }
            bQTagovEnabled = QTagovCP_chkEnabled.Checked;

            string sFields = "";
            if (QTagovCP_chkEn_iN.Checked) sFields += "iN,";
            if (QTagovCP_chkEn_iC.Checked) sFields += "iC,";
            if (QTagovCP_chkEn_iP.Checked) sFields += "iP,";
            if (QTagovCP_chkEn_iR.Checked) sFields += "iR,";
            if (QTagovCP_chkEn_TG.Checked) sFields += "TG,";
            if (QTagovCP_chkEn_TS.Checked) sFields += "TS,";
            if (QTagovCP_chkEn_TC.Checked) sFields += "TC,";
            if (QTagovCP_chkEn_TA.Checked) sFields += "TA,";
            if (sFields == "")
            {
                sQTagovFields = new string[0];
            }
            else
            {
                sFields = sFields.Substring(0, sFields.Length - 1);
                sQTagovFields = sFields.Split(',');
            }

            General_cmdRedraw_Click(new object(), new EventArgs());
        }
        private void QTagovCP_cmdRedraw_Click(object sender, EventArgs e)
        {
            General_cmdRedraw_Click(new object(), new EventArgs());
        }
        private void QTagovCP_cmdReload_Click(object sender, EventArgs e)
        {
            General_cmdReload_Click(new object(), new EventArgs());
        }

        private void General_cmdFBindsMk_Click(object sender, EventArgs e)
        {
            ShowPanel(pnQFBind);
        }
        private void QFBind_cmdSrcPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog(); if (fbd.SelectedPath == "") return;
            QFBind_txtSrcPath.Text = fbd.SelectedPath;
            QFBind_cmdRefresh_Click(new object(), new EventArgs());
        }
        private void QFBind_cmdDstPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog(); if (fbd.SelectedPath == "") return;
            QFBind_txtDstPath.Text = fbd.SelectedPath;
        }
        private void QFBind_cmdMovPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog(); if (fbd.SelectedPath == "") return;
            QFBind_txtMovPath.Text = fbd.SelectedPath;
        }
        private void QFBind_cmdRefresh_Click(object sender, EventArgs e)
        {
            string[] sFiles = System.IO.Directory.GetFiles
                (QFBind_txtSrcPath.Text);
            QFBind_lstFiles.Items.Clear();
            foreach (string s in sFiles)
                QFBind_lstFiles.Items.Add(s);
        }
        private void QFBind_lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (QFBind_chkAutoFN.Checked)
            {
                string sFN = QFBind_lstFiles.SelectedItem.ToString();
                sFN = sFN.Replace("\\", "/");
                sFN = sFN.Substring(sFN.LastIndexOf("/") + 1);
                QFBind_txtFilename.Text = sFN;
            }
        }
        private void QFBind_lstFiles_DoubleClick(object sender, EventArgs e)
        {
            Application.DoEvents();
            QFBind_cmdCreate_Click(new object(), new EventArgs());
        }
        private void QFBind_cmdCreate_Click(object sender, EventArgs e)
        {
            if (DispImageIndex < 0)
            {
                MessageBox.Show("Please identify the image to bind by opening it first.",
                    "You are doing it wrong.", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation); return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            string sFilter = "*." + idImages[DispImageIndex].sType;
            sfd.Filter = "Source format (" + sFilter + ")|" + sFilter;
            sfd.InitialDirectory = QFBind_txtDstPath.Text;
            sfd.OverwritePrompt = true; sfd.ShowDialog();
            string sOutPath = sfd.FileName;
            if (sOutPath == "") return;

            string sFile1 = cb.sAppPath + DB.Path + idImages[DispImageIndex].sPath;
            string sFile2 = QFBind_lstFiles.SelectedItem.ToString();
            string sName = QFBind_txtFilename.Text;
            PFBind_MakeGUI(sFile1, sFile2, sName, sOutPath);
            if (QFBind_chkMov.Checked)
                System.IO.File.Move(sFile2, QFBind_txtMovPath.Text);
        }
        private void PFBind_MakeGUI(string sFile1, string sFile2, string sName, string sOut)
        {
            frmWait.sMain = "Creating FBind";
            frmWait.bVisible = true;
            frmWait.bInstant = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            pFBind.Make(sFile1, sFile2, sName, sOut);
            frmWait.sMain = "FBind created";
            frmWait.bVisible = false;
        }

        private void General_cmdFBindsEx_Click(object sender, EventArgs e)
        {
            ShowPanel(pnFBindsEx);
        }
        private void FBindEx_cmdSelAll_Click(object sender, EventArgs e)
        {
            for (int a = 0; a < FBindEx_lstFiles.Items.Count; a++)
                FBindEx_lstFiles.SelectedItems.Add(FBindEx_lstFiles.Items[a]);
        }
        private void FBindEx_cmdSelInv_Click(object sender, EventArgs e)
        {
            bool[] bSelected = new bool[FBindEx_lstFiles.Items.Count];
            for (int a = 0; a < FBindEx_lstFiles.SelectedIndices.Count; a++)
                bSelected[FBindEx_lstFiles.SelectedIndices[a]] = true;
            FBindEx_lstFiles.SelectedItems.Clear();
            for (int a = 0; a < FBindEx_lstFiles.Items.Count; a++)
                if (!bSelected[a]) FBindEx_lstFiles.SelectedItems.Add
                    (FBindEx_lstFiles.Items[a]);
        }
        private void FBindEx_cmdFromImported_Click(object sender, EventArgs e)
        {
            FBindEx_cmdFromFolder.BackColor = clr_BtnOff;
            FBindEx_cmdFromImported.BackColor = clr_BtnOff;
            frmWait.sMain = "Finding FBinds...";
            frmWait.sFooter = "Scanning database";
            frmWait.bVisible = true;
            frmWait.bInstant = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            SearchPrm sp = new SearchPrm(false);
            sp.cFlagRet[0] = '1';
            ImageData[] idC = DB.Search(sp, 1);

            pFBind_Root = cb.sAppPath + DB.Path;
            FBindEx_lstFiles.Items.Clear();
            for (int a = 0; a < idC.Length; a++)
                FBindEx_lstFiles.Items.Add(idC[a].sPath);

            FBindEx_cmdFromImported.BackColor = clr_BtnOn;
            frmWait.sMain = "All done";
            frmWait.bVisible = false;
        }
        private void FBindEx_cmdFromFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog(); if (fbd.SelectedPath == "") return;

            FBindEx_cmdFromFolder.BackColor = clr_BtnOff;
            FBindEx_cmdFromImported.BackColor = clr_BtnOff;
            frmWait.sMain = "Finding FBinds...";
            frmWait.sFooter = "Scanning subdirectories";
            frmWait.bVisible = true;
            frmWait.bInstant = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            string[] sFiles = cb.GetPaths(fbd.SelectedPath, true);
            sFiles = cb.FilterArray(sFiles, DB.saAllowedTypes,
                cb.asIntArray(2, DB.saAllowedTypes.Length), false);
            pFBind_Root = fbd.SelectedPath;

            FBindEx_lstFiles.Items.Clear();
            for (int a = 0; a < sFiles.Length; a++)
            {
                frmWait.sFooter = "File " + a + " of " + sFiles.Length;
                frmWait.sHeader = sFiles[a]; Application.DoEvents();
                if (pFBind.isFBind(sFiles[a]))
                    FBindEx_lstFiles.Items.Add
                        (sFiles[a].Substring(pFBind_Root.Length));
            }

            FBindEx_cmdFromFolder.BackColor = clr_BtnOn;
            frmWait.sMain = "All done";
            frmWait.bVisible = false;
        }
        private void FBindEx_cmdPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog(); string sPath = fbd.SelectedPath;
            if (sPath == "") return;
            sPath = sPath.Replace("\\", "/");
            if (!sPath.EndsWith("/")) sPath += "/";
            FBindEx_txtPath.Text = sPath;
        }
        private void FBindEx_lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FBindEx_lstFiles.SelectedItems.Count != 1) return;
            string sFile = pFBind_Root + FBindEx_lstFiles.SelectedItem.ToString();
            FBind inf = pFBind.Extract(sFile);
            if (FBindEx_chkIgnoreCorrupted.Checked)
                if (inf.sHashNow != inf.sHashOld)
                {
                    FBindEx_txtFName.Text = "CORRUPTED";
                    FBindEx_txtFSize.Text = "OR VERY OLD";
                    FBindEx_txtHash.Text = "-- ignored --";
                    FBindEx_txtHash.BackColor = clr_BtnOff;
                    return;
                }

            FBindEx_txtFName.Text = inf.sName;
            FBindEx_txtFSize.Text = inf.msFile2.Length + "";
            FBindEx_txtHash.Text = inf.sHashOld + " / " + inf.sHashNow;
            if (inf.sHashNow == inf.sHashOld) FBindEx_txtHash.BackColor = clr_BtnOn;
            if (inf.sHashNow != inf.sHashOld) FBindEx_txtHash.BackColor = clr_BtnOff;
            if (inf.sHashOld == inf.sHashUnk) FBindEx_txtHash.BackColor = clr_BtnHold;
            Application.DoEvents();

            Image imTmp = cb.ResizeBitmap(new Bitmap(sFile),
                (int)Math.Round(1.5 * (double)FBindEx_pbPreview.Width),
                (int)Math.Round(1.5 * (double)FBindEx_pbPreview.Height),
                true, 2) as Image;
            if (FBindEx_pbPreview.BackgroundImage != null)
                FBindEx_pbPreview.BackgroundImage.Dispose();
            FBindEx_pbPreview.BackgroundImage = imTmp;
        }
        private void FBindEx_cmdExtract_Click(object sender, EventArgs e)
        {
            frmWait.sMain = "Extracting FBinds...";
            frmWait.bVisible = true;
            frmWait.bInstant = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            string sExPath = FBindEx_txtPath.Text;
            if (sExPath == "app.path / fbinds")
                sExPath = cb.sAppPath + "FBinds/";
            System.IO.Directory.CreateDirectory(sExPath);
            for (int a = 0; a < FBindEx_lstFiles.SelectedItems.Count; a++)
            {
                frmWait.sFooter = "File " + a + " of " +
                    FBindEx_lstFiles.SelectedItems.Count;
                Application.DoEvents();

                bool bDoThis = true;
                string sImPath = pFBind_Root +
                    FBindEx_lstFiles.SelectedItems[a].ToString();
                FBind inf = pFBind.Extract(sImPath);
                if (FBindEx_chkIgnoreCorrupted.Checked)
                    if (inf.sHashOld != inf.sHashNow)
                        bDoThis = false;

                if (bDoThis)
                {
                    int iWrCnt = -1;
                    string sWrDir = sExPath + inf.sName;
                    string sWrExt = sWrDir.Substring(sWrDir.LastIndexOf("."));
                    sWrDir = sWrDir.Substring(0, sWrDir.Length - sWrExt.Length);
                    if (System.IO.File.Exists(sWrDir + sWrExt))
                    {
                        iWrCnt = 1;
                        while (System.IO.File.Exists(sWrDir +
                            " (" + iWrCnt + ")" + sWrExt))
                            iWrCnt++;
                    }
                    string sWrPath = sWrDir;
                    if (iWrCnt != -1) sWrPath += " (" + iWrCnt + ")";
                    sWrPath += sWrExt;

                    System.IO.FileStream fs = new System.IO.FileStream(sWrPath,
                        System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    inf.msFile2.WriteTo(fs); fs.Flush(); fs.Close(); fs.Dispose();
                }
            }
            frmWait.sMain = "All done";
            frmWait.bVisible = false;
        }

        private void General_cmdTagbar_Click(object sender, EventArgs e)
        {
            ShowPanel(pnTagbar);
            if (sTagbar == null)
                Tagbar_Remake();
        }
        private void Tagbar_lbRefresh_Click(object sender, EventArgs e)
        {
            Tagbar_Remake();
        }
        private void Tagbar_Remake()
        {
            lst list = new lst(true);
            for (int a = 0; a < idImages.Length; a++)
            {
                string[] sTTags;

                sTTags = idImages[a].sTGen.Split(',');
                foreach (string sTag in sTTags)
                    if (sTag != "") list.Add(sTag);

                sTTags = idImages[a].sTSrc.Split(',');
                foreach (string sTag in sTTags)
                    if (sTag != "") list.Add(sTag);

                sTTags = idImages[a].sTChr.Split(',');
                foreach (string sTag in sTTags)
                    if (sTag != "") list.Add(sTag);

                sTTags = idImages[a].sTArt.Split(',');
                foreach (string sTag in sTTags)
                    if (sTag != "") list.Add(sTag);
            }
            string[] sTags = new string[list.Len];
            for (int a = 0; a < list.Len; a++)
                sTags[a] = (list.Cnt[a] + 1).ToString("d16") + list.gStr(a);
            sTags = SortStringArrayAlphabetically(sTags);
            Array.Reverse(sTags);

            sTagbar = new string[Math.Min(100, sTags.Length)];
            for (int a = 0; a < sTagbar.Length; a++)
                sTagbar[a] = "[" +
                    sTags[a].Substring(0, 16).TrimStart('0') + "]  " +
                    sTags[a].Substring(16);
            Tagbar_Redraw(false);
        }
        private void Tagbar_Redraw(bool bRemakeControls)
        {
            int iLaSY = 13 + 3;
            int iLaSX = pnTagbar.Width - 3 - 3;
            int iLaCY = (pnTagbar.Height - 3) / iLaSY;
            if (lbTagbar.Count != iLaCY - 1)
                bRemakeControls = true;

            if (bRemakeControls)
            {
                for (int a = 0; a < lbTagbar.Count; a++)
                {
                    lbTagbar[a].Visible = false;
                    this.Controls.Add(lbTagbar[a]);
                }
                while (lbTagbar.Count > 0) lbTagbar.Remove();
            }

            for (int a = 0; a < iLaCY - 1; a++)
            {
                if (bRemakeControls)
                {
                    lbTagbar.NewLabel();
                    pnTagbar.Controls.Add(lbTagbar[a]);
                    lbTagbar[a].Click += new EventHandler(lbTagbar_Click);
                }
                lbTagbar[a].Size = new Size(iLaSX, iLaSY);
                lbTagbar[a].Location = new Point(3, 5 + ((a + 1) * iLaSY));
                //lbTagbar[a].BorderStyle = BorderStyle.FixedSingle;
                //lbTagbar[a].BackColor = clr_pnThumb[0];
                lbTagbar[a].ForeColor = Color.White;
                if (a >= sTagbar.Length)
                    lbTagbar[a].Text = "-";
                else lbTagbar[a].Text = sTagbar[a];
            }
        }
        private void lbTagbar_Click(object sender, EventArgs e)
        {
            if (GUI_CheckForChanges()) return;
            if (!DB.IsOpen)
            {
                MessageBox.Show("Please select or create a database first.",
                    "You are doing it wrong.", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation); return;
            }

            string sTag = lbTagbar[lbTagbar.Ci.iIndex].Text;
            if (!sTag.Contains("]  ")) return;
            sTag = sTag.Substring(sTag.IndexOf("]  ") + 3);
            SearchPrm sp = new SearchPrm();
            sp.sTagsGeneral = sTag;
            sp.sTagsSource = sTag;
            sp.sTagsChars = sTag;
            sp.sTagsArtists = sTag;
            idImages = DB.Search(sp, 2);

            if (!pnThumbs.Visible)
                ShowPanel(pnThumbs);

            iThumbPage = 1;
            SetThumbPageRanges(0);
            RedrawThumbnails(false);
            ReloadThumbnails(false);
            iVMode = 0; iAMode = 0;
        }

        private void Colors_cmdApply_Click(object sender, EventArgs e)
        {
            try
            {
                clr_pnThumb[0] = HexToCol(Design_txcolThBrdIdle.Text);
                clr_pnThumb[1] = HexToCol(Design_txcolThBrdSel.Text);
                Color cAppBG = HexToCol(Design_txcolAppBG.Text);
                this.BackColor = cAppBG;
            }
            catch
            {
                MessageBox.Show("Learn 2 hex colours.", "You are doing it wrong.",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            ApplyColorsTo(this.Controls, 0);
        }
        private void ApplyColorsTo(Control.ControlCollection cnt, int iLev)
        {
            Color cMPnlBG = HexToCol(Design_txcolMPnlBG.Text);
            Color cPnlBG = HexToCol(Design_txcolPnlBG.Text);
            Color cTextBG = HexToCol(Design_txcolTextBG.Text);
            Color cBtnBG = HexToCol(Design_txcolBtnBG.Text);
            Color cFont = HexToCol(Design_txcolFont.Text);
            for (int a = 0; a < cnt.Count; a++)
            {
                string sType = cnt[a].GetType().ToString();
                if (sType == "System.Windows.Forms.Panel")
                {
                    ApplyColorsTo(cnt[a].Controls, iLev + 1);
                    if (iLev % 2 == 0) cnt[a].BackColor = cPnlBG;
                    if (iLev % 2 != 0) cnt[a].BackColor = cMPnlBG;
                    cnt[a].ForeColor = cFont;
                }
                if (sType == "System.Windows.Forms.TextBox")
                {
                    cnt[a].BackColor = cTextBG;
                    cnt[a].ForeColor = cFont;
                }
                if (sType == "System.Windows.Forms.ComboBox")
                {
                    cnt[a].BackColor = cTextBG;
                    cnt[a].ForeColor = cFont;
                }
                if (sType == "System.Windows.Forms.ListBox")
                {
                    cnt[a].BackColor = cTextBG;
                    cnt[a].ForeColor = cFont;
                }
                if (sType == "System.Windows.Forms.Button")
                {
                    cnt[a].BackColor = cBtnBG;
                    cnt[a].ForeColor = cFont;
                }
                if (sType == "System.Windows.Forms.Label")
                {
                    cnt[a].ForeColor = cFont;
                }
                if (sType == "System.Windows.Forms.CheckBox")
                {
                    cnt[a].ForeColor = cFont;
                }
            }
        }
        private void Colors_cmdSave_Click(object sender, EventArgs e)
        {
            System.IO.Directory.CreateDirectory(cb.sAppPath + "z_colors/");
            cb.FileWrite("z_colors/" + Design_txtPreset.Text + ".txt", false,
                Design_txcolAppBG.Text + "\r\n" + Design_txcolPnlBG.Text + "\r\n" +
                Design_txcolTextBG.Text + "\r\n" + Design_txcolMPnlBG.Text + "\r\n" +
                Design_txcolBtnBG.Text + "\r\n" + Design_txcolFont.Text + "\r\n" +
                Design_txcolThBrdSel.Text + "\r\n" + Design_txcolThBrdIdle.Text);
        }
        private void Colors_cmdLoad_Click(object sender, EventArgs e)
        {
            string[] sDesign = cb.FileRead("z_colors/" +
                Design_ddPresets.Text).Replace("\r", "").Split('\n');
            Design_txcolAppBG.Text = sDesign[0];
            Design_txcolPnlBG.Text = sDesign[1];
            Design_txcolTextBG.Text = sDesign[2];
            Design_txcolMPnlBG.Text = sDesign[3];
            Design_txcolBtnBG.Text = sDesign[4];
            Design_txcolFont.Text = sDesign[5];
            Design_txcolThBrdSel.Text = sDesign[6];
            Design_txcolThBrdIdle.Text = sDesign[7];
            Colors_cmdApply_Click(new object(), new EventArgs());
        }
        private Color HexToCol(string sColor)
        {
            if (sColor.Length > 6) sColor = sColor.Substring(sColor.Length - 6);
            int iR = Int32.Parse(sColor.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            int iG = Int32.Parse(sColor.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            int iB = Int32.Parse(sColor.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            return Color.FromArgb(iR, iG, iB);
        }

        private void WebServ_Enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (!WebServ_Enabled.Checked)
            {
                WebServer.Log("Stopping webserver...");
                WebServer.Stop();
            }
            else
            {
                int iPort = -1;
                try { iPort = Convert.ToInt32(WebServ_txtPort.Text); }
                catch
                {
                    MessageBox.Show("What kind of port is that?", "You are doing it wrong.",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return;
                }

                if (WebServer.bEnabled || WebServer.iActive > 0)
                {
                    MessageBox.Show("Please wait until active connections are closed.",
                        "Derp.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    WebServ_Enabled.Checked = false; return;
                }

                WebServer.Cred_AdminUSR = WebServ_txtUsername.Text;
                WebServer.Cred_AdminPWD = WebServ_txtPassword.Text;
                WebServer.Cred_GuestPWD = WebServ_txtGuestPw.Text;
                WebServer.Log("Webserver enabled" + "\r\n");
                tWebServLog_Tick(new object(), new EventArgs());
                WebServer.Start(iPort);
                tWebServLog.Start();
            }
        }
        private void tWebServLog_Tick(object sender, EventArgs e)
        {
            WebServ_lblActive.Text = WebServer.iActive + " active";
            if (WebServer.sLog != "")
            {
                WebServ_txtLog.Text =
                    WebServer.sLog +
                    WebServ_txtLog.Text;
                WebServer.sLog = "";
                Application.DoEvents();
            }
        }

        private int[] GetSelected(bool bVerifySrc)
        {
            bool bIter = true;
            int iThisID = thPages[1].idRange.X + iThClicked;
            if (bVerifySrc)
            {
                if (iThisID != -1)
                    if (!idImages[iThisID].bSel)
                        bIter = false;
            }

            if (!bIter)
                return new int[] { iThisID };
            else
            {
                int i = 0;
                for (int a = 0; a < idImages.Length; a++)
                {
                    if (idImages[a].bSel) i++;
                }
                int[] iRet = new int[i]; i = 0;
                for (int a = 0; a < idImages.Length; a++)
                    if (idImages[a].bSel)
                    {
                        iRet[i] = a; i++;
                    }
                return iRet;
            }
        }
        private int[] GetSelected()
        {
            return GetSelected(true);
        }
        private void rcmThumbs_emTags_Read_Click(object sender, EventArgs e)
        {
            int[] iImages = GetSelected();
            if (DialogResult.No == MessageBox.Show(
                "Do you wish to read emTags" + "\r\n" +
                "from the " + iImages.Length + " selected images?",
                "Hello", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)) return;

            frmWait.sHeader = ".:: emTagger ::.";
            frmWait.sMain = "Reading emTags";
            frmWait.sFooter = "Image 0 of " + iImages.Length;
            frmWait.bVisible = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            for (int a = 0; a < iImages.Length; a++)
            {
                frmWait.sFooter = "Image " + (a + 1) + " of " + iImages.Length;
                int i = iImages[a]; Application.DoEvents();
                string sPath = cb.sAppPath + DB.Path + idImages[i].sPath;
                string[] sTags = emTags.TagsRead(sPath);
                if (sTags.Length != 0)
                {
                    idImages[i].cFlag[DB.flEmTag] = '1'; //emTags synced
                    idImages[i].sName = sTags[0];
                    idImages[i].sCmnt = sTags[1];
                    idImages[i].sTGen = sTags[2];
                    idImages[i].sTSrc = sTags[3];
                    idImages[i].sTChr = sTags[4];
                    idImages[i].sTArt = sTags[5];
                }
                else idImages[i].cFlag[DB.flEmTag] = '0'; //no emTags
            }

            frmWait.sHeader = "Click this window or press space to close";
            frmWait.sMain = "emTags read";
            frmWait.bCloseAtClick = true;
        }
        private void rcmThumbs_emTags_Write_Click(object sender, EventArgs e)
        {
            int[] iImages = GetSelected();
            if (DialogResult.No == MessageBox.Show(
                "Do you wish to write emTags" + "\r\n" +
                "to the " + iImages.Length + " selected images?",
                "Hello", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)) return;

            bool bKeepMeta = (DialogResult.Yes == MessageBox.Show(
                "Should other metadata be kept intact?" + "\r\n\r\n" +
                "YES: FBinds will still work." + "\r\n" +
                "NO:  FBinds are converted to regular images.", "Hello",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question));

            frmWait.sHeader = ".:: emTagger ::.";
            frmWait.sMain = "Writing emTags";
            frmWait.sFooter = "Image 0 of " + iImages.Length;
            frmWait.bVisible = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }



            int iOK = 0; int iSU = 0;
            for (int a = 0; a < iImages.Length; a++)
            {
                frmWait.sFooter = "Image " + (a + 1) + " of " + iImages.Length;
                int i = iImages[a]; Application.DoEvents();
                string sPath = cb.sAppPath + DB.Path + idImages[i].sPath;
                if (idImages[i].sType == "jpg" ||
                    idImages[i].sType == "png" ||
                    idImages[i].sType == "gif") iSU++;
                else idImages[a].cFlag[DB.flEmTag] = '0'; //no emtags

                string[] sTags = new string[]{
                    idImages[i].sName, idImages[i].sCmnt,
                    idImages[i].sTGen, idImages[i].sTSrc,
                    idImages[i].sTChr, idImages[i].sTArt};
                if (emTags.TagsWrite(sPath, bKeepMeta, sTags))
                {
                    idImages[a].cFlag[DB.flEmTag] = '1'; //synced
                    iOK++;
                }
            }
            string sAddendum = "";
            if (iSU != iImages.Length) sAddendum += "\r\n" +
                (iImages.Length - iSU) + " images were not supported (bmp).";
            if (iOK != iSU) sAddendum += "\r\n" +
                (iSU - iOK) + " images couldn't be accessed (in use?)";



            frmWait.bVisible = false;
            frmWait.bInstant = true;
            while (frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            MessageBox.Show(iOK + " images were successfully emTagged." +
                sAddendum, "Hello", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void rcmThumbs_emTags_Merge_Click(object sender, EventArgs e)
        {
            int iUnsynced = 0;
            int[] iImages = GetSelected();
            for (int a = 0; a < iImages.Length; a++)
                if (idImages[iImages[a]].cFlag[DB.flEmTag] != '1')
                {
                    string sType = idImages[iImages[a]].sType;
                    if (sType == "jpg" ||
                        sType == "png" ||
                        sType == "gif")
                        iUnsynced++;
                }


            //  Select image group  -  all selected  /  known unsynced
            DialogResult dra = MessageBox.Show(
                "You have selected " + iImages.Length + " images for syncing." + "\r\n" +
                iUnsynced + " of these are known to be out-of-sync." + "\r\n\r\n" +
                "The emTags-feature is not 100% completed, so the" + "\r\n" +
                "tracer is known to be glitchy. For instance, it won't" + "\r\n" +
                "notice changes made using the mass edit feature." + "\r\n\r\n" +
                "YES: Resync all selected images (recommended)" + "\r\n" +
                "NO:  Only sync the images known to be out-of-sync", "Hello",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            int[] iSyncQ = new int[0];
            if (dra == DialogResult.No)
            {
                int iwz = 0;
                iSyncQ = new int[iUnsynced];
                for (int a = 0; a < iImages.Length; a++)
                    if (idImages[iImages[a]].cFlag[DB.flEmTag] != '1')
                    {
                        string sType = idImages[iImages[a]].sType;
                        if (sType == "jpg" ||
                            sType == "png" ||
                            sType == "gif")
                        {
                            iSyncQ[iwz] = iImages[a]; iwz++;
                        }
                    }
            }
            else iSyncQ = (int[])iImages.Clone();


            //  Keep other metadata?
            bool bKeepMeta = (DialogResult.Yes == MessageBox.Show(
                "Should other metadata be kept intact?" + "\r\n\r\n" +
                "YES: FBinds will still work." + "\r\n" +
                "NO:  FBinds are converted to regular images.", "Hello",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question));


            //  Select prioritation
            DialogResult dr = MessageBox.Show(
                "Which set of image names and comments do you wish to keep?" + "\r\n\r\n" +
                "YES: emTag names and comments will replace database." + "\r\n" +
                "        Also, tags from emTags will be sorted first in tags-view." + "\r\n\r\n" +
                "NO:  Database names and comments will replace emTags." + "\r\n" +
                "        Also, tags from the database will be sorted first.", "Hello",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel) return;


            //  Show waiting screen
            frmWait.sHeader = ".:: emTagger ::.";
            frmWait.sMain = "Syncing emTags";
            frmWait.sFooter = "Image 0 of " + iSyncQ.Length;
            frmWait.bVisible = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            for (int a = 0; a < iSyncQ.Length; a++)
            {
                frmWait.sFooter = "Image " + (a + 1) + " of " + iSyncQ.Length;
                int i = iSyncQ[a]; Application.DoEvents();
                string sPath = cb.sAppPath + DB.Path + idImages[i].sPath;
                string[] semTags = emTags.TagsRead(sPath);
                if (semTags.Length == 0)
                {
                    semTags = new string[6];
                    for (int b = 0; b < semTags.Length; b++)
                        semTags[b] = "";
                }
                if (dr == DialogResult.Yes) // emTags first
                {
                    if (semTags[0].Length < 3) semTags[0] = idImages[i].sName;
                    if (semTags[1].Length < 3) semTags[1] = idImages[i].sCmnt;
                    semTags[2] = cb.RemoveDupes(semTags[2] + ", " + idImages[i].sTGen);
                    semTags[3] = cb.RemoveDupes(semTags[3] + ", " + idImages[i].sTSrc);
                    semTags[4] = cb.RemoveDupes(semTags[4] + ", " + idImages[i].sTChr);
                    semTags[5] = cb.RemoveDupes(semTags[5] + ", " + idImages[i].sTArt);
                }
                else // Database first
                {
                    semTags[0] = idImages[i].sName;
                    semTags[1] = idImages[i].sCmnt;
                    semTags[2] = cb.RemoveDupes(idImages[i].sTGen + ", " + semTags[2]);
                    semTags[3] = cb.RemoveDupes(idImages[i].sTSrc + ", " + semTags[3]);
                    semTags[4] = cb.RemoveDupes(idImages[i].sTChr + ", " + semTags[4]);
                    semTags[5] = cb.RemoveDupes(idImages[i].sTArt + ", " + semTags[5]);
                }
                if (emTags.TagsWrite(sPath, true, semTags))
                {
                    idImages[i].sName = semTags[0];
                    idImages[i].sCmnt = semTags[1];
                    idImages[i].sTGen = semTags[2];
                    idImages[i].sTSrc = semTags[3];
                    idImages[i].sTChr = semTags[4];
                    idImages[i].sTArt = semTags[5];
                    idImages[i].cFlag[DB.flEmTag] = '1'; //emTags synced
                }
            }

            frmWait.sHeader = "Click this window or press space to close";
            frmWait.sMain = "emTags synced";
            frmWait.bCloseAtClick = true;
        }
        private void rcmThumbs_emTags_ClearTags_Click(object sender, EventArgs e)
        {
            int[] iImages = GetSelected();
            if (DialogResult.No == MessageBox.Show(
                "Are you sure you wish to remove emTags" + "\r\n" +
                "from the " + iImages.Length + " selected images?" + "\r\n\r\n" +
                "This will not destroy FBinds.", "O SHI-", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)) return;

            frmWait.sHeader = ".:: emTagger ::.";
            frmWait.sMain = "Removing emTags";
            frmWait.sFooter = "Image 0 of " + iImages.Length;
            frmWait.bVisible = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            int iOK = 0;
            for (int a = 0; a < iImages.Length; a++)
            {
                frmWait.sFooter = "Image " + (a + 1) + " of " + iImages.Length;
                int i = iImages[a]; Application.DoEvents();
                string sPath = cb.sAppPath + DB.Path + idImages[i].sPath;
                if (emTags.TagsWrite(sPath, true, new string[0])) iOK++;
            }
            string sAddendum = "";
            if (iOK != iImages.Length) sAddendum = "\r\n\r\n" +
                "I was unable to store all changes. Make sure that" + "\r\n" +
                "no images are opened in other applications!";

            frmWait.bVisible = false;
            frmWait.bInstant = true;
            while (frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            MessageBox.Show(iOK + " of " + iImages.Length + " images were modified." +
                sAddendum, "Hello", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void rcmThumbs_emTags_ClearMeta_Click(object sender, EventArgs e)
        {
            int[] iImages = GetSelected();
            if (DialogResult.No == MessageBox.Show(
                "Are you sure you wish to remove ALL METADATA" + "\r\n" +
                "from the " + iImages.Length + " selected images?" + "\r\n\r\n" +
                "This will convert FBinds into regular images.", "O SHI-",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)) return;

            frmWait.sHeader = ".:: emTagger ::.";
            frmWait.sMain = "Removing metadata";
            frmWait.sFooter = "Image 0 of " + iImages.Length;
            frmWait.bVisible = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            int iOK = 0;
            for (int a = 0; a < iImages.Length; a++)
            {
                frmWait.sFooter = "Image " + (a + 1) + " of " + iImages.Length;
                int i = iImages[a]; Application.DoEvents();
                string sPath = cb.sAppPath + DB.Path + idImages[i].sPath;
                if (emTags.TagsWrite(sPath, true, new string[0])) iOK++;
            }
            string sAddendum = "";
            if (iOK != iImages.Length) sAddendum = "\r\n\r\n" +
                "I was unable to store all changes. Make sure that" + "\r\n" +
                "no images are opened in other applications!";

            frmWait.bVisible = false;
            frmWait.bInstant = true;
            while (frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            MessageBox.Show(iOK + " of " + iImages.Length + " images were modified." +
                sAddendum, "Hello", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void rcmThumbs_emTags_View_Click(object sender, EventArgs e)
        {
            int iThisID = thPages[1].idRange.X + iThClicked;
            string sPath = cb.sAppPath + DB.Path + idImages[iThisID].sPath;
            string[] sTags = emTags.TagsRead(sPath);

            if (sTags.Length == 0)
            {
                MessageBox.Show("There are no embedded tags in this image.",
                    "emTags in #" + iThisID.ToString("X4"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Name:  " + sTags[0].Replace(",", ", ") + "\r\n" +
                    "Cmnt:  " + sTags[1].Replace(",", ", ") + "\r\n" +
                    "tGen:  " + sTags[2].Replace(",", ", ") + "\r\n" +
                    "tSrc:  " + sTags[3].Replace(",", ", ") + "\r\n" +
                    "tChr:  " + sTags[4].Replace(",", ", ") + "\r\n" +
                    "tArt:  " + sTags[5].Replace(",", ", ") + "",
                    "emTags in #" + iThisID.ToString("X4"));
            }
        }

        private void TagCopy(int iID, string sPattern)
        {
            idCopied = new ImageData();
            if (sPattern[0] == '1') idCopied.sName = idImages[iID].sName; else idCopied.sName = "\n";
            if (sPattern[1] == '1') idCopied.sCmnt = idImages[iID].sCmnt; else idCopied.sCmnt = "\n";
            if (sPattern[2] == '1') idCopied.sTGen = idImages[iID].sTGen; else idCopied.sTGen = "\n";
            if (sPattern[3] == '1') idCopied.sTSrc = idImages[iID].sTSrc; else idCopied.sTSrc = "\n";
            if (sPattern[4] == '1') idCopied.sTChr = idImages[iID].sTChr; else idCopied.sTChr = "\n";
            if (sPattern[5] == '1') idCopied.sTArt = idImages[iID].sTArt; else idCopied.sTArt = "\n";
        }
        private void TagCopyGUI(string sPattern)
        {
            int iID = thPages[1].idRange.X + iThClicked; TagCopy(iID, sPattern);
            frmTip.ShowMsg(true, "Tags copied from #" + iID.ToString("X4"));
        }
        private void TagPaste(int[] iID, bool bAppend, string sPattern)
        {
            frmWait.sHeader = ".:: Clipboard ::.";
            frmWait.sMain = "Pasting tags";
            frmWait.sFooter = "Image 0 of " + iID.Length;
            frmWait.bVisible = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            for (int a = 0; a < iID.Length; a++)
            {
                frmWait.sFooter = "Image " + (a + 1) + " of " + iID.Length;
                if (a.ToString().EndsWith("00")) Application.DoEvents();
                int i = iID[a]; string[] sOld = new string[]{
                    idImages[i].sName, idImages[i].sCmnt,
                    idImages[i].sTGen, idImages[i].sTSrc,
                    idImages[i].sTChr, idImages[i].sTArt};

                if (bAppend)
                {
                    if (sPattern[0] == '1' && idCopied.sName != "\n") idImages[i].sName = idCopied.sName;
                    if (sPattern[1] == '1' && idCopied.sCmnt != "\n") idImages[i].sCmnt = idCopied.sCmnt;
                    if (sPattern[2] == '1' && idCopied.sTGen != "\n") idImages[i].sTGen = cb.RemoveDupes(idImages[i].sTGen + ", " + idCopied.sTGen);
                    if (sPattern[3] == '1' && idCopied.sTSrc != "\n") idImages[i].sTSrc = cb.RemoveDupes(idImages[i].sTSrc + ", " + idCopied.sTSrc);
                    if (sPattern[4] == '1' && idCopied.sTChr != "\n") idImages[i].sTChr = cb.RemoveDupes(idImages[i].sTChr + ", " + idCopied.sTChr);
                    if (sPattern[5] == '1' && idCopied.sTArt != "\n") idImages[i].sTArt = cb.RemoveDupes(idImages[i].sTArt + ", " + idCopied.sTArt);
                }
                else
                {
                    if (sPattern[0] == '1' && idCopied.sName != "\n") idImages[i].sName = idCopied.sName;
                    if (sPattern[1] == '1' && idCopied.sCmnt != "\n") idImages[i].sCmnt = idCopied.sCmnt;
                    if (sPattern[2] == '1' && idCopied.sTGen != "\n") idImages[i].sTGen = idCopied.sTGen;
                    if (sPattern[3] == '1' && idCopied.sTSrc != "\n") idImages[i].sTSrc = idCopied.sTSrc;
                    if (sPattern[4] == '1' && idCopied.sTChr != "\n") idImages[i].sTChr = idCopied.sTChr;
                    if (sPattern[5] == '1' && idCopied.sTArt != "\n") idImages[i].sTArt = idCopied.sTArt;
                }
                if (idImages[i].sName != sOld[0] ||
                    idImages[i].sCmnt != sOld[1] ||
                    idImages[i].sTGen != sOld[2] ||
                    idImages[i].sTSrc != sOld[3] ||
                    idImages[i].sTChr != sOld[4] ||
                    idImages[i].sTArt != sOld[5])
                {
                    idImages[i].bMod = true;
                    if (idImages[i].cFlag[DB.flEmTag] == '1')
                        idImages[i].cFlag[DB.flEmTag] = '2';
                }
            }

            frmWait.sMain = "Tags pasted";
            frmWait.bVisible = false;
        }
        private void TagPasteGUI(string sPattern)
        {
            int[] iID = GetSelected();
            DialogResult dr = MessageBox.Show(
                "You are about to modify " + iID.Length + " images." + "\r\n" +
                "Do you wish to overwrite or append the tags?" + "\r\n\r\n" +
                "YES: Append" + "\r\n" + "NO:  Overwrite" + "\r\n\r\n" +
                "Image name/comment will be overwritten regardless.", "Hello",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel) return;

            TagPaste(iID, (dr == DialogResult.Yes), sPattern);
        }
        private void rcmThumbs_Edit_TagCopy_Everything_Click(object sender, EventArgs e)
        {
            TagCopyGUI("111111");
        }
        private void rcmThumbs_Edit_TagCopy_Tagfields_Click(object sender, EventArgs e)
        {
            TagCopyGUI("001111");
        }
        private void rcmThumbs_Edit_TagCopy_NameCmnt_Click(object sender, EventArgs e)
        {
            TagCopyGUI("110000");
        }
        private void rcmThumbs_Edit_TagCopy_Custom_Click(object sender, EventArgs e)
        {
            string sPattern = InputBox.Show(
                "Please specify what information to copy by" + "\r\n" +
                "entering either 1 or 0 at the appropriate place." + "\r\n\r\n" +
                "Char 1:  Name" + "\r\n" +
                "Char 2:  Comment" + "\r\n" +
                "Char 3:  Tags General" + "\r\n" +
                "Char 4:  Tags Source" + "\r\n" +
                "Char 5:  Tags Chars" + "\r\n" +
                "Char 6:  Tags Artist" + "\r\n\r\n" +
                "Example: Comment, TSource and TChars: 010110",
                "Are you rex enough for binary?").Text;
            if (sPattern.Length != 6)
            {
                MessageBox.Show("That's not 6 characters, you know?", "You are doing it wrong.",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rcmThumbs_Edit_TagCopy_Custom_Click(new object(), new EventArgs()); return;
            }
            TagCopyGUI(sPattern);
        }
        private void rcmThumbs_Edit_TagPaste_Everything_Click(object sender, EventArgs e)
        {
            TagPasteGUI("111111");
        }
        private void rcmThumbs_Edit_TagPaste_Tagfields_Click(object sender, EventArgs e)
        {
            TagPasteGUI("001111");
        }
        private void rcmThumbs_Edit_TagPaste_NameCmnt_Click(object sender, EventArgs e)
        {
            TagPasteGUI("110000");
        }
        private void rcmThumbs_Edit_TagPaste_Custom_Click(object sender, EventArgs e)
        {
            string sPattern = InputBox.Show(
                "Please specify what information to paste by" + "\r\n" +
                "entering either 1 or 0 at the appropriate place." + "\r\n\r\n" +
                "Char 1:  Name" + "\r\n" +
                "Char 2:  Comment" + "\r\n" +
                "Char 3:  Tags General" + "\r\n" +
                "Char 4:  Tags Source" + "\r\n" +
                "Char 5:  Tags Chars" + "\r\n" +
                "Char 6:  Tags Artist" + "\r\n\r\n" +
                "Example: Comment, TSource and TChars: 010110",
                "Are you rex enough for binary?").Text;
            if (sPattern.Length != 6)
            {
                MessageBox.Show("That's not 6 characters, you know?", "You are doing it wrong.",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rcmThumbs_Edit_TagCopy_Custom_Click(new object(), new EventArgs()); return;
            }
            TagPasteGUI(sPattern);
        }

        private void General_cmdHotTagger_Click(object sender, EventArgs e)
        {
            ShowPanel(pnHotTagger);
        }
        private void HotTagger_Enabled_CheckedChanged(object sender, EventArgs e)
        {
            bHotTagger = HotTagger_chkEnabled.Checked;
        }
        private void HotTagger_Exec(int iFn)
        {
            int iTarget = -1;
            int iAction = HotTagger_ddAction.SelectedIndex;
            string sValue = "";

            if (iFn == 1) iTarget = HotTagger_F1_Target.SelectedIndex;
            if (iFn == 2) iTarget = HotTagger_F2_Target.SelectedIndex;
            if (iFn == 3) iTarget = HotTagger_F3_Target.SelectedIndex;
            if (iFn == 4) iTarget = HotTagger_F4_Target.SelectedIndex;
            if (iFn == 5) iTarget = HotTagger_F5_Target.SelectedIndex;
            if (iFn == 6) iTarget = HotTagger_F6_Target.SelectedIndex;
            if (iFn == 7) iTarget = HotTagger_F7_Target.SelectedIndex;
            if (iFn == 8) iTarget = HotTagger_F8_Target.SelectedIndex;
            if (iFn == 9) iTarget = HotTagger_F9_Target.SelectedIndex;
            if (iFn == 10) iTarget = HotTagger_F10_Target.SelectedIndex;
            if (iFn == 11) iTarget = HotTagger_F11_Target.SelectedIndex;
            if (iFn == 12) iTarget = HotTagger_F12_Target.SelectedIndex;

            if (iFn == 1) sValue = HotTagger_F1_Val.Text;
            if (iFn == 2) sValue = HotTagger_F2_Val.Text;
            if (iFn == 3) sValue = HotTagger_F3_Val.Text;
            if (iFn == 4) sValue = HotTagger_F4_Val.Text;
            if (iFn == 5) sValue = HotTagger_F5_Val.Text;
            if (iFn == 6) sValue = HotTagger_F6_Val.Text;
            if (iFn == 7) sValue = HotTagger_F7_Val.Text;
            if (iFn == 8) sValue = HotTagger_F8_Val.Text;
            if (iFn == 9) sValue = HotTagger_F9_Val.Text;
            if (iFn == 10) sValue = HotTagger_F10_Val.Text;
            if (iFn == 11) sValue = HotTagger_F11_Val.Text;
            if (iFn == 12) sValue = HotTagger_F12_Val.Text;

            iTarget += 1; iAction += 1;
            if (iAction == 0)
            {
                MessageBox.Show("You forgot to set the action to execute again.",
                    "Dude...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (pnDisplay.Visible)
            {
                EditSingleParameter(DispImageIndex, iTarget, sValue, iAction);
                ViewImageInfo(DispImageIndex, true);
            }
            if (pnThumbs.Visible)
            {
                EditSingleParameter(iTarget, sValue, iAction);
            }
        }

        private int tagBase_Read(string[] sHash, int iMerge)
        {
            int iRet = 0;
            SQLiteTransaction DBtrs = DB.Data.BeginTransaction();

            SQLiteCommand DBcW = DB.Data.CreateCommand();
            SQLiteParameter[] dbP = new SQLiteParameter[8];
            DBcW.CommandText = "UPDATE 'images' SET name=?, cmnt=?, rate=?, tgen=?, tsrc=?, tchr=?, tart=? WHERE hash=?";
            for (int a = 0; a < dbP.Length; a++)
            {
                dbP[a] = DBcW.CreateParameter();
                DBcW.Parameters.Add(dbP[a]);
            }

            for (int a = 0; a < sHash.Length; a++)
            {
                frmWait.sFooter = "Image " + (a + 1) + " of " + sHash.Length;
                Application.DoEvents();

                SQLiteCommand TBc = tagBase.Data.CreateCommand();
                TBc.CommandText = "SELECT * FROM 'images' WHERE hash='" + sHash[a] + "'";
                SQLiteDataReader TBr = TBc.ExecuteReader();
                if (TBr.Read())
                {
                    int iRate = TBr.GetInt32(tagBase.diRate);
                    string sName = TBr.GetString(tagBase.diName);
                    string sCmnt = TBr.GetString(tagBase.diCmnt);
                    string sTGen = TBr.GetString(tagBase.diTGen);
                    string sTSrc = TBr.GetString(tagBase.diTSrc);
                    string sTChr = TBr.GetString(tagBase.diTChr);
                    string sTArt = TBr.GetString(tagBase.diTArt);

                    if (iMerge != 0)
                    {
                        SQLiteCommand DBcR = DB.Data.CreateCommand();
                        DBcR.CommandText = "SELECT * FROM 'images' WHERE hash='" + sHash[a] + "'";
                        SQLiteDataReader DBcRr = DBcR.ExecuteReader();
                        if (DBcRr.Read())
                        {
                            int iDRate = DBcRr.GetInt32(DB.diRate);
                            string sDName = DBcRr.GetString(DB.diName);
                            string sDCmnt = DBcRr.GetString(DB.diCmnt);
                            string sDTGen = DBcRr.GetString(DB.diTGen);
                            string sDTSrc = DBcRr.GetString(DB.diTSrc);
                            string sDTChr = DBcRr.GetString(DB.diTChr);
                            string sDTArt = DBcRr.GetString(DB.diTArt);

                            if (iMerge == 1) // Database first
                            {
                                if (sDName.Length >= 3) sName = sDName;
                                if (sDCmnt.Length >= 3) sCmnt = sDCmnt;
                                sTGen = cb.RemoveDupes(sDTGen + ", " + sTGen);
                                sTSrc = cb.RemoveDupes(sDTSrc + ", " + sTSrc);
                                sTChr = cb.RemoveDupes(sDTChr + ", " + sTChr);
                                sTArt = cb.RemoveDupes(sDTArt + ", " + sTArt);
                            }
                            else if (iMerge == 2) // tagBase first
                            {
                                if (sName.Length < 3) sName = sDName;
                                if (sCmnt.Length < 3) sCmnt = sDCmnt;
                                sTGen = cb.RemoveDupes(sTGen + ", " + sDTGen);
                                sTSrc = cb.RemoveDupes(sTSrc + ", " + sDTSrc);
                                sTChr = cb.RemoveDupes(sTChr + ", " + sDTChr);
                                sTArt = cb.RemoveDupes(sTArt + ", " + sDTArt);
                            }
                        }
                        DBcRr.Dispose(); DBcR.Dispose();
                    }
                    dbP[0].Value = sName; dbP[1].Value = sCmnt;
                    dbP[2].Value = iRate; dbP[3].Value = sTGen;
                    dbP[4].Value = sTSrc; dbP[5].Value = sTChr;
                    dbP[6].Value = sTArt; dbP[7].Value = sHash[a];
                    iRet += DBcW.ExecuteNonQuery();
                    TBr.Dispose(); TBc.Dispose();
                }
            }
            DBtrs.Commit(); DBcW.Dispose();
            DBtrs.Dispose(); return iRet;
        }
        private void tagBase_Write(string[] sHash, int iMerge)
        {
            SQLiteTransaction TBtrs = tagBase.Data.BeginTransaction();

            SQLiteCommand TBcW = tagBase.Data.CreateCommand();
            SQLiteParameter[] tbP = new SQLiteParameter[8];
            TBcW.CommandText = "UPDATE 'images' SET name=?, cmnt=?, rate=?, tgen=?, tsrc=?, tchr=?, tart=? WHERE hash=?";
            for (int a = 0; a < tbP.Length; a++)
            {
                tbP[a] = TBcW.CreateParameter();
                TBcW.Parameters.Add(tbP[a]);
            }

            SQLiteCommand TBcWM = tagBase.Data.CreateCommand();
            SQLiteParameter[] tbPM = new SQLiteParameter[8];
            TBcWM.CommandText = "INSERT INTO 'images' (hash, name, cmnt, rate, tgen, tsrc, tchr, tart) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
            for (int a = 0; a < tbP.Length; a++)
            {
                tbPM[a] = TBcWM.CreateParameter();
                TBcWM.Parameters.Add(tbPM[a]);
            }

            for (int a = 0; a < sHash.Length; a++)
            {
                frmWait.sFooter = "Image " + (a + 1) + " of " + sHash.Length;
                Application.DoEvents();

                SQLiteCommand DBc = DB.Data.CreateCommand();
                DBc.CommandText = "SELECT * FROM 'images' WHERE hash='" + sHash[a] + "'";
                SQLiteDataReader DBr = DBc.ExecuteReader();
                if (DBr.Read())
                {
                    int iRate = DBr.GetInt32(DB.diRate);
                    string sName = DBr.GetString(DB.diName);
                    string sCmnt = DBr.GetString(DB.diCmnt);
                    string sTGen = DBr.GetString(DB.diTGen);
                    string sTSrc = DBr.GetString(DB.diTSrc);
                    string sTChr = DBr.GetString(DB.diTChr);
                    string sTArt = DBr.GetString(DB.diTArt);

                    if (iMerge != 0)
                    {
                        SQLiteCommand TBcR = tagBase.Data.CreateCommand();
                        TBcR.CommandText = "SELECT * FROM 'images' WHERE hash='" + sHash[a] + "'";
                        SQLiteDataReader TBcRr = TBcR.ExecuteReader();
                        if (TBcRr.Read())
                        {
                            int iBRate = TBcRr.GetInt32(tagBase.diRate);
                            string sBName = TBcRr.GetString(tagBase.diName);
                            string sBCmnt = TBcRr.GetString(tagBase.diCmnt);
                            string sBTGen = TBcRr.GetString(tagBase.diTGen);
                            string sBTSrc = TBcRr.GetString(tagBase.diTSrc);
                            string sBTChr = TBcRr.GetString(tagBase.diTChr);
                            string sBTArt = TBcRr.GetString(tagBase.diTArt);

                            if (iMerge == 1) // Database first
                            {
                                if (sName.Length < 3) sName = sBName;
                                if (sCmnt.Length < 3) sCmnt = sBCmnt;
                                sTGen = cb.RemoveDupes(sTGen + ", " + sBTGen);
                                sTSrc = cb.RemoveDupes(sTSrc + ", " + sBTSrc);
                                sTChr = cb.RemoveDupes(sTChr + ", " + sBTChr);
                                sTArt = cb.RemoveDupes(sTArt + ", " + sBTArt);
                            }
                            else if (iMerge == 2) // tagBase first
                            {
                                if (sBName.Length >= 3) sName = sBName;
                                if (sBCmnt.Length >= 3) sCmnt = sBCmnt;
                                sTGen = cb.RemoveDupes(sBTGen + ", " + sTGen);
                                sTSrc = cb.RemoveDupes(sBTSrc + ", " + sTSrc);
                                sTChr = cb.RemoveDupes(sBTChr + ", " + sTChr);
                                sTArt = cb.RemoveDupes(sBTArt + ", " + sTArt);
                            }
                        }
                        TBcRr.Dispose(); TBcR.Dispose();
                    }
                    tbP[0].Value = sName;
                    tbP[1].Value = sCmnt;
                    tbP[2].Value = iRate;
                    tbP[3].Value = sTGen;
                    tbP[4].Value = sTSrc;
                    tbP[5].Value = sTChr;
                    tbP[6].Value = sTArt;
                    tbP[7].Value = sHash[a];
                    if (TBcW.ExecuteNonQuery() == 0)
                    {
                        tbPM[0].Value = sHash[a];
                        tbPM[1].Value = sName;
                        tbPM[2].Value = sCmnt;
                        tbPM[3].Value = iRate;
                        tbPM[4].Value = sTGen;
                        tbPM[5].Value = sTSrc;
                        tbPM[6].Value = sTChr;
                        tbPM[7].Value = sTArt;
                        TBcWM.ExecuteNonQuery();
                    }
                }
                DBr.Dispose(); DBc.Dispose();
            }
            TBtrs.Commit();
            TBcW.Dispose(); TBcWM.Dispose();
            TBtrs.Dispose();
        }
        private void tagBase_cmdBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = cb.sAppPath; ofd.ShowDialog();
            string sPath = ofd.FileName; if (sPath == "") return;
            sPath = sPath.Replace("\\", "/");
        }
        private void General_cmdTagBase_Click(object sender, EventArgs e)
        {
            ShowPanel(pnTagBase);
        }
        private void tagBase_cmdOpen_Click(object sender, EventArgs e)
        {
            tagBase_cmdOpen.Enabled = false;
            tagBase_cmdClose.Enabled = true;
            tagBase.Open(tagBase_txtPath.Text);
        }
        private void tagBase_cmdClose_Click(object sender, EventArgs e)
        {
            tagBase_cmdOpen.Enabled = true;
            tagBase_cmdClose.Enabled = false;
            tagBase.Close();
        }
        private void tagBase_cmdImportAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I don't have time to add this right now." + "\r\n" +
                "Here's how you can do it yourself:" + "\r\n\r\n" +
                "1. Ctrl+Home, \"View all images\"" + "\r\n" +
                "2. Ctrl+A, right-click any image" + "\r\n" +
                "3. tagBase -> Read" + "\r\n" +
                "4. ????" + "\r\n" +
                "5. PROFIT", "Hello");
        }
        private void rcmThumbs_tagBase_Read_Click(object sender, EventArgs e)
        {
            if (GUI_CheckForChanges()) return;
            if (!tagBase.IsOpen)
            {
                MessageBox.Show("You haven't opened a tagBase file.", "You are doing it wrong.",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return;
            }

            int iMerge = -1;
            int[] iImages = GetSelected();
            if (tagBase_Config_rdoReplace.Checked) iMerge = 0;
            if (tagBase_Config_rdoMergeDB.Checked) iMerge = 1;
            if (tagBase_Config_rdoMergeTB.Checked) iMerge = 2;
            if (iMerge == -1)
            {
                string sMerge = InputBox.Show("Trying to read " +
                    iImages.Length + " images from open tagBase..." + "\r\n\r\n" +
                    "Select merge mode." + "\r\n" +
                    "1: Replace old info (database) with new info (tagBase)" + "\r\n" +
                    "2: Merge - use name/comments from database" + "\r\n" +
                    "3: Merge - use name/comments from tagBase" + "\r\n\r\n" +
                    "Other: Cancel", "Hello").Text;
                try { iMerge = Convert.ToInt32(sMerge) - 1; }
                catch { }
            }
            if (iMerge < 0 || iMerge >= 3) return;

            frmWait.sHeader = ".:: tagBase ::.";
            frmWait.sMain = "Reading from tagBase";
            frmWait.sFooter = "Image 0 of " + iImages.Length;
            frmWait.bVisible = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            string[] sHash = new string[iImages.Length];
            for (int a = 0; a < sHash.Length; a++)
                sHash[a] = idImages[iImages[a]].sHash;
            int iCnt = tagBase_Read(sHash, iMerge);

            frmWait.sMain = "Reconstructing thumb view"; frmWait.bInstant = true;
            while (frmWait.bInstant) Application.DoEvents();
            idImages = DB.Search(LastSearch, LastSearchFallback);
            ShowPanel(pnThumbs); Application.DoEvents();
            RedrawThumbnails(true); ReloadThumbnails(false);
            iAMode = 0;

            frmWait.sHeader = "Click this window or press space to close";
            frmWait.sMain = "tagBase read";
            frmWait.bCloseAtClick = true;
        }
        private void rcmThumbs_tagBase_Write_Click(object sender, EventArgs e)
        {
            if (GUI_CheckForChanges()) return;
            if (!tagBase.IsOpen)
            {
                MessageBox.Show("You haven't opened a tagBase file.", "You are doing it wrong.",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return;
            }

            int iMerge = -1;
            int[] iImages = GetSelected();
            if (tagBase_Config_rdoReplace.Checked) iMerge = 0;
            if (tagBase_Config_rdoMergeDB.Checked) iMerge = 1;
            if (tagBase_Config_rdoMergeTB.Checked) iMerge = 2;
            if (iMerge == -1)
            {
                string sMerge = InputBox.Show("Trying to read " +
                    iImages.Length + " images from open tagBase..." + "\r\n\r\n" +
                    "Select merge mode." + "\r\n" +
                    "1: Replace old info (tagBase) with new info (database)" + "\r\n" +
                    "2: Merge - use name/comments from database" + "\r\n" +
                    "3: Merge - use name/comments from tagBase" + "\r\n\r\n" +
                    "Other: Cancel", "Hello").Text;
                try { iMerge = Convert.ToInt32(sMerge) - 1; }
                catch { }
            }
            if (iMerge < 0 || iMerge >= 3) return;

            frmWait.sHeader = ".:: tagBase ::.";
            frmWait.sMain = "Writing to tagBase";
            frmWait.sFooter = "Image 0 of " + iImages.Length;
            frmWait.bVisible = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            string[] sHash = new string[iImages.Length];
            for (int a = 0; a < sHash.Length; a++)
                sHash[a] = idImages[iImages[a]].sHash;
            tagBase_Write(sHash, iMerge);

            frmWait.sHeader = "Click this window or press space to close";
            frmWait.sMain = "tagBase written";
            frmWait.bCloseAtClick = true;
        }
        private void rcmThumbs_tagBase_Merge_Click(object sender, EventArgs e)
        {
            if (GUI_CheckForChanges()) return;
            if (!tagBase.IsOpen)
            {
                MessageBox.Show("You haven't opened a tagBase file.", "You are doing it wrong.",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return;
            }

            int iMerge = -1;
            int[] iImages = GetSelected();
            if (tagBase_Config_rdoMergeDB.Checked) iMerge = 1;
            if (tagBase_Config_rdoMergeTB.Checked) iMerge = 2;
            if (iMerge == -1)
            {
                string sMerge = InputBox.Show("Trying to merge with " +
                    iImages.Length + " images from open tagBase..." + "\r\n\r\n" +
                    "Select merge mode." + "\r\n" +
                    "1: Merge - use name/comments from database" + "\r\n" +
                    "2: Merge - use name/comments from tagBase" + "\r\n\r\n" +
                    "Other: Cancel", "Hello").Text;
                try { iMerge = Convert.ToInt32(sMerge); }
                catch { }
            }
            if (iMerge < 1 || iMerge > 2) return;

            frmWait.sHeader = ".:: tagBase ::.";
            frmWait.sMain = "Merging with tagBase";
            frmWait.sFooter = "Image 0 of " + iImages.Length;
            frmWait.bVisible = true;
            while (!frmWait.bActive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            string[] sHash = new string[iImages.Length];
            for (int a = 0; a < sHash.Length; a++)
                sHash[a] = idImages[iImages[a]].sHash;
            int iCnt = tagBase_Read(sHash, iMerge);
            tagBase_Write(sHash, 0);

            frmWait.sMain = "Reconstructing thumb view"; frmWait.bInstant = true;
            while (frmWait.bInstant) Application.DoEvents();
            idImages = DB.Search(LastSearch, LastSearchFallback);
            ShowPanel(pnThumbs); Application.DoEvents();
            RedrawThumbnails(true); ReloadThumbnails(false);
            iAMode = 0;

            frmWait.sHeader = "Click this window or press space to close";
            frmWait.sMain = "tagBase read";
            frmWait.bCloseAtClick = true;
        }
    }

    public class cb
    {
        public static string sAppPath = "";
        public static string sAppVer = "";

        public static byte[] qPicR(Bitmap bSrc)
        {
            try
            {
                unsafe
                {
                    int pxSize = 4;
                    int iW = bSrc.Width; int iH = bSrc.Height;
                    byte[] bPic = new byte[iW * iH * pxSize];

                    System.Drawing.Imaging.BitmapData bData =
                        bSrc.LockBits(new Rectangle(0, 0, iW, iH),
                        System.Drawing.Imaging.ImageLockMode.ReadOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    int iStride = bData.Stride;
                    IntPtr pScan0 = bData.Scan0;

                    for (int y = 0; y < iH; y++)
                    {
                        byte* pRow = (byte*)pScan0 + (y * iStride);
                        for (int x = 0; x < iW; x++)
                        {
                            int iLoc = ((y * iW) + x) * pxSize;
                            bPic[iLoc + 0] = pRow[x * pxSize + 3]; //A
                            bPic[iLoc + 1] = pRow[x * pxSize + 2]; //R
                            bPic[iLoc + 2] = pRow[x * pxSize + 1]; //G
                            bPic[iLoc + 3] = pRow[x * pxSize + 0]; //B
                        }
                    }

                    bSrc.UnlockBits(bData);
                    //bSrc.Dispose();

                    byte[] bResX = BitConverter.GetBytes(iW);
                    byte[] bResY = BitConverter.GetBytes(iH);
                    Array.Reverse(bResX); Array.Reverse(bResY);

                    byte[] bRet = new byte[bPic.Length + bResX.Length + bResY.Length];
                    bResX.CopyTo(bRet, 0);
                    bResY.CopyTo(bRet, bResX.Length);
                    bPic.CopyTo(bRet, bResX.Length + bResY.Length);
                    return bRet;
                }
            }
            catch { return new byte[0]; }
        }
        public static byte[, ,] qPicR2(Bitmap bSrc)
        {
            try
            {
                unsafe
                {
                    int pxSize = 4;
                    int iW = bSrc.Width; int iH = bSrc.Height;
                    byte[, ,] ret = new byte[iW, iH, 4];

                    System.Drawing.Imaging.BitmapData bData =
                        bSrc.LockBits(new Rectangle(0, 0, iW, iH),
                        System.Drawing.Imaging.ImageLockMode.ReadOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    int iStride = bData.Stride;
                    IntPtr pScan0 = bData.Scan0;

                    for (int y = 0; y < iH; y++)
                    {
                        byte* pRow = (byte*)pScan0 + (y * iStride);
                        for (int x = 0; x < iW; x++)
                        {
                            ret[x, y, 0] = pRow[x * pxSize + 3]; //A
                            ret[x, y, 1] = pRow[x * pxSize + 2]; //R
                            ret[x, y, 2] = pRow[x * pxSize + 1]; //G
                            ret[x, y, 3] = pRow[x * pxSize + 0]; //B
                        }
                    }

                    bSrc.UnlockBits(bData);
                    //bSrc.Dispose();
                    return ret;
                }
            }
            catch { return new byte[0, 0, 0]; }
        }
        public static Bitmap qPicW(byte[] bPic)
        {
            try
            {
                unsafe
                {
                    byte[] bResX = new byte[4];
                    byte[] bResY = new byte[4];
                    for (int a = 0; a < 4; a++)
                    {
                        bResX[a] = bPic[a + 0];
                        bResY[a] = bPic[a + 4];
                    }
                    Array.Reverse(bResX); Array.Reverse(bResY);
                    int iW = BitConverter.ToInt32(bResX, 0);
                    int iH = BitConverter.ToInt32(bResY, 0);
                    Bitmap ret = new Bitmap(iW, iH);

                    System.Drawing.Imaging.BitmapData bData =
                        ret.LockBits(new Rectangle(0, 0, iW, iH),
                        System.Drawing.Imaging.ImageLockMode.WriteOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    int iStride = bData.Stride;
                    IntPtr pScan0 = bData.Scan0;
                    int pxSize = 4;

                    for (int y = 0; y < iH; y++)
                    {
                        byte* pRow = (byte*)pScan0 + (y * iStride);
                        for (int x = 0; x < iW; x++)
                        {
                            int iLoc = ((y * iW) + x) * pxSize;
                            pRow[x * pxSize + 3] = bPic[iLoc + 0]; //A
                            pRow[x * pxSize + 2] = bPic[iLoc + 1]; //R
                            pRow[x * pxSize + 1] = bPic[iLoc + 2]; //G
                            pRow[x * pxSize + 0] = bPic[iLoc + 3]; //B
                        }
                    }

                    ret.UnlockBits(bData);
                    return ret;
                }
            }
            catch { return new Bitmap(1, 1); }
        }
        public static Bitmap qPicW(byte[, ,] baPic)
        {
            try
            {
                unsafe
                {
                    int iW = baPic.GetUpperBound(0) + 1;
                    int iH = baPic.GetUpperBound(1) + 1;
                    Bitmap ret = new Bitmap(iW, iH);

                    System.Drawing.Imaging.BitmapData bData =
                        ret.LockBits(new Rectangle(0, 0, iW, iH),
                        System.Drawing.Imaging.ImageLockMode.WriteOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    int iStride = bData.Stride;
                    IntPtr pScan0 = bData.Scan0;
                    int pxSize = 4;

                    for (int y = 0; y < iH; y++)
                    {
                        byte* pRow = (byte*)pScan0 + (y * iStride);
                        for (int x = 0; x < iW; x++)
                        {
                            pRow[x * pxSize + 3] = baPic[x, y, 0]; //A
                            pRow[x * pxSize + 2] = baPic[x, y, 1]; //R
                            pRow[x * pxSize + 1] = baPic[x, y, 2]; //G
                            pRow[x * pxSize + 0] = baPic[x, y, 3]; //B
                        }
                    }

                    ret.UnlockBits(bData);
                    return ret;
                }
            }
            catch { return new Bitmap(1, 1); }
        }
        public static byte[] qPicZ(byte[, ,] baPic)
        {
            int pxSize = 4;
            int iW = baPic.GetUpperBound(0) + 1;
            int iH = baPic.GetUpperBound(1) + 1;
            byte[] bPic = new byte[iW * iH * pxSize];
            for (int y = 0; y < iH; y++)
                for (int x = 0; x < iW; x++)
                    for (int c = 0; c < pxSize; c++)
                        bPic[(((y * iW) + x) * pxSize) + c] =
                            baPic[x, y, c];

            byte[] bResX = BitConverter.GetBytes(iW);
            byte[] bResY = BitConverter.GetBytes(iH);
            Array.Reverse(bResX); Array.Reverse(bResY);

            byte[] bRet = new byte[bPic.Length + bResX.Length + bResY.Length];
            bResX.CopyTo(bRet, 0);
            bResY.CopyTo(bRet, bResX.Length);
            bPic.CopyTo(bRet, bResX.Length + bResY.Length);
            return bRet;
        }
        public static byte[, ,] qPicZ(byte[] bPic)
        {
            int pxSize = 4;
            byte[] bResX = new byte[4];
            byte[] bResY = new byte[4];
            for (int a = 0; a < 4; a++)
            {
                bResX[a] = bPic[a + 0];
                bResY[a] = bPic[a + 4];
            }
            Array.Reverse(bResX); Array.Reverse(bResY);
            int iW = BitConverter.ToInt32(bResX, 0);
            int iH = BitConverter.ToInt32(bResY, 0);

            byte[, ,] ret = new byte[iW, iH, pxSize];
            for (int y = 0; y < iH; y++)
                for (int x = 0; x < iW; x++)
                {
                    int iLoc = ((y * iW) + x) * pxSize;
                    ret[x, y, 0] = bPic[iLoc + 0]; //A
                    ret[x, y, 1] = bPic[iLoc + 1]; //R
                    ret[x, y, 2] = bPic[iLoc + 2]; //G
                    ret[x, y, 3] = bPic[iLoc + 3]; //B
                }
            return ret;
        }

        public static Bitmap ResizeBitmap(Bitmap bPic, int iX, int iY, bool bKeepAspect, int iScaleMode, bool bEnlargen)
        {
            if (!bEnlargen)
            {
                if (iX > bPic.Width) iX = bPic.Width;
                if (iY > bPic.Height) iY = bPic.Height;
            }
            if (iX < 1) iX = 1; if (iY < 1) iY = 1;
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
                if (iScaleMode == 2) gOut.InterpolationMode =
                      System.Drawing.Drawing2D.InterpolationMode.High;
                if (iScaleMode == 3) gOut.InterpolationMode =
                      System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                gOut.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gOut.DrawImage(bPic, 0, 0, iX, iY);
            }
            return bOut;
        }
        public static Bitmap ResizeBitmap(Bitmap bPic, int iX, int iY, bool bKeepAspect, int iScaleMode)
        {
            return ResizeBitmap(bPic, iX, iY, bKeepAspect, iScaleMode, false);
        }
        public static bool bCmpBytes(byte[] b1, int iOfs, byte[] b2)
        {
            if (b1.Length < b2.Length + iOfs) return false;
            for (int a = 0; a < b2.Length; a++)
                if (b1[a + iOfs] != b2[a]) return false;
            return true;
        }
        public static string[] Split(string sSrc, string sDlm)
        {
            return sSrc.Split(new string[] { sDlm }, StringSplitOptions.None);
        }

        public static string FileRead(string sPath)
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(cb.sAppPath + sPath,
                    System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] b = new byte[fs.Length];
                fs.Read(b, 0, b.Length);
                fs.Close(); fs.Dispose();
                return System.Text.Encoding.UTF8.GetString(b);
            }
            catch { return ""; }
        }
        public static void FileWrite(string sPath, bool bAppend, string sVal)
        {
            try
            {
                System.IO.FileMode fMode = System.IO.FileMode.Create;
                if (bAppend) fMode = System.IO.FileMode.Append;
                System.IO.FileStream fs = new System.IO.FileStream(
                    cb.sAppPath + sPath, fMode, System.IO.FileAccess.Read);
                byte[] b = System.Text.Encoding.UTF8.GetBytes(sVal);
                fs.Write(b, 0, b.Length); fs.Close(); fs.Dispose();
            }
            catch { }
        }
        public static string MD5File(string sFile)
        {
            System.IO.FileStream fs = new System.IO.FileStream(
                sFile, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] bFile = new byte[fs.Length];
            fs.Read(bFile, 0, (int)fs.Length);
            fs.Close(); fs.Dispose();
            return MD5(bFile);
        }
        public static string MD5(byte[] bData)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider crypt =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bRet = crypt.ComputeHash(bData);
            string ret = "";
            for (int a = 0; a < bRet.Length; a++)
            {
                //string wat = bRet[a].ToString("x2");
                //while (wat.Length < 2) wat = "0" + wat;
                ret += bRet[a].ToString("x2"); //wat;
            }
            crypt.Clear(); return ret;
        }
        public static string AppendNumToFN(string sFilePath)
        {
            sFilePath = sFilePath.Replace("\\", "/");

            string sRoot = "";
            if (sFilePath.Contains("/")) sRoot =
                sFilePath.Substring(0, sFilePath.LastIndexOf("/") + 1);

            if (!System.IO.Directory.Exists(sRoot))
                System.IO.Directory.CreateDirectory(sRoot);

            string sFName = sFilePath.Substring(sRoot.Length);
            if (sFName.Contains("."))
                sFName = sFName.Substring(0, sFName.LastIndexOf("."));

            string sFExt = "";
            if (sFilePath.Length > sRoot.Length + sFName.Length)
                sFExt = sFilePath.Substring(sRoot.Length + sFName.Length);

            int iWrCnt = -1;
            if (System.IO.File.Exists(sRoot + sFName + sFExt))
            {
                iWrCnt = 1;
                while (System.IO.File.Exists(sRoot +
                    sFName + "(" + iWrCnt + ")" + sFExt))
                    iWrCnt++;
            }

            if (iWrCnt == -1) return sRoot + sFName + sFExt;
            else return sRoot + sFName + "(" + iWrCnt + ")" + sFExt;
        }

        public static int[] asIntArray(int iVal, int iLen)
        {
            int[] iaRet = new int[iLen];
            for (int a = 0; a < iaRet.Length; a++)
            {
                iaRet[a] = iVal;
            }
            return iaRet;
        }
        public static string RemoveDupes(string sVal)
        {
            while (sVal.Contains(" ,") || sVal.Contains(", "))
                sVal = sVal.Replace(" ,", ",").Replace(", ", ",");
            while (sVal.Contains(",,")) sVal = sVal.Replace(",,", ",");
            string[] saVal = sVal.Trim(',').Split(',');
            saVal = RemoveDupes(saVal);
            string sRet = "";
            for (int a = 0; a < saVal.Length; a++)
            {
                sRet += saVal[a] + ",";
            }
            return sRet.Trim(',', ' ');
        }
        public static string[] RemoveDupes(string[] saVal)
        {
            bool[] bInc = new bool[saVal.Length];
            for (int a = 0; a < bInc.Length; a++)
            {
                bInc[a] = true;
            }
            for (int a = 0; a < saVal.Length; a++)
            {
                for (int b = a + 1; b < saVal.Length; b++)
                {
                    if (saVal[a].ToLower() == saVal[b].ToLower())
                    {
                        bInc[b] = false;
                    }
                }
                if (saVal[a] == "") bInc[a] = false;
            }
            int iRetC = 0;
            for (int a = 0; a < saVal.Length; a++)
            {
                if (bInc[a]) iRetC++;
            }
            string[] saRet = new string[iRetC];
            iRetC = -1;
            for (int a = 0; a < saVal.Length; a++)
            {
                if (bInc[a])
                {
                    iRetC++;
                    saRet[iRetC] = saVal[a].Trim(',', ' ');
                }
            }
            return saRet;
        }
        public static string[] FilterArray(string[] saVal, string[] saPrm, int[] iaPrL, bool bCaseDiff)
        {
            int iCnt = 0; bool[] baInclude = new bool[saVal.Length];
            for (int a = 0; a < saVal.Length; a++)
            {
                for (int b = 0; b < saPrm.Length; b++)
                {
                    if (!baInclude[a])
                    {
                        string sCmp1 = saVal[a]; string sCmp2 = saPrm[b];
                        if (!bCaseDiff) { sCmp1 = sCmp1.ToLower(); sCmp2 = sCmp2.ToLower(); }
                        if (iaPrL[b] == 1) if (sCmp1.StartsWith(sCmp2)) baInclude[a] = true;
                        if (iaPrL[b] == 2) if (sCmp1.EndsWith(sCmp2)) baInclude[a] = true;
                        if (iaPrL[b] == 3) if (sCmp1.Contains(sCmp2)) baInclude[a] = true;
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
        public static string[] GetPaths(string sRoot, bool bRecursive)
        {
            Application.DoEvents();
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
    }
    public class thPageData
    {
        public Bitmap[] bImages = new Bitmap[0];
        public string[] sHashes = new string[0];
        public Point idRange = new Point(-1, -1);
    }
    public class ImageData
    {
        public bool bMod = false;
        public bool bSel = false;
        public bool bDel = false;
        public string sType = "";
        public string sHash = "";
        public char[] cFlag = new char[] { };
        public string sPath = "";
        public string sThmb = "";
        public Point ptRes = new Point(-1, -1);
        public long iLen = -1;
        public string sName = "";
        public string sCmnt = "";
        public int iRate = -1;
        public string sTGen = "";
        public string sTSrc = "";
        public string sTChr = "";
        public string sTArt = "";

        public ImageData()
        {
            cFlag = new char[32];
            for (int a = 0; a < 32; a++)
                cFlag[a] = '0';
        }
    }
    public class SearchPrm
    {
        public bool bDefRet = false;
        public string sImagName = "";
        public string sImagPath = "";
        public string sImagFormat = "";
        public string sImagRes = "";
        public string sImagRating = "";
        public string sTagsGeneral = "";
        public string sTagsSource = "";
        public string sTagsChars = "";
        public string sTagsArtists = "";
        public char[] cFlagDeny;
        public char[] cFlagRet;

        public SearchPrm()
        {
            bDefRet = true;
            Prepare();
        }
        public SearchPrm(bool bDTR)
        {
            bDefRet = bDTR;
            Prepare();
        }
        private void Prepare()
        {
            cFlagDeny = new char[32];
            cFlagRet = new char[32];
            for (int a = 0; a < 32; a++)
            {
                cFlagDeny[a] = '0';
                cFlagRet[a] = '0';
            }
        }
    }
    public class tmr
    {
        [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
        [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);
        private long l1, l2, lf;

        public tmr()
        {
            l1 = 0; l2 = 0;
            if (QueryPerformanceFrequency(out lf) == false)
                throw new Win32Exception();
        }
        public void Start()
        {
            QueryPerformanceCounter(out l1);
        }
        public void Stop()
        {
            QueryPerformanceCounter(out l2);
        }
        public double Ret
        {
            get { return (double)(l2 - l1) / (double)lf; }
        }
    }
    public class lst
    {
        private bool bWithCount;
        private object[] lstElm;
        public long[] Cnt;
        public long Len;

        public string gStr(long i)
        {
            return (string)lstElm[i];
        }
        public long gInt(long i)
        {
            return (long)lstElm[i];
        }

        public lst(bool bWCount)
        {
            bWithCount = bWCount;
            mkLst(1);
        }
        public lst(bool bWCount, int iStartSize)
        {
            bWithCount = bWCount;
            mkLst(iStartSize);
        }
        private void mkLst(int iSize)
        {
            lstElm = new object[iSize];
            Cnt = new long[iSize];
        }
        private void Enlargen()
        {
            object[] olstElm = (object[])lstElm.Clone();
            lstElm = new object[olstElm.Length * 2];
            olstElm.CopyTo(lstElm, 0);
            long[] oCnt = Cnt;
            Cnt = new long[lstElm.Length];
            oCnt.CopyTo(Cnt, 0);
        }

        public void Add(object elm)
        {
            if (bWithCount)
            {
                string sCmp = elm.ToString();
                for (int a = 0; a < Len; a++)
                    if (lstElm[a].ToString() == sCmp)
                    {
                        Cnt[a]++; return;
                    }
            }
            if (Len >= lstElm.Length) Enlargen();
            lstElm[Len] = elm; Len++;
        }
        //TODO: Add support for WithCount in the Remove function
        public void Rem(object elm)
        {
            string sCmp = elm.ToString();
            for (int a = 0; a < Len; a++)
            {
                if (lstElm[a].ToString() == sCmp) Len--;
            }
            int iLoc = 0;
            object[] olstElm = (object[])lstElm.Clone();
            lstElm = new object[olstElm.Length];
            for (int a = 0; a < Len; a++)
            {
                if (olstElm[a] != elm) lstElm[iLoc] = olstElm[a];
                iLoc++;
            }
        }
        public long Find(object elm)
        {
            string sCmp = elm.ToString();
            for (int a = 0; a < Len; a++)
            {
                if (lstElm[a].ToString() ==
                    sCmp) return a;
            }
            return -1;
        }
    }
    public class FBind
    {
        public System.IO.MemoryStream msFile1, msFile2;
        public string sName, sHashNow, sHashOld;
        public string sHashUnk = "Unknown";
    }
    public class pFBind
    {
        public static byte[] BoundGen = new byte[] {
            (byte)'!', (byte)'!', (byte)'!', (byte)'p', (byte)'F', (byte)'B',
            (byte)'i', (byte)'n', (byte)'d', (byte)'!', (byte)'!', (byte)'!' };
        public static byte[] BoundCrc = new byte[] {
            (byte)'!', (byte)'c', (byte)'r', (byte)'c', (byte)':' };

        public static FBind Extract(string sFile)
        {
            System.IO.FileStream fs = new System.IO.FileStream(sFile,
                System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] b = new byte[fs.Length];
            fs.Read(b, 0, (int)fs.Length);
            fs.Close(); fs.Dispose();

            int of1a = -1, of2a = -1, of3a = -1;
            int of1b = -1, of2b = -1, of3b = -1;
            for (int a = 0; a < b.Length; a++)
            {
                if (of1a == -1 || of2a == -1)
                    if (cb.bCmpBytes(b, a, BoundGen))
                    {
                        if (of1a == -1) of1a = a;
                        else if (of2a == -1) of2a = a;
                    }
                if (of3a == -1)
                    if (cb.bCmpBytes(b, a, BoundCrc))
                    {
                        if (of3a == -1) of3a = a;
                    }
            }
            of1b = of1a + BoundGen.Length;
            of2b = of2a + BoundGen.Length;
            of3b = of3a + BoundCrc.Length;
            if (of3a == -1) { of3a = b.Length; of3b = of3a; }

            Crc32 crc = new Crc32(); FBind ret = new FBind();
            ret.sName = System.Text.Encoding.ASCII.GetString(b, of1b, of2a - of1b);
            ret.msFile1 = new System.IO.MemoryStream();
            ret.msFile2 = new System.IO.MemoryStream();
            ret.msFile1.Write(b, 0, of1a);
            ret.msFile2.WriteByte(b[of2b + 0]);
            ret.msFile2.WriteByte(b[of2b + 2]);
            ret.msFile2.WriteByte(b[of2b + 4]);
            ret.msFile2.Write(b, of2b + 6, (of3a - of2b) - 6);
            ret.sHashNow = crc.Hash(ret.msFile2).ToString("X8");
            if (of3a == b.Length) ret.sHashOld = ret.sHashUnk;
            else ret.sHashOld = System.Text.Encoding.ASCII.GetString(b, of3b, 8);
            ret.msFile1.Seek(0, System.IO.SeekOrigin.Begin);
            ret.msFile2.Seek(0, System.IO.SeekOrigin.Begin);
            return ret;
        }
        public static void Make(string sFile1, string sFile2, string sName, string sOut)
        {
            System.IO.FileStream fs1 = new System.IO.FileStream(sFile1,
                System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] bfs1 = new byte[fs1.Length];
            fs1.Read(bfs1, 0, bfs1.Length);
            fs1.Flush(); fs1.Close(); fs1.Dispose();

            System.IO.FileStream fs2 = new System.IO.FileStream(sFile2,
                System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] bfs2 = new byte[fs2.Length];
            fs2.Read(bfs2, 0, bfs2.Length);
            fs2.Flush(); fs2.Close(); fs2.Dispose();

            Crc32 crc = new Crc32();
            string sCrc = crc.Hash(bfs2).ToString("X8");
            byte[] bCrc = System.Text.Encoding.ASCII.GetBytes(sCrc);
            byte[] bFN = System.Text.Encoding.ASCII.GetBytes(sName);

            System.IO.MemoryStream mso = new System.IO.MemoryStream();
            mso.Write(bfs1, 0, bfs1.Length);
            mso.Write(BoundGen, 0, BoundGen.Length);
            mso.Write(bFN, 0, bFN.Length);
            mso.Write(BoundGen, 0, BoundGen.Length);
            mso.Write(new byte[] { 
                bfs2[0], (byte)186, bfs2[1], (byte)186, 
                bfs2[2], (byte)186 }, 0, 6);
            mso.Write(bfs2, 3, bfs2.Length - 3);
            mso.Write(BoundCrc, 0, BoundCrc.Length);
            mso.Write(bCrc, 0, bCrc.Length);

            System.IO.FileStream fso = new System.IO.FileStream(sOut,
                System.IO.FileMode.Create, System.IO.FileAccess.Write);
            mso.WriteTo(fso); mso.Close(); mso.Dispose();
            fso.Flush(); fso.Close(); fso.Dispose();
        }
        public static bool isFBind(string sFile)
        {
            System.IO.FileStream fs = new System.IO.FileStream(sFile,
                    System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] bFile = new byte[fs.Length];
            fs.Read(bFile, 0, (int)fs.Length);
            fs.Close(); fs.Dispose();
            return isFBind(bFile);
        }
        public static bool isFBind(byte[] bFile)
        {
            for (int a = 0; a < bFile.Length; a++)
            {
                if (cb.bCmpBytes(bFile, a, BoundGen))
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class emTags
    {
        public static byte[] bDlm1 = System.Text.Encoding.ASCII.GetBytes("<pImgDB>");
        public static byte[] bDlm2 = System.Text.Encoding.ASCII.GetBytes("</pImgDB>");
        public static string[] sFmtH = new string[] { "jpg", "png", "gif" };
        private static string[] sOrdr = "Name,Cmnt,tGen,tSrc,tChr,tArt".Split(',');
        private static string[] sXML1 = "<Name>,<Cmnt>,<tGen>,<tSrc>,<tChr>,<tArt>".Split(',');
        private static string[] sXML2 = "</Name>,</Cmnt>,</tGen>,</tSrc>,</tChr>,</tArt>".Split(',');

        public static byte[][] bFmtH = new byte[][] { 
            new byte[]{ 0xff, 0xd8, 0xff },  //jpg - ff,d8,ff
            new byte[]{ 0x89, 0x50, 0x4e },  //png - 89,50,4e
            new byte[]{ 0x47, 0x49, 0x46 }}; //gif - 47,49,46

        public static byte[][] bFmtF = new byte[][] {
            new byte[]{ 0xff, 0xd9 }, //jpg - ff,d9
            new byte[]{ //png - 00,00,00,00,49,45,4E,44,AE,42,60,82
                0x00, 0x00, 0x00, 0x00, 
                0x49, 0x45, 0x4E, 0x44, 
                0xAE, 0x42, 0x60, 0x82 },
            new byte[]{ 0x00, 0x3b }}; //gif - 00,3b

        private static Point MetaArea(byte[] bFile, int iInitOfs)
        {
            int iOfs = -1; int iLen = -1;
            for (int a = iInitOfs; a < bFile.Length; a++)
            {
                if (iLen != -1) break;
                if (iOfs == -1) if (cb.bCmpBytes(bFile, a, bDlm1)) iOfs = a;
                if (iOfs != -1) if (cb.bCmpBytes(bFile, a, bDlm2))
                        iLen = (a - iOfs) + bDlm2.Length;
            }
            return new Point(iOfs, iLen);
        }
        public static bool TagsWrite(string sPath, bool bKeepMeta, string[] sTags)
        {
            byte[] bFile = null;
            try
            {
                System.IO.FileStream fsr = new System.IO.FileStream(sPath,
                    System.IO.FileMode.Open, System.IO.FileAccess.Read);
                bFile = new byte[fsr.Length];
                fsr.Read(bFile, 0, bFile.Length);
                fsr.Close(); fsr.Dispose();
            }
            catch { return false; }

            int iType = GetType(bFile);
            if (iType == -1) return false;


            // FIND OFFSETS FOR:  IMAGE, METADATA, OLD TAGS
            int iFileLen = -1; //string sWut = "";
            int iMetaOfs1 = -1; int iMetaLen1 = -1;
            int iMetaOfs2 = -1; int iMetaLen2 = -1;
            for (int a = 0; a < bFile.Length; a++)
            {
                //if (cb.bCmpBytes(bFile, a, new byte[] { 0xff, 0xd8 })) sWut += "o";
                //if (cb.bCmpBytes(bFile, a, new byte[] { 0xff, 0xd9 })) sWut += "Ø";
                if (cb.bCmpBytes(bFile, a, bFmtF[iType]))
                {
                    iFileLen = a + bFmtF[iType].Length;
                }
                if (cb.bCmpBytes(bFile, a, pFBind.BoundGen))
                {
                    break;
                }
            }
            //if (sPath.ToLower().EndsWith(".jpg") || sPath.ToLower().EndsWith(".jpeg"))
            //if (sWut != "oØ" && sWut != "ooØoØØ" && sWut != "ooØØ")
            //MessageBox.Show(sPath + "\r\n\r\n" + sWut);
            if (iFileLen != bFile.Length)
                if (bKeepMeta)
                {
                    Point ptOfs = MetaArea(bFile, 0);
                    if (ptOfs == new Point(-1, -1))
                    {
                        iMetaOfs1 = iFileLen;
                        iMetaLen1 = bFile.Length - iFileLen;
                    }
                    else
                    {
                        iMetaOfs1 = iFileLen;
                        iMetaLen1 = ptOfs.X - iFileLen;
                        iMetaOfs2 = ptOfs.X + ptOfs.Y;
                        iMetaLen2 = bFile.Length - iMetaOfs2;
                    }
                }


            // PREPARE TAGS BYTE ARRAY
            string sTW = ""; byte[] bTW = new byte[0];
            if (sTags.Length != sOrdr.Length) sTags = new string[sOrdr.Length];
            for (int a = 0; a < sTags.Length; a++) if (sTags[a] == null) sTags[a] = "";
            for (int a = 0; a < sTags.Length; a++) if (sTags[a] != "") sTW +=
                    "<" + sOrdr[a] + ">" + sTags[a] + "</" + sOrdr[a] + ">";
            if (sTW != "") bTW = System.Text.Encoding.UTF8.GetBytes(sTW);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ms.Write(bFile, 0, iFileLen);
            if (bTW.Length > 0)
            {
                ms.Write(bDlm1, 0, bDlm1.Length);
                ms.Write(bTW, 0, bTW.Length);
                ms.Write(bDlm2, 0, bDlm2.Length);
            }
            if (iMetaOfs1 != -1) ms.Write(bFile, iMetaOfs1, iMetaLen1);
            if (iMetaOfs2 != -1) ms.Write(bFile, iMetaOfs2, iMetaLen2);


            // WRITE TO FILE
            string sTmpPath = cb.sAppPath + "z_tmp/emTags.tmp";
            System.IO.FileStream fsw = new System.IO.FileStream(
                sTmpPath, System.IO.FileMode.Create);
            ms.WriteTo(fsw); ms.Close(); ms.Dispose();
            fsw.Flush(); fsw.Close(); fsw.Dispose();

            try
            {
                System.IO.File.Delete(sPath);
                System.IO.File.Move(sTmpPath, sPath);
            }
            catch { return false; }
            return true;
        }
        public static string[] TagsRead(string sPath)
        {
            //<pImgDB>
            //<Name> Image name    </Name>
            //<Cmnt> Image comment </Cmnt>
            //<tGen> Tags General  </tGen>
            //<tSrc> Tags Source   </tSrc>
            //<tChr> Tags Chars    </tChr>
            //<tArt> Tags Artist   </tArt>
            //</pImgDB>
            byte[] bFile = null;
            try
            {
                System.IO.FileStream fsr = new System.IO.FileStream(sPath,
                    System.IO.FileMode.Open, System.IO.FileAccess.Read);
                bFile = new byte[fsr.Length];
                fsr.Read(bFile, 0, bFile.Length);
                fsr.Close(); fsr.Dispose();
            }
            catch { return new string[0]; }
            return TagsRead(bFile);
        }
        public static string[] TagsRead(byte[] bFile)
        {
            Point ptOfs = MetaArea(bFile, 0);
            if (ptOfs == new Point(-1, -1))
                return new string[0];
            ptOfs.X += bDlm1.Length;
            ptOfs.Y -= bDlm1.Length + bDlm2.Length;

            // Prepare ret array
            string[] sRet = new string[sOrdr.Length];
            for (int a = 0; a < sRet.Length; a++) sRet[a] = "";

            string sTags = System.Text.Encoding.UTF8.GetString(bFile, ptOfs.X, ptOfs.Y);
            for (int a = 0; a < sOrdr.Length; a++)
            {
                // If contain tag, split from <TagD>
                if (sTags.Contains(sXML1[a])) sRet[a] = cb.Split(sTags, sXML1[a])[1];
                // If contain </TagD>, split to that
                if (sRet[a].Contains(sXML2[a])) sRet[a] = cb.Split(sRet[a], sXML2[a])[0];
                else if (sRet[a].Contains("<")) sRet[a] = sRet[a].Split('<')[0];
                // If not, try split to "<"
            }
            return sRet;
        }
        public static int GetType(byte[] bFile)
        {
            if (bFile.Length < 3) return -1;
            for (int a = 0; a < bFmtH.Length; a++)
            {
                bool bFound = true;
                for (int b = 0; b < bFmtH[a].Length; b++)
                    if (bFile[b] != bFmtH[a][b])
                        bFound = false;
                if (bFound) return a;
            }
            return -1;
        }
        public static int GetType(string sPath)
        {
            try
            {
                System.IO.FileStream fsr = new System.IO.FileStream(sPath,
                    System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] bFile = new byte[3];
                fsr.Read(bFile, 0, bFile.Length);
                fsr.Close(); fsr.Dispose();
                return GetType(bFile);
            }
            catch { }
            return -1;
        }
    }
    public class WebServer
    {
        public static bool bEnabled = false;
        public static bool bStop = false;
        public static int iActive = 0;
        public static int iPort = -1;
        public static string Cred_GuestPWD = "guest";
        public static string Cred_AdminUSR = "admin";
        public static string Cred_AdminPWD = "admin";
        public static Thread thListen;
        public static TcpListener sckListen;
        public static WebCliInf[] wci;
        public static string sLog = "";
        private static bool bResumeListen = true;
        private static Random rnd = new Random();
        private static string[] StrTableID;
        private static string[] StrTableVL;
        private static string[] LoConfigID;
        private static string[] LoConfigVL;
        private static int iThumbsPerPage = 1;

        public static void Start(int iListenPort)
        {
            //Create string table
            string[] StrTable = cb.Split(("\r\n" +
                cb.FileRead("z_webserv/stringtable.txt"))
                .Replace("\r", ""), "\nSTR:");
            StrTableID = new string[StrTable.Length - 1];
            StrTableVL = new string[StrTable.Length - 1];
            for (int a = 1; a < StrTable.Length; a++)
            {
                StrTable[a] = StrTable[a].Trim('\n');
                StrTableID[a - 1] = StrTable[a].Substring(0, StrTable[a].IndexOf(" ")).Trim(' ');
                StrTableVL[a - 1] = StrTable[a].Substring(StrTable[a].IndexOf(" ") + 1).Trim(' ');
            }

            //Create config table
            string[] LoConfig = cb.Split(("\r\n" +
                cb.FileRead("z_webserv/layoutcfg.txt"))
                .Replace("\r", ""), "\n\nLO!CONFIG::");
            LoConfigID = new string[LoConfig.Length - 1];
            LoConfigVL = new string[LoConfig.Length - 1];
            for (int a = 1; a < LoConfig.Length; a++)
            {
                LoConfig[a] = LoConfig[a].Trim('\n');
                LoConfigID[a - 1] = LoConfig[a].Substring(0, LoConfig[a].IndexOf("\n")).Trim(' ');
                LoConfigVL[a - 1] = LoConfig[a].Substring(LoConfig[a].IndexOf("\n") + 1).Replace("\n", "\r\n");
            }

            //Make session information holders
            wci = new WebCliInf[1000];
            for (int a = 0; a < wci.Length; a++)
            {
                wci[a] = new WebCliInf();
            }

            //BROWSE: Calculate thumbs per page
            iThumbsPerPage = cb.Split(cb.FileRead("z_webserv/browse.html"),
                "[[LO!Browse_ImageLink_Thumb_").Length - 1;

            //Setting variables and helpers
            thListen = new Thread(new ThreadStart(voListen));
            thListen.IsBackground = true;
            iPort = iListenPort;

            //Ready for take-off.
            thListen.Start();
            UPnP.OpenFirewallPort(iPort);
            Log("UPnP operation completed");
        }
        public static void Stop()
        {
            bStop = true;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(System.Net.IPAddress.Parse("127.0.0.1"), iPort);
            socket.Disconnect(false); socket.Close();
        }
        public static void voListen()
        {
            bStop = false;
            bEnabled = true;

            sckListen = new TcpListener(System.Net.IPAddress.Any, iPort);
            sckListen.Start();
            while (true)
            {
                if (bStop) break;
                Thread thHandle = new Thread(new ThreadStart(HandleConnection));
                thHandle.IsBackground = true; bResumeListen = false; thHandle.Start();
                while (!bResumeListen) System.Threading.Thread.Sleep(10);
            }

            sckListen.Stop();
            bEnabled = false;
            bStop = false;
            Log("Webserver disabled");
        }
        public static void Log(string s)
        {
            if (bStop) return;
            sLog = DateTime.Now.ToLongTimeString() +
                " - " + s + "\r\n" + sLog;
        }

        private static string GetFromStrTbl(string sValueIdentifier)
        {
            for (int a = 0; a < StrTableID.Length; a++)
            {
                if (StrTableID[a] == sValueIdentifier)
                    return StrTableVL[a];
            }
            return "!OH,SNAP!";
        }
        private static string GetFromLoConf(string sValueIdentifier)
        {
            for (int a = 0; a < LoConfigID.Length; a++)
            {
                if (LoConfigID[a] == sValueIdentifier)
                    return LoConfigVL[a];
            }
            return "!OH,SNAP!";
        }
        private static string TimeFromTick(long lTime)
        {
            if (lTime < 0) return "";
            DateTime dt = new DateTime(lTime * 10000000);
            return
                dt.DayOfWeek + " " +
                dt.ToLongDateString() + ", " +
                dt.ToLongTimeString();
        }
        private static int sckDataLen(Socket sck)
        {
            if (sck.Available == 0)
            {
                sck.Poll(100 * 1000, SelectMode.SelectRead);
                if (sck.Available == 0)
                {
                    sck.Poll(30 * 1000 * 1000, SelectMode.SelectRead);
                }
            }
            return sck.Available;
        }

        private static void HandleConnection()
        {
            Socket sck = sckListen.AcceptSocket(); bResumeListen = true;
            string sIP = sck.RemoteEndPoint.ToString().Split(':')[0];
            iActive++; //Log(sIP + " >> Connected");
            long lGSec = DateTime.Now.Ticks / 10000000;
            while (true) //try
            {
                #region Get request
                //Set socket timeouts
                sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 30000);
                sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 30000);

                //Read HTML header
                string sHeader = "";
                byte[] bHeader = new byte[1];
                try
                {
                    while (sck.Receive(bHeader) > 0)
                    {
                        sHeader += (char)bHeader[0] + "";
                        if (sHeader.EndsWith("\r\n\r\n")) break;
                    }
                }
                catch { }

                //Clean up and split header
                sHeader = sHeader.Replace("\r", "\n").Trim('\n');
                while (sHeader.Contains("\n\n"))
                    sHeader = sHeader.Replace("\n\n", "\n");
                string[] saHeader = sHeader.Split('\n');

                //Parse HTML
                string sReqUrl = "";
                string sDataBound = "";
                string[] sReqCookies = new string[0];
                int iDataLen = 0;
                for (int a = 0; a < saHeader.Length; a++)
                {
                    //Requested path
                    if (saHeader[a].Contains(" HTTP/"))
                    {
                        sReqUrl = saHeader[a];
                        sReqUrl = sReqUrl.Substring(sReqUrl.IndexOf(" ") + 1);
                        sReqUrl = sReqUrl.Substring(0, sReqUrl.LastIndexOf(" "));
                    }
                    //Cookies
                    if (saHeader[a].StartsWith("Cookie: "))
                    {
                        string sTmp = saHeader[a].Substring(8);
                        sReqCookies = cb.Split(sTmp.Split('\n')[0], "; ");
                    }
                    //Content length
                    if (saHeader[a].StartsWith("Content-Length: "))
                    {
                        iDataLen = Convert.ToInt32(saHeader[a].Substring(16));
                    }
                    //Postdata boundary
                    if (saHeader[a].StartsWith("Content-Type: multipart/form-data;"))
                    {
                        sDataBound = "--" + saHeader[a].Substring(saHeader[a].IndexOf("boundary=") + 9);
                        //sDataBound = sDataBound.Substring(0, sDataBound.IndexOf("\r\n"));
                    }
                }

                //Read HTML data
                int iBytesRead = 0;
                System.IO.MemoryStream msData = new System.IO.MemoryStream();
                while (iBytesRead < iDataLen)
                {
                    byte[] b = new byte[1024];
                    int iCnt = Math.Min(b.Length, sckDataLen(sck));
                    int iThCnt = sck.Receive(b, 0, iCnt, SocketFlags.None);
                    msData.Write(b, 0, iThCnt);
                    iBytesRead += iThCnt;
                }
                msData.Seek(0, System.IO.SeekOrigin.Begin);
                #endregion
                #region Parse request
                //Fallback to new user
                int iWCI = -1;
                string sSid = "";
                bool bSID_Has = false;
                bool bSID_Valid = false;
                bool bSID_MadeVld = false;
                bool bSID_MadeInvld = false;

                //Look for SID in cookies
                for (int a = 0; a < sReqCookies.Length; a++)
                    if (sReqCookies[a].StartsWith("SID="))
                    {
                        //User had a SID assigned
                        bSID_Has = true;
                        sSid = sReqCookies[a].Substring(4);
                    }

                //Create SID if none exists
                if (!bSID_Has)
                    sSid = rnd.Next(0x1000, 0x10000).ToString("X") +
                        rnd.Next(0x1000, 0x10000).ToString("X");

                //Try to identify SID
                for (int a = 0; a < wci.Length; a++)
                {
                    if (wci[a].sSID == sSid)
                    {
                        //User has valid SID
                        bSID_Valid = true;
                        iWCI = a; break;
                    }
                }

                //Find clean WCI if SID is invalid
                if (!bSID_Valid)
                {
                    iWCI = -1; int iOldest = 0;
                    for (int a = 0; a < wci.Length; a++)
                    {
                        //If clean WCI, tag for assign
                        if (wci[a].sSID == "") { iWCI = a; break; }
                        //If older WCI, tag for assign (secondary)
                        if (wci[a].lUsed < wci[iOldest].lUsed) iOldest = a;
                    }
                    //If no blank WCI, assign secondary (oldest)
                    if (iWCI == -1) iWCI = iOldest;
                    wci[iWCI] = new WebCliInf();
                }

                //Set fundamental variables
                bool bStatic = false;
                string sSetLoc = "";
                string sReqMime = "text/html; charset=utf-8";
                string sDefMime = "text/html; charset=utf-8";
                string sBody = "";
                byte[] bBody = null;
                #endregion

                #region req Login/out
                //Requested the login page
                if (sReqUrl == "/login")
                {
                    string sData = "";
                    byte[] bData = new byte[msData.Length];
                    msData.Read(bData, 0, bData.Length);
                    sData = System.Text.Encoding.UTF8.GetString(bData);

                    string[] saData = cb.Split(sData, sDataBound);
                    for (int a = 0; a < saData.Length; a++)
                    {
                        string sPrmName = saData[a];
                        if (sPrmName.Length > 8)
                        {
                            sPrmName = cb.Split(sPrmName, "data; name=\"")[1].Split('"')[0];
                            if (sPrmName == "user") wci[iWCI].sUser = saData[a].Split('\n')[3].Trim('\r');
                            if (sPrmName == "pass") wci[iWCI].sPass = saData[a].Split('\n')[3].Trim('\r');
                        }
                    }

                    if ((Cred_AdminUSR == wci[iWCI].sUser &&
                        Cred_AdminPWD == wci[iWCI].sPass) ||
                        Cred_GuestPWD == wci[iWCI].sPass)
                    {
                        bSID_Valid = true; bSID_MadeVld = true;
                        wci[iWCI].sSID = sSid;
                    }
                    else
                    {
                        bSID_Valid = false; bSID_MadeInvld = true;
                    }
                    sReqUrl = "/";
                }

                //Requested the logout page
                if (sReqUrl == "/logout")
                {
                    if (iWCI >= 0) wci[iWCI] = new WebCliInf();
                    if (bSID_Valid) bSID_MadeInvld = true;
                    sReqUrl = "/";
                }
                #endregion
                #region req Index, info
                //Requested the index page
                if (sReqUrl == "/")
                {
                    sBody = cb.FileRead("z_webserv/index.html");

                    if (bSID_Valid && !bSID_MadeInvld)
                    {
                        //Remove login-form if session is valid
                        string[] saBody = sBody.Replace("\r", "").Split('\n');
                        bool bAddThis = true; sBody = "";
                        foreach (string sTmp in saBody)
                        {
                            string sDm = sTmp.Trim('\t', ' ');
                            if (sDm == "<!-- [[ LOGIN ]] -->") bAddThis = false;
                            if (bAddThis) sBody += sTmp + "\r\n";
                            if (sDm == "<!-- [[ /LOGIN ]] -->") bAddThis = true;
                        }
                    }
                }

                //Requested the info page
                if (sReqUrl == "/info")
                {
                    sBody = cb.FileRead("z_webserv/info.html");
                }
                #endregion
                #region req Search, browse
                //Requested the search page
                if (sReqUrl == "/search")
                {
                    if (!bSID_Valid)
                        sBody = FallbackTo(GetFromStrTbl("USER_NOT_LOGGED_IN"));

                    else if (msData.Length == 0)
                    {
                        sBody = cb.FileRead("z_webserv/search.html");
                    }
                    else
                    {
                        string sData = "";
                        byte[] bData = new byte[msData.Length];
                        msData.Read(bData, 0, bData.Length);
                        sData = System.Text.Encoding.UTF8.GetString(bData);

                        string sSrcP = cb.Split(sData, sDataBound)[1];
                        //sSrcP = cb.Split(sSrcP, "data; name=\"")[1].Split('"')[0];
                        sSrcP = sSrcP.Split('\n')[3].Trim('\r');

                        SearchPrm sp = new SearchPrm();
                        sp.sImagName = sSrcP;
                        sp.sTagsGeneral = sSrcP;
                        sp.sTagsSource = sSrcP;
                        sp.sTagsChars = sSrcP;
                        sp.sTagsArtists = sSrcP;

                        wci[iWCI].sp = sp;
                        wci[iWCI].iCurPage = 1;
                        wci[iWCI].iCurImg = 1;

                        sBody = FallbackTo("Redirecting...");
                        sSetLoc = "/browse";
                    }
                }

                //Requested the browse page
                if (sReqUrl.StartsWith("/browse"))
                {
                    if (!bSID_Valid)
                        sBody = FallbackTo(GetFromStrTbl("USER_NOT_LOGGED_IN"));

                    else if (iThumbsPerPage == 0)
                        sBody = FallbackTo("Could not parse your template.<br>\r\n" +
                            "Error in: browse.html<br><br>\r\n" +
                            "Please verify your spelling of this parameter:<br>\r\n" +
                            "[[LO!Browse_ImageLink_Thumb_%n]]");

                    else
                    {
                        sBody = cb.FileRead("z_webserv/browse.html");
                        wci[iWCI].idImages = DB.Search(wci[iWCI].sp, 2);
                        wci[iWCI].iPages = (int)Math.Ceiling(
                            (double)wci[iWCI].idImages.Length /
                            (double)iThumbsPerPage);

                        string sSearch = wci[iWCI].sp.sTagsGeneral;
                        if (sSearch != "")
                            sBody = sBody.Replace("[[BROWSE_MODE]]", GetFromStrTbl("BROWSE_MODE_SEARCH"));
                        else sBody = sBody.Replace("[[BROWSE_MODE]]", GetFromStrTbl("BROWSE_MODE_ALL"));
                        sBody = sBody.Replace("[[SEARCH_PARAM]]", sSearch);

                        if (sReqUrl != "/browse")
                        {
                            int iPage; int.TryParse(sReqUrl.Substring(8), out iPage);
                            if (iPage == 0) iPage = 1; wci[iWCI].iCurPage = iPage;
                        }
                        int iOfs = (wci[iWCI].iCurPage - 1) * iThumbsPerPage;
                        for (int a = 0; a < iThumbsPerPage; a++)
                        {
                            string sPath = "/static/blank.gif";
                            int iThis = iOfs + a;
                            if (iThis < wci[iWCI].idImages.Length)
                                sPath = wci[iWCI].idImages[iThis].sHash;
                            sPath = "/grab/" + sPath;

                            string sThRef = "[[LO!Browse_ImageLink_Thumb_%n]]".Replace("%n", (a + 1) + "");
                            sBody = sBody.Replace(sThRef, GetFromLoConf("Browse_ImageLink_Thumb_%n"))
                                .Replace("[[IMAGE_DURL]]", sPath)
                                .Replace("[[THUMB_DURL]]", sPath + "th");
                        }
                    }
                }
                #endregion
                #region req Grab
                if (sReqUrl.StartsWith("/grab/"))
                {
                    string sHash = sReqUrl.Substring(6);
                    bool bThumb = sReqUrl.EndsWith("th");
                    if (bThumb) sHash = sHash.Substring
                        (0, sHash.Length - 2);

                    ImageData idInf = DB.Contains(sHash, true);
                    if (idInf.sHash != "")
                    {
                        if (idInf.sType == "jpg") sReqMime = "image/jpeg";
                        if (idInf.sType == "jpeg") sReqMime = "image/jpeg";
                        if (idInf.sType == "png") sReqMime = "image/png";
                        if (idInf.sType == "gif") sReqMime = "image/gif";
                        if (idInf.sType == "bmp") sReqMime = "image/bmp";
                        string sFPath = cb.sAppPath + DB.Path + idInf.sPath;

                        if (bThumb)
                        {
                            Bitmap bmTmp = new Bitmap(sFPath);
                            if (bmTmp.Height > bmTmp.Width)
                                bmTmp.RotateFlip(RotateFlipType.Rotate270FlipNone);

                            double dW = (double)bmTmp.Width;
                            double dH = (double)bmTmp.Height;
                            double dA = dW / dH;
                            Rectangle rcCrop = new Rectangle(0, 0, (int)dW, (int)dH);
                            if (dA > 1.6) dW = dH * 1.6; //too wide
                            else dH = dW / 1.6; //too tall
                            rcCrop.X = (rcCrop.Width - (int)dW) / 2;
                            rcCrop.Y = (rcCrop.Height - (int)dH) / 2;
                            rcCrop.Width = (int)dW;
                            rcCrop.Height = (int)dH;

                            Bitmap bmImg = (Bitmap)bmTmp.Clone(rcCrop, bmTmp.PixelFormat);
                            Bitmap bmTh = cb.ResizeBitmap(bmImg, 250, 250, true, 2);

                            System.Drawing.Imaging.ImageCodecInfo iCodec = null;
                            foreach (System.Drawing.Imaging.ImageCodecInfo iCd in
                                System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders())
                                if (iCd.MimeType == "image/jpeg") iCodec = iCd;
                            System.Drawing.Imaging.EncoderParameters iEnc =
                                new System.Drawing.Imaging.EncoderParameters(1);
                            iEnc.Param[0] = new System.Drawing.Imaging.EncoderParameter(
                                System.Drawing.Imaging.Encoder.Compression, 50L);

                            System.IO.MemoryStream msPic = new System.IO.MemoryStream();
                            bmTh.Save(msPic, iCodec, iEnc); msPic.Seek(0, System.IO.SeekOrigin.Begin);
                            bBody = new byte[msPic.Length]; msPic.Read(bBody, 0, bBody.Length);
                            bmTmp.Dispose(); bmImg.Dispose(); bmTh.Dispose(); msPic.Dispose();
                        }
                        else
                        {
                            System.IO.FileStream fs = new System.IO.FileStream(sFPath,
                                System.IO.FileMode.Open, System.IO.FileAccess.Read);
                            bBody = new byte[fs.Length]; fs.Read(bBody, 0, bBody.Length);
                            fs.Dispose();
                        }
                    }
                }
                #endregion
                #region req Import
                //Requested the import page
                else if (sReqUrl == "/import")
                {
                    if (!bSID_Valid)
                        sBody = FallbackTo(GetFromStrTbl("USER_NOT_LOGGED_IN"));

                    else if (wci[iWCI].sUser != Cred_AdminUSR)
                        sBody = FallbackTo(GetFromStrTbl("USER_LVL_TOO_LOW"));

                    else if (msData.Length == 0)
                    {
                        sBody = cb.FileRead("z_webserv/import.html");
                    }
                    else
                    {
                        byte[] bF = new byte[msData.Length];
                        msData.Read(bF, 0, bF.Length);
                        string sParm = System.Text.Encoding.
                            UTF8.GetString(bF);

                        string[] sInf = new string[7];
                        string sFName = "";
                        string sFDelm = "";
                        string sUrl = "";

                        string[] sPrm = cb.Split(sParm, sDataBound);
                        for (int a = 0; a < sPrm.Length; a++)
                        {
                            string sPName = sPrm[a];
                            if (sPName.Length > 8)
                            {
                                sPName = cb.Split(sPName, "data; name=\"")[1].Split('"')[0];
                                if (sPName == "name") sInf[0] = cb.Split(sPrm[a], "\r\n")[3];
                                if (sPName == "cmnt") sInf[1] = cb.Split(sPrm[a], "\r\n")[3];
                                if (sPName == "rate") sInf[2] = cb.Split(sPrm[a], "\r\n")[3];
                                if (sPName == "tgen") sInf[3] = cb.Split(sPrm[a], "\r\n")[3];
                                if (sPName == "tsrc") sInf[4] = cb.Split(sPrm[a], "\r\n")[3];
                                if (sPName == "tchr") sInf[5] = cb.Split(sPrm[a], "\r\n")[3];
                                if (sPName == "tart") sInf[6] = cb.Split(sPrm[a], "\r\n")[3];
                                if (sPName == "iurl") sUrl = cb.Split(sPrm[a], "\r\n")[3];
                                for (int b = 0; b < 7; b++) if (sInf[b] == null) sInf[b] = "";

                                sInf[0] = sInf[0].Trim(' ', '\r', '\n', ',');
                                sInf[1] = sInf[1].Trim(' ', '\r', '\n', ',');
                                sInf[2] = sInf[2].Trim(' ', '\r', '\n', ',');
                                sInf[3] = sInf[3].Trim(' ', '\r', '\n', ',');
                                sInf[4] = sInf[4].Trim(' ', '\r', '\n', ',');
                                sInf[5] = sInf[5].Trim(' ', '\r', '\n', ',');
                                sInf[6] = sInf[6].Trim(' ', '\r', '\n', ',');
                                if (sPName == "uimg")
                                {
                                    sFName = cb.Split(sPrm[a], "filename=\"")[1].Split('"')[0];
                                    sFDelm = cb.Split(sPrm[a], "\r\n")[2];
                                }
                            }
                        }
                        if (sUrl != "" && sUrl != "http://")
                        {
                            sBody = GetFromStrTbl("IMPORT_OTHSERV") + "<br><br>\r\n\r\n";
                            if (sUrl.EndsWith(".txt"))
                            {
                                WebReq wr = new WebReq();
                                wr.Request(sUrl);
                                while (!wr.isReady)
                                    System.Threading.Thread.Sleep(10);
                                string[] sIn = wr.sResponse.Replace("\r", "").Split('\n');

                                bool[] bInc = new bool[sIn.Length]; int iCnt = 0;
                                for (int b = 0; b < sIn.Length; b++)
                                    if (sIn[b].Contains("/")) { bInc[b] = true; iCnt++; }
                                string[] saUrl = new string[iCnt]; iCnt = 0;
                                for (int b = 0; b < sIn.Length; b++)
                                    if (bInc[b]) { saUrl[iCnt] = sIn[b]; iCnt++; }

                                DB.ImportFromURL(saUrl, sInf);
                                sBody += "Processed " + iCnt + " images.";
                                sBody = FallbackTo(sBody);
                            }
                            else
                            {
                                int iImported = DB.ImportFromURL(sUrl, sInf);
                                if (iImported == 0) sBody += GetFromStrTbl("IMPORT_OK");
                                if (iImported == 1) sBody += GetFromStrTbl("IMPORT_NODB");
                                if (iImported == 2) sBody += GetFromStrTbl("IMPORT_EXISTED");
                                if (iImported == 3) sBody += GetFromStrTbl("IMPORT_CORRUPTED");
                                sBody = FallbackTo(sBody);
                            }
                        }
                        else if (sFName != "")
                        {
                            if (sFName.EndsWith(".txt"))
                                sBody = GetFromStrTbl("IMPORT_OTHSERV") + "<br><br>\r\n\r\n";
                            else sBody = GetFromStrTbl("IMPORT_DIRECT") + "<br><br>\r\n\r\n";
                            byte[] bFDelm = System.Text.Encoding.UTF8.GetBytes(sFDelm);
                            byte[] bBound = System.Text.Encoding.UTF8.GetBytes(sDataBound);
                            int iScanLev = 0;
                            int iFOfs = -1;
                            int iFLen = -1;
                            for (int b = 0; b < bF.Length; b++)
                            {
                                if (iScanLev == 0)
                                    if (cb.bCmpBytes(bF, b, bFDelm))
                                    {
                                        iFOfs = b + bFDelm.Length + 4;
                                        iScanLev = 1;
                                    }
                                if (iScanLev == 1)
                                    if (cb.bCmpBytes(bF, b, bBound))
                                    {
                                        iFLen = b - iFOfs - 2;
                                        break;
                                    }
                            }
                            if (iFOfs == -1 || iFLen == -1)
                            {
                                sBody = FallbackTo(GetFromStrTbl("Could not identify submitted file"));
                            }
                            else
                            {
                                if (sFName.EndsWith(".txt"))
                                {
                                    msData.Dispose(); msData = new System.IO.MemoryStream(bF, iFOfs, iFLen);
                                    System.IO.StreamReader sr = new System.IO.StreamReader(msData);
                                    string[] sIn = sr.ReadToEnd().Replace("\r", "").Split('\n');

                                    bool[] bInc = new bool[sIn.Length]; int iCnt = 0;
                                    for (int b = 0; b < sIn.Length; b++)
                                        if (sIn[b].Contains("/")) { bInc[b] = true; iCnt++; }
                                    string[] saUrl = new string[iCnt]; iCnt = 0;
                                    for (int b = 0; b < sIn.Length; b++)
                                        if (bInc[b]) { saUrl[iCnt] = sIn[b]; iCnt++; }

                                    DB.ImportFromURL(saUrl, sInf);
                                    sBody += "Processed " + iCnt + " images.";
                                    sBody = FallbackTo(sBody);
                                }
                                else
                                {
                                    string sFullPath = cb.AppendNumToFN(cb.sAppPath + "z_webserv/static/upl/" + sFName);
                                    string sLocPath = sFullPath.Substring((cb.sAppPath + "z_webserv").Length);
                                    System.IO.FileStream fs = new System.IO.FileStream(sFullPath, System.IO.FileMode.Create);
                                    fs.Write(bF, iFOfs, iFLen); fs.Flush(); fs.Close(); fs.Dispose();

                                    cb.FileWrite("webserv_upload.txt", true, sIP + "\t" +
                                        sFullPath.Substring(sFullPath.LastIndexOf("/") + 1));

                                    sBody += "Download complete (" + iFLen + "B / " + (iFLen / 1024) + "kB)" + "<br>\r\n" +
                                        "File MD5: " + cb.MD5File(sFullPath) + "<br>\r\n" +
                                        "Saved to: " + "<a href=\"" + sLocPath + "\">" + sLocPath + "</a><br><br>\r\n";

                                    int iImported = DB.Import(new string[] { sFullPath }, sInf, 1, false)[0];
                                    if (iImported == 0) sBody += GetFromStrTbl("IMPORT_OK");
                                    if (iImported == 1) sBody += GetFromStrTbl("IMPORT_NODB");
                                    if (iImported == 2) sBody += GetFromStrTbl("IMPORT_EXISTED");
                                    if (iImported == 3) sBody += GetFromStrTbl("IMPORT_CORRUPTED");
                                    sBody = FallbackTo(sBody);
                                }
                            }
                        }
                        else
                        {
                            sBody = FallbackTo(GetFromStrTbl("NO_FILE_SUBMITTED"));
                        }
                    }
                }
                #endregion
                #region req Stats, Static, Error
                //Requested the stats page
                if (sReqUrl == "/stats")
                {
                    if (bSID_Valid) sBody = cb.FileRead("z_webserv/stats.html");
                    else sBody = FallbackTo(GetFromStrTbl("USER_NOT_LOGGED_IN"));
                }

                //Requested static page
                if (sReqUrl.StartsWith("/static/"))
                {
                    try
                    {
                        System.IO.FileStream fs = new System.IO.FileStream
                            ("z_webserv" + sReqUrl, System.IO.FileMode.Open,
                            System.IO.FileAccess.Read);
                        bBody = new byte[fs.Length];
                        fs.Read(bBody, 0, bBody.Length);
                        fs.Close(); fs.Dispose();
                        bStatic = true;
                    }
                    catch
                    {
                        sReqUrl = "/fallback";
                        sBody = FallbackTo(GetFromStrTbl("FILE_NOT_FOUND_EX"));
                    }
                }

                //Request couldn't be parsed
                if (sBody == "")
                {
                    sBody = FallbackTo(GetFromStrTbl("UNKNOWN_REQUEST_ROOT"));
                }
                #endregion

                #region Create response
                //Set moar variables
                string sVisitorState = "";
                if (!bSID_Has) sVisitorState = "VISITOR_STATE_NO_SESSION";
                if (bSID_Has && bSID_Valid) sVisitorState = "VISITOR_STATE_SESSION_OK";
                if (bSID_Has && !bSID_Valid) sVisitorState = "VISITOR_STATE_SESSION_NL";
                if (bSID_Valid && bSID_MadeVld) sVisitorState = "VISITOR_STATE_LOGIN_OK";
                if (!bSID_Valid && bSID_MadeInvld) sVisitorState = "VISITOR_STATE_LOGIN_NOK";
                if (bSID_Valid && bSID_MadeInvld) sVisitorState = "VISITOR_STATE_LOGOUT";
                string sUserLv = "null";
                if (wci[iWCI].sUser == "guest") sUserLv = "guest";
                if (wci[iWCI].sUser == Cred_AdminUSR) sUserLv = "admin";

                //Log the event
                Log(sIP + "  " + sUserLv.Substring(0, 1) + " < " + sReqUrl);

                //Handle binary requests
                string sReqFile = sReqUrl.Split('?')[0];
                string sReqFrmt = sReqFile.Substring(sReqFile.LastIndexOf(".") + 1);
                if (sReqFrmt.Length > 4) sReqFrmt = "";
                if (sReqFrmt == "txt") sReqMime = "text/plain";
                if (sReqFrmt == "rtf") sReqMime = "text/richtext";
                if (sReqFrmt == "css") sReqMime = "text/css";
                if (sReqFrmt == "js") sReqMime = "application/x-javascript";
                if (sReqFrmt == "exe") sReqMime = "application/octet-stream";
                if (sReqFrmt == "zip") sReqMime = "application/zip";
                if (sReqFrmt == "gif") sReqMime = "image/gif";
                if (sReqFrmt == "jpg") sReqMime = "image/jpeg";
                if (sReqFrmt == "png") sReqMime = "image/png";
                if (sReqFrmt == "bmp") sReqMime = "image/bmp";

                if (!bStatic)
                {
                    sBody = "<!-- Powered by pImgDB v" + cb.sAppVer + ". Visit praetox.com for more information -->" + "\r\n" + sBody;
                    sBody = sBody.Replace("[[VISITOR_STATE]]", GetFromStrTbl(sVisitorState));
                    for (int a = 0; a < StrTableID.Length; a++)
                    {
                        if (sBody.Contains(StrTableID[a]))
                            sBody = sBody.Replace(StrTableID[a], StrTableVL[a]);
                    }
                    string sTckUsed = TimeFromTick(wci[iWCI].lUsed);
                    string sTckLogin = TimeFromTick(wci[iWCI].lLogin);
                    if (sTckUsed == "") sTckUsed = "Just now";
                    if (sTckLogin == "") sTckLogin = "Just now";

                    string sPageCur = wci[iWCI].iCurPage + "";
                    string sPageTot = wci[iWCI].iPages + "";
                    string sPagePrev = (wci[iWCI].iCurPage - 1) + "";
                    string sPageNext = (wci[iWCI].iCurPage + 1) + "";
                    if (wci[iWCI].iCurPage == 1) sPagePrev = "#";
                    if (wci[iWCI].iCurPage == wci[iWCI].iPages) sPageNext = "#";

                    sBody = sBody.Replace("[[APPVER]]", cb.sAppVer)
                        .Replace("[[USER_LV]]", sUserLv)
                        .Replace("[[USER_NAME]]", wci[iWCI].sUser)
                        .Replace("[[USER_PASS]]", wci[iWCI].sPass)
                        .Replace("[[GTIME_USED]]", sTckUsed)
                        .Replace("[[GTIME_LOGIN]]", sTckLogin)
                        .Replace("[[PAGE_CUR]]", sPageCur)
                        .Replace("[[PAGE_PREV]]", sPagePrev)
                        .Replace("[[PAGE_NEXT]]", sPageNext)
                        .Replace("[[PAGE_TOT]]", sPageTot);
                }
                #endregion
                #region Send response
                if (bBody == null) bBody = System.Text.Encoding.UTF8.GetBytes(sBody);

                string sHdr =
                    "HTTP/1.1 200 OK" + "\r\n" +
                    "Server: pImgDB/" + cb.sAppVer + "\r\n" +
                    "Date: " + DateTime.Now.ToUniversalTime().ToString
                        ("R", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "\r\n";

                if (!bStatic && sReqMime == sDefMime) sHdr +=
                    "Cache-control: no-store, no-cache" + "\r\n" +
                    "Pragma: no-cache" + "\r\n";

                if (sSetLoc != "") sHdr +=
                    "Refresh: 1; url=" + sSetLoc + "\r\n";

                if (!bSID_Has && !bStatic) sHdr +=
                    "Set-Cookie: SID=" + sSid + "\r\n";
                if (bSID_Valid && bSID_MadeInvld) sHdr +=
                    "Set-Cookie: SID=DEADFEED; expires=" +
                    "Monday, 01-Jan-90 00:00:00 GMT" + "\r\n";

                sHdr +=
                    "Content-Type: " + sReqMime + "\r\n" +
                    "Content-Length: " + bBody.Length + "\r\n";

                //Refresh access timers
                wci[iWCI].lUsed = lGSec;

                byte[] bHdr = System.Text.Encoding.UTF8.GetBytes(sHdr + "\r\n");
                byte[] bPck = new byte[bHdr.Length + bBody.Length];
                bHdr.CopyTo(bPck, 0); bBody.CopyTo(bPck, bHdr.Length);
                try { sck.Send(bPck); }
                catch { }

                msData.Dispose(); break;
                #endregion
            }
            sck.Disconnect(false);
            sck.Close();
            iActive--;
        }
        private static string FallbackTo(string sContent)
        {
            return cb.FileRead("z_webserv/fallback.html")
                .Replace("[[CONTENT]]", sContent);
        }
    }
    public class WebCliInf
    {
        public string sSID = "null";
        public string sUser = "null";
        public string sPass = "null";
        public int iUserLev = -1;
        public SearchPrm sp = new SearchPrm();
        public ImageData[] idImages = new ImageData[0];
        public int iCurPage = 1;
        public int iCurImg = 0;
        public int iPages = -1;
        public long lLogin = -1;
        public long lUsed = -1;
    }
    public class DB
    {
        #region Pointers
        public const int diHash = 0;
        public const int diThmb = 1;
        public const int diType = 2;
        public const int diFLen = 3;
        public const int diXRes = 4;
        public const int diYRes = 5;
        public const int diName = 6;
        public const int diPath = 7;
        public const int diFlag = 8;
        public const int diCmnt = 9;
        public const int diRate = 10;
        public const int diTGen = 11;
        public const int diTSrc = 12;
        public const int diTChr = 13;
        public const int diTArt = 14;

        public const int flFBind = 0; //0-None, 1=Yes
        public const int flEmTag = 1; //0-None, 1-Sync, 2-Nosync
        #endregion
        public static SQLiteConnection Data;
        public static string Name = "";
        public static string Path = "";
        public static bool CanClose = true;
        public static bool IsOpen = false;

        public static Bitmap bmThumb = null;
        public static int iThumbIndex = 0;
        public static int iThumbBusy = 0;
        public static string SearchProg = "";
        public static string ImportProg = "";
        public static string[] saAllowedTypes =
            new string[] { ".jpg", ".png", ".gif", ".bmp" };

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
                        "'hash' char(32), " +
                        "'thmb' char(192), " +
                        "'type' varchar(4), " +
                        "'flen' unsigned int, " +
                        "'xres' int, " +
                        "'yres' int, " +
                        "'name' text, " +
                        "'path' varchar(192), " +
                        "'flag' char(32), " +
                        "'cmnt' text, " +
                        "'rate' int, " +
                        "'tgen' text, " +
                        "'tsrc' text, " +
                        "'tchr' text, " +
                        "'tart' text, " +
                        "PRIMARY KEY ('hash'))";
                    DBc.ExecuteNonQuery();
                }
                Program.Reg_Access("Last DB name", sDBName);
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
            cb.sAppPath = Application.StartupPath.Replace("\\", "/");
            if (!cb.sAppPath.EndsWith("/")) cb.sAppPath += "/";
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
                Program.Reg_Access("Last DB name", sDBName);
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
        public static ImageData Contains(string sHash, bool bGetInfo)
        {
            ImageData ret = new ImageData();
            if (!IsOpen) return ret;
            using (SQLiteCommand DBc = Data.CreateCommand())
            {
                DBc.CommandText = "SELECT * FROM 'images' WHERE hash=?";
                SQLiteParameter DBcP = DBc.CreateParameter();
                DBc.Parameters.Add(DBcP);
                DBcP.Value = sHash;
                using (SQLiteDataReader rd = DBc.ExecuteReader())
                {
                    if (!rd.Read()) return ret;
                    if (bGetInfo)
                    {
                        ret.sHash = rd.GetString(DB.diHash);
                        ret.sThmb = rd.GetString(DB.diThmb);
                        ret.sType = rd.GetString(DB.diType);
                        ret.iLen = rd.GetInt32(DB.diFLen);
                        ret.ptRes = new Point(
                            rd.GetInt32(DB.diXRes),
                            rd.GetInt32(DB.diYRes));
                        ret.sName = rd.GetString(DB.diName);
                        ret.sPath = rd.GetString(DB.diPath);
                        ret.cFlag = rd.GetString(DB.diFlag).ToCharArray(0, 32);
                        ret.sCmnt = rd.GetString(DB.diCmnt);
                        ret.iRate = rd.GetInt32(DB.diRate);
                        ret.sTGen = rd.GetString(DB.diTGen);
                        ret.sTSrc = rd.GetString(DB.diTSrc);
                        ret.sTChr = rd.GetString(DB.diTChr);
                        ret.sTArt = rd.GetString(DB.diTArt);
                        return ret;
                    }
                    else
                    {
                        ret.sHash = sHash; return ret;
                    }
                }
            }
        }
        public static int[] Import(string[] sPath, string[] sInfo, int iemTags, bool bImportIsLocal)
        {
            //ret:  0-imported  1-closed  2-exists  3-corrupt  4-404
            //
            //sInfo: sImageName, sImageComment, sImageRating,
            //sTagsGeneral, sTagsSource, sTagsChars, sTagsArtists
            //
            //iemTag: 0-ignore  1-before  2-after  3-repOne  4-repAll

            int[] ret = cb.asIntArray(0, sPath.Length);
            if (!IsOpen) return cb.asIntArray(1, sPath.Length);
            using (SQLiteTransaction dbTrs = DB.Data.BeginTransaction())
            {
                using (SQLiteCommand DBc = DB.Data.CreateCommand())
                {
                    DBc.CommandText = "INSERT INTO 'images' (" +
                        "hash, thmb, type, flen, xres, yres, name, path, flag, " +
                        "cmnt, rate, tgen, tsrc, tchr, tart) " +
                        "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    SQLiteParameter[] dbParam = new SQLiteParameter[15];
                    for (int a = 0; a < dbParam.Length; a++)
                    {
                        dbParam[a] = DBc.CreateParameter();
                        DBc.Parameters.Add(dbParam[a]);
                    }
                    for (int a = 0; a < sPath.Length; a++)
                    {
                        string[] sMyInfo = (string[])sInfo.Clone();
                        sPath[a] = sPath[a].Replace("\\", "/");

                        //  Read embedded tags
                        string[] semTags = new string[0];
                        if (iemTags != 0) semTags = emTags.TagsRead(sPath[a]);

                        //  Determine full replace
                        bool bemTagsOverride = false;
                        if (iemTags == 4)
                            for (int b = 0; b < semTags.Length; b++)
                                if (semTags[b] != "") bemTagsOverride = true;

                        //  Do information replacing
                        for (int b = 0; b < semTags.Length; b++)
                        {
                            int iAdd = 0; if (b > 1) iAdd++; //Array skew (rating)
                            if (semTags[b] != "")
                            {
                                if (b < 2) //Don't decimate non-tags (name or comment)
                                {
                                    if (iemTags == 1) sMyInfo[b] = semTags[b]; //Override (before)
                                }
                                else //We hit a tag-based bit of information
                                {
                                    if (iemTags == 1) sMyInfo[b + iAdd] = semTags[b] + ", " + sMyInfo[b + iAdd]; //Before
                                    if (iemTags == 2) sMyInfo[b + iAdd] = sMyInfo[b + iAdd] + ", " + semTags[b]; //After
                                }
                                if (iemTags == 3) sMyInfo[b + iAdd] = semTags[b]; //Substitute (replace-one)
                            }
                            if (bemTagsOverride) sMyInfo[b + iAdd] = semTags[b]; //Override (replace-all)
                        }

                        ImageData tID = DB.GenerateImageData(sPath[a], sMyInfo, bImportIsLocal, true);
                        if (semTags.Length != 0) tID.cFlag[DB.flEmTag] = '2'; //no-sync
                        ImportProg = (a + 1) + " of " + sPath.Length + " - " + sPath[a];

                        if (tID.sType == "nil") ret[a] = 3;
                        //if (Contains(tID.sHash, false).sHash!="") ret[a] = 2;
                        if (ret[a] == 0)
                        {
                            if (!bImportIsLocal)
                            {
                                string sNFPath = cb.sAppPath + DB.Path + tID.sPath;
                                if (System.IO.File.Exists(sNFPath)) ret[a] = 2;
                                else System.IO.File.Copy(sPath[a], sNFPath);
                            }
                            dbParam[DB.diHash].Value = tID.sHash;
                            dbParam[DB.diThmb].Value = tID.sThmb;
                            dbParam[DB.diType].Value = tID.sType;
                            dbParam[DB.diFLen].Value = tID.iLen;
                            dbParam[DB.diXRes].Value = tID.ptRes.X;
                            dbParam[DB.diYRes].Value = tID.ptRes.Y;
                            dbParam[DB.diName].Value = tID.sName;
                            dbParam[DB.diPath].Value = tID.sPath;
                            dbParam[DB.diFlag].Value = new string(tID.cFlag);
                            dbParam[DB.diCmnt].Value = tID.sCmnt;
                            dbParam[DB.diRate].Value = tID.iRate;
                            dbParam[DB.diTGen].Value = tID.sTGen;
                            dbParam[DB.diTSrc].Value = tID.sTSrc;
                            dbParam[DB.diTChr].Value = tID.sTChr;
                            dbParam[DB.diTArt].Value = tID.sTArt;
                            try
                            {
                                DBc.ExecuteNonQuery();
                            }
                            catch { }
                        }
                    }
                }
                dbTrs.Commit();
            }
            return ret;
        }
        public static int ImportFromURL(string sUrl, string[] sInfo)
        {
            WebReq wr = new WebReq();
            wr.Request(sUrl);
            while (!wr.isReady)
                System.Threading.Thread.Sleep(10);

            if (wr.sResponseCode == "")
            {
                string sFName = sUrl.Substring(sUrl.LastIndexOf("/") + 1);
                if (sFName.Contains("?"))
                {
                    sFName = sFName.Split('?')[0].Replace('.', '-');
                    string[] SFNe = sFName.Split('?')[1].Split('&');
                    for (int a = 0; a < SFNe.Length; a++)
                        sFName += " - " + SFNe[a].Split('=')[1];

                    if (wr.Headers["Content-Type"] == "image/jpeg") sFName += ".jpg";
                    if (wr.Headers["Content-Type"] == "image/png") sFName += ".png";
                    if (wr.Headers["Content-Type"] == "image/gif") sFName += ".gif";
                    if (wr.Headers["Content-Type"] == "image/bmp") sFName += ".bmp";
                }

                string sFullPath = cb.AppendNumToFN(cb.sAppPath + "z_webserv/static/upl/" + sFName);
                string sLocPath = sFullPath.Substring((cb.sAppPath + "z_webserv").Length);
                System.IO.FileStream fs = new System.IO.FileStream(sFullPath, System.IO.FileMode.Create);
                wr.msResponse.WriteTo(fs); fs.Flush(); fs.Close(); fs.Dispose(); wr.Dispose();

                return Import(new string[] { sFullPath }, sInfo, 1, false)[0];
            }
            else { wr.Dispose(); return 4; }
        }
        public static int[] ImportFromURL(string[] sUrl, string[] sInfo)
        {
            int[] ret = new int[sUrl.Length];
            for (int a = 0; a < sUrl.Length; a++)
                ret[a] = ImportFromURL(sUrl[a], sInfo);
            return ret;
        }

        public static ImageData GenerateImageData(string sPath,
            string[] sInfo, bool bImportIsLocal, bool bCheckFBind)
        {
            Bitmap bmTmp = null;
            string sName = sInfo[0];
            string sComment = sInfo[1];
            string sRating = sInfo[2];
            string sTGeneral = sInfo[3];
            string sTSource = sInfo[4];
            string sTChars = sInfo[5];
            string sTArtists = sInfo[6];

            sName = sName.Trim(' ');
            sComment = sComment.Trim(' ');
            sRating = sRating.Trim(' ');
            sTGeneral = sTGeneral.Trim(' ', ',');
            sTSource = sTSource.Trim(' ', ',');
            sTChars = sTChars.Trim(' ', ',');
            sTArtists = sTArtists.Trim(' ', ',');
            /*string[] saTGeneral = sTGeneral.Split(','); sTGeneral = "";
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
            sTArtists = sTArtists.Trim(',');*/

            ImageData ret = new ImageData();
            string[] saE = sPath.Split('/');
            string sFName = "";
            sFName = saE[saE.Length - 1];
            sFName = sFName.Substring(0, sFName.LastIndexOf("."));

            ret.bSel = false;
            ret.sHash = "";
            //ret.sType = saE[saE.Length - 1].Substring(sFName.Length + 1).ToLower();
            ret.ptRes = new Point(0, 0);
            ret.iLen = -1;

            try
            {
                int iThP = 8; //8*8*3 = 192b
                if (bmTmp != null) bmTmp.Dispose();
                bmTmp = (Bitmap)Bitmap.FromFile(sPath);

                int iSW = bmTmp.Width; int iSH = bmTmp.Height;
                byte[] bST = cb.qPicR(bmTmp);
                ret.sHash = cb.MD5(bST);

                if (iThumbBusy != 2)
                {
                    iThumbBusy = 1;
                    if (bmThumb != null) bmThumb.Dispose();
                    bmThumb = (Bitmap)bmTmp.Clone();
                    iThumbBusy = 0; iThumbIndex++;
                }

                /*Bitmap bmSrc = new Bitmap(iSW, iSH);
                using (Graphics gOut = Graphics.FromImage((Image)bmSrc))
                {
                    //ITT: CRUDE HAX
                    gOut.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    gOut.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    gOut.DrawImage(bmTmp, 0, 0, iSW, iSH);
                }
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                bmSrc.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] bST = ms.GetBuffer(); ms.Close(); ms.Dispose();
                ret.sHash = MD5(bST);*/

                /*byte[, ,] bSC = new byte[iSW, iSH, 3];
                int ibSTLoc = 54;
                for (int y = 0; y < iSH; y++)
                    for (int x = 0; x < iSW; x++)
                    {
                        if (bST[ibSTLoc + 2] == 0) bST[ibSTLoc + 2] = 1; //char 0
                        if (bST[ibSTLoc + 1] == 0) bST[ibSTLoc + 1] = 1; //should
                        if (bST[ibSTLoc + 0] == 0) bST[ibSTLoc + 0] = 1; //an hero
                        bSC[x, iSH - y - 1, 0] = bST[ibSTLoc + 2];
                        bSC[x, iSH - y - 1, 1] = bST[ibSTLoc + 1];
                        bSC[x, iSH - y - 1, 2] = bST[ibSTLoc + 0];
                        ibSTLoc += 4;
                    }*/

                int iPnC = 0;
                int iPnW = (int)Math.Floor((double)iSW / (double)iThP);
                int iPnH = (int)Math.Floor((double)iSH / (double)iThP);
                int iPnWs = (int)Math.Floor((double)(iSW - (iThP * iPnW)) / 2);
                int iPnHs = (int)Math.Floor((double)(iSH - (iThP * iPnH)) / 2);
                int[,] iaPnCols = new int[(iThP * iThP), 3];
                for (int pnY = 0; pnY < iThP; pnY++)
                    for (int pnX = 0; pnX < iThP; pnX++)
                    {
                        for (int y = pnY * iPnH; y < (pnY + 1) * iPnH; y++)
                            for (int x = pnX * iPnW; x < (pnX + 1) * iPnW; x++)
                            {
                                /*iaPnCols[iPnC, 0] += bSC[x + iPnWs, y + iPnHs, 0];
                                iaPnCols[iPnC, 1] += bSC[x + iPnWs, y + iPnHs, 1];
                                iaPnCols[iPnC, 2] += bSC[x + iPnWs, y + iPnHs, 2];*/
                                //int iSL = (((iSH - (y + iPnHs)) * iSW - iSW + (x + iPnWs)) * 4) + 54;
                                int iSL = ((((y + iPnHs) * iSW) + (x + iPnWs)) * 4) + 8;
                                iaPnCols[iPnC, 0] += bST[iSL + 1];
                                iaPnCols[iPnC, 1] += bST[iSL + 2];
                                iaPnCols[iPnC, 2] += bST[iSL + 3];
                            }
                        iaPnCols[iPnC, 0] /= iPnW * iPnH;
                        iaPnCols[iPnC, 1] /= iPnW * iPnH;
                        iaPnCols[iPnC, 2] /= iPnW * iPnH;
                        iPnC++;
                    }

                /*iPnC = 0;
                Bitmap bTest = new Bitmap(iThP, iThP);
                for (int y = 0; y < iThP; y++)
                    for (int x = 0; x < iThP; x++)
                    {
                        bTest.SetPixel(x, y, Color.FromArgb(
                            iaPnCols[iPnC, 0],
                            iaPnCols[iPnC, 1],
                            iaPnCols[iPnC, 2]));
                        iPnC++;
                    }
                Clipboard.Clear(); Clipboard.SetImage(bTest);
                Application.DoEvents(); MessageBox.Show("wat");*/

                char[] bTC = new char[iThP * iThP * 3];
                for (int a = 0; a < iThP * iThP; a++)
                {
                    bTC[(a * 3) + 0] = (char)iaPnCols[a, 0];
                    bTC[(a * 3) + 1] = (char)iaPnCols[a, 1];
                    bTC[(a * 3) + 2] = (char)iaPnCols[a, 2];
                }
                for (int a = 0; a < bTC.Length; a++)
                {
                    if (bTC[a] == '\0') bTC[a] = (char)((int)'\0' + 1); //csharp string terminate
                    if (bTC[a] == '\'') bTC[a] = (char)((int)'\'' + 1); //sqlite string terminate
                }
                ret.sThmb = new string(bTC);

                // Test to see whether image is chopped properly
                /*iPnC = 0;
                Bitmap bTCBT = (Bitmap)bmSrc.Clone();
                for (int pnY = 0; pnY < iThP; pnY++)
                    for (int pnX = 0; pnX < iThP; pnX++)
                    {
                        for (int y = pnY * iPnH; y < (pnY + 1) * iPnH; y++)
                            for (int x = pnX * iPnW; x < (pnX + 1) * iPnW; x++)
                            {
                                bTCBT.SetPixel(x + iPnWs, y + iPnHs, Color.FromArgb
                                    (iaPnCols[iPnC, 0], iaPnCols[iPnC, 1], iaPnCols[iPnC, 2]));
                            }
                        iPnC++;
                    }
                Clipboard.Clear(); Clipboard.SetImage(bTCBT as Image);*/

                /*Bitmap bTCB = new Bitmap(iThP, iThP);
                for (int y = 0; y < iThP; y++)
                    for (int x = 0; x < iThP; x++)
                    {
                        int i = (y * iThP) + x;
                        bTCB.SetPixel(x, y, Color.FromArgb
                            (iaPnCols[i, 0], iaPnCols[i, 1], iaPnCols[i, 2]));
                    }

                dbg.Stop(); dbgt += dbg.Ret;
                Clipboard.Clear(); Clipboard.SetImage(bTCB as Image);*/

                System.IO.FileStream fs = new System.IO.FileStream(
                    sPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] bI = new byte[fs.Length];
                fs.Read(bI, 0, (int)fs.Length);
                fs.Close(); fs.Dispose();

                if (bCheckFBind)
                {
                    if (pFBind.isFBind(bI))
                        ret.cFlag[DB.flFBind] = '1';
                }
                else ret.cFlag[DB.flFBind] = '?';
                ret.ptRes = (Point)bmTmp.Size;
                ret.iLen = bI.Length;

                int iFmtID = emTags.GetType(bI);
                if (iFmtID == -1) ret.sType = "bmp";
                else ret.sType = emTags.sFmtH[iFmtID];

                // OLD CODE
                /*Bitmap bmThmb = ResizeBitmap(bmTmp, iThP, iThP, false, 1);
                Bitmap bmPrev = (Bitmap)bmTmp.Clone(); bmTmp.Dispose();
                Import_pbPreview.Image = bmPrev; Application.DoEvents();
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                bmThmb.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //bmThmb.Save("c:\\thumb_dual.bmp");
                byte[] bT = ms.GetBuffer(); ms.Close(); ms.Dispose();
                char[] bTC = new char[iThP * iThP * 3]; int ibTLoc = 54;
                for (int y = 0; y < iThP; y++)
                    for (int x = 0; x < iThP; x++)
                    {
                        int iTL = ((iThP - y) * iThP - iThP + (x)) * 3;
                        bTC[iTL + 0] = (char)bT[ibTLoc + 2];
                        bTC[iTL + 1] = (char)bT[ibTLoc + 1];
                        bTC[iTL + 2] = (char)bT[ibTLoc + 0];
                        if (bTC[iTL + 0] == 0) bTC[iTL + 0] = (char)1;
                        if (bTC[iTL + 1] == 0) bTC[iTL + 1] = (char)1;
                        if (bTC[iTL + 2] == 0) bTC[iTL + 2] = (char)1;
                        //int iVal = (int)Math.Round(
                        //    (double)bT[ibTLoc + 2] * 0.30 + //lolreverse
                        //    (double)bT[ibTLoc + 1] * 0.59 + //lolreverse
                        //    (double)bT[ibTLoc + 0] * 0.11); //lolreverse
                        //if (iVal == 0) iVal = 1; //Char zero is a bad bitch
                        //bTC[(iThP - y) * iThP - iThP + (x)] = //lolreverse
                        //    (char)iVal;
                        ibTLoc += 4;
                    }
                ret.sThumb = new string(bTC);
                /*Bitmap bTCB = new Bitmap(iThP, iThP);
                int iThCol = -3;
                for (int y = 0; y < iThP; y++)
                    for (int x = 0; x < iThP; x++)
                    {
                        iThCol+=3;
                        bTCB.SetPixel(x, y, Color.FromArgb
                            //(bTC[(y * iThP) + x], bTC[(y * iThP) + x], bTC[(y * iThP) + x]));
                            (bTC[iThCol+0], bTC[iThCol+1], bTC[iThCol+2]));
                    }
                bTCB.Save("c:\\thumb_mono1.bmp");
                /.*string wut = new string(cTC);
                byte[] bTCC = new byte[iThP * iThP];
                for (int a = 0; a < bTCC.Length; a++)
                    bTCC[a] = (byte)wut[a];
                Bitmap bTCBB = new Bitmap(iThP, iThP);
                for (int y = 0; y < iThP; y++)
                    for (int x = 0; x < iThP; x++)
                        bTCBB.SetPixel(x, y, Color.FromArgb
                            (bTCC[(y * iThP) + x], bTCC[(y * iThP) + x], bTCC[(y * iThP) + x]));
                bTCBB.Save("c:\\thumb_mono2.bmp");
                Import_pbPreview.Image = bTCB; Application.DoEvents(); * /
                bmThmb.Dispose();

                System.IO.FileStream fs = new System.IO.FileStream(
                    sPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] bI = new byte[fs.Length];
                fs.Read(bI, 0, (int)fs.Length);
                fs.Close(); fs.Dispose();
                ret.iLength = bI.Length;
                //int iX = bI[17] + bI[18] * 256 + bI[19] * 256 * 256 + bI[20] * 256 * 256 * 256;
                //int iY = bI[21] + bI[22] * 256 + bI[23] * 256 * 256 + bI[24] * 256 * 256 * 256;
                ret.ptRes = (Point)Import_pbPreview.Image.Size;
                ret.sHash = MD5(bI);
                //MessageBox.Show(iX + "x" + iY); */
            }
            catch
            {
                ret.sType = "nil";
                ret.ptRes = new Point(0, 0); //(Point)Import_pbPreview.Image.Size;
            }

            if (bImportIsLocal) ret.sPath = sPath.Substring(cb.sAppPath.Length + DB.Path.Length);
            if (!bImportIsLocal) ret.sPath = ret.sHash + "." + ret.sType;

            //Image img = Bitmap.FromFile(sPath);
            //ret.ptRes = (Point)img.Size;
            int iLocal = 0; if (bImportIsLocal)
                iLocal = (cb.sAppPath + DB.Path).Split('/').Length - 2;
            for (int a = 1; a < saE.Length - iLocal; a++)
            {
                sName = sName.Replace("{" + a + "}", saE[a + iLocal]);
                sComment = sComment.Replace("{" + a + "}", saE[a + iLocal]);
                sRating = sRating.Replace("{" + a + "}", saE[a + iLocal]);
                sTGeneral = sTGeneral.Replace("{" + a + "}", saE[a + iLocal]);
                sTSource = sTSource.Replace("{" + a + "}", saE[a + iLocal]);
                sTArtists = sTArtists.Replace("{" + a + "}", saE[a + iLocal]);
                sTChars = sTChars.Replace("{" + a + "}", saE[a + iLocal]);
            }
            sName = sName.Replace("{fname}", sFName);
            sComment = sComment.Replace("{fname}", sFName);
            sRating = sRating.Replace("{fname}", sFName);
            sTGeneral = sTGeneral.Replace("{fname}", sFName);
            sTSource = sTSource.Replace("{fname}", sFName);
            sTArtists = sTArtists.Replace("{fname}", sFName);
            sTChars = sTChars.Replace("{fname}", sFName);

            int iRating = 0;
            if (!Int32.TryParse(sRating,
                out iRating)) iRating = -1;
            ret.sName = sName;
            ret.sCmnt = sComment;
            ret.iRate = iRating;
            ret.sTGen = cb.RemoveDupes(sTGeneral);
            ret.sTSrc = cb.RemoveDupes(sTSource);
            ret.sTChr = cb.RemoveDupes(sTChars);
            ret.sTArt = cb.RemoveDupes(sTArtists);
            return ret;
        }
        public static ImageData[] Search(SearchPrm sp, int iFallback)
        {
            return Search(sp, iFallback, new Point(0, -1));
        }
        public static ImageData[] Search(SearchPrm sp, int iFallback, Point ptRange)
        {
            SearchProg = "Initiating...";

            if (sp.bDefRet &&
                "" == sp.sImagFormat &&
                "" == sp.sImagName &&
                "" == sp.sImagPath &&
                "" == sp.sTagsArtists &&
                "" == sp.sTagsChars &&
                "" == sp.sTagsGeneral &&
                "" == sp.sTagsSource)
            {
                string sFormats = "";
                foreach (string sFormat in saAllowedTypes)
                    sFormats += "+" + sFormat.Substring(1) + ",";
                sFormats = sFormats.Substring(0, sFormats.Length - 1);
                sp.sImagFormat = sFormats;
            }

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

            bool bGlobalSearch = false;
            if (sp.sTagsGeneral != "" &&
                sp.sTagsGeneral == sp.sImagName &&
                sp.sTagsGeneral == sp.sTagsSource &&
                sp.sTagsGeneral == sp.sTagsChars &&
                sp.sTagsGeneral == sp.sTagsArtists)
                bGlobalSearch = true;

            char[] cFallback = new char[] { ' ', '*', '+', '-' };
            string[] imagName = sp.sImagName.Split(',');
            string[] imagFormat = sp.sImagFormat.Split(',');
            string[] imagRes = sp.sImagRes.Split(',');
            string[] imagRating = sp.sImagRating.Split(',');
            string[] tagsGeneral = sp.sTagsGeneral.Split(',');
            string[] tagsSource = sp.sTagsSource.Split(',');
            string[] tagsChars = sp.sTagsChars.Split(',');
            string[] tagsArtists = sp.sTagsArtists.Split(',');
            char[] cFlDeny = sp.cFlagDeny;
            char[] cFlRet = sp.cFlagRet;
            for (int a = 0; a < 1024; a++)
            {
                if (a < imagName.Length) imagName[a] = imagName[a].Trim(' ');
                if (a < imagFormat.Length) imagFormat[a] = imagFormat[a].Trim(' ');
                if (a < imagRes.Length) imagRes[a] = imagRes[a].Trim(' ');
                if (a < imagRating.Length) imagRating[a] = imagRating[a].Trim(' ');
                if (a < tagsGeneral.Length) tagsGeneral[a] = tagsGeneral[a].Trim(' ');
                if (a < tagsSource.Length) tagsSource[a] = tagsSource[a].Trim(' ');
                if (a < tagsChars.Length) tagsChars[a] = tagsChars[a].Trim(' ');
                if (a < tagsArtists.Length) tagsArtists[a] = tagsArtists[a].Trim(' ');
            }

            // Image Name
            int[] imagNameP = new int[imagName.Length];
            string[] imagNameV = new string[imagName.Length];
            if (imagName[0] != "")
                for (int a = 0; a < imagName.Length; a++)
                {
                    if (!imagName[a].StartsWith("*") &&
                        !imagName[a].StartsWith("+") &&
                        !imagName[a].StartsWith("-") &&
                        imagName[a] != "")
                        imagName[a] = cFallback[iFallback] + imagName[a];
                    if (imagName[a].StartsWith("*")) imagNameP[a] = 1;
                    if (imagName[a].StartsWith("+")) imagNameP[a] = 2;
                    if (imagName[a].StartsWith("-")) imagNameP[a] = 3;
                    imagNameV[a] = imagName[a].Substring(1).ToLower();
                }

            // Image Format
            int[] imagFormatP = new int[imagFormat.Length];
            string[] imagFormatV = new string[imagFormat.Length];
            if (imagFormat[0] != "")
                for (int a = 0; a < imagFormat.Length; a++)
                {
                    if (!imagFormat[a].StartsWith("*") &&
                        !imagFormat[a].StartsWith("+") &&
                        !imagFormat[a].StartsWith("-") &&
                        imagFormat[a] != "")
                        imagFormat[a] = cFallback[iFallback] + imagFormat[a];
                    if (imagFormat[a].StartsWith("*")) imagFormatP[a] = 1;
                    if (imagFormat[a].StartsWith("+")) imagFormatP[a] = 2;
                    if (imagFormat[a].StartsWith("-")) imagFormatP[a] = 3;
                    imagFormatV[a] = imagFormat[a].Substring(1).ToLower();
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
                    if (!tagsGeneral[a].StartsWith("*") &&
                        !tagsGeneral[a].StartsWith("+") &&
                        !tagsGeneral[a].StartsWith("-") &&
                        tagsGeneral[a] != "")
                        tagsGeneral[a] = cFallback[iFallback] + tagsGeneral[a];
                    if (tagsGeneral[a].StartsWith("*")) tagsGeneralP[a] = 1;
                    if (tagsGeneral[a].StartsWith("+")) tagsGeneralP[a] = 2;
                    if (tagsGeneral[a].StartsWith("-")) tagsGeneralP[a] = 3;
                    tagsGeneralV[a] = tagsGeneral[a].Substring(1).ToLower();
                }

            // Tags - Source
            int[] tagsSourceP = new int[tagsSource.Length];
            string[] tagsSourceV = new string[tagsSource.Length];
            if (tagsSource[0] != "")
                for (int a = 0; a < tagsSource.Length; a++)
                {
                    if (!tagsSource[a].StartsWith("*") &&
                        !tagsSource[a].StartsWith("+") &&
                        !tagsSource[a].StartsWith("-") &&
                        tagsSource[a] != "")
                        tagsSource[a] = cFallback[iFallback] + tagsSource[a];
                    if (tagsSource[a].StartsWith("*")) tagsSourceP[a] = 1;
                    if (tagsSource[a].StartsWith("+")) tagsSourceP[a] = 2;
                    if (tagsSource[a].StartsWith("-")) tagsSourceP[a] = 3;
                    tagsSourceV[a] = tagsSource[a].Substring(1).ToLower();
                }

            // Tags - Chars
            int[] tagsCharsP = new int[tagsChars.Length];
            string[] tagsCharsV = new string[tagsChars.Length];
            if (tagsChars[0] != "")
                for (int a = 0; a < tagsChars.Length; a++)
                {
                    if (!tagsChars[a].StartsWith("*") &&
                        !tagsChars[a].StartsWith("+") &&
                        !tagsChars[a].StartsWith("-") &&
                        tagsChars[a] != "")
                        tagsChars[a] = cFallback[iFallback] + tagsChars[a];
                    if (tagsChars[a].StartsWith("*")) tagsCharsP[a] = 1;
                    if (tagsChars[a].StartsWith("+")) tagsCharsP[a] = 2;
                    if (tagsChars[a].StartsWith("-")) tagsCharsP[a] = 3;
                    tagsCharsV[a] = tagsChars[a].Substring(1).ToLower();
                }

            // Tags - Artists
            int[] tagsArtistsP = new int[tagsArtists.Length];
            string[] tagsArtistsV = new string[tagsArtists.Length];
            if (tagsArtists[0] != "")
                for (int a = 0; a < tagsArtists.Length; a++)
                {
                    if (!tagsArtists[a].StartsWith("*") &&
                        !tagsArtists[a].StartsWith("+") &&
                        !tagsArtists[a].StartsWith("-") &&
                        tagsArtists[a] != "")
                        tagsArtists[a] = cFallback[iFallback] + tagsArtists[a];
                    if (tagsArtists[a].StartsWith("*")) tagsArtistsP[a] = 1;
                    if (tagsArtists[a].StartsWith("+")) tagsArtistsP[a] = 2;
                    if (tagsArtists[a].StartsWith("-")) tagsArtistsP[a] = 3;
                    tagsArtistsV[a] = tagsArtists[a].Substring(1).ToLower();
                }

            SearchProg = "Search started";
            int iCnt = DB.Entries(); int i = -1;
            int[] iRet = new int[iCnt];
            for (int a = 0; a < iRet.Length; a++) iRet[a] = 0;
            using (SQLiteCommand DBc = DB.Data.CreateCommand())
            {
                DBc.CommandText = "SELECT * FROM 'images'";
                using (SQLiteDataReader rd = DBc.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        i++;
                        string sHash = rd.GetString(DB.diHash);
                        string sThmb = rd.GetString(DB.diThmb);
                        string sType = rd.GetString(DB.diType);
                        int iLen = rd.GetInt32(DB.diFLen);
                        int iResX = rd.GetInt32(DB.diXRes);
                        int iResY = rd.GetInt32(DB.diYRes);
                        string sName = rd.GetString(DB.diName);
                        string sPath = rd.GetString(DB.diPath);
                        char[] cFlag = rd.GetString(DB.diFlag).ToCharArray(0, 32);
                        string sCmnt = rd.GetString(DB.diCmnt);
                        int iRate = rd.GetInt32(DB.diRate);
                        string[] sTGen = rd.GetString(DB.diTGen).Split(',');
                        string[] sTSrc = rd.GetString(DB.diTSrc).Split(',');
                        string[] sTChr = rd.GetString(DB.diTChr).Split(',');
                        string[] sTArt = rd.GetString(DB.diTArt).Split(',');

                        if (sName == "") sName = "tagme";
                        if (sCmnt == "") sCmnt = "tagme";
                        if (sTGen[0] == "") sTGen[0] = "tagme";
                        if (sTSrc[0] == "") sTSrc[0] = "tagme";
                        if (sTChr[0] == "") sTChr[0] = "tagme";
                        if (sTArt[0] == "") sTArt[0] = "tagme";
                        SearchProg = "Searching (" + i + " / " + iCnt + ")";

                        // Image Name
                        if (iRet[i] != 3)
                            if (imagName[0] != "")
                                for (int a = 0; a < imagName.Length; a++)
                                {
                                    if (iRet[i] != 3)
                                        if (sName.ToLower().Contains(imagNameV[a]))
                                        {
                                            if (iRet[i] != 1)
                                                if (imagNameP[a] == 2) iRet[i] = 2;
                                            if (imagNameP[a] == 3) iRet[i] = 3;
                                            if (imagNameP[a] == 1) iRet[i] = 1;
                                        }
                                    if (iRet[i] != 1)
                                        if (imagNameP[a] == 1) iRet[i] = 3;
                                }

                        // Image Format
                        if (iRet[i] != 3)
                            if (imagFormat[0] != "")
                                for (int a = 0; a < imagFormat.Length; a++)
                                {
                                    if (iRet[i] != 3)
                                        if (sType.ToLower() == imagFormatV[a])
                                        {
                                            if (iRet[i] != 1)
                                                if (imagFormatP[a] == 2) iRet[i] = 2;
                                            if (imagFormatP[a] == 3) iRet[i] = 3;
                                            if (imagFormatP[a] == 1) iRet[i] = 1;
                                        }
                                    if (iRet[i] != 1)
                                        if (sType.ToLower() != imagFormatV[a])
                                        {
                                            if (imagFormatP[a] == 1) iRet[i] = 3;
                                        }
                                }

                        // Image Resolution
                        if (iRet[i] != 3)
                            if (imagRes[0] != "")
                                for (int a = 0; a < imagRes.Length; a++)
                                    if (iRet[i] != 3)
                                    {
                                        if (imagResP[a] == 1)
                                        {
                                            if (iResX > imagResV[a].X) iRet[i] = 3;
                                            if (iResY > imagResV[a].Y) iRet[i] = 3;
                                        }
                                        if (imagResP[a] == 2)
                                        {
                                            if (iResX < imagResV[a].X) iRet[i] = 3;
                                            if (iResY < imagResV[a].Y) iRet[i] = 3;
                                        }
                                    }

                        // Image Rating
                        if (iRet[i] != 3)
                            if (imagRating[0] != "")
                                for (int a = 0; a < imagRating.Length; a++)
                                    if (iRet[i] != 3)
                                    {
                                        if (imagRatingP[a] == 1)
                                        {
                                            if (iRate > imagRatingV[a]) iRet[i] = 3;
                                        }
                                        if (imagRatingP[a] == 2)
                                        {
                                            if (iRate < imagRatingV[a]) iRet[i] = 3;
                                        }
                                    }

                        // Tags - General
                        if (iRet[i] != 3)
                            if (tagsGeneral[0] != "")
                                for (int a = 0; a < tagsGeneral.Length; a++)
                                {
                                    int iRetTh = 0;
                                    if (iRet[i] != 3)
                                        for (int b = 0; b < sTGen.Length; b++)
                                            if (iRetTh != 3)
                                                if (sTGen[b].ToLower() == tagsGeneralV[a])
                                                {
                                                    if (iRetTh != 1)
                                                        if (tagsGeneralP[a] == 2) iRetTh = 2;
                                                    if (tagsGeneralP[a] == 3) iRetTh = 3;
                                                    if (tagsGeneralP[a] == 1) iRetTh = 1;
                                                }
                                    if (iRetTh != 1)
                                        if (tagsGeneralP[a] == 1) iRetTh = 3;
                                    if (iRetTh != 0) iRet[i] = iRetTh;
                                }

                        // Tags - Source
                        if (iRet[i] != 3)
                            if (tagsSource[0] != "")
                                for (int a = 0; a < tagsSource.Length; a++)
                                {
                                    int iRetTh = 0;
                                    if (iRet[i] != 3)
                                        for (int b = 0; b < sTSrc.Length; b++)
                                            if (iRetTh != 3)
                                                if (sTSrc[b].ToLower() == tagsSourceV[a])
                                                {
                                                    if (iRetTh != 1)
                                                        if (tagsSourceP[a] == 2) iRetTh = 2;
                                                    if (tagsSourceP[a] == 3) iRetTh = 3;
                                                    if (tagsSourceP[a] == 1) iRetTh = 1;
                                                }
                                    if (iRetTh != 1)
                                        if (tagsSourceP[a] == 1) iRetTh = 3;
                                    if (iRetTh != 0) iRet[i] = iRetTh;
                                }

                        // Tags - Chars
                        if (iRet[i] != 3)
                            if (tagsChars[0] != "")
                                for (int a = 0; a < tagsChars.Length; a++)
                                {
                                    int iRetTh = 0;
                                    if (iRet[i] != 3)
                                        for (int b = 0; b < sTChr.Length; b++)
                                            if (iRetTh != 3)
                                                if (sTChr[b].ToLower() == tagsCharsV[a])
                                                {
                                                    if (iRetTh != 1)
                                                        if (tagsCharsP[a] == 2) iRetTh = 2;
                                                    if (tagsCharsP[a] == 3) iRetTh = 3;
                                                    if (tagsCharsP[a] == 1) iRetTh = 1;
                                                }
                                    if (iRetTh != 1)
                                        if (tagsCharsP[a] == 1) iRetTh = 3;
                                    if (iRetTh != 0) iRet[i] = iRetTh;
                                }

                        // Tags - Artists
                        if (iRet[i] != 3)
                            if (tagsArtists[0] != "")
                                for (int a = 0; a < tagsArtists.Length; a++)
                                {
                                    int iRetTh = 0;
                                    if (iRet[i] != 3)
                                        for (int b = 0; b < sTArt.Length; b++)
                                            if (iRetTh != 3)
                                                if (sTArt[b].ToLower() == tagsArtistsV[a])
                                                {
                                                    if (iRetTh != 1)
                                                        if (tagsArtistsP[a] == 2) iRetTh = 2;
                                                    if (tagsArtistsP[a] == 3) iRetTh = 3;
                                                    if (tagsArtistsP[a] == 1) iRetTh = 1;
                                                }
                                    if (iRetTh != 1)
                                        if (tagsArtistsP[a] == 1) iRetTh = 3;
                                    if (iRetTh != 0) iRet[i] = iRetTh;
                                }

                        if (bGlobalSearch)
                        {
                            iRet[i] = 0;
                            for (int a = 0; a < tagsGeneral.Length; a++)
                            {
                                int[] iRetC = new int[5];
                                if (tagsGeneral[a] == sName.ToLower())
                                    iRetC[0] = tagsGeneralP[a];

                                // -- GENERAL -- //
                                for (int b = 0; b < sTGen.Length; b++)
                                {
                                    if (iRetC[1] != 3)
                                        if (tagsGeneralV[a] == sTGen[b].ToLower())
                                        {
                                            if (iRetC[1] != 1)
                                                if (tagsGeneralP[a] == 2) iRetC[1] = 2;
                                            if (tagsGeneralP[a] == 3) iRetC[1] = 3;
                                            if (tagsGeneralP[a] == 1) iRetC[1] = 1;
                                        }
                                }
                                if (iRetC[1] != 1)
                                    if (tagsGeneralP[a] == 1) iRetC[1] = 3;

                                // -- SOURCE -- //
                                for (int b = 0; b < sTSrc.Length; b++)
                                {
                                    if (iRetC[2] != 3)
                                        if (tagsGeneralV[a] == sTSrc[b].ToLower())
                                        {
                                            if (iRetC[2] != 1)
                                                if (tagsGeneralP[a] == 2) iRetC[2] = 2;
                                            if (tagsGeneralP[a] == 3) iRetC[2] = 3;
                                            if (tagsGeneralP[a] == 1) iRetC[2] = 1;
                                        }
                                }
                                if (iRetC[2] != 1)
                                    if (tagsGeneralP[a] == 1) iRetC[2] = 3;

                                // -- CHARACTERS -- //
                                for (int b = 0; b < sTChr.Length; b++)
                                {
                                    if (iRetC[3] != 3)
                                        if (tagsGeneralV[a] == sTChr[b].ToLower())
                                        {
                                            if (iRetC[3] != 1)
                                                if (tagsGeneralP[a] == 2) iRetC[3] = 2;
                                            if (tagsGeneralP[a] == 3) iRetC[3] = 3;
                                            if (tagsGeneralP[a] == 1) iRetC[3] = 1;
                                        }
                                }
                                if (iRetC[3] != 1)
                                    if (tagsGeneralP[a] == 1) iRetC[3] = 3;

                                // -- ARTISTS -- //
                                for (int b = 0; b < sTArt.Length; b++)
                                {
                                    if (iRetC[4] != 3)
                                        if (tagsGeneralV[a] == sTArt[b].ToLower())
                                        {
                                            if (iRetC[4] != 1)
                                                if (tagsGeneralP[a] == 2) iRetC[4] = 2;
                                            if (tagsGeneralP[a] == 3) iRetC[4] = 3;
                                            if (tagsGeneralP[a] == 1) iRetC[4] = 1;
                                        }
                                }
                                if (iRetC[4] != 1)
                                    if (tagsGeneralP[a] == 1) iRetC[4] = 3;

                                // -- RETURN OR NOT? -- //
                                for (int b = 0; b < iRetC.Length; b++)
                                    if (iRetC[b] == 1 || iRetC[b] == 2)
                                        iRet[i] = 1;
                                if (iRet[i] != 1) iRet[i] = 3;
                            }
                        }

                        // Deny flags
                        if (iRet[i] != 3)
                            for (int a = 0; a < cFlDeny.Length; a++)
                                if (cFlDeny[a] == '1' && cFlag[a] == '1')
                                    iRet[i] = 3;

                        // Return flags
                        if (iRet[i] != 3)
                            for (int a = 0; a < cFlRet.Length; a++)
                                if (cFlRet[a] == '1' && cFlag[a] == '1')
                                    iRet[i] = 2;

                    }
                }
            }

            i = -1; iCnt = 0;
            SearchProg = "Gathering";
            for (int a = 0; a < iRet.Length; a++)
            {
                if (iRet[a] == 2 || iRet[a] == 1) iCnt++;
            }
            if (ptRange.Y != -1) iCnt = Math.Min(iCnt, ptRange.Y);
            ImageData[] idRet = new ImageData[iCnt];
            SearchProg = iCnt + " results";

            i = -1; iCnt = -1;
            using (SQLiteCommand DBc = DB.Data.CreateCommand())
            {
                DBc.CommandText = "SELECT * FROM 'images'";
                using (SQLiteDataReader rd = DBc.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        iCnt++;
                        if (iRet[iCnt] == 2 || iRet[iCnt] == 1)
                        {
                            i++; int j = i - ptRange.X;
                            if (j >= idRet.Length) break;
                            idRet[j] = new ImageData();
                            idRet[j].sHash = rd.GetString(DB.diHash);
                            idRet[j].sThmb = rd.GetString(DB.diThmb);
                            idRet[j].sType = rd.GetString(DB.diType);
                            idRet[j].iLen = rd.GetInt32(DB.diFLen);
                            idRet[j].ptRes.X = rd.GetInt32(DB.diXRes);
                            idRet[j].ptRes.Y = rd.GetInt32(DB.diYRes);
                            idRet[j].sName = rd.GetString(DB.diName);
                            idRet[j].sPath = rd.GetString(DB.diPath);
                            idRet[j].cFlag = rd.GetString(DB.diFlag).ToCharArray(0, 32);
                            idRet[j].sCmnt = rd.GetString(DB.diCmnt);
                            idRet[j].iRate = rd.GetInt32(DB.diRate);
                            idRet[j].sTGen = rd.GetString(DB.diTGen);
                            idRet[j].sTSrc = rd.GetString(DB.diTSrc);
                            idRet[j].sTChr = rd.GetString(DB.diTChr);
                            idRet[j].sTArt = rd.GetString(DB.diTArt);
                        }
                    }
                }
            }
            return idRet;
        }
    }
    public class tagBase
    {
        #region Pointers
        public const int diHash = 0;
        public const int diName = 1;
        public const int diCmnt = 2;
        public const int diRate = 3;
        public const int diTGen = 4;
        public const int diTSrc = 5;
        public const int diTChr = 6;
        public const int diTArt = 7;
        #endregion
        public static SQLiteConnection Data;
        public static string Path = "";
        public static bool CanClose = true;
        public static bool IsOpen = false;

        public static bool Create(string sPath, bool bOverwrite)
        {
            try
            {
                if (Data != null) { Data.Close(); Data.Dispose(); }
                if (System.IO.File.Exists(sPath))
                {
                    if (bOverwrite) System.IO.File.Delete(sPath);
                    else return false;
                }
                SQLiteConnection.CreateFile(sPath);
                SQLiteConnection.CompressFile(sPath);
                Data = new SQLiteConnection("Data source=" + sPath);
                Data.Open(); Path = sPath;

                using (SQLiteCommand DBc = Data.CreateCommand())
                {
                    DBc.CommandText = "CREATE TABLE 'images' (" +
                        "'hash' char(32), " +
                        "'name' text, " +
                        "'cmnt' text, " +
                        "'rate' int, " +
                        "'tgen' text, " +
                        "'tsrc' text, " +
                        "'tchr' text, " +
                        "'tart' text, " +
                        "PRIMARY KEY ('hash'))";
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
        public static bool Open(string sPath)
        {
            if (!System.IO.File.Exists(sPath))
                return Create(sPath, true);

            try
            {
                if (Data != null) { Data.Close(); Data.Dispose(); }
                Data = new SQLiteConnection("Data source=" + sPath);
                Data.Open();
            }
            catch
            {
                IsOpen = false;
                return false;
            }
            if (Data.State == ConnectionState.Open)
            {
                Path = sPath; IsOpen = true; return true;
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
    }
}
