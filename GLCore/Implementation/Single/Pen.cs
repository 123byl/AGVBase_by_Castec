using AGVDefine;
using Geometry;

namespace GLCore
{
    /// <summary>
    /// 畫筆
    /// </summary>
    internal class Pen : SingleLine, IPen
    {
        public Pen(string name) : base(name)
        {
            GLSetting = new GLSetting(EType.Pen);
        }

        public Pen(int x0, int y0, int x1, int y1, string name) : base(x0, x1, y0, y1, name)
        {
            GLSetting = new GLSetting(EType.Pen);
        }

        public Pen(ILine line, string name) : base(line, name)
        {
            GLSetting = new GLSetting(EType.Pen);
        }

        public Pen(IPair beg, IPair end, string name) : base(beg, end, name)
        {
            GLSetting = new GLSetting(EType.Pen);
        }
    }
}
