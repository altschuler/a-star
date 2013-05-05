using System.Collections.Generic;
using System.Linq;
using Heureka.Common;

namespace Heureka.Inference
{
    public class InferenceState : StateAbstract
    {
        public List<Literal> Clause { get; set; }

        public InferenceState() : this(new List<Literal>()) { }

        public InferenceState(List<Literal> clause)
        {
            this.Clause = clause;
        }

        public override bool Equals(object obj)
        {
            var other = obj as InferenceState;

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
