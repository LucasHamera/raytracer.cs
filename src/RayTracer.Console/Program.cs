using Raytracer.Canvas;
using Raytracer.Geometry.Baseline.Scenes;

namespace RayTracer.Console
{
    internal static class Program
    {
        public static void Main()
        {
            var tracer = new RayTracer();
            var canvas = new Canvas(256, 256);
            var scene = new MyScene();
            tracer.Render(scene, canvas);
        }
    }
}