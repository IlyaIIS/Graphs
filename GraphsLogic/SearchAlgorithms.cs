using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
                                //yield return log1;
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
                string log = "current way:";
                foreach (var item in nodes)
                    log += item.Id.ToString() + ", ";
                yield return log;
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
        public static IEnumerable<string> MaximumFlow(Graph graph, Node firstNode, Node lastNode)
        {
            int flow = 0;
            List<Node> way = new List<Node>();
            firstNode.Flag = 1;
            Node currentNode = firstNode;
            way.Add(currentNode);
            while (true)
            {
                Node previousNode = currentNode;
                foreach (var link in currentNode.Links) //ищем путь
                {
                    if(link.Node.Flag != 2 && link.Value != 0)
                    {
                        currentNode = link.Node;
                        currentNode.Flag = 3;
                        way.Add(currentNode);
                        break;
                    }
                }
                if(previousNode == currentNode)
                {
                    if (way.Count > 1)
                    {
                        way[^1].Flag = 2;
                        way.RemoveAt(way.Count - 1);
                        currentNode = way[^1];
                    }
                    else
                        break;
                }
                string log = "way:";
                foreach (var item in way)
                    log += item.Id.ToString();
                yield return log;
                if(currentNode == lastNode) //вычитаем поток
                {
                    List<int> weight = new List<int>();
                    List<Link> allLinks = new List<Link>();
                    for (var i = 0; i < way.Count - 1; i++)
                    {
                        foreach (var link in way[i].Links)
                        {
                            if (link.Node == way[i + 1])
                            {
                                weight.Add(link.Value);
                                allLinks.Add(link);
                            }
                        }
                    }
                    int minWeight = weight.Min();
                    flow += minWeight;
                    foreach (var link in allLinks)
                        link.Value -= minWeight;
                    for(var i = 0; i < allLinks.Count; i++)
                    {
                        if(allLinks[i].Value == 0)
                        {
                            while (way.Count > i + 1)
                            {
                                if (way[^1].Flag == 3)
                                    way[^1].Flag = 0;
                                way.RemoveAt(way.Count - 1);
                            }    
                        }
                    }
                    currentNode = way[^1];
                    yield return "Max flow: " + flow.ToString();
                }
            }
            yield break;
        }
    }
}
