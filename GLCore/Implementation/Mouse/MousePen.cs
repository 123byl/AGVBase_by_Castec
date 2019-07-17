using Geometry;
using SharpGL;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GLCore
{
    /// <summary>
    /// 滑鼠動作-擦子
    /// </summary>
    internal class MousePen : Mouse, IMousePen
    {
        /// <summary>
        /// 要加入 Database 的物件
        /// </summary>
        private IPen mPen = null;


        public MousePen(IPair UITranslate)
        {
            mPen = FactoryMode.Factory.Pen(FactoryMode.Factory.Line(), "Pen");
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

            mPen.Data.Begin = FactoryMode.Factory.Pair(pos);
            mPen.Data.End = FactoryMode.Factory.Pair(pos);
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public override void Draw(OpenGL gl)
        {
            if (IsPress) mPen?.Draw(gl);
        }

        /// <summary>
        /// 移動
        /// </summary>
        public override void Move(IPair pos)
        {
            if (Key == Keys.ShiftKey)
            {
                pos = mPen.Data.Begin.LimitedLine(pos);
            }
            base.Move(pos);

            if (IsPress)
            {
                mPen.Data.End = FactoryMode.Factory.Pair(pos);
            }
        }

        /// <summary>
        /// 放開
        /// </summary>
        public override void Up(IPair pos)
        {
            base.Up(pos);

            List<IPair> points = mPen.Data.ToPairs();
            if (points.Count != 0) Database.ObstaclePointsGM.DataList.AddRange(points);
        }
    }
}
