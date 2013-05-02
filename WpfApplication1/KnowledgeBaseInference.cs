using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectAI.RouteFinding
{
    public class KnowledgeBaseInference
    {
        public List<StateInference> Rules { get; set; }

        public KnowledgeBaseInference()
        {
            this.Rules = new List<StateInference>();
        }

        public NodeInference ApplyResolution(NodeInference node, StateInference action, List<StateInference> explored)
        {
            bool breaked = false;
            var state= new StateInference();

//fjerner ét literal-par og merger
            foreach (Literal literal in node.State.Clause)
            {
                foreach (Literal rule in action.Clause)
                {
                    if(literal.Name.Equals(rule.Name) && literal.Proposition != rule.Proposition)
                    {
                        state.Clause = node.State.Clause.Concat(action.Clause).ToList();
                        //Console.WriteLine(String.Join("  ", state.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList()));
                        //state.Clause.Union(new List<Literal>());
                        //Console.WriteLine(String.Join("  ", state.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList()));
                        
//Remove ONE positive
                        for (int i = 0; i < state.Clause.Count; i++)
                        {
                            Literal litz = state.Clause.ElementAt(i);
                            if (litz.Name.Equals(rule.Name) && litz.Proposition)
                            {
                                state.Clause.RemoveAt(i);
                                break;
                            }
                        }
//Remove ONE negation
                        for (int i = 0; i < state.Clause.Count; i++)
                        {
                            Literal litz = state.Clause.ElementAt(i);
                            if (litz.Name.Equals(rule.Name) && !litz.Proposition)
                            {
                                state.Clause.RemoveAt(i);
                                break;
                            }
                        }

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
                        //Console.WriteLine(String.Join("  ", state.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList()));
                        state.SortState();
                        return new NodeInference(node, node.Target, state, action);
                        //if (rule.Name.Equals("steam"))
                        //{
                        //    Console.WriteLine("what up?! "+String.Join("  ", state.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList()));
                        //}

                        


                        //Console.WriteLine("\nnode: " + String.Join("  ", node.State.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList())
                        //    + "\nrule: " + String.Join("  ", action.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList())
                        //    + "\nnew node: " + String.Join("  ", state.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList()) + "\n");
                        //if(explored.Contains(state))
                        //    Console.WriteLine("State has been explored before");

                        
                        breaked = true;
                        break;
                    }
                    if (breaked)
                        break;
                }
            }

//Fjerner resten af modsatte literals..            
            bool stupidToggle = true;
            stupidToggle = false;
            while(stupidToggle)
            {
                bool duplicate = false;
                string firstName = "";
                int firstIndex = 0;
                int secondIndex = 0;

                for (int i = 0; i < state.Clause.Count - 1; i++)
                {
                    for (int j = i + 1; j < state.Clause.Count; j++)
                    {
                        Literal first = state.Clause.ElementAt(i);
                        Literal second = state.Clause.ElementAt(j);

                        if (first.Name.Equals(second.Name) && first.Proposition != second.Proposition)
                        {
                            //Console.WriteLine("duplicate detected: " + String.Join("  ", state.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList()));
                            duplicate = true;
                            firstIndex = i;
                            firstName = first.Name;
                            secondIndex = j;
                        }

                        if (first.Name.Equals(second.Name) && first.Proposition == second.Proposition)
                        {
                            throw new Exception("Failed resolution #666");
                        }
                    }
                }
                if (duplicate)
                {
                    if (state.Clause.Count == 2)
                    {
                        state.Clause.RemoveAt(0);
                        state.Clause.RemoveAt(0);
                        return new NodeInference(node, node.Target, state,action);
                    }
                    state.Clause.RemoveAt(firstIndex);
                    state.Clause.RemoveAt(secondIndex - 1);
                    //Console.WriteLine("nedenstående linje burde være her jf. Dan True");
                    //state.Clause.Add(new Literal(firstName, false));


                    //Console.WriteLine("fixed node: " + String.Join("  ", state.Clause.Select(l => (l.Proposition ? "" : "_") + l.Name + ",").ToList()));
                }else
                    stupidToggle = false;
            }

            return new NodeInference(node, node.Target, state,action);


            
            throw new Exception("Failed resolution");
        }



        public List<StateInference> ActionsForNode(NodeInference node)
        {
            List<StateInference> relevantRules = new List<StateInference>();
            foreach (var state in this.Rules)
            {
                relevantRules.Add(state);
            }

            NodeInference parent = node.Parent;
//Må kun bruge regel fra KB én gang pr. søge-gren
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
                relevantRules.Add(parent.State);

                parent = parent.Parent; //lol, hvor meta
            }
//Ovenstående er et forsøg på at anvende anscestor rigtigt.

            List<StateInference> actions = new List<StateInference>();
            //foreach (var state in Rules)
            foreach (var state in relevantRules)
            {
                foreach (var literal in state.Clause)
                {
                    foreach (var innerLiterat in node.State.Clause)
                    {
                        if (innerLiterat.Name.Equals(literal.Name) && innerLiterat.Proposition != literal.Proposition)
                        {
                            actions.Add(state);
                        }
                    }
                }
            }

            return actions;
            //return Kb.Rules.Where(r => r.Clause.Any(l => l))).ToList();

        }
    }
}