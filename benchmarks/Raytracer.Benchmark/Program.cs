using BenchmarkDotNet.Running;

namespace Raytracer.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<RayTracerBenchmarks>();
        }
    }
}
