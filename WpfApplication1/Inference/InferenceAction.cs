using Heureka.Common;

namespace Heureka.Inference
{
    public class InferenceAction : ActionAbstract
    {
        public InferenceAction(StateAbstract startState, StateAbstract endState)
            : base(startState, endState) {}

        public override double Cost
        {
            // Cost is uniform
            get { return 1; }
        }
    }
}
