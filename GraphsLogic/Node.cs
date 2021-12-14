using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsLogic
{
    public class Node
    {
        public List<Link> Links { get; } = new List<Link>();
        public int Id { get; }
        public int Flag { get; set; } // 0 - не профденый, 1 - активный, 2 - пройденный
        public int WayLength { get; set; }
        public Node LastWayNode { get; set; }
        public Node(int id)
        {
            Id = id;
        }
    }
}
