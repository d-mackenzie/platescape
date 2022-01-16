using SkiaSharp;
using System;

namespace TeethInc.Chantry.Core.Extensions
{
    public static class BitmapExtensions
    {
        public static void ApplyFilter(this SKBitmap bitmap, Func<SKColor, SKColor> func)
        {
            var pixels = bitmap.Pixels;

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = func.Invoke(pixels[i]);
            }

            bitmap.Pixels = pixels;
        }
    }
}
