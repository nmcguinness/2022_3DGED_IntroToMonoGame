using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.WIC;

namespace GD
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private VertexPositionColor[] vertices;
        private short[] indices;
        private float rotZ;
        private Matrix world;
        private Matrix view;
        private Matrix projection;
        private BasicEffect effect;
        private RasterizerState rasterizerState;
        private Camera camera;
        private DemoVertexIndices demoVertexIndices;
        private DemoDrawUserPrimitives demoDrawUserPrimitives;
        private DemoDrawPrimitives demoDrawPrimitives;
        private DemoDrawIndexedPrimitives demoDrawIndexedPrimitives;

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

            //view
            view = Matrix.CreateLookAt(new Vector3(0, 2, 2),
                Vector3.Zero, Vector3.UnitY);

            //projection
            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver2, 640.0f / 480, 0.1f, 100);

            //effect
            effect = new BasicEffect(_graphics.GraphicsDevice);
            effect.VertexColorEnabled = true;

            //rasterizer state allows us to draw front and back using cullmode
            rasterizerState = new RasterizerState();

            //camera
            camera = new Camera(new Vector3(0, 0, 5), Vector3.Zero,
                Vector3.UnitY);

            //technique 2 - pass vertices and indices
            demoVertexIndices = new DemoVertexIndices();

            //technique 1 - pass vertices
            demoDrawUserPrimitives =
                new DemoDrawUserPrimitives(_graphics.GraphicsDevice);

            //technique 3 - set vertices on buffer
            demoDrawPrimitives =
                new DemoDrawPrimitives(_graphics.GraphicsDevice);

            //technique 4 - set vertex and index buffers. wahoo!
            demoDrawIndexedPrimitives
                = new DemoDrawIndexedPrimitives(_graphics.GraphicsDevice);

            base.Initialize();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //demoDrawPrimitives.Draw(Matrix.Identity, effect, camera);
            demoDrawIndexedPrimitives.Draw(Matrix.Identity,
                effect, camera);

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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
        private short[] indices;
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
                  //VertexPositionColor = 4xfloats (rgba), 3xfloats (xyz) => 7x4bytes = 28 bytes
                   //FL
                    new VertexPositionColor(new Vector3(-1,0,1), Color.Red),
                    //FR
                    new VertexPositionColor(new Vector3(1,0,1), Color.Green),
                    //BR
                    new VertexPositionColor(new Vector3(1,0,-1), Color.Blue),
                     //BL
                    new VertexPositionColor(new Vector3(-1,0,-1), Color.Yellow),
                     //Apex
                    new VertexPositionColor(new Vector3(0,1,0), Color.White),
                };

            indices = new short[]
            {
                0,1, //65,535
                1,2,
                2,3,
                3,0,
                //4 pairs of indices
                0,4, //FLU
                1,4, //FRU
                2,4, //BRU
                3,4 //BLU
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
        }

        protected override void Update(GameTime gameTime)
        {
            rotZ += MathHelper.ToRadians(1);
            world = Matrix.Identity * Matrix.CreateRotationY(rotZ);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            effect.World = world;
            effect.View = view;
            effect.Projection = projection;
            effect.CurrentTechnique.Passes[0].Apply();

            _graphics.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.LineList,
                vertices, 0, vertices.Length, indices, 0, 8);

            base.Draw(gameTime);
        }
    }
}
*/

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
                    //FL
                    new VertexPositionColor(new Vector3(-1,0,1), Color.Red),
                    //FR
                    new VertexPositionColor(new Vector3(1,0,1), Color.Green),
                    //BR
                    new VertexPositionColor(new Vector3(1,0,-1), Color.Blue),
                     //BL
                    new VertexPositionColor(new Vector3(-1,0,-1), Color.Yellow),
                    //FL
                    new VertexPositionColor(new Vector3(-1,0,1), Color.Red),

                    //FLU
                    new VertexPositionColor(new Vector3(0,1,0), Color.Black),

                    //BRU
                    new VertexPositionColor(new Vector3(1,0,-1), Color.White),
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

            //rotZ += MathHelper.ToRadians(1);
            //world = Matrix.Identity
            //    //  * Matrix.CreateRotationX(rotZ / 2)
            //    * Matrix.CreateRotationY(rotZ);

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
                PrimitiveType.LineStrip, vertices, 0, 6);

            base.Draw(gameTime);
        }
    }
}
 */

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

 */

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