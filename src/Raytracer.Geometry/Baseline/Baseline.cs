using Raytracer.Geometry.Common;

namespace Raytracer.Geometry.Baseline
{
    public class BaselineGeometry : IGeometry<float, Vec3, Color>
    {
        public float Sqrt(in float value)
        {
            var curr = value;
            var prev = 0.0f;

            while (curr != prev)
            {
                prev = curr;
                curr = 0.5f * (curr + value / curr);
            }

            return curr;
        }

        public float Pow(in float @base, int exp)
        {
            var val = 1.0f;
            while (exp > 0)
            {
                val *= @base;
                exp--;
            }

            return val;
        }

        public float Floor(in float value) => (int) (value >= 0 ? value : value - 1.0f);

        public float Clamp(in float value, in float min, in float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public float Dot(in Vec3 left, in Vec3 right) => left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        public float Mag(in Vec3 vector) => Sqrt(Dot(vector, vector));

        public Vec3 Norm(in Vec3 vector)
        {
            var mag = Mag(vector);
            return new Vec3(vector.X / mag, vector.Y / mag, vector.Z / mag);
        }

        public Vec3 Cross(in Vec3 v1, in Vec3 v2)
        {
            return new Vec3(
                v1.Y * v2.Z - v1.Z * v2.Y,
                v1.Z * v2.X - v1.X * v2.Z,
                v1.X * v2.Y - v1.Y * v2.X
            );
        }

        public Color Scale(in float value, in Color color)
        {
            return new Color(
                color.R * value,
                color.G * value,
                color.B * value
            );
        }
    }
}