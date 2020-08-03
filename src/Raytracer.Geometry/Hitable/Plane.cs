using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using Raytracer.Geometry.Geometries;
using Raytracer.Geometry.Models;
using Raytracer.Geometry.Surfaces;
using Raytracer.Geometry.Utils;

namespace Raytracer.Geometry.Hitable
{
    public readonly struct Plane<TSurface> : IHitable
        where TSurface : struct, ISurface<float, Vector256<float>, Vec3, Vec3s, Color, Colors>
    {
        private readonly Vec3 _normal;
        private readonly float _offset;

        public Plane(in Vec3 normal, in float offset, in TSurface surface) : this()
        {
            _normal = normal;
            _offset = offset;
            Surface = surface;
        }

        public ISurface<float, Vector256<float>, Vec3, Vec3s, Color, Colors> Surface
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        public Optional<Intersection> Intersect(in Ray ray)
        {
            var denom = GeometryMath.Dot(_normal, ray.Direction);
            if (denom > 0.0f)
                return new Optional<Intersection>();

            var distance = (GeometryMath.Dot(_normal, ray.Start) + _offset) / (-denom);
            var intersection = new Intersection(
                this, ray, distance
            );
            return new Optional<Intersection>(intersection);
        }

        public (Vector256<float>, Vector256<float>) Intersect(in Rays ray)
        {
            var denom = GeometryMath.Dot(_normal.Widen(), ray.Direction);
            var mask = Avx.CompareGreaterThan(denom, Vector256<float>.Zero);
            if (Avx.MoveMask(mask) == 0b11111111)
                return (Vector256<float>.Zero, Vector256<float>.Zero);

            var distance = Avx.Divide(
                Avx.Add(
                    GeometryMath.Dot(_normal.Widen(), ray.Start),
                    Vector256.Create(_offset)
                ),
                Avx.Subtract(Vector256<float>.Zero, denom)
            );
            return (distance, mask);
        }

        public Vec3 Normal(in Vec3 position)
        {
            return _normal;
        }

        public Vec3s Normal(in Vec3s position)
        {
            return _normal.Widen();
        }
    }
}