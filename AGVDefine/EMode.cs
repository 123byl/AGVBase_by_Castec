using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGVDefine {

    /// <summary>
    /// 車子模式
    /// </summary>
    public enum EMode : byte {
        /// <summary>
        /// 閒置
        /// </summary>
        Idle,
        /// <summary>
        /// 工作
        /// </summary>
        Work,
        /// <summary>
        /// 掃圖
        /// </summary>
        Map,
        /// <summary>
        /// 離線
        /// </summary>
        OffLine = 255
    }

}
