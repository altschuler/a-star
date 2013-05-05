using Heureka.Common;

namespace Heureka.Inference
{
    public class InferenceNode : AbstractNode
    {
        public InferenceNode(AbstractState startState, AbstractState targetState)
            : base(startState, targetState, null, null)
        {
            this.PathCost = 0;
            this.EstimatedTotalPathCost = this.PathCost + this.State.Clause.Count;
        }

        public InferenceNode(AbstractNode parent, AbstractState target, AbstractState state, AbstractAction action)
            : base(state, target, parent, action)
        {
            this.PathCost = this.Parent.PathCost + 1;
            this.EstimatedTotalPathCost = this.State.Clause.Count;
        }

        new public InferenceState State
        {
            get { return base.State as InferenceState; }
            set { base.State = value; }
        }

        new public InferenceNode Parent
        {
            get { return base.Parent as InferenceNode; }
            set { base.Parent = value; }
        }

        new public InferenceState Target
        {
            get { return base.Target as InferenceState; }
            set { base.Target = value; }
        }
    }
}
