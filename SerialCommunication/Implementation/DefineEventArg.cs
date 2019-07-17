using System;
using System.Net.Sockets;

namespace SerialCommunication
{
    /// <summary>
    /// <para>連線狀態改變時所引發的事件定義</para>
    /// </summary>
    public delegate void DelConnectStatusChangeEvent(object sender, ConnectStatusChangeEventArgs e);

    /// <summary>
    /// <para>序列化同步通訊收到資料時所引發的事件定義</para>
    /// </summary>
    public delegate void DelReceiveDataEvent(object sender, ReceiveDataEventArgs e);

    /// <summary>
    /// <para>連線狀態改變時所引發的事件參數</para>
    /// </summary>
    public class ConnectStatusChangeEventArgs : EventArgs
    {
        /// <summary>
        /// <para>遠端 IP</para>
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// <para>遠端埠號</para>
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 連線狀態
        /// </summary>
        public bool IsConnected { get; set; }
    }

    /// <summary>
    /// <para>序列化同步通訊收到資料時所引發的事件參數</para>
    /// </summary>
    public class ReceiveDataEventArgs : EventArgs
    {
        /// <summary>
        /// <para>建立序列化同步通訊收到資料時所引發的事件參數</para>
        /// </summary>
        public ReceiveDataEventArgs(ICanSendBySerial data, Socket remote)
        {
            Data = data;
            Remote = remote;
        }

        /// <summary>
        /// <para>序列化通訊資料</para>
        /// </summary>
        public ICanSendBySerial Data { get; }

        /// <summary>
        /// <para>通訊對象</para>
        /// </summary>
        public Socket Remote { get; }
    }
}
