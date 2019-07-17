using Geometry;

namespace GLCore
{
    /// <summary>
    /// 標示物介面
    /// </summary>
    public interface ISingle<TGeometry> : IDrawable, IName, IDragable, IHasGLSetting where TGeometry : IGeometry
    {
        /// <summary>
        /// 座標資料
        /// </summary>
        TGeometry Data { get; }

        /// <summary>
        /// 回傳名稱及座標，例如：Single,-100,100，座標格式依據 TGeometry 格式而定
        /// </summary>
        string ToString();
    }
}