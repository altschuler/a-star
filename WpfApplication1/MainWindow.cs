﻿using System;
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
      private Node Solution;
      private KnowledgeBase Kb;

      static public void Main()
      {
	Application.Run(new MainWindow());
      }

      public MainWindow()
      {	
	// Setup window
	this.Size = new Size(500, 500);
	this.Text = "A* Route finding";
	this.Paint += new PaintEventHandler(this.OnPaint);

	// Load and parse knowledge base
	Console.WriteLine("Parsing...");
    var kbFile = Environment.CurrentDirectory + @"/kb.txt";
    //var kbFile = Environment.CurrentDirectory + @"/manhattan.txt";
	this.Kb = KnowledgeBase.Parse(File.ReadAllText(kbFile));
	Console.WriteLine("Parsed");

	// Calculate route
	Console.WriteLine("Searching...");
	Console.WriteLine("Start:" + this.Kb.Start);
	Console.WriteLine("End:" + this.Kb.End);
	this.Solution = AStarSearcher.Search(this.Kb.States, this.Kb.Actions, this.Kb.Start, this.Kb.End);
	Console.WriteLine("Searched");
      }

      private void OnPaint(object sender, PaintEventArgs args)
      {
	if (this.Solution == null || this.Kb == null) 
	  return;

	Painter.DrawKnowledgeBase(args.Graphics, this.Solution, this.Kb);
      }
    }
}

