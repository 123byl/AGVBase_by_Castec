using AGVDefine;
using Geometry;
using System.Collections.Generic;

namespace GLCore
{
    /// <summary>
    /// 動態障礙點
    /// </summary>
    internal class DynamicObstaclePoints : MutiPair, IDynamicObstaclePoints
    {
        public DynamicObstaclePoints() : base()
        {
            GLSetting = new GLSetting(EType.DynamicObstaclePoints);
        }

        public DynamicObstaclePoints(IEnumerable<IPair> collection) : base(collection)
        {
            GLSetting = new GLSetting(EType.DynamicObstaclePoints);
        }

        public DynamicObstaclePoints(IPair item) : base(item)
        {
            GLSetting = new GLSetting(EType.DynamicObstaclePoints);
        }
    }
}