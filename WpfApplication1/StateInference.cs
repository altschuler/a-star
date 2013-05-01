using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAI.RouteFinding
{
    public class StateInference
    {
        public List<Literal> Clause { get; set; }

        public StateInference()
        {
            this.Clause = new List<Literal>();
        }

        public override bool Equals(object obj)
        {
            var other = obj as StateInference;

            if (other.Clause.Count != this.Clause.Count)
                return false;

            return this.Clause.All(l => other.Clause.Contains(l));
        }

        public StateInference(List<Literal> clause)
        {
            this.Clause = clause;
        }
    }
}
