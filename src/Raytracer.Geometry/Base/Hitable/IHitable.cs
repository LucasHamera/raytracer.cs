using System.Runtime.CompilerServices;
using Raytracer.Geometry.Base.Models;
using Raytracer.Geometry.Base.Surfaces;
using Raytracer.Geometry.Utils;

namespace Raytracer.Geometry.Base.Hitable
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
