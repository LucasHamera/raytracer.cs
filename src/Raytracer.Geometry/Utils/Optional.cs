using System;

namespace Raytracer.Geometry.Utils
{
    public readonly struct Optional<T>
    {
        public readonly T Value;
        public readonly bool HasValue;

        public Optional(in T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Value = value;
            HasValue = true;
        }

        public static implicit operator T(in Optional<T> optional) 
            => optional.Value;
        public static explicit operator bool(in Optional<T> optional) 
            => optional.HasValue;

        public static explicit operator Optional<T>(in T value)
            => new Optional<T>(value);
    }
}
