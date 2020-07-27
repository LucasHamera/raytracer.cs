using System.Runtime.CompilerServices;

namespace Raytracer.Geometry.Baseline.Surfaces
{
    public static class Shiny
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SurfaceTemplate<float, Vec3, Color> Create()
        {
            return new SurfaceTemplate<float, Vec3, Color>(
                Roughness,
                Diffuse,
                Specular,
                Reflect
            );
        }

        public const int Roughness = 250;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ref Color Diffuse(in Vec3 position)
            => ref Color.White;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ref Color Specular(in Vec3 position)
            => ref Color.Gray;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static float Reflect(in Vec3 position)
            => 0.7f;
    }
}