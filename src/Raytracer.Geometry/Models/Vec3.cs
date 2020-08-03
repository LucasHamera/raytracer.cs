using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

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

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Vec3s Widen()
        {
            return new Vec3s(
                Vector256.Create(X),
                Vector256.Create(Y),
                Vector256.Create(Z)
            );
        }
    }

    public readonly struct Vec3s
    {
        public readonly Vector256<float> X;
        public readonly Vector256<float> Y;
        public readonly Vector256<float> Z;

        public Vec3s(Vector256<float> x, Vector256<float> y, Vector256<float> z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vec3s operator +(in Vec3s left, in Vec3s right)
            => new Vec3s(Avx.Add(left.X, right.X), Avx.Add(left.Y, right.Y), Avx.Add(left.Z, right.Z));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vec3s operator -(in Vec3s left, in Vec3s right)
            => new Vec3s(Avx.Subtract(left.X, right.X), Avx.Subtract(left.Y, right.Y), Avx.Subtract(left.Z, right.Z));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vec3s operator *(in Vector256<float> left, in Vec3s right)
            => new Vec3s(
                Avx.Multiply(left, right.X),
                Avx.Multiply(left, right.Z),
                Avx.Multiply(left, right.Z)
            );
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vec3s operator *(in float left, in Vec3s right)
        {
            var lefts = Vector256.Create(left);
            return new Vec3s(
                Avx.Multiply(lefts, right.X),
                Avx.Multiply(lefts, right.Z),
                Avx.Multiply(lefts, right.Z)
            );
        }
    }
}