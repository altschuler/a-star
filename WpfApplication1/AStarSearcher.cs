using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectAI.RouteFinding
{
    static class AStarSearcher
    {
        public static SearchResult Search(StateRoutefinding start, StateRoutefinding end, bool inferenceEngine)
        {
            //Start_state er negeret "det man vil vise"
            //End_state er [], den tomme klausul
            var frontier = new PriorityQueue<NodeAbstract>();
            var explored = new List<StateAbstract>();
            var statesSearched = 0; //Bliver kun brugt af os af ren interesse

            //frontier.Add(new T());

            while (frontier.Count > 0)
            {
                // Chooses the lowest-cost node in the frontier
                NodeAbstract currentNode = frontier.Pop();
                if (currentNode.State.Equals(end))
                {
                    return new SearchResult(currentNode, statesSearched);
                }

                explored.Add(currentNode.State);

                //Get available Sctions to the State of the current Node
                List<ActionRoutefinding> availableActions = currentNode.State.AvailableActions;
                foreach (ActionRoutefinding action in availableActions/*.Where(a => a.StartState.Equals(currentNode.State))*/)
                {

                    //var child = new T() { Action = action, Parent = currentNode, };
                    var child = new NodeRoutefinding(null,null);
                    //currentNode, action, en

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
                // Filter actions to the ones connected to the state of the current node
                /*	    foreach (ActionRoutefinding action in actions.Where(a => a.StartState.Equals(currentNode.State)))
                        {

                            var child = new Node(currentNode, action, end);
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
                 */
            }

            return null;
        }
    }
}
