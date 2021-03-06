using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsMG
{
    static class Printer
    {
        static public SpriteBatch SpriteBatch { get; set; }
        static public Texture2D NodeSp { get; set; }
        static public Texture2D LineSp { get; set; }
        static public Texture2D ArrowSp { get; set; }

        static public SpriteFont Font { get; set; }
        static public SpriteFont EdgeFont { get; set; }

        static public Color[] FlagColors = new Color[4] { Color.Green, Color.Orange, Color.Red, Color.Yellow};

        static public List<string> Log = new List<string> { " " };

        static public void DrawGraph(SpriteBatch spriteBatch, Graph graph)
        {
            SpriteBatch = spriteBatch;
            DrawEdges(graph);
            DrawNodes(graph);
            DrawArrows(graph);
            DrawEdgesValues(graph);
            DrawFlowValue(graph);
        }

        static void DrawNodes(Graph graph)
        {
            foreach (Node node in graph.Nodes)
            {
                Color color;
                if (Menu.Buttons[ButtonType.BreadthingFirst].IsActive || Menu.Buttons[ButtonType.DepthingFirst].IsActive || Menu.Buttons[ButtonType.GettingMaxFlow].IsActive || Menu.Buttons[ButtonType.GettingWay].IsActive)
                    color = FlagColors[node.Flag];
                else
                    color = Color.White;

                SpriteBatch.Draw(NodeSp,
                                new Vector2(node.Position.X - graph.NodeSize / 2, node.Position.Y - graph.NodeSize / 2), null,
                                color, 0,
                                new Vector2(0, 0),
                                new Vector2(graph.NodeSize / NodeSp.Width, graph.NodeSize / NodeSp.Height),
                                SpriteEffects.None, 0);

                if (Menu.Buttons[ButtonType.ShowingNames].IsActive)
                    SpriteBatch.DrawString(Font, node.Origin.Id.ToString(), new Vector2(node.Position.X - (node.Size/5)* node.Origin.Id.ToString().Length, node.Position.Y - node.Size / 2 + 2), Color.Black);
            }
        }
        static void DrawEdges(Graph graph)
        {
            foreach (Node node in graph.Nodes)
            {
                foreach (Line line in node.Lines)
                {
                    if (node.IsUnderUpdating || Menu.Buttons[ButtonType.Spreading].IsActive)
                        line.Update();

                    SpriteBatch.Draw(LineSp,
                                    new Vector2(node.Position.X, node.Position.Y), null,
                                    line.Color, line.Angle,
                                    new Vector2(0, 0),
                                    new Vector2(line.Length, 1),
                                    SpriteEffects.None, 0);
                }
            }
        }
        static void DrawArrows(Graph graph)
        {
            foreach (Node node in graph.Nodes)
            {
                foreach (Line line in node.Lines)
                {
                    SpriteBatch.Draw(ArrowSp,
                                    new Vector2(node.Position.X + (float)Math.Cos(line.Angle) * line.Length, node.Position.Y + (float)Math.Sin(line.Angle) * line.Length), null,
                                    line.Color, line.Angle,
                                    new Vector2(0, 15.5f),
                                    new Vector2(6f / ArrowSp.Width, 6f / ArrowSp.Height),
                                    SpriteEffects.None, 0);
                }
            }
        }
        static void DrawEdgesValues(Graph graph)
        {
            if (Menu.Buttons[ButtonType.ShowingValues].IsActive)
            {
                foreach (Node node in graph.Nodes)
                {
                    foreach (Line line in node.Lines)
                    {
                        float x = node.Position.X + line.Length * (float)Math.Cos(line.Angle) * 0.7f;
                        float y = node.Position.Y + line.Length * (float)Math.Sin(line.Angle) * 0.7f;
                        string value = line.Value.ToString();
                        if(Menu.Buttons[ButtonType.GettingMaxFlow].IsActive)
                            value += " / " + line.FlowValue.ToString();
                        SpriteBatch.DrawString(EdgeFont, value, new Vector2(x, y), Color.Black);
                    }
                }
            }
        }

        static public void DrawMenu(Graph graph)
        {
            foreach (var item in Menu.Buttons)
            {
                Button button = item.Value;
                Texture2D texture = button.Sprites[button.IsActive ? 1 : 0];
                SpriteBatch.Draw(texture,
                                new Vector2(button.Position.X, button.Position.Y), null,
                                Color.White, 0,
                                new Vector2(0, 0),
                                new Vector2(button.Size.X / texture.Width, button.Size.Y / texture.Height),
                                SpriteEffects.None, 0);
            }

            if (Menu.Buttons[ButtonType.BreadthingFirst].IsActive || Menu.Buttons[ButtonType.DepthingFirst].IsActive || Menu.Buttons[ButtonType.GettingMaxFlow].IsActive || Menu.Buttons[ButtonType.GettingWay].IsActive)
            {
                StringBuilder activeNodes = new StringBuilder();
                StringBuilder passedNodes = new StringBuilder();

                foreach (Node node in graph.Nodes)
                {
                    if (node.Flag == 1)
                        activeNodes.Append(node.Id.ToString() + "  ");
                    else if (node.Flag == 2)
                        passedNodes.Append(node.Id.ToString() + "  ");
                }

                SpriteBatch.DrawString(Font, "Active vertices: " + activeNodes.ToString(), new Vector2(20, 600), Color.Black);
                SpriteBatch.DrawString(Font, "Passed vertices: " + passedNodes.ToString(), new Vector2(20, 620), Color.Black);
                SpriteBatch.DrawString(Font, "Log: " + Log[^1], new Vector2(20, 640), Color.Black);
            }

            {
                Button button = Menu.Buttons[ButtonType.Void];
                SpriteBatch.DrawString(Font, Menu.Value.ToString(), new Vector2(button.Position.X + button.Size.X/2 - 5*Menu.Value.ToString().Length, button.Position.Y + button.Size.Y / 4), Color.Black);
            }
        }

        static public void DrawFlowValue(Graph graph)
        {
            foreach (var node in graph.Nodes)
            {
                foreach (var line in node.Lines)
                {
                    line.UpdateFlowValue();
                }
            }
        }
    }
}
    

