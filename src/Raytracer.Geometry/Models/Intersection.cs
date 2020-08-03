using System.Runtime.Intrinsics;
using Raytracer.Geometry.Hitable;

namespace Raytracer.Geometry.Models
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
    
    public readonly struct OptionalIntersections
    {
        public OptionalIntersections(
            Vector256<int> thing,
            Rays ray,
            Vector256<float> distance,
            Vector256<float> hasValue)
        {
            Thing = thing;
            Ray = ray;
            Distance = distance;
            HasValue = hasValue;
        }

        public readonly Vector256<int> Thing;
        public readonly Rays Ray;
        public readonly Vector256<float> Distance;
        public readonly Vector256<float> HasValue;
    }
}
