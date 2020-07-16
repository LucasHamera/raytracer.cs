namespace Raytracer.Geometry.Common
{
    interface IGeometry<T, TVec3, TColor>
    {
        T Sqrt(T value);
        T Pow(T @base, int exp);
        T Floor(T value);
        T Clamp(T value, T min, T max);
        T Dot(TVec3 left, TVec3 right);
        T Mag(TVec3 vector);
        TVec3 Norm(TVec3 vector);
        TVec3 Cross(TVec3 left, TVec3 right);
        TColor Scale(T value, TColor color);
    }
}