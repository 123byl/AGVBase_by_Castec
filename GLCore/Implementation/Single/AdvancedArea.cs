using AGVDefine;
using Geometry;

namespace GLCore
{
    /// <summary>
    /// 禁止面
    /// </summary>
    internal class AdvancedArea : SingleArea, IAdvancedArea
    {
        public AdvancedArea(string name) : base(name)
        {
            GLSetting = new GLSetting(EType.AdvancedArea);
        }

        public AdvancedArea(int x0, int y0, int x1, int y1, string name) : base(x0, x1, y0, y1, name)
        {
            GLSetting = new GLSetting(EType.AdvancedArea);
        }

        public AdvancedArea(IArea area, string name) : base(area, name)
        {
            GLSetting = new GLSetting(EType.AdvancedArea);
        }

        public AdvancedArea(IPair min, IPair max, string name) : base(min, max, name)
        {
            GLSetting = new GLSetting(EType.AdvancedArea);
        }
    }
}