using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Node
    {
        public State State;
        public Node Parent;
        public Action Action; //The action taken to get to the node
        public double PathCost;

        public Node(Node parent, Action action)
        {
            this.State = action.EndState;
            this.Parent = parent;
            this.Action = action;
            this.PathCost = parent.PathCost + action.Cost;// -parent.calculateHValue(problemGoal); MANGLER H(n)
        }

        public Node(State initialState) //Only for the initial node
        {
            this.State = initialState;
            this.Parent = null;
            this.Action = null;
            this.PathCost = 0.0;
        }

        public double calculateHValue(State problemGoal)
        {
            double hValue = 0.0;
            int x = this.State.X;
            int y = this.State.Y;
            hValue = Math.Sqrt(Math.Pow(x-problemGoal.X,2)+Math.Pow(y-problemGoal.Y,2));


            return hValue;
        }
    }
}
