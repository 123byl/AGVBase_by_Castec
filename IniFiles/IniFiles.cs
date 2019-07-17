using System;
using System.Runtime.InteropServices;
using System.Text;
using static IniFiles.NativeMethods;

namespace IniFiles
{
    /// <summary>
    /// INI 讀寫器
    /// </summary>
    public static class IniFiles
    {
        /// <summary>
        /// 讀取字串
        /// </summary>
        public static string Read(string FileName, string Section, string Key, string Default, int stringLength = 128)
        {
            StringBuilder val = new StringBuilder(stringLength);
            int bufLen = GetPrivateProfileString(Section, Key, Default, val, stringLength, FileName);
            return val.ToString();
        }

        /// <summary>
        /// 讀取整數
        /// </summary>
        public static int Read(string FileName, string Section, string Key, int Default)
        {
            string intStr = Read(FileName, Section, Key, Convert.ToString(Default));
            try
            {
                return Convert.ToInt32(intStr);
            }
            catch (Exception)
            {
                return Default;
            }
        }

        /// <summary>
        /// 讀取小數
        /// </summary>
        public static double Read(string FileName, string Section, string Key, double Default)
        {
            string intStr = Read(FileName, Section, Key, Convert.ToString(Default));
            try
            {
                return Convert.ToDouble(intStr);
            }
            catch (Exception)
            {
                return Default;
            }
        }

        /// <summary>
        /// 讀取布林值
        /// </summary>
        public static bool Read(string FileName, string Section, string Key, bool Default)
        {
            string intStr = Read(FileName, Section, Key, Convert.ToString(Default));
            try
            {
                return Convert.ToBoolean(intStr);
            }
            catch (Exception)
            {
                return Default;
            }
        }

        /// <summary>
        /// 寫入字串
        /// </summary>
        public static bool Write(string FileName, string Section, string Key, string Value)
        {
            return WritePrivateProfileString(Section, Key, Value, FileName);
        }

        /// <summary>
        /// 寫入整數
        /// </summary>
        public static bool Write(string FileName, string Section, string Key, int Value)
        {
            return WritePrivateProfileString(Section, Key, Value.ToString(), FileName);
        }

        /// <summary>
        /// 寫入小數
        /// </summary>
        public static bool Write(string FileName, string Section, string Key, double Value)
        {
            return WritePrivateProfileString(Section, Key, Value.ToString(), FileName);
        }

        /// <summary>
        /// 寫入布林值
        /// </summary>
        public static bool Write(string FileName, string Section, string Key, bool Value)
        {
            return WritePrivateProfileString(Section, Key, Value.ToString(), FileName);
        }
    }

    /// <summary>
    /// 原生方法
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// 讀取函式
        /// </summary>
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 寫入函式
        /// </summary>
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
    }
}
