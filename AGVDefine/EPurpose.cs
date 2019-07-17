namespace AGVDefine
{
    /// <summary>
    /// <para>通訊目的</para>
    /// </summary>
    public enum EPurpose
    {
        #region Client Set

        /// <summary>
        /// 上傳地圖檔至 AGV
        /// </summary>
        UploadMapToAGV,

        /// <summary>
        /// 位置校正
        /// </summary>
        DoPositionComfirm,

        /// <summary>
        /// 設定位置
        /// </summary>
        SetPosition,

        /// <summary>
        /// 設定伺服馬達狀態
        /// </summary>
        SetServoMode,

        /// <summary>
        /// 設定手動運轉速度
        /// </summary>
        SetManualVelocity,

        /// <summary>
        /// 設定自動(工作)運轉速度
        /// </summary>
        SetWorkVelocity,

        /// <summary>
        /// 設定ID
        /// </summary>
        SetCarID,

        /// <summary>
        /// 設定名稱
        /// </summary>
        SetCarName,

        /// <summary>
        /// 設定掃描檔名稱
        /// </summary>
        SetScanningOriFileName,

        /// <summary>
        /// 停止掃描
        /// </summary>
        StopScanning,

        /// <summary>
        /// 切換地圖
        /// </summary>
        ChangeMap,

        /// <summary>
        /// 前往充電站
        /// </summary>
        DoCharging,

        /// <summary>
        /// 更改模式
        /// </summary>
        ChangeMode,

        /// <summary>
        /// 執行手動控制
        /// </summary>
        StartManualControl,

        /// <summary>
        /// 要求 AGV 自動回應路徑
        /// </summary>
        AutoReportPath,
        
        /// <summary>
        /// 跑點
        /// </summary>
        DoRuningByGoalName,

        #endregion Client Set

        #region Client Get

        /// <summary>
        /// 讀取目標點清單
        /// </summary>
        RequestGoalList,

        /// <summary>
        /// 讀取掃描檔清單
        /// </summary>
        RequestOriList,

        /// <summary>
        /// 讀取所有地圖檔清單
        /// </summary>
        RequestMapList,

        /// <summary>
        /// 要求掃描檔
        /// </summary>
        RequestOriFile,

        /// <summary>
        /// 要求地圖檔
        /// </summary>
        RequestMapFile,

        /// <summary>
        /// 要求回傳速度值
        /// </summary>
        RequestVelocity,

        /// <summary>
        /// 要求回傳加速度值
        /// </summary>
        RequestAcceleration,

        /// <summary>
        /// 要求回傳減速度值
        /// </summary>
        RequestDeceleration,

        /// <summary>
        /// 要求回傳 iTS 核心版本
        /// </summary>
        RequestITSVersion,

        /// <summary>
        /// 開啟或關閉自動回應狀態
        /// </summary>
        AutoReportStatus,

        /// <summary>
        /// 要求自動回傳雷射掃描結果
        /// </summary>
        AutoReportLaser,

        /// <summary>
        /// 要求雷射資料
        /// </summary>
        RequestLaser,

        /// <summary>
        /// 要求規劃路徑
        /// </summary>
        RequestPath,

        /// <summary>
        /// 要求AGV狀態
        /// </summary>
        RequestStatus,

        #endregion Client Get

        #region - VM -

        /// <summary>
        /// 獲得 AGV 地圖流水號
        /// </summary>
        RequestMapSerialNumber,

        /// <summary>
        /// 允許 AGV 移動
        /// </summary>
        AllowMoving,

        #endregion - VM -
    }
}