using AGVDefine;
using Geometry;
using SerialCommunicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialClient.Interface {

    /// <summary>
    /// GetCar命令
    /// </summary>
    public interface ICommandGetCar : IPacket<EClientPurpose> {
        /// <summary>
        /// 設定自動雷射回傳是否激活
        /// </summary>
        bool Enable { get; }
    }

    /// <summary>
    /// GetLaser命令
    /// </summary>
    public interface ICommandGetLaser :IPacket<EClientPurpose> {}

    /// <summary>
    /// SetServo命令
    /// </summary>
    public interface ICommandSetServo :IPacket<EClientPurpose> {
        /// <summary>
        /// 馬達激磁狀態設定
        /// </summary>
        bool ServoOn { get; }
    }

    /// <summary>
    /// SetWorkVelocity命令
    /// </summary>
    public interface ICommandSetWorkVelocity :IPacket<EClientPurpose> {
        /// <summary>
        /// 工作速度
        /// </summary>
        int WorkVelocity { get; }
    }

    /// <summary>
    /// SetPOS命令
    /// </summary>
    /// <remarks>
    /// 設定AGV座標
    /// </remarks>
    public interface ICommandSetPOS :IPacket<EClientPurpose> {
        /// <summary>
        /// 車子座標
        /// </summary>
        ITowardPair Position { get;}
    }
    
    /// <summary>
    /// 移動控制命令
    /// </summary>
    public interface ICommandSetMoving :IPacket<EClientPurpose> {
        /// <summary>
        /// 是否開始移動
        /// </summary>
        bool StartMoving { get; }
    }

    /// <summary>
    /// SetDriveVelo命令
    /// </summary>
    public interface ICommandSetDriveVelo :IPacket<EClientPurpose>{
        /// <summary>
        /// 右輪速度
        /// </summary>
        int RightVelocity { get; }
        /// <summary>
        /// 左輪速度
        /// </summary>
        int LeftVelocity { get; }
    }

    /// <summary>
    /// SetMode命令
    /// </summary>
    public interface ICommandSetMode :IPacket<EClientPurpose> {
        /// <summary>
        /// 要切換的模式
        /// </summary>
        EMode Mode { get; }
    }

    /// <summary>
    /// SetOriName命令
    /// </summary>
    public interface ICommandSetOriName :IPacket<EClientPurpose>{
        /// <summary>
        /// Ori檔名
        /// </summary>
        string OriName { get; }
    }

    /// <summary>
    /// SetPOSComfirm命令
    /// </summary>
    public interface ICommandSetPOSComfirm :IPacket<EClientPurpose> {}

    /// <summary>
    /// SetMapName命令
    /// </summary>
    public interface ICommandSetMapName :IPacket<EClientPurpose> {
        /// <summary>
        /// 要求載入的Map檔名
        /// </summary>
        string MapName { get; }
    }

    /// <summary>
    /// SetPathPlan命令
    /// </summary>
    public interface ICommandSetPathPlan :IPacket<EClientPurpose> {
        /// <summary>
        /// 目標Goal點索引值
        /// </summary>
        int GoalIndex { get; }
    }

    /// <summary>
    /// SetRun命令
    /// </summary>
    public interface ICommandSetRun :IPacket<EClientPurpose> {
        /// <summary>
        /// 目標Goal點索引值
        /// </summary>
        int GoalIndex { get;}
    }

    /// <summary>
    /// SetCharging命令
    /// </summary>
    public interface ICommandSetCharging : IPacket<EClientPurpose> {
        /// <summary>
        /// 充電站索引值
        /// </summary>
        int PowerIndex { get; }
    }

}
