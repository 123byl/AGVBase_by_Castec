using System;

namespace NetExtension
{
    /// <summary>
    /// <para>提供常用數值的操作方法</para>
    /// </summary>
    public static class ExNumberOperator
    {
        #region 邊界設定

        /// <summary>
        /// <para>將資料限定在最小值與最大值之間</para>
        /// <para>不協助檢查輸入的 <paramref name="min"/> 是否小於等於 <paramref name="max"/></para>
        /// <para>程式將優先比較 <paramref name="self"/> 與 <paramref name="min"/></para>
        /// <para>若 <paramref name="self"/> 小於等於 <paramref name="min"/> 則立刻返回 <paramref name="min"/> </para>
        /// </summary>
        public static byte Bound(this byte self, byte min, byte max)
        {
            return GenericBound(min, self, max);
        }

        /// <summary>
        /// <para>將資料限定在最小值與最大值之間</para>
        /// <para>不協助檢查輸入的 <paramref name="min"/> 是否小於等於 <paramref name="max"/></para>
        /// <para>程式將優先比較 <paramref name="self"/> 與 <paramref name="min"/></para>
        /// <para>若 <paramref name="self"/> 小於等於 <paramref name="min"/> 則立刻返回 <paramref name="min"/> </para>
        /// </summary>
        public static sbyte Bound(this sbyte self, sbyte min, sbyte max)
        {
            return GenericBound(min, self, max);
        }

        /// <summary>
        /// <para>將資料限定在最小值與最大值之間</para>
        /// <para>不協助檢查輸入的 <paramref name="min"/> 是否小於等於 <paramref name="max"/></para>
        /// <para>程式將優先比較 <paramref name="self"/> 與 <paramref name="min"/></para>
        /// <para>若 <paramref name="self"/> 小於等於 <paramref name="min"/> 則立刻返回 <paramref name="min"/> </para>
        /// </summary>
        public static int Bound(this int self, int min, int max)
        {
            return GenericBound(min, self, max);
        }

        /// <summary>
        /// <para>將資料限定在最小值與最大值之間</para>
        /// <para>不協助檢查輸入的 <paramref name="min"/> 是否小於等於 <paramref name="max"/></para>
        /// <para>程式將優先比較 <paramref name="self"/> 與 <paramref name="min"/></para>
        /// <para>若 <paramref name="self"/> 小於等於 <paramref name="min"/> 則立刻返回 <paramref name="min"/> </para>
        /// </summary>
        public static uint Bound(this uint self, uint min, uint max)
        {
            return GenericBound(min, self, max);
        }

        /// <summary>
        /// <para>將資料限定在最小值與最大值之間</para>
        /// <para>不協助檢查輸入的 <paramref name="min"/> 是否小於等於 <paramref name="max"/></para>
        /// <para>程式將優先比較 <paramref name="self"/> 與 <paramref name="min"/></para>
        /// <para>若 <paramref name="self"/> 小於等於 <paramref name="min"/> 則立刻返回 <paramref name="min"/> </para>
        /// </summary>
        public static short Bound(this short self, short min, short max)
        {
            return GenericBound(min, self, max);
        }

        /// <summary>
        /// <para>將資料限定在最小值與最大值之間</para>
        /// <para>不協助檢查輸入的 <paramref name="min"/> 是否小於等於 <paramref name="max"/></para>
        /// <para>程式將優先比較 <paramref name="self"/> 與 <paramref name="min"/></para>
        /// <para>若 <paramref name="self"/> 小於等於 <paramref name="min"/> 則立刻返回 <paramref name="min"/> </para>
        /// </summary>
        public static ushort Bound(this ushort self, ushort min, ushort max)
        {
            return GenericBound(min, self, max);
        }

        /// <summary>
        /// <para>將資料限定在最小值與最大值之間</para>
        /// <para>不協助檢查輸入的 <paramref name="min"/> 是否小於等於 <paramref name="max"/></para>
        /// <para>程式將優先比較 <paramref name="self"/> 與 <paramref name="min"/></para>
        /// <para>若 <paramref name="self"/> 小於等於 <paramref name="min"/> 則立刻返回 <paramref name="min"/> </para>
        /// </summary>
        public static long Bound(this long self, long min, long max)
        {
            return GenericBound(min, self, max);
        }

