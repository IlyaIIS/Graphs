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
        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }
        public void AddLink(Node first, Node second, int value = 1, LinkType type = LinkType.Bidirectional)
        {
            first.Links.Add(new Link(second, value));
            if (type == LinkType.Bidirectional)
                second.Links.Add(new Link(first, value));
        }

        public void RemoveNode(Node node)
        {
            Nodes.Remove(node);

            foreach (Link link in node.Links)
            {
                Node neig = link.Node;
                RemoveLink(neig, node);
            }

            node.Links.Clear();
        }
        public void RemoveLink(Node from, Node to, bool bothDirections = false)
        {
            for (int i = 0; i < from.Links.Count; i++)
            {
                if (from.Links[i].Node == to)
                {
                    from.Links.RemoveAt(i);
                    break;
                }
            }

            if (bothDirections)
                RemoveLink(to, from);
        }
    }

    public enum LinkType
    {
        Bidirectional, 
        Unidirectional
    }
}
