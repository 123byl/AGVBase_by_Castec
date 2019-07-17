using AGVDefine;
using System;

namespace SerialCommunicationData
{
    /// <summary>
    /// 流水號管理器
    /// </summary>
    internal static class SerialNumberManager {
        private static uint mSerialNumber = uint.MinValue;
        public static uint GenerateSerialNumber() {
            if (mSerialNumber == uint.MaxValue) {
                mSerialNumber = uint.MinValue;
            } else {
                mSerialNumber++;
            }
            return mSerialNumber;
        }
    }

    /// <summary>
    /// 下單封包
    /// </summary>
    /// <typeparam name="TDesign">設計圖</typeparam>
    /// <typeparam name="TRequirement">要求對方回應的產品</typeparam>
    [Serializable]
    internal class OrderPacket<TDesign, TRequirement> : IOrderPacket<TDesign, TRequirement>
    {
        /// <summary>
        /// 訂單設計圖
        /// </summary>
        public TDesign Design { get; set; }

        /// <summary>
        /// 下單種類
        /// </summary>
        public EPurpose Purpose { get; set; }

        /// <summary>
        /// 流水序列號
        /// </summary>
        public uint SerialNumber { get; set; } = SerialNumberManager.GenerateSerialNumber();
    }

    /// <summary>
    /// 產品封包
    /// </summary>
    /// <typeparam name="TDesign">原始訂單設計圖</typeparam>
    /// <typeparam name="TRequirement">原始訂單產品要求</typeparam>
    [Serializable]
    internal class ProductPacket<TDesign, TRequirement> : IProductPacket<TDesign, TRequirement>
    {
        /// <summary>
        /// 產品
        /// </summary>
        public TRequirement Product { get; set; }

        /// <summary>
        /// 產品種類
        /// </summary>
        public EPurpose Purpose { get; set; }

        /// <summary>
        /// 流水序列號
        /// </summary>
        public uint SerialNumber { get { return Order.SerialNumber; }}

        /// <summary>
        /// 原始訂單
        /// </summary>
        public IOrderPacket<TDesign, TRequirement> Order { get; set; }

        /// <summary>
        /// 設定產品並回傳產品封包
        /// </summary>
        /// <param name="requirement"></param>
        /// <returns></returns>
        public IProductPacket<TDesign,TRequirement> SetProduct(TRequirement requirement) {
            Product = requirement;
            return this;
        } 
    }
}