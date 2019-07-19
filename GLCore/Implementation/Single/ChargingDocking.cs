using AGVDefine;
using Geometry;
using System;

namespace GLCore
{
    /// <summary>
    /// 充電站
    /// </summary>
    [Serializable]
    internal class ChargingDocking : SingleTowardPair, IChargingDocking
    {
        public ChargingDocking(string name) : base(name)
        {
            GLSetting = new GLSetting(EType.ChargingDocking);
        }

        public ChargingDocking(int x, int y, double toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.ChargingDocking);
        }

        public ChargingDocking(int x, int y, IAngle toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.ChargingDocking);
        }

        public ChargingDocking(ITowardPair towardPair, string name) : base(towardPair, name)
        {
            GLSetting = new GLSetting(EType.ChargingDocking);
        }
    }
}