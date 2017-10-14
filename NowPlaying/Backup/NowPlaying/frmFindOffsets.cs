using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace NowPlaying {
    public partial class frmFindOffsets : Form {
        public frmFindOffsets(Memory mem) {
            InitializeComponent();
            this.mem = mem;
        }
        Memory mem;
        public int[] title;
        public int[] artis;
        public int[] album;
        public bool bSearched = false;

        private void cmdGo_Click(object sender, EventArgs e) {
            cmdGo.Enabled = false;
            txTitle.Enabled = false;
            txAlbum.Enabled = false;
            txArtist.Enabled = false;

            lbState.Text = "Searching for Title ...";
            Application.DoEvents();
            if (txTitle.Text != "")
                title = Find(txTitle.Text, Program.enc);
            else title = new int[0];

            lbState.Text = "Searching for Artist ...";
            Application.DoEvents();
            if (txArtist.Text != "")
                artis = Find(txArtist.Text, Program.enc);
            else artis = new int[0];

            lbState.Text = "Searching for Album ...";
            Application.DoEvents();
            if (txAlbum.Text != "")
                album = Find(txAlbum.Text, Program.enc);
            else album = new int[0];

            bSearched = true;
            this.Close();
        }

        public int[] Find(string var, Encoding enc) {
            byte[] scan = enc.GetBytes(var);
            int ofs = 0x400000;
            int end = 0x7FFFFFFF;
            int cnk = 0x100000;
            int myofs = ofs;
            Scanner[] oscn = new Scanner[4];
            int skw = (end - ofs) / oscn.Length;
            for (int a = 0; a < oscn.Length; a++) {
                int myend = myofs + skw - 1;
                if (a == oscn.Length - 1)
                    myend = end - ofs;
                oscn[a] = new Scanner();
                oscn[a].ScanMem(mem, scan,
                    myofs, myend, cnk);
                myofs += skw;
            }
            prg.Maximum = end - ofs;
            while (true) {
                int iProg = 0;
                bool bScanning = false;
                for (int a = 0; a < oscn.Length; a++) {
                    if (!oscn[a].done) bScanning = true;
                    if (oscn[a].prg > 0) iProg += oscn[a].prg;
                }
                prg.Value = iProg;
                if (!bScanning) break;
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }
            int retCnt = 0;
            for (int a = 0; a < oscn.Length; a++)
                retCnt += oscn[a].ret.Length;
            int[] ret = new int[retCnt];
            retCnt = 0;
            for (int a = 0; a < oscn.Length; a++) {
                oscn[a].ret.CopyTo(ret, retCnt);
                retCnt += oscn[a].ret.Length;
            }
            return ret;
        }

        private void label4_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void txTitle_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) cmdGo_Click(sender, e);
        }
        private void txArtist_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) cmdGo_Click(sender, e);
        }
        private void txAlbum_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) cmdGo_Click(sender, e);
        }

        private void frmFindOffsets_Load(object sender, EventArgs e) {
            this.Icon = frmMain.ico;
        }
    }
    public class Scanner {
        Memory mem; byte[] scan;
        int ofs; int end; int cnk;
        public int prg = -1;
        public int[] ret;
        public bool done;
        public void ScanMem(Memory mem, byte[] scan, int ofs, int end, int cnk) {
            this.mem = mem; this.scan = scan;
            this.ofs = ofs; this.end = end;
            this.cnk = cnk;
            System.Threading.Thread th = new System.Threading.Thread(
                new System.Threading.ThreadStart(doitfaggot));
            th.Start();
        }
        private void doitfaggot() {
            ArrayList al = new ArrayList();
            for (int a = ofs; a < end; a += cnk) {
                byte[] vb = mem.Read(a, cnk + scan.Length);
                for (int b = a; b < a + cnk; b++)
                    if (Compare(vb, scan, b - a))
                        al.Add(b); prg = a - ofs;
            }
            ret = new int[al.Count];
            for (int a = 0; a < ret.Length; a++)
                ret[a] = (int)al[a]; done = true;
        }
        private bool Compare(byte[] ba, byte[] bb, int ofs) {
            if (ba.Length < bb.Length + ofs) return false;
            for (int a = 0; a < bb.Length; a++)
                if (ba[a + ofs] != bb[a]) return false;
            return true;
        }
    }
}
