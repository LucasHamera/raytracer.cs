using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using Raytracer.Geometry.Base.Models;
using Raytracer.Geometry.SSE.Models;

namespace Raytracer.Geometry.SSE.Extensions
{
    public static class VecSSEExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Add(this in Vector128<float> left, in Vector128<float> right)
            => Sse.Add(left, right);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<int> Add(this in Vector128<int> left, in Vector128<int> right)
            => Sse2.Add(left, right);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Subtract(this in Vector128<float> left, in Vector128<float> right)
            => Sse.Subtract(left, right);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<int> Subtract(this in Vector128<int> left, in Vector128<int> right)
            => Sse2.Subtract(left, right);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Multiply(this in Vector128<float> left, in Vector128<float> right)
            => Sse.Multiply(left, right);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<long> Multiply(this in Vector128<int> left, in Vector128<int> right)
            => Sse41.Multiply(left, right);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Multiply(this in Vector128<float> left, in float value)
            => Sse.Multiply(left, Vector128.Create(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Opposite(this in Vector128<float> vector)
            => vector.Multiply(-1.0f);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<long> Multiply(this in Vector128<int> left, in int value)
            => Sse41.Multiply(left, Vector128.Create(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Divide(this in Vector128<float> left, in Vector128<float> right)
            => Sse.Divide(left, right);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static float HorizontalAdd(this in Vector128<float> vector)
        {
            // x3 + x2, x1 + x0, x3 + x2, x1 + x0
            var firstAdd = Sse3.HorizontalAdd(vector, vector);
            // x3 + x2 + x1 + x0, [...]
            var secondAdd = Sse3.HorizontalAdd(firstAdd, firstAdd);
            return secondAdd.GetElement(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static int HorizontalAdd(this in Vector128<int> vector)
        {
            // x3 + x2, x1 + x0, x3 + x2, x1 + x0
            var firstAdd = Ssse3.HorizontalAdd(vector, vector);
            // x3 + x2 + x1 + x0, [...]
            var secondAdd = Ssse3.HorizontalAdd(firstAdd, firstAdd);
            return secondAdd.GetElement(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Modulo(
            this in Vector128<float> left, in Vector128<float> right
        )
        {
            var div = left.Divide(right);
            var decimalPortion = div.Subtract(Sse41.RoundToNegativeInfinity(div));
            return right.Multiply(decimalPortion);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<int> ConvertToInt(this in Vector128<float> vector)
            => Sse2.ConvertToVector128Int32(vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> ConvertToFloat(this in Vector128<int> vector)
            => Sse2.ConvertToVector128Single(vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VecSSE ToVecSSE(in this Vec3 vec)
            => new VecSSE(
                Vector128.Create(vec.X),
                Vector128.Create(vec.Y),
                Vector128.Create(vec.Z)
            );

    }
}
