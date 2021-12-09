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
        private event Action click;

        public Button(Point pos, float size, Texture2D[] sprites, Action _click, bool isActive = false)
        {
            Position = pos;
            Size = size;
            Sprites = sprites;
            click += _click;
            IsActive = isActive;

            click += () => { IsActive = !IsActive; };
        }

        public bool TryClick(Point mousePos)
        {
            if (IsUnderPoint(mousePos))
            {
                click.Invoke();
                return true;
            }
            return false;
        }

        public void Click()
        {
            click.Invoke();
        }

        public bool IsUnderPoint(Point pos)
        {
            if (Position.X < pos.X && Position.X + Size > pos.X &&
                Position.Y < pos.Y && Position.Y + Size > pos.Y)
            {
                return true;
            }
            return false;
        }
    }
}
