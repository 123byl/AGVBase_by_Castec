using AGVDefine;
using Geometry;
using static FactoryMode;

#region 封包格式定義

// 上傳地圖檔至 AGV
using TOrderUploadMapToAGV = SerialCommunicationData.OrderPacket<SerialCommunicationData.IDocument, bool>;
using IOrderUploadMapToAGV = SerialCommunicationData.IOrderPacket<SerialCommunicationData.IDocument, bool>;
using IProductUploadMapToAGV = SerialCommunicationData.IProductPacket<SerialCommunicationData.IDocument, bool>;

// 位置校正
using TOrderDoPositionComfirm = SerialCommunicationData.OrderPacket<SerialCommunicationData.INothing, double>;
using IOrderDoPositionComfirm = SerialCommunicationData.IOrderPacket<SerialCommunicationData.INothing, double>;
using IProductDoPositionComfirm = SerialCommunicationData.IProductPacket<SerialCommunicationData.INothing, double>;

// 設定位置
using TOrderSetPosition = SerialCommunicationData.OrderPacket<Geometry.ITowardPair, bool>;
using IOrderSetPosition = SerialCommunicationData.IOrderPacket<Geometry.ITowardPair, bool>;
using IProductSetPosition = SerialCommunicationData.IProductPacket<Geometry.ITowardPair, bool>;

// 設定伺服馬達狀態
using TOrderSetServoMode = SerialCommunicationData.OrderPacket<bool, bool>;
using IOrderSetServoMode = SerialCommunicationData.IOrderPacket<bool, bool>;
using IProductSetServoMode = SerialCommunicationData.IProductPacket<bool, bool>;

// 設定手動運轉速度
using TOrderSetManualVelocity = SerialCommunicationData.OrderPacket<Geometry.IPair, bool>;
using IOrderSetManualVelocity = SerialCommunicationData.IOrderPacket<Geometry.IPair, bool>;
using IProductSetManualVelocity = SerialCommunicationData.IProductPacket<Geometry.IPair, bool>;

// 設定自動(工作)運轉速度
using TOrderSetWorkVelocity = SerialCommunicationData.OrderPacket<int, bool>;
using IOrderSetWorkVelocity = SerialCommunicationData.IOrderPacket<int, bool>;
using IProductSetWorkVelocity = SerialCommunicationData.IProductPacket<int, bool>;

// 設定加速度
using TOrderSetAccelerate = SerialCommunicationData.OrderPacket<int, bool>;
using IOrderSetAccelerate = SerialCommunicationData.IOrderPacket<int, bool>;
using IProductSetAccelerate = SerialCommunicationData.IProductPacket<int, bool>;

// 設定減速度
using TOrderSetDeceleration = SerialCommunicationData.OrderPacket<int, bool>;
using IOrderSetDeceleration = SerialCommunicationData.IOrderPacket<int, bool>;
using IProductSetDeceleration = SerialCommunicationData.IProductPacket<int, bool>;

// 設定ID
using TOrderSetCarID = SerialCommunicationData.OrderPacket<int, bool>;
using IOrderSetCarID = SerialCommunicationData.IOrderPacket<int, bool>;
using IProductSetCarID = SerialCommunicationData.IProductPacket<int, bool>;

// 設定名稱
using TOrderSetCarName = SerialCommunicationData.OrderPacket<string, bool>;
using IOrderSetCarName = SerialCommunicationData.IOrderPacket<string, bool>;
using IProductSetCarName = SerialCommunicationData.IProductPacket<string, bool>;

// 設定掃描檔名稱
using TOrderSetScaningOriFileName = SerialCommunicationData.OrderPacket<string, bool>;
using IOrderSetScaningOriFileName = SerialCommunicationData.IOrderPacket<string, bool>;
using IProductSetScaningOriFileName = SerialCommunicationData.IProductPacket<string, bool>;

// 切換地圖
using TOrderChangeMap = SerialCommunicationData.OrderPacket<string, bool>;
using IOrderChangeMap = SerialCommunicationData.IOrderPacket<string, bool>;
using IProductChangeMap = SerialCommunicationData.IProductPacket<string, bool>;

// 前往充電站
using TOrderDoCharging = SerialCommunicationData.OrderPacket<string, bool>;
using IOrderDoCharging = SerialCommunicationData.IOrderPacket<string, bool>;
using IProductDoCharging = SerialCommunicationData.IProductPacket<string, bool>;

// 更改模式
using TOrderChangeMode = SerialCommunicationData.OrderPacket<string, bool>;
using IOrderChangeMode = SerialCommunicationData.IOrderPacket<string, bool>;
using IProductChangeMode = SerialCommunicationData.IProductPacket<string, bool>;

// 執行手動控制
using TOrderStartManualControl = SerialCommunicationData.OrderPacket<bool, bool>;
using IOrderStartManualControl = SerialCommunicationData.IOrderPacket<bool, bool>;
using IProductStartManualControl = SerialCommunicationData.IProductPacket<bool, bool>;

// 要求 AGV 自動回應路徑
using TOrderAutoReportPath = SerialCommunicationData.OrderPacket<bool, System.Collections.Generic.List<Geometry.IPair>>;
using IOrderAutoReportPath = SerialCommunicationData.IOrderPacket<bool, System.Collections.Generic.List<Geometry.IPair>>;
using IProductAutoReportPath = SerialCommunicationData.IProductPacket<bool, System.Collections.Generic.List<Geometry.IPair>>;

// 跑點
using TOrderDoRuningByGoalName = SerialCommunicationData.OrderPacket<string, bool>;
using IOrderDoRuningByGoalName = SerialCommunicationData.IOrderPacket<string, bool>;
using IProductDoRuningByGoalName = SerialCommunicationData.IProductPacket<string, bool>;

