using Geometry;
using SerialCommunicationData;

namespace GLCore
{
    /// <summary>
    /// 車
    /// </summary>
    public interface IAGV : ISingle<ITowardPair>
    {
        /// <summary>
        /// 遠端 IP 位置
        /// </summary>
        string IP { get; set; }

        /// <summary>
        /// 雷射線
        /// </summary>
        ILaserPoints LaserAPoints { get; }

        /// <summary>
        /// 雷射線
        /// </summary>
        ILaserPoints LaserBPoints { get; }

        /// <summary>
        /// 雷射強度
        /// </summary>
        ILaserStrength LaserStrength { get; }

        /// <summary>
        /// 路徑
        /// </summary>
        IPath Path { get; }

        /// <summary>
        /// <para>安全框(mm)</para>
        /// </summary>
        IArea SafetyArea { get; }

        /// <summary>
        /// <para>AGV 狀態</para>
        /// </summary>
        IStatus Status { get; set; }
    }
}