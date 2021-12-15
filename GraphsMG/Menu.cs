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
        public static int maxId = 0;
        public static Dictionary<ButtonType, Button> Buttons { get; set; } = new Dictionary<ButtonType, Button>();

        public static void Initialize(Graph graph, Camera cam, Dictionary<ButtonType, Texture2D[]> textures)
        {
            string path = Directory.GetCurrentDirectory() + @"\matrix.csv";

            var size = new Vector2(30,30);

            Buttons.Add(ButtonType.Saving, new Button(new Point(cam.ViewportWidth - (int)size.X, 0 * (int)size.Y), size, textures[ButtonType.Saving], () => { graph.Save(path); }));
            Buttons.Add(ButtonType.Loading, new Button(new Point(cam.ViewportWidth - (int)size.X, 1 * (int)size.Y), size, textures[ButtonType.Loading], () => { graph.Load(path); }));

            Buttons.Add(ButtonType.Removing, new Button(new Point(0, 0 * (int)size.Y), size, textures[ButtonType.Removing], () => { }));
            Buttons.Add(ButtonType.LineType, new Button(new Point(0, (int)size.Y), size, textures[ButtonType.LineType], () => { }, true));
            Buttons.Add(ButtonType.Void, new Button(new Point(11, 2 * (int)size.Y), size, textures[ButtonType.Void], () => { }));
            Buttons.Add(ButtonType.Minus, new Button(new Point(0, 2 * (int)size.Y), new Vector2(10, size.Y), textures[ButtonType.Minus], () => { Value = Math.Max(1, Value - 1); }));
            Buttons.Add(ButtonType.Plus, new Button(new Point(42, 2 * (int)size.Y), new Vector2(10, size.Y), textures[ButtonType.Plus], () => { Value++; }));

            Buttons.Add(ButtonType.Spreading, new Button(new Point(0, (int)(3.5 * size.Y)), size, textures[ButtonType.Spreading], () => { }, true));
            Buttons.Add(ButtonType.ShowingNames, new Button(new Point(0, (int)(4.5 * size.Y)), size, textures[ButtonType.ShowingNames], () => { }, true));
            Buttons.Add(ButtonType.ShowingValues, new Button(new Point(0, (int)(5.5 * size.Y)), size, textures[ButtonType.ShowingValues], () => { }, true));

            Buttons.Add(ButtonType.BreadthingFirst, new Button(new Point(0, 7 * (int)size.Y), size, textures[ButtonType.BreadthingFirst], () => { }));
            Buttons.Add(ButtonType.DepthingFirst, new Button(new Point(0, 8 * (int)size.Y), size, textures[ButtonType.DepthingFirst], () => { }));
            Buttons.Add(ButtonType.GettingWay, new Button(new Point(0, 9 * (int)size.Y), size, textures[ButtonType.GettingWay], () => { }));
            Buttons.Add(ButtonType.GettingMaxFlow, new Button(new Point(0, 10 * (int)size.Y), size, textures[ButtonType.GettingMaxFlow], () => { }));
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
        Saving,
        Loading,
        Spreading,
        BreadthingFirst,
        DepthingFirst,
        GettingWay,
        Void,
        Plus,
        Minus,
        GettingMaxFlow,
        ShowingNames,
        ShowingValues
    }
}
