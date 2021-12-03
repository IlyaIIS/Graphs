using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsLogic
{
    public class Graph
    {
        public List<Node> Nodes { get; } = new List<Node>();

        public Graph()
        {

        }

        public void AddLink(Node first, Node second, double value = 0, LinkType type = LinkType.Bidirectional)
        {
            first.Links.Add(new Link(second, value));
            if (type == LinkType.Bidirectional)
                second.Links.Add(new Link(first, value));
        }
    }

    public enum LinkType
    {
        Bidirectional, 
        Unidirectional
    }
}
