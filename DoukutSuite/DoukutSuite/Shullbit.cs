using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace DoukutSuite {
    public class Vector {
        public float decay;
        public Color bgcolor;
        public Color fgcolor;
        Point MaxAbs = Point.Empty;
        public Vector(float dc, Color bg, Color fg) {
            decay = dc;
            bgcolor = bg;
            fgcolor = fg;
        }
        public Bitmap Draw(Point pt, Bitmap old) {
            Size sz = old.Size;
            if (MaxAbs.X < Math.Abs(pt.X))
                MaxAbs.X = Math.Abs(pt.X);
            if (MaxAbs.Y < Math.Abs(pt.Y))
                MaxAbs.Y = Math.Abs(pt.Y);
            Point mid = new Point(
                sz.Width / 2, sz.Height / 2);
            Point end = new Point(
                (int)((double)sz.Width / MaxAbs.X * pt.X / 2),
                (int)((double)sz.Height / MaxAbs.X * pt.Y / 2));
            if (end.X > 32768 || end.X < -32768) end.X = 0;
            if (end.Y > 32768 || end.Y < -32768) end.Y = 0;
            end.X += mid.X; end.Y += mid.Y;

            Pen pen = new Pen(fgcolor);
            Brush bru = new SolidBrush(fgcolor);
            Rectangle rc = new Rectangle(Point.Empty, sz);
            Bitmap ret = new Bitmap(sz.Width, sz.Height);
            using (Graphics g = Graphics.FromImage(ret)) {
                g.FillRectangle(new SolidBrush(bgcolor), rc);
                ImageAttributes attr = new ImageAttributes();
                attr.SetColorMatrix(new ColorMatrix(new float[][]{
                    new float[]{ 1f, 0f, 0f, 0f, 0f},
                    new float[]{ 0f, 1f, 0f, 0f, 0f},
                    new float[]{ 0f, 0f, 1f, 0f, 0f},
                    new float[]{ 0f, 0f, 0f, decay, 0f},
                    new float[]{ 0f, 0f, 0f, 0f, 1f}}));
                g.DrawImage(old, rc, 0, 0, sz.Width,
                    sz.Height, GraphicsUnit.Pixel, attr);
                for (int ox = -2; ox < 2; ox++) {
                    for (int oy = -2; oy < 2; oy++) {
                        Point pt1 = mid;
                        Point pt2 = end;
                        pt1.X += ox; pt2.X += ox;
                        pt1.Y += oy; pt2.Y += oy;
                        g.DrawLine(pen, pt1, pt2);
                    }
                }
            }
            return ret;
        }
    }
    public class Memory {
        #region API
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);
        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern long ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, ref int lpBuffer, int nSize, int lpNumberOfBytesWritten);
        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern long WriteProcessMemory(IntPtr hProcess, int lpBaseAddress, byte[] lpBuffer, int nSize, int lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hObject);
        #endregion
        #region Defines
        public const uint PROCESS_VM_READ = (0x0010);
        public const uint PROCESS_VM_WRITE = (0x0020);
        public const uint PROCESS_VM_OPERATION = (0x0008);
        public const uint PROCESS_ALL_ACCESS = (0x1F0FFF);
        private Process m_ReadProcess = null;
        private IntPtr m_hProcess = IntPtr.Zero;
        #endregion
        byte[] Int2Byte(int Value) {
            byte[] rawbuf = BitConverter.GetBytes(Value);
            int a = 0; for (a = rawbuf.Length; a > 0; a--)
                if (rawbuf[a - 1] != 0) break;
            byte[] buf = new byte[a];
            for (a = 0; a < buf.Length; a++)
                buf[a] = rawbuf[a];
            return buf;
        }
        public Memory(Process prc) {
            m_ReadProcess = prc;
            m_hProcess = OpenProcess(
                PROCESS_ALL_ACCESS, 1,
                (uint)m_ReadProcess.Id);
        }
        public void Dispose() {
            int iRetValue;
            iRetValue = CloseHandle(m_hProcess);
            if (iRetValue == 0) throw new
                Exception("CloseHandle failed");
        }
        #region Read memory
        public byte[] RBytes(int Address, int Length)
        {
            byte[] buf = new byte[Length];
            for (int a = 0; a < Length; a++)
            {
                int iBuf = 0;
                ReadProcessMemory(m_hProcess, Address + a, ref iBuf, 1, 0);
                buf[a] = (byte)iBuf;
            }
            return buf;
        }
        public int RInt(int Address, int Size)
        {
            int ret = 0;
            ReadProcessMemory(m_hProcess, Address, ref ret, Size, 0);
            return ret;
        }
        public int RInt(int Address)
        {
            int ret = 0;
            ReadProcessMemory(m_hProcess, Address, ref ret, 4, 0);
            return ret;
        }
        public uint RUInt(int Address, int Size)
        {
            int ret = 0; int buf = (int)ret;
            ReadProcessMemory(m_hProcess, Address, ref buf, Size, 0);
            return (uint)buf;
        }
        public double RDouble(int Address)
        {
            byte[] buf = new byte[8];
            for (int a = 0; a < 8; a++)
            {
                int iBuf = RInt(Address + a, 1);
                buf[a] = (byte)iBuf;
            }
            return BitConverter.ToDouble(buf, 0);
        }
        public string RString(int Address)
        {
            int a = 0, buf = 0; string ret = "";
            while (true)
            {
                buf = RInt(Address + a, 1);
                if (buf != 0)
                {
                    ret += (char)buf;
                }else{
                    break;
                }
                a++;
            }
            return ret;
        }
        public string RString(int Address, int Length)
        {
            string ret = "";
            for (int a = 0; a < Length; a++)
            {
                int chr = RInt(Address + a, 1);
                ret += (char)chr;
            }
            return ret;
        }
        #endregion
        #region Write memory
        public void WByte(int Address, byte[] Value)
        {
            WriteProcessMemory(m_hProcess, Address, Value, (int)Value.Length, 0);
        }
        public void WInt(int Address, int Value)
        {
            WInt(Address, Value, 4);
            //byte[] buf = Int2Byte(Value);
            //WriteProcessMemory(m_hProcess, Address, buf, (int)buf.Length, 0);
        }
        public void WInt(int Address, int Value, int Length)
        {
            byte[] buf = BitConverter.GetBytes(Value);
            WriteProcessMemory(m_hProcess, Address, buf, Length, 0);
        }
        public void WDouble(int Address, double Value)
        {
            byte[] buf = new byte[8];
            buf = BitConverter.GetBytes(Value);
            for (int a=0; a<8; a++)
            {
                WInt(Address + a, buf[a]);
            }
        }
        public void WString(int Address, string Value)
        {
            int Length = Value.Length;
            for (int a = 0; a < Length; a++)
            {
                WInt(Address + a, Convert.ToInt16(Value[a]), 1);
            }
            WInt(Address + Length, 0);
        }
        public void WByte(int Address, byte[] Value, int Offset, int Length)
        {
            for (int a = 0; a < Length; a++)
            {
                WInt(Address + a, Value[a + Offset]);
            }
        }
        public void WNops(int Address, int Amount)
        {
            Address--;
            for (int a = 1; a <= Amount; a++)
            {
                WInt(Address + a, 0x90);
            }
        }
        #endregion
    }
    public class Doukutsu {
        Memory m;
        public Point szTile;
        public Point szScreen;
        public Doukutsu() {
            Process prc = Process.
                GetProcessesByName(
                "doukutsu")[0];
            m = new Memory(prc);

            szTile = new Point(32, 32);
            szScreen = new Point(20, 15);
        }
        public struct GUIMode {
            public static int GUI_HALT = 0x0;
            public static int GUI_UNLOCK = 0x1;
            public static int GUI_SHOW = 0x2;
        }

        public Point Pos {
            get {
                return new Point(
                    m.RInt(ofs.PosX) / 256,
                    m.RInt(ofs.PosY) / 256);
            }
            set {
                m.WInt(ofs.PosX, value.X * 256);
                m.WInt(ofs.PosY, value.Y * 256);
            }
        }
        public Point Scr {
            get {
                return new Point(
                    m.RInt(ofs.ScrX) / 256,
                    m.RInt(ofs.ScrY) / 256);
            }
            set {
                m.WInt(ofs.ScrX, value.X * 256);
                m.WInt(ofs.ScrY, value.Y * 256);
            }
        }
        public Point Cam {
            get {
                return new Point(
                    m.RInt(ofs.CamX) / 256,
                    m.RInt(ofs.CamY) / 256);
            }
            set {
                m.WInt(ofs.CamX, value.X * 256);
                m.WInt(ofs.CamY, value.Y * 256);
            }
        }
        public Point Spd {
            get {
                return new Point(
                    m.RInt(ofs.SpdX) / 256,
                    m.RInt(ofs.SpdY) / 256);
            }
            set {
                m.WInt(ofs.SpdX, value.X * 256);
                m.WInt(ofs.SpdY, value.Y * 256);
            }
        }
        public int Spr {
            get {
                return m.RInt(ofs.Spr) / 256;
            }
            set {
                m.WInt(ofs.Spr, value * 256);
            }
        }
        public string Area {
            get {
                return m.RString(ofs.Area);
            }
            set {
                m.WString(ofs.Area, value);
            }
        }
        public void Gui(int mode) {
            m.WInt(ofs.gui, mode);
        }
    }
    public class ofs {
        public static int PosX = 0x49E654;
        public static int PosY = 0x49E658;
        public static int ScrX = 0x49E1C8; //0x49E65C;
        public static int ScrY = 0x49E1CC; //0x49E660;
        public static int CamX = 0x49E664;
        public static int CamY = 0x49E668;
        public static int SpdX = 0x49E66B;
        public static int SpdY = 0x49E66F;
        public static int Spr = 0x49E677;
        public static int Area = 0x49E598;
        public static int gui = 0x49E1E8;
    }
}