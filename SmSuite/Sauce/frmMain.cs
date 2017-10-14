/*  SMSuite -- StepMania MIGS calculator and stats display
 *  Copyright (C) 2009  Praetox (http://praetox.com/)
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace SMSuite {
    public partial class frmMain : Form {
        public frmMain() {
            this.FormBorderStyle = FormBorderStyle.None;
            alpha = System.IO.File.Exists("alpha.png");
            InitializeComponent();
        }
        bool alpha;
        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                if (alpha) cp.ExStyle |= 0x00080000; //WS_EX_LAYERED
                return cp;
            }
        }
        public void SetBitmap(Bitmap bitmap, byte opacity) {
            if (!alpha) {
                if (pbSkin.Image != null) {
                    pbSkin.Image.Dispose();
                    pbSkin.Image = null;
                }
                pbSkin.Image = bitmap;
                return;
            }
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException(
                    "Use an alphablended .png");

            IntPtr screenDc = W32.GetDC(IntPtr.Zero);
            IntPtr memDc = W32.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBitmap = W32.SelectObject(memDc, hBitmap);

                W32.Size size = new W32.Size(bitmap.Width, bitmap.Height);
                W32.Point pointSource = new W32.Point(0, 0);
                W32.Point topPos = new W32.Point(Left, Top);
                W32.BLENDFUNCTION blend = new W32.BLENDFUNCTION();
                blend.BlendOp = W32.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = opacity;
                blend.AlphaFormat = W32.AC_SRC_ALPHA;

                W32.UpdateLayeredWindow(Handle, screenDc,
                    ref topPos, ref size, memDc, ref pointSource,
                    0, ref blend, W32.ULW_ALPHA);
            } finally {
                W32.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero) {
                    W32.SelectObject(memDc, oldBitmap);
                    W32.DeleteObject(hBitmap);
                }
                W32.DeleteDC(memDc);
            }
        }

        SM sm;
        Bitmap bSkin = null;
        PictureBox pbSkin = null;
        GuiConf gc = new GuiConf();
        private void frmMain_Load(object sender, EventArgs e) {
            frmSetMode fsm = new frmSetMode();
            fsm.ShowDialog();
            if (fsm.mode == SM.Mode.None) GTFO();
            
            frmWaitSM fws = new frmWaitSM();
            fws.ShowDialog();
            if (fws.smProc == null) GTFO();
            
            sm = new SM(fws.smProc, fsm.mode);
            if (alpha) {
                bSkin = new Bitmap("alpha.png");
                gc.Load("alpha.txt");
            } else {
                bSkin = new Bitmap("skin.png");
                pbSkin = new PictureBox();
                pbSkin.Dock = DockStyle.Fill;
                this.Controls.Add(pbSkin);
                pbSkin.MouseDown +=
                    frmMain_MouseDown;
                gc.Load("skin.txt");
            }
            this.Size = bSkin.Size;
            Timer t = new Timer(); t.Tick +=
                delegate(object oa, EventArgs ob) {
                    SetBitmap(Draw(), 255);
                };
            t.Interval = 100; t.Start();
        }
        private void GTFO() {
            Process.GetCurrentProcess().Kill();
        }
        private Bitmap Draw() {
            Bitmap ret = new Bitmap(bSkin.Width, bSkin.Height);
            using (Graphics g = Graphics.FromImage(ret)) {
                g.DrawImage(bSkin, new Rectangle(
                    Point.Empty, bSkin.Size));

                GuiConfE[] gce = gc.Get();
                for (int a = 0; a < gce.Length; a++) {
                    if (gce[a].size == 0) continue;
                    if (gce[a].obj == "MODE") gce[a].
                        DrawString(g, "" + sm.App_Mode);
                    if (gce[a].obj == "MIGS") gce[a].
                        DrawString(g, "" + sm.Dance_MIGS);
                    if (gce[a].obj == "DNCE") gce[a].
                        DrawString(g, "" + sm.Dance_nPts);
                    if (gce[a].obj == "CMBO") gce[a].
                        DrawString(g, "" + sm.Count_Cmbo);
                    if (gce[a].obj == "MARV") gce[a].
                        DrawString(g, "" + sm.Count_Marv);
                    if (gce[a].obj == "PERF") gce[a].
                        DrawString(g, "" + sm.Count_Perf);
                    if (gce[a].obj == "GRET") gce[a].
                        DrawString(g, "" + sm.Count_Gret);
                    if (gce[a].obj == "GOOD") gce[a].
                        DrawString(g, "" + sm.Count_Good);
                    if (gce[a].obj == "BOOO") gce[a].
                        DrawString(g, "" + sm.Count_Booo);
                    if (gce[a].obj == "MISS") gce[a].
                        DrawString(g, "" + sm.Count_Miss);
                    if (gce[a].obj == "OKAY") gce[a].
                        DrawString(g, "" + sm.Count_Okay);
                }
            }
            return ret;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32")]
        public static extern bool ReleaseCapture();
        private void frmMain_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
