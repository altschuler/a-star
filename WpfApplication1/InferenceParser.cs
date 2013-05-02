using System;
using System.IO;

namespace ProjectAI.RouteFinding
{
  public class InferenceParser
  {
    public static KnowledgeBaseInference Parse(String file)
    {
      var kb = new KnowledgeBaseInference();
      foreach (var line in File.ReadAllLines(file))
      {
	var rule = new StateInference();
	foreach (string lit in line.Split(' '))
	{    
	  var isNegated = lit.StartsWith("-");
	  rule.Clause.Add(new Literal(isNegated ? lit.Substring(1) : lit, isNegated));
	}
	kb.Rules.Add(rule);
      }
      return kb;
    }
  }
}