using AGVDefine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using static NetExtension.ExLog;
using static NetExtension.ExThread;

namespace SerialCommunication
{
    /// <summary>
    /// <para>基於 TCP/IP 的序列化同步通訊伺服端</para>
    /// </summary>
    internal class SerialServer : ISerialServer
    {
        /// <summary>
        /// <para>一對一管理器執行緒鎖</para>
        /// </summary>
        private readonly object mHandlerKey = new object();

        /// <summary>
        /// <para>監聽函式鎖</para>
        /// </summary>
        private readonly object mListeningKey = new object();

        /// <summary>
        /// <para>分析資料執行緒</para>
        /// </summary>
        private Thread mAnalysisThread = null;

        /// <summary>
        /// <para>一對一管理器</para>
        /// </summary>
        private List<Handler> mHandlerList = new List<Handler>();

        /// <summary>
        /// <para>通訊伺服器</para>
        /// </summary>
        private Socket mServer = null;

        /// <summary>
        /// <para>等待連線請求執行緒</para>
        /// </summary>
        private Thread mWaitingConnectThread = null;

        /// <summary>
        /// <para>等待資料接收執行緒</para>
        /// </summary>
        private Thread mWaitingReciveThread = null;

        /// <summary>
        /// <para>當連線成功時所引起的事件</para>
        /// </summary>
        public event DelConnectStatusChangeEvent ConnectedEvent;

        /// <summary>
        /// <para>當連線結束時所引起的事件</para>
        /// </summary>
        public event DelConnectStatusChangeEvent DisconnectedEvent;

        /// <summary>
        /// <para>是否正在監聽連線請求</para>
        /// </summary>
        public bool IsListening { get; private set; } = false;

        /// <summary>
        /// <para>本地 IP</para>
        /// </summary>
        public string LocalIPPort {
            get { return mServer?.LocalEndPoint?.ToString() ?? string.Empty; }
        }

        /// <summary>
        /// <para>目前連線數量</para>
        /// </summary>
        public int OnLineCount { get { lock (mHandlerKey) return mHandlerList.Count; } }

        /// <summary>
        /// <para>正在線上的 IP:Port 列表</para>
        /// </summary>
        public List<string> OnLineIPPorts()
        {
            lock (mHandlerKey)
            {
                List<string> ips = new List<string>();
                foreach (var item in mHandlerList)
                {
                    ips.Add(item.Socket.RemoteEndPoint.ToString());
                }
                return ips;
            }
        }

        /// <summary>
        /// <para>對遠端傳送訊息</para>
        /// </summary>
        public bool Send(string IPPort, string msg)
        {
            return Send(IPPort, new SerialString(msg));
        }

        /// <summary>
        /// <para>對遠端傳送訊息</para>
        /// </summary>
        public bool Send(string IPPort, ICanSendBySerial msg)
        {
            lock (mHandlerKey)
            {
                try
                {
                    foreach (var item in mHandlerList)
                    {
                        if (item.Socket.RemoteEndPoint.ToString() == IPPort)
                        {
                            item.Send(msg);
                            return true;
                        }
                    }
                    ESerialComMsg.Warning.WriteLog("發送訊息時遠端 {0} 不存在", IPPort);
                }
                catch (Exception ex)
                {
                    ESerialComMsg.Error.WriteLog(ex, "發送訊息至 {0} 時失敗", IPPort);
                    return false;
                }
                return false;
            }
        }

        /// <summary>
        /// <para>對遠端傳送 Bin 檔案</para>
        /// </summary>
        public bool SendBinFile(string IPPort, string path)
        {
            lock (mHandlerKey)
            {
                try
                {
                    foreach (var item in mHandlerList)
                    {
                        if (item.Socket.RemoteEndPoint.ToString() == IPPort)
                        {
                            SerialBinFile file = new SerialBinFile(path);
                            item.Send(file);
                            ESerialComMsg.Report.WriteLog("向遠端 {0} 發送資料 {1}", IPPort, Path.GetFileName(path));
                            return true;
                        }
                    }
                    ESerialComMsg.Warning.WriteLog("發送資料 {0} 時遠端 {1} 不存在", path, IPPort);
                }
                catch (Exception ex)
                {
                    ESerialComMsg.Error.WriteLog(ex, "發送資料 {0} 至 {1} 時失敗", path, IPPort);
                    return false;
                }
                return false;
            }
        }

