using SkiaSharp;
using System;
using System.Diagnostics;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.Core.Services
{
	public class MosaicService
	{
		public Mosaic GetMosaic(SKBitmap filteredImage, ExtentSettings extentSettings, AlgorithmSettings algorithmSettings)
		{
			var sw = Stopwatch.StartNew();

			SKSizeI elementExtent = extentSettings.ElementExtent;

			using (SKBitmap resizedImage = filteredImage.Resize(filteredImage.Info.Size.GetSizeToFill(elementExtent), SKFilterQuality.High))
			{
				SKRectI cropRect = new SKRectI(
					(resizedImage.Width - elementExtent.Width) / 2,
					(resizedImage.Height - elementExtent.Height) / 2,
					((resizedImage.Width - elementExtent.Width) / 2) + elementExtent.Width,
					((resizedImage.Height - elementExtent.Height) / 2) + elementExtent.Height);

				using (SKBitmap croppedImage = resizedImage.GetCrop(cropRect))
				{
					var mosaicColors = algorithmSettings.Algorithm.GetMosaic(resizedImage, algorithmSettings.AllowedColors);

					Debug.WriteLine($"GetMosaic() done: {sw.ElapsedMilliseconds}ms");

					return new Mosaic(extentSettings.Baseplate, extentSettings.Element, mosaicColors, GetMosaicBitmap(mosaicColors, extentSettings));
				}
			}
		}

		private SKBitmap GetMosaicBitmap(LdColor[,] mosaicColors, ExtentSettings extentSettings)
		{
			SKBitmap mosaicImage = new SKBitmap(extentSettings.ElementExtent.Width, extentSettings.ElementExtent.Height);
			var mosaicPixels = mosaicImage.Pixels;

			for (int y = 0; y < extentSettings.ElementExtent.Height; y++)
			{
				for (int x = 0; x < extentSettings.ElementExtent.Width; x++)
				{
					mosaicPixels[mosaicImage.GetPixelIndex(x, y)] = mosaicColors[x, y].Color;
				}
			}

			mosaicImage.Pixels = mosaicPixels;
			mosaicImage = mosaicImage.Resize(extentSettings.StudExtent, SKFilterQuality.None);

			return mosaicImage;
		}
	}
}
