using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace fooRate {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
        }
        const string winTitle = "fooRate ";
        Database db = new Database();
        string lastfoo = "";
        private void guLoad_Click(object sender, EventArgs e) {
            int songs = db.Load("the.db");
            guSongsAll.Text = songs + "";
            guSongsDB.Text = songs + "";
        }
        private void guSave_Click(object sender, EventArgs e) {
            int songs = db.Save("the.db");
            guSongsAll.Text = songs + "";
            guSongsDB.Text = songs + "";
        }
        private void guRate_KeyDown(object sender, KeyEventArgs e) {
            int rating = -1;
            if (e.KeyCode == Keys.D0) rating = 0;
            if (e.KeyCode == Keys.D1) rating = 1;
            if (e.KeyCode == Keys.D2) rating = 2;
            if (e.KeyCode == Keys.D3) rating = 3;
            if (e.KeyCode == Keys.D4) rating = 4;
            if (e.KeyCode == Keys.D5) rating = 5;
            if (rating == -1) return;
            string song = getSong();
            if (song == "FAIL") {
                MessageBox.Show("Fuck fuck fuck oh shit fuck");
                return;
            }
            db.Rate(song, rating);
            guRecent.Items.Insert(0,
                rating + " " + song);
            while (guRecent.Items.Count > 5)
                guRecent.Items.RemoveAt(5);
            guSongsAll.Text = db.numSongs() + "";
            lastfoo = "";
        }
        string getSong() {
            try {
                Process proc = Process.GetProcessesByName("foobar2000")[0];
                string ret = proc.MainWindowTitle;
                ret = ret.Substring(guCutSubfix.Text.Length);
                ret = ret.Substring(0, ret.IndexOf
                    ("   [foobar2000 v"));

                int ofs = ret.IndexOf(".zip|");
                if (ofs == -1) return ret;
                string track = ret.Substring(ofs + 5);
                string path = ret.Substring(0, ofs);
                ofs = track.IndexOf(".");
                if (ofs == -1) return ret;

                track = track.Substring(0, ofs);
                return path + ".zip #" + track;
            } catch { }
            return "FAIL";
        }

        private void frmMain_Load(object sender, EventArgs e) {
            guLoad_Click(sender, e);
            Timer t = new Timer();
            t.Interval = 100;
            t.Tick += delegate(object toa, EventArgs tob) {
                string song = getSong();
                if (lastfoo != song) {
                    lastfoo = song;
                    if (song != "FAIL") {
                        this.Text = winTitle +
                            db.getRating(song);
                    } else {
                        this.Text = winTitle + song;
                    }
                }
            };
            t.Start();
        }
    }
    public class Database {
        ArrayList alSongs;
        public Database() {
            alSongs = new ArrayList();
        }
        public int Load(string filename) {
            alSongs = new ArrayList();
            if (!System.IO.File.Exists(filename)) return 0;
            string[] raw = System.IO.File.ReadAllText(
                filename, Encoding.UTF8).Replace("\r",
                "").Trim('\n').Split('\n');
            for (int a = 0; a < raw.Length; a++) {
                Song song = new Song();
                song.rating = Convert.ToInt32(
                    (char)(raw[a][0] - '0'));
                song.file = raw[a].Substring(2);
                alSongs.Add(song);
            }
            return alSongs.Count;
        }
        public int Save(string filename) {
            StringBuilder sb = new StringBuilder();
            for (int a = 0; a < alSongs.Count; a++) {
                Song song = (Song)alSongs[a];
                sb.Append(song.rating + " " +
                    song.file + "\r\n");
            }
            System.IO.File.WriteAllText(filename,
                sb.ToString(), Encoding.UTF8);
            return alSongs.Count;
        }
        public int Rate(string file, int rating) {
            int ret = -1, ofs = -1;
            Song newSong = new Song(file, rating);
            for (int a = 0; a < alSongs.Count; a++) {
                Song oldSong = (Song)alSongs[a];
                if (oldSong.file == newSong.file) {
                    ret = oldSong.rating;
                    ofs = a; break;
                }
            }
            if (ofs == -1 &&
                newSong.rating > 0) {
                alSongs.Add(newSong);
            } else if (ofs >= 0) {
                alSongs.RemoveAt(ofs);
                if (newSong.rating > 0) {
                    alSongs.Insert(ofs, newSong);
                }
            }
            return ret;
        }
        public int getRating(string file) {
            for (int a = 0; a < alSongs.Count; a++) {
                Song song = (Song)alSongs[a];
                if (song.file == file)
                    return song.rating;
            }
            return 0;
        }
        public int numSongs() {
            return alSongs.Count;
        }
    }
    public class Song {
        public int rating;
        public string file;
        public Song(string file, int rating) {
            this.file = file;
            this.rating = rating;
        }
        public Song() {
            this.file = "";
            this.rating = -1;
        }
    }
}
