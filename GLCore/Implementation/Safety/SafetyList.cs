using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GLCore
{
    /// <summary>
    /// 具執行緒安全的 List 介面
    /// </summary>
    [Serializable]
    internal class SafetyList<T> : ISafetyList<T>
    {
        private readonly object mKey = new object();

        private List<T> mData = new List<T>();

        private DateTime mLastEdit = default(DateTime);

        public SafetyList()
        {
        }

        public SafetyList(IEnumerable<T> collection)
        {
            AddRange(collection);
        }

        public SafetyList(T item)
        {
            Add(item);
        }

        /// <summary>
        /// 資料數
        /// </summary>
        public int Count { get { lock (mKey) return mData.Count; } }

        /// <summary>
        /// 最後修改時間
        /// </summary>
        public DateTime LastEdit { get { lock (mKey) return mLastEdit; } }

        /// <summary>
        /// 回傳引索值對應的資料
        /// </summary>
        public T this[int index] { get { lock (mKey) return mData[index]; } set { lock (mKey) mData[index] = value; } }

        /// <summary>
        /// 加入新元素
        /// </summary>
        public void Add(T item)
        {
            lock (mKey)
            {
                mData.Add(item);
                mLastEdit = DateTime.Now;
            }
        }

        /// <summary>
        /// 加入多個新元素
        /// </summary>
        public void AddRange(IEnumerable<T> collection)
        {
            lock (mKey)
            {
                mData.AddRange(collection);
                mLastEdit = DateTime.Now;
            }
        }

        /// <summary>
        /// 獲得唯讀陣列
        /// </summary>
        public ReadOnlyCollection<T> AsReadOnly()
        {
            lock (mKey) return mData.AsReadOnly();
        }

        /// <summary>
        /// 清除所有資料
        /// </summary>
        public void Clear()
        {
            lock (mKey)
            {
                mData.Clear();
                mLastEdit = DateTime.Now;
            }
        }

        /// <summary>
        /// 是否存在符合項目
        /// </summary>
        public bool Exists(Predicate<T> match) { lock (mKey) return mData.Exists(match); }

        /// <summary>
        /// 移除所有符合條件的值
        /// </summary>
        public void RemoveAll(Predicate<T> match)
        {
            lock (mKey)
            {
                mData.RemoveAll(match);
                mLastEdit = DateTime.Now;
            }
        }

        /// <summary>
        /// 移除相同的元素
        /// </summary>
        public void RemoveSameData()
        {
            lock (mKey)
            {
                mData = mData.Distinct().ToList();
                mLastEdit = DateTime.Now;
            }
        }

        /// <summary>
        /// 用新元素取代舊元素
        /// </summary>
        public void Replace(IEnumerable<T> collection)
        {
            lock (mKey)
            {
                mData.Clear();
                mData.AddRange(collection);
                mLastEdit = DateTime.Now;
            }
        }

        /// <summary>
        /// 具執行緒安全的迴圈操作
        /// </summary>
        public void SafetyForLoop(Action<T> action)
        {
            lock (mKey)
            {
                foreach (var item in mData)
                {
                    action(item);
                }
            }
        }

        /// <summary>
        /// 根據條件產生子陣列
        /// </summary>
        public List<T> SubList(Predicate<T> match)
        {
            lock (mKey)
            {
                List<T> res = new List<T>();
                foreach (var item in mData)
                {
                    if (match(item)) res.Add(item);
                }
                return res;
            }
        }
    }
}