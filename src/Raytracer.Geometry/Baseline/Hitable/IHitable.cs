using System.Runtime.CompilerServices;
using Raytracer.Geometry.Common;

namespace Raytracer.Geometry.Baseline.Hitable
{
    public interface IHitable<TSurface, THitable>
        where TSurface: struct, ISurface<float, Vec3, Color>
        where THitable : struct, IHitable<TSurface, THitable>
    {
        public TSurface Surface
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get; 
        }

        Optional<Intersection<THitable, TSurface>> Intersect(in Ray ray);

        Vec3 Normal(in Vec3 position);
    }
}
