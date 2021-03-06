using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GraphsLogic
{
    public static class CsvMatrix
    {
        private static int[,] GetMatrix(string path)
        {
            string[] lines = File.ReadAllLines(path = Directory.GetCurrentDirectory() + @"\matrix.csv");
            int[,] matrix = new int[lines.Length, lines.Length];
            for(int i = 0; i < lines.Length; i++)
            {
                int j = 0;
                lines[i] = lines[i].Replace(" ", "");
                string temp = "";
                foreach (char c in lines[i])
                {
                    if( c == ';')
                    {
                        matrix[i, j] = Int32.Parse(temp);
                        temp = string.Empty;
                        j++;
                        continue;
                    }
                    else
                    {
                        temp += c;
                    }
                }
            }
            return matrix;
        }

        public static void WriteMatrix(string path, double[,] matrix)
        {
            File.Delete(path);
            for(int i = 0; i < Math.Sqrt(matrix.Length); i++)
            {
                string temp = string.Empty;
                for(int j = 0; j < Math.Sqrt(matrix.Length); j++)
                {
                    temp += matrix[i,j] + ";";
                }
                temp += "\n";
                File.AppendAllText(path, temp);
            }
        }

        public static Graph GetGraph(string path)
        {
            int[,] matrix = GetMatrix(path);
            Graph graph = new Graph();
            for(int i = 0; i < Math.Sqrt(matrix.Length); i++)
                graph.Nodes.Add(new Node(i));
            for(int i = 0; i < Math.Sqrt(matrix.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(matrix.Length); j++)
                {
                    if (matrix[i, j] == 0)
                        continue;
                    graph.AddLink(graph.Nodes[i], graph.Nodes[j], matrix[i,j], LinkType.Unidirectional);
                }
            }
            return graph;
        }

        public static void SaveGraph(Graph graph, string path)
        {
            double[,] matrix = new double[graph.Nodes.Count, graph.Nodes.Count];
            
            foreach(Node node in graph.Nodes)
            {
                foreach(Link link in node.Links)
                {
                    matrix[node.Id, link.Node.Id] = link.Value;
                }
            }

            WriteMatrix(path, matrix);
        }  
    }
}
