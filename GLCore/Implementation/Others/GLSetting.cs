using AGVDefine;
using Geometry;
using System;

namespace GLCore
{
    /// <summary>
    /// OpenGL 繪圖設定
    /// </summary>
    [Serializable]
    internal class GLSetting : IGLSetting
    {
        private Color mMainColor = new Color(System.Drawing.Color.Black);

        private IPair mSize = FactoryMode.Factory.Pair(500, 500);

        private Color mSubColor = new Color(System.Drawing.Color.Red);

        public GLSetting()
        {
        }

        /// <summary>
        /// 根據型態設定 GL 繪圖參數
        /// </summary>
        public GLSetting(EType type)
        {
            Type = type;
            switch (type)
            {
                case EType.Top:
                    return;

                case EType.Bottom:
                    return;

                case EType.DragBound:
                    MainColor = new Color(System.Drawing.Color.Red);
                    LineStyle = ELineStyle._1100110011001100;
                    LineWidth = 3;
                    return;

                case EType.AGV:
                    BmpName = "AGV";
                    TowardLength = 0;
                    SubColor = new Color(System.Drawing.Color.Purple, 200);
                    return;

                case EType.General:
                    BmpName = "General";
                    return;

                case EType.Goal:
                    BmpName = "Goal";
                    return;

                case EType.GoalGeneral:
                    BmpName = "GoalGeneral";
                    return;

                case EType.GoalStandBy:
                    BmpName = "GoalStandBy";
                    return;

                case EType.GoalDoor:
                    BmpName = "GoalDoor";
                    return;

                case EType.GoalRiseUp:
                    BmpName = "GoalRiseUp";
                    return;

                case EType.GoalRiseDown:
                    BmpName = "GoalRiseDown";
                    return;

                case EType.GoalNormal:
                    BmpName = "GoalNormal";
                    return;

                case EType.GoalMagneticTracking:
                    BmpName = "GoalMagneticTracking";
                    return;

                case EType.MagneticTrackingFront:
                    BmpName = "MagneticTrackingFront";
                    return;

                case EType.MagneticTrackingRear:
                    BmpName = "MagneticTrackingRear";
                    return;

                case EType.ForbiddenLine:
                    MainColor = new Color(System.Drawing.Color.Orange, 150);
                    LineWidth = 3;
                    return;

                case EType.ForbiddenArea:
                    MainColor = new Color(System.Drawing.Color.Orange, 150);
                    return;

                case EType.AdvancedLine:
                    MainColor = new Color(System.Drawing.Color.Orange, 150);
                    LineWidth = 3;
                    return;

                case EType.AdvancedArea:
                    MainColor = new Color(System.Drawing.Color.Orange, 150);
                    return;

                case EType.LaserStrength:
                    MainColor = new Color(System.Drawing.Color.Purple);
                    PointSize = 5;
                    return;

                case EType.LaserPoints:
                    MainColor = new Color(System.Drawing.Color.Red);
                    PointSize = 4;
                    return;

                case EType.DynamicObstaclePoints:
                    MainColor = new Color(System.Drawing.Color.Blue);
                    PointSize = 7;
                    return;

                case EType.ObstacleLines:
                    return;

                case EType.ObstaclePoints:
                    return;

                case EType.NarrowLine:
                    BmpName = "NarrowLine";
                    MainColor = new Color(System.Drawing.Color.Red, 150);
                    LineStyle = ELineStyle._1111111011111110;
                    return;

                case EType.NarrowPassageWay:
                    BmpName = "NarrowPassageWay";
                    MainColor = new Color(System.Drawing.Color.Red, 150);
                    LineStyle = ELineStyle._1111111011111110;
                    return;

                case EType.MagneticTracking:
                    BmpName = "MagneticTracking";
                    MainColor = new Color(System.Drawing.Color.Red, 150);
                    LineStyle = ELineStyle._1111111011111110;
                    return;

                case EType.Power:
                    BmpName = "Power";
                    TowardLength = 1200;
                    return;

                case EType.ChargingDocking:
                    BmpName = "ChargingDocking";
                    TowardLength = 1200;
                    return;

                case EType.ConveyorDocking:
                    BmpName = "ConveyorDocking";
                    TowardLength = 1200;
                    return;

                case EType.Parking:
                    BmpName = "Parking";
                    return;

                case EType.GoalBuffer:
                    BmpName = "GoalBuffer";
                    return;

                case EType.Path:
                    MainColor = new Color(System.Drawing.Color.LimeGreen);
                    PointSize = 4;
                    LineWidth = 2;
                    return;

                case EType.Eraser:
                    MainColor = new Color(System.Drawing.Color.LightSlateGray, 100);
                    return;

                case EType.Pen:
                    MainColor = new Color(System.Drawing.Color.LightSlateGray);
                    return;

                default:
                    throw new Exception("缺少顯示定義");
            }
        }

        /// <summary>
        /// 圖檔來源
        /// </summary>
        public string BmpName { get; set; } = string.Empty;

        /// <summary>
        /// 線段樣式
        /// </summary>
        public ELineStyle LineStyle { get; set; } = ELineStyle._1111111111111111;

        /// <summary>
        /// 線條寬
        /// </summary>
        public float LineWidth { get; set; } = 2.0f;

        /// <summary>
        /// 主要顏色
        /// </summary>
        public IColor MainColor { get { return mMainColor; } set { mMainColor = new Color(value); } }

        /// <summary>
        /// 點大小
        /// </summary>
        public float PointSize { get; set; } = 3.0f;

        /// <summary>
        /// 繪圖尺寸
        /// </summary>
        public IPair Size { get { return mSize; } set { mSize = FactoryMode.Factory.Pair(value); } }

        /// <summary>
        /// 次要顏色
        /// </summary>
        public IColor SubColor { get { return mSubColor; } set { mSubColor = new Color(value); } }

        /// <summary>
        /// 標示線指示方向
        /// </summary>
        public int TowardLength { get; set; } = 250;

        /// <summary>
        /// 圖案類型
        /// </summary>
        public EType Type { get; set; } = EType.Top;
    }
}