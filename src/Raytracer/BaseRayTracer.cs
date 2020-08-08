using Raytracer.Geometry.Base.Models;
using Raytracer.Geometry.Scenes;
using Raytracer.Math;

namespace Raytracer
{
    public class BaseRayTracer
    {
        private readonly int _height;
        private readonly int _width;

        private readonly float _halfHeight;
        private readonly float _halfWidth;

        public BaseRayTracer(
            int height,
            int width
        )
        {
            _height = height;
            _width = width;

            _halfHeight = _height / 2.0f;
            _halfWidth = _width / 2.0f;
        }

        public Canvas.Canvas Render(MyScene scene)
        {
            var canvas = new Canvas.Canvas(_width, _height);

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    var point = ScalarRayTracerMath.Point(
                        x, y, _width, _height, _halfWidth, _halfHeight, scene.Camera);
                    var color = ScalarRayTracerMath.TraceRay(new Ray(scene.Camera.Position, point), scene, 0);
                    canvas[x, y] = color;
                }
            }

            return canvas;
        }
    }
}