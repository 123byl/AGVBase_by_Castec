using System;
using System.Collections.Generic;

namespace NetExtension
{
    /// <summary>
    /// <para>提供簡易的資料打包(Pakage)及反打包(Depakage)，用來作為通訊檢查</para>
    /// <para>打包格式如下：</para>
    /// <para>標頭+資料量+實際資料+檢查碼</para>
    /// <para>標頭佔 4 bytes 固定為 0x00,0x00,0xFF,0xFF </para>
    /// <para>資料量佔 4 bytes，等於 實際資料.Count </para>
    /// <para>檢查碼佔 1 byte，固定為 0x00</para>
    /// </summary>
    public static class ExPackage
    {
        /// <summary>
        /// <para>標頭</para>
        /// </summary>
        public static readonly byte[] Head = new byte[4] { 0x00, 0x00, 0xFF, 0xFF };

        /// <summary>
        /// <para>檢查碼</para>
        /// </summary>
        public static readonly byte[] Check = new byte[1] { 0x00 };

        /// <summary>
        /// <para>反打包第一筆資料，並刪除原始資料中已經反打包的部分</para>
        /// </summary>
        public static List<byte> Depakage(this List<byte> array)
        {
            for (int ii = 0; ii < array.Count - 7; ++ii)
            {
                // 比對開頭
                if (array[ii] == Head[0] && array[ii + 1] == Head[1] && array[ii + 2] == Head[2] && array[ii + 3] == Head[3])
                {
                    int dataLength = BitConverter.ToInt32(array.GetRange(ii + 4, 4).ToArray(), 0);
                    // 判斷長度
                    if ((dataLength >= 0) && (ii + 4 + 4 + dataLength + 1 - 1 < array.Count))
                    {
                        // 判斷檢查碼
                        if (array[ii + 4 + 4 + dataLength + 1 - 1] == Check[0])
                        {
                            List<byte> res = array.GetRange(ii + 4 + 4, dataLength);
                            array.RemoveRange(0, ii + 4 + 4 + dataLength + 1);
                            return res;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// <para>將原始資料打包</para>
        /// </summary>
        public static byte[] Package(this byte[] org)
        {
            byte[] res = new byte[4 + 4 + org.Length + 1];
            Head.CopyTo(res, 0);
            BitConverter.GetBytes(org.Length).CopyTo(res, 4);
            org.CopyTo(res, 4 + 4);
            res[4 + 4 + org.Length] = Check[0];
            return res;
        }

        /// <summary>
        /// <para>對 [<paramref name="beg"/>,<paramref name="end"/>] 之間每個位元做 XOR 運算</para>
        /// </summary>
        public static byte XOR(this byte[] org, int beg, int end)
        {
            byte res = org[beg];
            for (int ii = beg + 1; (ii <= end && ii < org.Length); ++ii) res ^= org[ii];
            return res;
        }

        /// <summary>
        /// 對所有位元做 XOR 運算
        /// </summary>
        public static byte XOR(this byte[] org)
        {
            return org.XOR(0, org.Length - 1);
        }

        /// <summary>
        /// <para>對 [<paramref name="beg"/>,<paramref name="end"/>] 之間每個位元做 XOR 運算</para>
        /// </summary>
        public static byte XOR(this List<byte> org, int beg, int end)
        {
            byte res = org[beg];
            for (int ii = beg + 1; (ii <= end && ii < org.Count); ++ii) res ^= org[ii];
            return res;
        }

        /// <summary>
        /// 對所有位元做 XOR 運算
        /// </summary>
        public static byte XOR(this List<byte> org)
        {
            return org.XOR(0, org.Count - 1);
        }
    }
}
