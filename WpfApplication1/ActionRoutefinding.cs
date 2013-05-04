using System;

namespace Heureka
{
    public class ActionRoutefinding : ActionAbstract
    {
        public String Name { get; set; }

        public ActionRoutefinding(String name, StateAbstract startState, StateAbstract endState)
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

        new public StateRoutefinding StartState
        {
            get { return base.StartState as StateRoutefinding; }
        }

        new public StateRoutefinding EndState
        {
            get { return base.EndState as StateRoutefinding; }
        }
    }
}
