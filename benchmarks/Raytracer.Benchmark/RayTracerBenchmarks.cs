using BenchmarkDotNet.Attributes;
using Raytracer.Geometry.Scenes;
using RayTracer = RayTracer.RayTracer;

namespace Raytracer.Benchmark
{
    public class RayTracerBenchmarks
    {
        private BaseRayTracer _baseRayTracer;
        private global::RayTracer.RayTracer _rayTracer;
        private MyScene _myScene;

        [Params(512)]
        public int RenderSize
        {
            get;
            set;
        }


        [GlobalSetup]
        public void Setup()
        {
            _baseRayTracer = new BaseRayTracer(RenderSize, RenderSize);
            _rayTracer = new global::RayTracer.RayTracer(RenderSize, RenderSize);
            _myScene = new MyScene();

        }

        [Benchmark]
        public void Base()
        {
            var canvas = _baseRayTracer.Render(_myScene);
        }


        [Benchmark]
        public void Improved()
        {
            var canvas = _rayTracer.Render(_myScene);
        }
    }
}
