using System;
using System.Threading;

namespace NetExtension
{
    /// <summary>
    /// <para>提供執行緒安全結束的操作方法</para>
    /// </summary>
    public static class ExThread
    {
        /// <summary>
        /// <para>提供執行緒安全結束的操作方法</para>
        /// <para>並回傳例外</para>
        /// </summary>
        public static Exception Kill(this System.Threading.Thread thread)
        {
            Exception e = null;
            try
            {
                if (thread != null && thread.IsAlive)
                {
                    thread.Abort();
                }
            }
            catch (Exception ex)
            {
                e = ex;
            }
            finally
            {
                while (thread != null && thread.IsAlive) SpinWait.SpinUntil(() => false, 1);
            }
            return e;
        }
    }
}
