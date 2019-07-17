using System;

namespace NetExtention
{
    /// <summary>
    /// Try Catch 包裝擴充功能
    /// </summary>
    public static class ExTry
    {
        /// <summary>
        /// 使用 Try Catch 包裝 Action
        /// </summary>
        public static void Try<T>(this T obj, Action<T> action)
        {
            try
            {
                action(obj);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 使用 Try Catch 包裝 Action
        /// </summary>
        public static void Try(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
            }
        }
    }
}
