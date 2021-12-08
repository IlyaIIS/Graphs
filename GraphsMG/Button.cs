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
        public int State { get; set; }
        public Texture2D[] Sprites { get; set; } = new Texture2D[2];
    }
    /*
     * two buttons - link button (Bidirectional, Unidirectional) and removing button (active and inactive)
     */
    enum LinButtonState
    {

    }
}
