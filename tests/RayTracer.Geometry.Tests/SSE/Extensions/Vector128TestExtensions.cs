using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using FluentAssertions;
using FluentAssertions.Primitives;
using Xunit;

namespace RayTracer.Geometry.Tests.SSE.Extensions
{
    public static class Vector128TestExtensions
    {
        public static AndConstraint<ObjectAssertions> BeApproximately(
            this ObjectAssertions assertions,
            in Vector128<float> value,
            in Vector128<float> expectedValue,
            in float precision,
            string because = "",
            params object[] becauseArgs
        )
        {
            var diffValueExpected = Sse.Subtract(value, expectedValue);

            Assert.InRange(Math.Abs(diffValueExpected.GetElement(0)), 0, precision);
            Assert.InRange(Math.Abs(diffValueExpected.GetElement(1)), 0, precision);
            Assert.InRange(Math.Abs(diffValueExpected.GetElement(2)), 0, precision);
            Assert.InRange(Math.Abs(diffValueExpected.GetElement(3)), 0, precision);

            return new AndConstraint<ObjectAssertions>(assertions);
        }
    }
}
