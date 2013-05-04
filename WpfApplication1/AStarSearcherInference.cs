﻿using System.Collections.Generic;

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

            frontier.Add(new NodeInference(start, end));

            while (frontier.Count > 0)
            {
                // Chooses the lowest-cost node in the frontier
                var currentNode = frontier.Pop();

                if (currentNode.State.Equals(end))
                {
                    return new SearchResult(currentNode, statesSearched, true);
                }

                explored.Add(currentNode.State);

                var actions = kb.ActionsForNode(currentNode);
                foreach (var action in actions)
                {
                    var child = kb.ApplyResolution(currentNode, action, explored);

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
