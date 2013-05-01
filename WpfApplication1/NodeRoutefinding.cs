using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectAI.RouteFinding
{
    public class NodeRoutefinding : NodeAbstract
    {
        public NodeRoutefinding(NodeRoutefinding parent, ActionRoutefinding action, StateRoutefinding state, StateRoutefinding target)
            : base(state, target)
        {
            this.State = state;
            this.Parent = parent;
            this.Action = action;
            if (this.Parent != null && this.Action != null)
                this.PathCost = this.Parent.PathCost + action.Cost;

            this.EstimatedTotalPathCost = this.PathCost + Math.Sqrt(Math.Pow(this.State.X - target.X, 2) + Math.Pow(this.State.Y - target.Y, 2));
        }

        public NodeRoutefinding(StateRoutefinding state, StateRoutefinding target) : this(null, null, state, target) { }    

    }
}
