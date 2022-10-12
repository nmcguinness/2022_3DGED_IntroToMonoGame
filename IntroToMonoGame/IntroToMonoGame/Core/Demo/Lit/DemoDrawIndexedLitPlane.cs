using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

/// Exercise using VertexPositionColorNormal - Draw a solid lit plane

namespace GD
{
    public class DemoDrawIndexedLitPlane
    {
        private GraphicsDevice graphicsDevice;
        private VertexPositionColorNormal[] verts;
        private VertexBuffer vertexBuffer;
        private short[] indices;
        private IndexBuffer indexBuffer;

        public DemoDrawIndexedLitPlane(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            InitializeVertices();
            InitializeBuffers();
        }

        public void InitializeVertices()
        {
            float halfSize = 0.5f;

            verts = new VertexPositionColorNormal[] {
                //top face
        new VertexPositionColorNormal(new Vector3(-halfSize, halfSize, halfSize),
        Color.Red, new Vector3(0,1,0)), //TFL - 0
                new VertexPositionColorNormal(new Vector3(halfSize, halfSize, halfSize),
                Color.Green, new Vector3(0,1,0)), //TRL - 1
                new VertexPositionColorNormal(new Vector3(halfSize, halfSize, -halfSize),
                Color.Blue, new Vector3(0,1,0)), //2
                   new VertexPositionColorNormal(new Vector3(-halfSize, halfSize, -halfSize),
                   Color.Yellow, new Vector3(0,1,0)), //3 etc
            };
        }

        private void InitializeBuffers()
        {
            //VPCN = 36 bytes
            //P:(x,y,z) = 3x floats = 12 bytes
            //C:(r,g,b) = 3x floats = 12 bytes
            //N:(x,y,z) = 3x floats = 12 bytes

            vertexBuffer = new VertexBuffer(graphicsDevice,
                typeof(VertexPositionColorNormal), verts.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(verts); //moving the vertices to VRAM

            indices = new short[] {
                //order of these indices affects WINDING order of triangle
                0, 3, 1, 1, 3, 2, //TOP
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