        /// <summary>
        /// <para>將資料限定在最小值與最大值之間</para>
        /// <para>不協助檢查輸入的 <paramref name="min"/> 是否小於等於 <paramref name="max"/></para>
        /// <para>程式將優先比較 <paramref name="self"/> 與 <paramref name="min"/></para>
        /// <para>若 <paramref name="self"/> 小於等於 <paramref name="min"/> 則立刻返回 <paramref name="min"/> </para>
        /// </summary>
        public static ulong Bound(this ulong self, ulong min, ulong max)
        {
            return GenericBound(min, self, max);
        }

        /// <summary>
        /// <para>將資料限定在最小值與最大值之間</para>
        /// <para>不協助檢查輸入的 <paramref name="min"/> 是否小於等於 <paramref name="max"/></para>
        /// <para>程式將優先比較 <paramref name="self"/> 與 <paramref name="min"/></para>
        /// <para>若 <paramref name="self"/> 小於等於 <paramref name="min"/> 則立刻返回 <paramref name="min"/> </para>
        /// </summary>
        public static float Bound(this float self, float min, float max)
        {
            return GenericBound(min, self, max);
        }

        /// <summary>
        /// <para>將資料限定在最小值與最大值之間</para>
        /// <para>不協助檢查輸入的 <paramref name="min"/> 是否小於等於 <paramref name="max"/></para>
        /// <para>程式將優先比較 <paramref name="self"/> 與 <paramref name="min"/></para>
        /// <para>若 <paramref name="self"/> 小於等於 <paramref name="min"/> 則立刻返回 <paramref name="min"/> </para>
        /// </summary>
        public static double Bound(this double self, double min, double max)
        {
            return GenericBound(min, self, max);
        }

        /// <summary>
        /// <para>將資料限定在最小值與最大值之間</para>
        /// <para>不協助檢查輸入的 <paramref name="min"/> 是否小於等於 <paramref name="max"/></para>
        /// <para>程式將優先比較 <paramref name="self"/> 與 <paramref name="min"/></para>
        /// <para>若 <paramref name="self"/> 小於等於 <paramref name="min"/> 則立刻返回 <paramref name="min"/> </para>
        /// </summary>
        public static char Bound(this char self, char min, char max)
        {
            return GenericBound(min, self, max);
        }

        /// <summary>
        /// <para>將資料限定在最小值與最大值之間</para>
        /// <para>不協助檢查輸入的 <paramref name="min"/> 是否小於等於 <paramref name="max"/></para>
        /// <para>程式將優先比較 <paramref name="self"/> 與 <paramref name="min"/></para>
        /// <para>若 <paramref name="self"/> 小於等於 <paramref name="min"/> 則立刻返回 <paramref name="min"/> </para>
        /// </summary>
        public static decimal Bound(this decimal self, decimal min, decimal max)
        {
            return GenericBound(min, self, max);
        }

        private static T GenericBound<T>(T min, T target, T max) where T : IComparable
        {
            if (target.CompareTo(min) <= 0) return min;
            if (target.CompareTo(max) >= 0) return max;
            return target;
        }

        #endregion 邊界設定

        #region 角度轉徑度

        /// <summary>
        /// <para>角度轉徑度倍率常數</para>
        /// </summary>
        public const double DegToRadFactor = Math.PI / 180.0;

        /// <summary>
        /// <para>回傳角度轉徑度後的結果</para>
        /// </summary>
        public static double DegToRad(this byte self)
        {
            return self * DegToRadFactor;
        }

        /// <summary>
        /// <para>回傳角度轉徑度後的結果</para>
        /// </summary>
        public static double DegToRad(this sbyte self)
        {
            return self * DegToRadFactor;
        }

        /// <summary>
        /// <para>回傳角度轉徑度後的結果</para>
        /// </summary>
        public static double DegToRad(this int self)
        {
            return self * DegToRadFactor;
        }

        /// <summary>
        /// <para>回傳角度轉徑度後的結果</para>
        /// </summary>
        public static double DegToRad(this uint self)
        {
            return self * DegToRadFactor;
        }

        /// <summary>
        /// <para>回傳角度轉徑度後的結果</para>
        /// </summary>
        public static double DegToRad(this short self)
        {
            return self * DegToRadFactor;
        }

