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

            frontier.Add(new NodeInference(start, end));

            while (frontier.Count > 0)
            {
                // Chooses the lowest-cost node in the frontier
                NodeInference currentNode = frontier.Pop();

                //if (!kb.Rules.Contains(currentNode.State))
                //{
                //    kb.Rules.Add(currentNode.State);
                //}
                //if(kb.Rules.Count % 100 == 0)
                //    Console.WriteLine("Rules.Count: " + kb.Rules.Count);
                //if (frontier.Count % 10 == 0 || true)
                //    Console.WriteLine("frontier.Count: " + frontier.Count);
                //Console.WriteLine(String.Join("  ", currentNode.State.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList()));
                if (currentNode.State.Clause.Count == 0)
                {
                    //Console.WriteLine(statesSearched);
		    //Console.WriteLine(String.Join("  ", currentNode.State.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList()));
                    NodeInference parent = currentNode.Parent;

                    while (parent != null)
                    {
    //                    Console.WriteLine(String.Join("  ", parent.State.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList()));
                        parent = parent.Parent;
                    }

                    //Console.WriteLine("Solution found");
                    return new SearchResultInference(statesSearched, true);
                }

                explored.Add(currentNode.State);

                
                //List<ActionRoutefinding> availableActions = currentNode.State.AvailableActions
                //Get available Actions to the State of the current Node
                List<StateInference> actions = kb.ActionsForNode(currentNode);
                
                List<NodeInference> children = new List<NodeInference>();
                //Apply resolution to get children
                foreach (StateInference action in actions)
                {
                    children.Add(kb.ApplyResolution(currentNode, action, explored));
                }
                //Console.WriteLine(children.Count);

///*.Where(a => a.StartState.Equals(currentNode.State))*/
                foreach (NodeInference child in children)
                {
                    //kb.Rules.Add(child.State);
                    //currentNode, action, en

                    statesSearched++;
                    if (!explored.Contains(child.State))
                    {
                        explored.Add(child.State);
                        frontier.Add(child);
                    }
                }
            }
//            Console.WriteLine(statesSearched);
//            Console.WriteLine("No solutions found");
	    return new SearchResultInference(statesSearched, false);
        }
    }
}
