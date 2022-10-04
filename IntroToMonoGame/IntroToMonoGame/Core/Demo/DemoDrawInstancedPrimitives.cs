using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices;
using Color = Microsoft.Xna.Framework.Color;
using VertexBufferBinding = Microsoft.Xna.Framework.Graphics.VertexBufferBinding;

namespace GD
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexInstanceTransform : IVertexType
    {
        public readonly Matrix Transform;

        public VertexInstanceTransform(Matrix transform)
        {
            Transform = transform;
        }

        VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;

        public static readonly VertexDeclaration VertexDeclaration =
          new VertexDeclaration(
                  new VertexElement(0, VertexElementFormat.Vector4,
                                        VertexElementUsage.TextureCoordinate, 0),
                  new VertexElement(16, VertexElementFormat.Vector4,
                                         VertexElementUsage.TextureCoordinate, 1),
                  new VertexElement(32, VertexElementFormat.Vector4,
                                         VertexElementUsage.TextureCoordinate, 2),
                  new VertexElement(48, VertexElementFormat.Vector4,
                                         VertexElementUsage.TextureCoordinate, 3));
    }

    public class DemoDrawInstancedPrimitives
    {
        private GraphicsDevice graphicsDevice;
        private Effect effect;
        private EffectParameter viewProjectionParameter;
        private EffectPass effectPass;
        private VertexPositionColor[] verts;
        private VertexBuffer vertexBuffer;
        private IndexBuffer indexBuffer;
        private int transformCount;
        private DynamicVertexBuffer instanceVertexBuffer;

        public DemoDrawInstancedPrimitives(GraphicsDevice graphicsDevice, Effect effect)
        {
            this.graphicsDevice = graphicsDevice;
            this.effect = effect;

            viewProjectionParameter = effect.Parameters["ViewProjection"];
            effectPass = effect.CurrentTechnique.Passes[0];
            effect.CurrentTechnique = effect.Techniques["DrawInstanced"];

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
            int x = 200;
            int y = 200;
            transformCount = x * y;

            #region Vertices

            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), verts.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(verts);

            var indices = new short[] {
                0, 1, 2, 0, 2, 3
            };

            indexBuffer = new IndexBuffer(graphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);

            #endregion Vertices

            #region Instance Transforms

            var count = 0;
            var instanceTransforms = new VertexInstanceTransform[transformCount];
            for (var i = 1; i <= x; i++)
            {
                for (var j = 1; j <= y; j++)
                {
                    instanceTransforms[count] = new VertexInstanceTransform(Matrix.Identity
                    * Matrix.CreateTranslation(new Vector3((i - x / 2) + 2, (j - y / 2) + 2, 0)));
                    count++;
                }
            }
            instanceVertexBuffer = new DynamicVertexBuffer(graphicsDevice,
                VertexInstanceTransform.VertexDeclaration, instanceTransforms.Length, BufferUsage.WriteOnly);
            instanceVertexBuffer.SetData(instanceTransforms, 0, instanceTransforms.Length, SetDataOptions.Discard);

            #endregion Instance Transforms
        }

        public void Draw(Camera camera)
        {
            //    effect.Parameters["ViewProjection"].SetValue(camera.View * camera.Projection);
            viewProjectionParameter.SetValue(camera.View * camera.Projection);
            //  effect.CurrentTechnique.Passes[0].Apply();
            effectPass.Apply();
            graphicsDevice.SetVertexBuffers(new VertexBufferBinding[] {
                new VertexBufferBinding(vertexBuffer),
                new VertexBufferBinding(instanceVertexBuffer, 0, 1)
            });
            graphicsDevice.Indices = indexBuffer;
            graphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, 2, transformCount);
        }
    }
}