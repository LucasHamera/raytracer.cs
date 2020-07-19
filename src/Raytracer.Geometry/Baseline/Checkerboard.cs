using Raytracer.Geometry.Common;

namespace Raytracer.Geometry.Baseline
{
    public readonly struct Checkerboard : ISurface<float, Vec3, Color>
    {
        public int Roughness
            => 150;

        public ref Color Diffuse(in Vec3 position)
        {
            var geometry = new BaselineGeometry();

            if ((int) (geometry.Floor(position.Z) + geometry.Floor(position.X)) % 2 != 0)
            {
                return ref Color.White;
            }
            return ref Color.Black;
        }

        public ref Color Specular(in Vec3 position)
            => ref Color.White;

        public float Reflect(in Vec3 position)
        {
            var geometry = new BaselineGeometry();

            return (int) (geometry.Floor(position.Z) + geometry.Floor(position.X)) % 2 != 0 
                ? 0.1f 
                : 0.7f;
        }
    }
}
