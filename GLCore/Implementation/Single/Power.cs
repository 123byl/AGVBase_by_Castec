using AGVDefine;
using Geometry;
using System;

namespace GLCore
{
    /// <summary>
    /// 充電站
    /// </summary>
    [Serializable]
    internal class Power : SingleTowardPair, IPower
    {
        public Power(string name) : base(name)
        {
            GLSetting = new GLSetting(EType.Power);
        }

        public Power(int x, int y, double toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.Power);
        }

        public Power(int x, int y, IAngle toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.Power);
        }

        public Power(ITowardPair towardPair, string name) : base(towardPair, name)
        {
            GLSetting = new GLSetting(EType.Power);
        }
    }
}