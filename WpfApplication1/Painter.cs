using System;
using System.Drawing;

namespace ProjectAI.RouteFinding
{
  public static class Painter
  {
    public static void DrawKnowledgeBase(Graphics gfx, Node traceNode, KnowledgeBase kb, float scale)
    {
	// Draw map of routes
	var pen = new Pen(Color.Black) { Width = 5 };
	foreach (var action in kb.Actions)
	  gfx.DrawLine(pen, action.StartState.X * scale, action.StartState.Y * scale, 
		       action.EndState.X * scale, action.EndState.Y * scale);

	// Draw calculated route
	pen = new Pen(Color.FromArgb(255, 0, 255, 0)) { Width = 3 };
	var last = kb.End;
	while (traceNode != null)
	{
	  gfx.DrawLine(pen, last.X * scale, last.Y * scale, 
		       traceNode.State.X * scale, traceNode.State.Y * scale);
	  last = traceNode.State;
	  traceNode = traceNode.Parent;
	}

	// Draw end marker
	pen = new Pen(Color.FromArgb(255, 0, 0, 255)) { Width = 3 };
	var esize = 12;
	gfx.DrawEllipse(pen, 
			(int)(kb.End.X * scale - esize * .5), 
			(int)(kb.End.Y * scale - esize * .5), 
			esize, esize);
    }
  }
}