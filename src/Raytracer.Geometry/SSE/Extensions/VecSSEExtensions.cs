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
        {
            if (Sse2.IsSupported)
                return Sse2.ConvertToVector128Int32(vector);

            return Vector128.Create(
                (int) vector.GetElement(0),
                (int) vector.GetElement(1),
                (int) vector.GetElement(2),
                (int) vector.GetElement(3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> ConvertToFloat(this in Vector128<int> vector)
        {
            if (Sse2.IsSupported)
                return Sse2.ConvertToVector128Single(vector);

            return Vector128.Create(
                (float) vector.GetElement(0),
                (float) vector.GetElement(1),
                (float) vector.GetElement(2),
                (float) vector.GetElement(3)
            );
        }

    }
}
