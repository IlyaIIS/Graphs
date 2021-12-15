using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GraphsLogic;
using Microsoft.Xna.Framework;

namespace GraphsMG
{
    class Graph
    {
        public GraphsLogic.Graph Origin { get; private set; }
        public List<Node> Nodes = new List<Node>();
        public float NodeSize { get; } = 20;

        public Graph()
        {
            Origin = new GraphsLogic.Graph();
        }

        public void AddNode(Vector2 position)
        {
            GraphsLogic.Node newNode = new GraphsLogic.Node(Origin.Nodes.Count);
            Origin.AddNode(newNode);
            Nodes.Add(new Node(newNode, position, NodeSize));
        }
        public void AddLink(Node firstNode, Node secondNode, bool isBiderectional, int value = 1)
        {
            Origin.AddLink(firstNode.Origin, secondNode.Origin, value, isBiderectional ? LinkType.Bidirectional : LinkType.Unidirectional);
            firstNode.Lines.Add(new Line(firstNode, secondNode, value));
            if (isBiderectional)
                secondNode.Lines.Add(new Line(secondNode, firstNode, value));
        }

        public void RemoveNode(Node node)
        {
            Nodes.Remove(node);
            node.Lines.Clear();

            foreach(Node subNode in Nodes)
            {
                foreach(Line line in subNode.Lines)
                {
                    if (line.To == node)
                    {
                        RemoveLine(subNode, node);
                        break;
                    }
                }
            }

            Origin.RemoveNode(node.Origin);
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
            Origin.RemoveLink(from.Origin, to.Origin);

            if (bothDirections)
                RemoveLine(to, from);
        }

        public void Save(string path)
        {
            CsvMatrix.SaveGraph(Origin, path);
        }

        public void Load(string path)
        {
            if (File.Exists(path))
                Update(CsvMatrix.GetGraph(path));
        }

        public void Update(GraphsLogic.Graph graph)
        {
            Nodes = new List<Node>();
            Origin = graph;
            Random rnd = new Random();

            int lineLenght = (int)Math.Floor(Math.Sqrt(Origin.Nodes.Count) +1);
            for (int i = 0; i < Origin.Nodes.Count; i++)
            {
                GraphsLogic.Node originNode = Origin.Nodes[i];
                Nodes.Add(new Node(originNode, new Vector2((float)(NodeSize * 3 * (i % lineLenght) + rnd.Next((int)NodeSize*2)-NodeSize), (float)(NodeSize * 3 * (i / lineLenght) + rnd.Next((int)NodeSize*2) - NodeSize)), NodeSize));
            }

            for (int i = 0; i < Origin.Nodes.Count; i++)
            {
                GraphsLogic.Node originNode = Origin.Nodes[i];
                foreach(Link link in originNode.Links)
                {
                    Node toNode = GetNodeByOriginNode(link.Node);

                    Nodes[i].Lines.Add(new Line(Nodes[i], toNode, link.Value));
                }
            }
        }

        private Node GetNodeByOriginNode(GraphsLogic.Node originNode)
        {
            Node output = null;
            foreach (Node node in Nodes)
            {
                if (node.Origin == originNode)
                {
                    output = node;
                    break;
                }
            }

            return output;
        }

        public void SpreadNodes()
        {
            double speed = 1;

            foreach(Node node in Nodes)
            {
                foreach (Line line in node.Lines)
                {
                    Node subNode = line.To;

                    double distance = Controller.GetPointDistance(node.Position, subNode.Position);
                    if (distance > NodeSize * 3)
                    {
                        double mode = 0.1;//30 * (distance - NodeSize * 5) / (distance * distance);
                        float angle = Controller.GetPointDirection(node.Position, subNode.Position);

                        node.Position += new Vector2((float)(Math.Cos(angle) * speed * mode), (float)(Math.Sin(angle) * speed * mode));
                    }
                }

                foreach (Node subNode in Nodes)
                {
                    if (subNode != node)
                    {
                        double distance = Controller.GetPointDistance(node.Position, subNode.Position);
                        double mode = 30*(distance - NodeSize * 5) / (distance*distance);
                        float angle = Controller.GetPointDirection(node.Position, subNode.Position);

                        node.Position += new Vector2((float)(Math.Cos(angle)*speed * mode), (float)(Math.Sin(angle) * speed * mode));
                    }
                }
            }
        }

        public Vector2 GetGraphCenter()
        {
            Vector2 center = new Vector2();
            if (Nodes.Count > 0)
            {
                foreach (Node node in Nodes)
                {
                    center += node.Position;
                }

                center /= Nodes.Count;
            }
            return center;
        }
    }
}
