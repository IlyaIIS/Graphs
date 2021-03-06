using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsLogic
{
    public class Link
    {
        public Node Node { get; set; }
        public int Value { get; set; } 
        public int FlowValue { get; set; }
        public bool Flag { get; set; } = true;
        public Link(Node node, int value = 1)
        {
            Node = node;
            Value = value;
            FlowValue = value;
        }
    }
}
