using Raytracer.Geometry.Common;

namespace Raytracer.Geometry.Baseline
{
    public readonly struct Intersection<THitable, TSurface, T, TVec3, TColor>
        where THitable: struct, IHitable<TSurface, T, TVec3, TColor>
        where TSurface : struct, ISurface<T, TVec3, TColor>
        where T : struct
        where TVec3 : struct
        where TColor : struct
    {
        public readonly THitable Thing;
        public readonly Ray Ray;
        public readonly float Distance;

        public Intersection(in THitable thing, in Ray ray, in float distance)
        {
            Thing = thing;
            Ray = ray;
            Distance = distance;
        }
    }
}
