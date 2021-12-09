using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsMG
{
    class Button
    {
        public Point Position { get; }
        public float Size { get; }
        public bool IsActive { get; private set; }
        public Texture2D[] Sprites { get; set; } = new Texture2D[2];
        private event Action Click;

        public Button(Point pos, float size, Texture2D[] sprites, Action click, bool isActive = false)
        {
            Position = pos;
            Size = size;
            Sprites = sprites;
            Click += click;
            IsActive = isActive;

            Click += () => { IsActive = !IsActive; };
        }

        public bool TryClick(Point mousePos)
        {
            if (Position.X < mousePos.X && Position.X + Size > mousePos.X && 
                Position.Y < mousePos.Y && Position.Y + Size > mousePos.Y)
            {
                Click.Invoke();
                return true;
            }
            return false;
        }
    }
}
