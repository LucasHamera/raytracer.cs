using System.Runtime.CompilerServices;

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
    }
}