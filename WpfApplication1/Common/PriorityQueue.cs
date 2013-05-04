using System;
using System.Collections.Generic;

namespace Heureka.Common
{
    public class PriorityQueue<T> : List<T> where T : IComparable
    {
        public new void Add(T item)
        {
            base.Add(item);
            this.Sort((item1, item2) => item1.CompareTo(item2));
        }

        public T Pop()
        {
            var item = this[0];
            this.Remove(item);
            return item;
        }
    }
}