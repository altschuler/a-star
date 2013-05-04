using System;

namespace ProjectAI.RouteFinding
{
    public class ActionInference : ActionAbstract
    {
        public ActionInference(StateInference startState, StateInference endState) : base(startState, endState)
        {
        }

        public override double Cost
        {
            // Cost is uniform
            get { return 1; }
        }

        //public ActionInference(StateRoutefinding startState, StateRoutefinding endState) : base(startState, endState) { }
    }
}
