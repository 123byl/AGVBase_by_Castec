namespace SerialCommunication
{
    /// <summary>
    /// <para>可被序列化的文字物件介面</para>
    /// </summary>
    public interface ISerialString : ICanSendBySerial
    {
        /// <summary>
        /// <para>檔案資料</para>
        /// </summary>
        string Data { get; set; }
    }
}
