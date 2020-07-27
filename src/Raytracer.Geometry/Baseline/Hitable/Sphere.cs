using System;
using System.Runtime.CompilerServices;
using Raytracer.Geometry.Baseline.Surfaces;
using Raytracer.Geometry.Common;

namespace Raytracer.Geometry.Baseline.Hitable
{
    public readonly struct Sphere : IHitable
    {
        private readonly Vec3 _centre;
        private readonly float _radius2;
        private readonly SurfaceTemplate<float, Vec3, Color> _surface;

        public Sphere(in Vec3 centre, in float radius, in SurfaceTemplate<float, Vec3, Color> surface) : this()
        {
            _centre = centre;
            _radius2 = radius * radius;
            _surface = surface;
        }

        public SurfaceTemplate<float, Vec3, Color> Surface
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _surface;
        }

        public Optional<Intersection> Intersect(in Ray ray)
        {
            var eo = _centre - ray.Start;
            var v = BaselineGeometry.Dot(eo, ray.Direction);
            var distance = 0.0f;

            if (v >= 0.0) {
                var disc = _radius2 - (BaselineGeometry.Dot(eo, eo) - v * v);
                if (disc >= 0.0) {
                    distance = v - BaselineGeometry.Sqrt(disc);
                }
            }

            if (Math.Abs(distance) < float.Epsilon) 
                return new Optional<Intersection>();

            var intersection = new Intersection(this, ray, distance);
            return new Optional<Intersection>(intersection);
        }

        public Vec3 Normal(in Vec3 position)
        {
            return BaselineGeometry.Norm(position - _centre);
        }
    }
}
