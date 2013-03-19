using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectAI.RouteFinding
{
  public class Node : IComparable
  {
    public double EstimatedTotalPathCost { get; set; }
    public double PathCost { get; set; }
    public State State { get; set; }
    public Node Parent { get; set; }
    public Action Action { get; set; }

    public Node(Node parent, Action action, State state, State target)
    {
      this.State = state;
      this.Parent = parent;
      this.Action = action;
      if (this.Parent != null && this.Action != null)
	this.PathCost = this.Parent.PathCost + action.Cost;

      this.EstimatedTotalPathCost = this.PathCost + Math.Sqrt(Math.Pow(this.State.X - target.X,2) + Math.Pow(this.State.Y - target.Y,2)); 
    }

    public Node(Node parent, Action action, State target) : this(parent, action, action.EndState, target) {}

    public Node(State state, State target) : this(null, null, state, target) {}

    public int CompareTo(object obj)
    {
      var other = obj as Node;
      if (this.EstimatedTotalPathCost > other.EstimatedTotalPathCost)
	return 1;
      if (this.EstimatedTotalPathCost < other.EstimatedTotalPathCost)
	return -1;
      return 0;
    }
  }
}
