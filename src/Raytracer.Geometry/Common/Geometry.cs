namespace Raytracer.Geometry.Common
{
    interface IGeometry<T, TVec3, TColor> 
        where T: struct
        where TVec3: struct
        where TColor: struct
    {
        T Sqrt(in T value);
        T Pow(in T @base, int exp);
        T Floor(in T value);
        T Clamp(in T value, in T min, in T max);
        T Dot(in TVec3 left, in TVec3 right);
        T Mag(in TVec3 vector);
        TVec3 Norm(in TVec3 vector);
        TVec3 Cross(in TVec3 left, in TVec3 right);
        TColor Scale(in T value, in TColor color);
    }
}