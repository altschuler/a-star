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
            
            while (frontier.Count > 0)
            {
                // Chooses the lowest-cost node in the frontier
                var currentNode = frontier.Pop();

                if (currentNode.State.Equals(end))
                    return new SearchResult(currentNode, statesSearched, true);

                explored.Add(currentNode.State);
                //Get available actions to the State of the current Node
                var actions = kb.ActionsForNode(currentNode);
//Explore /expand the current node
                foreach (var action in actions)
                {
                    var child = kb.Resolve(currentNode, action, end, explored);

                    
                    if (!explored.Contains(child.State))
                    {
                        explored.Add(child.State);
                        frontier.Add(child);
                        statesSearched++;
                        
                    }
                    else if(false)
                    {
                        //Giver mening hvis man ser eksemplet i bogen s. 84. Måske den kun skal køres én gang når "end" er nået?
                        for (int i = 0; i < frontier.Count; i++)
                        {
                            var frontierNode = frontier[i];
                            if (frontierNode.State.Equals(child.State) && frontierNode.PathCost > child.PathCost)
                            {
                                //System.Console.WriteLine("fronter[" + i + "].PathCost: " + frontier[i].PathCost + ", child.PathCost: " + child.PathCost);
                                //System.Console.WriteLine("fronter[" + i + "].EstimatedTotalCost: " + frontier[i].EstimatedTotalPathCost + ", child.EstimatedTotalCost: " + child.EstimatedTotalPathCost);
                                //System.Console.WriteLine("number of nodes added to frontier: " + statesSearched);
                                //System.Console.WriteLine("X: " +((RouteFinding.RouteFindingNode)child).State.X);
                                //System.Console.WriteLine("Y: " + ((RouteFinding.RouteFindingNode)child).State.Y);
                                //System.Console.WriteLine("child.parentX: " + ((RouteFinding.RouteFindingNode)child.Parent).State.X);
                                //System.Console.WriteLine("child.parentY: " + ((RouteFinding.RouteFindingNode)child.Parent).State.Y);
                                //System.Console.WriteLine("frontierNode.parentX: " + ((RouteFinding.RouteFindingNode)frontierNode.Parent).State.X);
                                //System.Console.WriteLine("frontierNode.parentY: " + ((RouteFinding.RouteFindingNode)frontierNode.Parent).State.Y);
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
