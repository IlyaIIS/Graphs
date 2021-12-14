using System;
using System.Collections.Generic;
using System.Text;
using GraphsLogic;
using Microsoft.Xna.Framework;

namespace GraphsMG
{
    class Line
    {
        public int Value { get; }
        public float Length { get; private set; }
        public float Angle { get; private set; }
        public Node From { get; }
        public Node To { get; }

        public Line(Node from, Node to, int value = 1)
        {
            Length = (float)Controller.GetPointDistance(from.Position, to.Position) - (to.Size + 6) / 2;
            Angle = Controller.GetPointDirection(from.Position, to.Position);
            From = from;
            To = to;
            Value = value;
        }
        public void Update()
        {
            Length = (float)Controller.GetPointDistance(From.Position, To.Position) - (To.Size + 6) / 2;
            Angle = Controller.GetPointDirection(From.Position, To.Position);
        }
    }
}
