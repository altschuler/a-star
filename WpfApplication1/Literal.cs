using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAI.RouteFinding
{
    public struct Literal
    {
        public bool Proposition;
        public string Name;

        public Literal(string name, bool negated)
        {
            this.Name = name;
            this.Proposition = negated;
        }

        public int CompareLiterals(Literal otherLit)
        {
            String thisTempName = this.Proposition ? "" : "_";
            thisTempName += this.Name;

            String otherTempName = otherLit.Proposition ? "" : "_";
            otherTempName += otherLit.Name;

            return thisTempName.CompareTo(otherTempName);
        }
    }

}