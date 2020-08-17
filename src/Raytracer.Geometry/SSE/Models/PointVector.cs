using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using Raytracer.Geometry.SSE.Extensions;

namespace Raytracer.Geometry.SSE.Models
{
    public readonly struct PointVector
    {
        public readonly Vector128<int> Indexes;
        public readonly Vector128<float> X;
        public readonly Vector128<float> Y;
        
        public PointVector(
            in Vector128<float> widthVector
        ) : this(Vector128.Create(0, 1, 2, 3), widthVector)
        {
        }

        public PointVector(
            in Vector128<int> indexes,
            in Vector128<float> widthVector
        )
        {
            Indexes = indexes;
            var floatIndexes = Indexes.ConvertToFloat();

            X = floatIndexes.Modulo(widthVector);
            Y = Sse41.RoundToNegativeInfinity(floatIndexes.Divide(widthVector));
        }

        public PointVector(
            in Vector128<int> indexes,
            in Vector128<float> x,
            in Vector128<float> y
        )
        {
            Indexes = indexes;
            X = x;
            Y = y;
        }

        public PointVector Next(
            in Vector128<int> mask,
            in Vector128<float> widthVector
        )
        {
            var max = MaxIndex();

            // Todo optimize
            var index1 = Indexes.GetElement(0);
            if (mask.GetElement(0) == 0)
                index1 = ++max;
            var index2 = Indexes.GetElement(1);
            if (mask.GetElement(1) == 0)
                index2 = ++max;
            var index3 = Indexes.GetElement(2);
            if (mask.GetElement(2) == 0)
                index3 = ++max;
            var index4 = Indexes.GetElement(3);
            if (mask.GetElement(3) == 0)
                index4 = ++max;

            var newIndexes = Vector128.Create(index1, index2, index3, index4);
            return new PointVector(newIndexes, widthVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int MaxIndex()
            => Indexes.GetElement(3);
    }
}
