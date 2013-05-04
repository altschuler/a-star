using System;
using System.IO;

namespace ProjectAI.RouteFinding
{
    public class InferenceParser
    {
        public static KnowledgeBaseInference Parse(string[] lines)
        {
            var kb = new KnowledgeBaseInference();
            foreach (var line in lines)
            {
                var rule = new StateInference();
                foreach (string lit in line.Split(' '))
                {
                    var isNegated = lit.StartsWith("-");
                    rule.Clause.Add(new Literal(isNegated ? lit.Substring(1) : lit, !isNegated));
                }
                kb.Rules.Add(rule);
            }
            return kb;
        }

        public static KnowledgeBaseInference ParseFile(string file)
        {
            return Parse(File.ReadAllLines(file));
        }

        public static KnowledgeBaseInference ParseString(string str)
        {
            return Parse(str.Split('\n'));
        }
    }
}