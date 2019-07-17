namespace GLCore
{
    /// <summary>
    /// 具執行續安全的存取集合基底介面
    /// </summary>
    public interface ISafety
    {
        /// <summary>
        /// 資料數
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 清除所有資料
        /// </summary>
        void Clear();
    }
}