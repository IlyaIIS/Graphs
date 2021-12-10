﻿using System;
using System.Collections.Generic;
using System.Text;
using GraphsLogic;
using Microsoft.Xna.Framework;

namespace GraphsMG
{
    class Node
    {
        public Point Position { get; set; }
        public Color Color { get; set; }
        public GraphsLogic.Node Origin { get; }
        public List<Line> Lines { get; } = new List<Line>();
        public bool IsUnderUpdating { get; set; }
        public float Size { get; set; }

        public Node(GraphsLogic.Node node, Point positon, float size)
        {
            Position = positon;
            Origin = node;
            Color = Color.White;
            Size = size;
        }
    }
}
