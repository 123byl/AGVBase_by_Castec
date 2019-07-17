using NetExtension;

namespace NetFunction
{
    /// <summary>
    /// <para>資料紀錄器</para>
    /// </summary>
    internal class Recode<T> : IRecode<T>
    {
        /// <summary>
        /// <para>新資料</para>
        /// </summary>
        public T NewData { get; set; }

        /// <summary>
        /// <para>舊資料</para>
        /// </summary>
        public T OldData { get; set; }

        /// <summary>
        /// 將當新資料存入舊資料中
        /// </summary>
        public void Push()
        {
            OldData = NewData.DeepCopy();
        }
    }
}
