//using AGVDefine;
//using Geometry;
//using SerialCommunicationData.Interface;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SerialClient.Interface {

//    /// <summary>
//    /// GatCar回應
//    /// </summary>
//    public interface IResponseGetCar : IPacket<EClientPurpose> {
//        /// <summary>
//        /// 自動回傳雷射功能的激活狀態
//        /// </summary>
//        bool Enable { get; }
//    }

//    /// <summary>
//    /// GetLaser回應
//    /// </summary>
//    public interface IResponseGetLaser :IPacket<EClientPurpose> {
//        /// <summary>
//        /// 雷射座標點集合
//        /// </summary>
//        IEnumerable<IPair> LaserPoints { get; }
//    }

//    /// <summary>
//    /// SetServo回應
//    /// </summary>
//    public interface IResponseSetServo :IPacket<EClientPurpose> {
//        /// <summary>
//        /// 目前的馬達激磁狀態
//        /// </summary>
//        bool ServoOn { get; }
//    }

//    /// <summary>
//    /// SetWorkVelocity回應
//    /// </summary>
//    public interface IResponseSetWorkVelocity:IPacket<EClientPurpose> {
//        /// <summary>
//        /// 當前工作速度
//        /// </summary>
//        int WorkVelocity { get; }
//    }

//    /// <summary>
//    /// SetPOS回應
//    /// </summary>
//    public interface IResponseSetPOS :IPacket<EClientPurpose>{
//        /// <summary>
//        /// 是否設定成功
//        /// </summary>
//        bool Success { get; }
//    }

//    /// <summary>
//    /// SetMoving回應
//    /// </summary>
//    public interface IResponseSetMoving :IPacket<EClientPurpose> {
//        /// <summary>
//        /// 是否正在移動中
//        /// </summary>
//        bool IsMoving { get;}
//    }

//    /// <summary>
//    /// SetDriveVelo回應
//    /// </summary>
//    public interface IResponseSetDriveVelo :IPacket<EClientPurpose> {
//        /// <summary>
//        /// 右輪速度
//        /// </summary>
//        int RightVelocity { get; }
//        /// <summary>
//        /// 左輪速度
//        /// </summary>
//        int LeftVelocity { get; }
//    }

//    /// <summary>
//    /// SetMode回應
//    /// </summary>
//    public interface IResponseSetMode :IPacket<EClientPurpose> {
//        /// <summary>
//        /// 當前模式
//        /// </summary>
//        EMode Mode { get; }
//    }

//    /// <summary>
//    /// SetOriName回應
//    /// </summary>
//    public interface IResponseSetOriName : IPacket<EClientPurpose> {
//        /// <summary>
//        /// Ori檔名
//        /// </summary>
//        string OriName { get; }
//    }

//    /// <summary>
//    /// SetPOSComfirm回應
//    /// </summary>
//    public interface IResponseSetPOSComfirm : IPacket<EClientPurpose> {
//        /// <summary>
//        /// 地圖相似度
//        /// </summary>
//        double Similarity { get; }
//    }

//    /// <summary>
//    /// SetMapName回應
//    /// </summary>
//    public interface IResponseSetMapName :IPacket<EClientPurpose> {
//        /// <summary>
//        /// 以載入的Map檔名
//        /// </summary>
//        string MapName { get; }
//    }

//    /// <summary>
//    /// SetPathPlan回應
//    /// </summary>
//    public interface IResponseSetPathPlan :IPacket<EClientPurpose> {
//        /// <summary>
//        /// 路徑是否規劃成功
//        /// </summary>
//        bool Success { get; }
//        /// <summary>
//        /// 目標Goal點索引值
//        /// </summary>
//        int GoalIndex { get; }
//        /// <summary>
//        /// 路徑點集合
//        /// </summary>
//        List<IPair> Path { get; }
//    }

//    /// <summary>
//    /// SetRun回應
//    /// </summary>
//    public interface IResponseSetRun : IPacket<EClientPurpose> {
//        /// <summary>
//        /// 路徑是否規劃成功
//        /// </summary>
//        bool Success { get; }
//        /// <summary>
//        /// 目標Goal點索引值
//        /// </summary>
//        int GoalIndex { get; }
//        /// <summary>
//        /// 路徑點集合
//        /// </summary>
//        List<IPair> Path { get; }
//    }

//    /// <summary>
//    /// SetCharging回應
//    /// </summary>
//    public interface IResponseSetCharging :IPacket<EClientPurpose> {
//        /// <summary>
//        /// 路徑規劃是否成功
//        /// </summary>
//        bool Success { get; }
//        /// <summary>
//        /// 充電站索引值
//        /// </summary>
//        int PowerIndex { get; }
//        /// <summary>
//        /// 路徑
//        /// </summary>
//        List<IPair> Path { get; }
//    }

//}
