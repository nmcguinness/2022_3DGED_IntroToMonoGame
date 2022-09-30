using Microsoft.Xna.Framework;
using System;

namespace GD
{
    public class Projection
    {
        protected float nearClipPlane;
        protected float farClipPlane;

        public float NearClipPlane { get => nearClipPlane; protected set => nearClipPlane = value; }
        public float FarClipPlane { get => farClipPlane; protected set => farClipPlane = value; }

        public Projection(float nearClipPlane, float farClipPlane)
        {
            NearClipPlane = nearClipPlane;
            FarClipPlane = farClipPlane;
        }

        public override bool Equals(object obj)
        {
            return obj is Projection projection &&
                   nearClipPlane == projection.nearClipPlane &&
                   farClipPlane == projection.farClipPlane &&
                   NearClipPlane == projection.NearClipPlane &&
                   FarClipPlane == projection.FarClipPlane;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(nearClipPlane, farClipPlane, NearClipPlane, FarClipPlane);
        }

        public object Clone()
        {
            return new Projection(nearClipPlane, farClipPlane);
        }
    }

    public class PerspectiveProjection : Projection
    {
        protected float fieldOfView;
        protected float aspectRatio;

        public Matrix Value
        {
            get
            {
                return Matrix.CreatePerspectiveFieldOfView(fieldOfView,
                    aspectRatio, nearClipPlane, farClipPlane);
            }
        }

        public float FieldOfView { get => fieldOfView; protected set => fieldOfView = value; }
        public float AspectRatio { get => aspectRatio; protected set => aspectRatio = value; }

        public PerspectiveProjection(float fieldOfView, float aspectRatio,
            float nearClipPlane, float farClipPlane)
            : base(nearClipPlane, farClipPlane)
        {
            FieldOfView = fieldOfView;
            AspectRatio = aspectRatio;
        }

        public override bool Equals(object obj)
        {
            return obj is PerspectiveProjection projection &&
                   base.Equals(obj) &&
                   nearClipPlane == projection.nearClipPlane &&
                   farClipPlane == projection.farClipPlane &&
                   fieldOfView == projection.fieldOfView &&
                   aspectRatio == projection.aspectRatio;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), nearClipPlane, farClipPlane, fieldOfView, aspectRatio);
        }

        public new object Clone()
        {
            return new PerspectiveProjection(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
        }
    }

    public class OrthographicProjection : Projection
    {
        protected float width;
        protected float height;

        public float Width { get => width; protected set => width = value; }
        public float Height { get => height; protected set => height = value; }

        public Matrix Value
        {
            get
            {
                return Matrix.CreateOrthographic(width, height, nearClipPlane,
                    farClipPlane);
            }
        }

        public OrthographicProjection(float width, float height,
         float nearClipPlane, float farClipPlane)
         : base(nearClipPlane, farClipPlane)
        {
            Width = width;
            Height = height;
        }

        public override bool Equals(object obj)
        {
            return obj is OrthographicProjection projection &&
                   base.Equals(obj) &&
                   nearClipPlane == projection.nearClipPlane &&
                   farClipPlane == projection.farClipPlane &&
                   width == projection.width &&
                   height == projection.height;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), nearClipPlane, farClipPlane, width, height);
        }

        public new object Clone()
        {
            return new PerspectiveProjection(width, height, nearClipPlane, farClipPlane);
        }
    }
}