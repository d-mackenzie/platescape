using SkiaSharp;
using System;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.Core.Models
{
	public class Mosaic
	{
		private Lazy<SKBitmap> _image;

		public SKSizeI BaseplateSize { get; }

		public SKSizeI ElementSize { get; }

		public LdColor[,] Colors { get; }

		public SKBitmap Image => _image.Value;

		public SKSizeI ElementExtent => new SKSizeI(Colors.GetLength(0), Colors.GetLength(1));

		public SKSizeI StudExtent => new SKSizeI(ElementExtent.Width * ElementSize.Width, ElementExtent.Height * ElementSize.Height);

		public SKSizeI BaseplateExtent => new SKSizeI(StudExtent.Width / BaseplateSize.Width, StudExtent.Height / BaseplateSize.Height);

		public SKSizeI ElementExtentPerBaseplate => new SKSizeI(BaseplateSize.Width / ElementSize.Width, BaseplateSize.Height / ElementSize.Height);

		public Mosaic(SKSizeI baseplateSize, SKSizeI elementSize, LdColor[,] colors)
		{
			BaseplateSize = baseplateSize;
			ElementSize = elementSize;
			Colors = colors;
			_image = new Lazy<SKBitmap>(GetBitmap);
		}

		public Mosaic GetBaseplateAsMosaic(int x, int y)
		{
			SKRectI baseplateBounds = GetElementBoundsForBaseplate(x, y);

			return new Mosaic(
				BaseplateSize,
				ElementSize,
				Colors.GetRect(baseplateBounds));
		}

		private SKRectI GetElementBoundsForBaseplate(int x, int y)
		{
			return new SKRectI()
			{
				Left = x * ElementExtentPerBaseplate.Width,
				Top = y * ElementExtentPerBaseplate.Height,
				Size = ElementExtentPerBaseplate
			};
		}

		private SKBitmap GetBitmap()
		{
			SKBitmap mosaicImage = new SKBitmap(ElementExtent.Width, ElementExtent.Height);
			var mosaicPixels = mosaicImage.Pixels;

			for (int y = 0; y < ElementExtent.Height; y++)
			{
				for (int x = 0; x < ElementExtent.Width; x++)
				{
					mosaicPixels[mosaicImage.GetPixelIndex(x, y)] = Colors[x, y].Color;
				}
			}

			mosaicImage.Pixels = mosaicPixels;
			mosaicImage = mosaicImage.Resize(StudExtent, SKFilterQuality.None);

			return mosaicImage;
		}
	}
}
