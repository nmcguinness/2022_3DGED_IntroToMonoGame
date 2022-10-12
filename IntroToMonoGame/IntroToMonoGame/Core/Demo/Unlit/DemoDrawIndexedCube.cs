using GD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

/// Exercise using Technique 4 - Draw a solid coloured cube

namespace IntroToMonoGame.Core.Demo.Unlit
{
    public class DemoDrawIndexedCube
    {
        private GraphicsDevice graphicsDevice;
        private VertexPositionColor[] verts;
        private VertexBuffer vertexBuffer;
        private short[] indices;
        private IndexBuffer indexBuffer;

        public DemoDrawIndexedCube(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            InitializeVertices();
            InitializeBuffers();
        }

        public void InitializeVertices()
        {
            float halfSize = 0.5f;

            verts = new VertexPositionColor[] {
                //top surface
        new VertexPositionColor(new Vector3(-halfSize, halfSize, halfSize), Color.Red), //TFL - 0
                new VertexPositionColor(new Vector3(halfSize, halfSize, halfSize), Color.Green), //TRL - 1
                new VertexPositionColor(new Vector3(halfSize, halfSize, -halfSize), Color.Blue), //2
                   new VertexPositionColor(new Vector3(-halfSize, halfSize, -halfSize), Color.Yellow), //3 etc

                //bottom surface
      new VertexPositionColor(new Vector3(-halfSize, -halfSize, halfSize), Color.Red), //BFL
                new VertexPositionColor(new Vector3(halfSize, -halfSize, halfSize), Color.Red), //BFR
                new VertexPositionColor(new Vector3(halfSize, -halfSize, -halfSize), Color.Red),
                   new VertexPositionColor(new Vector3(-halfSize, -halfSize, -halfSize), Color.Red)
            };
        }

        private void InitializeBuffers()
        {
            vertexBuffer = new VertexBuffer(graphicsDevice,
                typeof(VertexPositionColor), verts.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(verts); //moving the vertices to VRAM

            indices = new short[] {
                //order of these indices affects WINDING order of triangle
                0, 1, 4, 1,5,4, //FRONT
                0, 3, 1, 1, 3, 2, //TOP
                2, 3, 7, 2, 7, 6, //BACK
                3, 0, 7, 0, 4, 7, //LEFT
                1, 2, 6, 1, 6, 5, //RIGHT
                4, 5, 6, 4, 5, 7, //BOTTOM
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
                0, 0, indices.Length / 3);
        }
    }
}