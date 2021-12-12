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
        static public Dictionary<ButtonType, Button> Buttons { get; set; } = new Dictionary<ButtonType, Button>();

        static public void Initialize(Graph graph,Dictionary<ButtonType, Texture2D[]> textures)
        {
            string path = Directory.GetCurrentDirectory() + @"\matrix.csv";

            int size = 30;

            Buttons.Add(ButtonType.LineType, new Button(new Point(0, 0), size, textures[ButtonType.LineType], () => { }, true));
            Buttons.Add(ButtonType.Removing, new Button(new Point(0, size), size, textures[ButtonType.Removing], () => { }));
            Buttons.Add(ButtonType.Saving, new Button(new Point(0, 2*size), size, textures[ButtonType.Saving], () => { graph.Save(path); }));
            Buttons.Add(ButtonType.Loading, new Button(new Point(0, 3*size), size, textures[ButtonType.Loading], () => { graph.Load(path); }));
            Buttons.Add(ButtonType.Spreading, new Button(new Point(0, 4 * size), size, textures[ButtonType.Spreading], () => { }, true));
            //сортировки
            Buttons.Add(ButtonType.BreadthFirst, new Button(new Point(0, 5 * size), size, textures[ButtonType.BreadthFirst], () => { }));
            Buttons.Add(ButtonType.DepthFirst, new Button(new Point(0, 6 * size), size, textures[ButtonType.DepthFirst], () => { }));
        }

        static public Button GetButtonUnderPoint(Point pos)
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
        DepthFirst
    }
}
