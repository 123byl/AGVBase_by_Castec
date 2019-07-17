using System;

namespace NetExtension
{
    /// <summary>
    /// <para>提供常用的字串轉換格式</para>
    /// </summary>
    public static class ExToString
    {
        #region 日期轉字串

        /// <summary>
        /// <para>日期轉字串(時時分分)</para>
        /// <para>例如：0831(上午8點31分)</para>
        /// <para>例如：1300(下午1點)</para>
        /// </summary>
        public static string ToStrHHMM(this DateTime date)
        {
            return date.ToString("HHmm");
        }

        /// <summary>
        /// <para>日期轉字串(時時分分秒秒)</para>
        /// <para>例如：083129(上午8點31分29秒)</para>
        /// <para>例如：130006(下午1點0分6秒)</para>
        /// </summary>
        public static string ToStrHHMMSS(this DateTime date)
        {
            return date.ToString("HHmmss");
        }

        /// <summary>
        /// <para>日期轉字串(時時分分秒秒.毫秒)</para>
        /// <para>例如：083129.012(上午8點31分29.012秒)</para>
        /// <para>例如：130006.500(下午1點0分6.5秒)</para>
        /// </summary>
        public static string ToStrHHMMSSFFF(this DateTime date)
        {
            return date.ToString("HHmmss.fff");
        }

        /// <summary>
        /// <para>日期轉字串(月月日日)</para>
        /// <para>例如：0501(5月1日)</para>
        /// </summary>
        public static string ToStrMMDD(this DateTime date)
        {
            return date.ToString("MMdd");
        }

        /// <summary>
        /// <para>日期轉字串(年年月月日日)</para>
        /// <para>例如：170501(2017年5月1日)</para>
        /// </summary>
        public static string ToStrYYMMDD(this DateTime date)
        {
            return date.ToString("yyMMdd");
        }

        /// <summary>
        /// <para>日期轉字串(年年月月日日時時分分秒秒)</para>
        /// <para>例如：170501083129(2017年5月1日上午8點31分29秒)</para>
        /// </summary>
        public static string ToStrYYMMDDHHMMSS(this DateTime date)
        {
            return date.ToString("yyMMddHHmmss");
        }

        /// <summary>
        /// <para>日期轉字串(年年月月日日時時分分秒秒.毫秒)</para>
        /// <para>例如：170501083129.012(2017年5月1日上午8點31分29.012秒)</para>
        /// </summary>
        public static string ToStrYYMMDDHHMMSSFFF(this DateTime date)
        {
            return date.ToString("yyMMddHHmmss.fff");
        }

        /// <summary>
        /// <para>日期轉字串(年年年年月月日日)</para>
        /// <para>例如：20170501(2017年5月1日)</para>
        /// </summary>
        public static string ToStrYYYYMMDD(this DateTime date)
        {
            return date.ToString("yyyyMMdd");
        }

        /// <summary>
        /// <para>日期轉字串(年年年年月月日日時時分分秒秒.毫秒)</para>
        /// <para>例如：20170501083129.012(2017年5月1日上午8點31分29.012秒)</para>
        /// </summary>
        public static string ToStrYYYYMMDDHHMMSSFFF(this DateTime date)
        {
            return date.ToString("yyyyMMddHHmmss.fff");
        }

        #endregion 日期轉字串

        #region 浮點數轉字串

        /// <summary>
        /// <para>浮點數轉字串至小數點下一位</para>
        /// </summary>
        public static string ToStrF1(this float value)
        {
            return value.ToString("F1");
        }

        /// <summary>
        /// <para>浮點數轉字串至小數點下一位</para>
        /// </summary>
        public static string ToStrF1(this double value)
        {
            return value.ToString("F1");
        }

        /// <summary>
        /// <para>浮點數轉字串至小數點下兩位</para>
        /// </summary>
        public static string ToStrF2(this float value)
        {
            return value.ToString("F2");
        }

        /// <summary>
        /// <para>浮點數轉字串至小數點下兩位</para>
        /// </summary>
        public static string ToStrF2(this double value)
        {
            return value.ToString("F2");
        }

        /// <summary>
        /// <para>浮點數轉字串至小數點下三位</para>
        /// </summary>
        public static string ToStrF3(this float value)
        {
            return value.ToString("F3");
        }

        /// <summary>
        /// <para>浮點數轉字串至小數點下三位</para>
        /// </summary>
        public static string ToStrF3(this double value)
        {
            return value.ToString("F3");
        }

        #endregion 浮點數轉字串
    }
}
