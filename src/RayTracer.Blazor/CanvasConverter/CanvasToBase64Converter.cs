using System.Runtime.InteropServices;
using Raytracer.Canvas;
using Raytracer.Geometry.Models;

namespace RayTracer.Blazor.CanvasConverter
{
    public static class CanvasToBase64Converter
    {
        #region BMPHeader

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public readonly struct BMPHeader
        {
            [FieldOffset(0)]
            public readonly short Signature;
            [FieldOffset(2)]
            public readonly int FileSize;
            [FieldOffset(6)]
            public readonly int Reserved;
            [FieldOffset(10)]
            public readonly int Offset;
            [FieldOffset(14)]
            public readonly int BitmapInfoHeaderSize;
            [FieldOffset(18)]
            public readonly int Width;
            [FieldOffset(22)]
            public readonly int Height;
            [FieldOffset(26)]
            public readonly short PlanesNumber;
            [FieldOffset(28)]
            public readonly short BitsPerPixel;
            [FieldOffset(30)]
            public readonly int Compression;
            [FieldOffset(34)]
            public readonly int ImageDataSize;
            [FieldOffset(38)]
            public readonly int HorizontalResolution;
            [FieldOffset(42)]
            public readonly int VerticalResolution;
            [FieldOffset(46)]
            public readonly int ColorsNumber;
            [FieldOffset(50)]
            public readonly int ImportantColorsNumber;

            public BMPHeader(
                int fileSize,
                int width,
                int height,
                short bitsPerPixel,
                int compression,
                int imageDataSize,
                int horizontalResolution,
                int verticalResolution,
                int colorsNumber,
                int importantColorsNumber
            )
            {
                Signature = 0x4D42;
                FileSize = fileSize;
                Reserved = 0;
                Offset = 54;
                BitmapInfoHeaderSize = 40;
                Width = width;
                Height = height;
                PlanesNumber = 1;
                BitsPerPixel = bitsPerPixel;
                Compression = compression;
                ImageDataSize = imageDataSize;
                HorizontalResolution = horizontalResolution;
                VerticalResolution = verticalResolution;
                ColorsNumber = colorsNumber;
                ImportantColorsNumber = importantColorsNumber;
            }
        }

        #endregion

        public static string Convert(Canvas canvas)
        {
            var colors = canvas.Data;

            var floatValues = MemoryMarshal.Cast<Color, float>(colors);
            var imageBytes = new byte[floatValues.Length + Marshal.SizeOf<BMPHeader>()];

            var width = canvas.Width;
            var height = canvas.Height;

            var header = new BMPHeader(
                imageBytes.Length,
                width,
                height,
                24,
                0,
                floatValues.Length,
                0,
                0,
                3,
                0
            );

            unsafe
            {
                var headerHandle = GCHandle.Alloc(header, GCHandleType.Pinned);
                try
                {
                    var headerPtr = headerHandle.AddrOfPinnedObject();
                    Marshal.Copy(headerPtr, imageBytes, 0, sizeof(BMPHeader));

                    fixed(float* floatValuesPtr = floatValues)
                    fixed(byte* imageBytesPtr = imageBytes)
                    {
                        var floatIndex = 0;
                        for (int y = 0; y < height; y++)
                        for (int x = 0; x < width; x++, floatIndex += 3)
                        {
                            var byteIndex = 
                                            (-3 * (y - height) * width) // 00 lewy dolny, więc y - height, - przy 3 aby przeciwna (inaczej ujemne)
                                            + 3 * x                     // stride
                                            + sizeof(BMPHeader);        // header
                            // NOTE: data is stored in BGR order
                            *(imageBytesPtr + byteIndex + 0) = (byte)(byte.MaxValue * *(floatValuesPtr + floatIndex + 2));
                            *(imageBytesPtr + byteIndex + 1) = (byte)(byte.MaxValue * *(floatValuesPtr + floatIndex + 1));
                            *(imageBytesPtr + byteIndex + 2) = (byte)(byte.MaxValue * *(floatValuesPtr + floatIndex + 0));
                        }
                    }
                }
                finally
                {
                    headerHandle.Free();
                }
                return System.Convert.ToBase64String(imageBytes);
            }
        }
    }
}
