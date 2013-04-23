using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAI.RouteFinding
{
    class ActionInference : ActionAbstract
    {

        public override double Cost
        {
            get
            {
                if (this._Cost == null)
                    this._Cost = 133712345;

                return this._Cost.Value;
            }
        }

        public ActionInference(StateRoutefinding startState, StateRoutefinding endState) : base(startState, endState) { }
    }
}
