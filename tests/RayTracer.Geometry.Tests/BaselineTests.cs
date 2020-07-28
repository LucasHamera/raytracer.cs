using FluentAssertions;
using Raytracer.Geometry.Geometry;
using Raytracer.Geometry.Models;
using Xunit;

namespace RayTracer.Geometry.Tests
{
    public class BaselineTests
    {
        private const float Precision = 0.00001f;

        [Theory]
        [InlineData(0.0f, 0.0f)]
        [InlineData(4.0f, 2.0f)]
        public void Sqrt(float x, float expected)
        {
            BaselineGeometry.Sqrt(x).Should().BeApproximately(expected, Precision);
        }

        [Theory]
        [InlineData(2.0f, 0, 1.0f)]
        [InlineData(2.0f, 1, 2.0f)]
        [InlineData(4.0f, 2, 16.0f)]
        public void Pow(float @base, int exp, float expected)
        {
            BaselineGeometry.Pow(@base, exp).Should().BeApproximately(expected, Precision);
        }

        [Theory]
        [InlineData(0.0f, 0.0f)]
        [InlineData(2.0f, 2.0f)]
        [InlineData(3.4f, 3.0f)]
        [InlineData(3.6f, 3.0f)]
        [InlineData(-3.4f, -4.0f)]
        [InlineData(-3.6f, -4.0f)]
        public void Floor(float x, float expected)
        {
            BaselineGeometry.Floor(x).Should().BeApproximately(expected, Precision);
        }

        [Theory]
        [InlineData(2.0f, 0.0f, 1.0f, 1.0f)]
        [InlineData(-2.0f, 0.0f, 1.0f, 0.0f)]
        [InlineData(0.5f, 0.0f, 1.0f, 0.5f)]
        public void Clamp(float value, float min, float max, float expected)
        {
            BaselineGeometry.Clamp(value, min, max).Should().BeApproximately(expected, Precision);
        }

        [Theory]
        [InlineData(
            1.0f, 1.0f, 1.0f,
            1.0f, 2.0f, 3.0f,
            6.0f
        )]
        [InlineData(
            3.0f, -2.0f, 1.0f,
            1.0f, 2.0f, -3.0f,
            -4.0f
        )]
        public void Dot(
            float x1, float y1, float z1,
            float x2, float y2, float z2,
            float expected
        )
        {
            var u = new Vec3(x1, y1, z1);
            var v = new Vec3(x2, y2, z2);

            BaselineGeometry.Dot(u, v).Should().BeApproximately(expected, Precision);
        }

        [Theory]
        [InlineData(0.0f, 3.0f, 4.0f, 5.0f)]
        [InlineData(-3.0f, 0.0f, 4.0f, 5.0f)]
        public void Mag(
            float x, float y, float z,
            float expected
        )
        {
            var v = new Vec3(x, y, z);
            BaselineGeometry.Mag(v).Should().BeApproximately(expected, Precision);
        }

        [Theory]
        [InlineData(
            2.0f, 0.0f, 0.0f,
            1.0f, 0.0f, 0.0f
        )]
        [InlineData(
            3.0f, 0.0f, -4.0f,
            0.6f, 0.0f, -0.8f
        )]
        public void Norm(
            float x, float y, float z,
            float expectedX, float expectedY, float expectedZ
        )
        {
            var v = new Vec3(x, y, z);
            var result = BaselineGeometry.Norm(v);
            result.X.Should().BeApproximately(expectedX, Precision);
            result.Y.Should().BeApproximately(expectedY, Precision);
            result.Z.Should().BeApproximately(expectedZ, Precision);
        }

        [Theory]
        [InlineData(
            1.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 1.0f,
            0.0f, -1.0f, 1.0f
        )]
        [InlineData(
            -1.0f, 2.0f, -3.0f,
            1.0f, 1.0f, 1.0f,
            5.0f, -2.0f, -3.0f
        )]
        public void Cross(
            float x1, float y1, float z1,
            float x2, float y2, float z2,
            float expectedX, float expectedY, float expectedZ
        )
        {
            var u = new Vec3(x1, y1, z1);
            var v = new Vec3(x2, y2, z2);
            var w = BaselineGeometry.Cross(u, v);

            w.X.Should().BeApproximately(expectedX, Precision);
            w.Y.Should().BeApproximately(expectedY, Precision);
            w.Z.Should().BeApproximately(expectedZ, Precision);
        }

        [Theory]
        [InlineData(
            1.0f, 0.0f, 0.0f,
            1.0f,
            1.0f, 0.0f, 0.0f
        )]
        [InlineData(
            1.0f, 0.3f, 0.25f,
            2.0f,
            2.0f, 0.6f, 0.5f
        )]
        public void Scale(
            float r, float g, float b,
            float scale,
            float expectedR, float expectedG, float expectedB
        )
        {
            var c = new Color(r, g, b);
            var newColor = BaselineGeometry.Scale(scale, c);

            newColor.R.Should().BeApproximately(expectedR, Precision);
            newColor.G.Should().BeApproximately(expectedG, Precision);
            newColor.B.Should().BeApproximately(expectedB, Precision);
        }
    }
}