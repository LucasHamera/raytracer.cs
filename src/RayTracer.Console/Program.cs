using Raytracer.Canvas;
using Raytracer.Canvas.Extensions;
using Raytracer.Geometry.Scenes;

namespace RayTracer.Console
{
    internal static class Program
    {
        public static void Main()
        {
            var tracer = new RayTracer();
            var canvas = new Canvas(512, 512);
            var scene = new MyScene();
            tracer.Render(scene, canvas);
            canvas.ToFile("output.png");
        }
    }
}