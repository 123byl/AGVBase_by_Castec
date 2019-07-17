using AGVDefine;
using Geometry;
using GLCore.UI;
using SharpGL;
using System.Windows.Forms;

namespace GLCore
{
    /// <summary>
    /// 滑鼠動作-拖曳
    /// </summary>
    internal class MouseDrag : Mouse, IMouseDrag
    {
        /// <summary>
        /// 控制對象所在的字典集合
        /// </summary>
        private ISafetyDictionary mDictionary = null;

        /// <summary>
        /// 拖曳控制器
        /// </summary>
        private IDragManager mDragManager = FactoryMode.Factory.DragManager();

        /// <summary>
        /// 建構子
        /// </summary>
        public MouseDrag(bool canDrag, DelDragAreaEvent DragAreaEvent, DelDragLineEvent DragLineEvent, DelDragTowardPairEvent DragTowerPairEvent, DelClickAreaEvent ClickAreaEvent, DelClickLineEvent ClickLineEvent, DelClickTowardPairEvent ClickTowardPairEvent)
        {
            this.DragAreaEvent += DragAreaEvent;
            this.DragLineEvent += DragLineEvent;
            this.DragTowerPairEvent += DragTowerPairEvent;
            this.ClickAreaEvent += ClickAreaEvent;
            this.ClickLineEvent += ClickLineEvent;
            this.ClickTowerPairEvent += ClickTowardPairEvent;
            this.CanDrag = canDrag;
        }

        /// <summary>
        /// 當使用者點擊 Area 型態的物件時的事件
        /// </summary>
        public event DelClickAreaEvent ClickAreaEvent = null;

        /// <summary>
        /// 當使用者點擊 Line 型態的物件時的事件
        /// </summary>
        public event DelClickLineEvent ClickLineEvent = null;

        /// <summary>
        /// 當使用者點擊 ITowerPair 型態的物件時的事件
        /// </summary>
        public event DelClickTowardPairEvent ClickTowerPairEvent = null;

        /// <summary>
        /// 當使用者拖曳 Area 型態的物件時的事件
        /// </summary>
        public event DelDragAreaEvent DragAreaEvent = null;

        /// <summary>
        /// 當使用者拖曳 Line 型態的物件時的事件
        /// </summary>
        public event DelDragLineEvent DragLineEvent = null;

        /// <summary>
        /// 當使用者拖曳 ITowerPair 型態的物件時的事件
        /// </summary>
        public event DelDragTowardPairEvent DragTowerPairEvent = null;

        /// <summary>
        /// 是否可拖曳
        /// </summary>
        public bool CanDrag { get; set; }

        /// <summary>
        /// 拖曳對象 ID
        /// </summary>
        public uint DragTargetID { get; private set; }

        /// <summary>
        /// 點擊
        /// </summary>
        public override void Click(IPair pos)
        {
            base.Click(pos);

            mDragManager.DragTaeget = FindDragTarget(pos);
            if (mDragManager.DragTaeget == null) DragTargetID = 0;
            if (mDragManager.DragTaeget is ISingle<ITowardPair>) ClickTowerPairEvent?.Invoke(this, new TowerPairEventArgs((ISingle<ITowardPair>)mDragManager.DragTaeget, DragTargetID));
            if (mDragManager.DragTaeget is ISingle<ILine>) ClickLineEvent?.Invoke(this, new LineEventArgs((ISingle<ILine>)mDragManager.DragTaeget, DragTargetID));
            if (mDragManager.DragTaeget is ISingle<IArea>) ClickAreaEvent?.Invoke(this, new AreaEventArgs((ISingle<IArea>)mDragManager.DragTaeget, DragTargetID));
        }

