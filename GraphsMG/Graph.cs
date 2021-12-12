﻿using System;
using System.Collections.Generic;
using System.IO;
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

        public void AddNode(Vector2 position)
        {
            GraphsLogic.Node newNode = new GraphsLogic.Node(origin.Nodes.Count);
            origin.AddNode(newNode);
            Nodes.Add(new Node(newNode, position, NodeSize));
        }
        public GraphsLogic.Graph GetOrigin()
        {
            return origin;
        }
        public void AddLink(Node firstNode, Node secondNode, bool isBiderectional, double value = 1)
        {
            origin.AddLink(firstNode.Origin, secondNode.Origin, value, isBiderectional ? LinkType.Bidirectional : LinkType.Unidirectional);
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
                RemoveAllLines(node);
                //RemoveLine(neig, node);
            }

            node.Lines.Clear();

            origin.RemoveNode(node.Origin);
        }

        public void RemoveAllLines(Node mainNode)
        {
            foreach (var node in Nodes)
            {
                for (var i = 0; i < node.Lines.Count; i++)
                {
                    if(node.Lines[i].To == mainNode || node.Lines[i].From == mainNode ||
                        node.Lines[i].To is null || node.Lines[i].From is null)
                        node.Lines.RemoveAt(i);
                }
            }
            origin.RemoveAllLinks(mainNode.Origin);
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

        public void Save(string path)
        {
            CsvMatrix.SaveGraph(origin, path);
        }

        public void Load(string path)
        {
            if (File.Exists(path))
                Update(CsvMatrix.GetGraph(path));
        }

        public void Update(GraphsLogic.Graph graph)
        {
            Nodes = new List<Node>();
            origin = graph;
            Random rnd = new Random();

            int lineLenght = (int)Math.Floor(Math.Sqrt(origin.Nodes.Count) +1);
            for (int i = 0; i < origin.Nodes.Count; i++)
            {
                GraphsLogic.Node originNode = origin.Nodes[i];
                Nodes.Add(new Node(originNode, new Vector2((float)(NodeSize * 3 * (i % lineLenght) + rnd.Next((int)NodeSize*2)-NodeSize), (float)(NodeSize * 3 * (i / lineLenght) + rnd.Next((int)NodeSize*2) - NodeSize)), NodeSize));
            }

            for (int i = 0; i < origin.Nodes.Count; i++)
            {
                GraphsLogic.Node originNode = origin.Nodes[i];
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
