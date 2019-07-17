using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGVDefine
{
    /// <summary>
    /// 地圖檔常數定義
    /// </summary>
    public static class MapFile
    {
        /// <summary>
        /// 地圖檔開檔/寫檔過濾條件
        /// </summary>
        public const string Filter = "Castec AGV Map|*.map";

        /// <summary>
        /// 使用來分解 *.map 字串內容的分解字元
        /// </summary>
        public const char SplitChar = ',';

        /// <summary>
        /// 資料標頭
        /// </summary>
        public static class Head
        {
            /// <summary>
            /// 地圖檢查碼
            /// </summary>
            public const string CheckCode = "Check Code:";

            /// <summary>
            /// 禁止區列表
            /// </summary>
            public const string ForbiddenArea = "Forbidden Area";

            /// <summary>
            /// 禁止線列表
            /// </summary>
            public const string ForbiddenLine = "Forbidden Line";

            /// <summary>
            /// 優先區列表
            /// </summary>
            public const string AdvancedArea = "Advanced Area";

            /// <summary>
            /// 優先線列表
            /// </summary>
            public const string AdvancedLine = "Advanced Line";

            /// <summary>
            /// 目標點列表
            /// </summary>
            public const string GoalList = "Goal List";

            /// <summary>
            /// 地圖最大值
            /// </summary>
            public const string MaxPosition = "Maximum Position:";

            /// <summary>
            /// 地圖最小值
            /// </summary>
            public const string MinPosition = "Minimum Position:";

            /// <summary>
            /// 障礙點列表
            /// </summary>
            public const string ObstaclePoints = "Obstacle Points";

            /// <summary>
            /// 充電站列表
            /// </summary>
            public const string PowerList = "Power List";
        }
    }
}
