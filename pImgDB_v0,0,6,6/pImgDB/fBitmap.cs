using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace pImgDB
{
    public unsafe class fBitmap
    {
        public struct pixData
        {
            public byte a;
            public byte b;
            public byte g;
            public byte r;

            public pixData(byte bA, byte bR, byte bG, byte bB)
            {
                a = bA; r = bR; g = bG; b = bB;
            }
        }

        Bitmap bPic;
        int iPicW;
        BitmapData bData = null;
        Byte* pBase = null;

        public fBitmap(Bitmap b, bool bConv)
        {
            Init(b, bConv);
        }
        public fBitmap(Bitmap b)
        {
            Init(b, true);
        }
        public void Init(Bitmap b, bool bConv)
        {
            if (bConv)
            {
                System.IO.MemoryStream ms =
                    new System.IO.MemoryStream();
                b.Save(ms, ImageFormat.Bmp);
                b = new Bitmap(ms);
            }

            this.bPic = b;
            try { LockBM(); }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Flush()
        {
            try { UnlockBM(); }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Bitmap Bitmap
        {
            get { return bPic; }
        }

        public void pixSet(int x, int y, pixData col)
        {
            try
            {
                pixData* p = pixAt(x, y);
                p->a = col.a;
                p->r = col.r;
                p->g = col.g;
                p->b = col.b;
            }
            catch (AccessViolationException ave) { throw (ave); }
            catch (Exception ex) { throw ex; }
        }
        public pixData pixGet(int x, int y)
        {
            try
            {
                pixData* p = pixAt(x, y);
                return new pixData(p->a, p->r, p->g, p->b); //(int)
            }
            catch (AccessViolationException ave) { throw (ave); }
            catch (Exception ex) { throw ex; }
        }

        public void LockBM()
        {
            GraphicsUnit unit = GraphicsUnit.Pixel;
            RectangleF boundsF = bPic.GetBounds(ref unit);
            Rectangle bounds = new Rectangle(
                (int)boundsF.X,
                (int)boundsF.Y,
                (int)boundsF.Width,
                (int)boundsF.Height);

            iPicW = (int)boundsF.Width * sizeof(pixData);
            if (iPicW % 4 != 0)
                iPicW = 4 * (iPicW / 4 + 1);

            bData = bPic.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            pBase = (Byte*)bData.Scan0.ToPointer();
        }
        private pixData* pixAt(int x, int y)
        {
            return (pixData*)(pBase + y * iPicW + x * sizeof(pixData));
        }
        private void UnlockBM()
        {
            bPic.UnlockBits(bData);
            bData = null; pBase = null;
        }
    }
}
