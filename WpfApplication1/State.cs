using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectAI.RouteFinding
{
    public class State
    {
        public int X { get; set; }
        public int Y { get; set; }

        public State(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

	public override string ToString()
	{
	    return this.X + "," + this.Y;
	}
    }
}
