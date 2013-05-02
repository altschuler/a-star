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
      this.TestFile("Steam", "kbs/steam.kb", "steam", true);
      this.TestFile("pq", "kbs/pq.kb", "p q", true);
      this.RunInferenceTest06();
      //this.RunRoutefinding("A* Route finding", @"/manhattan.txt");
      //this.RunRoutefinding("A* Route finding", @"/kb.txt");
      Application.Exit();
    }

    private void RunInferenceTest06()
    {
        Console.WriteLine("Skal løses: ");
        var inferenceKB = new KnowledgeBaseInference();

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() {
            new Literal("p", false),
            new Literal("q", true) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() {
            new Literal("p", true),
            new Literal("q", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() {
            new Literal("p", false),
            new Literal("q", false) }));

        Console.WriteLine(AStarSearcherInference.Search(new StateInference(new List<Literal>() { new Literal("p", true), new Literal("q", true) }), new StateInference(), inferenceKB).Succeeded);
        
    }
    
    private void TestString(string name, string str, string target, bool shouldSucceed)
    {
//<<<<<<< HEAD
//        Console.WriteLine("Espresso (skal løses):");
//        var inferenceKB = new KnowledgeBaseInference();

//        //1st rule        
//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("okpump", false),
//            new Literal("onpump", false),
//            new Literal("water", true) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("manfill", false),
//            new Literal("water", true) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("manfill", false),
//            new Literal("onpump", false) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("manfill", true),
//            new Literal("onpump", true) }));
////5
//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("water", false),
//            new Literal("okboiler", false),
//            new Literal("onboiler", false),
//            new Literal("steam", true) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("water", true),
//            new Literal("steam", false) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("onboiler", true),
//            new Literal("steam", false) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("okboiler", true),
//            new Literal("steam", false) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("steam", false),
//            new Literal("coffee", false),
//            new Literal("hotdrink", true) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("steam", false),
//            new Literal("tea", false),
//            new Literal("hotdrink", true) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("coffee", true),
//            new Literal("tea", true) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("okpump", true) }));
//        //inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("manfill", true) }));
//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("okboiler", true) }));
//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("onboiler", true) }));

//        AStarSearcherInference.Search(new StateInference(new List<Literal>() { new Literal("hotdrink", false) }), new StateInference(), inferenceKB);
//=======
      this.Test(name, InferenceParser.ParseString(str), target, shouldSucceed);
//>>>>>>> 4dee8d37d66225ce8d96a66ed24be6cb19e87985
    }

    private void TestFile(string name, string file, string target, bool shouldSucceed)
    {
//<<<<<<< HEAD
//        var inferenceKB = new KnowledgeBaseInference();

//        //1st rule        
//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("okpump", false),
//            new Literal("onpump", false),
//            new Literal("water", true) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("manfill", false),
//            new Literal("water", true) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("manfill", false),
//            new Literal("onpump", false) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("manfill", true),
//            new Literal("onpump", true) }));
//        //5
//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("water", false),
//            new Literal("okboiler", false),
//            new Literal("onboiler", false),
//            new Literal("steam", true) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("water", true),
//            new Literal("steam", false) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("onboiler", true),
//            new Literal("steam", false) }));

//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
//            new Literal("okboiler", true),
//            new Literal("steam", false) }));

//        Console.WriteLine("Steam:");
//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("okpump", true) }));
//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("okboiler", false) }));
//        //inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("water", true) }));
//        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("onboiler", true) }));

//        AStarSearcherInference.Search(new StateInference(new List<Literal>() { new Literal("steam", true) }), new StateInference(), inferenceKB);
//=======
      this.Test(name, InferenceParser.ParseFile(file), target, shouldSucceed);
//>>>>>>> 4dee8d37d66225ce8d96a66ed24be6cb19e87985
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

