using System.Runtime.CompilerServices;

namespace Raytracer.Geometry.Models
{
    public readonly struct Vec3
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        public Vec3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vec3 operator +(in Vec3 left, in Vec3 right)
            => new Vec3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vec3 operator -(in Vec3 left, in Vec3 right)
            => new Vec3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vec3 operator *(in float left, in Vec3 right)
            => new Vec3(left * right.X, left * right.Y, left * right.Z);
    }
}
