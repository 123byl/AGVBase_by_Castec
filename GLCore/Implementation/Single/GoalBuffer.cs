using AGVDefine;
using Geometry;
using System;

namespace GLCore
{
    /// <summary>
    /// 窄道暫時停車區
    /// </summary>
    [Serializable]
    internal class GoalBuffer : SingleTowardPair, IGoalBuffer
    {
        public GoalBuffer(string name) : base(name)
        {
            GLSetting = new GLSetting(EType.GoalBuffer);
        }

        public GoalBuffer(int x, int y, double toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.GoalBuffer);
        }

        public GoalBuffer(int x, int y, IAngle toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.GoalBuffer);
        }

        public GoalBuffer(ITowardPair towardPair, string name) : base(towardPair, name)
        {
            GLSetting = new GLSetting(EType.GoalBuffer);
        }
    }
}