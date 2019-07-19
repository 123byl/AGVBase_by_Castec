using Geometry;
using GLCore;
using SharpGL;
using System;
using System.Windows.Forms;

namespace GLUI
{
    internal partial class Scene : IScene, IDisposable
    {
        private readonly OpenGLControl mOpenGLControl = null;

        private readonly ToolTip mToolTip = new ToolTip();

        /// <summary>
        /// 滑鼠動作
        /// </summary>
        private IMouse mMouse = null;

        private IPair mMousePosition = FactoryMode.Factory.Pair();

        public Scene(OpenGLControl OpenGLControl)
        {
            SetSelectMode();
            mOpenGLControl = OpenGLControl;
            mOpenGLControl.MouseWheel += BeMouseWheel;
            mOpenGLControl.MouseDown += BeMouseDown;
            mOpenGLControl.MouseMove += BeMouseMove;
            mOpenGLControl.MouseUp += BeMouseUp;
            mOpenGLControl.OpenGLDraw += GDIDraw;
            mOpenGLControl.Click += BeClick;
            mOpenGLControl.DoubleClick += BeDoubleClick;
            mOpenGLControl.KeyDown += BeKeyDown;
            mOpenGLControl.KeyUp += BeKeyUp;
        }

        /// <summary>
        /// 獲得控制底層
        /// </summary>
        public IScene BaseCtrl => this;

        /// <summary>
        /// 滑鼠是否按下
        /// </summary>
        public bool IsMousePress { get; private set; } = false;

        /// <summary>
        /// 是否畫坐標軸
        /// </summary>
        public bool ShowAxis { get; set; } = true;

        /// <summary>
        /// 是否顯示
        /// </summary>
        public bool ShowFPS { get { return mOpenGLControl.DrawFPS; } set { mOpenGLControl.DrawFPS = value; } }

        /// <summary>
        /// 是否畫網格
        /// </summary>
        public bool ShowGrid { get; set; } = true;

        /// <summary>
        /// 是否顯示物件名稱
        /// </summary>
        public bool ShowNames { get; set; } = true;

        private OpenGL mGL { get { return mOpenGLControl.OpenGL; } }

        /// <summary>
        /// 滑鼠動作
        /// </summary>
        private IMouse Mouse
        {
            get
            {
                return mMouse;
            }
            set
            {
                mMouse?.Release();
                mMouse = value; mMouse.UITranslateEvent += MMouse_UITranslateEvent;
            }
        }

        /// <summary>
        /// 新地圖(刪除所有共用 Database 並釋放拖曳對象)
        /// </summary>
        public void NewMap()
        {
            SetSelectMode();
            Database.ClearAll();
        }

        /// <summary>
        /// 加入標示物
        /// </summary>
        public void SetAddMode(object obj)
        {
            if (obj is ISingle<ITowardPair>)
                SetAddMode((ISingle<ITowardPair>)obj);
            else if (obj is ISingle<ILine>)
                SetAddMode((ISingle<ILine>)obj);
            else if (obj is ISingle<IArea>)
                SetAddMode((ISingle<IArea>)obj);
        }

        /// <summary>
        /// 加入標示物
        /// </summary>
        public void SetAddMode(ISingle<ITowardPair> obj)
        {
            Mouse = FactoryMode.Factory.MouseAddTowerPair(obj);
        }

        /// <summary>
        /// 加入標示線
        /// </summary>
        public void SetAddMode(ISingle<ILine> obj)
        {
            Mouse = FactoryMode.Factory.MouseAddLine(obj);
        }

        /// <summary>
        /// 加入標示面
        /// </summary>
        public void SetAddMode(ISingle<IArea> obj)
        {
            Mouse = FactoryMode.Factory.MouseAddArea(obj);
        }


        /// <summary>
        /// 將滑鼠設定為拖曳模式
        /// </summary>
        public void SetDragMode()
        {
            Mouse = FactoryMode.Factory.MouseDrag(DragAreaEvent, DragLineEvent, DragTowerPairEvent, ClickAreaEvent, ClickLineEvent, ClickTowerPairEvent);
        }

        /// <summary>
        /// 將滑鼠設定為擦子模式
        /// </summary>
        public void SetEraserMode(int size)
        {
            Mouse = FactoryMode.Factory.MouseEraser(size);
        }

        /// <summary>
        /// 將滑鼠設定為插入地圖模式
        /// </summary>
        public void SetInsertMapMode(string filename, IMouseInsertPanel panel)
        {
            Mouse = FactoryMode.Factory.MouseInsert(filename, panel);
            panel?.Show();
        }

        /// <summary>
        /// 將滑鼠設定為畫筆模式
        /// </summary>
        public void SetPenMode()
        {
            Mouse = FactoryMode.Factory.MousePen(Translate);
        }

        /// <summary>
        /// 將滑鼠設定為選擇模式
        /// </summary>
        public void SetSelectMode()
        {
            Mouse = FactoryMode.Factory.MouseSelect(DragAreaEvent, DragLineEvent, DragTowerPairEvent, ClickAreaEvent, ClickLineEvent, ClickTowerPairEvent);
        }

