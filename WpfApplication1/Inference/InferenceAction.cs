using Heureka.Common;

namespace Heureka.Inference
{
    public class InferenceAction : AbstractAction
    {
        public InferenceAction(AbstractState startState, AbstractState endState)
            : base(startState, endState) {}

        // Cost is uniform in inference searching
        public override double Cost
        {
            get { return 1; }
        }
    }
}
