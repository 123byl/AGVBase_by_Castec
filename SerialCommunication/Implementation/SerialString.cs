using System;

namespace SerialCommunication
{
    /// <summary>
    /// <para>可被序列化的文字物件</para>
    /// </summary>
    [Serializable]
    internal class SerialString : ISerialString
    {
        /// <summary>
        /// <para>建立符合序列化通訊界面的傳遞文字物件</para>
        /// </summary>
        public SerialString(string data)
        {
            Data = data;
        }

        /// <summary>
        /// <para>檔案資料</para>
        /// </summary>
        public string Data { get; set; }
    }
}
