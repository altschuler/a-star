using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectAI.RouteFinding
{
  public class PriorityQueue<T> : List<T> where T : IComparable
  {
    public PriorityQueue() : base() {}
    
    public new void Add(T item)
    {
      base.Add(item);
      base.Sort(delegate (T item1, T item2) { return item1.CompareTo(item2); });
    }

    public T Pop()
    {
      var item = this[0];
      this.Remove(item);
      return item;
    }
  }
}