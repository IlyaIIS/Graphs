using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphsMG
{
    static class Controller
    {
        static private KeyboardState keyState;
        static private MouseState mouseState;
        static public Camera Cam { get; set; }
        static private int preMouseScroll = 0;
        static Dictionary<Keys, bool> wasKeyPressed = new Dictionary<Keys, bool>()
        {
            { Keys.Q, false},
            { Keys.E, false},
            { Keys.R, false},
            { Keys.G, false},
        };
        static bool wasLeftPressed = false;
        static bool wasRightPressed = false;
        static Node PickedNode = null;

        static public void CheckKeyActions(Graph graph)
        {
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            CheckCamMove();
            CheckCamZoom();
            CheckMouseActions(graph);
            PerformKeyActions(graph);
        }
        static void CheckMouseActions(Graph graph)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                wasLeftPressed = true;
                if (PickedNode == null)
                {
                    Node node = GetNodeUnderMouse(graph);

                    if (node != null)
                    {
                        PickedNode = node;
                        PickedNode.IsUnderUpdating = true;
                        foreach (Line line in PickedNode.Lines)
                            line.To.IsUnderUpdating = true;
                    }
                }
                else
                {
                    PickedNode.Position = GetMousePosition();
                }
            }
            if (wasLeftPressed && mouseState.LeftButton == ButtonState.Released)
            {
                wasLeftPressed = false;
                PickedNode = null;
                Node node = GetNodeUnderMouse(graph);

                if (node == null)
                    graph.AddNode(GetMousePosition());
                else
                    node.EndOfReplacing();
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                wasRightPressed = true;

                if (PickedNode == null)
                {
                    Node node = GetNodeUnderMouse(graph);

                    if (node != null)
                        PickedNode = node;
                }
            }
            if (wasRightPressed && mouseState.RightButton == ButtonState.Released)
            {
                wasRightPressed = false;

                Node node = GetNodeUnderMouse(graph);

                if (node != null)
                    graph.AddLink(PickedNode, node);

                PickedNode = null;
            }
        }

        static private Point GetMousePosition()
        {
            return new Point((int)(mouseState.X / Cam.Zoom + Cam.Pos.X - (Cam.ViewportWidth / 2)/Cam.Zoom), (int)(mouseState.Y / Cam.Zoom + Cam.Pos.Y - (Cam.ViewportHeight / 2) / Cam.Zoom));
        }
        static void PerformKeyActions(Graph graph)
        {
            //DoActionIfKeyReleased(Keys.G, () => { field.G = field.G == 0 ? 1: 0 ; });
        }

        static void DoActionIfKeyReleased(Keys key, Action Action)
        {
            if (!keyState.IsKeyDown(key))
            {
                if (wasKeyPressed[key])
                {
                    wasKeyPressed[key] = false;
                    Action();
                }
            }
            else
            {
                wasKeyPressed[key] = true;
            }
        }
        static void CheckCamMove()
        {
            float speed = 10;

            if (keyState.IsKeyDown(Keys.Right))
                Cam.Move(new Vector2(speed, 0));
            if (keyState.IsKeyDown(Keys.Up))
                Cam.Move(new Vector2(0, -speed));
            if (keyState.IsKeyDown(Keys.Left))
                Cam.Move(new Vector2(-speed, 0));
            if (keyState.IsKeyDown(Keys.Down))
                Cam.Move(new Vector2(0, speed));
        }

        static void CheckCamZoom()
        {
            if (mouseState.ScrollWheelValue > preMouseScroll)
                Cam.Zoom += 0.1f * Cam.Zoom;
            if (mouseState.ScrollWheelValue < preMouseScroll)
                Cam.Zoom -= 0.1f * Cam.Zoom;

            preMouseScroll = mouseState.ScrollWheelValue;
        }


        static public Node GetNodeUnderMouse(Graph graph)
        {
            foreach(Node node in graph.Nodes)
            {
                if (GetPointDistance(GetMousePosition(), node.Position) <= graph.NodeSize)
                {
                    return node;
                }
            }
            return null;
        }

        public static double GetPointDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }
        public static float GetPointDirection(Point point1, Point point2)
        {
            var radian = Math.Atan2(-(point2.Y - point1.Y), (point2.X - point1.X));
            radian = (radian < 0) ? radian + Math.PI * 2 : radian;
            radian = Math.PI * 2 - radian;
            return (float)(radian);
        }
    }
}
