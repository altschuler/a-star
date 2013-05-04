using System;
using Heureka.Common;

namespace Heureka.RouteFinding
{
    public class RouteFindingAction : ActionAbstract
    {
        public String Name { get; set; }

        public RouteFindingAction(String name, StateAbstract startState, StateAbstract endState)
            : base(startState, endState)
        {
            this.Name = name;
        }

        public override double Cost
        {
            get
            {
                return Math.Sqrt(Math.Pow(this.EndState.X - this.StartState.X, 2) + Math.Pow(this.EndState.Y - this.StartState.Y, 2));
            }
        }

        new public RouteFindingState StartState
        {
            get { return base.StartState as RouteFindingState; }
        }

        new public RouteFindingState EndState
        {
            get { return base.EndState as RouteFindingState; }
        }
    }
}
