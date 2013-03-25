using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectAI.RouteFinding
{
  public class KnowledgeBase
  {
    public State Start { get; set; }
    public State End { get; set; }
    public List<Action> Actions { get; set; }
    public List<State> States { get; set; }

    public static KnowledgeBase Parse(string data)
    {
      var kb = new KnowledgeBase();
      kb.Actions = new List<Action>();
      kb.States = new List<State>();

      var split = data.Split('\n');
      split = split.Select(s => s.Trim()).ToArray();
      foreach(var entry in split)
      {
	// skip blank lines
	if (String.IsNullOrEmpty(entry)) continue;

	var entrySplit = entry.Split(' ');
	entrySplit = entrySplit.Select(s => s.Trim()).ToArray();
	if (entrySplit[0].Equals("start"))
	{
	  var spx = int.Parse(entrySplit[1]);
	  var spy = int.Parse(entrySplit[2]); 
	  kb.Start = kb.GetOrCreateState(spx, spy);
	  continue;
	}
	
	if (entrySplit[0].Equals("end"))
	{
	  var epx = int.Parse(entrySplit[1]);
	  var epy = int.Parse(entrySplit[2]); 
	  kb.End = kb.GetOrCreateState(epx, epy);
	  continue;
	}

	var sx = int.Parse(entrySplit[0]);
	var sy = int.Parse(entrySplit[1]);
	var ex = int.Parse(entrySplit[3]);
	var ey = int.Parse(entrySplit[4]);
	var actionName = entrySplit[2];
	
	var startState = kb.GetOrCreateState(sx, sy);
	var endState = kb.GetOrCreateState(ex, ey);
	
	var action = new Action(actionName, startState, endState);
	kb.Actions.Add(action);
      }

      return kb;
    }

    protected State GetOrCreateState(int px, int py)
    {
      	var state = this.States.SingleOrDefault(s => s.X == px && s.Y == py);
	if (state == null)
	{
	  state = new State(px, py);
	  this.States.Add(state);
	}
	return state;
    }
  }
}