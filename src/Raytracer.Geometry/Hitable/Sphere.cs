using System;
using System.Runtime.CompilerServices;
using Raytracer.Geometry.Geometries;
using Raytracer.Geometry.Models;
using Raytracer.Geometry.Surfaces;
using Raytracer.Geometry.Utils;

namespace Raytracer.Geometry.Hitable
{
    public readonly struct Sphere<TSurface> : IHitable
        where TSurface : struct, ISurface<float, Vec3, Color>
    {
        private readonly Vec3 _centre;
        private readonly float _radius2;

        public Sphere(in Vec3 centre, in float radius, in TSurface surface) : this()
        {
            _centre = centre;
            _radius2 = radius * radius;
            Surface = surface;
        }

        public ISurface<float, Vec3, Color> Surface
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        public Optional<Intersection> Intersect(in Ray ray)
        {
            var eo = _centre - ray.Start;
            var v = GeometryMath.Dot(eo, ray.Direction);
            var distance = 0.0f;

            if (v >= 0.0) {
                var disc = _radius2 - (GeometryMath.Dot(eo, eo) - v * v);
                if (disc >= 0.0) {
                    distance = v - GeometryMath.Sqrt(disc);
                }
            }

            if (Math.Abs(distance) < float.Epsilon) 
                return new Optional<Intersection>();

            var intersection = new Intersection(this, ray, distance);
            return new Optional<Intersection>(intersection);
        }

        public Vec3 Normal(in Vec3 position)
        {
            return GeometryMath.Norm(position - _centre);
        }
    }
}
