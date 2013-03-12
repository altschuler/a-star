using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
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
                Node currentNode = popFromFrontier(); /* chooses the lowest-cost node in the frontier */
                if (end == currentNode.State) 
                    return currentNode;

                Explored.Add(currentNode.State);
                foreach (Action action in actions)
                {
                    if (action.StartState != currentNode.State) //brug kun actions som hører til denne node's state!
                        continue;

                    Node child = new Node(currentNode, action);
                    if (!Frontier.Contains(child) && !Explored.Contains(child.State))
                    { Frontier.Add(child); Console.WriteLine("hverken frontier eller explored indeholder child"); }
                    for(int i = 0; i < Frontier.Count; i++){
                        Node frontierNode = Frontier[i];
                        if(frontierNode.State.Equals(child.State) && frontierNode.PathCost > child.PathCost){
                            Frontier[i] = child;
                            break;
                        }
                    }
                }
            }
        }

        private static Node popFromFrontier()
        {
            Node nextNode = null;
            double lowestCost = double.MaxValue;
            foreach (Node node in Frontier)
            {
                if (node.PathCost < lowestCost)
                {
                    nextNode = node;
                }
            }
            return nextNode;
        }
    }
}
