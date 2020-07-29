using System.Runtime.CompilerServices;
using Raytracer.Geometry.Geometries;
using Raytracer.Geometry.Models;
using Raytracer.Geometry.Surfaces;
using Raytracer.Geometry.Utils;

namespace Raytracer.Geometry.Hitable
{
    public readonly struct Plane<TSurface> : IHitable
        where TSurface : struct, ISurface<float, Vec3, Color>
    {
        private readonly Vec3 _normal;
        private readonly float _offset;

        public Plane(in Vec3 normal, in float offset, in TSurface surface) : this()
        {
            _normal = normal;
            _offset = offset;
            Surface = surface;
        }

        public ISurface<float, Vec3, Color> Surface
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

        public Vec3 Normal(in Vec3 position)
        {
            return _normal;
        }
    }
}
