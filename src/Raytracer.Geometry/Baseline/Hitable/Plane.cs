using System.Runtime.CompilerServices;
using Raytracer.Geometry.Common;

namespace Raytracer.Geometry.Baseline.Hitable
{
    public readonly struct Plane<TSurface> : IHitable<TSurface, Plane<TSurface>>
        where TSurface : struct, ISurface<float, Vec3, Color>
    {
        private readonly BaselineGeometry _geometry;
        private readonly Vec3 _normal;
        private readonly float _offset;

        public Plane(in Vec3 normal, in float offset, in TSurface surface) : this()
        {
            _geometry = new BaselineGeometry();
            
            _normal = normal;
            _offset = offset;
            Surface = surface;
        }

        public TSurface Surface 
        { 
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        public Optional<Intersection<Plane<TSurface>, TSurface>> Intersect(in Ray ray)
        {
            var denom = _geometry.Dot(_normal, ray.Direction);
            if (denom > 0.0f)
                return new Optional<Intersection<Plane<TSurface>, TSurface>>();
            
            var distance = (_geometry.Dot(_normal, ray.Start) + _offset) / (-denom);
            var intersection = new Intersection<Plane<TSurface>, TSurface>(
                this, ray, distance
            );
            return new Optional<Intersection<Plane<TSurface>, TSurface>>(intersection);
        }

        public Vec3 Normal(in Vec3 position)
        {
            return _normal;
        }
    }
}
