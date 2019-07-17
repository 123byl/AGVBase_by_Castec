using AGVDefine;
using Geometry;
using SharpGL;
using System.Collections.Generic;

namespace GLCore
{
    /// <summary>
    /// 路徑
    /// </summary>
    internal class Path : MutiPair, IPath
    {
        public Path() : base()
        {
            GLSetting = new GLSetting(EType.Path);
        }

        public Path(IEnumerable<IPair> collection) : base(collection)
        {
            GLSetting = new GLSetting(EType.Path);
        }

        public Path(IPair item) : base(item)
        {
            GLSetting = new GLSetting(EType.Path);
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public override void Draw(OpenGL gl)
        {
            base.DrawAsLine(gl);
            base.Draw(gl);
        }
    }
}
