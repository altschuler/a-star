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
	private SearchResult Solution;
	private KnowledgeBase Kb;

	static public void Main()
	{
	    Application.Run(new MainWindow());
	}

	public MainWindow()
	{

//Heuristic function for inference:
        //Indirect-search cost: Length of clause (number of literals)
        
        this.RunInferenceTest01();
        this.RunInferenceTest02();
        this.RunInferenceTest03();
        this.EspressoLightTest();
        this.RunInferenceTest06();
        
        this.EspressoTest();
        this.SteamTest();
        
        //this.RunRoutefinding("A* Route finding", @"/manhattan.txt");
        //this.RunRoutefinding("A* Route finding", @"/kb.txt");
	}

    private void RunInferenceTest01()
    {
        Console.WriteLine("skal løses:");
        var inferenceKB = new KnowledgeBaseInference();

        var rules = new StateInference(new List<Literal>() { new Literal("ja",false) });

        inferenceKB.Rules.Add(rules);
        AStarSearcherInference.Search(new StateInference(new List<Literal>() { new Literal("ja", true) }), new StateInference(), inferenceKB);
    }

    private void RunInferenceTest02()
    {
        Console.WriteLine("skal ikke løses:");
        var inferenceKB = new KnowledgeBaseInference();

        var rules = new StateInference(new List<Literal>() { new Literal("ja", false) });

        inferenceKB.Rules.Add(rules);
        AStarSearcherInference.Search(new StateInference(new List<Literal>() { new Literal("ja", false) }), new StateInference(), inferenceKB);
    }

    private void RunInferenceTest03()
    {
        Console.WriteLine("Breakfast (skal løses):");
        var inferenceKB = new KnowledgeBaseInference();

//1st rule        
        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("hotdrink", true),
            new Literal("juice", true),
            new Literal("food", true),
            new Literal("breakfast", false) }));
        //2nd
        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("hotdrink", true),
            new Literal("food", true),
            new Literal("breakfast", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("coffee", true),
            new Literal("cream", true),
            new Literal("hotdrink", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("tea", true),
            new Literal("hotdrink", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("toast", true),
            new Literal("butter", true),
            new Literal("food", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("egg", true),
            new Literal("food", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("coffee", false) }));
        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("tea", false) }));
        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("toast", false) }));
        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("butter", false) }));

        AStarSearcherInference.Search(new StateInference(new List<Literal>() { new Literal("breakfast", true) }), new StateInference(), inferenceKB);
    }


    private void EspressoTest()
    {
        Console.WriteLine("Espresso (skal løses):");
        var inferenceKB = new KnowledgeBaseInference();

        //1st rule        
        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("okpump", true),
            new Literal("onpump", true),
            new Literal("water", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("manfill", true),
            new Literal("water", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("manfill", true),
            new Literal("onpump", true) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("manfill", false),
            new Literal("onpump", false) }));
//5
        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("water", true),
            new Literal("okboiler", true),
            new Literal("onboiler", true),
            new Literal("steam", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("water", false),
            new Literal("steam", true) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("onboiler", false),
            new Literal("steam", true) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("okboiler", false),
            new Literal("steam", true) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("steam", true),
            new Literal("coffee", true),
            new Literal("hotdrink", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("steam", true),
            new Literal("tea", true),
            new Literal("hotdrink", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("coffee", false),
            new Literal("tea", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("okpump", false) }));
        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("okboiler", false) }));
        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("onboiler", false) }));

        AStarSearcherInference.Search(new StateInference(new List<Literal>() { new Literal("hotdrink", true) }), new StateInference(), inferenceKB);
    }


    private void SteamTest()
    {
        var inferenceKB = new KnowledgeBaseInference();

        //1st rule        
        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("okpump", true),
            new Literal("onpump", true),
            new Literal("water", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("manfill", true),
            new Literal("water", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("manfill", true),
            new Literal("onpump", true) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("manfill", false),
            new Literal("onpump", false) }));
        //5
        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("water", true),
            new Literal("okboiler", true),
            new Literal("onboiler", true),
            new Literal("steam", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("water", false),
            new Literal("steam", true) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("onboiler", false),
            new Literal("steam", true) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("okboiler", false),
            new Literal("steam", true) }));

        Console.WriteLine("Steam (should fail):");
        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("okpump", false) }));
        //inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("okboiler", false) }));
        //inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("onboiler", false) }));

        AStarSearcherInference.Search(new StateInference(new List<Literal>() { new Literal("steam", true) }), new StateInference(), inferenceKB);
    }
    private void EspressoLightTest()
    {
        Console.WriteLine("Espresso-light (skal løses):");
        var inferenceKB = new KnowledgeBaseInference();

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("steam", true),
            new Literal("coffee", true),
            new Literal("hotdrink", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("steam", true),
            new Literal("tea", true),
            new Literal("hotdrink", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("coffee", false),
            new Literal("tea", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { new Literal("steam", false) }));

        AStarSearcherInference.Search(new StateInference(new List<Literal>() { new Literal("hotdrink", true) }), new StateInference(), inferenceKB);
    }
    private void RunInferenceTest06()
    {
        Console.WriteLine("Skal løses: ");
        var inferenceKB = new KnowledgeBaseInference();

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("p", true),
            new Literal("q", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("p", false),
            new Literal("q", false) }));

        inferenceKB.Rules.Add(new StateInference(new List<Literal>() { 
            new Literal("p", true),
            new Literal("q", true) }));


        AStarSearcherInference.Search(new StateInference(new List<Literal>() { new Literal("p", false),new Literal("q", false)  }), new StateInference(), inferenceKB);
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
        Console.WriteLine(String.Format("{0} iterations to find target", this.Solution.Iterations));
    }

	private void OnPaint(object sender, PaintEventArgs args)
	{
	    if (this.Solution == null || this.Kb == null) 
		return;

        //Painter.DrawKnowledgeBase(args.Graphics, this.Solution.TraceNode, this.Kb);
	}
    }
}

