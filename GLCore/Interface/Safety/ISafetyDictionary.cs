using System;
using System.Collections.Generic;

namespace GLCore
{
    /// <summary>
    /// 具執行緒安全的字典介面，引索值為 uint
    /// </summary>
    public interface ISafetyDictionary
    {
        /// <summary>
        /// 是否包含該ID
        /// </summary>
        bool ContainsID(uint id);

        /// <summary>
        /// 回傳物件索引
        /// </summary>
        int IndexOf(uint uid);

        /// <summary>
        /// 移除指定元素
        /// </summary>
        void Remove(uint id);

        /// <summary>
        /// 將所有元素逐一轉成字串，並以陣列方式回傳結果
        /// </summary>
        List<string> ToStringList();
    }

    /// <summary>
    /// 具執行緒安全的字典介面，引索值為 uint
    /// </summary>
    public interface ISafetyDictionary<T> : ISafetyDictionary, ISafety, IDrawable
    {
        /// <summary>
        /// 回傳該 ID 對應的物件資料
        /// </summary>
        T this[uint id] { get; set; }

        /// <summary>
        /// 根據ID加入新元素，若ID本身存在，則將原本元素取代成新元素
        /// </summary>
        void Add(uint id, T value);

        /// <summary>
        /// 具執行緒安全編輯
        /// </summary>
        void SafetyEdit(uint id, Action<T> action);

        /// <summary>
        /// 具執行緒安全的迴圈操作
        /// </summary>
        void SafetyForLoop(Action<uint, T> action);
    }
}