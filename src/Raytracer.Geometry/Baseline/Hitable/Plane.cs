using System.Runtime.CompilerServices;
using Raytracer.Geometry.Baseline.Surfaces;
using Raytracer.Geometry.Common;

namespace Raytracer.Geometry.Baseline.Hitable
{
    public readonly struct Plane : IHitable
    {
        private readonly Vec3 _normal;
        private readonly float _offset;
        private readonly SurfaceTemplate<float, Vec3, Color> _surface;

        public Plane(in Vec3 normal, in float offset, in SurfaceTemplate<float, Vec3, Color> surface) : this()
        {
            _normal = normal;
            _offset = offset;
            _surface = surface;
        }

        public SurfaceTemplate<float, Vec3, Color> Surface
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _surface;
        }

        public Optional<Intersection> Intersect(in Ray ray)
        {
            var denom = BaselineGeometry.Dot(_normal, ray.Direction);
            if (denom > 0.0f)
                return new Optional<Intersection>();
            
            var distance = (BaselineGeometry.Dot(_normal, ray.Start) + _offset) / (-denom);
            var intersection = new Intersection(
                this, ray, distance
            );
            return new Optional<Intersection>(intersection);
        }

        public Vec3 Normal(in Vec3 position)
        {
            return _normal;
        }
    }
}
