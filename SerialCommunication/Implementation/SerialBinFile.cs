using System;
using System.IO;

namespace SerialCommunication
{
    /// <summary>
    /// <para>可被序列化的檔案(byte array)物件</para>
    /// </summary>
    [Serializable]
    internal class SerialBinFile : ISerialBinFile
    {
        /// <summary>
        /// <para>自動以 byte array 的方式讀取檔案</para>
        /// </summary>
        public SerialBinFile(string path)
        {
            Path = path;
            Data = File.ReadAllBytes(path);
        }

        /// <summary>
        /// <para>檔案資料</para>
        /// </summary>
        public byte[] Data { get; set; } = null;

        /// <summary>
        /// <para>檔案路徑</para>
        /// </summary>
        public string Path { get; set; } = string.Empty;
    }
}
