using AGVDefine;
using Geometry;
using System;

namespace GLCore
{
    /// <summary>
    /// 目標點
    /// </summary>
    [Serializable]
    internal class GoalDoor : SingleTowardPair, IGoalDoor
    {
        public GoalDoor(string name) : base(name)
        {
            GLSetting = new GLSetting(EType.GoalDoor);
        }

        public GoalDoor(int x, int y, double toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.GoalDoor);
        }

        public GoalDoor(int x, int y, IAngle toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.GoalDoor);
        }

        public GoalDoor(ITowardPair towardPair, string name) : base(towardPair, name)
        {
            GLSetting = new GLSetting(EType.GoalDoor);
        }
    }
}