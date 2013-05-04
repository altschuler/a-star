using System;

namespace ProjectAI.RouteFinding
{
    public class NodeRoutefinding : NodeAbstract
    {
        public NodeRoutefinding(StateRoutefinding state, StateRoutefinding target) : this(null, null, state, target) { }

        public NodeRoutefinding(NodeRoutefinding parent, ActionRoutefinding action, StateRoutefinding state, StateRoutefinding target)
            : base(state, target, parent, action)
        {
            if (this.Parent != null && this.Action != null)
                this.PathCost = this.Parent.PathCost + action.Cost;

            this.EstimatedTotalPathCost = this.PathCost + Math.Sqrt(Math.Pow(state.X - target.X, 2) + Math.Pow(state.Y - target.Y, 2));
        }


        public override int CompareTo(object obj)
        {
            var other = obj as NodeRoutefinding;
            if (other == null) return 1;
            if (this.EstimatedTotalPathCost > other.EstimatedTotalPathCost)
                return 1;
            if (this.EstimatedTotalPathCost < other.EstimatedTotalPathCost)
                return -1;
            return 0;
        }

        new public StateRoutefinding State
        {
            get { return base.State as StateRoutefinding; }
        }

        new public NodeRoutefinding Parent
        {
            get { return base.Parent as NodeRoutefinding; }
            set { base.Parent = value; }
        }

    }
}
