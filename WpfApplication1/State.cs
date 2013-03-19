using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    class State
    {
        public int X { get; set; }
        public int Y { get; set; }

        public State(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
