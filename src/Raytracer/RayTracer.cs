using System.Runtime.Intrinsics;
using Raytracer.Canvas;
using Raytracer.Geometry.Base.Geometries;
using Raytracer.Geometry.Base.Models;
using Raytracer.Geometry.Scenes;
using Raytracer.Geometry.SSE.Extensions;
using Raytracer.Geometry.SSE.Geometries;
using Raytracer.Geometry.SSE.Models;
using Raytracer.Geometry.Utils;

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

        public Canvas Render(MyScene scene)
        {
            var canvas = new Canvas(_width, _height);
            var cameraSSE = new CameraSSE(scene.Camera);


            var i = 0;
            while (i < _height * _width)
            {
                var x = i % _width;
                var y = i / _width;


                var xVector = Vector128.Create(x, x + 1.0f, x + 2.0f, x + 3.0f);
                var yVector = Vector128.Create(1.0f * y);
                var pointVector = Point(xVector, yVector, cameraSSE);

                var intersection = TraceRay(new RaySSE(cameraSSE.Position, pointVector), scene, 0);


            }

            for (; i < _height * _width; i ++)
            {
                var x = i % _width;
                var y = i / _width;

                var point = Point(x, y, scene.Camera);
            }

            return canvas;
        }

        private Vec3 Point(in int x, in int y, in Camera cam)
        {
            var recenterX = (x - (_halfWidth)) / 2.0f / _width;
            var recenterY = -(y - (_halfHeight)) / 2.0f / _height;

            return GeometryMath.Norm(cam.Forward + (recenterX * cam.Right + recenterY * cam.Up));
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

        private IntersectionSSE TraceRay(in RaySSE ray, MyScene scene, int depth)
        {
            var intersection = Intersect(ray, scene);
            return intersection.HasValue
                ? Shade(intersection.Value, scene, depth)
                : Color.Background;
        }

        private Optional<Intersection> Intersect(in RaySSE ray, in MyScene scene)
        {
            var closestDist = Vector128.Create(float.MaxValue);
            var closestInter = new Optional<Intersection>();

            foreach (var thing in scene.Things)
            {
                var inter = thing.Intersect(ray);
                if (inter.HasValue && inter.Value.Distance < closestDist)
                {
                    closestDist = inter.Value.Distance;
                    closestInter = inter;
                }
            }

            return closestInter;
        }
    }
}