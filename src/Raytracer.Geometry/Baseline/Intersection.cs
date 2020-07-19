using Raytracer.Geometry.Common;

namespace Raytracer.Geometry.Baseline
{
    public readonly struct Intersection<THitable>
        where THitable: struct, IHitable
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
