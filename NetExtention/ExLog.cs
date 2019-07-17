using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace NetExtension
{
    /// <summary>
    /// <para>提供以列舉型別(enum)為操作對象的 Log 檔案紀錄操作</para>
    /// <para>對於每種不同的列舉型別(enum)會產生對應的檔案分開儲存 Log</para>
    /// <para>目前預設每隔 5000ms(<see cref="ExLog.QueueMaxIntervals"/> ) 會檢查 Log 佇列並強制儲存、清空佇列 </para>
    /// <para>或當檔案大於 50筆(<see cref="ExLog.QueueMaxCount"/> ) 時也會強制儲存</para>
    /// <para>儲存所使用的執行緒為"前景執行緒"，這表示當使用者關閉視窗時，佇列中的殘餘項目也會被儲存</para>
    /// </summary>
    public static class ExLog
    {
        /// <summary>
        /// <para>Log 檔存放佇列</para>
        /// </summary>
        private static LogQueue mLogQueue = new LogQueue();

        /// <summary>
        /// <para>每隔多久時間(minute)需重新建立文字檔來儲存紀錄</para>
        /// </summary>
        public static int FileMaxMinuteIntervals { get; set; } = 1 * 60;

        /// <summary>
        /// <para>佇列站存器中允許的最大行數</para>
        /// </summary>
        public static int QueueMaxCount { get; set; } = 50;

        /// <summary>
        /// <para>每隔多久時間(ms)將 Log 強制紀錄至硬碟</para>
        /// </summary>
        public static int QueueMaxIntervals { get; set; } = 5 * 1000;

        /// <summary>
        /// <para>是否一併顯將錯誤示在 Console 視窗</para>
        /// </summary>
        public static bool ShowInConsole { get; set; } = false;

        /// <summary>
        /// <para>強制儲存，並等待儲存完畢</para>
        /// </summary>
        public static void Flush()
        {
            mLogQueue.Flush();
        }

        /// <summary>
        /// <para>將訊息 Log 寫入 (<paramref name="statusEnum"/>+時間編號).txt 中 </para>
        /// </summary>
        public static void WriteLog<TEnum>(this TEnum statusEnum, Exception ex, string str = "") where TEnum : struct, IConvertible
        {
            try
            {
                StackFrame sf = new StackTrace(ex, true).GetFrame(1);
                string file = Path.GetFileName(sf.GetFileName());
                int line = sf.GetFileLineNumber();
                string method = sf.GetMethod().Name;
                statusEnum.WriteLog(string.Format("e.src:{0}({1}){2},e.msg:{3} - {4}", file, line, method, ex.Message, str));
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// <para>將訊息 Log 寫入 (<paramref name="statusEnum"/>+時間編號).txt 中 </para>
        /// </summary>
        public static void WriteLog<TEnum>(this TEnum statusEnum, string format, params object[] arg) where TEnum : struct, IConvertible
        {
            string str = string.Format(format, arg);
            statusEnum.WriteLog(str);
        }

        /// <summary>
        /// <para>將訊息 Log 寫入 (<paramref name="statusEnum"/>+時間編號).txt 中 </para>
        /// </summary>
        public static void WriteLog<TEnum>(this TEnum statusEnum, Exception ex, string format, params object[] arg) where TEnum : struct, IConvertible
        {
            string str = string.Format(format, arg);
            statusEnum.WriteLog(ex, str);
        }

        /// <summary>
        /// <para>將訊息 Log 寫入 (<paramref name="statusEnum"/>+時間編號).txt 中 </para>
        /// </summary>
        public static void WriteLog<TEnum>(this TEnum statusEnum, string str) where TEnum : struct, IConvertible
        {
            var setting = new Setting<TEnum>();
            string logMsg = string.Format("[{0}]{1} - {2}", DateTime.Now.ToString("yyyyMMddHHmmss:fff"), statusEnum, str);
            if (setting.CanWriteToText_) mLogQueue.Enqueue(new LogInfo() { EnumType = typeof(TEnum).Name, Mssage = logMsg });
            if (ShowInConsole && setting.CanShowInConsole_) Console.WriteLine("{0} - {1}", typeof(TEnum).Name, logMsg);
        }

        /// <summary>
        /// <para> Log 資訊</para>
        /// </summary>
        private struct LogInfo
        {
            /// <summary>
            /// <para>列舉名稱</para>
            /// </summary>
            public string EnumType { get; set; }

            /// <summary>
            /// <para>儲存訊息</para>
            /// </summary>
            public string Mssage { get; set; }
        }

        /// <summary>
        /// <para>模式設定</para>
        /// </summary>
        public class Setting<TEnum> where TEnum : struct, IConvertible
        {
            /// <summary>
            /// <para>是否可以在 Debug 視窗中顯示</para>
            /// </summary>
            public static bool CanShowInConsole { get; set; } = true;

            /// <summary>
            /// <para>是否可以儲存至 Text 檔</para>
            /// </summary>
            public static bool CanWriteToText { get; set; } = true;

            /// <summary>
            /// <para>是否可以在 Debug 視窗中顯示</para>
            /// </summary>
            internal bool CanShowInConsole_ { get { return CanShowInConsole; } set { CanShowInConsole = value; } }

            /// <summary>
            /// <para>是否可以儲存至 Text 檔</para>
            /// </summary>
            internal bool CanWriteToText_ { get { return CanWriteToText; } set { CanWriteToText = value; } }
        }

        /// <summary>
        /// <para>Log 檔私有存放佇列實作</para>
        /// </summary>
        private class LogQueue
        {
            /// <summary>
            /// <para>呼叫強制寫入時執行緒鎖</para>
            /// </summary>
            private readonly object mFlushKey = new object();

            /// <summary>
            /// <para>尚未寫入硬碟的資料列</para>
            /// </summary>
            private List<LogInfo> mData = new List<LogInfo>();

            /// <summary>
            /// <para>最後一個檔案所建立的時間</para>
            /// </summary>
            private DateTime mFileCreatTime = DateTime.Now;

            /// <summary>
            /// <para>呼叫強制儲存的 Thread</para>
            /// </summary>
            private Thread mFlushThread;

            public LogQueue()
            {
                mFlushThread = new Thread(FlushLoop);
                mFlushThread.IsBackground = true;
                mFlushThread.Name = "Log Flush";
                mFlushThread.Start();
            }

            /// <summary>
            /// <para>加入要存檔的文字</para>
            /// </summary>
            public void Enqueue(LogInfo log)
            {
                lock (mFlushKey)
                {
                    mData.Add(log);
                }
            }

            /// <summary>
            /// <para>強制儲存，並等待儲存完畢</para>
            /// </summary>
            public void Flush()
            {
                lock (mFlushKey)
                {
                    if ((DateTime.Now - mFileCreatTime).TotalMinutes >= FileMaxMinuteIntervals) mFileCreatTime = DateTime.Now;
                    while (mData.Count > 0)
                    {
                        string enumType = mData[0].EnumType;
                        string fileName = string.Format("{0}.{1}.txt", mData[0].EnumType, mFileCreatTime.ToString("yyMMddHHmm"));
                        using (StreamWriter outputFile = new StreamWriter(fileName, true))
                        {
                            foreach (var item in mData)
                            {
                                if (item.EnumType == enumType) outputFile.WriteLine(item.Mssage);
                            }
                            mData.RemoveAll((item) => item.EnumType == enumType);
                            outputFile.Flush();
                        }
                    }
                }
            }

            /// <summary>
            /// <para>強制儲存迴圈</para>
            /// </summary>
            private void FlushLoop()
            {
                while (true)
                {
                    try
                    {
                        Flush();
                    }
                    catch (Exception)
                    {
                    }
                    SpinWait.SpinUntil(() => false, QueueMaxIntervals);
                }
            }
        }
    }
}
