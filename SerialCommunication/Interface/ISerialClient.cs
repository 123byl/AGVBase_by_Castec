using System;

namespace SerialCommunication
{
    /// <summary>
    /// <para>基於 TCP/IP 的序列化同步通訊用戶端介面</para>
    /// </summary>
    public interface ISerialClient : IDisposable
    {
        /// <summary>
        /// 連線狀態變更事件
        /// </summary>
        event DelConnectStatusChangeEvent ConnectChange;

        /// <summary>
        /// <para>是否正在連線</para>
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// <para>本地 IP</para>
        /// </summary>
        string LocalIPPort { get; }

        /// <summary>
        /// <para>獲得遠端 IP</para>
        /// </summary>
        string ServerIPPort { get; }

        /// <summary>
        /// <para>連線至遠端</para>
        /// </summary>
        void Connect(string IP, int port);

        /// <summary>
        /// <para>對遠端傳送訊息</para>
        /// </summary>
        bool Send(ICanSendBySerial msg);

        /// <summary>
        /// <para>對遠端傳送訊息</para>
        /// </summary>
        bool Send(string msg);

        /// <summary>
        /// <para>對遠端傳送 Bin 檔案</para>
        /// </summary>
        bool SendBinFile(string path);

        /// <summary>
        /// <para>停止連線</para>
        /// </summary>
        void Stop();
    }
}