        /// <summary>
        /// <para>開始監聽連線請求</para>
        /// </summary>
        public void StartListening(int port, int backlog, DelReceiveDataEvent ReceiveDataEvent)
        {
            lock (mListeningKey)
            {
                ESerialComMsg.Report.WriteLog("要求進行監聽 port:{0}", port);
                if (IsListening)
                {
                    ESerialComMsg.Warning.WriteLog("監聽程式正在被使用中，無法重複監聽");
                    return;
                }
                IsListening = true;
                // 建立 TCP/IP 伺服端
                mServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    mServer.Bind(new IPEndPoint(IPAddress.Any, port));
                    mServer.Listen(backlog);
                    mWaitingConnectThread = new Thread(WaitingConnect);
                    mWaitingConnectThread.Name = "Waiting Connect";
                    mWaitingConnectThread.IsBackground = true;
                    mWaitingConnectThread.Start(ReceiveDataEvent);
                    mWaitingReciveThread = new Thread(WaitingRecive);
                    mWaitingReciveThread.Name = "Waiting Recive";
                    mWaitingReciveThread.IsBackground = true;
                    mWaitingReciveThread.Start();
                    mAnalysisThread = new Thread(Analysis);
                    mAnalysisThread.Name = "Analysis";
                    mAnalysisThread.IsBackground = true;
                    mAnalysisThread.Start();
                }
                catch (Exception ex)
                {
                    ESerialComMsg.Error.WriteLog(ex, "監聽程式初始化失敗");
                    StopListening();
                }
            }
        }

        /// <summary>
        /// <para>停止單一連線對象</para>
        /// </summary>
        public void StopClient(string IPPort)
        {
            lock (mHandlerKey)
            {
                ESerialComMsg.Report.WriteLog("要求中斷與 {0} 的連線", IPPort);
                // 停止連線
                foreach (var item in mHandlerList)
                {
                    if (item.Socket.RemoteEndPoint.ToString() == IPPort)
                    {
                        ConnectStatusChangeEventArgs arg = null;
                        try
                        {
                            arg = new ConnectStatusChangeEventArgs() { IP = item.IP, Port = item.Port };
                            item.Socket.Shutdown(SocketShutdown.Both);
                            item.Socket.Close();
                            DisconnectedEvent?.Invoke(this, new ConnectStatusChangeEventArgs() { IP = item.IP, Port = item.Port });
                        }
                        catch (Exception ex)
                        {
                            ESerialComMsg.Error.WriteLog(ex, "中斷連線發生錯誤");
                        }
                        finally
                        {
                            DisconnectedEvent?.Invoke(this, arg);
                        }
                    }
                }
                // 移除列表
                mHandlerList.RemoveAll((item) => !item.Socket.Connected);
            }
        }

        /// <summary>
        /// <para>停止全部連線對象</para>
        /// </summary>
        public void StopClient()
        {
            lock (mHandlerKey)
            {
                ESerialComMsg.Report.WriteLog("要求中斷與所有 Client 的連線");
                // 停止連線
                foreach (var item in mHandlerList)
                {
                    ConnectStatusChangeEventArgs arg = null;
                    try
                    {
                        arg = new ConnectStatusChangeEventArgs() { IP = item.IP, Port = item.Port };
                        item.Socket.Shutdown(SocketShutdown.Both);
                        item.Socket.Close();
                    }
                    catch (Exception ex)
                    {
                        ESerialComMsg.Error.WriteLog(ex, "中斷連線發生錯誤");
                    }
                    finally
                    {
                        DisconnectedEvent?.Invoke(this, arg);
                    }
                }
                // 移除列表
                mHandlerList.RemoveAll((item) => !item.Socket.Connected);
            }
        }

