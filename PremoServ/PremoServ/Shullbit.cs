using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace PremoServ {
    public class Server {
        public bool active;
        public bool auth;

        int port;
        private Socket sck;
        public string user;
        public string pass;
        private string log;
        public Server(int port) {
            active = false;
            this.port = port;
            doLog("Trying to open port");
            sck = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            sck.Bind(new IPEndPoint(IPAddress.Any, port));
            doLog("Port now reserved");
        }
        public void Start() {
            doLog("Starting server");
            sck.Listen(20);
            System.Threading.Thread th = new System.Threading.
                Thread(new System.Threading.ThreadStart(worker));
            th.Start();
        }
        void worker() {
            doLog("Server thread init");
            UPnP.OpenFirewallPort(port);
            doLog("UPnP operation completed");
            active = true;
            while (active) {
                doLog("Awaiting connection");
                auth = false;
                Socket cli = sck.Accept();
                doLog("Connection from " + cli.RemoteEndPoint.ToString());
                //cli.ReceiveTimeout = 1000;
                //cli.SendTimeout = 1000;
                cli.NoDelay = true;
                while (cli.Connected) {
                    string cmd = GetLine(cli);
                    if (!active) break;
                    if (cmd.StartsWith("AUTH")) {
                        string[] cmds = cmd.Split('\t');
                        if (cmds[1] == user &&
                            cmds[2] == pass) {
                            SendLine(cli, "OK");
                            auth = true;
                        }
                        else {
                            SendLine(cli, "FAIL");
                            cli.Disconnect(false);
                        }
                    }
                    else if (auth && cmd != "") {
                        string[] cmds = cmd.Split(':');
                        if (cmds[0] == "KD") {
                            int kc = Convert.ToInt32(cmds[1]);
                            keybd.set(kc, true);
                        }
                        if (cmds[0] == "KU") {
                            int kc = Convert.ToInt32(cmds[1]);
                            keybd.set(kc, false);
                        }
                        if (cmds[0] == "MM") {
                            int x = Convert.ToInt32(cmds[1]);
                            int y = Convert.ToInt32(cmds[2]);
                            Point pos = Cursor.Position;
                            pos.X += x; pos.Y += y;
                            Cursor.Position = pos;
                        }
                        if (cmds[0] == "MD" ||
                            cmds[0] == "MU") {
                            mouse.exec(cmd);
                        }
                    }
                }
                cli.Close();
            }
            active = false;
        }
        private string GetLine(Socket sck) {
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
        private void SendLine(Socket sck, string str) {
            sck.Send(Encoding.ASCII.GetBytes(str + "\n"));
        }
        void doLog(string str) {
            log = str + "\n" + log;
        }
        public string getLog() {
            string ret = log;
            log = ""; return ret;
        }
    }
    public class keybd {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        private static uint KE_KEYDOWN = 0x00;
        private static uint KE_KEYUP = 0x02;
        public static void set(int key, bool down) {
            //MessageBox.Show(key + " - " + down); return;
            uint ust = KE_KEYDOWN;
            if (!down) ust = KE_KEYUP;
            keybd_event((byte)key, 0, ust, 0);
        }
    }
    public class mouse {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags,
            uint dx, uint dy, uint cButtons, IntPtr dwExtraInfo);
        public const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        public const uint MOUSEEVENTF_LEFTUP = 0x04;
        public const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const uint MOUSEEVENTF_RIGHTUP = 0x10;

        public static void exec(string ev) {
            uint act = 0;
            if (ev == "MD:L") act = MOUSEEVENTF_LEFTDOWN;
            if (ev == "MU:L") act = MOUSEEVENTF_LEFTUP;
            if (ev == "MD:R") act = MOUSEEVENTF_RIGHTDOWN;
            if (ev == "MU:R") act = MOUSEEVENTF_RIGHTUP;
            mouse_event(act, (uint)0, (uint)0, (uint)0, (IntPtr)0);
        }
    }
}