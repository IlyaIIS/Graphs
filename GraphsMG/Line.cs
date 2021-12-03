using System;
using System.Collections.Generic;
using System.Text;
using GraphsLogic;
using Microsoft.Xna.Framework;

namespace GraphsMG
{
    class Line
    {
        public double Value { get; }
        public float Length { get; private set; }
        public float Angle { get; private set; }
        public Node From { get; }
        public Node To { get; }

        public Line(Node from, Node to, double value = 1)
        {
            Length = (float)Controller.GetPointDistance(from.Position, to.Position);
            Angle = Controller.GetPointDirection(from.Position, to.Position);
            From = from;
            To = to;
            Value = value;
        }
        public void Update()
        {
            Length = (float)Controller.GetPointDistance(From.Position, To.Position);
            Angle = Controller.GetPointDirection(From.Position, To.Position);
        }
    }
}
