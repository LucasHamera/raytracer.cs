using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

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

            Execute.Assertion.BecauseOf(because, becauseArgs)
                .ForCondition(Math.Abs(diffValueExpected.GetElement(0)) <= precision);
            Execute.Assertion.BecauseOf(because, becauseArgs)
                .ForCondition(Math.Abs(diffValueExpected.GetElement(1)) <= precision);
            Execute.Assertion.BecauseOf(because, becauseArgs)
                .ForCondition(Math.Abs(diffValueExpected.GetElement(2)) <= precision);
            Execute.Assertion.BecauseOf(because, becauseArgs)
                .ForCondition(Math.Abs(diffValueExpected.GetElement(3)) <= precision);

            return new AndConstraint<ObjectAssertions>(assertions);
        }
    }
}
