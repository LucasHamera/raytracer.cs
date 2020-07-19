namespace Raytracer.Geometry.Baseline
{
    public readonly struct Ligth
    {
        public readonly Vec3 Position;
        public readonly Color Color;

        public Ligth(Vec3 position, Color color)
        {
            Position = position;
            Color = color;
        }
    }
}
