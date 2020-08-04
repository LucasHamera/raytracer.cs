using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using Raytracer.Geometry.SSE.Extensions;
using Raytracer.Geometry.SSE.Models;

namespace Raytracer.Geometry.SSE.Geometries
{
    public static class GeometryMathSSE
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Sqrt(in Vector128<float> value)
            => Sse.Sqrt(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Pow(in Vector128<float> @base,  int exp)
        {
            var val = Vector128.Create(1.0f);

            while (exp >= 8)
            {
                val = val
                    .Multiply(@base).Multiply(@base).Multiply(@base).Multiply(@base)
                    .Multiply(@base).Multiply(@base).Multiply(@base).Multiply(@base);
                exp -= 8;
            }

            if (exp >= 4)
            {
                val = val
                    .Multiply(@base).Multiply(@base).Multiply(@base).Multiply(@base);
                exp -= 4;
            }

            while (exp > 0)
            {
                val = val.Multiply(@base);
                --exp;
            }

            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Pow(in Vector128<float> @base, in Vector128<int> exp)
        {
            return Vector128.Create(
                (float)Math.Pow(@base.GetElement(0), exp.GetElement(0)),
                (float)Math.Pow(@base.GetElement(1), exp.GetElement(1)),
                (float)Math.Pow(@base.GetElement(2), exp.GetElement(2)),
                (float)Math.Pow(@base.GetElement(3), exp.GetElement(3))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Floor(in Vector128<float> value)
        {
            if (Sse41.IsSupported)
                return Sse41.Floor(value);

            var zeroVector = Vector128<float>.Zero;
            // value >= 0 ?
            var greater = Sse.CompareGreaterThan(value, zeroVector);
            // value
            var result = Sse.And(greater, value);
            // value - 1.0f
            var notGreaterMask = Sse.Xor(greater, Vector128.Create(1.0f));
            var notGraterValue = value.Subtract(Vector128.Create(-1.0f));
            var notGraterResult = Sse.And(notGraterValue, notGreaterMask);

            result = result.Add(notGraterResult);

            if (Sse2.IsSupported)
            {
                var resultInt = Sse2.ConvertToVector128Int32(result);
                return resultInt.AsSingle();
            }

            return result
                .ConvertToInt()
                .ConvertToFloat();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Clamp(in Vector128<float> value, in Vector128<float> min,
            in Vector128<float> max)
        {
            var lessValuesMask = Sse.CompareLessThan(value, min);
            var greaterValuesMask = Sse.CompareGreaterThan(value, max);

            if (Sse41.IsSupported)
            {
                // (value < min) return min
                var resultBlend = Sse41.BlendVariable(value, min, lessValuesMask);
                // value > max) return max
                return Sse41.BlendVariable(resultBlend, max, greaterValuesMask);
            }

            var greaterLessMask = Sse.And(lessValuesMask, greaterValuesMask);

            // min values
            var resultLess = Sse.And(lessValuesMask, min);
            // max values
            var resultGreater = Sse.And(greaterValuesMask, max);
            // another values
            var anotherValues = Sse.AndNot(greaterLessMask, value);

            return resultLess
                .Add(resultGreater)
                .Add(anotherValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Dot(in VecSSE left, in VecSSE right)
        {
            var xMul = left.X.Multiply(right.X);
            var yMul = left.Y.Multiply(right.Y);
            var zMul = left.Z.Multiply(right.Z);

            return xMul
                .Add(yMul)
                .Add(zMul);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector128<float> Mag(in VecSSE vector) 
            => Sqrt(Dot(vector, vector));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VecSSE Norm(in VecSSE vector)
        {
            var mag = Mag(vector);
            return new VecSSE(vector.X.Divide(mag), vector.Y.Divide(mag), vector.Z.Divide(mag));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VecSSE Cross(in VecSSE v1, in VecSSE v2)
        {
            var resultXFirstMultiply = v1.Y.Multiply(v2.Z);
            var resultXSecondMultiply = v1.Z.Multiply(v2.Y);

            var resultYFirstMultiply = v1.Z.Multiply(v2.X);
            var resultYSecondMultiply = v1.X.Multiply(v2.Z);

            var resultZFirstMultiply = v1.X.Multiply(v2.Y);
            var resultZSecondMultiply = v1.Y.Multiply(v2.X);

            return new VecSSE(
                resultXFirstMultiply.Subtract(resultXSecondMultiply),
                resultYFirstMultiply.Subtract(resultYSecondMultiply),
                resultZFirstMultiply.Subtract(resultZSecondMultiply)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ColorSSE Scale(in Vector128<float> value, in ColorSSE color)
        {
            return new ColorSSE(
                color.R.Multiply(value),
                color.G.Multiply(value),
                color.B.Multiply(value)
            );
        }
    }
}
