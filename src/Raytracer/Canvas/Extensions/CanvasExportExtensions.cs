using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Color = Raytracer.Geometry.Base.Models.Color;

namespace Raytracer.Canvas.Extensions
{
    public static class CanvasExportExtensions
    {
        public static void ToBitmap(this Canvas canvas, out Bitmap outBitmap)
        {
            var colors = canvas.Data;
            if (colors.IsEmpty)
            {
                outBitmap = new Bitmap(0, 0);
                return;
            }

            var width = canvas.Width;
            var height = canvas.Height;
            outBitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            var floatValues = MemoryMarshal.Cast<Color, float>(colors);

            var bitmapData = outBitmap.LockBits(
                new Rectangle(Point.Empty, outBitmap.Size),
                ImageLockMode.WriteOnly,
                outBitmap.PixelFormat
            );
            try
            {
                unsafe
                {
                    var bytesPtr = (byte*) bitmapData.Scan0.ToPointer();

                    fixed (float* floatValuesPtr = floatValues)
                    {
                        var floatIndex = 0;
                        for (int y = 0; y < height; y++)
                        for (int x = 0; x < width; x++, floatIndex += 3)
                        {
                            var byteIndex = y * bitmapData.Stride + 3 * x;
                            // NOTE: data is stored in BGR order
                            *(bytesPtr + byteIndex + 0) = (byte)(byte.MaxValue * *(floatValuesPtr + floatIndex + 2));
                            *(bytesPtr + byteIndex + 1) = (byte)(byte.MaxValue * *(floatValuesPtr + floatIndex + 1));
                            *(bytesPtr + byteIndex + 2) = (byte)(byte.MaxValue * *(floatValuesPtr + floatIndex + 0));
                        }
                    }
                }
            }
            finally
            {
                outBitmap.UnlockBits(bitmapData);
            }
        }

        public static void ToFile(this Canvas canvas, string path)
        {
            canvas.ToBitmap(out var bitmap);
            try
            {
                bitmap.Save(path);
            }
            finally
            {
                bitmap.Dispose();
            }
        }
    }
}