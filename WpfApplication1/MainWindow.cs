using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace ProjectAI.RouteFinding
{
    public class MainWindow : Form
    {
        private int InferenceTestCounter;
        private SearchResult RouteSearchResult;
        private KnowledgeBase RouteKB;

        static public void Main()
        {
            Application.Run(new MainWindow());
        }

        public MainWindow()
        {
            //Heuristic function for inference:
            //Indirect-search cost: Length of clause (number of literals)

            this.TestAllInference();
            this.RunRoutefinding("A* Route finding", "route_kbs/manhattan.txt", new StateRoutefinding(0, 0), new StateRoutefinding(9, 5));
            this.RunRoutefinding("A* Route finding", "route_kbs/kb.txt", new StateRoutefinding(45, 70), new StateRoutefinding(65, 100));
            Application.Exit();
        }

        private void TestAllInference()
        {
            int succesCounter = 0;
            if (this.TestString("Simple success", "ja", "-ja", true)) succesCounter++;
            if (this.TestString("Simple failure", "ja", "ja", false)) succesCounter++;
            if (this.TestFile("Breakfast", "inference_kbs/breakfast.kb", "-breakfast", true)) succesCounter++;
            if (this.TestFile("pq", "inference_kbs/pq.kb", "q p", true)) succesCounter++;
            if (this.TestFile("Espresso light", "inference_kbs/espresso_light.kb", "-hot-drink", true)) succesCounter++;
            if (this.TestFile("Steam", "inference_kbs/steam.kb", "-steam", true)) succesCounter++;
            if (this.TestFile("No steam (boiler off)", "inference_kbs/steam_boiler_off.kb", "steam", true)) succesCounter++;
            if (this.TestFile("No steam (boiler broken)", "inference_kbs/steam_boiler_broken.kb", "steam", true)) succesCounter++;
            if (this.TestFile("Espresso", "inference_kbs/espresso.kb", "-hot-drink", true)) succesCounter++;
            //if (this.TestFile("Unsolvable steam", "inference_kbs/steam.kb", "steam", false)) succesCounter++;

            double succesRatio = succesCounter / (double)this.InferenceTestCounter;
            Console.WriteLine("Inference-succes-ratio: " + succesRatio + ", passed " + succesCounter + " of " + this.InferenceTestCounter);
        }

        private bool TestString(string name, string str, string target, bool shouldSucceed)
        {
            return this.Test(name, InferenceParser.ParseString(str), target, shouldSucceed);
        }

        private bool TestFile(string name, string file, string target, bool shouldSucceed)
        {
            return this.Test(name, InferenceParser.ParseFile(file), target, shouldSucceed);
        }

        private bool Test(string name, KnowledgeBaseInference kb, string target, bool shouldSucceed)
        {
            this.InferenceTestCounter++;
            Console.Write("Test: " + name + " - ");
            var parsedTarget = InferenceParser.ParseString(target).Rules.First();
            var targetState = new StateInference(parsedTarget.Clause);
            var result = AStarSearcher.Search(new StateInference(), new NodeInference(targetState, new StateInference()), kb);

            Console.Write(result.Succeeded == shouldSucceed ? "SUCCESS" : "FAILURE");
            Console.WriteLine((result.Succeeded ? " (solved " : " (not solved ") + result.Iterations + ")");
            return result.Succeeded == shouldSucceed;
        }

        private void RunRoutefinding(String title, String filepath, StateRoutefinding startState, StateRoutefinding endState)
        {
            // Setup window
            this.Size = new Size(500, 500);
            this.Text = title;
            this.Paint += this.OnPaint;

            // Load and parse knowledge base
            var kb = KnowledgeBase.Parse(File.ReadAllText(filepath));

            // Calculate route
            Console.Write("Searching...");
            var result = AStarSearcher.Search(endState, new NodeRoutefinding(startState, endState),  kb);
            Console.WriteLine(result.Succeeded + " " + result.Iterations);

            this.RouteSearchResult = result;
            this.RouteKB = kb;
        }

        private void OnPaint(object sender, PaintEventArgs args)
        {
            if (this.RouteSearchResult == null) 
                return;

            Painter.DrawKnowledgeBase(args.Graphics, this.RouteSearchResult.TraceNode as NodeRoutefinding, this.RouteKB, new StateRoutefinding(45, 70), new StateRoutefinding(65, 100));
        }
    }
}

