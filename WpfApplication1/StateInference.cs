using System.Collections.Generic;
using System.Linq;

namespace ProjectAI.RouteFinding
{
    public class StateInference : StateAbstract
    {
        // TODO make use of AvailableAction
        public List<Literal> Clause { get; set; }

        public StateInference() : this(new List<Literal>()) { }

        public StateInference(List<Literal> clause)
        {
            this.Clause = clause;
        }

        public override bool Equals(object obj)
        {
            var other = obj as StateInference;

            if (other.Clause.Count != this.Clause.Count)
                return false;

            return this.Clause.All(l => other.Clause.Contains(l));
        }

        public override int GetHashCode()
        {
            return this.Clause.GetHashCode();
        }
    }
}
