using System.Runtime.CompilerServices;
using Raytracer.Geometry.Models;
using Raytracer.Geometry.Surfaces;
using Raytracer.Geometry.Utils;

namespace Raytracer.Geometry.Hitable
{
    public interface IHitable
    {
        public ISurface<float, Vec3, Color> Surface
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get; 
        }

        Optional<Intersection> Intersect(in Ray ray);

        Vec3 Normal(in Vec3 position);
    }
}
