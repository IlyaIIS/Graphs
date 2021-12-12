using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsLogic
{
    public static class SearchAlgorithms
    {
        public static IEnumerable<bool> BreadthFirst(Graph graph, Node firstNode)
        {
            SetFlagsToZero(graph);

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
                                link.Node.Flag = -1;
                            }
                        }

                        node.Flag = 2;
                        passedNodesNum++;
                    }
                }

                foreach(Node node in graph.Nodes)
                {
                    if (node.Flag == -1)
                        node.Flag = 1;
                }

                yield return true;
            } while (passedNodesNum != lastPassedNodesNum);
            yield break;
        }
        public static IEnumerable<Queue<Node>> DepthFirst(Graph graph, Node firstNode)
        {
            SetFlagsToZero(graph);

            Queue<Node> QNodes = new Queue<Node>();
            firstNode.Flag = 2;
            foreach(var node in graph.Nodes)
            {
                yield return QNodes;
                QNodes.Enqueue(node);
                DepthFirst(graph, node);
            }
            yield return QNodes;
        }

        public static void SetFlagsToZero(Graph graph)
        {
            foreach (var node in graph.Nodes)
                node.Flag = 0;
        }
    }
}
