using System.Runtime.Intrinsics;
using FluentAssertions;
using Raytracer.Geometry.SSE.Models;
using Xunit;

namespace RayTracer.Geometry.Tests.SSE.Models
{
    public class PointVectorTests
    {
        [Theory]
        [InlineData(
            255.0f,
            0, 1, 2, 3,
            0.0f, 1.0f, 2.0f, 3.0f,
            0.0f, 0.0f, 0.0f, 0.0f
        )]
        [InlineData(
            2.0f,
            0, 1, 2, 3,
            0.0f, 1.0f, 0.0f, 1.0f,
            0.0f, 0.0f, 1.0f, 1.0f
        )]
        public void GivenWidthVectorConstructorShouldCreateValidPointVector(
            float width,
            int index1, int index2, int index3, int index4,
            float x1, float x2, float x3, float x4,
            float y1, float y2, float y3, float y4
        )
        {
            var widthVector = Vector128.Create(width);

            var pointVector = new PointVector(widthVector);

            pointVector.Indexes
                .Should().Be(Vector128.Create(index1, index2, index3, index4));
            pointVector.X
                .Should().Be(Vector128.Create(x1, x2, x3, x4));
            pointVector.Y
                .Should().Be(Vector128.Create(y1, y2, y3, y4));
        }

        [Theory]
        [InlineData(
            255.0f,
            1, 0, 1, 0,
            0, 4, 2, 5,
            0.0f, 4.0f, 2.0f, 5.0f,
            0.0f, 0.0f, 0.0f, 0.0f
        )]
        [InlineData(
            255.0f,
            1, 1, 1, 1,
            0, 1, 2, 3,
            0.0f, 1.0f, 2.0f, 3.0f,
            0.0f, 0.0f, 0.0f, 0.0f
        )]
        [InlineData(
            255.0f,
            0, 0, 0, 0,
            4, 5, 6, 7,
            4.0f, 5.0f, 6.0f, 7.0f,
            0.0f, 0.0f, 0.0f, 0.0f
        )]
        [InlineData(
            4.0f,
            1, 0, 1, 0,
            0, 4, 2, 5,
            0.0f, 0.0f, 2.0f, 1.0f,
            0.0f, 1.0f, 0.0f, 1.0f
        )]
        public void GivenValidPointVectorAndWidthVectorShouldCalculateValidNextPointVector(
            float width,
            int mask1, int mask2, int mask3, int mask4,
            int index1, int index2, int index3, int index4,
            float x1, float x2, float x3, float x4,
            float y1, float y2, float y3, float y4
        )
        {
            var widthVector = Vector128.Create(width);
            var pointVector = new PointVector(widthVector);
            var mask = Vector128.Create(mask1, mask2, mask3, mask4);

            var nextPointVector = pointVector.Next(mask, widthVector);

            nextPointVector.Indexes
                .Should().Be(Vector128.Create(index1, index2, index3, index4));
            nextPointVector.X
                .Should().Be(Vector128.Create(x1, x2, x3, x4));
            nextPointVector.Y
                .Should().Be(Vector128.Create(y1, y2, y3, y4));
        }
    }
}
