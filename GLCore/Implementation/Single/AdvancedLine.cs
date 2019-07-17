using AGVDefine;
using Geometry;
using System;

namespace GLCore
{
    /// <summary>
    /// 禁止線
    /// </summary>
    [Serializable]
    internal class AdvancedLine : SingleLine, IAdvancedLine
    {
        public AdvancedLine(string name) : base(name)
        {
            GLSetting = new GLSetting(EType.AdvancedLine);
        }

        public AdvancedLine(int x0, int y0, int x1, int y1, string name) : base(x0, x1, y0, y1, name)
        {
            GLSetting = new GLSetting(EType.AdvancedLine);
        }

        public AdvancedLine(ILine line, string name) : base(line, name)
        {
            GLSetting = new GLSetting(EType.AdvancedLine);
        }

        public AdvancedLine(IPair beg, IPair end, string name) : base(beg, end, name)
        {
            GLSetting = new GLSetting(EType.AdvancedLine);
        }
    }
}