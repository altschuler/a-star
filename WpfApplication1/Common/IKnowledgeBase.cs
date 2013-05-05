using System.Collections.Generic;

namespace Heureka.Common
{
    public interface IKnowledgeBase
    {
        IEnumerable<ActionAbstract> ActionsForNode(NodeAbstract node);

        NodeAbstract Resolve(NodeAbstract node, ActionAbstract action, StateAbstract targetState);
    }
}