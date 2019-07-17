using Geometry;

namespace GLCore
{
    /// <summary>
    /// 雷射點
    /// </summary>
    public interface ILaserPoints : IMuti<IPair>
    {
        /// <summary>
        /// 雷射中心
        /// </summary>
        IPair Center { get; }

        /// <summary>
        /// 是否顯示雷射中心
        /// </summary>
        bool ShowCenter { get; set; }
    }
}