using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
            var startState = new State(35, 80);
            var endState = new State(45,70);
            var x10y70 = new State(10, 70);
            var x20y50 = new State(20, 50);
            var x35y35 = new State(35, 35);
            var x35y80 = startState;
            var x65y100 = new State(65, 100);
            var x45y70 = new State(45, 70);
            var x55y55 = new State(55, 55);
            var x80y70 = new State(80, 70);
            var x60y150 = new State(60, 150);
            var x65y110 = new State(65, 110);
            var x70y85 = new State(70, 85);
            var x25y100 = new State(25, 100);
            var x50y90 = new State(50, 90);
            var x35y120 = new State(35, 120);

            var states = new List<State>();
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
                new Action(x70y85, "Noerregade", x80y70)
                    
                    };
            

            AStarSearcher.Search(states, actions, startState, endState);
        
        }

        private void initialize()
        {
            
        }
    }
}
