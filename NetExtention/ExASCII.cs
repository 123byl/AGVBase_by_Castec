using System;
using System.Text;

namespace NetExtension
{
    /// <summary>
    /// <para>提供常用的 ASCII 轉換及位元操作</para>
    /// </summary>
    public static class ExASCII
    {
        /// <summary>
        /// <para>將 ASCII 字元轉成 byte</para>
        /// <para>例如：'A' 轉成 0x41</para>
        /// </summary>
        public static byte ASCIICharToByte(this char c)
        {
            byte[] byteArray = BitConverter.GetBytes(c);
            return byteArray[0];
        }

        /// <summary>
        /// <para>將 ASCII 字串轉成 byte 陣列</para>
        /// <para>例如："A01" 轉成 {0x41,0x30,0x31}</para>
        /// </summary>
        public static byte[] ASCIIToBytes(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        /// <summary>
        /// <para>將 byte 陣列轉成 ASCII 字串</para>
        /// <para>例如：{0x41,0x30,0x31} 轉成 "A01"</para>
        /// </summary>
        public static string BytesToASCII(this byte[] array)
        {
            return Encoding.ASCII.GetString(array);
        }

        /// <summary>
        /// <para>將 byte 陣列轉成 16 進制的字串</para>
        /// <para>例如：{0xFF,0x30,0x31} 轉成 "FF3031"</para>
        /// </summary>
        public static string BytesToByteString(this byte[] array)
        {
            string res = string.Empty;
            foreach (byte item in array)
            {
                res = res + item.ToString("X2");
            }
            return res;
        }

        /// <summary>
        /// <para>獲得高位元值</para>
        /// <para>例如：0x1234 回傳 0x12</para>
        /// </summary>
        public static byte GetHeightBits(this UInt16 value)
        {
            return (byte)(value >> 8);
        }

        /// <summary>
        /// <para>獲得低位元值</para>
        /// <para>例如：0x1234 回傳 0x34</para>
        /// </summary>
        public static byte GetLowBits(this UInt16 value)
        {
            return (byte)((value << 8) >> 8);
        }
    }
}
