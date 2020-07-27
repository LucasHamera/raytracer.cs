using System.Runtime.CompilerServices;

namespace Raytracer.Geometry.Baseline.Surfaces
{
    public static class Checkerboard
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

        public const int Roughness = 150;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ref Color Diffuse(in Vec3 position)
        {
            if ((int) (BaselineGeometry.Floor(position.Z) + BaselineGeometry.Floor(position.X)) % 2 != 0)
            {
                return ref Color.White;
            }
            return ref Color.Black;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ref Color Specular(in Vec3 position)
            => ref Color.White;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static float Reflect(in Vec3 position)
        {
            return (int) (BaselineGeometry.Floor(position.Z) + BaselineGeometry.Floor(position.X)) % 2 != 0
                ? 0.1f
                : 0.7f;
        }
    }
}
