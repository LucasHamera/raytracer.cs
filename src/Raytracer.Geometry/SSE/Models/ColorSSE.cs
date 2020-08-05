using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Raytracer.Geometry.SSE.Extensions;

namespace Raytracer.Geometry.SSE.Models
{
    public readonly struct ColorSSE
    {
        public readonly Vector128<float> R;
        public readonly Vector128<float> G;
        public readonly Vector128<float> B;

        public ColorSSE(in float r, in float g, in float b)
        {
            R = Vector128.Create(r);
            G = Vector128.Create(g);
            B = Vector128.Create(b);
        }
        public ColorSSE(in Vector128<float> r, in Vector128<float> g, in Vector128<float> b)
        {
            R = r;
            G = g;
            B = b;
        }

        private static ColorSSE _white = new ColorSSE(1.0f, 1.0f, 1.0f);
        public static ref ColorSSE White
            => ref _white;

        private static ColorSSE _gray = new ColorSSE(0.5f, 0.5f, 0.5f);
        public static ref ColorSSE Gray
            => ref _gray;

        private static ColorSSE _black = new ColorSSE(0.0f, 0.0f, 0.0f);
        public static ref ColorSSE Black
            => ref _black;

        public static ref ColorSSE Background
            => ref _black;

        public static ref ColorSSE DefaultColor
            => ref _black;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ColorSSE operator +(ColorSSE a, ColorSSE b) 
            => new ColorSSE(a.R.Add(b.R), a.G.Add(b.G), a.B.Add(b.B));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ColorSSE operator *(ColorSSE a, ColorSSE b) 
            => new ColorSSE(a.R.Multiply(b.R), a.G.Multiply(b.G), a.B.Multiply(b.B));
    }
}
