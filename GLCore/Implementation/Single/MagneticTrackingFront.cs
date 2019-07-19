using AGVDefine;
using Geometry;
using System;

namespace GLCore
{
    /// <summary>
    /// 目標點
    /// </summary>
    [Serializable]
    internal class MagneticTrackingFront : SingleTowardPair, IMagneticTrackingFront
    {
        public MagneticTrackingFront(string name) : base(name)
        {
            GLSetting = new GLSetting(EType.MagneticTrackingFront);
        }

        public MagneticTrackingFront(int x, int y, double toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.MagneticTrackingFront);
        }

        public MagneticTrackingFront(int x, int y, IAngle toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.MagneticTrackingFront);
        }

        public MagneticTrackingFront(ITowardPair towardPair, string name) : base(towardPair, name)
        {
            GLSetting = new GLSetting(EType.MagneticTrackingFront);
        }
    }
}