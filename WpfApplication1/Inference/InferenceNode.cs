using Heureka.Common;

namespace Heureka.Inference
{
    public class InferenceNode : NodeAbstract
    {
        public InferenceNode(StateAbstract startState, StateAbstract targetState)
            : base(startState, targetState, null, null)
        {
            this.PathCost = 0;
            this.EstimatedTotalPathCost = this.PathCost + this.State.Clause.Count;
        }

        public InferenceNode(NodeAbstract parent, StateAbstract target, StateAbstract state, ActionAbstract action)
            : base(state, target, parent, action)
        {
            this.PathCost = this.Parent.PathCost + 1;
            this.EstimatedTotalPathCost = this.PathCost + this.State.Clause.Count;
        }

        //public override int CompareTo(object obj)
        //{
        //    var other = obj as InferenceNode;
        //    if (other == null) return 1;
        //    if (this.State.Clause.Count > other.State.Clause.Count)
        //        return 1;
        //    if (this.State.Clause.Count < other.State.Clause.Count)
        //        return -1;

        //    return 0;
        //}

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
