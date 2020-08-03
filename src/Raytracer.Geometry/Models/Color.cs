using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Raytracer.Geometry.Models
{
    public readonly struct Color
    {
        public readonly float R;
        public readonly float G;
        public readonly float B;

        public Color(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        private static Color _white = new Color(1.0f, 1.0f, 1.0f);

        public static ref Color White
            => ref _white;

        private static Color _gray = new Color(0.5f, 0.5f, 0.5f);

        public static ref Color Gray
            => ref _gray;

        private static Color _black = new Color(0.0f, 0.0f, 0.0f);

        public static ref Color Black
            => ref _black;

        public static ref Color Background
            => ref _black;

        public static ref Color DefaultColor
            => ref _black;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Color operator +(Color a, Color b) => new Color(a.R + b.R, a.G + b.G, a.B + b.B);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Color operator *(Color a, Color b) => new Color(a.R * b.R, a.G * b.G, a.B * b.B);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Colors Widen()
        {
            return new Colors(
                Vector256.Create(R),
                Vector256.Create(G),
                Vector256.Create(B)
            );
        }
    }

    public readonly struct Colors
    {
        public readonly Vector256<float> R;
        public readonly Vector256<float> G;
        public readonly Vector256<float> B;

        public Colors(Vector256<float> r, Vector256<float> g, Vector256<float> b)
        {
            R = r;
            G = g;
            B = b;
        }

        private static Colors _white = new Colors(
            Vector256.Create(1.0f),
            Vector256.Create(1.0f),
            Vector256.Create(1.0f)
        );

        public static ref Colors White
            => ref _white;

        private static Colors _gray = new Colors(
            Vector256.Create(0.5f),
            Vector256.Create(0.5f),
            Vector256.Create(0.5f)
        );

        public static ref Colors Gray
            => ref _gray;

        private static Colors _black = new Colors(
            Vector256.Create(0.0f),
            Vector256.Create(0.0f),
            Vector256.Create(0.0f)
        );

        public static ref Colors Black
            => ref _black;

        public static ref Colors Background
            => ref _black;

        public static ref Colors DefaultColor
            => ref _black;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Colors operator +(Colors a, Colors b) => new Colors(
            Avx.Add(a.R, b.R),
            Avx.Add(a.G, b.G),
            Avx.Add(a.B, b.B)
        );

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Colors operator *(Colors a, Colors b) => new Colors(
            Avx.Multiply(a.R, b.R),
            Avx.Multiply(a.G, b.G),
            Avx.Multiply(a.B, b.B)
        );
    }
}