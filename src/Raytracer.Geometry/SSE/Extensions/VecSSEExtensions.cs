using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Raytracer.Geometry.SSE.Extensions
{
    public static class VecSSEExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Add(this in Vector128<float> left, in Vector128<float> right)
            => Sse.Add(left, right);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Subtract(this in Vector128<float> left, in Vector128<float> right)
            => Sse.Subtract(left, right);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Multiply(this in Vector128<float> left, in Vector128<float> right)
            => Sse.Multiply(left, right);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Multiply(this in Vector128<float> left, in float value)
            => Sse.Multiply(left, Vector128.Create(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Divide(this in Vector128<float> left, in Vector128<float> right)
            => Sse.Divide(left, right);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<int> ConvertToInt(this in Vector128<float> vector)
            => Sse2.ConvertToVector128Int32(vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> ConvertToFloat(this in Vector128<int> vector)
            => Sse2.ConvertToVector128Single(vector);
    }
}
