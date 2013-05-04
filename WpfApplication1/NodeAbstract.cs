using System;
using System.Collections.Generic;

namespace ProjectAI.RouteFinding
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

        public abstract int CompareTo(object obj);
    }
}
