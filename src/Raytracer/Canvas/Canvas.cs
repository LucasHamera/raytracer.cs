using System;
using Raytracer.Geometry.Baseline;

namespace Raytracer.Canvas
{
    public class Canvas
    {
        private readonly Color[] _pixels;

        public int Width { get; }
        public int Height { get; }

        public ReadOnlySpan<Color> Data => _pixels.AsSpan();

        public Color this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Width) throw new ArgumentException(nameof(x));
                if (y < 0 || y >= Height) throw new ArgumentException(nameof(y));
                return _pixels[y * Width + x];
            }
            set
            {
                if (x < 0 || x >= Width) throw new ArgumentException(nameof(x));
                if (y < 0 || y >= Height) throw new ArgumentException(nameof(y));
                _pixels[y * Width + x] = value;
            }
        }

        public Canvas(int width, int height)
        {
            if (width <= 0) throw new ArgumentException(nameof(width));
            if (height <= 0) throw new ArgumentException(nameof(height));

            _pixels = new Color[width * height];
            Width = width;
            Height = height;
        }
    }
}