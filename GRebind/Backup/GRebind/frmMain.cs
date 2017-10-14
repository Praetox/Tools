using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace GRebind
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        #region APIs
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("User32.Dll")]
        static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("user32.dll")]
        static extern byte VkKeyScan(char ch);
        #endregion
        #region Hook stuff
        private static bool KH_Active = false;
        private static bool KH_InCallback = false;
        private static LowLevelKeyboardProc KH_Proc = HookCallback;
        private static IntPtr KH_HID = IntPtr.Zero;
        private const int WH_KEYBOARD_LL = 0x0D;
        private static uint WM_KEYDOWN = 0x100;
        private static uint WM_KEYUP = 0x101;
        private static uint KE_KEYDOWN = 0x00;
        private static uint KE_KEYUP = 0x02;
        private static int IK_Key = 0;
        #endregion
        #region Global variables
        private static string sTarget = "";
        private static Random rnd = new Random();
        private static int[] RM_Key1 = new int[0];
        private static int[] RM_Key2 = new int[0];
        private static IntPtr IP_Handle = (IntPtr)0;
        private static bool bGlobal = false;
        private static bool bInject = false;
        private static bool bRapid = false;
        private static bool bIntRapid = false;
        private static string sAppPath = "";
        public static string sAppVer = "";
        public static string sPrgDomain = "http://tox.awardspace.us/div/";
        public static string sToxDomain = "http://praetox.com/";
        public static bool[] bKeyStates = new bool[255];
        public static int iRapid_D = 50;
        public static int iRapid_U = 50;
        public static int iRapid_DF = 7;
        public static int iRapid_UF = 7;
        public static long[] lRapid_DT = new long[255];
        public static long[] lRapid_UT = new long[255];
        #endregion

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
        public void FileWrite(string sFile, string sVal, bool bAppend)
        {
            System.IO.FileMode AccessType = System.IO.FileMode.Create;
            if (bAppend) AccessType = System.IO.FileMode.Append;
            System.IO.FileStream fs = new System.IO.FileStream(sFile, AccessType);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.GetEncoding("iso-8859-1"));
            sw.Write(sVal); sw.Close(); sw.Dispose();
        }
        public void FileWrite(string sFile, string sVal)
        {
            FileWrite(sFile, sVal, false);
        }
        public static long Tick()
        {
            return System.DateTime.Now.Ticks / 10000000;
        }
        public static long TickMS()
        {
            return System.DateTime.Now.Ticks / 10000;
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, KH_Proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            bool bCaught = false;
            bool bResume = true;
            if (nCode < 0) bResume = false;
            if (!bInject && KH_InCallback) bResume = false;
            if (bResume)
            {
                KH_InCallback = true;
                bool bKD = (wParam == (IntPtr)WM_KEYDOWN);
                bool bKU = (wParam == (IntPtr)WM_KEYUP);
                if (bKD || bKU)
                {
                    if (bGlobal || IP_Handle != (IntPtr)0)
                    {
                        int iKey = Marshal.ReadInt32(lParam);
                        if (bKD) IK_Key = iKey;
                        if (bKD) bKeyStates[iKey] = true;
                        if (bKU) bKeyStates[iKey] = false;
                        if (KH_Active)
                        {
                            for (int a = 0; a < RM_Key1.Length; a++)
                            {
                                if (iKey == RM_Key1[a]) bCaught = true;
                            }
                            if (bCaught)
                            {
                                uint wPrm = 0;
                                if (bInject && bKD) wPrm = WM_KEYDOWN;
                                if (bInject && !bKD) wPrm = WM_KEYUP;
                                if (!bInject && bKD) wPrm = KE_KEYDOWN;
                                if (!bInject && !bKD) wPrm = KE_KEYUP;
                                for (int a = 0; a < RM_Key1.Length; a++)
                                {
                                    if (iKey == RM_Key1[a])
                                    {
                                        lRapid_DT[RM_Key2[a]] = 0;
                                        lRapid_UT[RM_Key2[a]] = 0;
                                        if (bInject)
                                            PostMessage(IP_Handle, wPrm, RM_Key2[a], 0);
                                        else
                                            keybd_event((byte)RM_Key2[a], 0, wPrm, 0);
                                    }
                                }
                            }
                        }
                    }
                }
                KH_InCallback = false;
            }
            if (bCaught) return (IntPtr)1;
            return CallNextHookEx(KH_HID, nCode, wParam, lParam);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            sAppVer = Application.ProductVersion.Substring(
                0, Application.ProductVersion.LastIndexOf("."));
            this.Text += sAppVer;

            KH_HID = SetHook(KH_Proc);
            msg.Dock = DockStyle.Fill;
            Process[] procs = Process.GetProcesses();
            string[] saprocs = new string[procs.Length];
            for (int a = 0; a < saprocs.Length; a++)
            {
                saprocs[a] = procs[a].ProcessName;
            }
            saprocs = SortStringArrayAlphabetically(saprocs);
            foreach (string proc in saprocs)
                Target_dd.Items.Add(proc);
            Target_dd.SelectedIndex = 0;

            sAppPath = Application.StartupPath.Replace("\\", "/");
            if (!sAppPath.EndsWith("/")) sAppPath += "/";
            Profile_ReloadList();

            try
            {
                bool bUpdateCheckOK = true;
                WebReq WR = new WebReq();
                WR.Request(sPrgDomain + "GRebind_version.php?cv=" + sAppVer);
                long lUpdateStart = Tick();
                while (!WR.isReady && bUpdateCheckOK)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(10);
                    if (Tick() > lUpdateStart + 10000) bUpdateCheckOK = false;
                }
                string wrh = WR.Response;
                if (!bUpdateCheckOK) throw new Exception("wat");
                if (wrh.Contains("<WebReq_Error>")) throw new Exception("wat");

                if (!wrh.Contains("<VERSION>" + sAppVer + "</VERSION>"))
                {
                    string sNewVer = wrh;
                    sNewVer = sNewVer.Substring(sNewVer.IndexOf("<VERSION>") + 9);
                    sNewVer = sNewVer.Substring(0, sNewVer.IndexOf("</VERSION>"));
                    bool GetUpdate = (DialogResult.Yes == MessageBox.Show(
                        "A new version (" + sNewVer + ") is available. Update?",
                        "Updater", MessageBoxButtons.YesNo));
                    if (GetUpdate)
                    {
                        string UpdateLink = new System.Net.WebClient().DownloadString(
                            sToxDomain + "inf/GRebind_link.html").Split('%')[1];
                        System.Diagnostics.Process.Start(UpdateLink + "?cv=" + sAppVer);
                        Application.Exit();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Couldn't check for updates.", "Oh snap.");
            }

            if (Profile_dd.SelectedIndex == -1)
            {
                this.Opacity = 0; this.Visible = true;
                this.BringToFront(); this.Focus();
                Application.DoEvents();
                for (double a = 0; a <= 1; a += 0.05)
                {
                    this.Opacity = a; Application.DoEvents();
                    System.Threading.Thread.Sleep(10);
                }
                this.Opacity = 1;
            }
            else this.Show(); Application.DoEvents();
            tPollFocus.Start(); tPollProc.Start();
            tRapid.Start(); tHide.Start();
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnhookWindowsHookEx(KH_HID);
        }

        private void Keys_ReloadList()
        {
            Keys_lst.Items.Clear();
            for (int a = 0; a < RM_Key1.Length; a++)
            {
                string sK1 = ((Keys)RM_Key1[a]).ToString();
                string sK2 = ((Keys)RM_Key2[a]).ToString();
                while (sK1.Length < 8) sK1 = " " + sK1;
                while (sK2.Length < 8) sK2 = sK2 + " ";
                if (sK1.Length > 8) sK1 = sK1.Substring(0, 8);
                if (sK2.Length > 8) sK2 = sK2.Substring(0, 8);
                Keys_lst.Items.Add(" " + sK1 + " >> " + sK2 + " ");
            }
        }
        private void Keys_cmdAdd_Click(object sender, EventArgs e)
        {
            msg.Text = "";
            tPollFocus.Enabled = false;
            tPollProc.Enabled = false;
            Target_pn.Visible = false;
            Profile_pn.Visible = false;
            Options_pn.Visible = false;
            Keys_pn.Visible = false;
            Logo.Visible = false;
            msg.Visible = true;
            cmdDummy.Focus();
            bGlobal = false;
            IP_Handle = this.Handle;
            if (!KH_Active) KH_HID = SetHook(KH_Proc);
            KH_Active = false;

            IK_Key = 0;
            msg.Text = "Press key to assign to";
            while (IK_Key == 0)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            int iKey1 = IK_Key;

            IK_Key = 0;
            msg.Text = "Press key to emulate";
            while (IK_Key == 0)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            int iKey2 = IK_Key;

            int[] bKeys1 = RM_Key1; RM_Key1 = new int[bKeys1.Length + 1];
            int[] bKeys2 = RM_Key2; RM_Key2 = new int[bKeys2.Length + 1];
            bKeys1.CopyTo(RM_Key1, 0); RM_Key1[RM_Key1.Length - 1] = iKey1;
            bKeys2.CopyTo(RM_Key2, 0); RM_Key2[RM_Key2.Length - 1] = iKey2;
            Keys_ReloadList();

            UnhookWindowsHookEx(KH_HID);
            IP_Handle = (IntPtr)0;
            msg.Visible = false;
            Logo.Visible = true;
            Keys_pn.Visible = true;
            Options_pn.Visible = true;
            Profile_pn.Visible = true;
            Target_pn.Visible = true;
            tPollProc.Enabled = true;
            tPollFocus.Enabled = true;
            Keys_cmdAdd.Focus();
            msg.Text = "";
        }
        private void Keys_cmdRem_Click(object sender, EventArgs e)
        {
            int cRem = 0;
            bool[] bRem = new bool[RM_Key1.Length];
            ListBox.SelectedIndexCollection iRem = Keys_lst.SelectedIndices;
            for (int a = 0; a < iRem.Count; a++)
            {
                bRem[iRem[a]] = true;
            }
            for (int a = 0; a < bRem.Length; a++)
            {
                if (bRem[a]) cRem++;
            }

            int iLoc = 0;
            int[] bKeys1 = RM_Key1; RM_Key1 = new int[bKeys1.Length - cRem];
            int[] bKeys2 = RM_Key2; RM_Key2 = new int[bKeys2.Length - cRem];

            iLoc = -1;
            for (int a = 0; a < bRem.Length; a++)
            {
                if (!bRem[a])
                {
                    iLoc++; RM_Key1[iLoc] = bKeys1[a];
                }
            }
            iLoc = -1;
            for (int a = 0; a < bRem.Length; a++)
            {
                if (!bRem[a])
                {
                    iLoc++; RM_Key2[iLoc] = bKeys2[a];
                }
            }
            Keys_ReloadList();
        }
        private void Keys_lst_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int iKey1 = Convert.ToInt32(InputBox.Show("Enter the keycode of the key to assign to", "Hello").Text);
                int iKey2 = Convert.ToInt32(InputBox.Show("Enter the keycode of the key to emulate", "Hello").Text);
                int[] bKeys1 = RM_Key1; RM_Key1 = new int[bKeys1.Length + 1];
                int[] bKeys2 = RM_Key2; RM_Key2 = new int[bKeys2.Length + 1];
                bKeys1.CopyTo(RM_Key1, 0); RM_Key1[RM_Key1.Length - 1] = iKey1;
                bKeys2.CopyTo(RM_Key2, 0); RM_Key2[RM_Key2.Length - 1] = iKey2;
                Keys_ReloadList();
            }
            catch
            {
                MessageBox.Show("Sorry, but only numbers are allowed.",
                    "You are doing it wrong", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private void Profile_ReloadList()
        {
            Profile_dd.Items.Clear();
            string[] saFolders = System.IO.Directory.GetFiles(sAppPath, "_*.cfg");
            for (int a = 0; a < saFolders.Length; a++)
            {
                Profile_dd.Items.Add(saFolders[a].Substring(1 + sAppPath.Length,
                    saFolders[a].Length - 5 - sAppPath.Length));
            }
            string sLastProfile = Program.Reg_Access("Current profile", "");
            if (sLastProfile != "")
            {
                Profile_dd.SelectedIndex = -1;
                for (int a = 0; a < saFolders.Length; a++)
                {
                    if ((saFolders[a].Substring(1 + sAppPath.Length,
                        saFolders[a].Length - 5 - sAppPath.Length))
                        == sLastProfile) Profile_dd.SelectedIndex = a;
                }
            }
        }
        private void Profile_cmdSave_Click(object sender, EventArgs e)
        {
            string sName = InputBox.Show(
                "Choose a profile name.", "Profile",
                Profile_dd.Text).Text;
            if (sName == "") return;
            sName = sName
                .Replace("\\", "").Replace("/", "").Replace(":", "")
                .Replace("\"", "").Replace("*", "").Replace("?", "")
                .Replace("<", "").Replace(">", "").Replace("|", "");
            if (sName == "")
            {
                MessageBox.Show("Invalid name. Profile was NOT saved.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return;
            }

            if (sTarget == "") sTarget = "!NO_TARGET!";
            string sTxt = sTarget + "\r\n";
            if (bInject) sTxt += "1"; else sTxt += "0"; sTxt += "-";
            if (bRapid) sTxt += "1"; else sTxt += "0"; sTxt += "-";
            sTxt += iRapid_D + "-" + iRapid_U + "-";
            sTxt += iRapid_DF + "-" + iRapid_UF + "\r\n";
            
            for (int a = 0; a < RM_Key1.Length; a++)
            {
                sTxt += RM_Key1[a] + "-" + RM_Key2[a] + "\r\n";
            }
            FileWrite("_" + sName + ".cfg", sTxt);
            Program.Reg_Access("Current profile", sName);
            Profile_ReloadList();
        }
        private void Profile_cmdLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string sTxt = FileRead("_" + Profile_dd.Text + ".cfg");
                if (sTxt != "")
                {
                    string sName = Profile_dd.Text;
                    Program.Reg_Access("Current profile", sName);
                    for (int a = 0; a < Profile_dd.Items.Count; a++)
                    {
                        if (Profile_dd.Items[a].ToString() == sName)
                            Profile_dd.SelectedIndex = a;
                    }
                    string[] saInf = sTxt.Trim(new char[] { '\r', '\n' }).Replace("\r", "").Split('\n');
                    sTarget = saInf[0];
                    Target_dd.Text = saInf[0];

                    string[] saPrm = saInf[1].Split('-');
                    int[] iaPrm = new int[saPrm.Length];
                    for (int a = 0; a < saPrm.Length; a++)
                        iaPrm[a] = Convert.ToInt32(saPrm[a]);
                    if (iaPrm[0] == 1) bInject = true; else bInject = false;
                    if (iaPrm[1] == 1) bRapid = true; else bRapid = false;
                    iRapid_D = iaPrm[2]; iRapid_U = iaPrm[3];
                    iRapid_DF = iaPrm[4]; iRapid_UF = iaPrm[5];
                    Options_chkInject.Checked = bInject;
                    Options_chkRapid.Checked = bRapid;
                    Options_Rapid_txtD.Text = iRapid_D + "";
                    Options_Rapid_txtDF.Text = iRapid_DF + "";
                    Options_Rapid_txtU.Text = iRapid_U + "";
                    Options_Rapid_txtUF.Text = iRapid_UF + "";

                    RM_Key1 = new int[saInf.Length - 2];
                    RM_Key2 = new int[saInf.Length - 2];
                    for (int a = 0; a < saInf.Length - 2; a++)
                    {
                        string[] saKeys = saInf[a + 2].Split('-');
                        RM_Key1[a] = Convert.ToInt16(saKeys[0]);
                        RM_Key2[a] = Convert.ToInt16(saKeys[1]);
                    }
                    Keys_ReloadList();
                }
                else
                {
                    MessageBox.Show("No profile to load", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show(
                    "I'm sorry, but this version of GRebind can't" + "\r\n" +
                    "read profiles created with older versions.",
                    "Oh snap.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void Profile_dd_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (Profile_dd.SelectedIndex == -1) return;
            Profile_cmdLoad_Click(new object(), new EventArgs());
        }

        private void Target_dd_SelectedIndexChanged(object sender, EventArgs e)
        {
            sTarget = Target_dd.Items[Target_dd.SelectedIndex].ToString();
        }

        private void tPollFocus_Tick(object sender, EventArgs e)
        {
            IntPtr ipFGW = GetForegroundWindow();
            if (bGlobal) IP_Handle = ipFGW;

            if (IP_Handle == ipFGW)
                KH_Active = true;
            else KH_Active = false;
        }
        private void tPollProc_Tick(object sender, EventArgs e)
        {
            if (sTarget != "All applications")
            {
                bGlobal = false;
                Process[] procs = Process.GetProcessesByName(sTarget);
                if (procs.Length == 0) IP_Handle = (IntPtr)0;
                else IP_Handle = procs[0].MainWindowHandle;
            }
            else bGlobal = true;
        }
        private void Logo_Click(object sender, EventArgs e)
        {
            Process.Start(sToxDomain);
        }

        private void Options_chkInject_CheckedChanged(object sender, EventArgs e)
        {
            bInject = Options_chkInject.Checked;
        }
        private void Options_chkRapid_CheckedChanged(object sender, EventArgs e)
        {
            bRapid = Options_chkRapid.Checked;
        }
        private void Options_Rapid_txtD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                iRapid_D = Convert.ToInt32(Options_Rapid_txtD.Text);
            }
            catch { }
        }
        private void Options_Rapid_txtU_TextChanged(object sender, EventArgs e)
        {
            try
            {
                iRapid_U = Convert.ToInt32(Options_Rapid_txtU.Text);
            }
            catch { }
        }
        private void Options_Rapid_txtDF_TextChanged(object sender, EventArgs e)
        {
            try
            {
                iRapid_DF = Convert.ToInt32(Options_Rapid_txtDF.Text);
            }
            catch { }
        }
        private void Options_Rapid_txtUF_TextChanged(object sender, EventArgs e)
        {
            try
            {
                iRapid_UF = Convert.ToInt32(Options_Rapid_txtUF.Text);
            }
            catch { }
        }
        private void Options_CharCodeMap_Click(object sender, EventArgs e)
        {
            string sOut = "<html><head><title>GRebind - Charcode map</title>" + "\r\n" +
                "<style type=\"text/css\">" + "\r\n" +
                "body{   background: #fff; padding: 8px; color: #000; }" + "\r\n" +
                "table{  background: #eee; padding: 4px;" + "\r\n" +
                "        border-color: #bbb; border-style: solid;" + "\r\n" +
                "        border-width: 1px 1px 1px 1px; }" + "\r\n" +
                "td{     text-align: left; margin: 0px; padding: 4px;" + "\r\n" +
                "        background-color: #fff; border-color: #ccc;" + "\r\n" +
                "        border-width: 1px; border-style: solid; }" + "\r\n" +
                "</style></head><body>" + "\r\n" +
                "<center><h2>What's this for?</h2><p>" + "\r\n" +
                "If you want to emulate a key which you don't have, you will need the keycodes for the key to<br>" + "\r\n" +
                "assign to and the key to emulate. Once you know the keycodes, you can add the binding<br>" + "\r\n" +
                "by double-clicking the \"Mapped keys\" list and entering the keycodes manually.<br>" + "\r\n" +
                "<b>TIP: Press ctrl-f and enter the key's name.</b></p>" + "\r\n" +
                "<br><table cellspacing=4>" + "\r\n" + "\r\n" +
                "<tr><td>Keycode</td><td>Key</td></tr>" + "\r\n";

            for (int a = 0; a < 255; a++)
            {
                sOut += "<tr><td>" + a + "</td><td>" + (Keys)a + "</td></tr>" + "\r\n";
            }

            sOut += "\r\n" + "</table></center></body></html>";
            byte[] bChars = System.Text.Encoding.UTF8.GetBytes(sOut);
            System.IO.FileStream fs = new System.IO.FileStream(
                "charmap.html", System.IO.FileMode.Create);
            fs.Write(bChars, 0, bChars.Length);
            fs.Flush(); fs.Close(); fs.Dispose();
            System.Diagnostics.Process.Start("charmap.html");
        }

        private void tRapid_Tick(object sender, EventArgs e)
        {
            if (!bRapid) return;
            if (bIntRapid) return;
            if (!KH_Active) return;
            bIntRapid = true;
            long lTck = TickMS();
            for (int a = 0; a < bKeyStates.Length; a++)
            {
                if (bKeyStates[a])
                {
                    int iKC = -1;
                    for (int b = 0; b < RM_Key1.Length; b++)
                    {
                        if (a == RM_Key1[b])
                        {
                            iKC = RM_Key2[b];
                        }
                    }
                    if (iKC != -1)
                    {
                        if (lRapid_DT[iKC] == 0 && lRapid_UT[iKC] == 0) lRapid_UT[iKC] = 1;
                        if (lRapid_DT[iKC] != 0 && lRapid_DT[iKC] < lTck)
                        {
                            lRapid_DT[iKC] = 0;
                            KH_InCallback = true;
                            if (bInject && !bGlobal) PostMessage(IP_Handle, WM_KEYDOWN, iKC, 0);
                            else keybd_event((byte)iKC, 0, KE_KEYDOWN, 0);
                            KH_InCallback = false;
                            lRapid_UT[iKC] = lTck + iRapid_U + rnd.Next(0, iRapid_UF + 1);
                        }
                        if (lRapid_UT[iKC] != 0 && lRapid_UT[iKC] < lTck)
                        {
                            lRapid_UT[iKC] = 0;
                            KH_InCallback = true;
                            if (bInject && !bGlobal) PostMessage(IP_Handle, WM_KEYUP, iKC, 0);
                            else keybd_event((byte)iKC, 0, KE_KEYUP, 0);
                            KH_InCallback = false;
                            lRapid_DT[iKC] = lTck + iRapid_D + rnd.Next(0, iRapid_DF + 1);
                        }
                    }
                }
            }
            bIntRapid = false;
        }
        private void tHide_Tick(object sender, EventArgs e)
        {
            tHide.Stop();
            if (Profile_dd.SelectedIndex != -1)
            {
                this.Hide();
                nIco.ShowBalloonTip(5000, "Running in background",
                    "GRebind is running with the last used profile.\r\n" +
                    "To show or hide the application, click this icon.",
                    ToolTipIcon.Info);
            }
        }
        private void nIco_Click(object sender, EventArgs e)
        {
            if (this.Visible) this.Hide();
            else this.Show();
        }
    }
}
