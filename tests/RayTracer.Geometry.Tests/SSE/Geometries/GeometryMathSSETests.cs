using System.Runtime.Intrinsics;
using FluentAssertions;
using Raytracer.Geometry.SSE.Geometries;
using RayTracer.Geometry.Tests.SSE.Extensions;
using Xunit;

namespace RayTracer.Geometry.Tests.SSE.Geometries
{
    public class GeometryMathSSETests
    {
        private const float Precision = 0.00001f;

        [Fact]
        public void Sqrt()
        {
            // #1
            var x = Vector128.Create(0.0f);
            var expected = Vector128.Create(0.0f);

            var result = GeometryMathSSE.Sqrt(x);
            
            result.Should()
                .BeApproximately(x, expected, Precision);

            // #2
            x = Vector128.Create(4.0f);
            expected = Vector128.Create(2.0f);

            result = GeometryMathSSE.Sqrt(x);

            result.Should()
                .BeApproximately(result, expected, Precision);
        }

        [Fact]
        public void Pow()
        {
            // #1
            Vector128<float> @base = Vector128.Create(2.0f);
            int exp = 0;
            Vector128<float> expected = Vector128.Create(1.0f);

           var result = GeometryMathSSE.Pow(@base, exp);

           result
               .Should()
               .BeApproximately(result, expected, Precision);
        }
    }
}
