namespace Raytracer.Geometry.Base.Models
{
    public readonly struct Ray
    {
        public readonly Vec3 Start;
        public readonly Vec3 Direction;

        public Ray(in Vec3 start, in Vec3 direction)
        {
            Start = start;
            Direction = direction;
        }
    }
}
