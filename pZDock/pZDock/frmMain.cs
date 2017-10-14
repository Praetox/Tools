using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pZDock
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        static int t2aDelay = 0, iYOfs = 7, iAppsLoc = 2, iAppsWd;
        static double dOpacity = 1;
        static bool bDockVis = false, bCancelHide = false, bHideCancelled = false;
        static string[] Apps; static Icon[] Icons;
        PBoxArray aPBox; public static PictureBox aPBoxClicked; public static int aPBoxClickedNum;

        #region Conversions
        /// <summary>
        /// Returns character from ascii
        /// </summary>
        static string Chr(int id)
        {
            return Convert.ToChar(id).ToString();
        }
        /// <summary>
        /// Returns ascii from character
        /// </summary>
        static int Asc(char id)
        {
            return Convert.ToInt16(id);
        }
        /// <summary>
        /// Converts integer to byte
        /// </summary>
        public static byte[] Int2Byte(int Value)
        {
            byte[] rawbuf = BitConverter.GetBytes(Value);
            int a = 0; for (a = rawbuf.Length; a > 0; a--) if (rawbuf[a - 1] != 0) break;
            byte[] buf = new byte[a]; for (a = 0; a < buf.Length; a++) buf[a] = rawbuf[a];
            return buf;
        }
        /// <summary>
        /// Converts byte to string
        /// </summary>
        public static String Byte2Str(byte[] Value)
        {
            int len = 0; for (len = Value.Length; len > 0; len--) if (Value[len - 1] != 0) break;
            string ret = ""; for (int a = 0; a < len; a++) ret += (char)Value[a];
            //byte[] buf = new byte[a]; for (a = 0; a < buf.Length; a++) buf[a] = Value[a];
            return ret; //System.Text.Encoding.ASCII.GetString(buf);
        }
        /// <summary>
        /// Converts string to byte
        /// </summary>
        public static byte[] Str2Byte(string Value)
        {
            byte[] ret = new byte[Value.Length];
            for (int a = 0; a < Value.Length; a++) ret[a] = (byte)Value[a];
            return ret;
        }
        #endregion
        #region "Standard" functions
        ///<summary>
        /// Splits msg by delim, returns bit part
        ///</summary>
        public static string Split(string msg, string delim, int part)
        {
            if (msg == "" || msg == null || delim == "" || delim == null) return "";
            if (msg.IndexOf(delim) == -1) return msg;
            for (int a = 0; a < part; a++)
            {
                msg = msg.Substring(msg.IndexOf(delim) + delim.Length);
            }
            if (msg.IndexOf(delim) == -1) return msg;
            return msg.Substring(0, msg.IndexOf(delim));
        }
        ///<summary>
        /// Splits msg by delim, returns string array
        ///</summary>
        public static string[] aSplit(string msg, string delim)
        {
            if (msg == "" || msg == null || delim == "" || delim == null) return new string[0];
            int spt = 0; string[] ret = new string[Countword(msg, delim) + 1];
            while (msg.Contains(delim))
            {
                ret[spt] = msg.Substring(0, msg.IndexOf(delim)); spt++;
                msg = msg.Substring(msg.IndexOf(delim) + delim.Length);
            }
            ret[spt] = msg;
            return ret;
        }
        /// <summary>
        /// Alternative aSplit, should be faster
        /// </summary>
        public static string[] aSplit2(string msg, string delim)
        {
            int spt = 0; string[] ret = new string[Countword(msg, delim) + 1];
            int FoundPos = 0; int delimLen = delim.Length;
            while (FoundPos != -1)
            {
                FoundPos = msg.IndexOf(delim);
                if (FoundPos != -1)
                {
                    ret[spt] = msg.Substring(0, FoundPos); spt++;
                    msg = msg.Substring(FoundPos + delimLen);
                }
            }
            ret[spt] = msg; return ret;
        }
        ///<summary>
        /// Counts occurrences of delim within msg
        ///</summary>
        public static int Countword(string msg, string delim)
        {
            int ret = 0; if (msg == "" || msg == null || delim == "" || delim == null) return 0;
            while (msg.Contains(delim))
            {
                msg = msg.Substring(msg.IndexOf(delim) + delim.Length); ret++;
            }
            return ret;
        }
        /// <summary>
        /// Alternative Countword, should be faster
        /// </summary>
        public static int Countword2(string msg, string delim)
        {
            int ret = 0; int FoundPos = 0; int delimLen = delim.Length;
            while (FoundPos != -1)
            {
                FoundPos = msg.IndexOf(delim);
                if (FoundPos != -1)
                {
                    msg = msg.Substring(FoundPos + delimLen); ret++;
                }
            }
            return ret;
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
            string ret = "";
            for (int a = 0; a < vl; a++)
            {
                ret += " ";
            }
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
        public static int sTick()
        {
            return (System.DateTime.Now.Hour * 60 * 60) +
                   (System.DateTime.Now.Minute * 60) +
                   (System.DateTime.Now.Second);
        }
        ///<summary>
        /// Returns what sTick will be after vl seconds
        ///</summary>
        public static int T2A(int vl)
        {
            int ret = sTick() + vl + t2aDelay;
            if (ret > 86400) ret -= 86400;
            return ret;
        }
        ///<summary>
        /// Returns the waiting time until sTick is vl
        ///</summary>
        public static int T2B(int vl)
        {
            int ret = vl - sTick();
            if (ret < 0) ret += 86400;
            if (ret > 3600) ret = 0;
            return ret;
        }
        ///<summary>
        /// Returns how many seconds sTick has passed vl
        ///</summary>
        public static int T2C(int vl)
        {
            int ret = sTick() - vl;
            if (ret < 0) ret += 86400;
            if (ret > 3600) ret = 0;
            return ret;
        }
        ///<summary>
        /// Sleeps for vl seconds
        ///</summary>
        public static void iSlp(int vl)
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
        public static long Tick()
        {
            return System.DateTime.Now.Ticks / 10000;
        }
        ///<summary>
        /// Returns hh:mm:ss of vl (seconds)
        ///</summary>
        public static string s2ms(int vl)
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
        /// <summary>
        /// This function returns false and makes regkey if not exist.
        /// </summary>
        static bool Reg_DoesExist(string regPath)
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser;
                key = key.OpenSubKey(regPath, true);
                long lol = key.SubKeyCount;
                return true;
            }
            catch
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser;
                key.CreateSubKey(regPath);
                return false;
            }
        }
        ///<summary>
        /// Returns current date and time
        ///</summary>
        public static string TDNow()
        {
            return System.DateTime.Now.ToShortDateString() + " .::. " +
                   System.DateTime.Now.ToLongTimeString();
        }
        ///<summary>
        /// Returns MD5 of vl
        ///</summary>
        public static string MD5(string vl)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(vl);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
        #endregion
        #region File and array manipulation
        /// <summary>
        /// Reads sFile, works with norwegian letters ae oo aa
        /// </summary>
        public static string FileRead(string sFile)
        {
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(sFile, Encoding.GetEncoding("iso-8859-1"));
                string ret = sr.ReadToEnd();
                while ((ret.Substring(ret.Length - 1) == "\r") || (ret.Substring(ret.Length - 1) == "\n"))
                    ret = ret.Substring(0, ret.Length - 1);
                sr.Close(); sr.Dispose(); return ret;
            }
            catch { return ""; }
        }
        /// <summary>
        /// Writes sVal to sFile, works with norwegian letters ae oo aa
        /// </summary>
        /// <param name="sFile">Target file</param>
        /// <param name="sVal">The string to write</param>
        /// <param name="bAppend">Append instead of overwrite</param>
        public static void FileWrite(string sFile, string sVal, bool bAppend)
        {
            System.IO.FileMode AccessType = System.IO.FileMode.Create;
            if (bAppend) AccessType = System.IO.FileMode.Append;
            System.IO.FileStream fs = new System.IO.FileStream(sFile, AccessType);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.GetEncoding("iso-8859-1"));
            sw.Write(sVal); sw.Close(); sw.Dispose();
        }
        public static void FileWrite(string sFile, string sVal)
        {
            FileWrite(sFile, sVal, false);
        }
        /// <summary>
        /// Sorts vl[] alphabetically, ignores letters other than 0-9 a-z
        /// </summary>
        public static string[] SortStringArrayAlphabetically(string[] vl)
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
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            aPBox = new PBoxArray(this); IconExtractor iconex = new IconExtractor();
            Apps = FileRead("apps.txt").Replace("\r", "").Split('\n');
            Icons = new Icon[Apps.GetUpperBound(0)+1];
            for (int a = 0; a <= Apps.GetUpperBound(0); a++)
            {
                aPBox.NewPBox();
                try {
                    Icons[a] = iconex.Extract(Apps[a], IconSize.Large);
                }
                catch {
                    Icons[a] = this.Icon;
                }
                aPBox[a].BackgroundImage = Icons[a].ToBitmap() as Image;
                aPBox[a].BackgroundImageLayout = ImageLayout.Zoom;
                aPBox[a].Size = new Size(64, 64);
                aPBox[a].Visible = true;
                TTip.SetToolTip(aPBox[a], Apps[a].Split('\\').Last());
            }
            iAppsWd = (Apps.GetUpperBound(0) + 1) * 66; Cover.BringToFront();
            this.Location = new Point(
                (Screen.PrimaryScreen.Bounds.Width / 2) - (this.Width / 2),
                (-this.Size.Height) + iYOfs);
        }

        private void AppsPlace()
        {
            int CrsX = Cursor.Position.X;
            aPBox[0].Location = new Point(iAppsLoc, 2);
            int iCurLoc = iAppsLoc;
            if (aPBox.Count > 1)
            {
                for (int a = 1; a < aPBox.Count; a++)
                {
                    iCurLoc += 66;
                    aPBox[a].Location = new Point(iCurLoc, 2);
                }
            }
        }
        private void AppsRedraw()
        {
            for (int a = 0; a < aPBox.Count; a++)
                aPBox[a].Refresh();
        }

        private void frmMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (!bDockVis) DockVis(true);
            else bCancelHide = true;
        }
        private void Cover_Click(object sender, EventArgs e)
        {
            DockVis(!bDockVis);
        }

        private void DockVis(bool bDockShow)
        {
            tMover.Enabled = false; tHider.Enabled = false;
            double crX = -1; double crY = -1; double crW = -1; double crH = -1;
            double stX = -1; double stY = -1; double stW = -1; double stH = -1;
            double crO = -1; double stO = -1; double steps = -1;
            if (bDockShow)
            {
                iAppsLoc = -(iAppsWd / 2) + (Screen.PrimaryScreen.Bounds.Width / 2);
                Cover.Visible = false; AppsPlace(); AppsRedraw();
                crX = this.Location.X; crY = this.Location.Y; crO = this.Opacity;
                crW = this.Size.Width; crH = this.Size.Height;
                steps = 8; stY = 0 / steps; stX = crX / steps; stO = crO / steps;
                stW = (Screen.PrimaryScreen.Bounds.Width - crW) / steps; stH = 0 / steps;
                for (int vl = 0; vl < steps; vl++)
                {
                    crX -= stX; crY += stY; crW += stW; crY += stY;
                    this.Location = new Point((int)crX, (int)crY);
                    this.Size = new Size((int)crW, (int)crH);
                    this.Opacity = crO;
                    Application.DoEvents(); System.Threading.Thread.Sleep(10);
                }
                crX = this.Location.X; crY = this.Location.Y; crO = this.Opacity;
                crW = this.Size.Width; crH = this.Size.Height;
                steps = 16; stX = 0 / steps; stY = (68 - iYOfs) / steps;
                stW = 0 / steps; stH = 0 / steps; stO = (dOpacity - crO) / steps;
                for (int vl = 0; vl < steps; vl++)
                {
                    crX += stX; crY += stY; crW += stW; crH += stH; crO += stO;
                    this.Location = new Point((int)crX, (int)crY); this.Opacity = crO;
                    Application.DoEvents(); System.Threading.Thread.Sleep(10);
                }
                bDockVis = true;
            }
            else
            {
                crX = this.Location.X; crY = this.Location.Y; crO = this.Opacity;
                crW = this.Size.Width; crH = this.Size.Height;
                steps = 16; stX = 0 / steps; stY = (68 - iYOfs) / steps;
                stW = 0 / steps; stH = 0 / steps; stO = (crO - 0.5) / steps;
                for (int vl = 0; vl < steps; vl++)
                {
                    crX += stX; crY -= stY; crW += stW; crH += stH; crO -= stO;
                    this.Location = new Point((int)crX, (int)crY); this.Opacity = crO;
                    Application.DoEvents(); System.Threading.Thread.Sleep(10);
                }
                Cover.Visible = true;
                crX = this.Location.X; crY = this.Location.Y; crO = this.Opacity;
                crW = this.Size.Width; crH = this.Size.Height;
                steps = 8; stY = 0 / steps; stX = ((Screen.PrimaryScreen.Bounds.Width - 128) / 2) / steps;
                stW = (Screen.PrimaryScreen.Bounds.Width - 128) / steps; stH = 0 / steps; stO = crO / steps;
                for (int vl = 0; vl < steps; vl++)
                {
                    crX += stX; crY += stY; crW -= stW; crY += stY;
                    this.Location = new Point((int)crX, (int)crY);
                    this.Size = new Size((int)crW, (int)crH);
                    this.Opacity = crO;
                    Application.DoEvents(); System.Threading.Thread.Sleep(10);
                }
                bDockVis = false;
            }
            tHider.Enabled = true; tMover.Enabled = true;
        }

        private void tHider_Tick(object sender, EventArgs e)
        {
            if (!bDockVis) { return; }
            int CurY = Cursor.Position.Y;
            int ThiY = this.Location.Y;
            if (bHideCancelled && CurY < ThiY + 128)
            {
                bCancelHide = false;
                bHideCancelled = false;
            }
            if (CurY > ThiY + 128)
            {
                if (bCancelHide)
                {
                    bHideCancelled = true;
                }
                else
                {
                    DockVis(false);
                }
            }
        }

        private void tMover_Tick(object sender, EventArgs e)
        {
            if (!bDockVis) return;
            int xDiff = (this.Width / 2) - Cursor.Position.X;
            xDiff = xDiff / 10;
            if (xDiff == 0) return;
            if (iAppsLoc > Screen.PrimaryScreen.Bounds.Width) xDiff = -10;
            if (iAppsLoc < -(iAppsWd)) xDiff = 10;
            {
                iAppsLoc += xDiff; AppsPlace(); Application.DoEvents();
            }
        }

        private void tAppStart_Tick(object sender, EventArgs e)
        {
            if (aPBoxClicked != null)
            {
                aPBoxClicked = null;
                System.Diagnostics.Process.Start(Apps[aPBoxClickedNum]);
            }
        }

        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            MessageBox.Show(e.Data.GetType().ToString());
        }
    }
}
