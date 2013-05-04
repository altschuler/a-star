using System.Collections.Generic;

namespace Heureka
{
    static class AStarSearcher
    {
        public static SearchResult Search(StateAbstract end, NodeAbstract initialNode, IKnowledgeBase kb)
        {
            //Start_state er negeret "det man vil vise"
            //End_state er [], den tomme klausul
            var frontier = new PriorityQueue<NodeAbstract>();
            var explored = new List<StateAbstract>();
            var statesSearched = 0; //Bliver kun brugt af os af ren interesse

            frontier.Add(initialNode);
            
            while (frontier.Count > 0)
            {
                // Chooses the lowest-cost node in the frontier
                var currentNode = frontier.Pop();

                if (currentNode.State.Equals(end))
                {
                    return new SearchResult(currentNode, statesSearched, true);
                }

                explored.Add(currentNode.State);
                //Get available actions to the State of the current Node
                var actions = kb.ActionsForNode(currentNode);
                foreach (var action in actions)
                {
                    var child = kb.Resolve(currentNode, action, end, explored);

                    statesSearched++;
                    if (!explored.Contains(child.State))
                    {
                        explored.Add(child.State);
                        frontier.Add(child);
                    }

                    //Giver mening hvis man ser eksemplet i bogen s. 84. Måske den kun skal køres én gang når "end" er nået?
                    for (int i = 0; i < frontier.Count; i++)
                    {
                        var frontierNode = frontier[i];
                        if (frontierNode.State.Equals(child.State) && frontierNode.PathCost > child.PathCost)
                        {
                            frontier[i] = child;
                            break;
                        }
                    }
                }
            }

            return new SearchResult(null, statesSearched, false);
        }
    }
}
