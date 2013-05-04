using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectAI.RouteFinding
{
    public class KnowledgeBaseInference : IKnowledgeBase
    {
        public List<StateInference> Rules { get; set; }

        public KnowledgeBaseInference()
        {
            this.Rules = new List<StateInference>();
        }

        public NodeInference ApplyResolution(NodeInference node, ActionAbstract act, IEnumerable<StateAbstract> explored)
        {
            var state = new StateInference();
            var action = act.StartState as StateInference;

            foreach (var literal in node.State.Clause)
            {
                foreach (var rule in action.Clause)
                {
                    if (literal.Name.Equals(rule.Name) && literal.Proposition != rule.Proposition)
                    {
                        //Merger samtlige literals fra de to clauses
                        state.Clause = new List<Literal>(node.State.Clause.Concat(action.Clause).ToList());

                        //Remove ONE positive
                        for (var i = 0; i < state.Clause.Count; i++)
                        {
                            var litz = state.Clause.ElementAt(i);
                            if (litz.Name.Equals(rule.Name) && litz.Proposition)
                            {
                                state.Clause.Remove(litz);
                                break;
                            }
                        }
                        //Remove ONE negation
                        for (var i = 0; i < state.Clause.Count; i++)
                        {
                            var litz = state.Clause.ElementAt(i);
                            if (litz.Name.Equals(rule.Name) && !litz.Proposition)
                            {
                                state.Clause.Remove(litz);
                                break;
                            }
                        }

                        //Removes duplicates such that "a OR a OR b" becomes "a OR b"
                        var ls = new List<Literal>();
                        foreach (var lit in state.Clause)
                        {
                            var found = false;
                            foreach (var litInner in ls)
                            {
                                if (litInner.Equals(lit))
                                    found = true;
                            }
                            if (!found) ls.Add(lit);
                        }
                        state.Clause = ls;
                        //                        state.SortState(); //Used for debugging
                        return new NodeInference(node, node.Target, state, new ActionInference(state, node.Target));
                    }
                }
            }

            //Fjerner resten af modsatte literals..            
            return new NodeInference(node, node.Target, state, new ActionInference(state, node.Target));
        }

        public IEnumerable<ActionAbstract> ActionsForNode(NodeAbstract node)
        {
            List<StateInference> relevantRules = this.Rules.ToList();

            var parent = node.Parent;
            //M� kun bruge regel fra KB �n gang pr. s�ge-gren
            /*
                        if (relevantRules.Contains(node.Action))
                        {
                            //Console.WriteLine("removeing "+String.Join("  ", node.Action.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList()));
                            relevantRules.Remove(node.Action);
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
                relevantRules.Add(parent.State as StateInference);
                parent = parent.Parent;
            }
            //Ovenst�ende er et fors�g p� at anvende anscestor rigtigt.

            var actions = new List<ActionAbstract>();
            //foreach (var state in Rules)
            foreach (var state in relevantRules)
            {
                foreach (var literal in state.Clause)
                {
                    foreach (var innerLiterat in ((StateInference)node.State).Clause)
                    {
                        if (innerLiterat.Name.Equals(literal.Name) && innerLiterat.Proposition != literal.Proposition)
                        {
                            actions.Add(new ActionInference(state, node.Target as StateInference));
                        }
                    }
                }
            }

            return actions;
        }

        public NodeAbstract Resolve(NodeAbstract node, ActionAbstract action, StateAbstract targetState, IEnumerable<StateAbstract> explored)
        {
            return this.ApplyResolution(node as NodeInference, action, explored);
        }
    }
}