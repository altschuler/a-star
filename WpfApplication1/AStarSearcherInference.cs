using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectAI.RouteFinding
{
    public static class AStarSearcherInference
    {
        public static SearchResultInference Search(StateInference start, StateInference end, KnowledgeBaseInference kb)
        {
            //Start_state er negeret "det man vil vise"
            //End_state er [], den tomme klausul

            var frontier = new PriorityQueue<NodeInference>();
            var explored = new List<StateInference>();
            var statesSearched = 0; //Bliver kun brugt af os af ren interesse

            NodeInference firstNode = new NodeInference(start, end);

            //Console.WriteLine(String.Join("  ", firstNode.State.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList()));

            frontier.Add(firstNode);

            while (frontier.Count > 0)
            {
                // Chooses the lowest-cost node in the frontier
                NodeInference currentNode = frontier.Pop();
                
                
                if (currentNode.State.Clause.Count == 0)
                {
                    NodeInference parent = currentNode.Parent;
                    return new SearchResultInference(statesSearched, true);
                }

                explored.Add(currentNode.State);

                List<StateInference> actions = kb.ActionsForNode(currentNode);
                
                List<NodeInference> children = new List<NodeInference>();
                //Apply resolution to get children
                foreach (StateInference action in actions)
                {
                    children.Add(kb.ApplyResolution(currentNode, action, explored));
                }

                foreach (NodeInference child in children)
                {

                    statesSearched++;
                    if (!explored.Contains(child.State))
                    {
                        explored.Add(child.State);
                        frontier.Add(child);
                    }
                }
            }

	    return new SearchResultInference(statesSearched, false);
        }
    }
}
