namespace Raytracer.Geometry.Baseline.Surfaces
{

    public readonly  struct SurfaceTemplate<T, TVec3, TColor>
        where T : struct
        where TVec3 : struct
        where TColor : struct
    {
        public readonly int Roughness;
        public readonly DiffuseDelegate Diffuse;
        public readonly SpecularDelegate Specular;
        public readonly ReflectDelegate Reflect;

        public SurfaceTemplate(
            in int roughness,
            in DiffuseDelegate diffuse, 
            in SpecularDelegate specular,
            in ReflectDelegate reflect
        )
        {
            Roughness = roughness;
            Diffuse = diffuse;
            Specular = specular;
            Reflect = reflect;
        }

        public delegate ref TColor DiffuseDelegate(in TVec3 position);
        public delegate ref TColor SpecularDelegate(in TVec3 position);
        public delegate T ReflectDelegate(in TVec3 position);
    }
}
