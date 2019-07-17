using DiscriptExtention;

namespace AGVDefine
{
    /// <summary>
    /// IO 列表定義
    /// </summary>
    public enum EIODefine
    {
        /// <summary>
        /// 安全雷射觸發
        /// </summary>
        [Discript(Content = "X1100 Safetey Laser Alarm")]
        X1100,

        /// <summary>
        /// 急停觸發
        /// </summary>
        [Discript(Content = "X1101 EMO")]
        X1101,

        /// <summary>
        /// 電源板準備完成
        /// </summary>
        [Discript(Content = "X1102 Power Board Ready")]
        X1102,

        /// <summary>
        /// 解煞車
        /// </summary>
        [Discript(Content = "X1103 Breke Signal")]
        X1103,

        /// <summary>
        /// 硬體鎖，或 UR 的 Home Sensor
        /// </summary>
        [Discript(Content = "X1104 Hardware Lock")]
        X1104,

        /// <summary>
        /// 保留
        /// </summary>
        [Discript(Content = "X1105 Reserve")]
        X1105,

        /// <summary>
        /// 保留
        /// </summary>
        [Discript(Content = "X1106 Reserve")]
        X1106,

        /// <summary>
        /// 保留
        /// </summary>
        [Discript(Content = "X1107 Reserve")]
        X1107,

        /// <summary>
        /// 保留
        /// </summary>
        [Discript(Content = "X1108 Reserve")]
        X1108,

        /// <summary>
        /// 保留
        /// </summary>
        [Discript(Content = "X1109 Reserve")]
        X1109,
    }
}