        /// <summary>
        /// <para>停止連線請求</para>
        /// </summary>
        public void StopListening()
        {
            lock (mListeningKey)
            {
                ESerialComMsg.Report.WriteLog("要求關閉監聽");
                try
                {
                    StopClient();
                    mServer?.Close();
                    mWaitingConnectThread?.Kill();
                    mWaitingReciveThread?.Kill();
                    mAnalysisThread?.Kill();
                }
                catch (Exception ex)
                {
                    ESerialComMsg.Error.WriteLog(ex, "關閉監聽發生錯誤");
                }
                finally
                {
                    mWaitingConnectThread = null;
                    mWaitingReciveThread = null;
                    mAnalysisThread = null;
                    IsListening = false;
                    mServer = null;
                }
            }
        }

        /// <summary>
        /// <para>分析資料迴圈</para>
        /// </summary>
        private void Analysis()
        {
            int index = 0;
            while (true)
            {
                Handler handler = null;
                while (true)
                {
                    lock (mHandlerKey)
                    {
                        if (mHandlerList.Count == 0) break;
                        index++;
                        if (index >= mHandlerList.Count) index = 0; // 處理到最後一個 Client 就回到第一筆來處理
                        handler = mHandlerList[index];
                    }
                    handler?.Analysis();
                }
                SpinWait.SpinUntil(() => false, 1);
            }
        }

        /// <summary>
        /// <para>等待連線請求</para>
        /// </summary>
        private void WaitingConnect(object ReceiveDataEvent)
        {
            while (IsListening)
            {
                try
                {
                    Socket socket = mServer.Accept();
                    Handler handler = new Handler(socket, (DelReceiveDataEvent)ReceiveDataEvent);
                    lock (mHandlerKey) mHandlerList.Add(handler);
                    ConnectedEvent?.Invoke(this, new ConnectStatusChangeEventArgs() { IP = handler.IP, Port = handler.Port });
                    ESerialComMsg.Report.WriteLog("{0} 已接受來自 {1} 的連線", handler.Socket.LocalEndPoint.ToString(), handler.Socket.RemoteEndPoint.ToString());
                }
                catch (Exception ex)
                {
                    ESerialComMsg.Error.WriteLog(ex, "接受連線錯誤");
                }
            }
        }

        /// <summary>
        /// <para>等待資料接收</para>
        /// </summary>
        private void WaitingRecive()
        {
            int index = 0;
            while (true)
            {
                Handler handler = null;
                while (true)
                {
                    lock (mHandlerKey)
                    {
                        if (mHandlerList.Count == 0) break;
                        index++;
                        if (index >= mHandlerList.Count) index = 0; // 處理到最後一個 Client 就回到第一筆來處理
                        handler = mHandlerList[index];
                    }
                    if (handler != null && handler.Receive(10) == false) StopClient(handler.Socket.RemoteEndPoint.ToString());
                }
            }
        }

        #region IDisposable Support

        private bool disposedValue = false;

        /// <summary>
        /// <para>釋放資源</para>
        /// </summary>
        ~SerialServer()
        {
            Dispose(false);
        }

        // 偵測多餘的呼叫

        // 加入這個程式碼的目的在正確實作可處置的模式。
        /// <summary>
        /// <para>釋放資源</para>
        /// </summary>
        public void Dispose()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// <para>釋放資源</para>
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 處置 Managed 狀態 (Managed 物件)。
                    //mServer.Shutdown(SocketShutdown.Both);
                    StopListening();
                }

                // TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。

                disposedValue = true;
            }
        }

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放 Unmanaged 資源的程式碼時，才覆寫完成項。
        // ~SerialServer() {
        //   // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        #endregion IDisposable Support
    }
}
