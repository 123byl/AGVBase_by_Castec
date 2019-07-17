using System;

namespace AGVDefine
{
    /// <summary>
    /// 圖案類型
    /// </summary>
    [Serializable]
    public enum EType : int
    {
        /// <summary>
        /// 頂層
        /// </summary>
        Top = 0,

        /// <summary>
        /// 底層
        /// </summary>
        Bottom = -100,

        /// <summary>
        /// 拖曳物件邊框
        /// </summary>
        DragBound = -1,

        /// <summary>
        /// 車
        /// </summary>
        AGV = -2,

        /// <summary>
        /// 雷射點
        /// </summary>
        LaserPoints = -3,

        /// <summary>
        /// 雷射強度
        /// </summary>
        LaserStrength = -4,

        /// <summary>
        /// 動態障礙點
        /// </summary>
        DynamicObstaclePoints = -5,

        /// <summary>
        /// 路徑
        /// </summary>
        Path = -6,

        /// <summary>
        /// 窄道
        /// </summary>
        NarrowLine = -21,

        /// <summary>
        /// 目標點
        /// </summary>
        Goal = -22,

        /// <summary>
        /// 充電站
        /// </summary>
        Power = -23,

        /// <summary>
        /// 窄道暫時停車區
        /// </summary>
        Parking = -24,

        /// <summary>
        /// 禁止線
        /// </summary>
        ForbiddenLine = -25,

        /// <summary>
        /// 禁止面
        /// </summary>
        ForbiddenArea = -26,

        /// <summary>
        /// 優先線
        /// </summary>
        AdvancedLine = -27,

        /// <summary>
        /// 優先面
        /// </summary>
        AdvancedArea = -28,

        /// <summary>
        /// 坐標軸
        /// </summary>
        Axis = -91,

        /// <summary>
        /// 障礙線
        /// </summary>
        ObstacleLines = -92,

        /// <summary>
        /// 障礙點
        /// </summary>
        ObstaclePoints = -93,

        /// <summary>
        /// 網格
        /// </summary>
        Grid = -99,

        /// <summary>
        /// 擦子
        /// </summary>
        Eraser = -90,

        /// <summary>
        /// 畫筆
        /// </summary>
        Pen = -89,
    }
}