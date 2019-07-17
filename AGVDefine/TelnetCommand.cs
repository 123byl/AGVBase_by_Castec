using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGVDefine {
    /// <summary>
    /// 通訊傳輸指令
    /// </summary>
    public class TelnetCommand {
        /// <summary>
        /// 通訊要求指令
        /// </summary>
        public static class Action {
            /// <summary>
            /// 讀取參數指令
            /// </summary>
            public const string Get = "Get";
            /// <summary>
            /// 設定參數指令
            /// </summary>
            public const string Set = "Set";
            /// <summary>
            /// 傳送檔案指令
            /// </summary>
            public const string Send = "Send";
        }
        /// <summary>
        /// 設定參數指令
        /// </summary>
        public static class Set {
            /// <summary>
            /// 開啟馬達運轉
            /// </summary>
            public const string Start = "Start";
            /// <summary>
            /// 停止馬達運轉
            /// </summary>
            public const string Stop = "Stop";
            /// <summary>
            /// AGV 位置定位調整
            /// </summary>
            public const string POSConfirm = "POSConfirm";
            /// <summary>
            /// 設定AGV位置
            /// </summary>
            public const string POS = "POS";
            /// <summary>
            /// 啟動馬達
            /// </summary>
            public const string ServoOn = "ServoOn";
            /// <summary>
            /// 關閉馬達
            /// </summary>
            public const string ServoOff = "ServoOff";
            /// <summary>
            /// 設定手動行駛速度
            /// </summary>
            public const string DriveVelo = "DriveVelo";
            /// <summary>
            /// 設應工作模式行駛速度
            /// </summary>
            public const string WorkVelo = "WorkVelo";
            /// <summary>
            /// 設定加速度
            /// </summary>
            public const string Acce = "Acce";
            /// <summary>
            /// 設定減速度
            /// </summary>
            public const string Dece = "Dece";
            /// <summary>
            /// 設定即停模式
            /// </summary>
            public const string StopMode = "StopMode";
            /// <summary>
            /// 設定AGV ID
            /// </summary>
            public const string ID = "ID";
            /// <summary>
            /// 設定地圖掃描檔名稱
            /// </summary>
            public const string OriName = "OriName";
            /// <summary>
            /// 設定地圖檔名稱
            /// </summary>
            public const string MapName = "MapName";
            /// <summary>
            /// 規劃路徑(測試用)
            /// </summary>
            public const string PathPlan = "PathPlan";
            /// <summary>
            /// 前往指定目標點
            /// </summary>
            public const string Goto = "Goto";
            /// <summary>
            /// 前往充電站
            /// </summary>
            public const string Charging = "Charging";
            /// <summary>
            /// 切換指定模式
            /// </summary>
            public const string Mode = "Mode";
            /// <summary>
            /// 重置AGV 遠端處理執行緒
            /// </summary>
            public const string ThreadReset = "ThreadReset";
        }
        /// <summary>
        /// 讀取參數資訊
        /// </summary>
        public static class Get {
            /// <summary>
            /// 測試通訊指令
            /// </summary>
            public const string Hello = "Hello";
            /// <summary>
            /// 讀取目標點清單
            /// </summary>
            public const string GoalList = "GoalList";
            /// <summary>
            /// 讀取地圖掃描檔清單
            /// </summary>
            public const string OriList = "OriList";
            /// <summary>
            /// 讀取地圖檔清單
            /// </summary>
            public const string MapList = "MapList";
            /// <summary>
            /// 傳送指定的掃描檔
            /// </summary>
            public const string Ori = "Ori";
            /// <summary>
            /// 傳送指定的地圖檔
            /// </summary>
            public const string Map = "Map";
            /// <summary>
            /// 讀取當前馬達
            /// </summary>
            public const string Info = "Info";
            /// <summary>
            /// 讀取速度設定值
            /// </summary>
            public const string Velo = "Velo";
            /// <summary>
            /// 讀取加速度設定值
            /// </summary>
            public const string Acce = "Acce";
            /// <summary>
            /// 讀取減速度設定值
            /// </summary>
            public const string Dece = "Dece";
            /// <summary>
            /// 確認通訊是否連接
            /// </summary>
            public const string IsOpen = "IsOpen";
            /// <summary>
            /// 讀取編碼器數值
            /// </summary>
            public const string Encoder = "encoder";
            /// <summary>
            /// 即停模式設定值
            /// </summary>
            public const string StopMode = "StopMode";
            /// <summary>
            /// 開啟即時資訊傳遞
            /// </summary>
            public const string Car = "Car";
            /// <summary>
            /// 傳送一次laser數值
            /// </summary>
            public const string Laser = "Laser";
        }
    }
}
