using System;

namespace Heureka.Common
{
    public abstract class AbstractNode : IComparable
    {
        public AbstractState State { get; set; }
        public AbstractState Target { get; set; }
        public AbstractNode Parent { get; set; }
        public AbstractAction Action { get; set; }

        public double EstimatedTotalPathCost { get; protected set; }
        public double PathCost { get; protected set; }

        protected AbstractNode(AbstractState state, AbstractState target, AbstractNode Parent, AbstractAction action)
        {
            this.State = state;
            this.Target = target;
            this.Parent = Parent;
            this.Action = action;
        }

        public int CompareTo(object obj)
        {
            var other = obj as AbstractNode;
            if (other == null) return 1;
            if (this.EstimatedTotalPathCost > other.EstimatedTotalPathCost)
                return 1;
            if (this.EstimatedTotalPathCost < other.EstimatedTotalPathCost)
                return -1;
            return 0;
        }
    }
}
