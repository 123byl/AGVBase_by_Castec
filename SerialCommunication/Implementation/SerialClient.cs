using AGVDefine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using static NetExtension.ExLog;
using static NetExtension.ExThread;

namespace SerialCommunication
{
    /// <summary>
    /// <para>基於 TCP/IP 的序列化同步通訊用戶端</para>
    /// </summary>
    internal class SerialClient : ISerialClient
    {
        /// <summary>
        /// <para>一對一通訊管理器</para>
        /// </summary>
        private Handler mHandler = null;

        /// <summary>
        /// <para>等待資料接收執行緒</para>
        /// </summary>
        private Thread mWaitingReciveThread = null;

        /// <summary>
        /// 連線狀態變更事件
        /// </summary>
        public event DelConnectStatusChangeEvent ConnectChange = null;

        /// <summary>
        /// <para>建立基於 TCP/IP 的序列化同步通訊用戶端</para>
        /// </summary>
        public SerialClient(DelReceiveDataEvent ReceiveDataEvent)
        {
            mReceiveDataEvent = ReceiveDataEvent;
        }

        /// <summary>
        /// <para>序列化同步通訊收到資料時所引發的事件</para>
        /// </summary>
        private event DelReceiveDataEvent mReceiveDataEvent;

        /// <summary>
        /// <para>是否正在連線</para>
        /// </summary>
        public bool Connected {
            get {
                bool? connectd = mHandler?.Socket?.Connected;
                return connectd ?? false;
            }
        }

        /// <summary>
        /// <para>本地 IP</para>
        /// </summary>
        public string LocalIPPort {
            get { return mHandler?.Socket?.LocalEndPoint?.ToString() ?? string.Empty; }
        }

        /// <summary>
        /// <para>獲得遠端 IP</para>
        /// </summary>
        public string ServerIPPort {
            get { return mHandler?.Socket?.RemoteEndPoint?.ToString() ?? string.Empty; }
        }

        /// <summary>
        /// <para>連線至遠端</para>
        /// </summary>
        public void Connect(string IP, int port)
        {
            try
            {
                ESerialComMsg.Report.WriteLog("要求連線至 {0}:{1}", IP, port);
                if (Connected)
                {
                    ESerialComMsg.Warning.WriteLog("此元件的連線已存在，無法重複連線");
                    return;
                }
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(IP), port);
                socket.Connect(remoteEP);
                mHandler = new Handler(socket, mReceiveDataEvent);
                mWaitingReciveThread = new Thread(WaitingRecive);
                mWaitingReciveThread.Name = "Waiting Recive";
                mWaitingReciveThread.IsBackground = true;
                mWaitingReciveThread.Start();
                ESerialComMsg.Report.WriteLog("成功與 {0}:{1} 進行連線", IP, port);
                ConnectChange?.Invoke(this, new ConnectStatusChangeEventArgs() { IP = IP,Port = port,IsConnected = true });
            }
            catch (Exception ex)
            {
                ESerialComMsg.Error.WriteLog(ex, "與 {0}:{1} 進行連線失敗", IP, port);
            }
        }

        /// <summary>
        /// <para>對遠端傳送訊息</para>
        /// </summary>
        public bool Send(string msg)
        {
            try
            {
                mHandler.Send(new SerialString(msg));
                return true;
            }
            catch (Exception ex)
            {
                ESerialComMsg.Error.WriteLog(ex, "發送訊息至 {0} 時失敗", ServerIPPort);
                return false;
            }
        }

        /// <summary>
        /// <para>對遠端傳送訊息</para>
        /// </summary>
        public bool Send(ICanSendBySerial msg)
        {
            try
            {
                mHandler.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                ESerialComMsg.Error.WriteLog(ex, "發送訊息至 {0} 時失敗", ServerIPPort);
                return false;
            }
        }

        /// <summary>
        /// <para>對遠端傳送 Bin 檔案</para>
        /// </summary>
        public bool SendBinFile(string path)
        {
            try
            {
                SerialBinFile file = new SerialBinFile(path);
                mHandler.Send(file);
                ESerialComMsg.Report.WriteLog("向遠端 {0} 發送資料 {1}", ServerIPPort, Path.GetFileName(path));
                return true;
            }
            catch (Exception ex)
            {
                ESerialComMsg.Error.WriteLog(ex, "發送資料 {0} 至 {1} 時失敗", path, ServerIPPort);
                return false;
            }
        }

        /// <summary>
        /// <para>停止連線</para>
        /// </summary>
        public void Stop()
        {
            lock (this) {
                // 停止連線
                try {
                    if (mHandler != null) {
                        ESerialComMsg.Report.WriteLog("要求中斷與 Server 的連線");
                        var local = mHandler.Socket.LocalEndPoint as IPEndPoint;
                        mHandler.Dispose();
                        mHandler = null;
                        mWaitingReciveThread.Kill();
                        ConnectChange?.Invoke(this, new ConnectStatusChangeEventArgs() { IP = local.Address.ToString(), Port = local.Port, IsConnected = false });
                        ESerialComMsg.Report.WriteLog("成功中斷與 Server 的連線");
                    }
                } catch (Exception ex) {
                    ESerialComMsg.Error.WriteLog(ex, "中斷連線發生錯誤");
                } finally {
                    mWaitingReciveThread = null;
                }
            }            
        }

        /// <summary>
        /// <para>等待資料接收</para>
        /// </summary>
        private void WaitingRecive()
        {
            while (Connected)
            {
                try
                {
                    if (mHandler != null && mHandler.Receive(10) == false) break;
                    mHandler.Analysis();
                }
                catch (Exception ex)
                {
                    ESerialComMsg.Error.WriteLog(ex, "接收資料錯誤");
                }
            }
            /*-- 從另一條執行緒中斷自己 --*/
            Task.Factory.StartNew(Stop);
        }

        #region IDisposable Support

        private bool disposedValue = false; // 偵測多餘的呼叫

        // 加入這個程式碼的目的在正確實作可處置的模式。
        public void Dispose()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 處置 Managed 狀態 (Managed 物件)。
                    Stop();
                }

                // TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。

                disposedValue = true;
            }
        }

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放 Unmanaged 資源的程式碼時，才覆寫完成項。
        // ~SerialClient() {
        //   // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        #endregion IDisposable Support
    }
}
