using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsLogic
{
    public class Node
    {
        public List<Link> Links { get; } = new List<Link>();
        public double Value { get; set; }

        public Node(double value = 0)
        {
            Value = value;
        }
    }
}
