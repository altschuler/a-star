using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectAI.RouteFinding
{
    public class KnowledgeBase : IKnowledgeBase
    {
        public List<ActionAbstract> Actions { get; set; }
        public List<StateAbstract> States { get; set; }

        public static KnowledgeBase Parse(string data)
        {
            var kb = new KnowledgeBase
                {
                    Actions = new List<ActionAbstract>(),
                    States = new List<StateAbstract>()
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

                var action = new ActionRoutefinding(actionName, startState, endState);
                kb.Actions.Add(action);
            }

            return kb;
        }

        public int MaxX { get { return this.States.Select(s => ((StateRoutefinding)s).X).Max(); } }
        public int MaxY { get { return this.States.Select(s => ((StateRoutefinding)s).Y).Max(); } }

        protected StateRoutefinding GetOrCreateState(int px, int py)
        {
            var tmpState = new StateRoutefinding(px, py);
            var state = this.States.SingleOrDefault(s => s.Equals(tmpState));
            if (state == null)
            {
                state = new StateRoutefinding(px, py);
                this.States.Add(state);
            }
            return (StateRoutefinding)state;
        }

        public IEnumerable<ActionAbstract> ActionsForNode(NodeAbstract node)
        {
            return this.Actions.Where(a => a.StartState.Equals(node.State)).ToList();
        }

        public NodeAbstract Resolve(NodeAbstract node, ActionAbstract action, StateAbstract targetState, IEnumerable<StateAbstract> explored)
        {
            return new NodeRoutefinding(node as NodeRoutefinding, action as ActionRoutefinding, action.EndState as StateRoutefinding, targetState as StateRoutefinding);
        }
    }

    public interface IKnowledgeBase
    {
        IEnumerable<ActionAbstract> ActionsForNode(NodeAbstract node);

        NodeAbstract Resolve(NodeAbstract node, ActionAbstract action, StateAbstract targetState, IEnumerable<StateAbstract> explored);
    }
}