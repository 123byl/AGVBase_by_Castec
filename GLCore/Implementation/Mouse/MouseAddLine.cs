using Geometry;
using SharpGL;
using System.Windows.Forms;

namespace GLCore
{
    /// <summary>
    /// 滑鼠動作-加入標示線
    /// </summary>
    internal class MouseAddLine : Mouse, IMouseAddLine
    {
        /// <summary>
        /// 決定末端點
        /// </summary>
        private bool mDecidedEnd = false;

        /// <summary>
        /// 要加入 Database 的物件
        /// </summary>
        private ISingle<ILine> mObject = null;

        public MouseAddLine(ISingle<ILine> obj)
        {
            mObject = obj;
        }

        /// <summary>
        /// 點擊
        /// </summary>
        public override void Click(IPair pos)
        {
            base.Click(pos);
        }

        /// <summary>
        /// 按下
        /// </summary>
        public override void Down(IPair pos)
        {
            base.Down(pos);

            mDecidedEnd = true;
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public override void Draw(OpenGL gl)
        {
            if (IsPress) mObject?.Draw(gl);
        }

        /// <summary>
        /// 按下按鈕
        /// </summary>
        public override void KeyDown(Keys key)
        {
            base.KeyDown(key);
            Move(CurrentPos);
        }

        /// <summary>
        /// 放開按鈕
        /// </summary>
        public override void KeyUp()
        {
            base.KeyUp();
            Move(CurrentPos);
        }

        /// <summary>
        /// 移動
        /// </summary>
        public override void Move(IPair pos)
        {
            base.Move(pos);

            if (mObject == null)
            {
                MoveUI();
                return;
            }

            if (mDecidedEnd)
            {
                if (Key == Keys.ShiftKey)
                {
                    mObject.Data.End = mObject.Data.Begin.LimitedLine(pos);
                }
                else
                {
                    mObject.Data.End = FactoryMode.Factory.Pair(pos);
                }
            }
            else
            {
                mObject.Data.Begin = FactoryMode.Factory.Pair(pos);
                mObject.Data.End = FactoryMode.Factory.Pair(pos);
            }
        }

        /// <summary>
        /// 放開
        /// </summary>
        public override void Up(IPair pos)
        {
            base.Up(pos);

            if (mObject is IForbiddenLine)
            {
                var tmp = mObject as IForbiddenLine;
                Database.ForbiddenLineGM.Add(Database.ID.GenerateID(), tmp);
            }
            if (mObject is IAdvancedLine)
            {
                var tmp = mObject as IAdvancedLine;
                Database.AdvancedLineGM.Add(Database.ID.GenerateID(), tmp);
            }
            if (mObject is INarrowLine)
            {
                var tmp = mObject as INarrowLine;
                Database.NarrowLineGM.Add(Database.ID.GenerateID(), tmp);
            }

            mObject = null;
        }
    }
}
