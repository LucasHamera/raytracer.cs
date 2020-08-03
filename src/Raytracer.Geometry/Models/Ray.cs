namespace Raytracer.Geometry.Models
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

    public readonly struct Rays
    {
        public readonly Vec3s Start;
        public readonly Vec3s Direction;

        public Rays(
            Vec3s start,
            Vec3s direction
        )
        {
            Start = start;
            Direction = direction;
        }
    }
}