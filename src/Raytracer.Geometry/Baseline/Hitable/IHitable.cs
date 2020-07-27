using System.Runtime.CompilerServices;
using Raytracer.Geometry.Baseline.Surfaces;
using Raytracer.Geometry.Common;

namespace Raytracer.Geometry.Baseline.Hitable
{
    public interface IHitable
    {
        public SurfaceTemplate<float, Vec3, Color> Surface
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get; 
        }

        Optional<Intersection> Intersect(in Ray ray);

        Vec3 Normal(in Vec3 position);
    }
}
