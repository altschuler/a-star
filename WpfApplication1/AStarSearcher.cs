using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectAI.RouteFinding
{
    static class AStarSearcher
    {
      public static Node Search(List<State> states, List<Action> actions, State start, State end)
      {	
	PriorityQueue<Node> frontier = new PriorityQueue<Node>(); 
	List<State> explored = new List<State>();

	frontier.Add(new Node(start, end));

	while (frontier.Count > 0)
	{
	  // Chooses the lowest-cost node in the frontier
	  Node currentNode = frontier.Pop(); 
	  if (currentNode.State.Equals(end))
	    return currentNode;

	  explored.Add(currentNode.State);
	  // Filter actions to the ones connected to the current node
	  foreach (Action action in actions.Where(a => a.StartState.Equals(currentNode.State)))
	  {
	      var child = new Node(currentNode, action, end);
	      Console.WriteLine(child.State);
	      if (!explored.Contains(child.State))// && !frontier.Any(n => n.State.Equals(child.State)))
	      {
		  explored.Add(child.State);
		  frontier.Add(child);
	      }

	    // Ligegyldig? A* finder den korteste i forvejen.
	    //for(int i = 0; i < frontier.Count; i++)
	    //{ 
	    //  var frontierNode = frontier[i];
	    //  if(frontierNode.State.Equals(child.State) && frontierNode.PathCost > child.PathCost)
	    //  {
	    //	frontier[i] = child;
	    //	Console.WriteLine("Dette burde ikke nås i A*");
	    //	break;
	    //  }
	    //}
	  }
	}

	return null;
      }
    }
}
