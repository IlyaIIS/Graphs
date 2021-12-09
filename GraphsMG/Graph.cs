using System;
using System.Collections.Generic;
using System.Text;
using GraphsLogic;
using Microsoft.Xna.Framework;

namespace GraphsMG
{
    class Graph
    {
        private GraphsLogic.Graph origin;
        public List<Node> Nodes = new List<Node>();
        public float NodeSize { get; } = 20;

        public Graph()
        {
            origin = new GraphsLogic.Graph();
        }

        public void AddNode(Point position, double value = 1)
        {
            GraphsLogic.Node newNode = new GraphsLogic.Node(value);
            origin.AddNode(newNode);
            Nodes.Add(new Node(newNode, position, NodeSize));
        }

        public void AddLink(Node firstNode, Node secondNode, bool isBiderectional, double value = 0)
        {
            origin.AddLink(firstNode.Origin, secondNode.Origin, value);
            firstNode.Lines.Add(new Line(firstNode, secondNode, value));
            if (isBiderectional)
                secondNode.Lines.Add(new Line(secondNode, firstNode, value));
        }

        public void RemoveNode(Node node)
        {
            Nodes.Remove(node);

            foreach (Line line in node.Lines)
            {
                Node neig = line.To;
                RemoveLine(neig, node);
            }

            node.Lines.Clear();

            origin.RemoveNode(node.Origin);
        }

        public void RemoveLine(Node from, Node to, bool bothDirections = false)
        {
            for (int i = 0; i < from.Lines.Count; i++)
            {
                if (from.Lines[i].To == to)
                {
                    from.Lines.RemoveAt(i);
                    break;
                }
            }
            origin.RemoveLink(from.Origin, to.Origin);

            if (bothDirections)
                RemoveLine(to, from);
        }
    }
}
