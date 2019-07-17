using Geometry;
using SharpGL;
using System.Windows.Forms;

namespace GLCore
{
    /// <summary>
    /// 滑鼠動作-加入標示面
    /// </summary>
    internal class MouseAddArea : Mouse, IMouseAddArea
    {
        /// <summary>
        /// 決定最大值
        /// </summary>
        private bool mDecidedMax = false;

        /// <summary>
        /// 要加入 Database 的物件
        /// </summary>
        private ISingle<IArea> mObject = null;

        public MouseAddArea(ISingle<IArea> obj)
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

            mDecidedMax = true;
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public override void Draw(OpenGL gl)
        {
            mObject?.Draw(gl);
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

            if (mDecidedMax)
            {
                if (Key == Keys.ShiftKey)
                {
                    mObject.Data.Set(PressPos, PressPos.LimitedSquare(pos));
                }
                else
                {
                    mObject.Data.Set(PressPos, pos);
                }
            }
            else
            {
                mObject.Data.Set(pos, pos.Add(FactoryMode.Factory.Pair(100, 100)));
            }
        }

        /// <summary>
        /// 放開
        /// </summary>
        public override void Up(IPair pos)
        {
            base.Up(pos);

            if (mObject is IForbiddenArea)
            {
                var tmp = mObject as IForbiddenArea;
                Database.ForbiddenAreaGM.Add(Database.ID.GenerateID(), tmp);
            }

            if (mObject is IAdvancedArea)
            {
                var tmp = mObject as IAdvancedArea;
                Database.AdvancedAreaGM.Add(Database.ID.GenerateID(), tmp);
            }
            mObject = null;
        }
    }
}
