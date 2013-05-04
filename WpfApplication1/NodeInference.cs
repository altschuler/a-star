using System;

namespace ProjectAI.RouteFinding
{
    public class NodeInference : NodeAbstract
    {
        public double PathCost { get; protected set; }

        public NodeInference(StateInference startState, StateInference targetState) :base(startState, targetState)
        {
            this.State = startState;
            this.Target = targetState;
            this.PathCost = 0;
            this.Parent = null;
        }

        public NodeInference(NodeInference parent, StateInference target, StateInference state, ActionInference action) : base(state, target)
        {
            this.Parent = parent;
            this.State = state;
            this.Target = target;
            this.Action = action;
            this.PathCost = this.Parent.PathCost + 1;
        }

        public override int CompareTo(object obj)
        {
            var other = obj as NodeInference;

            if (this.State.Clause.Count > other.State.Clause.Count)
                return 1;
            if (this.State.Clause.Count < other.State.Clause.Count)
                return -1;

            return 0;
        }

        new public StateInference State
        {
            get { return base.State as StateInference; }
            set { base.State = value; }
        }

        new public NodeInference Parent
        {
            get { return base.Parent as NodeInference; }
            set { base.Parent = value; }
        }

        new public ActionInference Action
        {
            get { return base.Action as ActionInference; }
            set { base.Action = value; }
        }

        new public StateInference Target
        {
            get { return base.Target as StateInference; }
            set { base.Target = value; }
        }
    }
}
