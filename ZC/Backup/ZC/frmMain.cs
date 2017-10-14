using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using System.Runtime.InteropServices;

namespace ZC
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

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
        #endregion
        #region "Standard" functions
        ///<summary>
        /// Splits msg by delim, returns bit part
        ///</summary>
        public static string Split(string msg, string delim, int part)
        {
            if (msg == "" || msg == null || delim == "" || delim == null) return "";
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
        public string[] aSplit2(string msg, string delim)
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
        public int Countword2(string msg, string delim)
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
        /// <summary>
        /// This function returns false and makes regkey if not exist.
        /// </summary>
        private bool Reg_DoesExist(string regPath)
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
        public string FileRead(string sFile)
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
        /// <param name="sFile"></param>
        /// <param name="sVal"></param>
        public void FileWrite(string sFile, string sVal)
        {
            System.IO.FileStream fs = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.GetEncoding("iso-8859-1"));
            sw.Write(sVal); sw.Close(); sw.Dispose();
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

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        Form fBG = new frmBackground();
        PanelArray AppPanel;    public static Panel aPanelClicked;      public static int aPanelClickedNum;
        PBoxArray AppIcon;      public static PictureBox aPBoxClicked;  public static int aPBoxClickedNum;
        LabelArray AppName;     public static Label aLabelClicked;      public static int aLabelClickedNum;
        LabelArray AppText;
        int PanCntX, PanCntY, PanCntT; AppInf[] Apps;

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0); this.Size = Screen.PrimaryScreen.Bounds.Size;
            cmdOptions.Location = new Point(12, this.Height - 35);
            cmdExit.Location = new Point(this.Width - 87, this.Height - 35);
            fBG.Show(); Application.DoEvents();

            string sAppsRaw = FileRead("apps.txt").Replace("\r", "");
            while (sAppsRaw.IndexOf("\n\n") != -1) sAppsRaw = sAppsRaw.Replace("\n\n", "\n");
            while (sAppsRaw.StartsWith("\n")) sAppsRaw = sAppsRaw.Substring(1);
            while (sAppsRaw.EndsWith("\n")) sAppsRaw = sAppsRaw.Substring(0, sAppsRaw.Length - 1);
            string[] AppsRaw = sAppsRaw.Split('\n');
            IconExtractor iconex = new IconExtractor();
            Apps = new AppInf[AppsRaw.GetUpperBound(0) + 1];
            for (int a = 0; a <= AppsRaw.GetUpperBound(0); a++)
            {
                Apps[a] = new AppInf();
                Apps[a].Name = Split(AppsRaw[a], "&%", 0);
                Apps[a].Path = Split(AppsRaw[a], "&%", 1);
				Apps[a].Comment = Split(AppsRaw[a], "&%", 2).Replace(":n:", "\r\n");
                if (!System.IO.File.Exists(Apps[a].Path)) MessageBox.Show(
                    "Critical error!\r\n" +
                    "The executable file for " + Apps[a].Name + " could NOT be found.\r\n\r\n" +
                    "Please verify the following location.\r\n" + Apps[a].Path);
				try{
					Apps[a].Icon = iconex.Extract(Apps[a].Path, IconSize.Large);
				}catch{
					Apps[a].Icon = this.Icon;
				}
            }

            AppPanel = new PanelArray(this); AppIcon = new PBoxArray(this);
            AppName = new LabelArray(this); AppText = new LabelArray(this);
            /* Control creation documentation - preset preferences
             * 
             * Unit name	Unit type	Unit loc	Unit size	Unit color
             * AppBox		Panel		12, 12		253, 070	064, 064, 064
             * AppIcon		PictureBox	03, 03		064, 064	016, 016, 016
             * AppName		Label		73, 03		174, 020	064, 064, 064
             * AppText		Label		73, 23		174, 044	032, 032, 032
             */
            PanCntX = Screen.PrimaryScreen.Bounds.Width / 260;
            PanCntY = (Screen.PrimaryScreen.Bounds.Height-28) / 80;
            PanCntT = PanCntX * PanCntY; int xOfs = 0, yOfs = 0;
            int PanDistX = (Screen.PrimaryScreen.Bounds.Width - (PanCntX * 260) - 14) / (PanCntX - 1);
            int PanDistY = ((Screen.PrimaryScreen.Bounds.Height - 28) - (PanCntY * 80) - 14) / (PanCntY - 1);
            for (int y = 1; y <= PanCntY; y++)
            {
                yOfs += PanDistY + 80; if (y == 1) yOfs = 12;
                for (int x = 1; x <= PanCntX; x++)
                {
                    int aThis = ControlIntFromXY(x, y);
                    if (aThis > Apps.GetUpperBound(0)) goto OutOfApps;
                    xOfs += PanDistX + 260; if (x == 1) xOfs = 12;
                    AppPanel.NewPanel(); AppIcon.NewPBox(); AppName.NewLabel(); AppText.NewLabel();
                    AppPanel[aThis].Size = new Size(253, 70);
                    AppPanel[aThis].Location = new Point(((x * 260) - 260) + 12, ((y * 80) - 80) + 12);
                    AppPanel[aThis].Left = xOfs; AppPanel[aThis].Top = yOfs;
                    AppPanel[aThis].BackColor = Color.FromArgb(64, 64, 64);
                    AppPanel[aThis].Controls.Add(AppIcon[aThis]);
                    AppPanel[aThis].Controls.Add(AppName[aThis]);
                    AppPanel[aThis].Controls.Add(AppText[aThis]);
                    AppIcon[aThis].Size = new Size(64, 64);
                    AppIcon[aThis].Location = new Point(3, 3);
                    AppIcon[aThis].BackColor = Color.FromArgb(32, 32, 32); //16, 16, 16);
                    AppIcon[aThis].BackgroundImage = (Apps[aThis].Icon.ToBitmap() as Image);
                    AppIcon[aThis].BackgroundImageLayout = ImageLayout.Stretch;
                    AppName[aThis].AutoSize = false; AppName[aThis].Size = new Size(178, 17);
                    AppName[aThis].Location = new Point(71, 3);
                    AppName[aThis].BackColor = Color.FromArgb(32, 32, 32); //64, 64, 64);
                    AppName[aThis].ForeColor = Color.FromArgb(255, 255, 255);
                    AppName[aThis].Text = Apps[aThis].Name;
                    AppName[aThis].TextAlign = ContentAlignment.MiddleCenter;
                    AppName[aThis].Font = new Font("Arial", 10F, FontStyle.Bold | FontStyle.Italic);
                    AppText[aThis].AutoSize = false; AppText[aThis].Size = new Size(178, 44);
                    AppText[aThis].Location = new Point(71, 23);
                    AppText[aThis].BackColor = Color.FromArgb(32, 32, 32);
                    AppText[aThis].ForeColor = Color.FromArgb(255, 255, 255);
                    AppText[aThis].Text = Apps[aThis].Comment;
                    AppText[aThis].TextAlign = ContentAlignment.TopCenter;
                    AppText[aThis].Font = new Font("Arial", 8.25F, FontStyle.Regular);
                    AppPanel[aThis].BringToFront(); AppIcon[aThis].BringToFront();
                    AppName[aThis].BringToFront(); AppText[aThis].BringToFront();
                }
            }
        OutOfApps:
            return;
        }

        private int ControlIntFromXY(int x, int y)
        {
            return ((y * PanCntX) - PanCntX) + x - 1;
        }
        private Point XYFromControlInt(int ind)
        {
            return new Point(ind/PanCntY, ind - (ind/PanCntY));
        }

        private void cmdOptions_Click(object sender, EventArgs e)
        {

        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tPollForClicks_Tick(object sender, EventArgs e)
        {
            if (aPanelClicked != null)
            {
                aPanelClicked = null; this.Hide(); fBG.Hide();
                System.Diagnostics.Process.Start(Apps[aPanelClickedNum - 1].Path);
            }
            if (aLabelClicked != null)
            {
                aLabelClicked = null; this.Hide(); fBG.Hide();
                System.Diagnostics.Process.Start(Apps[aLabelClickedNum - 1].Path);
            }
            if (aPBoxClicked != null)
            {
                aPBoxClicked = null; this.Hide(); fBG.Hide();
                System.Diagnostics.Process.Start(Apps[aPBoxClickedNum - 1].Path);
            }
        }

        private void tHotkeys_Tick(object sender, EventArgs e)
        {
            tHotkeys.Interval = 10;
            bool keyCtrl = false, keyAlt = false, keyShift = false;
            if (GetAsyncKeyState(Keys.LControlKey) != 0) keyCtrl = true;
            if (GetAsyncKeyState(Keys.LMenu) != 0) keyAlt = true;
            if (GetAsyncKeyState(Keys.ShiftKey) != 0) keyShift = true;
            if (keyCtrl && keyAlt && keyShift)
            {
                tHotkeys.Interval = 1000;
                //System.Media.SystemSounds.Exclamation.Play();
                fBG.Hide(); this.Hide(); Application.DoEvents();
                fBG.Show(); Application.DoEvents(); this.Show();
            }
            if (cmdOptions.Focused || cmdExit.Focused)
            {
                if (GetAsyncKeyState(Keys.Z) != 0)
                {
                    tHotkeys.Interval = 1000;
                    //System.Media.SystemSounds.Asterisk.Play();
                    fBG.Hide(); this.Hide(); Application.DoEvents();
                }
                if (GetAsyncKeyState(Keys.I) != 0)
                {
                    MessageBox.Show("System information\r\n\r\n" +
                        "Windows credentials: " +
                        System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
                }
            }
        }
    }

    public class AppInf
    {
        public string Name, Comment, Path;
        public Icon Icon;
    }
}