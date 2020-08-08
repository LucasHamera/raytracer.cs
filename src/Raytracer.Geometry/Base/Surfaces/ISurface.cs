using System.Runtime.CompilerServices;

namespace Raytracer.Geometry.Base.Surfaces
{
    public interface ISurface<out T, TVec3, TColor>
        where T : struct
        where TVec3 : struct
        where TColor : struct
    {
        public int Roughness
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        ref TColor Diffuse(in TVec3 position);
        ref TColor Specular(in TVec3 position);
        T Reflect(in TVec3 position);
    }
}