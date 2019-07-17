using static FactoryMode;

namespace SerialCommunication
{
    /// <summary>
    /// <para>擴充物件製造工廠</para>
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// <para>建立可被序列化的檔案(byte array)物件</para>
        /// </summary>
        public static ISerialBinFile SerialBinFile(this IFactory factory, string path)
        {
            return new SerialBinFile(path);
        }

        /// <summary>
        /// <para>建立基於 TCP/IP 的序列化同步通訊用戶端物件</para>
        /// </summary>
        public static ISerialClient SerialClient(this IFactory factory, DelReceiveDataEvent ReceiveDataEvent)
        {
            return new SerialClient(ReceiveDataEvent);
        }

        /// <summary>
        /// <para>建立基於 TCP/IP 的序列化同步通訊伺服端物件</para>
        /// </summary>
        public static ISerialServer SerialServer(this IFactory factory)
        {
            return new SerialServer();
        }

        /// <summary>
        /// <para>建立可被序列化的文字物件</para>
        /// </summary>
        public static ISerialString SerialString(this IFactory factory, string data)
        {
            return new SerialString(data);
        }
    }
}
