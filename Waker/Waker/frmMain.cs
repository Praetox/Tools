using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Waker
{
    public partial class frmMain : Form
    {
        private MP3 Song = new MP3();

        public frmMain()
        {
            InitializeComponent();
        }

        private void tMain_Tick(object sender, EventArgs e)
        {
            lbt.Text = System.DateTime.Now.ToLongTimeString();
            if (!chkEnabled.Checked) return;
            if (System.DateTime.Now.ToShortTimeString() != txtTime.Text) return;
            Song.Close(); Song.Open(txtFile.Text); Song.Play(true);
            chkEnabled.Checked = false;
            cnt.Visible = true; Application.DoEvents();
            cmdSNZ.Focus();
        }
        private void cmdPlay_Click(object sender, EventArgs e)
        {
            Song.Close(); Song.Open(txtFile.Text); Song.Play(true);
        }
        private void cmdStop_Click(object sender, EventArgs e)
        {
            Song.Close();
        }
        private void cmdSNZ_Click(object sender, EventArgs e)
        {
            chkEnabled.Checked = false; Song.Close(); tSnooze.Enabled = true;
        }
        private void tSnooze_Tick(object sender, EventArgs e)
        {
            chkEnabled.Checked = true; tSnooze.Enabled = false;
            txtTime.Text = System.DateTime.Now.ToShortTimeString();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
            this.Location = new Point(0, 0);
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //tx.Text = "Praetox Screensaver";
            cnt.Left = (this.Width - cnt.Width) / 2;
            cnt.Top = (this.Height - cnt.Height) / 2;
            lbt.Left = (this.Width - lbt.Width) / 2;
            lbt.Top = cnt.Top - lbt.Height;
            txtTime.Text = System.IO.File.
                ReadAllText("c:\\waker.txt");
            this.Show();
            for (double a = 0; a < 1; a += 0.01)
            {
                this.Opacity = a;
                Application.DoEvents();
            }
            this.Opacity = 1;
        }

        private void cmdHide_Click(object sender, EventArgs e)
        {
            cnt.Visible = false;
            this.Focus();
        }

        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            cnt.Visible = true;
        }

        private void tMover_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            lbt.Left = random.Next(0, (this.Width - lbt.Width));
            lbt.Top = random.Next(0, (this.Height - lbt.Height));
            
        }
    }

    public class MP3
    {
        private string Pcommand; private bool isOpen;

        [DllImport("winmm.dll")]
        private static extern long mciSendString(
            string strCommand,          // The command to execute
            StringBuilder strReturn,    // The returned string (if any)
            int iReturnLength,          // The bitcount of the returned string
            IntPtr hwndCallback);       // Callback value of spec. handle
        public MP3()
        {
        }

        public void Close()
        {
            Pcommand = "close Waker";
            mciSendString(Pcommand, null, 0, IntPtr.Zero);
            isOpen = false;
        }

        public void Open(string sFileName)
        {
            Pcommand = "open \"" + sFileName + "\" type mpegvideo alias Waker";
            mciSendString(Pcommand, null, 0, IntPtr.Zero);
            isOpen = true;
        }

        public void Play(bool loop)
        {
            if (isOpen)
            {
                Pcommand = "play Waker";
                if (loop) Pcommand += " REPEAT";
                mciSendString(Pcommand, null, 0, IntPtr.Zero);
            }
        }
    }
}