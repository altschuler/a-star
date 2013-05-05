using System;

namespace Heureka.Common
{
    public abstract class NodeAbstract : IComparable
    {
        public StateAbstract State { get; set; }
        public StateAbstract Target { get; set; }
        public NodeAbstract Parent { get; set; }
        public ActionAbstract Action { get; set; }

        public double EstimatedTotalPathCost { get; protected set; }
        public double PathCost { get; protected set; }

        protected NodeAbstract(StateAbstract state, StateAbstract target, NodeAbstract Parent, ActionAbstract action)
        {
            this.State = state;
            this.Target = target;
            this.Parent = Parent;
            this.Action = action;
        }

        public override int CompareTo(object obj)
        {
            var other = obj as NodeAbstract;
            if (other == null) return 1;
            if (this.EstimatedTotalPathCost > other.EstimatedTotalPathCost)
                return 1;
            if (this.EstimatedTotalPathCost < other.EstimatedTotalPathCost)
                return -1;
            return 0;
        }
    }
}
