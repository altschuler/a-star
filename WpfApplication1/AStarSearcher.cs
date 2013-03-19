using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectAI.RouteFinding
{
    static class AStarSearcher
    {
        static List<Node> Frontier = new List<Node>(); //jf s. 84 bør Frontier have egenskaberne fra en priority queue + hash table
        static List<State> Explored = new List<State>();


        public static Node Search(List<State> states, List<Action> actions, State start, State end)
        {
            Node initialNode = new Node(start);
            Frontier.Add(initialNode);

            while (true)
            {
                if(Frontier.Count == 0)
                    return null;
                Node currentNode = PopFromFrontier(end); /* chooses the lowest-cost node in the frontier */
                if (end == currentNode.State) 
                    return currentNode;

                Explored.Add(currentNode.State);
                foreach (Action action in actions)  // Svarer til "Expand frontier"
                {
                    if (action.StartState != currentNode.State) //brug kun actions som hører til denne node's state!
                        continue;

                    Node child = new Node(currentNode, action);
                    if (!Frontier.Contains(child) && !Explored.Contains(child.State))
                        Frontier.Add(child);

                    for(int i = 0; i < Frontier.Count; i++){ //Ligegyldig? A* finder den korteste i forvejen.
                        Node frontierNode = Frontier[i];
                        if(frontierNode.State.Equals(child.State) && frontierNode.PathCost > child.PathCost){
                            Frontier[i] = child;
                            Console.WriteLine("Dette burde ikke nås i A*, da den allerede har tjekket de korteste paths");
                            break;
                        }
                    }
                }
            }
        }

        private static Node PopFromFrontier(State goal)
        {
            Node nextNode = null;
            double lowestCost = double.MaxValue;
            foreach (Node node in Frontier)
            {   
                if (node.TotalCostForGoal(goal) < lowestCost)
                {
                    nextNode = node;
                    lowestCost = node.TotalCostForGoal(goal);
                }
            }
            Frontier.Remove(nextNode);
            return nextNode;
        }
    }
}
