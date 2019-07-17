using System;
using System.Collections.Generic;

namespace SerialCommunication
{
    /// <summary>
    /// <para>基於 TCP/IP 的序列化同步通訊伺服端介面</para>
    /// </summary>
    public interface ISerialServer : IDisposable
    {
        /// <summary>
        /// <para>當連線成功時所引起的事件</para>
        /// </summary>
        event DelConnectStatusChangeEvent ConnectedEvent;

        /// <summary>
        /// <para>當連線結束時所引起的事件</para>
        /// </summary>
        event DelConnectStatusChangeEvent DisconnectedEvent;

        /// <summary>
        /// <para>是否正在監聽連線請求</para>
        /// </summary>
        bool IsListening { get; }

        /// <summary>
        /// <para>本地 IP</para>
        /// </summary>
        string LocalIPPort { get; }

        /// <summary>
        /// <para>目前連線數量</para>
        /// </summary>
        int OnLineCount { get; }

        /// <summary>
        /// <para>正在線上的 IP:Port 列表</para>
        /// </summary>
        List<string> OnLineIPPorts();

        /// <summary>
        /// <para>對遠端傳送訊息</para>
        /// </summary>
        bool Send(string IPPort, ICanSendBySerial msg);

        /// <summary>
        /// <para>對遠端傳送訊息</para>
        /// </summary>
        bool Send(string IPPort, string msg);

        /// <summary>
        /// <para>對遠端傳送 Bin 檔案</para>
        /// </summary>
        bool SendBinFile(string IPPortstring, string path);

        /// <summary>
        /// <para>開始監聽連線請求</para>
        /// </summary>
        void StartListening(int port, int backlog, DelReceiveDataEvent ReceiveDataEvent);

        /// <summary>
        /// <para>停止全部連線對象</para>
        /// </summary>
        void StopClient();

        /// <summary>
        /// <para>停止單一連線對象</para>
        /// </summary>
        void StopClient(string IPPort);

        /// <summary>
        /// <para>停止連線請求</para>
        /// </summary>
        void StopListening();
    }
}
