using Heureka.Common;

namespace Heureka.Inference
{
    public class InferenceAction : ActionAbstract
    {
        public InferenceAction(StateAbstract startState, StateAbstract endState)
            : base(startState, endState) {}

        // Cost is uniform in inference searching
        public override double Cost
        {
            get { return 1; }
        }
    }
}
