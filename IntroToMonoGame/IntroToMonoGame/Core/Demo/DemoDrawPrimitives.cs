using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace GD
{
    public class DemoDrawPrimitives
    {
        private GraphicsDevice graphicsDevice;
        private VertexPositionColor[] verts;
        private VertexBuffer vertexBuffer;

        public DemoDrawPrimitives(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            InitializeVertices();
            InitializeBuffer();
        }

        public void InitializeVertices()
        {
            verts = new VertexPositionColor[] {
                new VertexPositionColor(new Vector3(0, 1, 0), Color.Red),
                new VertexPositionColor(new Vector3(1, 0, 0), Color.Green),
                new VertexPositionColor(new Vector3(-1, 0, 0), Color.Blue),
            };
        }

        private void InitializeBuffer()
        {
            vertexBuffer = new VertexBuffer(graphicsDevice,
                typeof(VertexPositionColor), verts.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(verts);
        }

        public void Draw(Matrix world,
            BasicEffect effect, Camera camera)
        {
            effect.World = world;
            effect.View = camera.View;
            effect.Projection = camera.Projection;

            effect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.SetVertexBuffer(vertexBuffer);
            graphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
        }
    }
}