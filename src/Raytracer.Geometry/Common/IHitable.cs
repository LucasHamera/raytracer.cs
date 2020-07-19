using System.Runtime.CompilerServices;
using Raytracer.Geometry.Baseline;

namespace Raytracer.Geometry.Common
{
    public interface IHitable<TSurface, T, TVec3, TColor>
        where TSurface: struct, ISurface<T, TVec3, TColor>
        where T : struct
        where TVec3 : struct
        where TColor : struct

    {
        public TSurface Surface
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get; 
        }

        Optional<Intersection<THitable, TSurface, T, TVec3, TColor>> Intersect<THitable>(in Ray ray)
            where THitable : struct, IHitable<TSurface, T, TVec3, TColor>;

        TVec3 Normal(in TVec3 position);
    }
}
