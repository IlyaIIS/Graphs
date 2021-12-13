using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsLogic
{
    public static class SearchAlgorithms
    {
        public static IEnumerable<bool> BreadthFirst(Graph graph, Node firstNode)
        {
            ResetFlags(graph);

            int passedNodesNum = 0;
            int lastPassedNodesNum;
            firstNode.Flag = 1;
            yield return true;
            do
            {
                lastPassedNodesNum = passedNodesNum;

                foreach (Node node in graph.Nodes)
                {
                    if (node.Flag == 1)
                    {
                        foreach (Link link in node.Links)
                        {
                            if (link.Node.Flag == 0)
                            {
                                link.Node.Flag = 3;
                            }
                        }

                        node.Flag = 2;
                        passedNodesNum++;
                    }
                }

                foreach(Node node in graph.Nodes)
                {
                    if (node.Flag == 3)
                        node.Flag = 1;
                }

                yield return true;
            } while (passedNodesNum != lastPassedNodesNum);
            yield break;
        }
        public static IEnumerable<bool> DepthFirst(Graph graph, Node firstNode)
        {
            ResetFlags(graph);
            Stack<Node> nodes = new Stack<Node>();
            firstNode.Flag = 1;
            do
            {
                foreach (var node in graph.Nodes)
                {
                    if (node.Flag == 1)
                    {
                        foreach (var link in node.Links)
                        {
                            if (link.Node.Flag == 0)
                            {
                                yield return true;
                                nodes.Push(node);
                                nodes.Push(link.Node);
                                break;
                            }
                        }
                        node.Flag = 2;
                    }
                }
                if (nodes.Count == 1)
                {
                    int linksNum = 0;
                    Node n = nodes.Peek();
                    foreach (var link in n.Links)
                    {
                        if (link.Node.Flag == 0)
                            linksNum++;
                    }
                    if (linksNum > 0)
                        nodes.Peek().Flag = 1;
                    else
                        nodes.Pop().Flag = 1;
                }
                else if (nodes.Count > 1)
                    nodes.Pop().Flag = 1;
                yield return true;
            } while (nodes.Count != 0);
            yield break;
        }

        public static void ResetFlags(Graph graph)
        {
            foreach (var node in graph.Nodes)
                node.Flag = 0;
        }
        public static void GetWay(Graph graph, Node firstNode, Node lastNode)
        {
            //
        }
    }
}
