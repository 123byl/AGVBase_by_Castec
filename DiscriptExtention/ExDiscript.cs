using System;
using System.Collections.Generic;

namespace DiscriptExtention
{
    /// <summary>
    /// 描述屬性擴充方法
    /// </summary>
    public static class ExDiscript
    {
        /// <summary>
        /// 以陣列方式回傳在 <see cref="Enum"/> 中所有成員的 <see cref="DiscriptAttribute.Content"/> 內容
        /// </summary>
        public static IEnumerable<string> GetAllContectAtEnum<T>()
        {
            List<string> res = new List<string>();
            Array values = Enum.GetValues(typeof(T));
            foreach (var value in values)
            {
                res.Add(((T)value).GetContent());
            }
            return res;
        }

        /// <summary>
        /// 使用反射動態取得 <see cref="DiscriptAttribute.Content"/> 內容，
        /// 如果 <see cref="DiscriptAttribute.Content"/> 為 null， 則返回 <paramref name="status"/> 名稱
        /// </summary>
        public static string GetContent<T>(this T status)
        {
            var field = typeof(T).GetField(status.ToString());
            if (field == null) return status.ToString();
            var attr = Attribute.GetCustomAttribute(field, typeof(DiscriptAttribute)) as DiscriptAttribute;
            return attr?.Content ?? status.ToString();
        }
    }

    /// <summary>
    /// 描述屬性
    /// </summary>
    public class DiscriptAttribute : Attribute
    {
        /// <summary>
        /// 描述內容
        /// </summary>
        public string Content { get; set; }
    }
}
