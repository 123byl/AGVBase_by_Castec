using Geometry;
using SharpGL;
using System.Windows.Forms;

namespace GLCore
{
    /// <summary>
    /// 滑鼠動作-加入標示點
    /// </summary>
    internal class MouseAddTowerPair : Mouse, IMouseAddTowerPair
    {
        /// <summary>
        /// 決定方向
        /// </summary>
        private bool mDecidedTower = false;

        /// <summary>
        /// 要加入 Database 的物件
        /// </summary>
        private ISingle<ITowardPair> mObject = null;

        public MouseAddTowerPair(ISingle<ITowardPair> obj)
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

            mDecidedTower = true;
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

            if (mDecidedTower)
            {
                if (Key == Keys.ShiftKey)
                {
                    mObject.Data.Toward = mObject.Data.Position.LimitedAngle(pos);
                }
                else
                {
                    mObject.Data.Toward = pos.Subtraction(mObject.Data.Position).Angle();
                }
            }
            else
            {
                mObject.Data.Position = FactoryMode.Factory.Pair(pos);
            }
        }

        /// <summary>
        /// 放開
        /// </summary>
        public override void Up(IPair pos)
        {
            base.Up(pos);


            if (mObject is IParking)
            {
                var tmp = mObject as IParking;
                Database.ParkingGM.Add(Database.ID.GenerateID(), tmp);
            }
            if (mObject is IPower)
            {
                var tmp = mObject as IPower;
                Database.PowerGM.Add(Database.ID.GenerateID(), tmp);
            }
            if (mObject is IGoal)
            {
                var tmp = mObject as IGoal;
                Database.GoalGM.Add(Database.ID.GenerateID(), tmp);
            }
            mObject = null;
        }
    }
}
