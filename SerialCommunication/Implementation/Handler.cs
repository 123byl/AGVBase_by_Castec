using AGVDefine;
using NetExtension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SerialCommunication
{
    /// <summary>
    /// <para>一對一通訊管理器</para>
    /// </summary>
    internal class Handler:IDisposable
    {
        /// <summary>
        /// <para>忙碌狀態鎖</para>
        /// </summary>
        private readonly object mBussyKey = new object();

        /// <summary>
        /// <para>資料收集區</para>
        /// </summary>
        private List<byte> Buffer = new List<byte>();

        /// <summary>
        /// <para>使用現有接口來產生通訊管理器，不檢查 <paramref name="socket"/> 是否為 null </para>
        /// </summary>
        public Handler(Socket socket, DelReceiveDataEvent ReceiveDataEvent)
        {
            Socket = socket;
            var g = new SocketAsyncEventArgs();
            string[] tmps = socket.RemoteEndPoint.ToString().Split(':');
            IP = tmps[0];
            Port = int.Parse(tmps[1]);
            if (ReceiveDataEvent != null) this.ReceiveDataEvent += ReceiveDataEvent;
        }

        /// <summary>
        /// <para>序列化同步通訊收到資料時所引發的事件</para>
        /// </summary>
        public event DelReceiveDataEvent ReceiveDataEvent;

        /// <summary>
        /// <para>遠端 IP</para>
        /// </summary>
        public string IP { get; }

        /// <summary>
        /// <para>接收資料區是否發生改變</para>
        /// </summary>
        public bool IsBufferChanged { get; private set; }

        /// <summary>
        /// <para>是否處於忙碌狀態</para>
        /// </summary>
        public bool IsBussy { get; private set; } = false;

        /// <summary>
        /// <para>遠端埠號</para>
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// <para>連線接口</para>
        /// </summary>
        public Socket Socket { get; private set; } = null;

        /// <summary>
        /// <para>分析資料</para>
        /// </summary>
        public void Analysis()
        {
            List<byte> res = null;
            try
            {
                lock (mBussyKey)
                {
                    if (IsBussy) return; // 系統忙碌中，無法分析資料
                    IsBussy = true;
                }

                res = Buffer.Depakage();
                if (res != null)
                {
                    ESerialComMsg.Report.WriteLog("分析出資料 {0} bytes, 剩餘資料 {1} bytes", res.Count, Buffer.Count);
                    RaiseReceiveDataEvent(res);
                }
            }
            catch (IOException ex)
            {
                ESerialComMsg.Error.WriteLog(ex, "分析資料錯誤");
            }

            // 分析資料函式結束
            lock (mBussyKey) IsBussy = false;
        }

        /// <summary>
        /// <para>接收資料，回傳 True 表示連線正常，回傳 False 表示斷線</para>
        /// </summary>
        public bool Receive(int timeout)
        {
            int total = 0;
            bool connected = true;
            try
            {
                lock (mBussyKey)
                {
                    if (IsBussy) return connected; // 系統忙碌中，無法接收資料
                    IsBussy = true;
                }

                using (NetworkStream stream = new NetworkStream(Socket))
                {
                    stream.ReadTimeout = timeout;
                    if (stream.CanRead && stream.DataAvailable)
                    {
                        byte[] buffer = new byte[Socket.ReceiveBufferSize];
                        int size = stream.Read(buffer, 0, buffer.Length);
                        if (size == 0)
                        {
                            connected = false;
                        }
                        total += size;
                        IsBufferChanged = true;
                        if (size < buffer.Length) Array.Resize(ref buffer, size);
                        Buffer.AddRange(buffer);
                    }
                }
            }
            catch (IOException io)
            {
                // 無資料可讀取
                Console.WriteLine(io.Message);
            }
            catch (Exception ex)
            {
                connected = false;
                ESerialComMsg.Error.WriteLog(ex, "接收資料錯誤");
            }

            // 接收資料函式結束
            if (total != 0)
            {
                ESerialComMsg.Report.WriteLog("已接收來自 {0} 的資料共 {1} bytes", Socket.RemoteEndPoint.ToString(), total);
            }
            lock (mBussyKey) IsBussy = false;
            return connected;
        }

        /// <summary>
        /// <para>對遠端傳送訊息</para>
        /// </summary>
        public void Send(ICanSendBySerial msg)
        {
            Socket.Send(msg.SerializeToStream().ToArray().Package());
        }

        /// <summary>
        /// <para>發布資料</para>
        /// </summary>
        private void RaiseReceiveDataEvent(List<byte> res)
        {
            if (ReceiveDataEvent == null) return;
            Task t = new Task(() =>
            {
                try
                {
                    using (MemoryStream stream = new MemoryStream(res.ToArray()))
                    {
                        ICanSendBySerial data = stream.DeserializeFromStream<ICanSendBySerial>();
                        if (data == null || ReceiveDataEvent == null) return;
                        ReceiveDataEvent?.Invoke(this, new ReceiveDataEventArgs(data, Socket));
                    }
                }
                catch (Exception ex)
                {
                    ESerialComMsg.Error.WriteLog(ex, "反序列化失敗或發布事件失敗");
                }
            });

            t.Start();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 偵測多餘的呼叫

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: 處置 Managed 狀態 (Managed 物件)。
                    Socket.Shutdown(SocketShutdown.Both);
                    Socket.Close();
                    Socket.Dispose();
                    Socket = null;
                }

                // TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。

                disposedValue = true;
            }
        }

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放 Unmanaged 資源的程式碼時，才覆寫完成項。
        // ~Handler() {
        //   // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 加入這個程式碼的目的在正確實作可處置的模式。
        public void Dispose() {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
