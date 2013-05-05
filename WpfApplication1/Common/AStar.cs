using System.Collections.Generic;

namespace Heureka.Common
{
    static class AStar
    {
        public static SearchResult Search(NodeAbstract initialNode, IKnowledgeBase kb)
        {
            var frontier = new PriorityQueue<NodeAbstract>();
            var explored = new List<StateAbstract>();
            var statesSearched = 0; //Bliver kun brugt af os af ren interesse
            var end = initialNode.Target;
            frontier.Add(initialNode);
            explored.Add(initialNode.State);
//<<<<<<< HEAD
//=======
            
//>>>>>>> cb9a1aaf7405db5cbc1becbefa4a8208eda35635
            while (frontier.Count > 0)
            {
                // Chooses the lowest-cost node in the frontier
                var currentNode = frontier.Pop();

                if (currentNode.State.Equals(end))
                    return new SearchResult(currentNode, statesSearched, true);

//<<<<<<< HEAD
                
//=======
//>>>>>>> cb9a1aaf7405db5cbc1becbefa4a8208eda35635
                //Get available actions to the State of the current Node
                var actions = kb.ActionsForNode(currentNode);
//Explore /expand the current node
                foreach (var action in actions)
                {
                    var child = kb.Resolve(currentNode, action, end);
                    //System.Console.WriteLine("Frontier.Count: " + frontier.Count);
                    
                    if (!explored.Contains(child.State) && !frontier.Contains(child))
                    {
                        explored.Add(child.State);
                        frontier.Add(child);
                        statesSearched++;
                    }
                    else if(true)
                    {
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
            }

            return new SearchResult(null, statesSearched, false);
        }
    }
}
