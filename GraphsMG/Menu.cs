using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GraphsMG
{
    static class Menu
    {
        public static int Value = 1;
        public static Dictionary<ButtonType, Button> Buttons { get; set; } = new Dictionary<ButtonType, Button>();

        public static void Initialize(Graph graph,Dictionary<ButtonType, Texture2D[]> textures)
        {
            string path = Directory.GetCurrentDirectory() + @"\matrix.csv";

            var size = new Vector2(30,30);

            Buttons.Add(ButtonType.LineType, new Button(new Point(0, 0), size, textures[ButtonType.LineType], () => { }, true));
            Buttons.Add(ButtonType.Removing, new Button(new Point(0, (int)size.Y), size, textures[ButtonType.Removing], () => { }));
            Buttons.Add(ButtonType.Saving, new Button(new Point(0, 2*(int)size.Y), size, textures[ButtonType.Saving], () => { graph.Save(path); }));
            Buttons.Add(ButtonType.Loading, new Button(new Point(0, 3* (int)size.Y), size, textures[ButtonType.Loading], () => { graph.Load(path); }));
            Buttons.Add(ButtonType.Spreading, new Button(new Point(0, 4 * (int)size.Y), size, textures[ButtonType.Spreading], () => { }, true));
            Buttons.Add(ButtonType.BreadthFirst, new Button(new Point(0, 5 * (int)size.Y), size, textures[ButtonType.BreadthFirst], () => { }));
            Buttons.Add(ButtonType.DepthFirst, new Button(new Point(0, 6 * (int)size.Y), size, textures[ButtonType.DepthFirst], () => { }));
            Buttons.Add(ButtonType.GetWay, new Button(new Point(0, 7 * (int)size.Y), size, textures[ButtonType.GetWay], () => { }));
            Buttons.Add(ButtonType.Void, new Button(new Point(11, 8 * (int)size.Y), size, textures[ButtonType.Void], () => { }));
            Buttons.Add(ButtonType.Minus, new Button(new Point(0, 8 * (int)size.Y), new Vector2(10, size.Y), textures[ButtonType.Minus], () => { Value = Math.Max(1,Value-1); }));
            Buttons.Add(ButtonType.Plus, new Button(new Point(42, 8 * (int)size.Y), new Vector2(10, size.Y), textures[ButtonType.Plus], () => { Value++; }));
            Buttons.Add(ButtonType.MaxFlow, new Button(new Point(0, 9 * (int)size.Y), size, textures[ButtonType.MaxFlow], () => { }));
        }

        public static Button GetButtonUnderPoint(Point pos)
        {
            foreach (var item in Buttons)
                if (item.Value.IsUnderPoint(pos))
                    return item.Value;

            return null;
        }
    }

    public enum ButtonType
    {
        LineType,
        Removing,
        ShowingLineValues,
        ShowingNodeNames,
        Saving,
        Loading,
        Spreading,
        BreadthFirst,
        DepthFirst,
        GetWay,
        Void,
        Plus,
        Minus,
        MaxFlow
    }
}
