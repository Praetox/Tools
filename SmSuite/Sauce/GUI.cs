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
using System.Drawing;
using System.Runtime.InteropServices;

public class GuiConfE {
    public string obj;
    public string font;
    public int size;
    public char rel;
    public Point locMain;
    public Color colMain;
    public Point locShad;
    public Color colShad;
    public GuiConfE() {
        obj = "";
        font = "";
        size = 0;
        rel = 'c';
        locMain = Point.Empty;
        colMain = Color.White;
        locShad = Point.Empty;
        colShad = Color.Black;
    }
    public GuiConfE(string str) {
        string[] astr = str.Split('\n');
        for (int a = 1; a < astr.Length; a++) {
            if (astr[a].StartsWith("main ") ||
                astr[a].StartsWith("shad ")) {
                //main 32,16,ffffffff
                //left , top , color RGBA
                string[] tmp = astr[a].Substring(5).Split(',');
                Point ptLoc = new Point(
                    Convert.ToInt32(tmp[0]),
                    Convert.ToInt32(tmp[1]));
                tmp[2] = tmp[2].Substring(6) + tmp[2].Substring(0, 6);
                Color coCol = Color.FromArgb(int.Parse(tmp[2],
                    System.Globalization.NumberStyles.HexNumber));

                if (astr[a].StartsWith("main ")) {
                    locMain = ptLoc; colMain = coCol;
                } else {
                    locShad = ptLoc; colShad = coCol;
                }
            }
            if (astr[a].StartsWith("font ")) {
                //font l32,Comic Sans MS
                //orientation size , face
                string tmp = astr[a].Substring(5);
                string siz = tmp.Substring(
                    1, tmp.IndexOf(",") - 1);
                font = tmp.Substring(siz.Length + 2);
                size = Convert.ToInt32(siz);
                rel = tmp[0];
            }
        }
        obj = astr[0];
    }
    public void DrawString(Graphics g, string str) {
        Font fn = new Font(font, size);
        Point skew = Point.Empty;
        if (rel != 'l') {
            SizeF area = g.MeasureString(str, fn);
            skew.X = (int)Math.Round(area.Width);
            if (rel == 'c') skew.X /= 2;
        }
        g.DrawString(str, fn, new SolidBrush(colShad),
            (float)(locShad.X - skew.X),
            (float)(locShad.Y - skew.Y));
        g.DrawString(str, fn, new SolidBrush(colMain),
            (float)(locMain.X - skew.X),
            (float)(locMain.Y - skew.Y));
    }
}
public class GuiConf {
    GuiConfE[] lst = new GuiConfE[0];
    public GuiConfE[] Get() {
        return lst;
    }
    public void Load(string fn) {
        string[] str = split(System.IO.File.
            ReadAllText(fn).Replace("\r", "")
            .Trim('\n').TrimStart(':'), "\n::");
        lst = new GuiConfE[str.Length];
        for (int a = 0; a < lst.Length; a++)
            lst[a] = new GuiConfE(str[a]);
    }
    string[] split(string a, string b) {
        return a.Split(new string[] { b },
            StringSplitOptions.None);
    }
}
public class W32 {
    public enum Bool {
        False = 0,
        True
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct Point {
        public Int32 x;
        public Int32 y;
        public Point(Int32 x, Int32 y) {
            this.x = x; this.y = y;
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Size {
        public Int32 cx;
        public Int32 cy;
        public Size(Int32 cx, Int32 cy) {
            this.cx = cx; this.cy = cy;
        }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ARGB {
        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Alpha;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BLENDFUNCTION {
        public byte BlendOp;
        public byte BlendFlags;
        public byte SourceConstantAlpha;
        public byte AlphaFormat;
    }
    public const Int32 ULW_COLORKEY = 0x01;
    public const Int32 ULW_ALPHA = 0x02;
    public const Int32 ULW_OPAQUE = 0x04;
    public const byte AC_SRC_OVER = 0x00;
    public const byte AC_SRC_ALPHA = 0x01;

    [DllImport("user32")]
    public static extern Bool UpdateLayeredWindow(
        IntPtr hwnd, IntPtr hdcDst, ref Point pptDst,
        ref Size psize, IntPtr hdcSrc, ref Point pprSrc,
        Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);
    [DllImport("user32")]
    public static extern IntPtr GetDC(IntPtr hWnd);
    [DllImport("user32")]
    public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
    [DllImport("gdi32")]
    public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
    [DllImport("gdi32")]
    public static extern Bool DeleteDC(IntPtr hdc);
    [DllImport("gdi32")]
    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
    [DllImport("gdi32")]
    public static extern Bool DeleteObject(IntPtr hObject);
}