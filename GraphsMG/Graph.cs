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
            origin.Nodes.Add(newNode);
            Nodes.Add(new Node(newNode, position, NodeSize));
        }

        public void AddLink(Node firstNode, Node secondNode, bool isBiderectional, double value = 0)
        {
            origin.AddLink(firstNode.Origin, secondNode.Origin, value);
            firstNode.Lines.Add(new Line(firstNode, secondNode, value));
            if (isBiderectional)
                secondNode.Lines.Add(new Line(secondNode, firstNode, value));
        }
    }
}
