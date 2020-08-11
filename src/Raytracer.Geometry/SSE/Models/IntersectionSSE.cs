using System.Runtime.Intrinsics;

namespace Raytracer.Geometry.SSE.Models
{
    public readonly struct IntersectionSSE
    {
        public readonly Vector128<int> Mask;
        public readonly Vector128<float> Distances;
        public readonly Vector128<int> HitableIndices;

        public IntersectionSSE(
            Vector128<int> mask, 
            Vector128<float> distances,
            Vector128<int> hitableIndices
        )
        {
            Mask = mask;
            Distances = distances;
            HitableIndices = hitableIndices;
        }
    }
}
