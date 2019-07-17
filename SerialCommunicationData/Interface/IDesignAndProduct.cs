using AGVDefine;
using Geometry;

namespace SerialCommunicationData
{
    /// <summary>
    /// 沒有附帶設計圖或產品
    /// </summary>
    public interface INothing
    {
    }

    /// <summary>
    /// 附帶 AGV 狀態的設計圖或產品
    /// </summary>
    public interface IStatus
    {
        /// <summary>
        /// 當下加速度(mm/s^2)
        /// </summary>
        double Acceleration { get; set; }

        /// <summary>
        /// 電池電量(%)
        /// </summary>
        double Battery { get; set; }

        /// <summary>
        /// 電池電流(安培)
        /// 正值表示放電電流
        /// 負值表示充電電流
        /// </summary>
        double Current { get; set; }

        /// <summary>
        /// 當下位置，單位 mm、deg
        /// </summary>
        ITowardPair Data { get; }

        /// <summary>
        /// AGV 狀態描述
        /// </summary>
        EDescription Description { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        string AlarmMessage { get; set; }

        /// <summary>
        /// IO 資料
        /// </summary>
        bool[] IO { get; }

        /// <summary>
        /// 地圖匹配度(%)
        /// </summary>
        double MapMatch { get; set; }

        /// <summary>
        /// 地圖名稱或識別碼
        /// </summary>
        string MapToken { get; set; }

        /// <summary>
        /// AGV 名稱
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 安全框(mm)
        /// </summary>
        IArea SafetyArea { get; }

        /// <summary>
        /// 當下實際速度(mm/s)
        /// </summary>
        double Velocity { get; set; }

        /// <summary>
        /// 電池電壓(伏特)
        /// </summary>
        double Voltage { get; set; }

        /// <summary>
        /// 車子行駛優先權
        /// </summary>
        int Priority { get; set; }
    }

    /// <summary>
    /// 附帶檔案文件的設計圖或產品
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// 副檔名
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// 原始檔路徑
        /// </summary>
        string Source { get; }

        /// <summary>
        /// 原始檔檔名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 另存新檔
        /// </summary>
        bool SaveAs(string path);

        /// <summary>
        /// 檔案資料
        /// </summary>
        byte[] Data { get; set; }

        /// <summary>
        /// 載入檔案
        /// </summary>
        bool Load(string path);
    }
}