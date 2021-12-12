using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using GraphsLogic;
using System.Text;
using System.Threading;

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
        static bool wasMiddlePressed = false;
        static Node PickedNode = null;
        public static Vector2 MiddlePressedPisition;

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
            MiddleMouseCamDragActions();

            Button button = Menu.GetButtonUnderPoint(mouseState.Position);

            if (button == null)
            {
                if (Menu.Buttons[ButtonType.BreadthFirst].IsActive)
                {
                    LeftMouseAlgorithmsActions();
                }
                else if (Menu.Buttons[ButtonType.Removing].IsActive)
                {
                    LeftMouseNodeRemovingActions();
                    RightMouseLineRemovingActions();
                }
                else
                {
                    LeftMouseNodeAddActions();
                    RightMouseLineAddActions();
                }
            }
            else
            {
                LeftMouseButtonClickActions();
            }

            void LeftMouseNodeAddActions()
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
                            foreach (Node subNode in graph.Nodes)
                            {
                                foreach (Line line in subNode.Lines)
                                {
                                    if (line.To == node)
                                    {
                                        subNode.IsUnderUpdating = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        PickedNode.Position = GetMousePosition();
                        if (Menu.Buttons[ButtonType.BreadthFirst].IsActive == true)
                            StartBreadthFirst(PickedNode);
                        else if (Menu.Buttons[ButtonType.DepthFirst].IsActive == true)
                            StartDepthFirst(PickedNode);
                    }
                }
                else if (wasLeftPressed && mouseState.LeftButton == ButtonState.Released)
                {
                    wasLeftPressed = false;

                    if (PickedNode == null)
                        graph.AddNode(GetMousePosition());
                    else
                        foreach (Node node in graph.Nodes)
                            node.IsUnderUpdating = false;

                    PickedNode = null;
                }
            }
            void RightMouseLineAddActions()
            {
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
                else if (wasRightPressed && mouseState.RightButton == ButtonState.Released)
                {
                    wasRightPressed = false;

                    Node node = GetNodeUnderMouse(graph);

                    if (PickedNode != null && node != null && PickedNode != node)
                    {
                        bool lineAlreadyExists = false;
                        foreach(Line line in PickedNode.Lines)
                        {
                            if (line.To == node)
                            {
                                lineAlreadyExists = true;
                                break;
                            }
                        }

                        if (!lineAlreadyExists)
                            graph.AddLink(PickedNode, node, Menu.Buttons[ButtonType.LineType].IsActive);
                    }

                    PickedNode = null;
                }
            }
            void MiddleMouseCamDragActions()
            {
                if (mouseState.MiddleButton == ButtonState.Pressed)
                {
                    wasMiddlePressed = true;

                    if (MiddlePressedPisition != Vector2.Zero)
                        Cam.Move(MiddlePressedPisition - GetMousePosition());

                    MiddlePressedPisition = GetMousePosition();
                }
                else if (wasMiddlePressed && mouseState.MiddleButton == ButtonState.Released)
                {
                    wasMiddlePressed = false;

                    MiddlePressedPisition = Vector2.Zero;
                }
            }

            void LeftMouseNodeRemovingActions()
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    wasLeftPressed = true;

                    if (PickedNode == null)
                    {
                        Node node = GetNodeUnderMouse(graph);

                        if (node != null)
                            PickedNode = node;
                    }
                }
                else if (wasLeftPressed && mouseState.LeftButton == ButtonState.Released)
                {
                    wasLeftPressed = false;

                    Node node = GetNodeUnderMouse(graph);

                    if (PickedNode != null && node != null)
                        if (PickedNode == node)
                            graph.RemoveNode(node);

                    PickedNode = null;
                }
            }
            void RightMouseLineRemovingActions()
            {
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
                else if (wasRightPressed && mouseState.RightButton == ButtonState.Released)
                {
                    wasRightPressed = false;

                    Node node = GetNodeUnderMouse(graph);

                    if (PickedNode != null && node != null)
                        if (PickedNode != node)
                            graph.RemoveLine(PickedNode, node, Menu.Buttons[ButtonType.LineType].IsActive);

                    PickedNode = null;
                }
            }

            void LeftMouseButtonClickActions()
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    wasLeftPressed = true;
                }
                else if (wasLeftPressed && mouseState.LeftButton == ButtonState.Released)
                {
                    wasLeftPressed = false;

                    button.TryClick(mouseState.Position);
                    //if (button == Menu.Buttons[ButtonType.BreadthFirst])
                        //SearchAlgorithms.SetFlagsToZero(graph.GetOrigin());
                }
            }

            void LeftMouseAlgorithmsActions()
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    wasLeftPressed = true;
                }
                else if (wasLeftPressed && mouseState.LeftButton == ButtonState.Released)
                {
                    wasLeftPressed = false;

                    Node node = GetNodeUnderMouse(graph);

                    if (node != null)
                        StartBreadthFirst(node);
                }
            }
            void StartBreadthFirst(Node node)
            {
                Thread t = new Thread(new ThreadStart(() => {
                    var algorithm = SearchAlgorithms.BreadthFirst(graph.GetOrigin(), node.Origin).GetEnumerator();
                    while (algorithm.MoveNext())
                    {
                        Thread.Sleep(1000);
                    }

                    Menu.Buttons[ButtonType.BreadthFirst].Click();
                    SearchAlgorithms.SetFlagsToZero(graph.GetOrigin());
                }));
                t.Start();
            }
            void StartDepthFirst(Node node)
            {
                Thread t = new Thread(new ThreadStart(() => {
                    var algorithm = SearchAlgorithms.DepthFirst(graph.GetOrigin(), node.Origin).GetEnumerator();
                    while (algorithm.MoveNext())
                    {
                        Thread.Sleep(1000);
                    }

                    Menu.Buttons[ButtonType.DepthFirst].Click();
                    SearchAlgorithms.SetFlagsToZero(graph.GetOrigin());
                }));
                t.Start();
            }
        }

        static private Vector2 GetMousePosition()
        {
            return new Vector2(mouseState.X / Cam.Zoom + Cam.Pos.X - (Cam.ViewportWidth / 2)/Cam.Zoom, mouseState.Y / Cam.Zoom + Cam.Pos.Y - (Cam.ViewportHeight / 2) / Cam.Zoom);
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
                if (GetPointDistance(GetMousePosition(), node.Position) <= graph.NodeSize / 2)
                {
                    return node;
                }
            }
            return null;
        }

        public static double GetPointDistance(Vector2 point1, Vector2 point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }
        public static float GetPointDirection(Vector2 point1, Vector2 point2)
        {
            var radian = Math.Atan2(-(point2.Y - point1.Y), (point2.X - point1.X));
            radian = (radian < 0) ? radian + Math.PI * 2 : radian;
            radian = Math.PI * 2 - radian;
            return (float)(radian);
        }
    }
}
