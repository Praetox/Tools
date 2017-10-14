using System;
using System.Collections.Generic;
using System.Text;

namespace pImgDB
{
    class Crc32
    {
        public const UInt32 Polynomial = 0xedb88320;
        public const UInt32 Seed = 0xffffffff;
        private UInt32[] crcTable;
        public Crc32()
        {
            crcTable = new UInt32[256];
            for (int a = 0; a < 256; a++)
            {
                UInt32 entry = (UInt32)a;
                for (int b = 0; b < 8; b++)
                    if ((entry & 1) == 1)
                        entry = (entry >> 1) ^ Polynomial;
                    else
                        entry = entry >> 1;
                crcTable[a] = entry;
            }
        }

        public long Hash(byte[] b)
        {
            ulong uCRC = Seed;
            for (long a = 0; a < b.Length; a++)
            {
                uCRC = (uCRC >> 8) ^ crcTable[(uCRC & 0xFF) ^ b[a]];
            }
            return Convert.ToInt64(uCRC ^ Seed);
        }

        public long Hash(System.IO.MemoryStream ms)
        {
            byte[] b = new byte[ms.Length];
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            ms.Read(b, 0, (int)ms.Length);
            return Hash(b);
        }
    }
}