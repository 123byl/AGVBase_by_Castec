using AGVDefine;
using Geometry;
using System;

namespace GLCore
{
    /// <summary>
    /// 目標點
    /// </summary>
    [Serializable]
    internal class GoalMagneticTracking : SingleTowardPair, IGoalMagneticTracking
    {
        public GoalMagneticTracking(string name) : base(name)
        {
            GLSetting = new GLSetting(EType.GoalMagneticTracking);
        }

        public GoalMagneticTracking(int x, int y, double toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.GoalMagneticTracking);
        }

        public GoalMagneticTracking(int x, int y, IAngle toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.GoalMagneticTracking);
        }

        public GoalMagneticTracking(ITowardPair towardPair, string name) : base(towardPair, name)
        {
            GLSetting = new GLSetting(EType.GoalMagneticTracking);
        }
    }
}