using GD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

/// Technique 4 - Setting an array of vertices and indices on VRAM on GPU

namespace IntroToMonoGame.Core.Demo.Unlit
{
    public class DemoDrawIndexedPrimitives
    {
        private GraphicsDevice graphicsDevice;
        private VertexPositionColor[] verts;
        private VertexBuffer vertexBuffer;
        private IndexBuffer indexBuffer;

        public DemoDrawIndexedPrimitives(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            InitializeVertices();
            InitializeBuffers();
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

        private void InitializeBuffers()
        {
            vertexBuffer = new VertexBuffer(graphicsDevice,
                typeof(VertexPositionColor), verts.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(verts); //moving the vertices to VRAM

            var indices = new short[] {
                //order of these indices affects WINDING order of triangle
                0, 1, 2, 0, 2, 3,
            };

            indexBuffer = new IndexBuffer(graphicsDevice, typeof(short),
                indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices); //moving the indices to VRAM
        }

        public void Draw(Matrix world,
            BasicEffect effect, Camera camera)
        {
            effect.World = world;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.CurrentTechnique.Passes[0].Apply();

            //we are pointing the GPU at the vbuf and ibuf
            graphicsDevice.SetVertexBuffer(vertexBuffer);
            graphicsDevice.Indices = indexBuffer;

            graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                0, 0, 2);
        }
    }
}