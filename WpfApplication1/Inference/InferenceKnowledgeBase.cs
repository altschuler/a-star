using System.Collections.Generic;
using System.IO;
using System.Linq;
using Heureka.Common;

namespace Heureka.Inference
{
    public class InferenceKnowledgeBase : IKnowledgeBase
    {
        public List<InferenceState> Rules { get; set; }

        public InferenceKnowledgeBase()
        {
            this.Rules = new List<InferenceState>();
        }

        public InferenceNode ApplyResolution(InferenceNode parent, ActionAbstract act, IEnumerable<StateAbstract> explored)
        {
            var state = new InferenceState();
            var action = act.StartState as InferenceState;

            foreach (var literal in parent.State.Clause)
            {
                foreach (var rule in action.Clause)
                {
                    if (literal.Name.Equals(rule.Name) && literal.Proposition != rule.Proposition)
                    {
                        // Merger samtlige literals fra de to clauses
                        state.Clause = parent.State.Clause.Concat(action.Clause).ToList();

                        // Remove ONE positive
                        foreach (var lit in state.Clause)
                        {
                            if (lit.Name.Equals(rule.Name) && lit.Proposition)
                            {
                                state.Clause.Remove(lit);
                                break;
                            }
                        }

                        // Remove ONE negation
                        foreach (var lit in state.Clause)
                        {
                            if (lit.Name.Equals(rule.Name) && !lit.Proposition)
                            {
                                state.Clause.Remove(lit);
                                break;
                            }
                        }

                        //Removes duplicates such that "a OR a OR b" becomes "a OR b"
                        var ls = new List<Literal>();
                        foreach (var lit in state.Clause)
                        {
                            if (!ls.Contains(lit)) 
                                ls.Add(lit);
                        }
                        state.Clause = ls;
                        return new InferenceNode(parent, parent.Target, state, new InferenceAction(state, parent.Target));
                    }
                }
            }

            //Fjerner resten af modsatte literals..            
            return new InferenceNode(parent, parent.Target, state, new InferenceAction(state, parent.Target));
        }

        public IEnumerable<ActionAbstract> ActionsForNode(NodeAbstract node)
        {
            var relevantRules = this.Rules.ToList();

            var parent = node.Parent;
            //Må kun bruge regel fra KB én gang pr. søge-gren
            /*
                        if (relevantRules.Contains(inferenceNode.Action))
                        {
                            //Console.WriteLine("removeing "+String.Join("  ", inferenceNode.Action.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList()));
                            relevantRules.Remove(inferenceNode.Action);
                        }

                        while (parent != null)
                        {

                            if (relevantRules.Contains(parent.Action))
                                relevantRules.Remove(parent.Action);

                            parent = parent.Parent; //lol, hvor meta
                        }
                        */
            parent = node.Parent;
            while (parent != null)
            {
                relevantRules.Add(parent.State as InferenceState);
                parent = parent.Parent;
            }
            //Ovenstående er et forsøg på at anvende anscestor rigtigt.

            var actions = new List<ActionAbstract>();
            //foreach (var state in Rules)
            foreach (var state in relevantRules)
            {
                foreach (var literal in state.Clause)
                {
                    foreach (var innerLiterat in ((InferenceState)node.State).Clause)
                    {
                        if (innerLiterat.Name.Equals(literal.Name) && innerLiterat.Proposition != literal.Proposition)
                        {
                            actions.Add(new InferenceAction(state, node.Target as InferenceState));
                        }
                    }
                }
            }

            return actions;
        }

        public NodeAbstract Resolve(NodeAbstract parent, ActionAbstract action, StateAbstract targetState, IEnumerable<StateAbstract> explored)
        {
            return this.ApplyResolution(parent as InferenceNode, action, explored);
        }

        public static InferenceKnowledgeBase Parse(string[] lines)
        {
            var kb = new InferenceKnowledgeBase();
            foreach (var line in lines)
            {
                var rule = new InferenceState();
                foreach (var lit in line.Split(' '))
                {
                    var isNegated = lit.StartsWith("-");
                    rule.Clause.Add(new Literal(isNegated ? lit.Substring(1) : lit, !isNegated));
                }
                kb.Rules.Add(rule);
            }
            return kb;
        }

        public static InferenceKnowledgeBase ParseFile(string file)
        {
            return Parse(File.ReadAllLines(file));
        }

        public static InferenceKnowledgeBase ParseString(string str)
        {
            return Parse(str.Split('\n'));
        }
    }
}