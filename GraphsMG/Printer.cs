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

        static public void DrawGraph(SpriteBatch spriteBatch, Graph graph)
        {
            SpriteBatch = spriteBatch;
            DrawEdges(graph);
            DrawNodes(graph);
            DrawArrows(graph);
        }

        static void DrawNodes(Graph graph)
        {
            foreach (Node node in graph.Nodes)
            {
                SpriteBatch.Draw(NodeSp,
                                new Vector2(node.Position.X - graph.NodeSize/2, node.Position.Y - graph.NodeSize/2), null,
                                node.Color, 0,
                                new Vector2(0,0),
                                new Vector2(graph.NodeSize / NodeSp.Width, graph.NodeSize / NodeSp.Height),
                                SpriteEffects.None, 0);
            }

            //инфа от тайла под курсором
            {
                MouseState mouse = Mouse.GetState();
                //Tile tile = field.GetTileFromCoord(mouse.X + Controller.Cam.Pos.X - Controller.Cam.ViewportWidth/2, mouse.Y + Controller.Cam.Pos.Y - Controller.Cam.ViewportHeight / 2);

                //SpriteBatch.DrawString(Font2, Math.Round(tile.Altitude).ToString(), new Vector2((float)tile.X, (float)tile.Y), Color.Black);
                //SpriteBatch.DrawString(Font2, tile.Resources[0].Type.ToString().Substring(0, 3), new Vector2((float)tile.X, (float)tile.Y), Color.Black);
                //SpriteBatch.DrawString(Font2, tile.alt.ToString(), new Vector2((float)tile.X, (float)tile.Y), Color.Black);
            }
        }
        static void DrawEdges(Graph graph)
        {
            foreach (Node node in graph.Nodes)
            {
                foreach (Line line in node.Lines)
                {
                    if (node.IsUnderUpdating)
                        SpriteBatch.Draw(LineSp,
                                    new Vector2(node.Position.X, node.Position.Y), null,
                                    node.Color, Controller.GetPointDirection(line.From.Position, line.To.Position),
                                    new Vector2(0, 0),
                                    new Vector2((float)Controller.GetPointDistance(line.From.Position, line.To.Position), 1),
                                    SpriteEffects.None, 0);
                    else
                        SpriteBatch.Draw(LineSp,
                                        new Vector2(node.Position.X, node.Position.Y), null,
                                        node.Color, line.Angle,
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
                    if (!node.IsUnderUpdating)
                        SpriteBatch.Draw(ArrowSp,
                                        new Vector2(node.Position.X + (float)Math.Cos(line.Angle) * line.Length, node.Position.Y + (float)Math.Sin(line.Angle) * line.Length), null,
                                        node.Color, line.Angle,
                                        new Vector2(0, 15.5f),
                                        new Vector2(6f / ArrowSp.Width, 6f / ArrowSp.Height),
                                        SpriteEffects.None, 0);
                }
            }
        }

        static public void DrawTechInf(Graph graph)
        {
            MouseState mouse = Mouse.GetState();
            //Tile tile = Map.GetTileFromCoord(mouse.X, mouse.Y );

            //SpriteBatch.DrawString(Font2, tile.Altitude.ToString(), new Vector2((float)tile.X, (float)(tile.Y)), Color.Black);
            //SpriteBatch.DrawString(Font, mouse.X + " " + mouse.Y, new Vector2(mouse.X, mouse.Y - 20), Color.Black);
            //SpriteBatch.DrawString(Font2, Controller.Cam._pos.X + " " + Controller.Cam._pos.Y, new Vector2(mouse.X, mouse.Y - 20), Color.Black);
        }
    }
}
