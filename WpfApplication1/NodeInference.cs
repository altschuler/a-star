using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAI.RouteFinding
{
    public class NodeInference : IComparable
    {
        public StateInference State { get; set; }
        public StateInference Target { get; set; }
        public NodeInference Parent { get; set; }
        public StateInference Action { get; set; }
        public double PathCost { get; protected set; }

        public NodeInference(StateInference startState, StateInference target)//initializing constructor
        {
            this.State = startState;
            this.Target = target;
            this.PathCost = 0;
            this.Parent = null;
        }

        public NodeInference(NodeInference parent, StateInference end, StateInference state, StateInference appliedRule)
        {
            this.Parent = parent;
            this.State = state;
            this.Target = end;
            this.Action = appliedRule;
            this.PathCost = this.Parent.PathCost + 1;
        }



        public int CompareTo(object obj)
        {
            var other = obj as NodeInference;
            //if (this.EstimatedTotalPathCost > other.EstimatedTotalPathCost)
            //    return 1;
            //if (this.EstimatedTotalPathCost < other.EstimatedTotalPathCost)
            //    return -1;

            if (this.State.Clause.Count > other.State.Clause.Count)
                return 1;
            if (this.State.Clause.Count < other.State.Clause.Count)
                return -1;

            return 0;
        }



    }
}
