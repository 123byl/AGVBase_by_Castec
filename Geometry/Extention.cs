using System;
using System.Collections.Generic;

namespace Geometry
{
    /// <summary>
    /// 提供 <see cref="IPair"/>,<see cref="ITowardPair"/>,<see cref="ILine"/> 等幾何操作方法
    /// </summary>
    public static class Extention
    {
        /// <summary>
        /// 座標取絕對值
        /// </summary>
        public static IPair Abs(this IPair value)
        {
            return new Pair(Math.Abs(value.X), Math.Abs(value.Y));
        }

        /// <summary>
        /// 加法
        /// </summary>
        public static IPair Add(this IPair lhs, IPair rhs)
        {
            return new Pair(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        /// <summary>
        /// 加法
        /// </summary>
        public static IPair Add(this IPair lhs, int x, int y)
        {
            return new Pair(lhs.X + x, lhs.Y + y);
        }

        /// <summary>
        /// 與 X 軸夾角
        /// </summary>
        public static IAngle Angle(this IPair pos)
        {
            return new Angle(Math.Atan2(pos.Y, pos.X) * 180 / Math.PI);
        }

        /// <summary>
        /// 與另一點夾角
        /// </summary>
        public static IAngle Angle(this IPair pos,IPair newPosition) {
            double calX = pos.X - newPosition.X;
            double calY = pos.Y - newPosition.Y;
            double calTheta = Math.Atan2(calY, calX) * 180 / Math.PI;
            return new Angle(calTheta);
        }

        /// <summary>
        /// 回傳線段的中心座標
        /// </summary>
        public static IPair Center(this ILine line)
        {
            return new Pair(line.Begin.Add(line.End).Division(2));
        }

        /// <summary>
        /// 回傳面的中心座標
        /// </summary>
        public static IPair Center(this IArea area)
        {
            return new Pair(area.Min.Add(area.Max).Division(2));
        }

        /// <summary>
        /// 將面轉換為四條線
        /// </summary>
        public static ILine[] ConvertToLines(this IArea area)
        {
            IPair p0 = area.Min;
            IPair p2 = area.Max;
            IPair p1 = new Pair(p2.X, p0.Y);
            IPair p3 = new Pair(p0.X, p2.Y);
            return new ILine[] { new Line(p0, p1), new Line(p1, p2), new Line(p2, p3), new Line(p3, p0) };
        }

        /// <summary>
        /// 除法
        /// </summary>
        public static IPair Division(this IPair lhs, int rhs)
        {
            return new Pair(lhs.X / rhs, lhs.Y / rhs);
        }

        /// <summary>
        /// 回傳高度
        /// </summary>
        public static int Height(this IArea area)
        {
            return area.Max.Y - area.Min.Y;
        }

        /// <summary>
        /// 求線段長度
        /// </summary>
        public static double Length(this ILine line)
        {
            int x = line.End.X - line.Begin.X;
            int y = line.End.Y - line.Begin.Y;
            return Math.Sqrt(x * x + y * y);
        }

        /// <summary>
        /// 兩點間距離
        /// </summary>
        public static double LengthTo(this IPair beg, IPair end)
        {
            return Math.Sqrt((beg.X - end.X) * (beg.X - end.X) + (beg.Y - end.Y) * (beg.Y - end.Y));
        }

        /// <summary>
        /// 求夾角，角度值僅有 0, 45, 90, 135, 180, 225, 270, 315 度等
        /// </summary>
        public static IAngle LimitedAngle(this IPair beg, IPair end)
        {
            IPair delta = beg.LimitedLine(end).Subtraction(beg);
            return delta.Angle();
        }

        /// <summary>
        /// 求新的末端點座標，使得他成為水平線、鉛錘線或 45 度角的線
        /// </summary>
        public static IPair LimitedLine(this IPair beg, IPair end)
        {
            IPair delta = end.Subtraction(beg);
            if (Math.Abs(delta.X) > 2 * Math.Abs(delta.Y))
            {
                return beg.Add(delta.X, 0);
            }
            else if (Math.Abs(delta.Y) > 2 * Math.Abs(delta.X))
            {
                return beg.Add(0, delta.Y);
            }
            else
            {
                int min = Math.Min(Math.Abs(delta.X), Math.Abs(delta.Y));
                return beg.Add(Math.Sign(delta.X) * min, Math.Sign(delta.Y) * min);
            }
        }

        /// <summary>
        /// 求新的末端點座標，使得他成為一個正方形
        /// </summary>
        public static IPair LimitedSquare(this IPair beg, IPair end)
        {
            IPair delta = end.Subtraction(beg);
            int min = Math.Min(Math.Abs(delta.X), Math.Abs(delta.Y));
            delta = FactoryMode.Factory.Pair(Math.Sign(delta.X) * min, Math.Sign(delta.Y) * min);
            return beg.Add(delta);
        }

        /// <summary>
        /// 回傳右下角座標
        /// </summary>
        public static IPair MaxXMinY(this IArea area)
        {
            return new Pair(area.Max.X, area.Min.Y);
        }

        /// <summary>
        /// 回傳左上角座標
        /// </summary>
        public static IPair MinXMaxY(this IArea area)
        {
            return new Pair(area.Min.X, area.Max.Y);
        }

        /// <summary>
        /// 沿著(0,0)旋轉
        /// </summary>
        public static IPair Rotate(this IPair lhs, IAngle angle)
        {
            double theta = angle.Theta * Math.PI / 180.0;
            double x = lhs.X * Math.Cos(theta) + lhs.Y * Math.Sin(theta);
            double y = -lhs.X * Math.Sin(theta) + lhs.Y * Math.Cos(theta);
            return new Pair(x, y);
        }

        /// <summary>
        /// 重設座標，並自動依照座標大小分配給 Min/Max
        /// </summary>
        public static void Set(this IArea area, int x0, int y0, int x1, int y1)
        {
            area.Min.X = Math.Min(x0, x1);
            area.Min.Y = Math.Min(y0, y1);
            area.Max.X = Math.Max(x0, x1);
            area.Max.Y = Math.Max(y0, y1);
        }

        /// <summary>
        /// 重設座標，並自動依照座標大小分配給 Min/Max
        /// </summary>
        public static void Set(this IArea area, IPair p0, IPair p1)
        {
            area.Set(p0.X, p0.Y, p1.X, p1.Y);
        }

        /// <summary>
        /// 求移動後座標
        /// </summary>
        public static IPair Shift(this IPair center, IAngle toward, int length)
        {
            return center.Shift(toward.Theta, length);
        }

        /// <summary>
        /// 求移動後座標
        /// </summary>
        public static IPair Shift(this IPair center, double toward, int length)
        {
            return new Pair((int)(center.X + length * Math.Cos(toward * Math.PI / 180)), (int)(center.Y + length * Math.Sin(toward * Math.PI / 180)));
        }

        /// <summary>
        /// 回傳面的長寬尺寸
        /// </summary>
        public static IPair Size(this IArea area)
        {
            return new Pair(area.Max.X - area.Min.X, area.Max.Y - area.Min.Y);
        }

        /// <summary>
        /// 減法
        /// </summary>
        public static IPair Subtraction(this IPair lhs, IPair rhs)
        {
            return new Pair(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        /// <summary>
        /// 將線段轉為點集合
        /// </summary>
        public static List<IPair> ToPairs(this ILine line)
        {
            List<IPair> res = new List<IPair>();
            int dx = line.End.X - line.Begin.X;
            int dy = line.End.Y - line.Begin.Y;
            int sx = Math.Sign(dx);
            int sy = Math.Sign(dy);
            if (dx == 0 && dy == 0) return res;
            if (Math.Abs(dx) >= Math.Abs(dy))
            {
                for (int tx = 0; tx != dx + sx; tx += sx)
                {
                    int ty = (int)(((double)tx) / dx * dy);
                    res.Add(line.Begin.Add(tx, ty));
                }
                return res;
            }
            else
            {
                for (int ty = 0; ty != dy + sy; ty += sy)
                {
                    int tx = (int)(((double)ty) / dy * dx);
                    res.Add(line.Begin.Add(tx, ty));
                }
                return res;
            }
        }

        /// <summary>
        /// 回傳寬度
        /// </summary>
        public static int Width(this IArea area)
        {
            return area.Max.X - area.Min.X;
        }
        
    }
}
