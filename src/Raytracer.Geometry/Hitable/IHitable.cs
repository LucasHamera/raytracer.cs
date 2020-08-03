using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Raytracer.Geometry.Models;
using Raytracer.Geometry.Surfaces;
using Raytracer.Geometry.Utils;

namespace Raytracer.Geometry.Hitable
{
    public interface IHitable
    {
        public ISurface<float, Vector256<float>, Vec3, Vec3s, Color, Colors> Surface
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get; 
        }

        Optional<Intersection> Intersect(in Ray ray);
        (Vector256<float>, Vector256<float>) Intersect(in Rays ray);

        Vec3 Normal(in Vec3 position);
        Vec3s Normal(in Vec3s position);
    }
}