// 讀取目標點清單
using TOrderRequestGoalList = SerialCommunicationData.OrderPacket<SerialCommunicationData.INothing, System.Collections.Generic.List<string>>;
using IOrderRequestGoalList = SerialCommunicationData.IOrderPacket<SerialCommunicationData.INothing, System.Collections.Generic.List<string>>;
using IProductRequestGoalList = SerialCommunicationData.IProductPacket<SerialCommunicationData.INothing, System.Collections.Generic.List<string>>;

// 讀取掃描檔清單
using TOrderRequestOriFileList = SerialCommunicationData.OrderPacket<SerialCommunicationData.INothing, System.Collections.Generic.List<string>>;
using IOrderRequestOriFileList = SerialCommunicationData.IOrderPacket<SerialCommunicationData.INothing, System.Collections.Generic.List<string>>;
using IProductRequestOriFileList = SerialCommunicationData.IProductPacket<SerialCommunicationData.INothing, System.Collections.Generic.List<string>>;

// 讀取所有地圖檔清單
using TOrderRequestMapList = SerialCommunicationData.OrderPacket<SerialCommunicationData.INothing, System.Collections.Generic.List<string>>;
using IOrderRequestMapList = SerialCommunicationData.IOrderPacket<SerialCommunicationData.INothing, System.Collections.Generic.List<string>>;
using IProductRequestMapList = SerialCommunicationData.IProductPacket<SerialCommunicationData.INothing, System.Collections.Generic.List<string>>;

// 要求掃描檔
using TOrderRequestOriFile = SerialCommunicationData.OrderPacket<string, SerialCommunicationData.IDocument>;
using IOrderRequestOriFile = SerialCommunicationData.IOrderPacket<string, SerialCommunicationData.IDocument>;
using IProductRequestOriFile = SerialCommunicationData.IProductPacket<string, SerialCommunicationData.IDocument>;

// 要求地圖檔
using TOrderRequestMapFile = SerialCommunicationData.OrderPacket<string, SerialCommunicationData.IDocument>;
using IOrderRequestMapFile = SerialCommunicationData.IOrderPacket<string, SerialCommunicationData.IDocument>;
using IProductRequestMapFile = SerialCommunicationData.IProductPacket<string, SerialCommunicationData.IDocument>;

// 要求回傳速度值
using TOrderRequestVelocity = SerialCommunicationData.OrderPacket<SerialCommunicationData.INothing, int>;
using IOrderRequestVelocity = SerialCommunicationData.IOrderPacket<SerialCommunicationData.INothing, int>;
using IProductRequestVelocity = SerialCommunicationData.IProductPacket<SerialCommunicationData.INothing, int>;

// 要求回傳加速度值
using TOrderRequestAcceleration = SerialCommunicationData.OrderPacket<SerialCommunicationData.INothing, int>;
using IOrderRequestAcceleration = SerialCommunicationData.IOrderPacket<SerialCommunicationData.INothing, int>;
using IProductRequestAcceleration = SerialCommunicationData.IProductPacket<SerialCommunicationData.INothing, int>;

// 要求回傳減速度值
using TOrderRequestDeceleration = SerialCommunicationData.OrderPacket<SerialCommunicationData.INothing, int>;
using IOrderRequestDeceleration = SerialCommunicationData.IOrderPacket<SerialCommunicationData.INothing, int>;
using IProductRequestDeceleration = SerialCommunicationData.IProductPacket<SerialCommunicationData.INothing, int>;

// 要求回傳 iTS 核心版本
using TOrderRequestITSVersion = SerialCommunicationData.OrderPacket<SerialCommunicationData.INothing, string>;
using IOrderRequestITSVersion = SerialCommunicationData.IOrderPacket<SerialCommunicationData.INothing, string>;
using IProductRequestITSVersion = SerialCommunicationData.IProductPacket<SerialCommunicationData.INothing, string>;

// 開啟或關閉自動回應狀態
using TOrderAutoReportStatus = SerialCommunicationData.OrderPacket<bool, SerialCommunicationData.IStatus>;
using IOrderAutoReportStatus = SerialCommunicationData.IOrderPacket<bool, SerialCommunicationData.IStatus>;
using IProductAutoReportStatus = SerialCommunicationData.IProductPacket<bool, SerialCommunicationData.IStatus>;

// 要求自動回傳雷射掃描結果
using TOrderAutoReportLaser = SerialCommunicationData.OrderPacket<bool, System.Collections.Generic.List<Geometry.IPair>>;
using IOrderAutoReportLaser = SerialCommunicationData.IOrderPacket<bool, System.Collections.Generic.List<Geometry.IPair>>;
using IProductAutoReportLaser = SerialCommunicationData.IProductPacket<bool, System.Collections.Generic.List<Geometry.IPair>>;

// 獲得 AGV 地圖流水號
using TOrderRequestMapSerialNumber = SerialCommunicationData.OrderPacket<SerialCommunicationData.INothing, string>;
using IOrderRequestMapSerialNumber = SerialCommunicationData.IOrderPacket<SerialCommunicationData.INothing, string>;
using IProductRequestMapSerialNumber = SerialCommunicationData.IProductPacket<SerialCommunicationData.INothing, string>;

// 允許 AGV 移動
using TOrderAllowMoving = SerialCommunicationData.OrderPacket<bool, bool>;
using IOrderAllowMoving = SerialCommunicationData.IOrderPacket<bool, bool>;
using IProductAllowMoving = SerialCommunicationData.IProductPacket<bool, bool>;

