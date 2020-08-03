using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Raytracer.Geometry.Geometries;
using Raytracer.Geometry.Models;

namespace Raytracer.Geometry.Surfaces
{
    public readonly struct Checkerboard : ISurface<float, Vector256<float>, Vec3, Vec3s, Color, Colors>
    {
        public int Roughness
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            get => 150;
        }

        public ref Color Diffuse(in Vec3 position)
        {
            if ((int) (GeometryMath.Floor(position.Z) + GeometryMath.Floor(position.X)) % 2 != 0)
            {
                return ref Color.White;
            }
            return ref Color.Black;
        }

        public ref Colors Diffuse(in Vec3s position)
        {
            // if ((int) (GeometryMath.Floor(position.Z) + GeometryMath.Floor(position.X)) % 2 != 0)
            // {
                // return ref Colors.White;
            // }
            return ref Colors.Black;
        }

        public ref Color Specular(in Vec3 position)
            => ref Color.White;

        public ref Colors Specular(in Vec3s position)
            => ref Colors.White;

        public float Reflect(in Vec3 position)
        {
            return (int) (GeometryMath.Floor(position.Z) + GeometryMath.Floor(position.X)) % 2 != 0
                ? 0.1f
                : 0.7f;
        }

        public Vector256<float> Reflect(in Vec3s position)
        {
            // return (int) (GeometryMath.Floor(position.Z) + GeometryMath.Floor(position.X)) % 2 != 0
            //     ? 0.1f
            //     : 0.7f;
            
            throw new NotImplementedException();
        }
    }
}
