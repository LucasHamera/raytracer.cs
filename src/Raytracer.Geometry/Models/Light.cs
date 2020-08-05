namespace Raytracer.Geometry.Models
{
    public readonly struct Light
    {
        public readonly Vec3 Position;
        public readonly Color Color;

        public Light(in Vec3 position, in Color color)
        {
            Position = position;
            Color = color;
        }
    }
}
