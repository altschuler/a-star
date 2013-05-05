namespace Heureka.Common
{
    abstract public class AbstractAction
    {
        public AbstractState StartState { get; set; }
        public AbstractState EndState { get; set; }

        abstract public double Cost { get; }

        protected AbstractAction(AbstractState startState, AbstractState endState)
        {
            this.StartState = startState;
            this.EndState = endState;
        }
    }
}
