using AGVDefine;
using Geometry;
using SharpGL;
using System.Collections.Generic;

namespace GLCore
{
    /// <summary>
    /// 雷射點
    /// </summary>
    internal class LaserPoints : MutiPair, ILaserPoints
    {
        public LaserPoints() : base()
        {
            GLSetting = new GLSetting(EType.LaserPoints);
        }

        public LaserPoints(IEnumerable<IPair> collection) : base(collection)
        {
            GLSetting = new GLSetting(EType.LaserPoints);
        }

        public LaserPoints(IPair item) : base(item)
        {
            GLSetting = new GLSetting(EType.LaserPoints);
        }

        /// <summary>
        /// 雷射中心
        /// </summary>
        public IPair Center { get; } = FactoryMode.Factory.Pair(0, 0);

        /// <summary>
        /// 是否顯示雷射中心
        /// </summary>
        public bool ShowCenter { get; set; } = false;

        /// <summary>
        /// 繪圖
        /// </summary>
        public override void Draw(OpenGL gl)
        {
            if (DataList.Count == 0) return;
            base.DrawAsLine(gl);
            base.Draw(gl);
            float z = (int)GLSetting.Type - 0.1f;
            if (!ShowCenter) return;

            // 畫雷射中心與雷射點的連線
            gl.Begin(OpenGL.GL_LINES);
            {
                gl.Vertex(Center.X, Center.Y, z);
                gl.Vertex(DataList[0].X, DataList[0].Y, z);
                gl.Vertex(Center.X, Center.Y, z);
                gl.Vertex(DataList[DataList.Count - 1].X, DataList[DataList.Count - 1].Y, z);
            }
            gl.End();
        }
    }
}