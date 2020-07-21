using BenchmarkDotNet.Attributes;
using Raytracer.Geometry.Baseline.Scenes;

namespace Raytracer.Benchmark
{
    public class BaselineRayTracerBenchmark
    {
        private RayTracer.RayTracer _rayTracer;
        private MyScene _myScene;
        private Canvas.Canvas _canvas;

        [Params(512)]
        public int RenderSize
        {
            get;
            set;
        }


        [GlobalSetup]
        public void Setup()
        {
            _rayTracer = new RayTracer.RayTracer();
            _myScene = new MyScene();
            _canvas = new Canvas.Canvas(RenderSize, RenderSize);

        }

        [Benchmark]
        public void Baseline()
        {
            _rayTracer.Render(_myScene, _canvas);
        }
    }
}
