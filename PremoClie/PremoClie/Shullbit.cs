using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace PremoClie {
    public class Client {
        Socket sck;
        public Client(string ip, int port) {
            sck = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            sck.SendTimeout = 1000;
            sck.ReceiveTimeout = 1000;
            sck.NoDelay = true;
            sck.Connect(IPAddress.Parse(ip), port);
        }
        public bool Auth(string user, string pass) {
            SendLine("AUTH\t" + user + "\t" + pass);
            string rsp = GetLine();
            return (rsp == "OK");
        }
        public string GetLine() {
            try {
                StringBuilder ret = new StringBuilder();
                while (sck.Connected) {
                    byte[] b = new byte[1];
                    sck.Receive(b);
                    if ((char)b[0] == '\n') break;
                    ret.Append((char)b[0] + "");
                }
                return ret.ToString();
            }
            catch { return ""; }
        }
        public void SendLine(string str) {
            sck.Send(Encoding.ASCII.GetBytes(str + "\n"));
        }
    }
    public class Keybd {
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        Client cli;
        public bool active;
        private LowLevelKeyboardProc KH_Proc;
        private IntPtr KH_HID = IntPtr.Zero;
        private const int WH_KEYBOARD_LL = 0x0D;
        private static uint WM_KEYDOWN = 0x100;
        private static uint WM_KEYUP = 0x101;
        private static uint WM_SYSDOWN = 0x104;

        int[] kq = new int[]{
            (int)Keys.A,
            (int)Keys.Q,
            (int)Keys.Escape};
        bool[] bq = new bool[]{
            false, false, false};

        public Keybd(Client cli) {
            this.cli = cli;
            active = false;
            KH_Proc = HookCallback;
            KH_HID = SetHook(KH_Proc);
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private IntPtr SetHook(LowLevelKeyboardProc proc) {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule) {
                return SetWindowsHookEx(WH_KEYBOARD_LL, KH_Proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam) {
            int iKey = Marshal.ReadInt32(lParam);
            bool bSD = (wParam == (IntPtr)WM_SYSDOWN);
            bool bKD = (wParam == (IntPtr)WM_KEYDOWN);
            bool bKU = (wParam == (IntPtr)WM_KEYUP);
            frmMain.head = nCode + " - " + wParam +
                " - " + iKey + "(" + (Keys)iKey + ")";
            if (bSD || bKD || bKU) {
                if (active) {
                    pollQuit(iKey, bSD || bKD);
                    string arg = "KD";
                    if (bKU) arg = "KU";
                    cli.SendLine(arg + ":" + iKey);
                }
            }
            if (active) return (IntPtr)1;
            return CallNextHookEx(KH_HID, nCode, wParam, lParam);
        }
        private void pollQuit(int iKey, bool bKD) {
            for (int a = 0; a < kq.Length; a++) {
                if (kq[a] == iKey)
                    bq[a] = bKD;
            }
            bool quit = true;
            for (int a = 0; a < kq.Length; a++) {
                if (!bq[a]) quit = false;
            }
            if (quit) {
                Dispose();
                System.Diagnostics.Process.
                    GetCurrentProcess().Kill();
            }
        }
        public void Dispose() {
            UnhookWindowsHookEx(KH_HID);
        }
    }
    public class Mouse {
        Timer t;
        Client cli;
        Point center;
        public Mouse(Client cli) {
            this.cli = cli;
            center = Point.Empty;
            t = new Timer(); t.Interval = 1;
            t.Tick += new EventHandler(t_Tick);
        }
        void t_Tick(object sender, EventArgs e) {
            Point pt = Cursor.Position;
            if (pt != center && cli != null) {
                Cursor.Position = center;
                cli.SendLine("MM:" +
                    (pt.X - center.X) + ":" +
                    (pt.Y - center.Y));
            }
        }
        public void setCenter(Point pt) {
            center = pt;
        }
        public void start() {
            t.Start();
        }
        public void stop() {
            t.Stop();
        }
    }
}