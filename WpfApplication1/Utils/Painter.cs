using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using Heureka.RouteFinding;

namespace Heureka.Utils
{
    public static class Painter
    {
        public static void DrawKnowledgeBase(Graphics gfx, RouteFindingNode traceNode, RouteFindingKnowledgeBase kb, RouteFindingState startState, RouteFindingState endState)
        {
            var offset = 10;
            var scale = Math.Max(kb.MaxX, kb.MaxY);
            scale = 400 / scale;

            // Draw map of routes
            var pen = new Pen(Color.FromArgb(100, 1, 1, 0)) { Width = 5 };
            pen.EndCap = LineCap.ArrowAnchor;
            pen.StartCap = LineCap.RoundAnchor;
            foreach (var action in kb.Actions.Select(a => (RouteFindingAction)a))
                gfx.DrawLine(pen, action.StartState.X * scale + offset, action.StartState.Y * scale + offset,
                      action.EndState.X * scale + offset, action.EndState.Y * scale + offset);


            // Draw calculated route
            pen = new Pen(Color.FromArgb(255, 0, 255, 0)) { Width = 2.4f };
            var last = endState;
            while (traceNode != null)
            {
                gfx.DrawLine(pen, last.X * scale + offset, last.Y * scale + offset, traceNode.State.X * scale + offset, traceNode.State.Y * scale + offset);
                last = traceNode.State;
                traceNode = traceNode.Parent;
            }

            // Draw end marker
            pen = new Pen(Color.FromArgb(255, 255, 0, 0)) { Width = 3 };
            var esize = 12;
            gfx.DrawEllipse(pen,
                    (int)(endState.X * scale - esize * .5) + offset,
                    (int)(endState.Y * scale - esize * .5) + offset,
                    esize, esize);


            // Draw start marker
            pen = new Pen(Color.FromArgb(255, 0, 0, 255)) { Width = 3 };
            esize = 12;
            gfx.DrawEllipse(pen,
                    (int)(startState.X * scale - esize * .5) + offset,
                    (int)(startState.Y * scale - esize * .5) + offset,
                    esize, esize);
        }
    }
}