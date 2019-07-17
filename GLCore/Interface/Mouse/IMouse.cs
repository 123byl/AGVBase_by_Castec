using Geometry;
using System;
using System.Windows.Forms;

namespace GLCore
{
    /// <summary>
    /// UI 介面拖曳事件委派
    /// </summary>
    public delegate void DelUITranslateEvent(object sender, UITranslateEventArgs e);

    /// <summary>
    /// 滑鼠動作介面
    /// </summary>
    public interface IMouse : IDrawable
    {
        /// <summary>
        /// UI 介面拖曳事件
        /// </summary>
        event DelUITranslateEvent UITranslateEvent;

        /// <summary>
        /// 滑鼠目前位置
        /// </summary>
        IPair CurrentPos { get; }

        /// <summary>
        /// 滑鼠是否被按下
        /// </summary>
        bool IsPress { get; }

        /// <summary>
        /// 按鍵
        /// </summary>
        Keys Key { get; }

        /// <summary>
        /// 滑鼠按下時的位置
        /// </summary>
        IPair PressPos { get; }

        /// <summary>
        /// 滑鼠放開時的位置
        /// </summary>
        IPair UpPos { get; }

        /// <summary>
        /// 點擊
        /// </summary>
        void Click(IPair pos);

        /// <summary>
        /// 按下
        /// </summary>
        void Down(IPair pos);

        /// <summary>
        /// 按下按鈕
        /// </summary>
        void KeyDown(Keys key);

        /// <summary>
        /// 放開按鈕
        /// </summary>
        void KeyUp();

        /// <summary>
        /// 移動
        /// </summary>
        void Move(IPair pos);

        /// <summary>
        /// 釋放資源
        /// </summary>
        void Release();

        /// <summary>
        /// 放開
        /// </summary>
        void Up(IPair pos);
    }

    /// <summary>
    /// 滑鼠拖曳 UI 事件參數
    /// </summary>
    public class UITranslateEventArgs : EventArgs
    {
        /// <summary>
        /// 滑鼠起始位置(mm)
        /// </summary>
        public IPair From { get; set; }

        /// <summary>
        /// 滑鼠結束位置(mm)
        /// </summary>
        public IPair Target { get; set; }
    }
}
