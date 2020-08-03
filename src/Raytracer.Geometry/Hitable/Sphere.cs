using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using Raytracer.Geometry.Geometries;
using Raytracer.Geometry.Models;
using Raytracer.Geometry.Surfaces;
using Raytracer.Geometry.Utils;

namespace Raytracer.Geometry.Hitable
{
    public readonly struct Sphere<TSurface> : IHitable
        where TSurface : struct, ISurface<float, Vector256<float>, Vec3, Vec3s, Color, Colors>
    {
        private readonly Vec3 _centre;
        private readonly float _radius2;

        public Sphere(in Vec3 centre, in float radius, in TSurface surface) : this()
        {
            _centre = centre;
            _radius2 = radius * radius;
            Surface = surface;
        }

        public ISurface<float, Vector256<float>, Vec3, Vec3s, Color, Colors> Surface
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        public Optional<Intersection> Intersect(in Ray ray)
        {
            var eo = _centre - ray.Start;
            var v = GeometryMath.Dot(eo, ray.Direction);
            var distance = 0.0f;

            if (v >= 0.0)
            {
                var disc = _radius2 - (GeometryMath.Dot(eo, eo) - v * v);
                if (disc >= 0.0)
                {
                    distance = v - GeometryMath.Sqrt(disc);
                }
            }

            if (Math.Abs(distance) < float.Epsilon)
                return new Optional<Intersection>();

            var intersection = new Intersection(this, ray, distance);
            return new Optional<Intersection>(intersection);
        }

        public (Vector256<float>, Vector256<float>) Intersect(in Rays ray)
        {
            var eo = _centre.Widen() - ray.Start;
            var v = GeometryMath.Dot(eo, ray.Direction);
            var mask1 = Avx.CompareGreaterThanOrEqual(v, Vector256<float>.Zero);
            if (Avx.MoveMask(mask1) == 0)
                return (Vector256<float>.Zero, mask1);

            var disc = Avx.Subtract(
                Vector256.Create(_radius2),
                Avx.Subtract(
                    GeometryMath.Dot(eo, eo),
                    Avx.Multiply(v, v))
            );
            var mask2 = Avx.CompareGreaterThanOrEqual(v, Vector256<float>.Zero);
            if (Avx.MoveMask(mask2) == 0)
                return (Vector256<float>.Zero, mask2);

            var distance = Avx.Subtract(v, GeometryMath.Sqrt(disc));
            return (distance, Avx.And(mask1, mask2));
        }

        public Vec3 Normal(in Vec3 position)
        {
            return GeometryMath.Norm(position - _centre);
        }

        public Vec3s Normal(in Vec3s position)
        {
            return GeometryMath.Norm(position - _centre.Widen());
        }
    }
}