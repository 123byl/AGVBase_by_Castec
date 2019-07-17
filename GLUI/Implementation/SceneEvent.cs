using Geometry;
using GLCore;
using SharpGL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GLUI
{
    // 控制項相關程式碼
    internal partial class Scene
    {
        private Point mMousePos = new Point();

        /// <summary>
        /// 當使用者點擊 Area 型態的物件時的事件
        /// </summary>
        public event DelClickAreaEvent ClickAreaEvent;

        /// <summary>
        /// 當使用者點擊 Line 型態的物件時的事件
        /// </summary>
        public event DelClickLineEvent ClickLineEvent;

        /// <summary>
        /// 當使用者點擊 ITowerPair 型態的物件時的事件
        /// </summary>
        public event DelClickTowardPairEvent ClickTowerPairEvent;

        /// <summary>
        /// 當使用者拖曳 Area 型態的物件時的事件
        /// </summary>
        public event DelDragAreaEvent DragAreaEvent;

        /// <summary>
        /// 當使用者拖曳 Line 型態的物件時的事件
        /// </summary>
        public event DelDragLineEvent DragLineEvent;

        /// <summary>
        /// 當使用者拖曳 ITowerPair 型態的物件時的事件
        /// </summary>
        public event DelDragTowardPairEvent DragTowerPairEvent;

        /// <summary>
        /// 滑鼠點擊事件
        /// </summary>
        public event DelGLMouseEvent GLClickEvent;

        /// <summary>
        /// 滑鼠雙點擊事件
        /// </summary>
        public event DelGLMouseEvent GLDoubleClickEvent;

        /// <summary>
        /// 滑鼠按下事件
        /// </summary>
        public event DelGLMouseEvent GLDownEvent;

        /// <summary>
        /// 滑鼠移動事件
        /// </summary>
        public event DelGLMouseEvent GLMoveEvent;

        /// <summary>
        /// 滑鼠放開事件
        /// </summary>
        public event DelGLMouseEvent GLMoveUp;

        /// <summary>
        /// 介面被點擊
        /// </summary>
        private void BeClick(object sender, EventArgs e)
        {
            /*發佈事件*/
            MouseEventArgs mouse = (MouseEventArgs)e;
            GLClickEvent?.Invoke(sender, new GLMouseEventArgs() { Position = ScreenToGL(mouse.X, mouse.Y) });
        }

        /// <summary>
        /// 介面被雙擊
        /// </summary>
        private void BeDoubleClick(object sender, EventArgs e)
        {
            /*發佈事件*/
            MouseEventArgs mouse = (MouseEventArgs)e;
            GLDoubleClickEvent?.Invoke(sender, new GLMouseEventArgs() { Position = ScreenToGL(mouse.X, mouse.Y) });
            /*執行滑鼠動作*/
            Mouse?.Click(ScreenToGL(mouse.X, mouse.Y));
        }

        /// <summary>
        /// 介面按鈕觸發
        /// </summary>
        private void BeKeyDown(object sender, KeyEventArgs e)
        {
            mMouse?.KeyDown(e.KeyCode);
        }

        /// <summary>
        /// 介面釋放按鈕
        /// </summary>
        private void BeKeyUp(object sender, KeyEventArgs e)
        {
            mMouse?.KeyUp();
        }

        /// <summary>
        /// 滑鼠按下
        /// </summary>
        private void BeMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            /*發佈事件*/
            MouseEventArgs mouse = (MouseEventArgs)e;
            GLDownEvent?.Invoke(sender, new GLMouseEventArgs() { Position = ScreenToGL(mouse.X, mouse.Y) });
            /*執行滑鼠動作*/
            Mouse?.Down(ScreenToGL(mouse.X, mouse.Y));
        }

        /// <summary>
        /// 滑鼠移動
        /// </summary>
        private void BeMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mMousePos = e.Location;
            /*發佈事件*/
            GLMoveEvent?.Invoke(sender, new GLMouseEventArgs() { Position = ScreenToGL(e.X, e.Y) });
            /*執行滑鼠動作*/
            Mouse?.Move(ScreenToGL(e.X, e.Y));
            /*顯示座標*/
            ShowTip(ScreenToGL(e.X, e.Y));
        }

        /// <summary>
        /// 滑鼠放開
        /// </summary>
        private void BeMouseUp(object sender, MouseEventArgs e)
        {
            /*執行滑鼠動作*/
            Mouse?.Up(ScreenToGL(e.X, e.Y));
            /*發佈事件*/
            GLMoveUp?.Invoke(sender, new GLMouseEventArgs() { Position = ScreenToGL(e.X, e.Y) });

        }

        /// <summary>
        /// 滑鼠滾輪
        /// </summary>
        private void BeMouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            IPair orgMousePoint = ScreenToGL(e.X, e.Y);
            Focus(orgMousePoint);
            if (e.Delta > 0) Zoom *= 1.2;
            if (e.Delta < 0) Zoom /= 1.2;
            MoveMap(orgMousePoint, ScreenToGL(e.X, e.Y));
        }

        /// <summary>
        /// 重新繪圖
        /// </summary>
        private void GDIDraw(object sender, RenderEventArgs args)
        {
            GDIDraw();
        }

        /// <summary>
        /// 介面拖曳
        /// </summary>
        private void MMouse_UITranslateEvent(object sender, UITranslateEventArgs e)
        {
            MoveMap(e.From, e.Target);
            e.Target = ScreenToGL(mMousePos.X, mMousePos.Y);
        }
    }
}
