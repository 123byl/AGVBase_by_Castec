using AGVDefine;
using Geometry;
using System.Collections.Generic;

namespace GLCore
{
    /// <summary>
    /// 障礙線
    /// </summary>
    internal class ObstacleLines : MutiLine, IObstacleLines
    {
        public ObstacleLines() : base()
        {
            GLSetting = new GLSetting(EType.ObstacleLines);
        }

        public ObstacleLines(IEnumerable<ILine> collection) : base(collection)
        {
            GLSetting = new GLSetting(EType.ObstacleLines);
        }

        public ObstacleLines(ILine item) : base(item)
        {
            GLSetting = new GLSetting(EType.ObstacleLines);
        }
    }
}