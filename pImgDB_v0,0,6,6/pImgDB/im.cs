using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace pImgDB
{
    class im
    {
        public static byte[] ito1b(Bitmap bSrc)
        {
            try
            {
                unsafe
                {
                    int pxSize = 4;
                    int iW = bSrc.Width; int iH = bSrc.Height;
                    byte[] bPic = new byte[iW * iH * pxSize];

                    System.Drawing.Imaging.BitmapData bData =
                        bSrc.LockBits(new Rectangle(0, 0, iW, iH),
                        System.Drawing.Imaging.ImageLockMode.ReadOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    int iStride = bData.Stride;
                    IntPtr pScan0 = bData.Scan0;

                    for (int y = 0; y < iH; y++)
                    {
                        byte* pRow = (byte*)pScan0 + (y * iStride);
                        for (int x = 0; x < iW; x++)
                        {
                            int iLoc = ((y * iW) + x) * pxSize;
                            bPic[iLoc + 0] = pRow[x * pxSize + 3]; //A
                            bPic[iLoc + 1] = pRow[x * pxSize + 2]; //R
                            bPic[iLoc + 2] = pRow[x * pxSize + 1]; //G
                            bPic[iLoc + 3] = pRow[x * pxSize + 0]; //B
                        }
                    }

                    bSrc.UnlockBits(bData);
                    //bSrc.Dispose();

                    byte[] bResX = BitConverter.GetBytes(iW);
                    byte[] bResY = BitConverter.GetBytes(iH);
                    Array.Reverse(bResX); Array.Reverse(bResY);

                    byte[] bRet = new byte[bPic.Length + bResX.Length + bResY.Length];
                    bResX.CopyTo(bRet, 0);
                    bResY.CopyTo(bRet, bResX.Length);
                    bPic.CopyTo(bRet, bResX.Length + bResY.Length);
                    return bRet;
                }
            }
            catch { return new byte[0]; }
        }
        public static byte[, ,] ito3b(Bitmap bSrc)
        {
            try
            {
                unsafe
                {
                    int pxSize = 4;
                    int iW = bSrc.Width; int iH = bSrc.Height;
                    byte[, ,] ret = new byte[iW, iH, 4];

                    System.Drawing.Imaging.BitmapData bData =
                        bSrc.LockBits(new Rectangle(0, 0, iW, iH),
                        System.Drawing.Imaging.ImageLockMode.ReadOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    int iStride = bData.Stride;
                    IntPtr pScan0 = bData.Scan0;

                    for (int y = 0; y < iH; y++)
                    {
                        byte* pRow = (byte*)pScan0 + (y * iStride);
                        for (int x = 0; x < iW; x++)
                        {
                            ret[x, y, 0] = pRow[x * pxSize + 3]; //A
                            ret[x, y, 1] = pRow[x * pxSize + 2]; //R
                            ret[x, y, 2] = pRow[x * pxSize + 1]; //G
                            ret[x, y, 3] = pRow[x * pxSize + 0]; //B
                        }
                    }

                    bSrc.UnlockBits(bData);
                    //bSrc.Dispose();
                    return ret;
                }
            }
            catch { return new byte[0, 0, 0]; }
        }
        public static Bitmap btoi(byte[] bPic)
        {
            try
            {
                unsafe
                {
                    byte[] bResX = new byte[4];
                    byte[] bResY = new byte[4];
                    for (int a = 0; a < 4; a++)
                    {
                        bResX[a] = bPic[a + 0];
                        bResY[a] = bPic[a + 4];
                    }
                    Array.Reverse(bResX); Array.Reverse(bResY);
                    int iW = BitConverter.ToInt32(bResX, 0);
                    int iH = BitConverter.ToInt32(bResY, 0);
                    Bitmap ret = new Bitmap(iW, iH);

                    System.Drawing.Imaging.BitmapData bData =
                        ret.LockBits(new Rectangle(0, 0, iW, iH),
                        System.Drawing.Imaging.ImageLockMode.WriteOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    int iStride = bData.Stride;
                    IntPtr pScan0 = bData.Scan0;
                    int pxSize = 4;

                    for (int y = 0; y < iH; y++)
                    {
                        byte* pRow = (byte*)pScan0 + (y * iStride);
                        for (int x = 0; x < iW; x++)
                        {
                            int iLoc = ((y * iW) + x) * pxSize;
                            pRow[x * pxSize + 3] = bPic[iLoc + 0]; //A
                            pRow[x * pxSize + 2] = bPic[iLoc + 1]; //R
                            pRow[x * pxSize + 1] = bPic[iLoc + 2]; //G
                            pRow[x * pxSize + 0] = bPic[iLoc + 3]; //B
                        }
                    }

                    ret.UnlockBits(bData);
                    return ret;
                }
            }
            catch { return new Bitmap(1, 1); }
        }
        public static Bitmap btoi(byte[, ,] baPic)
        {
            try
            {
                unsafe
                {
                    int iW = baPic.GetUpperBound(0) + 1;
                    int iH = baPic.GetUpperBound(1) + 1;
                    Bitmap ret = new Bitmap(iW, iH);

                    System.Drawing.Imaging.BitmapData bData =
                        ret.LockBits(new Rectangle(0, 0, iW, iH),
                        System.Drawing.Imaging.ImageLockMode.WriteOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    int iStride = bData.Stride;
                    IntPtr pScan0 = bData.Scan0;
                    int pxSize = 4;

                    for (int y = 0; y < iH; y++)
                    {
                        byte* pRow = (byte*)pScan0 + (y * iStride);
                        for (int x = 0; x < iW; x++)
                        {
                            pRow[x * pxSize + 3] = baPic[x, y, 0]; //A
                            pRow[x * pxSize + 2] = baPic[x, y, 1]; //R
                            pRow[x * pxSize + 1] = baPic[x, y, 2]; //G
                            pRow[x * pxSize + 0] = baPic[x, y, 3]; //B
                        }
                    }

                    ret.UnlockBits(bData);
                    return ret;
                }
            }
            catch { return new Bitmap(1, 1); }
        }
        public static byte[] btob(byte[, ,] baPic)
        {
            int pxSize = 4;
            int iW = baPic.GetUpperBound(0) + 1;
            int iH = baPic.GetUpperBound(1) + 1;
            byte[] bPic = new byte[iW * iH * pxSize];
            for (int y = 0; y < iH; y++)
                for (int x = 0; x < iW; x++)
                    for (int c = 0; c < pxSize; c++)
                        bPic[(((y * iW) + x) * pxSize) + c] =
                            baPic[x, y, c];

            byte[] bResX = BitConverter.GetBytes(iW);
            byte[] bResY = BitConverter.GetBytes(iH);
            Array.Reverse(bResX); Array.Reverse(bResY);

            byte[] bRet = new byte[bPic.Length + bResX.Length + bResY.Length];
            bResX.CopyTo(bRet, 0);
            bResY.CopyTo(bRet, bResX.Length);
            bPic.CopyTo(bRet, bResX.Length + bResY.Length);
            return bRet;
        }
        public static byte[, ,] btob(byte[] bPic)
        {
            int pxSize = 4;
            byte[] bResX = new byte[4];
            byte[] bResY = new byte[4];
            for (int a = 0; a < 4; a++)
            {
                bResX[a] = bPic[a + 0];
                bResY[a] = bPic[a + 4];
            }
            Array.Reverse(bResX); Array.Reverse(bResY);
            int iW = BitConverter.ToInt32(bResX, 0);
            int iH = BitConverter.ToInt32(bResY, 0);

            byte[, ,] ret = new byte[iW, iH, pxSize];
            for (int y = 0; y < iH; y++)
                for (int x = 0; x < iW; x++)
                {
                    int iLoc = ((y * iW) + x) * pxSize;
                    ret[x, y, 0] = bPic[iLoc + 0]; //A
                    ret[x, y, 1] = bPic[iLoc + 1]; //R
                    ret[x, y, 2] = bPic[iLoc + 2]; //G
                    ret[x, y, 3] = bPic[iLoc + 3]; //B
                }
            return ret;
        }

        public static Bitmap LoadMem(string sPath)
        {
            System.IO.FileStream fs = new System.IO.FileStream(sPath,
                System.IO.FileMode.Open, System.IO.FileAccess.Read);
            Bitmap b = new Bitmap(fs); fs.Close(); fs.Dispose();
            return b;
        }
        public static Bitmap LoadGDI(string sPath)
        {
            return new Bitmap(sPath);
        }
        public static Bitmap RotateFlip(Bitmap bSrc, int iRot, bool bH, bool bV)
        {
            RotateFlipType rft = RotateFlip(iRot, bH, bV);
            if (rft != RotateFlipType.RotateNoneFlipNone)
                bSrc.RotateFlip(rft); return bSrc;
        }
        public static Bitmap RotateFlip(Bitmap bSrc, string sOpt)
        {
            RotateFlipType rft = RotateFlip(sOpt);
            if (rft != RotateFlipType.RotateNoneFlipNone)
                bSrc.RotateFlip(rft); return bSrc;
        }
        public static RotateFlipType RotateFlip(string sOpt)
        {
            int iRot = 0;
            sOpt = sOpt.ToLower();
            bool bFlipH = sOpt.Contains("h");
            bool bFlipV = sOpt.Contains("v");
            iRot += sOpt.Split('1').Length * 1;
            iRot += sOpt.Split('2').Length * 2;
            iRot += sOpt.Split('3').Length * 3;
            return RotateFlip(iRot, bFlipH, bFlipV);
        }
        public static RotateFlipType RotateFlip(int iRot, bool bH, bool bV)
        {
            if (bH && bV)
            {
                iRot += 2;
                bH = false;
                bV = false;
            }
            iRot %= 4;

            if (bH)
            {
                if (iRot == 0) return RotateFlipType.RotateNoneFlipX;
                if (iRot == 1) return RotateFlipType.Rotate90FlipX;
                if (iRot == 2) return RotateFlipType.Rotate180FlipX;
                if (iRot == 3) return RotateFlipType.Rotate270FlipX;
            }
            if (bV)
            {
                if (iRot == 0) return RotateFlipType.RotateNoneFlipY;
                if (iRot == 1) return RotateFlipType.Rotate90FlipY;
                if (iRot == 2) return RotateFlipType.Rotate180FlipY;
                if (iRot == 3) return RotateFlipType.Rotate270FlipY;
            }
            if (!bH && !bV)
            {
                if (iRot == 0) return RotateFlipType.RotateNoneFlipNone;
                if (iRot == 1) return RotateFlipType.Rotate90FlipNone;
                if (iRot == 2) return RotateFlipType.Rotate180FlipNone;
                if (iRot == 3) return RotateFlipType.Rotate270FlipNone;
            }
            return RotateFlipType.RotateNoneFlipNone;
        }
        public static Bitmap Resize(Bitmap bPic, int iX, int iY, bool bKeepAspect, int iScaleMode, bool bEnlargen)
        {
            if (!bEnlargen)
            {
                if (iX > bPic.Width) iX = bPic.Width;
                if (iY > bPic.Height) iY = bPic.Height;
            }
            if (iX < 1) iX = 1; if (iY < 1) iY = 1;
            double dRaX = (double)bPic.Width / (double)iX;
            double dRaY = (double)bPic.Height / (double)iY;
            if (bKeepAspect)
            {
                if (dRaX > dRaY) iY = (int)Math.Round((double)iX / ((double)bPic.Width / (double)bPic.Height));
                if (dRaY > dRaX) iX = (int)Math.Round((double)iY * ((double)bPic.Width / (double)bPic.Height));
            }
            Bitmap bOut = new Bitmap(iX, iY);
            using (Graphics gOut = Graphics.FromImage((Image)bOut))
            {
                gOut.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                if (iScaleMode == 2) gOut.InterpolationMode =
                      System.Drawing.Drawing2D.InterpolationMode.High;
                if (iScaleMode == 3) gOut.InterpolationMode =
                      System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                gOut.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gOut.DrawImage(bPic, 0, 0, iX, iY);
            }
            return bOut;
        }
        public static Bitmap Resize(Bitmap bPic, int iX, int iY, bool bKeepAspect, int iScaleMode)
        {
            return Resize(bPic, iX, iY, bKeepAspect, iScaleMode, false);
        }
    }
}
