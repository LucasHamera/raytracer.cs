using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Raytracer.Geometry.Models
{
    public class Floats
    {
        public readonly Vector256<float> Data;

        public Floats(Vector256<float> data)
        {
            Data = data;
        }
        
        public Floats(float value)
        {
            Data = Vector256.Create(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Floats operator +(in Floats left, in Floats right)
            => new Floats(Avx.Add(left.Data, right.Data));
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Floats operator *(in Floats left, in Floats right)
            => new Floats(Avx.Multiply(left.Data, right.Data));
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Floats operator /(in Floats left, in Floats right)
            => new Floats(Avx.Divide(left.Data, right.Data));
    }
}