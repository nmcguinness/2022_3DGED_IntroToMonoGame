using Microsoft.Xna.Framework;

namespace GD
{
    public class Camera
    {
        private Matrix view;
        private Matrix projection;
        public Matrix View => view;
        public Matrix Projection => projection;

        public Camera(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector)
        {
            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, cameraUpVector);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.Pi / 2,
                16 / 10.0f, 0.1f, 1000);
        }
    }
}