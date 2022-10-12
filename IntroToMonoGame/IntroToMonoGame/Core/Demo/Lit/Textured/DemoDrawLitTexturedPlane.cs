using GD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// Exercise using VertexPositionNormalTexture - Draw a textured lit plane

namespace IntroToMonoGame.Core.Demo.Lit
{
    public class DemoDrawLitTexturedPlane
    {
        private GraphicsDevice graphicsDevice;
        private VertexPositionNormalTexture[] verts;

        public DemoDrawLitTexturedPlane(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            InitializeVertices();
        }

        public void InitializeVertices()
        {
            verts = new VertexPositionNormalTexture[] {
             new VertexPositionNormalTexture(
                new Vector3(-1, 1, 0), Vector3.UnitZ, new Vector2(0, 0)),
            new VertexPositionNormalTexture(
               new Vector3(1, 1, 0), Vector3.UnitZ, new Vector2(1, 0)),
            new VertexPositionNormalTexture(
              new Vector3(-1, -1, 0), Vector3.UnitZ, new Vector2(0, 1)),
            new VertexPositionNormalTexture(
             new Vector3(1, 1, 0), Vector3.UnitZ, new Vector2(1, 0)),
            new VertexPositionNormalTexture(
               new Vector3(1, -1, 0), Vector3.UnitZ, new Vector2(1, 1)),
            new VertexPositionNormalTexture(
              new Vector3(-1, -1, 0), Vector3.UnitZ, new Vector2(0, 1))
        };
        }

        public void Draw(Matrix world,
            BasicEffect effect, Camera camera, Texture2D texture)
        {
            effect.World = world;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.Texture = texture;

            effect.CurrentTechnique.Passes[0].Apply();

            graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, verts, 0, 2);
        }
    }
}