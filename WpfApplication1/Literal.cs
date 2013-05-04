using System;

namespace ProjectAI.RouteFinding
{
    public struct Literal
    {
        public readonly bool Proposition;
        public readonly string Name;

        public Literal(string name, bool negated)
        {
            this.Name = name;
            this.Proposition = negated;
        }

        public override bool Equals(Object other)
        {
            return (this.Name.Equals(((Literal)other).Name) && this.Proposition.Equals(((Literal)other).Proposition));
        }

        public override int GetHashCode()
        {
            // From StackOverflow
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + this.Name.GetHashCode();
                hash = hash * 23 + this.Proposition.GetHashCode();
                return hash;
            }
        }
    }

}