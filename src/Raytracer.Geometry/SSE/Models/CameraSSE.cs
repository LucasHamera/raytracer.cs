using Raytracer.Geometry.Geometries;
using Raytracer.Geometry.Models;
using Raytracer.Geometry.SSE.Extensions;

namespace Raytracer.Geometry.SSE.Models
{
    public readonly struct CameraSSE
    {
        public readonly VecSSE Position;
        public readonly VecSSE Forward;
        public readonly VecSSE Right;
        public readonly VecSSE Up;

        public CameraSSE(in Vec3 position, in Vec3 lookAt)
        {
            var forwardVec3 = GeometryMath.Norm(lookAt - position);
            var rightVec3 = 1.5f * GeometryMath.Norm(GeometryMath.Cross(forwardVec3, new Vec3(0.0f, -1.0f, 0.0f)));
            var upVec3 = 1.5f * GeometryMath.Norm(GeometryMath.Cross(forwardVec3, rightVec3));

            Position = VecSSE.FromVec3(position);
            Forward = VecSSE.FromVec3(forwardVec3);
            Right = VecSSE.FromVec3(rightVec3);
            Up = VecSSE.FromVec3(upVec3);
        }

        public CameraSSE(Camera camera)
        {
            Position = camera.Position.ToVecSSE();
            Forward = camera.Forward.ToVecSSE();
            Right = camera.Right.ToVecSSE();
            Up = camera.Up.ToVecSSE();
        }
    }
}
