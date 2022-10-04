using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace GD
{
    public class DemoDrawUserPrimitives
    {
        private GraphicsDevice graphicsDevice;
        private VertexPositionColor[] verts;

        public DemoDrawUserPrimitives(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            InitializeVertices();
        }

        public void InitializeVertices()
        {
            verts = new VertexPositionColor[] {
                new VertexPositionColor(new Vector3(0, 1, 0), Color.Red),
                new VertexPositionColor(new Vector3(1, 0, 0), Color.Green),
                new VertexPositionColor(new Vector3(-1, 0, 0), Color.Blue),
            };
        }

        public void Draw(Matrix world,
            BasicEffect effect, Camera camera)
        {
            effect.World = world;
            effect.View = camera.View;
            effect.Projection = camera.Projection;

            effect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawUserPrimitives(
                PrimitiveType.TriangleList, verts, 0, 1);
        }
    }
}