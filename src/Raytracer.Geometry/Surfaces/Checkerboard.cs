using System.Runtime.CompilerServices;
using Raytracer.Geometry.Models;

namespace Raytracer.Geometry.Surfaces
{
    public readonly struct Checkerboard : ISurface<float, Vec3, Color>
    {
        public int Roughness
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            get => 150;
        }

        public ref Color Diffuse(in Vec3 position)
        {
            if ((int) (Geometries.Geometry.Floor(position.Z) + Geometries.Geometry.Floor(position.X)) % 2 != 0)
            {
                return ref Color.White;
            }
            return ref Color.Black;
        }

        public ref Color Specular(in Vec3 position)
            => ref Color.White;

        public float Reflect(in Vec3 position)
        {
            return (int) (Geometries.Geometry.Floor(position.Z) + Geometries.Geometry.Floor(position.X)) % 2 != 0
                ? 0.1f
                : 0.7f;
        }
    }
}