        /// <summary>
        /// 按下
        /// </summary>
        public override void Down(IPair pos)
        {
            base.Down(pos);

            if (mDragManager.Status == EDragStatus.Ready)
            {
                mDragManager.TakeControl(CurrentPos);
            }
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public override void Draw(OpenGL gl)
        {
            mDragManager.Draw(gl);
        }

        /// <summary>
        /// 按下按鈕
        /// </summary>
        public override void KeyDown(Keys key)
        {
            base.KeyDown(key);
            if (key == Keys.Delete && DragTargetID != 0)
            {
                mDragManager.DragTaeget = null;
                mDictionary?.Remove(DragTargetID);
                DragTargetID = 0;
            }
            else if (key == Keys.F1 && DragTargetID != 0 && mDragManager.DragTaeget is ISingle<ITowardPair>)
            {
                new SingleTowardPairEditor(mDragManager.DragTaeget as ISingle<ITowardPair>);
            }
        }

        /// <summary>
        /// 移動
        /// </summary>
        public override void Move(IPair pos)
        {
            base.Move(pos);


            if (CanDrag && IsPress && mDragManager.Status == EDragStatus.Dragging)
            {
                mDragManager.Drag(CurrentPos);
                if (mDragManager.DragTaeget == null) DragTargetID = 0;
                if (mDragManager.DragTaeget is ISingle<ITowardPair>) DragTowerPairEvent?.Invoke(this, new TowerPairEventArgs((ISingle<ITowardPair>)mDragManager.DragTaeget, DragTargetID));
                if (mDragManager.DragTaeget is ISingle<ILine>) DragLineEvent?.Invoke(this, new LineEventArgs((ISingle<ILine>)mDragManager.DragTaeget, DragTargetID));
                if (mDragManager.DragTaeget is ISingle<IArea>) DragAreaEvent?.Invoke(this, new AreaEventArgs((ISingle<IArea>)mDragManager.DragTaeget, DragTargetID));
            }
            else
            {
                MoveUI();
            }
        }

        /// <summary>
        /// 放開
        /// </summary>
        public override void Up(IPair pos)
        {
            base.Up(pos);

            mDragManager.ReleaseControl();
        }

        /// <summary>
        /// 尋找控制對象
        /// </summary>
        private IDragable FindAreaDragTarget<T>(ISafetyDictionary<T> dictionary, IPair pos) where T : ISingle<IArea>, IDragable
        {
            IDragable res = null;
            dictionary.SafetyForLoop((id, value) =>
            {
                if (value.Interference(pos))
                {
                    DragTargetID = id;
                    res = value;
                }
            });
            return res;
        }

        /// <summary>
        /// 尋找控制對象
        /// </summary>
        private IDragable FindDragTarget(IPair pos)
        {
            IDragable res = null;
            res = FindTowardPairDragTarget(Database.AGVGM, pos);
            if (res != null) { mDictionary = Database.AGVGM; return res; }
            res = FindTowardPairDragTarget(Database.PowerGM, pos);
            if (res != null) { mDictionary = Database.PowerGM; return res; }
            res = FindTowardPairDragTarget(Database.GoalGM, pos);
            if (res != null) { mDictionary = Database.GoalGM; return res; }
            res = FindTowardPairDragTarget(Database.ParkingGM, pos);
            if (res != null) { mDictionary = Database.ParkingGM; return res; }
            res = FindLineDragTarget(Database.ForbiddenLineGM, pos);
            if (res != null) { mDictionary = Database.ForbiddenLineGM; return res; }
            res = FindLineDragTarget(Database.AdvancedLineGM, pos);
            if (res != null) { mDictionary = Database.AdvancedLineGM; return res; }
            res = FindLineDragTarget(Database.NarrowLineGM, pos);
            if (res != null) { mDictionary = Database.NarrowLineGM; return res; }
            res = FindAreaDragTarget(Database.ForbiddenAreaGM, pos);
            if (res != null) { mDictionary = Database.ForbiddenAreaGM; return res; }
            res = FindAreaDragTarget(Database.AdvancedAreaGM, pos);
            if (res != null) { mDictionary = Database.AdvancedAreaGM; return res; }
            return res;
        }

        /// <summary>
        /// 尋找控制對象
        /// </summary>
        private IDragable FindLineDragTarget<T>(ISafetyDictionary<T> dictionary, IPair pos) where T : ISingle<ILine>, IDragable
        {
            IDragable res = null;
            dictionary.SafetyForLoop((id, value) =>
            {
                if (value.Interference(pos))
                {
                    DragTargetID = id;
                    res = value;
                }
            });
            return res;
        }

        /// <summary>
        /// 尋找控制對象
        /// </summary>
        private IDragable FindTowardPairDragTarget<T>(ISafetyDictionary<T> dictionary, IPair pos) where T : ISingle<ITowardPair>, IDragable
        {
            IDragable res = null;
            dictionary.SafetyForLoop((id, value) =>
            {
                if (value.Interference(pos))
                {
                    DragTargetID = id;
                    res = value;
                }
            });
            return res;
        }
    }
}