//停止掃描
using TOrderStopScaning = SerialCommunicationData.OrderPacket<SerialCommunicationData.INothing, bool>;
using IOrderStopScaning = SerialCommunicationData.IOrderPacket<SerialCommunicationData.INothing, bool>;
using IProductStopScaning = SerialCommunicationData.IProductPacket<SerialCommunicationData.INothing, bool>;

//要求雷射資料
using TOrderRequestLaser = SerialCommunicationData.OrderPacket<SerialCommunicationData.INothing, System.Collections.Generic.List<Geometry.IPair>>;
using IOrderRequestLaser = SerialCommunicationData.IOrderPacket<SerialCommunicationData.INothing, System.Collections.Generic.List<Geometry.IPair>>;
using IProductRequestLaser = SerialCommunicationData.IProductPacket<SerialCommunicationData.INothing, System.Collections.Generic.List<Geometry.IPair>>;

//要求規劃路徑
using TOrderRequestPath = SerialCommunicationData.OrderPacket<string, System.Collections.Generic.List<Geometry.IPair>>;
using IOrderRequestPath = SerialCommunicationData.IOrderPacket<string, System.Collections.Generic.List<Geometry.IPair>>;
using IProductRequestPath = SerialCommunicationData.IProductPacket<string, System.Collections.Generic.List<Geometry.IPair>>;

//要求AGV狀態
using TOrderRequestStatus = SerialCommunicationData.OrderPacket<SerialCommunicationData.INothing, SerialCommunicationData.IStatus>;
using IOrderRequestStatus = SerialCommunicationData.IOrderPacket<SerialCommunicationData.INothing, SerialCommunicationData.IStatus>;
using IProductRequestStatus = SerialCommunicationData.IProductPacket<SerialCommunicationData.INothing, SerialCommunicationData.IStatus>;

using System.Collections.Generic;
using System.IO;

#endregion 封包格式定義

namespace SerialCommunicationData
{
    /// <summary>
    /// <para>擴充物件製造工廠</para>
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// <para>建立 AGV 狀態</para>
        /// </summary>
        public static IStatus Status(this IFactory factory)
        {
            return new Status();
        }

        /// <summary>
        /// 建立空白Document物件
        /// </summary>
        public static IDocument Document(this IFactory factory) {
            return new Document();
        }

