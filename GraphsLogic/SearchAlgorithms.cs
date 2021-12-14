using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsLogic
{
    public static class SearchAlgorithms
    {
        public static IEnumerable<string> BreadthFirst(Graph graph, Node firstNode)
        {
            ResetFlags(graph);

            List<string> passedNodes;
            List<StringBuilder> activatedNodes;

            int passedNodesNum = 0;
            int lastPassedNodesNum;
            firstNode.Flag = 1;
            yield return "Beginning of breadth-first search from vertice " + firstNode.Id;
            do
            {
                passedNodes = new List<string>();
                activatedNodes = new List<StringBuilder>();

                lastPassedNodesNum = passedNodesNum;

                foreach (Node node in graph.Nodes)
                {
                    if (node.Flag == 1)
                    {
                        passedNodes.Add(node.Id.ToString());
                        activatedNodes.Add(new StringBuilder());

                        foreach (Link link in node.Links)
                        {
                            if (link.Node.Flag == 0)
                            {
                                link.Node.Flag = 3;
                                activatedNodes[^1].Append(link.Node.Id + " ");
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

                StringBuilder log;
                if (passedNodes.Count > 0)
                {
                    log = new StringBuilder("Vertice ");
                    for (int i = 0; i < passedNodes.Count; i++)
                    {
                        log.Append(passedNodes[i] + " spread to " + activatedNodes[i] + "; ");
                    }
                }
                else
                {
                    log = new StringBuilder("All available vertices passed");
                }

                yield return log.ToString();
            } while (passedNodesNum != lastPassedNodesNum);
            yield break;
        }
        public static IEnumerable<string> DepthFirst(Graph graph, Node firstNode)
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
                                yield return "blabla";
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
                yield return "blabla";
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
