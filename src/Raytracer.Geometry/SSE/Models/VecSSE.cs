using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Raytracer.Geometry.Base.Models;
using Raytracer.Geometry.SSE.Extensions;

namespace Raytracer.Geometry.SSE.Models
{
    public readonly struct VecSSE
    {
        public readonly Vector128<float> X;
        public readonly Vector128<float> Y;
        public readonly Vector128<float> Z;

        public VecSSE(in Vector128<float> x, in Vector128<float> y, in Vector128<float> z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VecSSE FromVec3(in Vec3 vec3)
            => new VecSSE(Vector128.Create(vec3.X), Vector128.Create(vec3.Y), Vector128.Create(vec3.Z));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VecSSE operator +(in VecSSE left, in VecSSE right)
            => new VecSSE(left.X.Add(right.X), left.Y.Add(right.Y), left.Z.Add(right.Z));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VecSSE operator -(in VecSSE left, in VecSSE right)
            => new VecSSE(left.X.Subtract(right.X), left.Y.Subtract(right.Y), left.Z.Subtract(right.Z));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VecSSE operator *(in Vector128<float> left, in VecSSE right)
            => new VecSSE(left.Multiply(right.X), left.Multiply(right.Y), left.Multiply(right.Z));
    }
}
