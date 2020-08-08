using System;
using System.Runtime.CompilerServices;
using Raytracer.Geometry.Base.Models;

namespace Raytracer.Geometry.Base.Geometries
{
    public static class GeometryMath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static float Sqrt(in float value)
        {
            var curr = value;
            var prev = 0.0f;

            while (Math.Abs(curr - prev) > float.Epsilon)
            {
                prev = curr;
                curr = 0.5f * (curr + value / curr);
            }

            return curr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static float Pow(in float @base, int exp)
        {
            var val = 1.0f;

            while (exp >= 8)
            {
                val *= @base * @base * @base * @base * @base * @base * @base * @base;
                exp -= 8;
            }

            if (exp >= 4)
            {
                val *= @base * @base * @base * @base;
                exp -= 4;
            }

            while (exp > 0)
            {
                val *= @base;
                --exp;
            }

            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static float Floor(in float value) => (int) (value >= 0 ? value : value - 1.0f);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static float Clamp(in float value, in float min, in float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static float Dot(in Vec3 left, in Vec3 right) => left.X * right.X + left.Y * right.Y + left.Z * right.Z;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static float Mag(in Vec3 vector) => Sqrt(Dot(vector, vector));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vec3 Norm(in Vec3 vector)
        {
            var mag = Mag(vector);
            return new Vec3(vector.X / mag, vector.Y / mag, vector.Z / mag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vec3 Cross(in Vec3 v1, in Vec3 v2)
        {
            return new Vec3(
                v1.Y * v2.Z - v1.Z * v2.Y,
                v1.Z * v2.X - v1.X * v2.Z,
                v1.X * v2.Y - v1.Y * v2.X
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Color Scale(in float value, in Color color)
        {
            return new Color(
                color.R * value,
                color.G * value,
                color.B * value
            );
        }
    }
}