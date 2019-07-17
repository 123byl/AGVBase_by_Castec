namespace GLCore
{
    /// <summary>
    /// 滑鼠動作介面-插入地圖
    /// </summary>
    public interface IMouseInsert : IMouse
    {
        /// <summary>
        /// 取消
        /// </summary>
        void Cancel();

        /// <summary>
        /// 地圖左/右移
        /// </summary>
        void Horizontal(int delta);

        /// <summary>
        /// 決定插入
        /// </summary>
        void Insert();

        /// <summary>
        /// 旋轉
        /// </summary>
        void Rotate(double theta);

        /// <summary>
        /// 選擇插入區域
        /// </summary>
        void SetRange();

        /// <summary>
        /// 地圖上/下移
        /// </summary>
        void Vertical(int delta);
    }
}
