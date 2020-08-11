using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using Raytracer.Geometry.SSE.Extensions;

namespace Raytracer.Geometry.SSE.Models
{
    public struct RenderPositioner
    {
        private int _index;
        public int Index
        {

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _index;
        }

        public IntersectionSSE TakeStep(IntersectionSSE intersection)
        {
            var calculatedColorsCount =
                Sse2.CompareEqual(intersection.Mask, Vector128<int>.Zero).HorizontalAdd();
            Interlocked.Add(ref _index, calculatedColorsCount);

            return intersection;
        }
    }
}
