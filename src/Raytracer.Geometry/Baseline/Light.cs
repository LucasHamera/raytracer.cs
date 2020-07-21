namespace Raytracer.Geometry.Baseline
{
    public readonly struct Light
    {
        public readonly Vec3 Position;
        public readonly Color Color;

        public Light(Vec3 position, Color color)
        {
            Position = position;
            Color = color;
        }
    }
}
