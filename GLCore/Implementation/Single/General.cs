using AGVDefine;
using Geometry;
using System;

namespace GLCore
{
    /// <summary>
    /// 目標點
    /// </summary>
    [Serializable]
    internal class General : SingleTowardPair, IGeneral
    {
        public General(string name) : base(name)
        {
            GLSetting = new GLSetting(EType.General);
        }

        public General(int x, int y, double toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.General);
        }

        public General(int x, int y, IAngle toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.General);
        }

        public General(ITowardPair towardPair, string name) : base(towardPair, name)
        {
            GLSetting = new GLSetting(EType.General);
        }
    }
}