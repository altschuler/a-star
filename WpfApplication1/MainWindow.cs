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

      //this.RunRoutefinding("A* Route finding", @"/manhattan.txt");
      //this.RunRoutefinding("A* Route finding", @"/kb.txt");
      Application.Exit();
    }

    private void TestAllInference()
    {
        int succesCounter = 0;
        if(this.TestString("Simple success", "ja", "-ja", true)) succesCounter++;
        if (this.TestString("Simple failure", "ja", "ja", false)) succesCounter++;
        if (this.TestFile("Breakfast", "kbs/breakfast.kb", "-breakfast", true)) succesCounter++;
        if (this.TestFile("pq", "kbs/pq.kb", "q p", true)) succesCounter++;
        if (this.TestFile("Espresso light", "kbs/espresso_light.kb", "-hot-drink", true)) succesCounter++;
        if (this.TestFile("Steam", "kbs/steam.kb", "-steam", true)) succesCounter++;
        if (this.TestFile("No steam (boiler off)", "kbs/steam_boiler_off.kb", "steam", true)) succesCounter++;
        if (this.TestFile("No steam (boiler broken)", "kbs/steam_boiler_broken.kb", "steam", true)) succesCounter++;
        if (this.TestFile("Espresso", "kbs/espresso.kb", "-hot-drink", true)) succesCounter++;

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
      var fakeKb = InferenceParser.ParseString(target);
      var targetState = new StateInference(fakeKb.Rules.First().Clause);
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

