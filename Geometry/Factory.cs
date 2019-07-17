using static FactoryMode;

namespace Geometry
{
    /// <summary>
    /// <para>擴充物件製造工廠</para>
    /// </summary>
    public static class Factory
    {
        #region Angle

        /// <summary>
        /// 建立 [0,360) 之間的角度物件
        /// </summary>
        public static IAngle Angle(this IFactory factory)
        {
            return new Angle();
        }

        /// <summary>
        /// 建立 [0,360) 之間的角度物件
        /// </summary>
        public static IAngle Angle(this IFactory factory, double angle)
        {
            return new Angle(angle);
        }

        /// <summary>
        /// 建立 [0,360) 之間的角度物件
        /// </summary>
        public static IAngle Angle(this IFactory factory, IAngle angle)
        {
            return new Angle(angle);
        }

        #endregion Angle

        #region Area

        /// <summary>
        /// 建立自動調整 Max 與 Min 使得 Max 總是維持在左上角，Min 總是在右下角的面物件
        /// </summary>
        public static IArea Area(this IFactory factory)
        {
            return new Area();
        }

        /// <summary>
        /// 建立自動調整 Max 與 Min 使得 Max 總是維持在左上角，Min 總是在右下角的面物件
        /// </summary>
        public static IArea Area(this IFactory factory, IArea area)
        {
            return new Area(area);
        }

        /// <summary>
        /// 建立自動調整 Max 與 Min 使得 Max 總是維持在左上角，Min 總是在右下角的面物件
        /// </summary>
        public static IArea Area(this IFactory factory, IPair min, IPair max)
        {
            return new Area(min, max);
        }

        /// <summary>
        /// 建立自動調整 Max 與 Min 使得 Max 總是維持在左上角，Min 總是在右下角的面物件
        /// </summary>
        public static IArea Area(this IFactory factory, int x0, int y0, int x1, int y1)
        {
            return new Area(x0, y0, x1, y1);
        }

        /// <summary>
        /// 建立自動調整 Max 與 Min 使得 Max 總是維持在左上角，Min 總是在右下角的面物件
        /// </summary>
        public static IArea Area(this IFactory factory, IPair center, int width, int height)
        {
            return new Area(center, width, height);
        }

        /// <summary>
        /// 建立自動調整 Max 與 Min 使得 Max 總是維持在左上角，Min 總是在右下角的面物件
        /// </summary>
        public static IArea Area(this IFactory factory, double x0, double y0, double x1, double y1)
        {
            return new Area(x0, y0, x1, y1);
        }

        #endregion Area

        #region Line

        /// <summary>
        /// 建立線段物件
        /// </summary>
        public static ILine Line(this IFactory factory)
        {
            return new Line();
        }

        /// <summary>
        /// 建立線段物件
        /// </summary>
        public static ILine Line(this IFactory factory, ILine line)
        {
            return new Line(line);
        }

        /// <summary>
        /// 建立線段物件
        /// </summary>
        public static ILine Line(this IFactory factory, IPair beg, IPair end)
        {
            return new Line(beg, end);
        }

        /// <summary>
        /// 建立線段物件
        /// </summary>
        public static ILine Line(this IFactory factory, int x0, int y0, int x1, int y1)
        {
            return new Line(x0, y0, x1, y1);
        }

        /// <summary>
        /// 建立線段物件
        /// </summary>
        public static ILine Line(this IFactory factory, double x0, double y0, double x1, double y1)
        {
            return new Line(x0, y0, x1, y1);
        }

        #endregion Line

        #region Pair

        /// <summary>
        /// 建立整數座標物件
        /// </summary>
        public static IPair Pair(this IFactory factory)
        {
            return new Pair();
        }

        /// <summary>
        /// 建立整數座標物件
        /// </summary>
        public static IPair Pair(this IFactory factory, IPair pair)
        {
            return new Pair(pair);
        }

        /// <summary>
        /// 建立整數座標物件
        /// </summary>
        public static IPair Pair(this IFactory factory, int x, int y)
        {
            return new Pair(x, y);
        }

        /// <summary>
        /// 建立整數座標物件
        /// </summary>
        public static IPair Pair(this IFactory factory, double x, double y)
        {
            return new Pair(x, y);
        }

        #endregion Pair

        #region TowardPair

        /// <summary>
        /// 建立具有方向性的點物件
        /// </summary>
        public static ITowardPair TowardPair(this IFactory factory)
        {
            return new TowardPair();
        }

        /// <summary>
        /// 建立具有方向性的點物件
        /// </summary>
        public static ITowardPair TowardPair(this IFactory factory, ITowardPair towardPair)
        {
            return new TowardPair(towardPair);
        }

        /// <summary>
        /// 建立具有方向性的點物件
        /// </summary>
        public static ITowardPair TowardPair(this IFactory factory, IPair position, double toward)
        {
            return new TowardPair(position, toward);
        }

        /// <summary>
        /// 建立具有方向性的點物件
        /// </summary>
        public static ITowardPair TowardPair(this IFactory factory, IPair position, IAngle toward)
        {
            return new TowardPair(position, toward);
        }

        /// <summary>
        /// 建立具有方向性的點物件
        /// </summary>
        public static ITowardPair TowardPair(this IFactory factory, int x, int y, double toward)
        {
            return new TowardPair(x, y, toward);
        }

        /// <summary>
        /// 建立具有方向性的點物件
        /// </summary>
        public static ITowardPair TowardPair(this IFactory factory, int x, int y, IAngle toward)
        {
            return new TowardPair(x, y, toward);
        }

        /// <summary>
        /// 建立具有方向性的點物件
        /// </summary>
        public static ITowardPair TowardPair(this IFactory factory, double x, double y, double toward)
        {
            return new TowardPair(x, y, toward);
        }

        /// <summary>
        /// 建立具有方向性的點物件
        /// </summary>
        public static ITowardPair TowardPair(this IFactory factory, double x, double y, IAngle toward)
        {
            return new TowardPair(x, y, toward);
        }

        #endregion TowardPair
    }
}
