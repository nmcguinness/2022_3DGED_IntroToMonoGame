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
            SetGraphics(640, 480, false);

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
            view = Matrix.CreateLookAt(new Vector3(0, 0, 5),
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