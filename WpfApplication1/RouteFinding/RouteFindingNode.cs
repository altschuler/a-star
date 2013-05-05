using System;
using Heureka.Common;

namespace Heureka.RouteFinding
{
    public class RouteFindingNode : NodeAbstract
    {
        public RouteFindingNode(RouteFindingState state, RouteFindingState target) : this(null, null, state, target) { }

        public RouteFindingNode(NodeAbstract parent, ActionAbstract action, RouteFindingState state, RouteFindingState target)
            : base(state, target, parent, action)
        {
            if (this.Parent != null && this.Action != null)
                this.PathCost = this.Parent.PathCost + action.Cost;

            this.EstimatedTotalPathCost = this.PathCost + Math.Sqrt(Math.Pow(state.X - target.X, 2) + Math.Pow(state.Y - target.Y, 2));
        }


        //public override int CompareTo(object obj)
        //{
        //    var other = obj as RouteFindingNode;
        //    if (other == null) return 1;
        //    if (this.EstimatedTotalPathCost > other.EstimatedTotalPathCost)
        //        return 1;
        //    if (this.EstimatedTotalPathCost < other.EstimatedTotalPathCost)
        //        return -1;
        //    return 0;
        //}

        new public RouteFindingState State
        {
            get { return base.State as RouteFindingState; }
        }

        new public RouteFindingNode Parent
        {
            get { return base.Parent as RouteFindingNode; }
            set { base.Parent = value; }
        }
    }
}
