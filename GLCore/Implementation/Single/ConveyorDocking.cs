using AGVDefine;
using Geometry;
using System;

namespace GLCore
{
    /// <summary>
    /// 皮帶式或滾輪式產品產線流道對接
    /// </summary>
    [Serializable]
    internal class ConveyorDocking : SingleTowardPair, IConveyorDocking
    {
        public ConveyorDocking(string name) : base(name)
        {
            GLSetting = new GLSetting(EType.ConveyorDocking);
        }

        public ConveyorDocking(int x, int y, double toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.ConveyorDocking);
        }

        public ConveyorDocking(int x, int y, IAngle toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.ConveyorDocking);
        }

        public ConveyorDocking(ITowardPair towardPair, string name) : base(towardPair, name)
        {
            GLSetting = new GLSetting(EType.ConveyorDocking);
        }
    }
}