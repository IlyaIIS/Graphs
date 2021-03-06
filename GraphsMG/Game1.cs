using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GraphsMG
{
    class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Camera Camera = new Camera();
        public Graph Graph;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Camera.Pos = new Vector2(0f, 0f);
            Camera.Zoom = 3;
            Controller.Cam = Camera;

            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;
        }

        protected override void Initialize()
        {
            Graph = new Graph();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Printer.NodeSp = Content.Load<Texture2D>("Circle");
            Printer.LineSp = Content.Load<Texture2D>("Point");
            Printer.ArrowSp = Content.Load<Texture2D>("Arrow");

            Menu.Initialize(Graph, Camera, new Dictionary<ButtonType , Texture2D[]> {
                { ButtonType.LineType, new Texture2D[2] { Content.Load<Texture2D>("ButtonLink1"), Content.Load<Texture2D>("ButtonLink2") } },
                { ButtonType.Removing, new Texture2D[2] { Content.Load<Texture2D>("ButtonRemove1"), Content.Load<Texture2D>("ButtonRemove2") } },
                { ButtonType.Saving, new Texture2D[2] { Content.Load<Texture2D>("ButtonSave"), Content.Load<Texture2D>("ButtonSave") } },
                { ButtonType.Loading, new Texture2D[2] { Content.Load<Texture2D>("ButtonLoad"), Content.Load<Texture2D>("ButtonLoad") } },
                { ButtonType.Spreading, new Texture2D[2] { Content.Load<Texture2D>("ButtonSpread1"), Content.Load<Texture2D>("ButtonSpread2") } },
                { ButtonType.DepthingFirst, new Texture2D[2] { Content.Load<Texture2D>("ButtonDepthFirst1"), Content.Load<Texture2D>("ButtonDepthFirst2") } },
                { ButtonType.BreadthingFirst, new Texture2D[2] { Content.Load<Texture2D>("ButtonBreadthFirst1"), Content.Load<Texture2D>("ButtonBreadthFirst2") } },
                { ButtonType.GettingWay, new Texture2D[2] { Content.Load<Texture2D>("ButtonWay1"), Content.Load<Texture2D>("ButtonWay2") } },
                { ButtonType.Void, new Texture2D[2] { Content.Load<Texture2D>("ButtonVoid"), Content.Load<Texture2D>("ButtonVoid") } },
                { ButtonType.Minus, new Texture2D[2] { Content.Load<Texture2D>("ButtonMinus"), Content.Load<Texture2D>("ButtonMinus") } },
                { ButtonType.Plus, new Texture2D[2] { Content.Load<Texture2D>("ButtonPlus"), Content.Load<Texture2D>("ButtonPlus") } },
                { ButtonType.GettingMaxFlow, new Texture2D[2] { Content.Load<Texture2D>("ButtonMaxFlow1"), Content.Load<Texture2D>("ButtonMaxFlow2") } },
                { ButtonType.ShowingNames, new Texture2D[2] { Content.Load<Texture2D>("ButtonShowNames1"), Content.Load<Texture2D>("ButtonShowNames2") } },
                { ButtonType.ShowingValues, new Texture2D[2] { Content.Load<Texture2D>("ButtonShowValues1"), Content.Load<Texture2D>("ButtonShowValues2") } },
            });

            Printer.Font = Content.Load<SpriteFont>("defaultFont");
            Printer.EdgeFont = Content.Load<SpriteFont>("edgeFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            if (IsActive)
                Controller.CheckKeyActions(Graph);

            if (Menu.Buttons[ButtonType.Spreading].IsActive)
            {
                Graph.SpreadNodes();
                Vector2 center = Graph.GetGraphCenter();
                Camera.Move((center - Camera.Pos) / 100);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Camera.get_transformation(GraphicsDevice));

            Printer.DrawGraph(_spriteBatch, Graph);
            
            _spriteBatch.End();


            _spriteBatch.Begin();

            Printer.DrawMenu(Graph);

            _spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}
