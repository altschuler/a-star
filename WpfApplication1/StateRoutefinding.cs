﻿namespace Heureka
{
    public class StateRoutefinding : StateAbstract
    {
        public int X { get; set; }
        public int Y { get; set; }

        public StateRoutefinding(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return this.X + "," + this.Y;
        }

        public override bool Equals(object obj)
        {
            var other = obj as StateRoutefinding;
            return (other.X.Equals(this.X) && other.Y.Equals(this.Y));
        }

    }
}
