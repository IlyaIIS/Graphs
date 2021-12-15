using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsLogic
{
    public class Graph
    {
        public List<Node> Nodes { get; } = new List<Node>();
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
            node.Links.Clear();

            foreach (Node subNode in Nodes)
            {
                foreach (Link link in subNode.Links)
                {
                    if (link.Node == node)
                    {
                        RemoveLink(subNode, node);
                        break;
                    }
                }
            }

            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].Id = i;
            }
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
