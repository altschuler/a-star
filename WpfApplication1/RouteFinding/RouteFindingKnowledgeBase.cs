using System;
using System.Collections.Generic;
using System.Linq;
using Heureka.Common;

namespace Heureka.RouteFinding
{
    public class RouteFindingKnowledgeBase : IKnowledgeBase
    {
        public List<AbstractAction> Actions { get; set; }
        public List<AbstractState> States { get; set; }

        public static RouteFindingKnowledgeBase Parse(string data)
        {
            var kb = new RouteFindingKnowledgeBase
                {
                    Actions = new List<AbstractAction>(),
                    States = new List<AbstractState>()
                };

            var split = data.Split('\n');
            split = split.Select(s => s.Trim()).ToArray();
            foreach (var entry in split)
            {
                // skip blank lines
                if (String.IsNullOrEmpty(entry)) continue;

                var entrySplit = entry.Split(' ');
                entrySplit = entrySplit.Select(s => s.Trim()).ToArray();

                var sx = int.Parse(entrySplit[0]);
                var sy = int.Parse(entrySplit[1]);
                var ex = int.Parse(entrySplit[3]);
                var ey = int.Parse(entrySplit[4]);
                var actionName = entrySplit[2];

                var startState = kb.GetOrCreateState(sx, sy);
                var endState = kb.GetOrCreateState(ex, ey);

                var action = new RouteFindingAction(actionName, startState, endState);
                kb.Actions.Add(action);
            }

            return kb;
        }

        public int MaxX { get { return this.States.Select(s => ((RouteFindingState)s).X).Max(); } }
        public int MaxY { get { return this.States.Select(s => ((RouteFindingState)s).Y).Max(); } }

        protected RouteFindingState GetOrCreateState(int px, int py)
        {
            var tmpState = new RouteFindingState(px, py);
            var state = this.States.SingleOrDefault(s => s.Equals(tmpState));
            if (state == null)
            {
                state = new RouteFindingState(px, py);
                this.States.Add(state);
            }
            return (RouteFindingState)state;
        }

        public IEnumerable<AbstractAction> ActionsForNode(AbstractNode node)
        {
            return this.Actions.Where(a => a.StartState.Equals(node.State)).ToList();
        }

        public AbstractNode Resolve(AbstractNode parent, AbstractAction action, AbstractState targetState)
        {
            return new RouteFindingNode(parent, action, action.EndState as RouteFindingState, targetState as RouteFindingState);
        }
    }
}