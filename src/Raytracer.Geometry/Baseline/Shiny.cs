using System.Runtime.CompilerServices;
using Raytracer.Geometry.Common;

namespace Raytracer.Geometry.Baseline
{
    public readonly struct Shiny : ISurface<float, Vec3, Color>
    {
        public int Roughness
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => 250;
        }

        public ref Color Diffuse(in Vec3 position)
            => ref Color.White;

        public ref Color Specular(in Vec3 position)
            => ref Color.Gray;

        public float Reflect(in Vec3 position)
            => 0.7f;
    }
}