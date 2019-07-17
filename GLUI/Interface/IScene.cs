using Geometry;
using GLCore;
using System;

namespace GLUI
{
    /// <summary>
    /// 滑鼠相關事件委派
    /// </summary>
    public delegate void DelGLMouseEvent(object sender, GLMouseEventArgs e);

    /// <summary>
    /// 畫面
    /// </summary>
    public interface IScene
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
        /// 滑鼠點擊事件
        /// </summary>
        event DelGLMouseEvent GLClickEvent;

        /// <summary>
        /// 滑鼠雙點擊事件
        /// </summary>
        event DelGLMouseEvent GLDoubleClickEvent;

        /// <summary>
        /// 滑鼠按下事件
        /// </summary>
        event DelGLMouseEvent GLDownEvent;

        /// <summary>
        /// 滑鼠移動事件
        /// </summary>
        event DelGLMouseEvent GLMoveEvent;

        /// <summary>
        /// 滑鼠放開事件
        /// </summary>
        event DelGLMouseEvent GLMoveUp;

        /// <summary>
        /// 坐標軸大小(mm)
        /// </summary>
        int AxisLength { get; }

        /// <summary>
        /// 獲得控制底層
        /// </summary>
        IScene BaseCtrl { get; }

        /// <summary>
        /// 網格大小(mm)
        /// </summary>
        int GridSize { get; }

        /// <summary>
        /// 滑鼠是否按下
        /// </summary>
        bool IsMousePress { get; }

        /// <summary>
        /// 是否畫坐標軸
        /// </summary>
        bool ShowAxis { get; set; }

        /// <summary>
        /// 是否顯示
        /// </summary>
        bool ShowFPS { get; set; }

        /// <summary>
        /// 是否畫網格
        /// </summary>
        bool ShowGrid { get; set; }

        /// <summary>
        /// 是否顯示物件名稱
        /// </summary>
        bool ShowNames { get; set; }

        /// <summary>
        /// 平移
        /// </summary>
        IPair Translate { get; }

        /// <summary>
        /// 縮放
        /// </summary>
        double Zoom { get; set; }

        /// <summary>
        /// 設定焦點
        /// </summary>
        void Focus<T>(T focus) where T : IPair;

        /// <summary>
        /// 設定焦點
        /// </summary>
        void Focus(int x, int y);

        /// <summary>
        /// 實際座標轉螢幕座標
        /// </summary>
        IPair GLToScreen<T>(T gl) where T : IPair;

        /// <summary>
        /// 實際座標轉螢幕座標
        /// </summary>
        IPair GLToScreen(int x, int y);

        /// <summary>
        /// 新地圖(刪除所有共用 Database)
        /// </summary>
        void NewMap();

        /// <summary>
        /// 螢幕座標轉實際座標
        /// </summary>
        IPair ScreenToGL<T>(T screen) where T : IPair;

        /// <summary>
        /// 螢幕座標轉實際座標
        /// </summary>
        IPair ScreenToGL(int x, int y);

        /// <summary>
        /// 加入標示物
        /// </summary>
        void SetAddMode(ISingle<ITowardPair> obj);

        /// <summary>
        /// 加入標示物
        /// </summary>
        void SetAddMode(object obj);

        /// <summary>
        /// 加入標示線
        /// </summary>
        void SetAddMode(ISingle<ILine> obj);

        /// <summary>
        /// 加入標示面
        /// </summary>
        void SetAddMode(ISingle<IArea> obj);

        /// <summary>
        /// 將滑鼠設定為拖曳模式
        /// </summary>
        void SetDragMode();

        /// <summary>
        /// 將滑鼠設定為選擇模式
        /// </summary>
        void SetEraserMode(int size);

        /// <summary>
        /// 將滑鼠設定為插入地圖模式
        /// </summary>
        void SetInsertMapMode(string filename, IMouseInsertPanel panel);

        /// <summary>
        /// 將滑鼠設定為畫筆模式
        /// </summary>
        void SetPenMode();

        /// <summary>
        /// 將滑鼠設定為選擇模式
        /// </summary>
        void SetSelectMode();

        #region 顏色

        /// <summary>
        /// X 軸顏色
        /// </summary>
        IColor AxisXColor { get; }

        /// <summary>
        /// Y 軸顏色
        /// </summary>
        IColor AxisYColor { get; }

        /// <summary>
        /// 背景色
        /// </summary>
        IColor BackgroundColor { get; }

        /// <summary>
        /// 網格顏色
        /// </summary>
        IColor GridColor { get; }

        /// <summary>
        /// 文字顏色
        /// </summary>
        IColor TextColor { get; }

        #endregion 顏色
    }

    /// <summary>
    /// 傳遞滑鼠相關事件之參數
    /// </summary>
    public class GLMouseEventArgs : EventArgs
    {
        /// <summary>
        /// 實際座標
        /// </summary>
        public IPair Position { get; set; }
    }
}
