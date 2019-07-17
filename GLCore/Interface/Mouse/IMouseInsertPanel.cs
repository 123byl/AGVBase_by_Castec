namespace GLCore
{
    /// <summary>
    /// GL 插入地圖控制器
    /// </summary>
    public interface IMouseInsertPanel
    {
        /// <summary>
        /// 養藏
        /// </summary>
        void Hide();

        /// <summary>
        /// 設定滑鼠對象
        /// </summary>
        void SetMouse(IMouseInsert mouse);

        /// <summary>
        /// 顯示
        /// </summary>
        void Show();
    }
}
