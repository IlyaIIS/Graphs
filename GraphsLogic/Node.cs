using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsLogic
{
    public class Node
    {
        public List<Link> Links { get; } = new List<Link>();
        public int Id { get; }
        public int Flag { get; set; } // 0 - не пройденый, 1 - активный, 2 - пройденный, 3 - доп флаг
        public int WayLength { get; set; } // длина пути от исходной вершины
        public Node LastWayNode { get; set; } // предыдущий нод при поиске пути
        public Node(int id)
        {
            Id = id;
        }
    }
}
