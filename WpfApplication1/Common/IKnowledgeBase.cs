using System.Collections.Generic;

namespace Heureka.Common
{
    public interface IKnowledgeBase
    {
        IEnumerable<AbstractAction> ActionsForNode(AbstractNode node);

        AbstractNode Resolve(AbstractNode node, AbstractAction action, AbstractState targetState);
    }
}