        /// <summary>
        /// <para>回傳角度轉徑度後的結果</para>
        /// </summary>
        public static double DegToRad(this ushort self)
        {
            return self * DegToRadFactor;
        }

        /// <summary>
        /// <para>回傳角度轉徑度後的結果</para>
        /// </summary>
        public static double DegToRad(this long self)
        {
            return self * DegToRadFactor;
        }

        /// <summary>
        /// <para>回傳角度轉徑度後的結果</para>
        /// </summary>
        public static double DegToRad(this ulong self)
        {
            return self * DegToRadFactor;
        }

        /// <summary>
        /// <para>回傳角度轉徑度後的結果</para>
        /// </summary>
        public static double DegToRad(this float self)
        {
            return self * DegToRadFactor;
        }

        /// <summary>
        /// <para>回傳角度轉徑度後的結果</para>
        /// </summary>
        public static double DegToRad(this double self)
        {
            return self * DegToRadFactor;
        }

        /// <summary>
        /// <para>回傳角度轉徑度後的結果</para>
        /// </summary>
        public static double DegToRad(this char self)
        {
            return self * DegToRadFactor;
        }

        #endregion 角度轉徑度

        #region 徑度轉角度

        /// <summary>
        /// <para>徑度轉角度倍率常數</para>
        /// </summary>
        public const double RadToDegFactor = 180.0 / Math.PI;

        /// <summary>
        /// <para>回傳徑度轉角度後的結果</para>
        /// </summary>
        public static double RadToDeg(this byte self)
        {
            return self * RadToDegFactor;
        }

        /// <summary>
        /// <para>回傳徑度轉角度後的結果</para>
        /// </summary>
        public static double RadToDeg(this sbyte self)
        {
            return self * RadToDegFactor;
        }

        /// <summary>
        /// <para>回傳徑度轉角度後的結果</para>
        /// </summary>
        public static double RadToDeg(this int self)
        {
            return self * RadToDegFactor;
        }

        /// <summary>
        /// <para>回傳徑度轉角度後的結果</para>
        /// </summary>
        public static double RadToDeg(this uint self)
        {
            return self * RadToDegFactor;
        }

        /// <summary>
        /// <para>回傳徑度轉角度後的結果</para>
        /// </summary>
        public static double RadToDeg(this short self)
        {
            return self * RadToDegFactor;
        }

        /// <summary>
        /// <para>回傳徑度轉角度後的結果</para>
        /// </summary>
        public static double RadToDeg(this ushort self)
        {
            return self * RadToDegFactor;
        }

        /// <summary>
        /// <para>回傳徑度轉角度後的結果</para>
        /// </summary>
        public static double RadToDeg(this long self)
        {
            return self * RadToDegFactor;
        }

        /// <summary>
        /// <para>回傳徑度轉角度後的結果</para>
        /// </summary>
        public static double RadToDeg(this ulong self)
        {
            return self * RadToDegFactor;
        }

        /// <summary>
        /// <para>回傳徑度轉角度後的結果</para>
        /// </summary>
        public static double RadToDeg(this float self)
        {
            return self * RadToDegFactor;
        }

        /// <summary>
        /// <para>回傳徑度轉角度後的結果</para>
        /// </summary>
        public static double RadToDeg(this double self)
        {
            return self * RadToDegFactor;
        }

        /// <summary>
        /// <para>回傳徑度轉角度後的結果</para>
        /// </summary>
        public static double RadToDeg(this char self)
        {
            return self * RadToDegFactor;
        }

        #endregion 徑度轉角度

        #region 浮點數比較

        /// <summary>
        /// <para>使用絕對誤差方式比較兩數是否相等</para>
        /// </summary>
        public static bool AbsErrorEqual(this float lhs, float rhs, float error = 0.0001f)
        {
            return Math.Abs(lhs - rhs) <= error;
        }

        /// <summary>
        /// <para>使用絕對誤差方式比較兩數是否相等</para>
        /// </summary>
        public static bool AbsErrorEqual(this double lhs, double rhs, double error = 0.0001)
        {
            return Math.Abs(lhs - rhs) <= error;
        }

        #endregion 浮點數比較
    }
}