        /// <summary>
        ///  畫資料庫
        /// </summary>
        private void DrawDataBase()
        {
            Database.DynamicObstaclePointsGM.Draw(mGL);
            Database.ObstacleLinesGM.Draw(mGL);
            Database.ObstaclePointsGM.Draw(mGL);
            Database.AdvancedAreaGM.Draw(mGL);
            Database.AdvancedLineGM.Draw(mGL);
            Database.ForbiddenAreaGM.Draw(mGL);
            Database.ForbiddenLineGM.Draw(mGL);

            Database.NarrowLineGM.Draw(mGL);
            Database.NarrowPassageWayGM.Draw(mGL);
            Database.MagneticTrackingGM.Draw(mGL);

            Database.ParkingGM.Draw(mGL);
            Database.GoalBufferGM.Draw(mGL);

            Database.PowerGM.Draw(mGL);
            Database.ChargingDockingGM.Draw(mGL);
            Database.ConveyorDockingGM.Draw(mGL);

            Database.GeneralGM.Draw(mGL);
            Database.GoalGM.Draw(mGL);
            Database.GoalGeneralGM.Draw(mGL);
            Database.GoalStandByGM.Draw(mGL);
            Database.GoalDoorGM.Draw(mGL);
            Database.GoalRiseUpGM.Draw(mGL);
            Database.GoalRiseDownGM.Draw(mGL);
            Database.GoalNormalGM.Draw(mGL);
            Database.GoalMagneticTrackingGM.Draw(mGL);
            Database.MagneticTrackingFrontGM.Draw(mGL);
            Database.MagneticTrackingRearGM.Draw(mGL);

            Database.AGVGM.Draw(mGL);
        }

        /// <summary>
        /// 畫控制對象
        /// </summary>
        private void DrawDragManager()
        {
            Mouse?.Draw(mGL);
        }


        /// <summary>
        /// 繪圖
        /// </summary>
        private void GDIDraw()
        {
            InitialDraw();
            if (ShowGrid)
                DrawGrid();
            if (ShowAxis)
                DrawAxis();
            DrawDataBase();
            DrawDragManager();
            if (ShowNames)
                DrawNames();
        }

        /// <summary>
        /// 初始化畫布
        /// </summary>
        private void InitialDraw()
        {
            mGL.ClearColor(BackgroundColor.R / 255.0f,
                                     BackgroundColor.G / 255.0f,
                                     BackgroundColor.B / 255.0f,
                                     BackgroundColor.A / 255.0f);
            mGL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            if (ShowNames)
                mGL.ClearText();
            // 投影矩陣
            mGL.MatrixMode(OpenGL.GL_PROJECTION);
            // MatrixMode 後要執行 LoadIdentity
            mGL.LoadIdentity();
            // 畫布的大小（正交）

            mGL.Ortho(-Zoom * mOpenGLControl.Width / 2,
                               Zoom * mOpenGLControl.Width / 2,
                              -Zoom * mOpenGLControl.Height / 2,
                               Zoom * mOpenGLControl.Height / 2, -10, 100);
            // 繪圖矩陣
            mGL.MatrixMode(OpenGL.GL_MODELVIEW);
            // MatrixMode 後要執行 LoadIdentity
            mGL.LoadIdentity();
            //線條去鋸齒
            // gl.Enable(OpenGL.GL_LINE_SMOOTH);

            // 點去鋸齒
            // gl.Enable(OpenGL.GL_POINT_SMOOTH);

            // 多邊形去鋸齒
            // gl.Enable(OpenGL.GL_POLYGON_SMOOTH);

            //// 多邊形去鋸齒
            // gl.Enable(OpenGL.GL_SMOOTH);

            //深度測試
            mGL.Enable(OpenGL.GL_DEPTH_TEST);

            //設定混和模式
            mGL.Enable(OpenGL.GL_BLEND);
            mGL.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);

            mGL.CullFace(OpenGL.GL_CCW);
            // 設定座標原點
            mGL.Translate(Translate.X, Translate.Y, 0.0f);
        }

        /// <summary>
        /// 將現實座標從 from 移動到 target
        /// </summary>
        private void MoveMap(IPair from, IPair target)
        {
            Translate.X += (target.X - from.X);
            Translate.Y += (target.Y - from.Y);
        }

        private void PushNames<T>(ISafetyDictionary<T> dictionary) where T : ISingle<ITowardPair>, IDragable
        {
            dictionary.SafetyForLoop((id, value) =>
            {
                mGL.PushText(value.Data.Position, value.Name);
            });
        }

        private void ShowTip(IPair pos)
        {
            string msg = string.Format("({0},{1})", pos.X, pos.Y);
            string orgMsg = mToolTip.GetToolTip(mOpenGLControl);
            if (msg != orgMsg)
            {
                mToolTip.SetToolTip(mOpenGLControl, msg);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 偵測多餘的呼叫

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放 Unmanaged 資源的程式碼時，才覆寫完成項。
        ~Scene()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(false);
        }

        // 加入這個程式碼的目的在正確實作可處置的模式。
        public void Dispose()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 處置 Managed 狀態 (Managed 物件)。
                    mToolTip.Dispose();
                }

                // TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。

                disposedValue = true;
            }
        }
        #endregion IDisposable Support
    }
}