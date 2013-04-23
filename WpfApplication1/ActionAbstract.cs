using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAI.RouteFinding
{
    abstract public class ActionAbstract
    {
        public StateRoutefinding StartState {  get; set; }
        public StateRoutefinding EndState { get; set; }

        protected double? _Cost;

        abstract public double Cost{ get; }

        public ActionAbstract(StateRoutefinding startState, StateRoutefinding endState)
        {
            this.StartState = startState;
            this.EndState = endState;
        }
    }
}
