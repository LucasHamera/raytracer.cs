using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Raytracer.Geometry.Models;

namespace Raytracer.Geometry.Surfaces
{
    public readonly struct Shiny : ISurface<float, Vector256<float>, Vec3, Vec3s, Color, Colors>
    {
        public int Roughness
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => 250;
        }

        public ref Color Diffuse(in Vec3 position)
            => ref Color.White;

        public ref Colors Diffuse(in Vec3s position) =>
            ref Colors.White;

        public ref Color Specular(in Vec3 position)
            => ref Color.Gray;

        public ref Colors Specular(in Vec3s position)
            => ref Colors.Gray;

        public float Reflect(in Vec3 position)
            => 0.7f;

        public Vector256<float> Reflect(in Vec3s position)
            => Vector256.Create(0.7f);
    }
}