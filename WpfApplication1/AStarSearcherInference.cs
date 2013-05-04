using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectAI.RouteFinding
{
    public static class AStarSearcherInference
    {
        public static SearchResult Search(StateInference start, StateInference end, KnowledgeBaseInference kb)
        {
            //Start_state er negeret "det man vil vise"
            //End_state er [], den tomme klausul

            var frontier = new PriorityQueue<NodeInference>();
            var explored = new List<StateInference>();
            var statesSearched = 0; //Bliver kun brugt af os af ren interesse

            var firstNode = new NodeInference(start, end);

            //Console.WriteLine(String.Join("  ", firstNode.State.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList()));

            frontier.Add(firstNode);

            while (frontier.Count > 0)
            {
                // Chooses the lowest-cost node in the frontier
                var currentNode = frontier.Pop();

                if (currentNode.State.Clause.Count == 0)
                {
                    return new SearchResult(currentNode, statesSearched, true);
                }

                explored.Add(currentNode.State);

                var actions = kb.ActionsForNode(currentNode);
                var children = new List<NodeInference>();
                //Apply resolution to children
                foreach (var action in actions)
                {
                    children.Add(kb.ApplyResolution(currentNode, action, explored));
                }

                foreach (var child in children)
                {
                    statesSearched++;
                    if (!explored.Contains(child.State))
                    {
                        explored.Add(child.State);
                        frontier.Add(child);
                    }
                }
            }

            return new SearchResult(null, statesSearched, false);
        }
    }
}
