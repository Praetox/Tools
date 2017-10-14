using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoukutSuite {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);

        Timer stats;
        Doukutsu dou;
        Vector vcSpeed;
        Bitmap map;

        void initDebug() {
            int ofs = 0x49E640;
            Memory m = new Memory(System.Diagnostics.
                Process.GetProcessesByName("doukutsu")[0]);

            string clip = "";
            for (int a = 0; a < 20; a++) {
                int pos = a + ofs;
                clip += pos.ToString("X")
                    + " :: " + "\r\n";
            }
            Clipboard.Clear();
            Clipboard.SetText(clip);

            Timer t = new Timer(); t.Interval = 100;
            t.Tick += delegate(object oa, EventArgs ob) {
                string str = "";
                for (int a = 0; a < 20; a++) {
                    int pos = a + ofs;
                    str += pos.ToString("X") + " :: "
                        + m.RInt(pos) + "\r\n";
                }
            };
            t.Start();
        }
        private void frmMain_Load(object sender, EventArgs e) {
            Mapper_Ofs.Text = "650x138";

            dou = new Doukutsu();
            vcSpeed = new Vector(
                .9f, this.BackColor,
                this.ForeColor);

            guVcSpeed.Image = new Bitmap(guVcSpeed.Width, guVcSpeed.Height);
            using (Graphics g = Graphics.FromImage(guVcSpeed.Image)) {
                g.FillRectangle(new SolidBrush(this.BackColor),
                    new Rectangle(Point.Empty, guVcSpeed.Size));
            }

            stats = new Timer(); stats.Interval = 10;
            stats.Tick += new EventHandler(stats_Tick);
            stats.Start(); this.Left = 4;

            Timer hot = new Timer(); hot.Interval = 1;
            hot.Tick += new EventHandler(hot_Tick);
            hot.Start();
        }
        void hot_Tick(object sender, EventArgs e) {
            int posX = 0, posY = 0;
            if (GetAsyncKeyState(Keys.R) == -32767) posX = -dou.szTile.X;
            if (GetAsyncKeyState(Keys.Y) == -32767) posX = dou.szTile.X;
            if (GetAsyncKeyState(Keys.D5) == -32767) posY = -dou.szTile.Y;
            if (GetAsyncKeyState(Keys.T) == -32767) posY = dou.szTile.Y;
            bool cam = (GetAsyncKeyState(Keys.LControlKey) < 0);
            if (GetAsyncKeyState(Keys.LShiftKey) < 0) {
                if (cam) {
                    //Background moves at 1/8th
                    //speed of the foreground
                    Point Step = MapperGetStepsize();
                    posX *= Step.X; posY *= Step.Y;
                } else {
                    Point scr = dou.szScreen;
                    posX *= scr.X; posY *= scr.Y;
                }
            }
            if (Math.Abs(posX) > 1 ||
                Math.Abs(posY) > 1) {
                Point pt = Point.Empty;
                if (cam) pt = dou.Scr; else pt = dou.Pos;
                pt.X += posX; if (pt.X < 0) pt.X = 0;
                pt.Y += posY; if (pt.Y < 0) pt.Y = 0;
                if (cam) dou.Scr = pt; else dou.Pos = pt;
            }
            if (GetAsyncKeyState(Keys.U) == -32767) {
                MapperGrabAt(dou.Scr);
            }
        }
        void stats_Tick(object sender, EventArgs e) {
            Point pos = dou.Pos;
            Point spd = dou.Spd;
            int spr = dou.Spr;
            Point scr = dou.Scr;
            Point cam = dou.Cam;
            string area = dou.Area;

            QuotePosX.Text = pos.X + "";
            QuotePosY.Text = pos.Y + "";
            QuoteSpdX.Text = spd.X + "";
            QuoteSpdY.Text = spd.Y + "";
            QuoteSpr.Text = spr + "";
            SekaiScrX.Text = scr.X + "";
            SekaiScrY.Text = scr.Y + "";
            SekaiCamX.Text = cam.X + "";
            SekaiCamY.Text = cam.Y + "";
            SekaiArea.Text = area;

            Bitmap bold = (Bitmap)guVcSpeed.Image;
            Bitmap bnew = vcSpeed.Draw(spd, bold);
            guVcSpeed.Image = bnew;
            bold.Dispose();
        }

        private void Opmode_0_Click(object sender, EventArgs e) {
            dou.Gui(Doukutsu.GUIMode.GUI_HALT);
        }
        private void Opmode_1_Click(object sender, EventArgs e) {
            dou.Gui(Doukutsu.GUIMode.GUI_UNLOCK);
        }
        private void Opmode_2_Click(object sender, EventArgs e) {
            dou.Gui(Doukutsu.GUIMode.GUI_SHOW);
        }
        private void Opmode_3_Click(object sender, EventArgs e) {
            dou.Gui(Doukutsu.GUIMode.GUI_UNLOCK +
                Doukutsu.GUIMode.GUI_SHOW);
        }
        
        private Bitmap getScreen(Rectangle rc) {
            Bitmap ret = new Bitmap(rc.Width, rc.Height);
            using (Graphics g = Graphics.FromImage(ret)) {
                g.CopyFromScreen(rc.Location, Point.Empty,
                    rc.Size, CopyPixelOperation.SourceCopy);
            }
            return ret;
        }
        private string Filter(string str, string mask) {
            string ret = "";
            for (int a = 0; a < str.Length; a++) {
                if (mask.Contains(str[a] + ""))
                    ret += str[a] + "";
            }
            return ret;
        }
        private void Mapper_New_Click(object sender, EventArgs e) {
            Size szScreen = new Size(
                dou.szScreen.X * dou.szTile.X / 2,
                dou.szScreen.Y * dou.szTile.Y / 2);
            szScreen.Width += dou.Scr.X / 2;
            szScreen.Height += dou.Scr.Y / 2;
            Mapper_Name.Text = Filter(dou.
                Area.Replace("?", " 2"),
                "abcdefghijklmnopqrstuvwxyz" +
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "1234567890' ");
            if (map != null) map.Dispose();
            map = new Bitmap(
                szScreen.Width,
                szScreen.Height);
        }
        private void Mapper_OfsGet_Click(object sender, EventArgs e) {
            Point pt = Cursor.Position;
            Mapper_Ofs.Text = pt.X + "x" + pt.Y;
            MapperDrawSample();
        }
        void MapperDrawSample() {
            stats.Stop();
            if (guVcSpeed.Image != null)
                guVcSpeed.Image.Dispose();
            Point pt = MapperGetOffset();
            Bitmap bm = getScreen(new Rectangle(pt.X, pt.Y, 16, 16));
            Bitmap crop = new Bitmap(guVcSpeed.Width, guVcSpeed.Height);
            using (Graphics g = Graphics.FromImage(crop)) {
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(bm, new Rectangle(Point.Empty, crop.Size));
            }
            guVcSpeed.Image = crop;
            Application.DoEvents();
            bm.Dispose();
            System.Threading.
                Thread.Sleep(400);
            stats.Start();
        }
        Point MapperGetOffset() {
            return GetPoint(Mapper_Ofs.Text, 0);
        }
        Point MapperGetStepsize() {
            return GetPoint(Mapper_Stepsize.Text, 1);
        }
        Point GetPoint(string str, int start) {
            try {
                string[] vars = str.Split('x');
                return new Point(
                    Convert.ToInt32(vars[start + 0]),
                    Convert.ToInt32(vars[start + 1]));
            } catch { return Point.Empty; }
        }
        private void Mapper_Ofs_KeyDown(object sender, KeyEventArgs e) {
            Point pt = MapperGetOffset();
            Point pn = pt;
            if (e.KeyCode == Keys.Left) pt.X--;
            if (e.KeyCode == Keys.Right) pt.X++;
            if (e.KeyCode == Keys.Up) pt.Y--;
            if (e.KeyCode == Keys.Down) pt.Y++;
            if (pt.X != pn.X || pt.Y != pn.Y) {
                Mapper_Ofs.Text = pt.X + "x" + pt.Y;
                MapperDrawSample();
            }
        }
        private void Mapper_Save_Click(object sender, EventArgs e) {
            map.Save(Mapper_Name.Text + ".png");
        }
        void MapperGrabAt(Point ptDou) {
            dou.Scr = ptDou;
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);

            Point ptScr = MapperGetOffset();
            Bitmap bm = getScreen(new Rectangle
                (ptScr.X, ptScr.Y, 320, 240));
            ptDou.X /= 2; ptDou.Y /= 2;
            using (Graphics g = Graphics.FromImage(map)) {
                g.DrawImage(bm, ptDou);
            }
        }
        private void Mapper_Auto_Click(object sender, EventArgs e) {
            Application.DoEvents();
            System.Threading.Thread.Sleep(1000);

            Point ptStep = MapperGetStepsize();
            string mode = Mapper_Stepsize
                .Text.Substring(0, 1);

            if (mode == "D") {
                Point scr = Point.Empty;
                Point ptMap = (Point)map.Size;
                ptMap.X *= 2; ptMap.Y *= 2;
                while (scr.Y <= ptMap.Y) {
                    scr.X = 0;
                    while (scr.X <= ptMap.X) {
                        MapperGrabAt(scr);
                        scr.X += dou.szTile.X * ptStep.X;
                    }
                    scr.Y += dou.szTile.Y * ptStep.Y;
                }
            }
            if (mode == "U"){
                Point ptMap = (Point)map.Size; //bitmap size
                ptMap.X *= 2; ptMap.Y *= 2; //doukutsu is bm x2
                ptMap.X -= ptMap.X % (dou.szTile.X * ptStep.X);
                ptMap.Y -= ptMap.Y % (dou.szTile.Y * ptStep.Y);
                ptMap.X += dou.szTile.X * ptStep.X;
                ptMap.Y += dou.szTile.Y * ptStep.Y;
                Point scr = ptMap; //start at bottom-right
                while (scr.Y >= 0) {
                    scr.X = ptMap.X;
                    while (scr.X >= 0) {
                        MapperGrabAt(scr);
                        scr.X -= dou.szTile.X * ptStep.X;
                    }
                    scr.Y -= dou.szTile.Y * ptStep.Y;
                }
            }
        }
    }
}
