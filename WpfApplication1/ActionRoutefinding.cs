﻿using System;

namespace ProjectAI.RouteFinding
{
    public class ActionRoutefinding : ActionAbstract
    {
        public String Name { get; set; }

        public ActionRoutefinding(String name, StateRoutefinding startState, StateRoutefinding endState)
            : base(startState, endState)
        {
            this.Name = name;
        }

        public override double Cost
        {
            get
            {
                if (this._Cost == null)
                    this._Cost = Math.Sqrt(Math.Pow(this.EndState.X - this.StartState.X, 2) + Math.Pow(this.EndState.Y - this.StartState.Y, 2));

                return this._Cost.Value;
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
