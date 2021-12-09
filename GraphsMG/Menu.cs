using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsMG
{
    static class Menu
    {
        static public Dictionary<ButtonType,Button> Buttons { get; set; } = new Dictionary<ButtonType,Button>();
        
        static public void Initialize(Dictionary<ButtonType, Texture2D[]> textures)
        {
            int size = 30;

            Buttons.Add(ButtonType.LineType, new Button(new Point(0, 0), size, textures[ButtonType.LineType], () => { }, true));
        }
    }

    public enum ButtonType
    {
        LineType,
        Removing,
        ShowingLineValues,
        ShowingNodeNames
    }
}
