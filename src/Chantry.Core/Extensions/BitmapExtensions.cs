using SkiaSharp;
using System;
using System.Runtime.CompilerServices;

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

		public static int GetPixelIndex(this SKBitmap bitmap, int x, int y)
		{
			return (y * bitmap.Width) + x;
		}

		public static SKBitmap GetCrop(this SKBitmap bitmap, SKRectI rect)
		{
			using var ret = new SKBitmap(rect.Width, rect.Height);
			bitmap.ExtractSubset(ret, rect);

			return ret;
		}
	}
}
