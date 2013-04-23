using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAI.RouteFinding
{
    public abstract class NodeAbstract : IComparable
    {
        protected double _EstimatedTotalPathCost;
        protected double _PathCost;
        public StateAbstract State { get; set; }
        public NodeAbstract Parent { get; set; }
        public ActionAbstract Action { get; set; }

        public double EstimatedTotalPathCost { get; protected set; }
        public double PathCost { get; protected set; }

        public int CompareTo(object obj)
        {
            var other = obj as NodeRoutefinding;
            if (this.EstimatedTotalPathCost > other.EstimatedTotalPathCost)
                return 1;
            if (this.EstimatedTotalPathCost < other.EstimatedTotalPathCost)
                return -1;
            return 0;
        }
    }
}
