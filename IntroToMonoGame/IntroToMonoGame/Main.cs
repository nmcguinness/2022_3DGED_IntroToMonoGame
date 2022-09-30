using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GD
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private VertexPositionColor[] vertices;
        private float rotZ;
        private Matrix world;
        private Matrix view;
        private Matrix projection;
        private BasicEffect effect;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private void SetGraphics(
            int width, int height, bool isMouseVisible)
        {
            //calling set property
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            IsMouseVisible = isMouseVisible;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            SetGraphics(1024, 768, false);

            //vertices using a specific vertex type
            vertices = new VertexPositionColor[]
                {
                    //pyramid base - front
                    new VertexPositionColor(new Vector3(-1,0,1), Color.Red),
                    new VertexPositionColor(new Vector3(1,0,1), Color.Red),

                    //pyramid base - back
                    new VertexPositionColor(new Vector3(-1,0,-1), Color.Blue),
                    new VertexPositionColor(new Vector3(1,0,-1), Color.Blue),

                     //pyramid base - left
                    new VertexPositionColor(new Vector3(-1,0,1), Color.Green),
                    new VertexPositionColor(new Vector3(-1,0,-1), Color.Green),

                    //pyramid base - right
                    new VertexPositionColor(new Vector3(1,0,1), new Color(255,255, 0)),
                    new VertexPositionColor(new Vector3(1,0,-1), new Color(255,255, 0)),

                    //pyramid uprights - FL
                    new VertexPositionColor(new Vector3(-1,0,1), Color.White),
                    new VertexPositionColor(new Vector3(0,1,0), Color.White),

                    //pyramid uprights - FR
                    new VertexPositionColor(new Vector3(1,0,1), Color.White),
                    new VertexPositionColor(new Vector3(0,1,0), Color.White),

                    //pyramid uprights - BL
                    new VertexPositionColor(new Vector3(-1,0,-1), Color.Black),
                    new VertexPositionColor(new Vector3(0,1,0), Color.Black),

                     //pyramid uprights - BR
                    new VertexPositionColor(new Vector3(1,0,-1), Color.Black),
                    new VertexPositionColor(new Vector3(0,1,0), Color.Black),
                };

            //world
            rotZ = 0;
            world = Matrix.Identity * Matrix.CreateRotationZ(rotZ);

            //view
            view = Matrix.CreateLookAt(new Vector3(0, 2, 2),
                Vector3.Zero, Vector3.UnitY);

            //projection
            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver2, 640.0f / 480, 0.1f, 100);

            //effect
            effect = new BasicEffect(_graphics.GraphicsDevice);
            effect.VertexColorEnabled = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //    System.Diagnostics.Debug.WriteLine("LoadContent...");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            rotZ += MathHelper.ToRadians(1);
            world = Matrix.Identity
                //  * Matrix.CreateRotationX(rotZ / 2)
                * Matrix.CreateRotationY(rotZ);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            effect.World = world;
            effect.View = view;
            effect.Projection = projection;

            //setting the W, V, P variables for the draw frame
            effect.CurrentTechnique.Passes[0].Apply();

            //pass vertices
            _graphics.GraphicsDevice.
                DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.LineList, vertices, 0, 8);

            base.Draw(gameTime);
        }
    }
}

/*
 namespace GD
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private VertexPositionColor[] vertices;
        private float rotZ;
        private Matrix world;
        private Matrix view;
        private Matrix projection;
        private BasicEffect effect;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private void SetGraphics(
            int width, int height, bool isMouseVisible)
        {
            //calling set property
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            IsMouseVisible = isMouseVisible;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            SetGraphics(1024, 768, false);

            //vertices using a specific vertex type
            vertices = new VertexPositionColor[]
                {
                    new VertexPositionColor(new Vector3(-1,0,0), Color.Red),
                    new VertexPositionColor(new Vector3(1,0,0), Color.Blue),

                    new VertexPositionColor(new Vector3(0,1,0), Color.Green),
                    new VertexPositionColor(new Vector3(0,-1,0), Color.Orange),
                };

            //world
            rotZ = 0;
            world = Matrix.Identity * Matrix.CreateRotationZ(rotZ);

            //view
            view = Matrix.CreateLookAt(new Vector3(0, 0, 2),
                Vector3.Zero, Vector3.UnitY);

            //projection
            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver2, 640.0f / 480, 0.1f, 100);

            //effect
            effect = new BasicEffect(_graphics.GraphicsDevice);
            effect.VertexColorEnabled = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //    System.Diagnostics.Debug.WriteLine("LoadContent...");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            rotZ += MathHelper.ToRadians(1);
            world = Matrix.Identity * Matrix.CreateRotationZ(rotZ);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            effect.World = world;
            effect.View = view;
            effect.Projection = projection;

            //setting the W, V, P variables for the draw frame
            effect.CurrentTechnique.Passes[0].Apply();

            //pass vertices
            _graphics.GraphicsDevice.
                DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.LineList, vertices, 0, 2);

            base.Draw(gameTime);
        }
    }
}
 */