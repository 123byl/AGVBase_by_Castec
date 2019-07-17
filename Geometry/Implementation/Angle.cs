using System;

namespace Geometry
{
    /// <summary>
    /// 提供介於[0,360)之間的角度
    /// </summary>
    [Serializable]
    internal class Angle : IAngle
    {
        private double mValue = 0;

        public Angle()
        {
        }

        public Angle(IAngle angle)
        {
            Theta = angle.Theta;
        }

        public Angle(double angle)
        {
            Theta = angle;
        }

        /// <summary>
        /// 角度值
        /// </summary>
        public double Theta { get { return mValue; } set { mValue = Normalization(value); } }

        /// <summary>
        /// 將角度正規劃在 [0,360) 區間
        /// </summary>
        public static double Normalization(double ang)
        {
            double thetaTmp = ang % 360;
            if (thetaTmp < 0)
                ang = thetaTmp + 360;
            else
                ang = thetaTmp;
            return ang;
        }

        /// <summary>
        /// 比較是否相等
        /// </summary>
        public bool Equals(IAngle other)
        {
            return ((int)(1000 * Theta)) == ((int)(1000 * other.Theta));
        }

        /// <summary>
        /// 回傳湊雜碼 (int)(1000 * Theta)
        /// </summary>
        public override int GetHashCode()
        {
            return (int)(1000 * Theta);
        }

        /// <summary>
        /// 回傳角度至小數點第二位，例如：90.00
        /// </summary>
        public override string ToString()
        {
            return Theta.ToString("F2");
        }
    }
}