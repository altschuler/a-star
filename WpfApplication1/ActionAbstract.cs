namespace ProjectAI.RouteFinding
{
    abstract public class ActionAbstract
    {
        public StateAbstract StartState { get; set; }
        public StateAbstract EndState { get; set; }

        protected double? _Cost;

        abstract public double Cost { get; }

        protected ActionAbstract(StateAbstract startState, StateAbstract endState)
        {
            this.StartState = startState;
            this.EndState = endState;
        }
    }
}
