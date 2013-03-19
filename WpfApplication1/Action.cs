using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectAI.RouteFinding
{
  public class Action
  {
    public String Name { get; set; }
    public State StartState { get; set; }
    public State EndState { get; set; }

    private double? _Cost;

    public Action(String name, State startState, State endState)
    {
      this.Name = name;
      this.StartState = startState;
      this.EndState = endState;
    }

    public double Cost
    {
      get
      {
	if(this._Cost == null)
	  this._Cost = Math.Sqrt(Math.Pow(this.EndState.X - this.StartState.X, 2) + Math.Pow(this.EndState.Y - this.StartState.Y, 2));

	return this._Cost.Value;
      }
    }
  }
}
