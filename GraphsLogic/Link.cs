using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsLogic
{
    public class Link
    {
        public Node Node { get; set; }
        public double Value { get; set; } 

        public Link(Node node, double value = 1)
        {
            Node = node;
            Value = value;
        }
    }
}
