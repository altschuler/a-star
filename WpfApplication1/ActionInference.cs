namespace Heureka
{
    public class ActionInference : ActionAbstract
    {
        public ActionInference(StateAbstract startState, StateAbstract endState)
            : base(startState, endState)
        {
        }

        public override double Cost
        {
            // Cost is uniform
            get { return 1; }
        }
    }
}
