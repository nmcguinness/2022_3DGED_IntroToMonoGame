using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace GD
{
    public class DemoVertexIndexBuffer
    {
        private int primitiveCount;
        private VertexPositionColor[] vertices;
        private short[] indices;

        public DemoVertexIndexBuffer()
        {
            InitializeVertices();
        }

        private void InitializeVertices()
        {
            primitiveCount = 8;

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
        }

        public void Draw(GraphicsDevice graphicsDevice,
            BasicEffect effect,
            Matrix world,
            Matrix view, Matrix projection)
        {
            effect.World = world;
            effect.View = view;
            effect.Projection = projection;
            effect.CurrentTechnique.Passes[0].Apply();

            //do the draw here!!!!
            graphicsDevice.DrawUserIndexedPrimitives(
                PrimitiveType.LineList,
            vertices, 0, vertices.Length, indices, 0, primitiveCount);
        }
    }
}