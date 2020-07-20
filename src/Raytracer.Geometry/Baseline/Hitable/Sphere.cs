using System;
using System.Runtime.CompilerServices;
using Raytracer.Geometry.Common;

namespace Raytracer.Geometry.Baseline.Hitable
{
    public readonly struct Sphere<TSurface> : IHitable<TSurface, Sphere<TSurface>>
        where TSurface : struct, ISurface<float, Vec3, Color>
    {
        private readonly BaselineGeometry _geometry;
        private readonly Vec3 _centre;
        private readonly float _radius2;

        public Sphere(in Vec3 centre, in float radius2) : this()
        {
            _geometry = new BaselineGeometry();

            _centre = centre;
            _radius2 = radius2;
        }

        public TSurface Surface
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        public Optional<Intersection<Sphere<TSurface>, TSurface>> Intersect(in Ray ray)
        {
            var eo = _centre - ray.Start;
            var v = _geometry.Dot(eo, ray.Direction);
            var distance = 0.0f;

            if (v >= 0.0) {
                var disc = _radius2 - (_geometry.Dot(eo, eo) - v * v);
                if (disc >= 0.0) {
                    distance = v - _geometry.Sqrt(disc);
                }
            }

            if (Math.Abs(distance) < float.Epsilon) 
                return new Optional<Intersection<Sphere<TSurface>, TSurface>>();

            var intersection = new Intersection<Sphere<TSurface>, TSurface>(this, ray, distance);
            return new Optional<Intersection<Sphere<TSurface>, TSurface>>(intersection);
        }

        public Vec3 Normal(in Vec3 position)
        {
            return _geometry.Norm(position - _centre);
        }
    }
}