        /// <summary>
        /// 載入檔案建立Document物件
        /// </summary>
        public static IDocument Document(this IFactory factory,string path) {
            IDocument doc = new Document();
            return doc.Load(path) ? doc : null;
        }
    }

    /// <summary>
    /// 訂單管理器
    /// </summary>
    public static class OrderPacketFactoryMode
    {
        #region 擴充管理器 - FactoryMode -

        /// <summary>
        /// 訂單製造工廠介面
        /// </summary>
        public interface IOrderPacketFactory { }

        /// <summary>
        /// 訂單製造工廠
        /// </summary>
        private static IOrderPacketFactory mOrder = new OrderPacketFactoryImplementation();

        /// <summary>
        /// 訂單製造工廠實作
        /// </summary>
        private class OrderPacketFactoryImplementation : IOrderPacketFactory { }

        /// <summary>
        /// 訂單製造工廠
        /// </summary>
        public static IOrderPacketFactory Order(this IFactory factory)
        {
            return mOrder;
        }

        #endregion 擴充管理器 - FactoryMode -

        #region 建立對應的產品

        /// <summary>
        /// 建立對應的產品
        /// </summary>
        /// <typeparam name="TDesign">原始設計圖</typeparam>
        /// <typeparam name="TRequirement">產品需求(這裡限定為 struct)</typeparam>
        /// <param name="order">原始訂單</param>
        public static IProductPacket<TDesign, TRequirement> CreatProduct<TDesign, TRequirement>(this IOrderPacket<TDesign, TRequirement> order)
        {
            return new ProductPacket<TDesign, TRequirement>()
            {
                Order = order,
                Purpose = order.Purpose,
                Product = default(TRequirement)
            };
        }
        
        /// <summary>
        /// 以產品建立封包
        /// </summary>
        public static IProductPacket<TDesign,TRequirement> CreatProduct<TDesign,TRequirement>(this IOrderPacket<TDesign,TRequirement> order, TRequirement requirement) {
            return new ProductPacket<TDesign, TRequirement>() {
                Order = order,
                Purpose = order.Purpose,
                Product = requirement
            };
        }

        /// <summary>
        /// 以檔案路徑建立檔案產品封包
        /// </summary>
        public static IProductPacket<TDesign,IDocument> CreatProduct<TDesign>(this IOrderPacket<TDesign,IDocument> order,string path) {
            return order.CreatProduct(FactoryMode.Factory.Document(path));
        }

        /// <summary>
        /// 載入檔案並回傳封包
        /// </summary>
        public static IProductPacket<TDesign,IDocument> SetProduct<TDesign>(this IProductPacket<TDesign,IDocument> product,string path) {
            product.SetProduct(FactoryMode.Factory.Document(path));
            return product;
        }

        #endregion 建立對應的產品

        #region 訂單/產品轉型
        /// <summary>
        /// 嘗試轉換為上傳地圖檔至 AGV的訂單
        /// </summary>
        public static IOrderUploadMapToAGV ToIUploadMapToAGV(this IOrderPacket packet)
        {
            return packet as IOrderUploadMapToAGV;
        }

        /// <summary>
        /// 嘗試轉換為上傳地圖檔至 AGV的產品
        /// </summary>
        public static IProductUploadMapToAGV ToIUploadMapToAGV(this IProductPacket packet)
        {
            return packet as IProductUploadMapToAGV;
        }

        /// <summary>
        /// 嘗試轉換為位置校正的訂單
        /// </summary>
        public static IOrderDoPositionComfirm ToIDoPositionComfirm(this IOrderPacket packet)
        {
            return packet as IOrderDoPositionComfirm;
        }

        /// <summary>
        /// 嘗試轉換為位置校正的產品
        /// </summary>
        public static IProductDoPositionComfirm ToIDoPositionComfirm(this IProductPacket packet)
        {
            return packet as IProductDoPositionComfirm;
        }

        /// <summary>
        /// 嘗試轉換為設定位置的訂單
        /// </summary>
        public static IOrderSetPosition ToISetPosition(this IOrderPacket packet)
        {
            return packet as IOrderSetPosition;
        }

        /// <summary>
        /// 嘗試轉換為設定位置的產品
        /// </summary>
        public static IProductSetPosition ToISetPosition(this IProductPacket packet)
        {
            return packet as IProductSetPosition;
        }

        /// <summary>
        /// 嘗試轉換為設定伺服馬達狀態的訂單
        /// </summary>
        public static IOrderSetServoMode ToISetServoMode(this IOrderPacket packet)
        {
            return packet as IOrderSetServoMode;
        }

        /// <summary>
        /// 嘗試轉換為設定伺服馬達狀態的產品
        /// </summary>
        public static IProductSetServoMode ToISetServoMode(this IProductPacket packet)
        {
            return packet as IProductSetServoMode;
        }

        /// <summary>
        /// 嘗試轉換為設定手動運轉速度的訂單
        /// </summary>
        public static IOrderSetManualVelocity ToISetManualVelocity(this IOrderPacket packet)
        {
            return packet as IOrderSetManualVelocity;
        }

        /// <summary>
        /// 嘗試轉換為設定手動運轉速度的產品
        /// </summary>
        public static IProductSetManualVelocity ToISetManualVelocity(this IProductPacket packet)
        {
            return packet as IProductSetManualVelocity;
        }

        /// <summary>
        /// 嘗試轉換為設定自動(工作)運轉速度的訂單
        /// </summary>
        public static IOrderSetWorkVelocity ToISetWorkVelocity(this IOrderPacket packet)
        {
            return packet as IOrderSetWorkVelocity;
        }

        /// <summary>
        /// 嘗試轉換為設定自動(工作)運轉速度的產品
        /// </summary>
        public static IProductSetWorkVelocity ToISetWorkVelocity(this IProductPacket packet)
        {
            return packet as IProductSetWorkVelocity;
        }

        /// <summary>
        /// 嘗試轉換為設定加速度的訂單
        /// </summary>
        public static IOrderSetAccelerate ToISetAccelerate(this IOrderPacket packet)
        {
            return packet as IOrderSetAccelerate;
        }

        /// <summary>
        /// 嘗試轉換為設定加速度的產品
        /// </summary>
        public static IProductSetAccelerate ToISetAccelerate(this IProductPacket packet)
        {
            return packet as IProductSetAccelerate;
        }

        /// <summary>
        /// 嘗試轉換為設定減速度的訂單
        /// </summary>
        public static IOrderSetDeceleration ToISetDeceleration(this IOrderPacket packet)
        {
            return packet as IOrderSetDeceleration;
        }

        /// <summary>
        /// 嘗試轉換為設定減速度的產品
        /// </summary>
        public static IProductSetDeceleration ToISetDeceleration(this IProductPacket packet)
        {
            return packet as IProductSetDeceleration;
        }

        /// <summary>
        /// 嘗試轉換為設定ID的訂單
        /// </summary>
        public static IOrderSetCarID ToISetCarID(this IOrderPacket packet)
        {
            return packet as IOrderSetCarID;
        }

        /// <summary>
        /// 嘗試轉換為設定ID的產品
        /// </summary>
        public static IProductSetCarID ToISetCarID(this IProductPacket packet)
        {
            return packet as IProductSetCarID;
        }

        /// <summary>
        /// 嘗試轉換為設定名稱的訂單
        /// </summary>
        public static IOrderSetCarName ToISetCarName(this IOrderPacket packet)
        {
            return packet as IOrderSetCarName;
        }

        /// <summary>
        /// 嘗試轉換為設定名稱的產品
        /// </summary>
        public static IProductSetCarName ToISetCarName(this IProductPacket packet)
        {
            return packet as IProductSetCarName;
        }

        /// <summary>
        /// 嘗試轉換為設定掃描檔名稱的訂單
        /// </summary>
        public static IOrderSetScaningOriFileName ToISetScanningOriFileName(this IOrderPacket packet)
        {
            return packet as IOrderSetScaningOriFileName;
        }

        /// <summary>
        /// 嘗試轉換為設定掃描檔名稱的產品
        /// </summary>
        public static IProductSetScaningOriFileName ToISetScanningOriFileName(this IProductPacket packet)
        {
            return packet as IProductSetScaningOriFileName;
        }

        /// <summary>
        /// 嘗試轉換為切換地圖的訂單
        /// </summary>
        public static IOrderChangeMap ToIChangeMap(this IOrderPacket packet)
        {
            return packet as IOrderChangeMap;
        }

        /// <summary>
        /// 嘗試轉換為切換地圖的產品
        /// </summary>
        public static IProductChangeMap ToIChangeMap(this IProductPacket packet)
        {
            return packet as IProductChangeMap;
        }

        /// <summary>
        /// 嘗試轉換為前往充電站的訂單
        /// </summary>
        public static IOrderDoCharging ToIDoCharging(this IOrderPacket packet)
        {
            return packet as IOrderDoCharging;
        }

        /// <summary>
        /// 嘗試轉換為前往充電站的產品
        /// </summary>
        public static IProductDoCharging ToIDoCharging(this IProductPacket packet)
        {
            return packet as IProductDoCharging;
        }

        /// <summary>
        /// 嘗試轉換為更改模式的訂單
        /// </summary>
        public static IOrderChangeMode ToIChangeMode(this IOrderPacket packet)
        {
            return packet as IOrderChangeMode;
        }

        /// <summary>
        /// 嘗試轉換為更改模式的產品
        /// </summary>
        public static IProductChangeMode ToIChangeMode(this IProductPacket packet)
        {
            return packet as IProductChangeMode;
        }

        /// <summary>
        /// 嘗試轉換為執行手動控制的訂單
        /// </summary>
        public static IOrderStartManualControl ToIStartManualControl(this IOrderPacket packet)
        {
            return packet as IOrderStartManualControl;
        }

        /// <summary>
        /// 嘗試轉換為執行手動控制的產品
        /// </summary>
        public static IProductStartManualControl ToIStartManualControl(this IProductPacket packet)
        {
            return packet as IProductStartManualControl;
        }

        /// <summary>
        /// 嘗試轉換為要求 AGV 自動回應路徑的訂單
        /// </summary>
        public static IOrderAutoReportPath ToIAutoReportPath(this IOrderPacket packet)
        {
            return packet as IOrderAutoReportPath;
        }

        /// <summary>
        /// 嘗試轉換為要求 AGV 當前路徑的產品
        /// </summary>
        public static IProductAutoReportPath ToIAutoReportPath(this IProductPacket packet)
        {
            return packet as IProductAutoReportPath;
        }
        
        /// <summary>
        /// 嘗試轉換為跑點的訂單
        /// </summary>
        public static IOrderDoRuningByGoalName ToIDoRunningByGoalName(this IOrderPacket packet)
        {
            return packet as IOrderDoRuningByGoalName;
        }

        /// <summary>
        /// 嘗試轉換為跑點的產品
        /// </summary>
        public static IProductDoRuningByGoalName ToIDoRuningByGoalName(this IProductPacket packet)
        {
            return packet as IProductDoRuningByGoalName;
        }

        /// <summary>
        /// 嘗試轉換為讀取目標點清單的訂單
        /// </summary>
        public static IOrderRequestGoalList ToIRequestGoalList(this IOrderPacket packet)
        {
            return packet as IOrderRequestGoalList;
        }

        /// <summary>
        /// 嘗試轉換為讀取目標點清單的產品
        /// </summary>
        public static IProductRequestGoalList ToIRequestGoalList(this IProductPacket packet)
        {
            return packet as IProductRequestGoalList;
        }

        /// <summary>
        /// 嘗試轉換為讀取掃描檔清單的訂單
        /// </summary>
        public static IOrderRequestOriFileList ToIRequestOriList(this IOrderPacket packet)
        {
            return packet as IOrderRequestOriFileList;
        }

        /// <summary>
        /// 嘗試轉換為讀取掃描檔清單的產品
        /// </summary>
        public static IProductRequestOriFileList ToIRequestOriList(this IProductPacket packet)
        {
            return packet as IProductRequestOriFileList;
        }

        /// <summary>
        /// 嘗試轉換為讀取所有地圖檔清單的訂單
        /// </summary>
        public static IOrderRequestMapList ToIRequestMapList(this IOrderPacket packet)
        {
            return packet as IOrderRequestMapList;
        }

        /// <summary>
        /// 嘗試轉換為讀取所有地圖檔清單的產品
        /// </summary>
        public static IProductRequestMapList ToIRequestMapList(this IProductPacket packet)
        {
            return packet as IProductRequestMapList;
        }

        /// <summary>
        /// 嘗試轉換為要求掃描檔的訂單
        /// </summary>
        public static IOrderRequestOriFile ToIRequestOriFile(this IOrderPacket packet)
        {
            return packet as IOrderRequestOriFile;
        }

        /// <summary>
        /// 嘗試轉換為要求掃描檔的產品
        /// </summary>
        public static IProductRequestOriFile ToIRequestOriFile(this IProductPacket packet)
        {
            return packet as IProductRequestOriFile;
        }

        /// <summary>
        /// 嘗試轉換為要求地圖檔的訂單
        /// </summary>
        public static IOrderRequestMapFile ToIRequestMapFile(this IOrderPacket packet)
        {
            return packet as IOrderRequestMapFile;
        }

        /// <summary>
        /// 嘗試轉換為要求地圖檔的產品
        /// </summary>
        public static IProductRequestMapFile ToIRequestMapFile(this IProductPacket packet)
        {
            return packet as IProductRequestMapFile;
        }

        /// <summary>
        /// 嘗試轉換為要求回傳速度值的訂單
        /// </summary>
        public static IOrderRequestVelocity ToIRequestVelocity(this IOrderPacket packet)
        {
            return packet as IOrderRequestVelocity;
        }

        /// <summary>
        /// 嘗試轉換為要求回傳速度值的產品
        /// </summary>
        public static IProductRequestVelocity ToIRequestVelocity(this IProductPacket packet)
        {
            return packet as IProductRequestVelocity;
        }

        /// <summary>
        /// 嘗試轉換為要求回傳加速度值的訂單
        /// </summary>
        public static IOrderRequestAcceleration ToIRequestAcceleration(this IOrderPacket packet)
        {
            return packet as IOrderRequestAcceleration;
        }

        /// <summary>
        /// 嘗試轉換為要求回傳加速度值的產品
        /// </summary>
        public static IProductRequestAcceleration ToIRequestAcceleration(this IProductPacket packet)
        {
            return packet as IProductRequestAcceleration;
        }

        /// <summary>
        /// 嘗試轉換為要求回傳減速度值的訂單
        /// </summary>
        public static IOrderRequestDeceleration ToIRequestDeceleration(this IOrderPacket packet)
        {
            return packet as IOrderRequestDeceleration;
        }

        /// <summary>
        /// 嘗試轉換為要求回傳減速度值的產品
        /// </summary>
        public static IProductRequestDeceleration ToIRequestDeceleration(this IProductPacket packet)
        {
            return packet as IProductRequestDeceleration;
        }

        /// <summary>
        /// 嘗試轉換為要求回傳 iTS 核心版本的訂單
        /// </summary>
        public static IOrderRequestITSVersion ToIRequestITSVersion(this IOrderPacket packet)
        {
            return packet as IOrderRequestITSVersion;
        }

        /// <summary>
        /// 嘗試轉換為要求回傳 iTS 核心版本的產品
        /// </summary>
        public static IProductRequestITSVersion ToIRequestITSVersion(this IProductPacket packet)
        {
            return packet as IProductRequestITSVersion;
        }

        /// <summary>
        /// 嘗試轉換為開啟或關閉自動回應狀態的訂單
        /// </summary>
        public static IOrderAutoReportStatus ToIAutoReportStatus(this IOrderPacket packet)
        {
            return packet as IOrderAutoReportStatus;
        }

        /// <summary>
        /// 嘗試轉換為開啟或關閉自動回應狀態的產品
        /// </summary>
        public static IProductAutoReportStatus ToIAutoReportStatus(this IProductPacket packet)
        {
            return packet as IProductAutoReportStatus;
        }

        /// <summary>
        /// 嘗試轉換為要求自動回傳雷射掃描結果的訂單
        /// </summary>
        public static IOrderAutoReportLaser ToIAutoReportLaser(this IOrderPacket packet)
        {
            return packet as IOrderAutoReportLaser;
        }

        /// <summary>
        /// 嘗試轉換為要求自動回傳雷射掃描結果的產品
        /// </summary>
        public static IProductAutoReportLaser ToIAutoReportLaser(this IProductPacket packet)
        {
            return packet as IProductAutoReportLaser;
        }

        /// <summary>
        /// 嘗試轉換為獲得 AGV 地圖流水號的訂單
        /// </summary>
        public static IOrderRequestMapSerialNumber ToIRequestMapSerialNumber(this IOrderPacket packet)
        {
            return packet as IOrderRequestMapSerialNumber;
        }

        /// <summary>
        /// 嘗試轉換為獲得 AGV 地圖流水號的產品
        /// </summary>
        public static IProductRequestMapSerialNumber ToIRequestMapSerialNumber(this IProductPacket packet)
        {
            return packet as IProductRequestMapSerialNumber;
        }

        /// <summary>
        /// 嘗試轉換為允許 AGV 移動的訂單
        /// </summary>
        public static IOrderAllowMoving ToIAllowMoving(this IOrderPacket packet)
        {
            return packet as IOrderAllowMoving;
        }

        /// <summary>
        /// 嘗試轉換為允許 AGV 移動的產品
        /// </summary>
        public static IProductAllowMoving ToIAllowMoving(this IProductPacket packet)
        {
            return packet as IProductAllowMoving;
        }

        /// <summary>
        /// 嘗試轉換為停止掃描地圖的訂單
        /// </summary>
        public static IOrderStopScaning ToIStopScanning(this IOrderPacket packet) {
            return packet as IOrderStopScaning;
        }

        /// <summary>
        /// 嘗試轉換為停止掃描地圖的產品
        /// </summary>
        public static IProductStopScaning ToIStopScanning(this IProductPacket packet) {
            return packet as IProductStopScaning;
        }

        /// <summary>
        /// 嘗試轉換為要求雷射的訂單
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static IOrderRequestLaser ToIRequestLaser(this IOrderPacket packet) {
            return packet as IOrderRequestLaser;
        }

        /// <summary>
        /// 嘗試轉換為要求雷射的產品
        /// </summary>
        public static IProductRequestLaser ToIRequestLaser(this IProductPacket packet) {
            return packet as IProductRequestLaser;
        }

        /// <summary>
        /// 嘗試轉換為要求規劃路徑的訂單
        /// </summary>
        public static IOrderRequestPath ToIRequestPath(this IOrderPacket packet) {
            return packet as IOrderRequestPath;
        }

        /// <summary>
        /// 嘗試轉換為規劃路徑的產品
        /// </summary>
        public static IProductRequestPath ToIRequestPath(this IProductPacket packet) {
            return packet as IProductRequestPath;
        }

        /// <summary>
        /// 嘗試轉換為要求AGV狀態的訂單
        /// </summary>
        public static IOrderRequestStatus ToIRequestStatus(this IOrderPacket packet) {
            return packet as IOrderRequestStatus;
        }

        /// <summary>
        /// 嘗試轉換為要求AGV狀態的產品
        /// </summary>
        public static IProductRequestStatus ToIRequestStatus(this IProductPacket packet) {
            return packet as IProductRequestStatus;
        }

        #endregion

        #region 製造工廠
        /// <summary>
        /// 建立一個上傳地圖檔至 AGV的訂單
        /// </summary>
        public static IOrderUploadMapToAGV UploadMapToAGV(this IOrderPacketFactory factory, string path)
        {
            IDocument doc = FactoryMode.Factory.Document(path);
            doc.Load(path);

            return new TOrderUploadMapToAGV()
            {
                Purpose = EPurpose.UploadMapToAGV,
                Design = doc,
            };
        }

        /// <summary>
        /// 建立一個位置校正的訂單
        /// </summary>
        public static IOrderDoPositionComfirm DoPositionComfirm(this IOrderPacketFactory factory)
        {
            return new TOrderDoPositionComfirm()
            {
                Purpose = EPurpose.DoPositionComfirm,
                Design = new Nothing(),
            };
        }

        /// <summary>
        /// 建立一個設定位置的訂單
        /// </summary>
        public static IOrderSetPosition SetPosition(this IOrderPacketFactory factory, Geometry.ITowardPair towardPair)
        {
            return new TOrderSetPosition()
            {
                Purpose = EPurpose.SetPosition,
                Design = towardPair,
            };
        }

        /// <summary>
        /// 建立一個設定位置的訂單
        /// </summary>
        public static IOrderSetPosition SetPosition(this IOrderPacketFactory factory, int x, int y, double toward)
        {
            return new TOrderSetPosition()
            {
                Purpose = EPurpose.SetPosition,
                Design = FactoryMode.Factory.TowardPair(x, y, toward),
            };
        }

        /// <summary>
        /// 建立一個設定伺服馬達狀態的訂單
        /// </summary>
        public static IOrderSetServoMode SetServoMode(this IOrderPacketFactory factory, bool on)
        {
            return new TOrderSetServoMode()
            {
                Purpose = EPurpose.SetServoMode,
                Design = on,
            };
        }

        /// <summary>
        /// 建立一個設定手動運轉速度的訂單
        /// </summary>
        public static IOrderSetManualVelocity SetManualVelocity(this IOrderPacketFactory factory, Geometry.IPair velocity)
        {
            return new TOrderSetManualVelocity()
            {
                Purpose = EPurpose.SetManualVelocity,
                Design = velocity,
            };
        }

        /// <summary>
        /// 建立一個設定手動運轉速度的訂單
        /// </summary>
        public static IOrderSetManualVelocity SetManualVelocity(this IOrderPacketFactory factory, int leftWheel, int rightWheel)
        {
            return new TOrderSetManualVelocity()
            {
                Purpose = EPurpose.SetManualVelocity,
                Design = FactoryMode.Factory.Pair(leftWheel, rightWheel),
            };
        }

        /// <summary>
        /// 建立一個設定自動(工作)運轉速度的訂單
        /// </summary>
        public static IOrderSetWorkVelocity SetWorkVelocity(this IOrderPacketFactory factory, int velocity)
        {
            return new TOrderSetWorkVelocity()
            {
                Purpose = EPurpose.SetWorkVelocity,
                Design = velocity,
            };
        }
        
        /// <summary>
        /// 建立一個設定ID的訂單
        /// </summary>
        public static IOrderSetCarID SetCarID(this IOrderPacketFactory factory, int id)
        {
            return new TOrderSetCarID()
            {
                Purpose = EPurpose.SetCarID,
                Design = id,
            };
        }

        /// <summary>
        /// 建立一個設定名稱的訂單
        /// </summary>
        public static IOrderSetCarName SetCarName(this IOrderPacketFactory factory, string name)
        {
            return new TOrderSetCarName()
            {
                Purpose = EPurpose.SetCarName,
                Design = name,
            };
        }

        /// <summary>
        /// 建立一個設定掃描檔名稱的訂單
        /// </summary>
        public static IOrderSetScaningOriFileName SetScanningOriFileName(this IOrderPacketFactory factory, string filename)
        {
            return new TOrderSetScaningOriFileName()
            {
                Purpose = EPurpose.SetScanningOriFileName,
                Design = filename,
            };
        }

        /// <summary>
        /// 建立停止掃描的訂單
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IOrderStopScaning StopScanning(this IOrderPacketFactory factory) {
            return new TOrderStopScaning() { 
                Purpose = EPurpose.StopScanning,
                Design = new Nothing()
            };
        }

        /// <summary>
        /// 建立一個切換地圖的訂單
        /// </summary>
        public static IOrderChangeMap ChangeMap(this IOrderPacketFactory factory, string filename)
        {
            return new TOrderChangeMap()
            {
                Purpose = EPurpose.ChangeMap,
                Design = filename,
            };
        }

        /// <summary>
        /// 建立一個前往充電站的訂單
        /// </summary>
        public static IOrderDoCharging DoCharging(this IOrderPacketFactory factory, string powerName)
        {
            return new TOrderDoCharging()
            {
                Purpose = EPurpose.DoCharging,
                Design = powerName,
            };
        }

        /// <summary>
        /// 建立一個更改模式的訂單
        /// </summary>
        public static IOrderChangeMode ChangeMode(this IOrderPacketFactory factory, string mode)
        {
            return new TOrderChangeMode()
            {
                Purpose = EPurpose.ChangeMode,
                Design = mode,
            };
        }

        /// <summary>
        /// 建立一個執行手動控制的訂單
        /// </summary>
        public static IOrderStartManualControl StartManualControl(this IOrderPacketFactory factory, bool manual)
        {
            return new TOrderStartManualControl()
            {
                Purpose = EPurpose.StartManualControl,
                Design = manual,
            };
        }

        /// <summary>
        /// 建立一個要求 AGV 自動回應路徑的訂單
        /// </summary>
        public static IOrderAutoReportPath AutoReportPath(this IOrderPacketFactory factory, bool on)
        {
            return new TOrderAutoReportPath()
            {
                Purpose = EPurpose.AutoReportPath,
                Design = on,
            };
        }
        
        /// <summary>
        /// 建立一個跑點的訂單
        /// </summary>
        public static IOrderDoRuningByGoalName DoRuningByGoalName(this IOrderPacketFactory factory, string name)
        {
            return new TOrderDoRuningByGoalName()
            {
                Purpose = EPurpose.DoRuningByGoalName,
                Design = name,
            };
        }

        /// <summary>
        /// 建立一個讀取目標點清單的訂單
        /// </summary>
        public static IOrderRequestGoalList RequestGoalList(this IOrderPacketFactory factory)
        {
            return new TOrderRequestGoalList()
            {
                Purpose = EPurpose.RequestGoalList,
                Design = new Nothing(),
            };
        }

        /// <summary>
        /// 建立一個讀取掃描檔清單的訂單
        /// </summary>
        public static IOrderRequestOriFileList RequestOriList(this IOrderPacketFactory factory)
        {
            return new TOrderRequestOriFileList()
            {
                Purpose = EPurpose.RequestOriList,
                Design = new Nothing(),
            };
        }

        /// <summary>
        /// 建立一個讀取所有地圖檔清單的訂單
        /// </summary>
        public static IOrderRequestMapList RequestMapList(this IOrderPacketFactory factory)
        {
            return new TOrderRequestMapList()
            {
                Purpose = EPurpose.RequestMapList,
                Design = new Nothing(),
            };
        }

        /// <summary>
        /// 建立一個要求掃描檔的訂單
        /// </summary>
        public static IOrderRequestOriFile RequestOriFile(this IOrderPacketFactory factory, string filename)
        {
            return new TOrderRequestOriFile()
            {
                Purpose = EPurpose.RequestOriFile,
                Design = filename,
            };
        }

        /// <summary>
        /// 建立一個要求地圖檔的訂單
        /// </summary>
        public static IOrderRequestMapFile RequestMapFile(this IOrderPacketFactory factory, string filename)
        {
            return new TOrderRequestMapFile()
            {
                Purpose = EPurpose.RequestMapFile,
                Design = filename,
            };
        }

        /// <summary>
        /// 建立一個要求回傳速度值的訂單
        /// </summary>
        public static IOrderRequestVelocity RequestVelocity(this IOrderPacketFactory factory)
        {
            return new TOrderRequestVelocity()
            {
                Purpose = EPurpose.RequestVelocity,
                Design = new Nothing(),
            };
        }

        /// <summary>
        /// 建立一個要求回傳加速度值的訂單
        /// </summary>
        public static IOrderRequestAcceleration RequestAcceleration(this IOrderPacketFactory factory)
        {
            return new TOrderRequestAcceleration()
            {
                Purpose = EPurpose.RequestAcceleration,
                Design = new Nothing(),
            };
        }

        /// <summary>
        /// 建立一個要求回傳減速度值的訂單
        /// </summary>
        public static IOrderRequestDeceleration RequestDeceleration(this IOrderPacketFactory factory)
        {
            return new TOrderRequestDeceleration()
            {
                Purpose = EPurpose.RequestDeceleration,
                Design = new Nothing(),
            };
        }

        /// <summary>
        /// 建立一個要求回傳 iTS 核心版本的訂單
        /// </summary>
        public static IOrderRequestITSVersion RequestITSVersion(this IOrderPacketFactory factory)
        {
            return new TOrderRequestITSVersion()
            {
                Purpose = EPurpose.RequestITSVersion,
                Design = new Nothing(),
            };
        }

        /// <summary>
        /// 建立一個開啟或關閉自動回應狀態的訂單
        /// </summary>
        public static IOrderAutoReportStatus AutoReportStatus(this IOrderPacketFactory factory, bool on)
        {
            return new TOrderAutoReportStatus()
            {
                Purpose = EPurpose.AutoReportStatus,
                Design = on,
            };
        }

        /// <summary>
        /// 建立一個要求自動回傳雷射掃描結果的訂單
        /// </summary>
        public static IOrderAutoReportLaser AutoReportLaser(this IOrderPacketFactory factory, bool on)
        {
            return new TOrderAutoReportLaser()
            {
                Purpose = EPurpose.AutoReportLaser,
                Design = on,
            };
        }

        /// <summary>
        /// 建立一個獲得 AGV 地圖流水號的訂單
        /// </summary>
        public static IOrderRequestMapSerialNumber RequestMapSerialNumber(this IOrderPacketFactory factory)
        {
            return new TOrderRequestMapSerialNumber()
            {
                Purpose = EPurpose.RequestMapSerialNumber,
                Design = new Nothing(),
            };
        }

        /// <summary>
        /// 建立一個允許 AGV 移動的訂單
        /// </summary>
        public static IOrderAllowMoving AllowMoving(this IOrderPacketFactory factory, bool allow)
        {
            return new TOrderAllowMoving()
            {
                Purpose = EPurpose.AllowMoving,
                Design = allow,
            };
        }

        /// <summary>
        /// 建立一個要求雷射資料的訂單
        /// </summary>
        public static IOrderRequestLaser RequestLaser(this IOrderPacketFactory factory) {
            return new TOrderRequestLaser() {
                Purpose = EPurpose.RequestLaser,
                Design = null
            };
        }
        
        /// <summary>
        /// 建立一個要求路徑的訂單
        /// </summary>
        public static IOrderRequestPath RequestPath(this IOrderPacketFactory factory,string goalName) {
            return new TOrderRequestPath() {
                Purpose = EPurpose.RequestPath,
                Design = goalName
            };
        }

        /// <summary>
        /// 建立一個要求AGV狀態的訂單
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IOrderRequestStatus RequestStatus(this IOrderPacketFactory factory) {
            return new TOrderRequestStatus() {
                Purpose = EPurpose.RequestStatus,
                Design = null,
            };
        }

        #endregion 製造工廠
    }
}