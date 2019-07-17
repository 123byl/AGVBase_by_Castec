using Geometry;
using System.Collections.Generic;
using System.Linq;

namespace KDTree
{
    public enum ESplitAxis
    {
        AxisX,

        AxisY,
    }

    internal class KDNode
    {
        public KDNode()
        {
        }

        public int Max { get; private set; } = int.MaxValue;
        public int Min { get; private set; } = int.MinValue;
        public IPair Node { get; private set; } = null;
        public ESplitAxis SplitAxis { get; private set; }
        private KDNode Left { get; set; } = null;
        private KDNode Right { get; set; } = null;

        public void BuildTree(IEnumerable<IPair> data)
        {
            KillTree();
            if (data.Count() == 0) return;
            if (data.Count() == 1) { Node = FactoryMode.Factory.Pair(data.First()); return; }
            var sortX = data.OrderBy((item) => item.X);
            var sortY = data.OrderBy((item) => item.Y);
            BuildTree(sortX, sortY, ESplitAxis.AxisX);
        }

        private void BuildTree(IEnumerable<IPair> sortX, IEnumerable<IPair> sortY, ESplitAxis axis)
        {
            var sort = axis == ESplitAxis.AxisX ? sortX : sortY;
            Min = axis == ESplitAxis.AxisX ? sort.First().X : sort.First().Y;
            Max = axis == ESplitAxis.AxisX ? sort.Last().X : sort.Last().Y;
            SplitAxis = axis;
            int count = sort.Count();

            if (count == 0) return;
            if (count == 1) { Node = FactoryMode.Factory.Pair(sort.First()); return; }

            Node = FactoryMode.Factory.Pair(sort.ElementAt(count / 2));

            sortX = sortX.Where((item) => item.Equals(Node));
            sortY = sortY.Where((item) => item.Equals(Node));

            var sortXR = sortX.Where(largeThenNode);
            var sortYR = sortY.Where(largeThenNode);
            var sortXL = sortX.Where(smallThenNode);
            var sortYL = sortY.Where(smallThenNode);

            if (sortXR != null && sortXR.Count() != 0)
            {
                Right = new KDNode();
                Right.BuildTree(sortXR, sortYR, axis == ESplitAxis.AxisX ? ESplitAxis.AxisY : ESplitAxis.AxisX);
            }
            if (sortXL != null && sortXL.Count() != 0)
            {
                Left = new KDNode();
                Left.BuildTree(sortXL, sortYL, axis == ESplitAxis.AxisX ? ESplitAxis.AxisY : ESplitAxis.AxisX);
            }
        }

        private void KillTree()
        {
            Left?.KillTree();
            Right.KillTree();
            Node = null;
        }

        private bool largeThenNode(IPair arg)
        {
            if (SplitAxis == ESplitAxis.AxisX)
            {
                return arg.X > Node.X;
            }
            else //(SplitAxis == ESplitAxis.AxisY)
            {
                return arg.Y > Node.Y;
            }
        }

        private bool smallThenNode(IPair arg)
        {
            return !largeThenNode(arg);
        }
    }
}
