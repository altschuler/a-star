using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAI.RouteFinding
{
    public struct Literal
    {
        public bool Negated;
        public string Name;

        public Literal(string name, bool negated)
        {
            this.Name = name;
            this.Negated = negated;
        }
    }

}