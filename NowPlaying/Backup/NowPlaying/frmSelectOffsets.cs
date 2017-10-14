using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NowPlaying {
    public partial class frmSelectOffsets : Form {
        public frmSelectOffsets(Memory mem, int[] title, int[] artist, int[] album) {
            InitializeComponent();
            this.mem = mem;
            this.title = title;
            this.artis = artist;
            this.album = album;
        }
        Memory mem;
        int[] title;
        int[] artis;
        int[] album;
        public int ititle = 0;
        public int iartis = 0;
        public int ialbum = 0;
        public bool spotify;

        private void label3_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private void frmSelectOffsets_Load(object sender, EventArgs e) {
            this.Icon = frmMain.ico;
            cmdRefresh_Click(sender, e);
            cmdOK.Focus();
        }
        private void cmdRefresh_Click(object sender, EventArgs e) {
            listTitle.Items.Clear();
            listTitle.Items.Add("(none)");
            for (int a = 0; a < title.Length; a++)
                listTitle.Items.Add("0x" + title[a].ToString("X") + ": " +
                    mem.Read(title[a], Program.enc));

            listArtist.Items.Clear();
            listArtist.Items.Add("(none)");
            for (int a = 0; a < artis.Length; a++)
                listArtist.Items.Add("0x" + artis[a].ToString("X") + ": " +
                    mem.Read(artis[a], Program.enc));

            listAlbum.Items.Clear();
            listAlbum.Items.Add("(none)");
            for (int a = 0; a < album.Length; a++)
                listAlbum.Items.Add("0x" + album[a].ToString("X") + ": " +
                    mem.Read(album[a], Program.enc));
        }

        private void tRefresh_Tick(object sender, EventArgs e) {
            if (title.Length > ititle && ititle >= 0)
                lbTitle.Text = "" + mem.Read(title[ititle], Program.enc);
            else lbTitle.Text = "(no value)";

            if (artis.Length > iartis && iartis >= 0)
                lbArtist.Text = "" + mem.Read(artis[iartis], Program.enc);
            else lbArtist.Text = "(no value)";

            if (album.Length > ialbum && ialbum >= 0)
                lbAlbum.Text = "" + mem.Read(album[ialbum], Program.enc);
            else lbAlbum.Text = "(no value)";
        }
        private void cmdOK_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void listTitle_SelectedIndexChanged(object sender, EventArgs e) {
            ititle = listTitle.SelectedIndex - 1;
        }
        private void listArtist_SelectedIndexChanged(object sender, EventArgs e) {
            iartis = listArtist.SelectedIndex - 1;
        }
        private void listAlbum_SelectedIndexChanged(object sender, EventArgs e) {
            ialbum = listAlbum.SelectedIndex - 1;
        }
    }
}
