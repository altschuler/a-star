using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace ProjectAI.RouteFinding
{
  public class MainWindow : Form
  {
    private KnowledgeBase Kb;

    static public void Main()
    {
      Application.Run(new MainWindow());
    }

    public MainWindow()
    {
      //Heuristic function for inference:
      //Indirect-search cost: Length of clause (number of literals)

      this.TestString("Simple success", "ja", "-ja", true);
      this.TestString("Simple failure", "ja", "ja", false);
      this.TestFile("Breakfast", "kbs/breakfast.kb", "-breakfast", true);
      this.TestFile("Espresso", "kbs/espresso.kb", "-hot-drink", true);
      this.TestFile("Espresso light", "kbs/espresso_light.kb", "-hotdrink", true);
      this.TestFile("Steam", "kbs/steam.kb", "-steam", true);
      this.TestFile("pq", "kbs/pq.kb", "p q", true);

      //this.RunRoutefinding("A* Route finding", @"/manhattan.txt");
      //this.RunRoutefinding("A* Route finding", @"/kb.txt");
      Application.Exit();
    }
    
    private void TestString(string name, string str, string target, bool shouldSucceed)
    {
      this.Test(name, InferenceParser.ParseString(str), target, shouldSucceed);
    }

    private void TestFile(string name, string file, string target, bool shouldSucceed)
    {
      this.Test(name, InferenceParser.ParseFile(file), target, shouldSucceed);
    }

    private void Test(string name, KnowledgeBaseInference kb, string target, bool shouldSucceed)
    {
      Console.Write("Test: " + name + " - ");
      var fakeKb = InferenceParser.ParseString(target);
      var targetState = new StateInference(fakeKb.Rules.First().Clause);
      var result = AStarSearcherInference.Search(targetState, new StateInference(), kb);      

      Console.Write(result.Succeeded == shouldSucceed ? "SUCCESS" : "FAILURE");
      Console.WriteLine((result.Succeeded ? " (solved " : " (not solved ") + result.Iterations + ")");
    }

    private void RunRoutefinding(String title, String filepath)
    {
      // Setup window
      this.Size = new Size(500, 500);
      this.Text = title;
      this.Paint += new PaintEventHandler(this.OnPaint);

      // Load and parse knowledge base
      Console.Write("Parsing...");
      var kbFile = Environment.CurrentDirectory + filepath;
      this.Kb = KnowledgeBase.Parse(File.ReadAllText(kbFile));
      Console.WriteLine(" done");

      // Calculate route
      Console.Write("Searching...");
      //this.Solution = AStarSearcher.Search(/*this.Kb.States, this.Kb.Actions,*/ this.Kb.Start, this.Kb.End);
      Console.WriteLine(" done");
      //Console.WriteLine(String.Format("{0} iterations to find target", this.Solution.Iterations));
    }

    private void OnPaint(object sender, PaintEventArgs args)
    {
      //if (this.Solution == null || this.Kb == null) 
	//return;

      //Painter.DrawKnowledgeBase(args.Graphics, this.Solution.TraceNode, this.Kb);
    }
  }
}

