using Raytracer.Geometry.Baseline.Hitable;

namespace Raytracer.Geometry.Baseline
{
    public readonly struct Intersection
    {
        public readonly IHitable Thing;
        public readonly Ray Ray;
        public readonly float Distance;

        public Intersection(in IHitable thing, in Ray ray, in float distance)
        {
            Thing = thing;
            Ray = ray;
            Distance = distance;
        }
    }
}
