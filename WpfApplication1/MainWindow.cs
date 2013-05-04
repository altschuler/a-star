using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace ProjectAI.RouteFinding
{
    public class MainWindow : Form
    {
        private int InferenceTestCounter = 0;
        static public void Main()
        {
            Application.Run(new MainWindow());
        }

        public MainWindow()
        {
            //Heuristic function for inference:
            //Indirect-search cost: Length of clause (number of literals)

            this.TestAllInference();

            this.RunRoutefinding("A* Route finding", "manhattan.txt");
            //this.RunRoutefinding("A* Route finding", @"/kb.txt");
            Application.Exit();
        }

        private void TestAllInference()
        {
            int succesCounter = 0;
            if (this.TestString("Simple success", "ja", "-ja", true)) succesCounter++;
            if (this.TestString("Simple failure", "ja", "ja", false)) succesCounter++;
            if (this.TestFile("Breakfast", "kbs/breakfast.kb", "-breakfast", true)) succesCounter++;
            if (this.TestFile("pq", "kbs/pq.kb", "q p", true)) succesCounter++;
            if (this.TestFile("Espresso light", "kbs/espresso_light.kb", "-hot-drink", true)) succesCounter++;
            if (this.TestFile("Steam", "kbs/steam.kb", "-steam", true)) succesCounter++;
            if (this.TestFile("No steam (boiler off)", "kbs/steam_boiler_off.kb", "steam", true)) succesCounter++;
            if (this.TestFile("No steam (boiler broken)", "kbs/steam_boiler_broken.kb", "steam", true)) succesCounter++;
            if (this.TestFile("Espresso", "kbs/espresso.kb", "-hot-drink", true)) succesCounter++;
            //if (this.TestFile("Unsolvable steam", "kbs/steam.kb", "steam", false)) succesCounter++;

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
            var result = AStarSearcherInference.Search(targetState, new StateInference(), kb);

            Console.Write(result.Succeeded == shouldSucceed ? "SUCCESS" : "FAILURE");
            Console.WriteLine((result.Succeeded ? " (solved " : " (not solved ") + result.Iterations + ")");
            return result.Succeeded == shouldSucceed;
        }

        private void RunRoutefinding(String title, String filepath)
        {
            // Setup window
            this.Size = new Size(500, 500);
            this.Text = title;
            this.Paint += this.OnPaint;

            // Load and parse knowledge base
            var kb = KnowledgeBase.Parse(File.ReadAllText(filepath));

            // Calculate route
            Console.Write("Searching...");
            var result = AStarSearcher.Search(new StateRoutefinding(0, 0), new StateRoutefinding(9, 5), kb);
            Console.WriteLine(result.Succeeded + " " + result.Iterations);
        }

        private void OnPaint(object sender, PaintEventArgs args)
        {
            //if (this.Solution == null || this.Kb == null) 
            //return;

            //Painter.DrawKnowledgeBase(args.Graphics, this.Solution.TraceNode, this.Kb);
        }
    }
}

