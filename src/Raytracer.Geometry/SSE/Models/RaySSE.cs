namespace Raytracer.Geometry.SSE.Models
{
    public readonly struct RaySSE
    {
        public readonly VecSSE Start;
        public readonly VecSSE Direction;

        public RaySSE(in VecSSE start, in VecSSE direction)
        {
            Start = start;
            Direction = direction;
        }
    }
}
