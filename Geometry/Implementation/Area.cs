using System;

namespace Geometry
{
    /// <summary>
    /// 面，自動調整 Max 與 Min 使得 Max 總是維持在左上角，Min 總是在右下角
    /// </summary>
    [Serializable]
    internal class Area : IArea
    {
        private Pair mMax = new Pair();

        private Pair mMin = new Pair();

        public Area()
        {
        }

        public Area(IPair min, IPair max)
        {
            this.Set(min, max);
        }

        public Area(IPair center, int width, int height)
        {
            this.Set(center.X - width / 2, center.Y - height / 2, center.X + width / 2, center.Y + height / 2);
        }

        public Area(IArea area)
        {
            this.Set(area.Min, area.Max);
        }

        public Area(double x0, double y0, double x1, double y1)
        {
            this.Set((int)x0, (int)y0, (int)x1, (int)y1);
        }

        public Area(int x0, int y0, int x1, int y1)
        {
            this.Set(x0, y0, x1, y1);
        }

        /// <summary>
        /// 最大值座標
        /// </summary>
        public IPair Max { get { return mMax; } set { this.Set(mMin, value); } }

        /// <summary>
        /// 最小值座標
        /// </summary>
        public IPair Min { get { return mMin; } set { this.Set(value, mMin); } }

        /// <summary>
        /// 比較是否相等
        /// </summary>
        public bool Equals(IArea other)
        {
            return Min.Equals(other.Min) && Max.Equals(other.Max);
        }

        /// <summary>
        /// 回傳湊雜碼 Min^Max
        /// </summary>
        public override int GetHashCode()
        {
            return Min.GetHashCode() ^ Max.GetHashCode();
        }

        /// <summary>
        /// 回傳座標，例如：-100,100,0,200(前面兩個數值代表最小值座標，後面代表最大值座標)
        /// </summary>
        public override string ToString()
        {
            return Min.ToString() + "," + Max.ToString();
        }
    }
}