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
            // TODO: Add your initialization logic here
            Graph = new Graph();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Printer.NodeSp = Content.Load<Texture2D>("Circle");
            Printer.LineSp = Content.Load<Texture2D>("Point");
            Printer.ArrowSp = Content.Load<Texture2D>("Arrow");

            Menu.Initialize(Graph, new Dictionary<ButtonType , Texture2D[]> {
                { ButtonType.LineType, new Texture2D[2] { Content.Load<Texture2D>("ButtonLink1"), Content.Load<Texture2D>("ButtonLink2") } },
                { ButtonType.Removing, new Texture2D[2] { Content.Load<Texture2D>("ButtonRemove1"), Content.Load<Texture2D>("ButtonRemove2") } },
                { ButtonType.Saving, new Texture2D[2] { Content.Load<Texture2D>("ButtonSave"), Content.Load<Texture2D>("ButtonSave") } },
                { ButtonType.Loading, new Texture2D[2] { Content.Load<Texture2D>("ButtonLoad"), Content.Load<Texture2D>("ButtonLoad") } }
            });

            Printer.Font = Content.Load<SpriteFont>("defaultFont");
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (IsActive)
                Controller.CheckKeyActions(Graph);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Camera.get_transformation(GraphicsDevice));

            Printer.DrawGraph(_spriteBatch, Graph);
            
            _spriteBatch.End();


            _spriteBatch.Begin();

            Printer.DrawTechInf(Graph);
            Printer.DrawMenu();

            _spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}
