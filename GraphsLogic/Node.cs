﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsLogic
{
    public class Node
    {
        public List<Link> Links { get; } = new List<Link>();
        public int Id { get; }
        public Node(int id)
        {
            Id = id;
        }
    }
}
