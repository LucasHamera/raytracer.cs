using System.Runtime.Intrinsics;
using Raytracer.Canvas;
using Raytracer.Geometry.Scenes;
using Raytracer.Geometry.SSE.Extensions;
using Raytracer.Geometry.SSE.Geometries;
using Raytracer.Geometry.SSE.Models;
using Raytracer.Math;

namespace RayTracer
{
    public class RayTracer
    {
        private readonly int _height;
        private readonly Vector128<float> _heightVector;
        private readonly int _width;
        private readonly Vector128<float> _widthVector;

        private readonly float _halfHeight;
        private readonly Vector128<float> _halfHeightVector;
        private readonly float _halfWidth;
        private readonly Vector128<float> _halfWidthVector;

        public RayTracer(
            int height,
            int width
        )
        {
            _height = height;
            _heightVector = Vector128.Create(1.0f * _height);
            _width = width;
            _widthVector = Vector128.Create(1.0f * _width);

            _halfHeight = _height / 2.0f;
            _halfHeightVector = Vector128.Create(_halfHeight);
            _halfWidth = _width / 2.0f;
            _halfWidthVector = Vector128.Create(_halfWidth);
        }

        private VecSSE Point(in Vector128<float> x, in Vector128<float> y, in CameraSSE cam)
        {
            var twosVector = Vector128.Create(2.0f);

            var recenterX = x
                .Subtract(_halfWidthVector)
                .Divide(twosVector)
                .Divide(_widthVector);

            var recenterY = y
                .Subtract(Vector128.Create(_halfWidth))
                .Opposite()
                .Divide(Vector128.Create(2.0f))
                .Divide(Vector128.Create(1.0f * _height));

            return GeometryMathSSE.Norm(cam.Forward + (recenterX * cam.Right + recenterY * cam.Up));
        }

        public Canvas Render(MyScene scene)
        {
            var canvas = new Canvas(_width, _height);
            var cameraSSE = new CameraSSE(scene.Camera);

            for (var y = 0; y < _height; y++)
            {
                var x = 0;

                for (; x < _width; x += 4)
                {
                    var xVector = Vector128.Create(x, x + 1.0f, x + 2.0f, x + 3.0f);
                    var yVector = Vector128.Create(1.0f * y);

                    var pointVector = Point(xVector, yVector, cameraSSE);
                }

                for (; x < _width; x++)
                {
                    var point = ScalarRayTracerMath.Point(
                        x, y, _width, _height, _halfWidth, _halfHeight, scene.Camera);
                }
            }

            return canvas;
        }
    }
}