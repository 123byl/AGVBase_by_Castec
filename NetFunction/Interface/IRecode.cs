namespace NetFunction
{
    /// <summary>
    /// <para>資料紀錄器</para>
    /// </summary>
    public interface IRecode<T>
    {
        /// <summary>
        /// <para>新資料</para>
        /// </summary>
        T NewData { get; set; }

        /// <summary>
        /// <para>舊資料</para>
        /// </summary>
        T OldData { get; set; }

        /// <summary>
        /// 將當新資料存入舊資料中
        /// </summary>
        void Push();
    }
}
