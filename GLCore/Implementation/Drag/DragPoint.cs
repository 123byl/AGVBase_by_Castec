using Geometry;

namespace GLCore
{
    /// <summary>
    /// 拖曳控制點
    /// </summary>
    internal class DragPoint : IDragPoint
    {
        public DragPoint(IPair point, int size, DelDragAction action)
        {
            Point = FactoryMode.Factory.Pair(point);
            Size = FactoryMode.Factory.Pair(size, size);
            DragActionEvent = action;
        }

        public DragPoint(IPair point, IPair size, DelDragAction action)
        {
            Point = FactoryMode.Factory.Pair(point);
            Size = FactoryMode.Factory.Pair(size);
            DragActionEvent = action;
        }

        public DragPoint(IPair point, DelDragAction action)
        {
            Point = FactoryMode.Factory.Pair(point);
            Size = FactoryMode.Factory.Pair(100, 100);
            DragActionEvent = action;
        }

        /// <summary>
        /// 拖曳點更新時動作
        /// </summary>
        public DelDragAction DragActionEvent { get; set; }

        /// <summary>
        /// 拖曳點
        /// </summary>
        public IPair Point { get; private set; } = FactoryMode.Factory.Pair();

        /// <summary>
        /// 拖曳點大小
        /// </summary>
        public IPair Size { get; private set; } = FactoryMode.Factory.Pair();

        /// <summary>
        /// 更新拖曳點座標
        /// </summary>
        public void UpdateDragPoint(IPair newPoint)
        {
            DragActionEvent?.Invoke(newPoint);
        }
    }
}