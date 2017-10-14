using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pImgSort
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        

        private void Keeplist_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(
                "mkdir pack01\r\n" +
                "mkdir pack02\r\n" +
                "mkdir pack03\r\n" +
                Keeplist.Text);
            Keeplist.Text = "";
        }

        private void tHotkeys_Tick(object sender, EventArgs e)
        {
            if (GetAsyncKeyState(Keys.D1) == -32767) cpFile("pack01");
            if (GetAsyncKeyState(Keys.D2) == -32767) cpFile("pack02");
            if (GetAsyncKeyState(Keys.D3) == -32767) cpFile("pack03");
            if (GetAsyncKeyState(Keys.Q) == -32767)
            {
                string txt = Keeplist.Text;
                if (txt.Length > 0)
                {
                    string strDelim = " - Windows ";
                    if (GetActiveWindowText().Contains(strDelim))
                    {
                        if (txt.Substring(txt.IndexOf("\r\n")).Length > 2)
                        {
                            txt = txt.Substring(txt.IndexOf("\r\n") + 2);
                            Keeplist.Text = txt;
                        }
                        else Keeplist.Text = "";
                        System.Media.SystemSounds.Exclamation.Play();
                    }
                }
                else System.Media.SystemSounds.Hand.Play();
            }
        }
        private void cpFile(string str)
        {
            string strDelim = " - Windows ";
            string txt = GetActiveWindowText();
            if (txt.Contains(strDelim))
            {
                txt = txt.Substring(0, txt.IndexOf(strDelim));
                Keeplist.Text = "move \"" +
                    txt + "\" \"" + str +
                    "\\\"\r\n" + Keeplist.Text;
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private String GetActiveWindowText()
        {
            StringBuilder buf = new StringBuilder(255);
            IntPtr hWnd = GetForegroundWindow();
            if (GetWindowText(hWnd, buf, 255) > 0)
                return buf.ToString();
            return "";
        }
    }
}
