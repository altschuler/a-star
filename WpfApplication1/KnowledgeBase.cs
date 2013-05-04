using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectAI.RouteFinding
{
    public class KnowledgeBase
    {
        public List<ActionRoutefinding> Actions { get; set; }
        public List<StateRoutefinding> States { get; set; }

        public static KnowledgeBase Parse(string data)
        {
            var kb = new KnowledgeBase
                {
                    Actions = new List<ActionRoutefinding>(),
                    States = new List<StateRoutefinding>()
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

        public int MaxX { get { return this.States.Select(s => s.X).Max(); } }
        public int MaxY { get { return this.States.Select(s => s.Y).Max(); } }

        protected StateRoutefinding GetOrCreateState(int px, int py)
        {
            var state = this.States.SingleOrDefault(s => s.X == px && s.Y == py);
            if (state == null)
            {
                state = new StateRoutefinding(px, py);
                this.States.Add(state);
            }
            return state;
        }

        public List<ActionRoutefinding> ActionsForNode(NodeAbstract node)
        {
            return this.Actions.Where(a => a.StartState.Equals(node.State)).ToList();
        }
    }
}