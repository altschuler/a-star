using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Heureka.RouteFinding;
using Heureka.Common;
using Heureka.Testing;
using Heureka.Utils;

namespace Heureka
{
    public class Program : Form
    {
        private SearchResult RouteSearchResult;
        private RouteFindingKnowledgeBase RouteFindingKb;

        static public void Main()
        {
            Application.Run(new Program());
        }

        public Program()
        {
            this.RunInferenceTests();
            this.RunRouteFindingTests();

            this.PaintRoute();
        }

        private void RunRouteFindingTests()
        {
            var suite = new TestSuite("Route finding", false);
            suite.AddTest("Manhattan", "route_kbs/manhattan.kb", "0 0,9 5", true);
            suite.AddTest("Copenhagen", "route_kbs/copenhagen.kb", "45 70,65 100", true);
            suite.Run();
        }

        private void RunInferenceTests()
        {
            var suite = new TestSuite("Inference", true);
            suite.AddTest("Simple success", "inference_kbs/simple.kb", "-yes", true);
            suite.AddTest("Simple failure", "inference_kbs/simple.kb", "yes", false);
            suite.AddTest("Breakfast", "inference_kbs/breakfast.kb", "-breakfast", true);
            suite.AddTest("Ancestor (pq)", "inference_kbs/pq.kb", "q p", true);
            suite.AddTest("Espresso light", "inference_kbs/espresso_light.kb", "-hot-drink", true);
            suite.AddTest("Steam", "inference_kbs/steam.kb", "-steam", true);
            suite.AddTest("No steam (boiler off)", "inference_kbs/steam_boiler_off.kb", "steam", true);
            suite.AddTest("No steam (boiler broken)", "inference_kbs/steam_boiler_broken.kb", "steam", true);
            suite.AddTest("Espresso", "inference_kbs/espresso.kb", "-hot-drink", true);
            //suite.AddTest("Steam", "inference_kbs/steam.kb", "steam", true);
            suite.Run();
        }

        private void PaintRoute()
        {
            // Setup window
            this.Size = new Size(500, 500);
            this.Text = "Copenhagen route";
            this.Paint += this.OnPaint;

            this.RouteFindingKb = RouteFindingKnowledgeBase.Parse(File.ReadAllText("route_kbs/copenhagen.kb"));
            this.RouteSearchResult = AStar.Search(new RouteFindingNode(new RouteFindingState(45, 70), new RouteFindingState(65, 100)), this.RouteFindingKb);
        }

        private void OnPaint(object sender, PaintEventArgs args)
        {
            if (this.RouteSearchResult == null)  return;
            Painter.DrawKnowledgeBase(args.Graphics, this.RouteSearchResult.TraceNode as RouteFindingNode, this.RouteFindingKb, new RouteFindingState(45, 70), new RouteFindingState(65, 100));
        }
    }
}

