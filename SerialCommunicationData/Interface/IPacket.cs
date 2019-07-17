using AGVDefine;
using SerialCommunication;

namespace SerialCommunicationData
{
    /// <summary>
    /// <see cref="IOrderPacket{TDesign, TRequirement}"/> 與 <see cref="IProductPacket{TDesign, TRequirement}"/> 共同基礎封包
    /// </summary>
    public interface IBasicPacket : ICanSendBySerial
    {
        /// <summary>
        /// 封包目的
        /// </summary>
        EPurpose Purpose { get; }

        /// <summary>
        /// 封包流水序列號
        /// </summary>
        uint SerialNumber { get; }
    }

    /// <summary>
    /// 下單封包
    /// </summary>
    public interface IOrderPacket: IBasicPacket { }

    /// <summary>
    /// 下單封包
    /// </summary>
    /// <typeparam name="TDesign">設計圖</typeparam>
    /// <typeparam name="TRequirement">要求對方回應的產品</typeparam>
    public interface IOrderPacket<TDesign, TRequirement> :IOrderPacket
    {
        /// <summary>
        /// 訂單設計圖
        /// </summary>
        TDesign Design { get; set; }
    }

    /// <summary>
    /// 產品封包
    /// </summary>
    public interface IProductPacket: IBasicPacket { }

    /// <summary>
    /// 產品封包
    /// </summary>
    /// <typeparam name="TDesign">原始訂單設計圖</typeparam>
    /// <typeparam name="TRequirement">原始訂單產品要求</typeparam>
    public interface IProductPacket<TDesign, TRequirement> :IProductPacket
    {
        /// <summary>
        /// 產品
        /// </summary>
        TRequirement Product { get; set; }

        /// <summary>
        /// 原始訂單
        /// </summary>
        IOrderPacket<TDesign, TRequirement> Order { get; set; }

        /// <summary>
        /// 設定產品並回傳產品封包
        /// </summary>
        /// <param name="requirement"></param>
        /// <returns></returns>
        IProductPacket<TDesign, TRequirement> SetProduct(TRequirement requirement);
    }
}