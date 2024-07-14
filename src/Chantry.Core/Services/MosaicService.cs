using SkiaSharp;
using System;
using System.Diagnostics;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Logging;
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

				using (SKBitmap croppedImage = new SKBitmap(cropRect.Width, cropRect.Height))
				{
					resizedImage.ExtractSubset(croppedImage, cropRect);

					var mosaicColors = algorithmSettings.Algorithm.GetMosaic(croppedImage, algorithmSettings.AllowedColors);

					Logger.Trace($"{sw.ElapsedMilliseconds}ms");

					return new Mosaic(extentSettings.BaseplateSize, extentSettings.ElementSize, mosaicColors);
				}
			}
		}
	}
}
