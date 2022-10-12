using GD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

/// Technique 2 - Passing an array of vertices and an array of indices

namespace IntroToMonoGame.Core.Demo.Unlit
{
    public class DemoDrawUserIndexedPrimitives
    {
        private GraphicsDevice graphicsDevice;
        private VertexPositionColor[] verts;
        private VertexBuffer vertexBuffer;
        private short[] indices;

        public DemoDrawUserIndexedPrimitives(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            InitializeVertices();
            InitializeBufferAndIndices();
        }

        public void InitializeVertices()
        {
            verts = new VertexPositionColor[] {
                new VertexPositionColor(new Vector3(-1, -1, 0), Color.Red),
                new VertexPositionColor(new Vector3(-1, 1, 0), Color.Green),
                new VertexPositionColor(new Vector3(1,1, 0), Color.Blue),
                   new VertexPositionColor(new Vector3(1,-1, 0), Color.Yellow)
            };
        }

        private void InitializeBufferAndIndices()
        {
            vertexBuffer = new VertexBuffer(graphicsDevice,
                typeof(VertexPositionColor), verts.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(verts);

            indices = new short[] {
                0, 1, 2, 0, 2, 3
            };
        }

        public void Draw(Matrix world,
            BasicEffect effect, Camera camera)
        {
            effect.World = world;
            effect.View = camera.View;
            effect.Projection = camera.Projection;

            effect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawUserIndexedPrimitives(
                PrimitiveType.TriangleList, verts, 0, verts.Length, indices, 0, 2);
        }
    }
}