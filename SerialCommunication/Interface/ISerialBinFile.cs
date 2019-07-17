namespace SerialCommunication
{
    /// <summary>
    /// <para>可被序列化的檔案(byte array)物件介面</para>
    /// </summary>
    public interface ISerialBinFile : ICanSendBySerial
    {
        /// <summary>
        /// <para>檔案資料</para>
        /// </summary>
        byte[] Data { get; set; }

        /// <summary>
        /// <para>檔案路徑</para>
        /// </summary>
        string Path { get; set; }
    }
}
