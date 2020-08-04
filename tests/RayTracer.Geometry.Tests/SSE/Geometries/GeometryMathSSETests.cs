using System.Runtime.Intrinsics;
using FluentAssertions;
using Raytracer.Geometry.SSE.Geometries;
using Raytracer.Geometry.SSE.Models;
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
            var x = Vector128.Create(0.0f, 4.0f, 16.0f, 64.0f);
            var expected = Vector128.Create(0.0f, 2.0f, 4.0f, 8.0f);

            var result = GeometryMathSSE.Sqrt(x);
            
            result.Should()
                .BeApproximately(result, expected, Precision);
        }

        [Fact]
        public void Pow()
        {
            var @base = Vector128.Create(2.0f, 2.0f, 4.0f, 2.0f);
            var exp = Vector128.Create(0, 1, 2, 3);
            var expected = Vector128.Create(1.0f, 2.0f, 16.0f, 8.0f);

           var result = GeometryMathSSE.Pow(@base, exp);

           result
               .Should()
               .BeApproximately(result, expected, Precision);
        }

        [Fact]
        public void Floor()
        {
            var x = Vector128.Create(2.0f, 3.4f, -3.4f, -3.6f);
            var expected = Vector128.Create(2.0f, 3.0f, -4.0f, -4.0f);

            var result = GeometryMathSSE.Floor(x);

            result
                .Should()
                .BeApproximately(result, expected, Precision);
        }

        [Fact]
        public void Clamp()
        {
            var value = Vector128.Create(2.0f, -2.0f, 0.5f, 0.0f);
            var min = Vector128.Create(0.0f);
            var max = Vector128.Create(1.0f);
            var expected = Vector128.Create(1.0f, 0.0f, 0.5f, 0.0f);

            var result = GeometryMathSSE.Clamp(value, min, max);

            result
                .Should()
                .BeApproximately(result, expected, Precision);
        }

        [Fact]
        public void Dot()
        {
            var uX = Vector128.Create(1.0f, 3.0f, 1.0f, 3.0f);
            var uY = Vector128.Create(1.0f, -2.0f, 1.0f, -2.0f);
            var uZ = Vector128.Create(1.0f, 1.0f, 1.0f, 1.0f);
            var u = new VecSSE(uX, uY, uZ);

            var vX = Vector128.Create(1.0f, 1.0f, 1.0f, 1.0f);
            var vY = Vector128.Create(2.0f, 2.0f, 2.0f, 2.0f);
            var vZ = Vector128.Create(3.0f, -3.0f, 3.0f, -3.0f);
            var v = new VecSSE(vX, vY, vZ);

            var expected = Vector128.Create(6.0f, -4.0f, 6.0f, -4.0f);

            var result = GeometryMathSSE.Dot(u, v);

            result
                .Should()
                .BeApproximately(result, expected, Precision);
        }

        [Fact]
        public void Mag()
        {
            var vX = Vector128.Create(0.0f, -3.0f, 0.0f, -3.0f);
            var vY = Vector128.Create(3.0f, 0.0f, 3.0f, 0.0f);
            var vZ = Vector128.Create(4.0f, 4.0f, 4.0f, 4.0f);
            var v = new VecSSE(vX, vY, vZ);

            var expected = Vector128.Create(5.0f);
            var result = GeometryMathSSE.Mag(v);

            result
                .Should()
                .BeApproximately(result, expected, Precision);
        }


        [Fact]
        public void Norm()
        {
            var vX = Vector128.Create(2.0f, 3.0f, 2.0f, 3.0f);
            var vY = Vector128.Create(0.0f, 0.0f, 0.0f, 0.0f);
            var vZ = Vector128.Create(0.0f, -4.0f, 0.0f, -4.0f);
            var v = new VecSSE(vX, vY, vZ);

            var expectedX = Vector128.Create(1.0f, 0.6f, 1.0f, 0.6f);
            var expectedY = Vector128.Create(0.0f, 0.0f, 0.0f, 0.0f);
            var expectedZ = Vector128.Create(0.0f, -0.8f, 0.0f, -0.8f);

            var result = GeometryMathSSE.Norm(v);

            result.X.Should().BeApproximately(result.X, expectedX, Precision);
            result.Y.Should().BeApproximately(result.Y, expectedY, Precision);
            result.Z.Should().BeApproximately(result.Z, expectedZ, Precision);
        }

        [Fact]
        public void Cross()
        {
            var uX = Vector128.Create(1.0f, -1.0f, 1.0f, -1.0f);
            var uY = Vector128.Create(0.0f, 2.0f, 0.0f, 2.0f);
            var uZ = Vector128.Create(0.0f, -3.0f, 0.0f, -3.0f);
            var u = new VecSSE(uX, uY, uZ);

            var vX = Vector128.Create(0.0f, 1.0f, 0.0f, 1.0f);
            var vY = Vector128.Create(1.0f, 1.0f, 1.0f, 1.0f);
            var vZ = Vector128.Create(1.0f, 1.0f, 1.0f, 1.0f);
            var v = new VecSSE(vX, vY, vZ);

            var expectedX = Vector128.Create(0.0f, 5.0f, 0.0f, 5.0f);
            var expectedY = Vector128.Create(-1.0f, -2.0f, -1.0f, -2.0f);
            var expectedZ = Vector128.Create(1.0f, -3.0f, 1.0f, -3.0f);

            var result = GeometryMathSSE.Cross(u, v);

            result.X.Should().BeApproximately(result.X, expectedX, Precision);
            result.Y.Should().BeApproximately(result.Y, expectedY, Precision);
            result.Z.Should().BeApproximately(result.Z, expectedZ, Precision);
        }

        [Fact]
        public void Scale()
        {
            var cR = Vector128.Create(1.0f, 1.0f, 1.0f, 1.0f);
            var cG = Vector128.Create(0.0f, 0.3f, 0.0f, 0.3f);
            var cB = Vector128.Create(0.0f, 0.25f, 0.0f, 0.25f);
            var c = new ColorSSE(cR, cG, cB);

            var expectedR = Vector128.Create(1.0f, 2.0f, 1.0f, 2.0f);
            var expectedG = Vector128.Create(0.0f, 0.6f, 0.0f, 0.6f);
            var expectedB = Vector128.Create(0.0f, 0.5f, 0.0f, 0.5f);

            var scale = Vector128.Create(1.0f, 2.0f, 1.0f, 2.0f);

            var result = GeometryMathSSE.Scale(scale, c);

            result.R.Should().BeApproximately(result.R, expectedR, Precision);
            result.G.Should().BeApproximately(result.G, expectedG, Precision);
            result.B.Should().BeApproximately(result.B, expectedB, Precision);
        }
    }
}
