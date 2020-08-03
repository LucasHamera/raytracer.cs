using System.Runtime.CompilerServices;

namespace Raytracer.Geometry.Surfaces
{
    public interface ISurface<out T, out Ts, TVec3, TVec3s, TColor, TColors>
        where T : struct
        where Ts : struct
        where TVec3 : struct
        where TVec3s : struct
        where TColor : struct
    {
        public int Roughness
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        ref TColor Diffuse(in TVec3 position);
        ref TColors Diffuse(in TVec3s position);
        ref TColor Specular(in TVec3 position);
        ref TColors Specular(in TVec3s position);
        T Reflect(in TVec3 position);
        Ts Reflect(in TVec3s position);
    }
}