using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGVDefine {
    public enum EPort {

        VMPort = 8000,

        /// <summary>
        /// Port for
        /// </summary>
        VehiclePlanner = 8090,

        port_receiveCmd = 400,
        port_sendState = 800,
        port_sendFile = 600,
        port_receiveFile = 700,
        port_sendPath = 900,
    }
}
