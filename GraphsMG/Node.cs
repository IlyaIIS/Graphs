using System;
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
        public double Value { get { return Origin.Value; } set { Origin.Value = Value; } }
        public List<Line> Lines { get; } = new List<Line>();
        public bool IsUnderUpdating { get; set; }

        public Node(GraphsLogic.Node node, Point positon)
        {
            Position = positon;
            Origin = node;
            Color = Color.White;
        }

        public void EndOfReplacing()
        {
            foreach(Line line in Lines)
            {
                IsUnderUpdating = false;
                line.Update();
                line.To.IsUnderUpdating = false;

                foreach (Line subLine in line.To.Lines)
                    subLine.Update();
            }
        }
    }
}
