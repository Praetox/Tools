using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections;

namespace NowPlaying {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
        }

        #region Api
        [DllImport("user32.dll")]
        extern static int GetWindowText(int hWnd, StringBuilder text, int count);
        [DllImport("User32.dll")]
        extern static int ShowWindow(System.IntPtr hWnd, short cmdShow);

        private const Int32 LWA_COLORKEY = 0x1;
        private const Int32 LWA_ALPHA = 0x2;
        private const Int32 WS_EX_LAYERED = 0x00080000;

        public struct BlendF {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        public const byte AC_SRC_OVER = 0x0;
        public const byte AC_SRC_ALPHA = 0x1;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BlendF pblend, Int32 dwFlags);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X,
           int Y, int cx, int cy, uint uFlags);
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        // From winuser.h
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOZORDER = 0x0004;
        const UInt32 SWP_NOREDRAW = 0x0008;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        const UInt32 SWP_FRAMECHANGED = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
        const UInt32 SWP_SHOWWINDOW = 0x0040;
        const UInt32 SWP_HIDEWINDOW = 0x0080;
        const UInt32 SWP_NOCOPYBITS = 0x0100;
        const UInt32 SWP_NOOWNERZORDER = 0x0200;  /* Don't do owner Z ordering */
        const UInt32 SWP_NOSENDCHANGING = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */
        const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        private void SetBitmap(Bitmap img, byte opacity) {
            IntPtr screenDc = GetDC(IntPtr.Zero);
            IntPtr memDc = CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try {
                hBitmap = img.GetHbitmap(Color.FromArgb(0));
                oldBitmap = SelectObject(memDc, hBitmap);

                Size size = new Size(img.Width, img.Height);
                Point pointSource = new Point(0, 0);
                Point topPos = new Point(Left, Top);
                BlendF blend = new BlendF();
                blend.BlendOp = AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = opacity;
                blend.AlphaFormat = AC_SRC_ALPHA;

                UpdateLayeredWindow(this.Handle, screenDc, ref topPos, ref size,
                    memDc, ref pointSource, 0, ref blend, LWA_ALPHA);
            }
            finally {
                ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero) {
                    SelectObject(memDc, oldBitmap);
                    DeleteObject(hBitmap);
                }
                DeleteDC(memDc);
            }
        }
        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_LAYERED;
                return cp;
            }
        }
        #endregion
        #region Skin
        Point lcTitle = Point.Empty;
        Point lcArtis = Point.Empty;
        Point lcAlbum = Point.Empty;
        Point lcSyncd = Point.Empty;
        int szTitle = 0;
        int szArtis = 0;
        int szAlbum = 0;
        Color coTitle = Color.Black;
        Color coArtis = Color.Black;
        Color coAlbum = Color.Black;
        Color csTitle = Color.Black;
        Color csArtis = Color.Black;
        Color csAlbum = Color.Black;
        string fnTitle = "";
        string fnArtis = "";
        string fnAlbum = "";
        Point ptMouseOfs = Point.Empty;
        Bitmap bmMain;
        Bitmap bmSync1;
        Bitmap bmSync2;
        Bitmap bmSync3;
        Bitmap bmSync;
        byte opacity = 0;
        bool doVisible = true;
        Timer tHideme = new Timer();
        #endregion
        #region Core
        NotifyIcon ni;
        public static Icon ico;
        public static Icon ico1;
        public static Icon ico2;
        public static Icon ico3;
        ContextMenu rcMenu;
        bool AlwaysOnTop = false;
        bool ShowOnChange = true;
        //string dom = "http://yuki.praetox.com/sig/make.php";
        string dom = "http://192.168.0.100/sig/make.php";
        Memory mem = new Memory();
        int[] adr = new int[0];
        string mask = "";
        string proc = "";
        #endregion
        #region Song
        bool spotify;
        string sOFile = "";
        int ptrTitle = 0;
        int ptrArtis = 0;
        int ptrAlbum = 0;
        string usr = "";
        string pwd = "";
        string sTitle = "";
        string sArtis = "";
        string sAlbum = "";
        #endregion

        int FindNextPar(string str, int from) {
            int junk = str.IndexOf("%junk%", from);
            int title = str.IndexOf("%title%", from);
            int artis = str.IndexOf("%artist%", from);
            int album = str.IndexOf("%album%", from);
            int track = str.IndexOf("%trackno%", from);
            if (junk < 0) junk = 9001;
            if (title < 0) title = 9001;
            if (artis < 0) artis = 9001;
            if (album < 0) album = 9001;
            if (track < 0) track = 9001;
            int min = MathMin(junk,
                title, artis, album, track);
            if (min == 9001) min = -1;
            return min;
        }
        int MathMin(params int[] vars) {
            int ret = vars[0];
            for (int a = 1; a < vars.Length; a++)
                if (vars[a] < ret) ret = vars[a];
            return ret;
        }
        void tState_Tick(object sender, EventArgs e) {
            string snTrack = "";
            string snTitle = "";
            string snArtis = "";
            string snAlbum = "";
            if (mask != "") {
                int ofs = 0;
                Process[] prc = Process.GetProcessesByName(proc);
                if (prc.Length == 0) this.Close();
                string wts = prc[0].MainWindowTitle;
                string msk = mask;
                try {
                    if (!msk.StartsWith("%")) {
                        int cutat = FindNextPar(msk, 0);
                        msk = msk.Substring(cutat);
                        wts = wts.Substring(cutat);
                    }
                    while (true) {
                        int nextp = msk.IndexOf("%", ofs + 1);
                        string vartyp = msk.Substring(1, nextp - 1);
                        string cutat = msk.Substring(nextp + 1);
                        int nextpar = FindNextPar(msk, 1);
                        if (nextpar != -1) msk = msk
                            .Substring(nextpar);
                        int cutnextpar = FindNextPar(cutat, 0);
                        if (cutnextpar != -1) cutat = cutat
                            .Substring(0, cutnextpar);
                        int icutat = wts.IndexOf(cutat);
                        string var = wts;
                        if (icutat > 0) {
                            var = wts.Substring(0, icutat);
                            wts = wts.Substring(icutat + cutat.Length);
                        }
                        if (vartyp == "title") snTitle = var;
                        if (vartyp == "trackno") snTrack = var;
                        if (vartyp == "artist") snArtis = var;
                        if (vartyp == "album") snAlbum = var;
                        if (nextpar == -1) break;
                    }
                    //if (spotify) snAlbum = B62(SongID());
                }
                catch { }
            }
            else {
                try {
                    if (ptrTitle > 0) snTitle = mem.Read(ptrTitle, Program.enc);
                    if (ptrArtis > 0) snArtis = mem.Read(ptrArtis, Program.enc);
                    if (ptrAlbum > 0) snAlbum = mem.Read(ptrAlbum, Program.enc);
                    if (spotify) {
                        int ofs = snArtis.IndexOf(" – ");
                        snTitle = snArtis.Substring(0, ofs);
                        snArtis = snArtis.Substring(ofs + 3);
                    }
                }
                catch { this.Close(); }
            }
            snTrack = snTrack.Trim();
            snTitle = snTitle.Trim();
            snArtis = snArtis.Trim();
            snAlbum = snAlbum.Trim();
            if (snTrack != "") snTitle =
                snTrack + ". " + snTitle;
            if (sTitle != snTitle ||
                sArtis != snArtis ||
                sAlbum != snAlbum) {

                bmSync = bmSync1;
                ni.Icon = ico1;
                tUpload.Stop();
                tUpload.Start();

                sTitle = snTitle;
                sArtis = snArtis;
                sAlbum = snAlbum;
                doDraw(true);
            }
        }
        Color ParseColor(string hex) {
            int r = Convert.ToInt32(hex.Substring(0, 2), 16);
            int g = Convert.ToInt32(hex.Substring(2, 2), 16);
            int b = Convert.ToInt32(hex.Substring(4, 2), 16);
            int a = Convert.ToInt32(hex.Substring(6, 2), 16);
            return Color.FromArgb(a, r, g, b);
        }
        void frmMain_Load(object sender, EventArgs e) {
            ico = this.Icon; loadSkin();
            this.Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Width - bmMain.Width,
                Screen.PrimaryScreen.WorkingArea.Height - bmMain.Height);

            frmSelectAccount fa = new frmSelectAccount();
            fa.ShowDialog(); usr = fa.usr; pwd = fa.pwd;
            if (string.IsNullOrEmpty(usr)) DieNow();

            frmSelectPlayer fb = new frmSelectPlayer();
            fb.ShowDialog(); string app = fb.appname;
            if (string.IsNullOrEmpty(app)) DieNow();
            spotify = (fb.appname == "spotify");

            if (fb.appmask == "") {
                mem.Open(Process.GetProcessesByName(app)[0]);
                frmFindOffsets fc = new frmFindOffsets(mem);
                fc.ShowDialog(); if (!fc.bSearched) DieNow();

                frmSelectOffsets fd = new frmSelectOffsets(
                    mem, fc.title, fc.artis, fc.album);
                fd.ShowDialog();
                if (fc.title.Length > fd.ititle && fd.ititle >= 0) ptrTitle = fc.title[fd.ititle];
                if (fc.artis.Length > fd.iartis && fd.iartis >= 0) ptrArtis = fc.artis[fd.iartis];
                if (fc.album.Length > fd.ialbum && fd.ialbum >= 0) ptrAlbum = fc.album[fd.ialbum];
            }
            else {
                proc = fb.appname;
                mask = fb.appmask;
            }

            if (spotify) {
                string sRoot = Environment.GetEnvironmentVariable
                    ("appdata") + @"\Spotify\Users";
                string[] sPaths = System.IO.Directory.GetDirectories(sRoot);
                long ltLastMod = 0; int iLastMod = 0;
                for (int a = 0; a < sPaths.Length; a++) {
                    long ltThisMod = 0;
                    string sFile = sPaths[a] + @"\guistate";
                    if (System.IO.File.Exists(sFile))
                        ltThisMod = System.IO.File.
                            GetLastWriteTime(sFile).Ticks;
                    if (ltThisMod > ltLastMod)
                        iLastMod = a;
                }
                sOFile = sPaths[iLastMod] + @"\guistate";
            }

            ico1 = new Icon(getEmbeddedStream("Resources.Sennheiser_1.ico"));
            ico2 = new Icon(getEmbeddedStream("Resources.Sennheiser_2.ico"));
            ico3 = new Icon(getEmbeddedStream("Resources.Sennheiser_3.ico"));
            ni = new NotifyIcon(); ni.Visible = true; ni.Icon = ico1;
            ni.MouseClick += new MouseEventHandler(ni_MouseClick);
            tState_Tick(new object(), new EventArgs()); tState.Start();

            Timer tdoVisible = new Timer(); tdoVisible.Interval = 10;
            tdoVisible.Tick += new EventHandler(tdoVisible_Tick);
            tdoVisible.Start();

            tHideme.Tick += delegate(object lol, EventArgs wut) {
                tHideme.Stop(); if (!AlwaysOnTop) doVisible = false;
            };

            MenuItem[] mi = new MenuItem[4];
            mi[0] = new MenuItem("Always on top", new EventHandler(miOnTop));
            mi[1] = new MenuItem("Show at song change", new EventHandler(miAutoshow));
            mi[2] = new MenuItem("Reload skin", new EventHandler(miReSkin));
            mi[3] = new MenuItem("Exit", new EventHandler(miExit));
            mi[1].Checked = true; rcMenu = new ContextMenu(mi);
            ni.ContextMenu = rcMenu; draw();
        }
        void miOnTop(object sender, EventArgs e) {
            AlwaysOnTop = !rcMenu.MenuItems[0].Checked;
            rcMenu.MenuItems[0].Checked = AlwaysOnTop;
            doVisible = true; tHideme.Stop(); draw();
            this.TopMost = (AlwaysOnTop || ShowOnChange);
        }
        void miAutoshow(object sender, EventArgs e) {
            ShowOnChange = !rcMenu.MenuItems[1].Checked;
            rcMenu.MenuItems[1].Checked = ShowOnChange;
            if (ShowOnChange) tHideme.Start();
            else { tHideme.Stop(); doVisible = true; }
            this.TopMost = (AlwaysOnTop || ShowOnChange);
        }
        void miReSkin(object sender, EventArgs e) {
            loadSkin(); draw();
        }
        void miExit(object sender, EventArgs e) {
            this.Close();
        }

        void DieNow() {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        void ni_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                doVisible = !doVisible; tHideme.Stop();
            }
        }
        void tUpload_Tick(object sender, EventArgs e) {
            tUpload.Stop(); ni.Icon = ico2;
            bmSync = bmSync2; doDraw(false);
            Application.DoEvents(); WebReq WReq = new WebReq();
            string sMPBound = "----------" + WReq.RandomChars(22);
            string sData = "--" + sMPBound + "\r\n" +
                "Content-Disposition: form-data; name=\"usr\"" + "\r\n" +
                "\r\n" + usr + "\r\n" + "--" + sMPBound + "\r\n" +
                "Content-Disposition: form-data; name=\"pwd\"" + "\r\n" +
                "\r\n" + pwd + "\r\n" + "--" + sMPBound + "\r\n" +
                "Content-Disposition: form-data; name=\"title\"" + "\r\n" +
                "\r\n" + sTitle + "\r\n" + "--" + sMPBound + "\r\n" +
                "Content-Disposition: form-data; name=\"artist\"" + "\r\n" +
                "\r\n" + sArtis + "\r\n" + "--" + sMPBound + "\r\n" +
                "Content-Disposition: form-data; name=\"album\"" + "\r\n" +
                "\r\n" + sAlbum + "\r\n" + "--" + sMPBound + "--" + "\r\n";
            byte[] bData = Encoding.UTF8.GetBytes(sData);
            WReq.Request(new Uri(dom), new
                System.Net.WebHeaderCollection(),
                bData, sMPBound, 3, "", true);
            while (!WReq.isReady) {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            bmSync = bmSync3;
            ni.Icon = ico3;
            doDraw(false);
        }
        string B62(string sVar) {
            char[] v = ("0123456789" +
                "abcdefghijklmnopqrstuvwxyz" +
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
                .ToCharArray();
            BigInteger bi = new BigInteger(sVar, 16);
            BigInteger mod = new BigInteger(62);
            string sRet = "";
            while (bi.min(1).ToString() == "1") {
                int r = (bi % 62).IntValue();
                sRet = v[r] + sRet;
                bi = bi / 62;
            }
            return sRet;
        }
        string SongID() {
            string s = System.IO.File.ReadAllText(sOFile);
            s = s.Split(new string[] { "playing_track_id" }, StringSplitOptions.None)[1];
            return s.Split('\"')[2];
        }
        
        void frmMain_Resize(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Minimized) Hide();
        }
        void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
            tUpload.Stop(); tState.Stop();
            this.Hide(); Application.DoEvents();
            sTitle = ""; sArtis = ""; sAlbum = "";
            tUpload_Tick(sender, e); //Blank sig
            ni.Visible = false; Application.DoEvents();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        System.IO.Stream getEmbeddedStream(string filename) {
            System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
            string file = a.FullName.Substring(0, a.FullName.IndexOf(",")) + "." + filename;
            return a.GetManifestResourceStream(file);
        }

        void loadSkin() {
            if (bmMain != null) bmMain.Dispose();
            if (bmSync1 != null) bmSync1.Dispose();
            if (bmSync2 != null) bmSync2.Dispose();
            if (bmSync3 != null) bmSync3.Dispose();
            if (bmSync != null) bmSync.Dispose();
            if (System.IO.File.Exists("Skin\\Main.png")) bmMain = new Bitmap("Skin\\Main.png");
            if (System.IO.File.Exists("Skin\\Sync1.png")) bmSync1 = new Bitmap("Skin\\Sync1.png");
            if (System.IO.File.Exists("Skin\\Sync2.png")) bmSync2 = new Bitmap("Skin\\Sync2.png");
            if (System.IO.File.Exists("Skin\\Sync3.png")) bmSync3 = new Bitmap("Skin\\Sync3.png");
            bmSync = bmSync3;
            if (bmMain == null) {
                MessageBox.Show("Could not load skin!", "Fatal error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            string[] conf = System.IO.File.ReadAllText("Skin\\Conf.txt")
                .Replace("\r", "").Trim('\n').Split('\n');
            string[] cfTitle = conf[0].Split('.');
            string[] cfArtis = conf[1].Split('.');
            string[] cfAlbum = conf[2].Split('.');
            string[] cfSyncd = conf[3].Split('.');
            lcTitle = new Point(Convert.ToInt32(cfTitle[0]), Convert.ToInt32(cfTitle[1]));
            lcArtis = new Point(Convert.ToInt32(cfArtis[0]), Convert.ToInt32(cfArtis[1]));
            lcAlbum = new Point(Convert.ToInt32(cfAlbum[0]), Convert.ToInt32(cfAlbum[1]));
            lcSyncd = new Point(Convert.ToInt32(cfSyncd[0]), Convert.ToInt32(cfSyncd[1]));
            coTitle = ParseColor(cfTitle[2]); csTitle = ParseColor(cfTitle[3]);
            coArtis = ParseColor(cfArtis[2]); csArtis = ParseColor(cfArtis[3]);
            coAlbum = ParseColor(cfAlbum[2]); csAlbum = ParseColor(cfAlbum[3]);
            szTitle = Convert.ToInt32(cfTitle[4]);
            szArtis = Convert.ToInt32(cfArtis[4]);
            szAlbum = Convert.ToInt32(cfAlbum[4]);
            tHideme.Interval = Convert.ToInt32(cfSyncd[2]);
        }
        void draw() {
            Bitmap bo = new Bitmap(bmMain.Width, bmMain.Height);
            using (Graphics g = Graphics.FromImage(bo)) {
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.DrawImageUnscaled(bmMain, Point.Empty);
                drawText(g, sTitle, lcTitle, fnTitle, szTitle, coTitle, csTitle);
                drawText(g, sArtis, lcArtis, fnArtis, szArtis, coArtis, csArtis);
                drawText(g, sAlbum, lcAlbum, fnAlbum, szAlbum, coAlbum, csAlbum);
                if (bmSync != null) g.DrawImageUnscaled(bmSync, lcSyncd);
            }
            SetBitmap(bo, opacity);
        }
        void drawText(Graphics g, string str, Point pos, string fn, int sz, Color c1, Color c2) {
            g.DrawString(str, new Font(fn, sz), new System.Drawing.SolidBrush(c2), (float)pos.X + 1f, (float)pos.Y + 1f);
            g.DrawString(str, new Font(fn, sz), new System.Drawing.SolidBrush(c1), (float)pos.X + 0f, (float)pos.Y + 0f);
        }
        void frmMain_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) ptMouseOfs = e.Location;
            if (e.Button == MouseButtons.Right) rcMenu.Show(this, e.Location);
        }
        void frmMain_MouseMove(object sender, MouseEventArgs e) {
            if (ShowOnChange) {
                tHideme.Stop(); tHideme.Start();
            }
            if (e.Button != MouseButtons.Left) return;
            Point ptLoc = this.Location;
            ptLoc.X += e.X - ptMouseOfs.X;
            ptLoc.Y += e.Y - ptMouseOfs.Y;
            this.Location = ptLoc;
        }
        void doDraw(bool songChange) {
            if (!ShowOnChange ||
                AlwaysOnTop) draw();
            else {
                if (songChange) {
                    if (opacity < 0xff)
                        doVisible = true;
                    else {
                        draw();
                        tHideme.Stop();
                        tHideme.Start();
                    }
                }
            }
        }
        void tdoVisible_Tick(object sender, EventArgs e) {
            if (doVisible && opacity == 0x0) {
                tHideme.Stop();
                if (AlwaysOnTop || ShowOnChange)
                    this.TopMost = true;
                ShowWindow(this.Handle, 4);
            }
            if (doVisible && opacity < 0xff) {
                opacity += 0x11; draw();
                if (opacity == 0xff) 
                    if (ShowOnChange && !AlwaysOnTop)
                        tHideme.Start();
            }
            if (!doVisible && opacity > 0x0) {
                opacity -= 0x11; draw();
                if (opacity == 0x0) {
                    this.TopMost = false;
                    this.Hide();
                }
            }
        }
    }
    public class Memory {
        public const uint PROCESS_VM_READ = (0x0010);

        [DllImport("kernel32.dll")] //Open
        public static extern IntPtr OpenProcess(
            UInt32 dwDesiredAccess,
            Int32 bInheritHandle,
            UInt32 dwProcessId);

        [DllImport("kernel32.dll")] //Close
        public static extern Int32 CloseHandle(
            IntPtr hObject);

        [DllImport("kernel32.dll")] //Read
        public static extern Int32 ReadProcessMemory(
            IntPtr hProcess, IntPtr lpBaseAddress,
            [In, Out] byte[] buffer, UInt32 size,
            out IntPtr lpNumberOfBytesRead);

        private IntPtr hProcess = IntPtr.Zero;
        public void Open(Process proc) {
            hProcess = OpenProcess(
                PROCESS_VM_READ,
                1, (UInt32)proc.Id);
        }
        public void Close() {
            if (hProcess != IntPtr.Zero)
                CloseHandle(hProcess);
            hProcess = IntPtr.Zero;
        }
        public byte[] Read(int ofs, int len) {
            byte[] ret = new byte[len];
            IntPtr ipRead = IntPtr.Zero;
            ReadProcessMemory(hProcess,
                (IntPtr)ofs, ret, //derp
                (uint)len, out ipRead);
            return ret;
        }
        public string Read(int ofs, Encoding enc) {
            byte[] var = Read(ofs, 256);
            string svar = enc.GetString(var);
            int nullTerm = svar.IndexOf("\0");
            if (nullTerm != -1) svar =
                svar.Substring(0, nullTerm);
            return svar;
        }
    }
}
