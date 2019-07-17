using Geometry;

namespace GLCore
{
    /// <summary>
    /// 顯示集合介面
    /// </summary>
    public interface IMuti<TGeometry> : IDrawable, IName, IHasGLSetting where TGeometry : IGeometry
    {
        /// <summary>
        /// 集合資料
        /// </summary>
        ISafetyList<TGeometry> DataList { get; }

        /// <summary>
        /// 旋轉
        /// </summary>
        IAngle Rotate { get; set; }

        /// <summary>
        /// 偏移
        /// </summary>
        IPair Shift { get; set; }

        /// <summary>
        /// 重新生成頂點陣列(加速顯示)
        /// </summary>
        void BuildVertexArray();
    }
}