using AGVDefine;
using Geometry;
using SharpGL;
using System;

namespace GLCore
{
    /// <summary>
    /// 窄道
    /// </summary>
    [Serializable]
    internal class NarrowPassageWay : SingleLine, INarrowPassageWay
    {
        public NarrowPassageWay(string name) : base(name)
        {
            GLSetting = new GLSetting(EType.NarrowPassageWay);
        }

        public NarrowPassageWay(int x0, int y0, int x1, int y1, string name) : base(x0, x1, y0, y1, name)
        {
            GLSetting = new GLSetting(EType.NarrowPassageWay);
        }

        public NarrowPassageWay(ILine line, string name) : base(line, name)
        {
            GLSetting = new GLSetting(EType.NarrowPassageWay);
        }

        public NarrowPassageWay(IPair beg, IPair end, string name) : base(beg, end, name)
        {
            GLSetting = new GLSetting(EType.NarrowPassageWay);
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public override void Draw(OpenGL gl)
        {
            gl.PushMatrix();
            {
                IPair center = Data.Center();
                IAngle angle = Data.End.Subtraction(Data.Begin).Angle();
                int width = (int)Data.Length();
                gl.Translate(center.X, center.Y, 0);
                gl.Rotate(angle.Theta, 0, 0, 1);
                gl.TextureBmp(GLSetting.BmpName, FactoryMode.Factory.Pair(width, 30), GLSetting.MainColor, GLSetting.Type);
            }
            gl.PopMatrix();
        }
    }
}