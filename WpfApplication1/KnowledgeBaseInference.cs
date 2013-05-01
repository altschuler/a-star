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

        public NodeInference ApplyResolution(NodeInference node, StateInference action)
        {
            bool breaked = false;
            var state= new StateInference();

//fjerner ét literal-par og merger
            foreach (Literal literal in node.State.Clause)
            {
                foreach (Literal rule in action.Clause)
                {
                    if(literal.Name.Equals(rule.Name) && literal.Negated != rule.Negated)
                    {
                        
                        state.Clause = node.State.Clause.Union(action.Clause).Where(l => !l.Name.Equals(literal.Name)).ToList();
                        int waterCounter = 0;
                        foreach (Literal catLitter in state.Clause)
                        {
                            if (catLitter.Name.Equals("water"))
                            {
                                waterCounter++;
                            }
                        }
                        if (waterCounter > 1)
                        {
                            //Console.WriteLine("");
                            //Console.WriteLine("node: " + String.Join("  ", node.State.Clause.Select(l => (l.Negated ? "_" : "") + l.Name + ",").ToList()));
                            //Console.WriteLine("rule: " + String.Join("  ", action.Clause.Select(l => (l.Negated ? "_" : "") + l.Name + ",").ToList()));
                            //Console.WriteLine("new node: " + String.Join("  ", state.Clause.Select(l => (l.Negated ? "_" : "") + l.Name + ",").ToList()));
                            //Console.WriteLine("");
                        }
                        breaked = true;
                        break;
                    }
                    if (breaked)
                        break;
                }
            }

//Fjerner resten af modsatte literals..            
            bool stupidToggle = true;

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

                        if (first.Name.Equals(second.Name) && first.Negated != second.Negated)
                        {
                            //Console.WriteLine("duplicate detected: " + String.Join("  ", state.Clause.Select(l => (l.Negated ? "_" : "") + l.Name + ",").ToList()));
                            duplicate = true;
                            firstIndex = i;
                            firstName = first.Name;
                            secondIndex = j;
                        }

                        if (first.Name.Equals(second.Name) && first.Negated == second.Negated)
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
                        return new NodeInference(node, node.Target, state);
                    }
                    state.Clause.RemoveAt(firstIndex);
                    state.Clause.RemoveAt(secondIndex - 1);
                    //Console.WriteLine("nedenstående linje burde være her jf. Dan True");
                    //state.Clause.Add(new Literal(firstName, false));


                    //Console.WriteLine("fixed node: " + String.Join("  ", state.Clause.Select(l => (l.Negated ? "_" : "") + l.Name + ",").ToList()));
                }else
                    stupidToggle = false;
            }

            return new NodeInference(node, node.Target, state);


            
            throw new Exception("Failed resolution");
        }



        public List<StateInference> ActionsForNode(NodeInference node)
        {
            List<StateInference> actions = new List<StateInference>();
            foreach (var state in Rules)
            {
                foreach (var literal in state.Clause)
                {
                    foreach (var innerLiterat in node.State.Clause)
                    {
                        if (innerLiterat.Name.Equals(literal.Name) && innerLiterat.Negated != literal.Negated)
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