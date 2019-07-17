using Geometry;
using System;

namespace GLCore
{
    /// <summary>
    /// 當使用者點擊 Area 型態的物件時的事件委派
    /// </summary>
    public delegate void DelClickAreaEvent(object sender, AreaEventArgs e);

    /// <summary>
    /// 當使用者點擊 Area 型態的物件時的事件委派
    /// </summary>
    public delegate void DelClickLineEvent(object sender, LineEventArgs e);

    /// <summary>
    /// 當使用者點擊 ITowerPair 型態的物件時的事件委派
    /// </summary>
    public delegate void DelClickTowardPairEvent(object sender, TowerPairEventArgs e);

    /// <summary>
    /// 當使用者拖曳 Area 型態的物件時的事件委派
    /// </summary>
    public delegate void DelDragAreaEvent(object sender, AreaEventArgs e);

    /// <summary>
    /// 當使用者拖曳 Area 型態的物件時的事件委派
    /// </summary>
    public delegate void DelDragLineEvent(object sender, LineEventArgs e);

    /// <summary>
    /// 當使用者拖曳 ITowerPair 型態的物件時的事件委派
    /// </summary>
    public delegate void DelDragTowardPairEvent(object sender, TowerPairEventArgs e);

    /// <summary>
    /// 滑鼠動作-拖曳
    /// </summary>
    public interface IMouseDrag : IMouse
    {
        /// <summary>
        /// 當使用者點擊 Area 型態的物件時的事件
        /// </summary>
        event DelClickAreaEvent ClickAreaEvent;

        /// <summary>
        /// 當使用者點擊 Line 型態的物件時的事件
        /// </summary>
        event DelClickLineEvent ClickLineEvent;

        /// <summary>
        /// 當使用者點擊 ITowerPair 型態的物件時的事件
        /// </summary>
        event DelClickTowardPairEvent ClickTowerPairEvent;

        /// <summary>
        /// 當使用者拖曳 Area 型態的物件時的事件
        /// </summary>
        event DelDragAreaEvent DragAreaEvent;

        /// <summary>
        /// 當使用者拖曳 Line 型態的物件時的事件
        /// </summary>
        event DelDragLineEvent DragLineEvent;

        /// <summary>
        /// 當使用者拖曳 ITowerPair 型態的物件時的事件
        /// </summary>
        event DelDragTowardPairEvent DragTowerPairEvent;

        /// <summary>
        /// 是否可拖曳
        /// </summary>
        bool CanDrag { get; set; }

        /// <summary>
        /// 拖曳對象 ID
        /// </summary>
        uint DragTargetID { get; }
    }


    /// <summary>
    /// 傳遞 Area 型態的拖曳資料事件參數
    /// </summary>
    public class AreaEventArgs : EventArgs
    {
        /// <summary>
        /// 建立傳遞 ITowerPair 型態的拖曳資料事件參數
        /// </summary>
        public AreaEventArgs(ISingle<IArea> dragTarget, uint id)
        {
            DargTarget = dragTarget;
            ID = id;
        }

        /// <summary>
        /// 拖曳對象
        /// </summary>
        public ISingle<IArea> DargTarget { get; }

        /// <summary>
        /// 拖曳對象 ID
        /// </summary>
        public uint ID { get; }
    }

    /// <summary>
    /// 傳遞 Line 型態的拖曳資料事件參數
    /// </summary>
    public class LineEventArgs : EventArgs
    {
        /// <summary>
        /// 建立傳遞 ITowerPair 型態的拖曳資料事件參數
        /// </summary>
        public LineEventArgs(ISingle<ILine> dragTarget, uint id)
        {
            DargTarget = dragTarget;
            ID = id;
        }

        /// <summary>
        /// 拖曳對象
        /// </summary>
        public ISingle<ILine> DargTarget { get; }

        /// <summary>
        /// 拖曳對象 ID
        /// </summary>
        public uint ID { get; }
    }

    /// <summary>
    /// 傳遞 ITowerPair 型態的拖曳資料事件參數
    /// </summary>
    public class TowerPairEventArgs : EventArgs
    {
        /// <summary>
        /// 建立傳遞 ITowerPair 型態的拖曳資料事件參數
        /// </summary>
        public TowerPairEventArgs(ISingle<ITowardPair> dragTarget, uint id)
        {
            DargTarget = dragTarget;
            ID = id;
        }

        /// <summary>
        /// 拖曳對象
        /// </summary>
        public ISingle<ITowardPair> DargTarget { get; }

        /// <summary>
        /// 拖曳對象 ID
        /// </summary>
        public uint ID { get; }
    }
}
