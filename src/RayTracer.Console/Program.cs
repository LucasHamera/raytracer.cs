using Raytracer;
using Raytracer.Canvas.Extensions;
using Raytracer.Geometry.Scenes;

namespace RayTracer.Console
{
    internal static class Program
    {
        public static void Main()
        {
            var tracer = new ParallelRayTracer(512, 512);
            var scene = new MyScene();
            var canvas = tracer.Render(scene);
            canvas.ToFile("output.png");
        }
    }
}