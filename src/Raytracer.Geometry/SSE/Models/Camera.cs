using Raytracer.Geometry.Geometries;
using Raytracer.Geometry.Models;

namespace Raytracer.Geometry.SSE.Models
{
    public readonly struct Camera
    {
        public readonly VecSSE Position;
        public readonly VecSSE Forward;
        public readonly VecSSE Right;
        public readonly VecSSE Up;

        public Camera(in Vec3 position, in Vec3 lookAt)
        {
            var forwardVec3 = GeometryMath.Norm(lookAt - position);
            var rightVec3 = 1.5f * GeometryMath.Norm(GeometryMath.Cross(forwardVec3, new Vec3(0.0f, -1.0f, 0.0f)));
            var upVec3 = 1.5f * GeometryMath.Norm(GeometryMath.Cross(forwardVec3, rightVec3));

            Position = VecSSE.FromVec3(position);
            Forward = VecSSE.FromVec3(forwardVec3);
            Right = VecSSE.FromVec3(rightVec3);
            Up = VecSSE.FromVec3(upVec3);
        }
    }
}
