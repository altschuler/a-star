using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Heureka.Common;
using Heureka.Inference;
using Heureka.RouteFinding;

namespace Heureka.Testing
{
    public class TestSuite
    {
        public string Name { get; set; }
        public List<Test> Tests { get; set; }
        public bool IsInference { get; set; }

        public TestSuite(string name, bool isInference)
        {
            this.Tests = new List<Test>();
            this.Name = name;
            this.IsInference = isInference;
        }

        public void AddTest(string name, string file, string target, bool expected)
        {
            this.Tests.Add(new Test(name, file, target, expected));
        }

        public void Run()
        {
            Console.WriteLine();
            Console.WriteLine("--- " + this.Name + " ---");
            var succeeded = 0;
            foreach (var test in this.Tests)
            {
                Console.Write("Test: " + test.Name);
                SearchResult result;
                if (this.IsInference)
                {
                    var kb = InferenceKnowledgeBase.ParseFile(test.File);

                    var parsedTarget = InferenceKnowledgeBase.ParseString(test.Target).Rules.First();
                    var targetState = new InferenceState(parsedTarget.Clause);

                    result = AStar.Search(new InferenceNode(targetState, new InferenceState()), kb);
                }
                else
                {
                    var kb = RouteFindingKnowledgeBase.Parse(File.ReadAllText(test.File));

                    var coords = test.Target.Split(',');
                    var startCoord = coords[0].Split(' ');
                    var endCoord = coords[1].Split(' ');
                    var startState = new RouteFindingState(int.Parse(startCoord[0]), int.Parse(startCoord[1]));
                    var endState = new RouteFindingState(int.Parse(endCoord[0]), int.Parse(endCoord[1]));

                    result = AStar.Search(new RouteFindingNode(startState, endState), kb);
                }

                if (result.Succeeded == test.Exepected)
                    succeeded++;

                Console.Write(result.Succeeded == test.Exepected ? ": SUCCESS" : ": FAILURE");
                Console.WriteLine((result.Succeeded ? " (solved " : " (not solved ") + result.Iterations + ")");
            }
            var succesRatio = (100 * succeeded) / (double)this.Tests.Count;
            Console.WriteLine("Passed " + succeeded + " of " + this.Tests.Count + " (" + succesRatio.ToString("000") + "%)");
        }
    }
}