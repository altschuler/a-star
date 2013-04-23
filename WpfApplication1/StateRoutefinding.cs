using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectAI.RouteFinding
{
    public class StateRoutefinding : StateAbstract
    {
        public int X { get; set; }
        public int Y { get; set; }

        public StateRoutefinding(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.AvailableActions = new List<ActionRoutefinding>();
        }

	    public override string ToString()
	    {
	        return this.X + "," + this.Y;
	    }
    }
}
