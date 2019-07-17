using Geometry;
using SharpGL;

namespace GLCore
{
    /// <summary>
    /// 滑鼠動作-擦子
    /// </summary>
    internal class MouseEraser : Mouse, IMouseEraser
    {
        /// <summary>
        /// 要加入 Database 的物件
        /// </summary>
        private IEraser mEraser = null;

        private int mSize = 0;

        public MouseEraser(int size)
        {
            mSize = size;
            mEraser = FactoryMode.Factory.Eraser(FactoryMode.Factory.Area(-size / 2, -size / 2, size / 2, size / 2), "Eraser");
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
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public override void Draw(OpenGL gl)
        {
            mEraser?.Draw(gl);
        }

        /// <summary>
        /// 移動
        /// </summary>
        public override void Move(IPair pos)
        {
            base.Move(pos);

            mEraser.Data.Set(pos.X - mSize / 2, pos.Y - mSize / 2, pos.X + mSize / 2, pos.Y + mSize / 2);
            if (IsPress)
            {
                Database.ObstaclePointsGM.DataList.RemoveAll((item) => mEraser.Interference(item));
            }
        }

        /// <summary>
        /// 放開
        /// </summary>
        public override void Up(IPair pos)
        {
            base.Up(pos);
        }
    }
}
