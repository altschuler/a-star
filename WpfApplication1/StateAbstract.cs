using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAI.RouteFinding
{
    public abstract class StateAbstract
    {
        public List<ActionRoutefinding> AvailableActions { get; set; }
    }
}
