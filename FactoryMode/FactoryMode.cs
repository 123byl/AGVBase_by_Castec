/// <summary>
/// <para>工廠模式擴充管理器</para>
/// <para>方案中所有的實作都將被隱藏(internal)，只開放介面使用</para>
/// <para>為了讓使用者方便管理製造工廠</para>
/// <para>請將物件製造方法寫成如以下格式</para>
/// <code>
/// public static IAngle Angle(this IFactory factory)
/// {
///     return new Angle();
/// }
/// </code>
/// <para>使用方式為呼叫：</para>
/// <code>
/// <see cref="Factory"/>.Angle();
/// </code>   
/// </summary>
public static class FactoryMode
{
    /// <summary>
    /// <para>製造工廠介面</para>
    /// </summary>
    public interface IFactory { }

    /// <summary>
    /// <para>製造工廠</para>
    /// </summary>
    public static IFactory Factory { get; } = new FactoryImplementation();

    /// <summary>
    /// <para>製造工廠實作</para>
    /// </summary>
    internal class FactoryImplementation : IFactory { }
}
