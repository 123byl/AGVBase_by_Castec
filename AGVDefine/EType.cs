using System;

namespace AGVDefine
{
    /// <summary>
    /// 圖案圖層類型
    /// </summary>
    [Serializable]
    public enum EType : int
    {
        /// <summary>
        /// 頂層
        /// </summary>
        Top = 0,

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
        /// 充電站
        /// </summary>
        Power = -20,

        /// <summary>
        /// 充電站前的定位點
        /// </summary>
        ChargingDocking = -21,

        /// <summary>
        /// 皮帶式或滾輪式接貨定位點
        /// </summary>
        ConveyorDocking = -22,

        /// <summary>
        /// 窄道軌跡線
        /// </summary>
        NarrowLine = -30,

        /// <summary>
        /// 窄道通道
        /// </summary>
        NarrowPassageWay = -31,

        /// <summary>
        /// 磁帶條前後的目標點
        /// </summary>
        GoalMagneticTracking = -32,

        /// <summary>
        /// 磁帶條的目標點
        /// </summary>
        MagneticTracking = -33,

        /// <summary>
        /// 前磁帶條的目標點
        /// </summary>
        MagneticTrackingFront = -34,

        /// <summary>
        /// 後磁帶條的目標點
        /// </summary>
        MagneticTrackingRear = -35,

        /// <summary>
        /// 目標點
        /// </summary>
        Goal = -40,

        /// <summary>
        /// 廣義目標
        /// </summary>
        General = -41,

        /// <summary>
        /// 廣義目標點
        /// </summary>
        GoalGeneral = -42,

        /// <summary>
        /// 緩衝目標點
        /// </summary>
        GoalStandBy = -43,

        /// <summary>
        /// 門目標點
        /// </summary>
        GoalDoor = -44,

        /// <summary>
        /// Jacking Mechanism 頂昇機械昇目標點
        /// </summary>
        GoalRiseUp = -45,

        /// <summary>
        /// Jacking Mechanism 頂昇機械降目標點
        /// </summary>
        GoalRiseDown = -46,

        /// <summary>
        /// 一般正規目標點
        /// </summary>
        GoalNormal = -47,

        /// <summary>
        /// 一般正規目標點
        /// </summary>
        GoalBuffer = -48,

        /// <summary>
        /// 窄道暫時停車區
        /// </summary>
        Parking = -49,

        /// <summary>
        /// 禁止線
        /// </summary>
        ForbiddenLine = -50,

        /// <summary>
        /// 禁止面
        /// </summary>
        ForbiddenArea = -55,

        /// <summary>
        /// 優先線
        /// </summary>
        AdvancedLine = -60,

        /// <summary>
        /// 優先面
        /// </summary>
        AdvancedArea = -65,

        /// <summary>
        /// 畫筆
        /// </summary>
        Pen = -89,

        /// <summary>
        /// 擦子
        /// </summary>
        Eraser = -90,

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
        /// 底層
        /// </summary>
        Bottom = -100,
    }
}