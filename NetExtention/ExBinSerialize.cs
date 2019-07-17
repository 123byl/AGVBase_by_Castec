using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace NetExtension
{
    /// <summary>
    /// <para>提供可序列化物件(T)的序列化轉換、檔案存取、物件比較等擴充方法</para>
    /// <para>物件(T)若要序列化，需要標記為[Serializable]</para>
    /// <para>程式中不主動檢查物件(T)是否有標記[Serializable]</para>
    /// </summary>
    public static class ExBinSerialize
    {
        /// <summary>
        /// <para>以二進制序列化(<see cref="BinaryFormatter"/>)方式比較兩物件是否相等</para>
        /// </summary>
        public static bool BinEqual<T>(this T lhs, T rhs)
        {
            using (MemoryStream mlhs = lhs.SerializeToStream())
            {
                using (MemoryStream mrhs = rhs.SerializeToStream())
                {
                    return mlhs.ToArray().SequenceEqual(mrhs.ToArray());
                }
            }
        }

        /// <summary>
        /// <para>深複製</para>
        /// <para>提供可序列化物件(T)的序列化複製功能</para>
        /// </summary>
        public static T DeepCopy<T>(this T obj)
        {
            using (MemoryStream stream = obj.SerializeToStream())
            {
                return DeserializeFromStream<T>(stream);
            }
        }

        /// <summary>
        /// <para>反序列化</para>
        /// <para>將資料流(<see cref="MemoryStream"/>)反序列化成物件(T)</para>
        /// </summary>
        public static T DeserializeFromStream<T>(this MemoryStream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            object obj = formatter.Deserialize(stream);
            return (T)obj;
        }

        /// <summary>
        /// <para>將檔案寫入資料流(<see cref="MemoryStream"/>)<paramref name="stream"/>中</para>
        /// <para>不檢查檔案是否存在</para>
        /// </summary>
        public static void ReadFromFile(this MemoryStream stream, string fileName)
        {
            using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                file.CopyTo(stream);
            }
        }

        /// <summary>
        /// <para>將資料流(<see cref="MemoryStream"/>)覆寫入檔案中</para>
        /// </summary>
        public static void SaveToFile(this MemoryStream stream, string fileName)
        {
            File.WriteAllBytes(fileName, stream.ToArray());
        }

        /// <summary>
        /// <para>序列化</para>
        /// <para>將有標記可序列化[Serializable]的物件以二進制序列化(<see cref="BinaryFormatter"/>)成資料流</para>
        /// </summary>
        public static MemoryStream SerializeToStream(this object obj)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            return stream;
        }
    }


}
