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

        public InferenceNode ApplyResolution(InferenceNode parent, AbstractAction act)
        {
            var state = new InferenceState();
            var actionState = act.StartState as InferenceState;

            foreach (var firstLiteral in parent.State.Clause)
            {
                foreach (var secondLiteral in actionState.Clause)
                {
                    if (firstLiteral.Name.Equals(secondLiteral.Name) && firstLiteral.Proposition != secondLiteral.Proposition)
                    {
                        // Merger samtlige literals fra de to clauses
                        state.Clause = parent.State.Clause.Concat(actionState.Clause).ToList();

                        // Fjern en enkelt positiv og en enkelt negativ
                        state.Clause.Remove(state.Clause.First(lit => lit.Name.Equals(secondLiteral.Name) && lit.Proposition));
                        state.Clause.Remove(state.Clause.First(lit => lit.Name.Equals(secondLiteral.Name) && !lit.Proposition));

                        // Fjerne duplikater, f.eks. A & A & B -> A & B
                        state.Clause = state.Clause.Distinct().ToList();

                        return new InferenceNode(parent, parent.Target, state, new InferenceAction(state, parent.Target));
                    }
                }
            }

            //Fjerner resten af modsatte literals..            
            return new InferenceNode(parent, parent.Target, state, new InferenceAction(state, parent.Target));
        }

        public IEnumerable<AbstractAction> ActionsForNode(AbstractNode node)
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

            var actions = new List<AbstractAction>();
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

        public AbstractNode Resolve(AbstractNode parent, AbstractAction action, AbstractState targetState)
        {
            return this.ApplyResolution(parent as InferenceNode, action);
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