using AGVDefine;
using Geometry;
using System.Collections.Generic;

namespace GLCore
{
    /// <summary>
    /// 雷射強度
    /// </summary>
    internal class LaserStrength : MutiPair, ILaserStrength
    {
        public LaserStrength() : base()
        {
            GLSetting = new GLSetting(EType.LaserStrength);
        }

        public LaserStrength(IEnumerable<IPair> collection) : base(collection)
        {
            GLSetting = new GLSetting(EType.LaserStrength);
        }

        public LaserStrength(IPair item) : base(item)
        {
            GLSetting = new GLSetting(EType.LaserStrength);
        }
    }
}