using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsLogic
{
    public static class SearchAlgorithms
    {
        public static IEnumerable<Queue<Node>> BreadthFirst(Graph graph, Node firstNode)
        {
            Queue<Node> QNodes = new Queue<Node>();
            firstNode.Passed = true;
            foreach (var link in firstNode.Links)
            {
                if(!link.Node.Passed)
                {
                    QNodes.Enqueue(link.Node);
                    link.Node.Passed = true;
                    //yield return QNodes;
                }
            }
            while (QNodes.Count > 0)
                BreadthFirst(graph, QNodes.Dequeue());
            yield return QNodes;
        }
        public static IEnumerable<Queue<Node>> DepthFirst(Graph graph, Node firstNode)
        {
            Queue<Node> QNodes = new Queue<Node>();
            firstNode.Passed = true;
            foreach(var node in graph.Nodes)
            {
                //yield return QNodes;
                QNodes.Enqueue(node);
                DepthFirst(graph, node);
            }
            yield return QNodes;
        }

        public static void SetToZero(Graph graph)
        {
            foreach (var node in graph.Nodes)
                node.Passed = false;
        }
    }
}
