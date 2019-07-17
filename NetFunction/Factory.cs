using static FactoryMode;

namespace NetFunction
{
    /// <summary>
    /// <para>擴充物件製造工廠</para>
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// <para>建立資料紀錄器</para>
        /// </summary>
        public static IRecode<T> Recode<T>(this IFactory factory, T newData, T oldData)
        {
            return new Recode<T>() { NewData = newData, OldData = oldData };
        }
    }
}
