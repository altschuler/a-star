using System.Collections.Generic;

namespace ProjectAI.RouteFinding
{
    public abstract class StateAbstract
    {
        public List<ActionAbstract> AvailableActions { get; set; }
    }
}
