using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectAI.RouteFinding
{
    static class AStarSearcher
    {
	public static SearchResult Search(List<State> states, List<Action> actions, State start, State end)
	{	
	    var frontier = new PriorityQueue<Node>(); 
	    var explored = new List<State>();
	    var statesSearched = 0;

	    frontier.Add(new Node(start, end));

	    while (frontier.Count > 0)
	    {
		// Chooses the lowest-cost node in the frontier
		Node currentNode = frontier.Pop();
		if (currentNode.State.Equals(end))
		{
		    return new SearchResult(currentNode, statesSearched);
		}

		explored.Add(currentNode.State);
		// Filter actions to the ones connected to the current node
		foreach (Action action in actions.Where(a => a.StartState.Equals(currentNode.State)))
		{
		    var child = new Node(currentNode, action, end);
		    statesSearched++;
		    if (!explored.Contains(child.State))
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
