using AGVDefine;
using Geometry;
using System;
using System.IO;

namespace SerialCommunicationData
{
    /// <summary>
    /// 沒有附帶設計圖或產品
    /// </summary>
    [Serializable]
    internal class Nothing : INothing
    {
    }

    /// <summary>
    /// 附帶 AGV 狀態的設計圖或產品
    /// </summary>
    [Serializable]
    internal class Status : IStatus
    {
        /// <summary>
        /// <para>當下加速度(mm/s^2)</para>
        /// </summary>
        public double Acceleration { get; set; }

        /// <summary>
        /// <para>電池電量(%)</para>
        /// </summary>
        public double Battery { get; set; }
        
        /// <summary>
        /// <para>電池電流(安培)</para>
        /// <para>正值表示放電電流</para>
        /// <para>負值表示充電電流</para>
        /// </summary>
        public double Current { get; set; }

        /// <summary>
        /// <para>當下位置，單位 mm、deg</para>
        /// </summary>
        public ITowardPair Data { get; } = FactoryMode.Factory.TowardPair();

        /// <summary>
        /// <para>AGV 狀態描述</para>
        /// </summary>
        public EDescription Description { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string AlarmMessage { get; set; }

        /// <summary>
        /// <para>IO 資料</para>
        /// </summary>
        public bool[] IO { get; } = new bool[Enum.GetNames(typeof(EIODefine)).Length];

        /// <summary>
        /// <para>地圖匹配度(%)</para>
        /// </summary>
        public double MapMatch { get; set; }

        /// <summary>
        /// <para>地圖名稱或識別碼</para>
        /// </summary>
        public string MapToken { get; set; }

        /// <summary>
        /// <para>AGV 名稱</para>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// <para>安全框(mm)</para>
        /// </summary>
        public IArea SafetyArea { get; } = FactoryMode.Factory.Area(-400, -300, 400, 300);

        /// <summary>
        /// <para>當下實際速度(mm/s)</para>
        /// </summary>
        public double Velocity { get; set; }

        /// <summary>
        /// <para>電池電壓(伏特)</para>
        /// </summary>
        public double Voltage { get; set; }

        /// <summary>
        /// <para>行駛優先權</para>
        /// </summary>
        public int Priority { get; set; }
    }

    /// <summary>
    /// 附帶檔案文件的設計圖或產品
    /// </summary>
    [Serializable]
    internal class Document : IDocument
    {
        /// <summary>
        /// 副檔名
        /// </summary>
        public string Extension { get; private set; }

        /// <summary>
        /// 原始檔路徑
        /// </summary>
        public string Source { get; private set; }

        /// <summary>
        /// 原始檔檔名
        /// </summary>
        public string Name { get; private set; }

        public Document(string path = null) {
            if (File.Exists(path)) Load(path);
        }

        /// <summary>
        /// 另存新檔
        /// </summary>
        public bool SaveAs(string path)
        {
            try
            {
                File.WriteAllBytes(path + '\\' + Name, Data);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 檔案資料
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 載入檔案
        /// </summary>
        public bool Load(string path)
        {
            try
            {
                var tmpName = Path.GetFileName(path);
                var tmpExtension = Path.GetExtension(path);
                var tmpData = File.ReadAllBytes(path);

                Source = path;
                Name = tmpName;
                Extension = tmpExtension;
                Data = tmpData;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}