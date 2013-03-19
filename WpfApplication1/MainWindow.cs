using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;

namespace ProjectAI.RouteFinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class MainWindow : Form
    {
      static public void Main()
      {
	System.Windows.Forms.Application.Run(new MainWindow());
      }

        public MainWindow()
        {
            var states = new List<State>();
            var startState = new State(35, 80); 
            var endState = new State(45,70);
            var x10y70 = new State(10, 70);     states.Add(x10y70);
            var x20y50 = new State(20, 50);     states.Add(x20y50);
            var x35y35 = new State(35, 35);     states.Add(x35y35);
            var x35y80 = startState;            states.Add(x35y80);
            var x65y100 = new State(65, 100);   states.Add(x65y100);
            var x45y70 = endState;              states.Add(x45y70);
            var x55y55 = new State(55, 55);     states.Add(x55y55);
            var x80y70 = new State(80, 70);     states.Add(x80y70);
            var x60y150 = new State(60, 150);   states.Add(x60y150);
            var x65y110 = new State(65, 110);   states.Add(x65y110);
            var x70y85 = new State(70, 85);     states.Add(x70y85);
            var x25y100 = new State(25, 100);   states.Add(x25y100);
            var x50y90 = new State(50, 90);     states.Add(x50y90);
            var x35y120 = new State(35, 120);   states.Add(x35y120);

            
            var actions = new List<Action>() {
                new Action(x10y70, "Vestervoldgade",x20y50), 
                new Action(x20y50, "Vestervoldgade", x10y70),
                new Action(x20y50, "Vestervoldgade", x35y35),
                new Action(x35y35, "Vestervoldgade", x20y50),

                new Action(x10y70, "SktPedersStraede", x35y80),
                new Action(x35y80, "SktPedersStraede", x50y90),
                new Action(x65y100, "SktPedersStraede", x50y90),

                new Action(x20y50, "Studiestraede", x45y70),
                new Action(x45y70, "Studiestraede", x70y85),

                new Action(x55y55, "Vestergade", x35y35),
                new Action(x80y70, "Vestergade", x55y55),

                new Action(x60y150, "Noerregade", x65y110),
                new Action(x65y110, "Noerregade", x65y100),
                new Action(x65y100, "Noerregade", x70y85),
                new Action(x70y85, "Noerregade", x80y70),

                new Action(x45y70, "Larsbjoernsstraede", x55y55),
                new Action(x45y70, "Larsbjoernsstraede", x35y80),

                new Action(x25y100, "TeglgaardsStraede", x35y80),

                new Action(x50y90, "LarslejStraede", x35y120),

                new Action(x10y70, "Noerrevoldgade", x25y100),
                new Action(x25y100, "Noerrevoldgade", x10y70),
                new Action(x25y100, "Noerrevoldgade", x35y120),
                new Action(x35y120, "Noerrevoldgade", x25y100),
                new Action(x35y120, "Noerrevoldgade", x60y150),
                new Action(x60y150, "Noerrevoldgade", x35y120)
                    };
            

            Node solution = AStarSearcher.Search(states, actions, startState, endState);

            Node tracingNode = solution;
            Console.WriteLine("Løsningen baglæns: ");
            while (tracingNode.Parent != null)
            {
                Console.WriteLine(tracingNode.Action.Name + ", pathcost: " + tracingNode.PathCost +  ", (X,Y): (" + tracingNode.State.X + " , " + tracingNode.State.Y + ")");
                tracingNode = tracingNode.Parent;
            }
            Console.WriteLine("startpunkt(X,Y): (" + startState.X + " , " + startState.Y + ")");

        }

        private void initialize()
        {
            
        }
    }
}
