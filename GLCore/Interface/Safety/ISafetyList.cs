using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GLCore
{
    /// <summary>
    /// 具執行緒安全的 List 介面
    /// </summary>
    public interface ISafetyList<T> : ISafety
    {
        /// <summary>
        /// 最後修改時間
        /// </summary>
        DateTime LastEdit { get; }

        /// <summary>
        /// 回傳引索值對應的資料
        /// </summary>
        T this[int index] { get; set; }

        /// <summary>
        /// 加入新元素
        /// </summary>
        void Add(T item);

        /// <summary>
        /// 加入多個新元素
        /// </summary>
        void AddRange(IEnumerable<T> collection);

        /// <summary>
        /// 獲得唯讀陣列
        /// </summary>
        ReadOnlyCollection<T> AsReadOnly();

        /// <summary>
        /// 是否存在符合項目
        /// </summary>
        bool Exists(Predicate<T> match);

        /// <summary>
        /// 移除所有符合條件的值
        /// </summary>
        void RemoveAll(Predicate<T> match);

        /// <summary>
        /// 移除相同的元素
        /// </summary>
        void RemoveSameData();

        /// <summary>
        /// 用新元素取代舊元素
        /// </summary>
        void Replace(IEnumerable<T> collection);

        /// <summary>
        /// 具執行緒安全的迴圈操作
        /// </summary>
        void SafetyForLoop(Action<T> action);

        /// <summary>
        /// 根據條件產生子陣列
        /// </summary>
        List<T> SubList(Predicate<T> match);
    }
}