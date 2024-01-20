using SkiaSharp;
using System;
using System.Diagnostics;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.Core.Algorithms;

namespace TeethInc.Chantry.Core.Services
{
	public class MosaicService
	{
		private ColorService _colorService;
		private LdrawService _ldrawService;

		public MosaicService(LdrawService ldrawService)
		{
			_ldrawService = ldrawService;
			_colorService = new ColorService(_ldrawService);

		}

		public Mosaic GetMosaic(SKBitmap filteredImage, ExtentSettings extentSettings, AlgorithmSettings algorithmSettings)
		{
			var sw = Stopwatch.StartNew();

			SKSizeI elementExtent = extentSettings.ElementExtent;

			var colors = new LdColor[elementExtent.Width, elementExtent.Height];

			using (SKBitmap source = filteredImage.Resize(filteredImage.Info.Size.GetSizeToFill(elementExtent), SKFilterQuality.High))
			{
				SKBitmap mosaic = new SKBitmap(elementExtent.Width, elementExtent.Height);

				var mosaicPixels = mosaic.Pixels;

				var mosaicColors = algorithmSettings.Algorithm.GetMosaic(filteredImage, extentSettings.ElementExtent, algorithmSettings.AllowedColors);

				for (int y = 0; y < elementExtent.Height; y++)
				{
					for (int x = 0; x < elementExtent.Width; x++)
					{
						mosaicPixels[mosaic.GetPixelIndex(x, y)] = mosaicColors[x, y].Color;
					}
				}

				mosaic.Pixels = mosaicPixels;
				mosaic = mosaic.Resize(extentSettings.StudExtent, SKFilterQuality.None);

				Debug.WriteLine($"GetMosaic() done: {sw.ElapsedMilliseconds}ms");

				return new Mosaic(extentSettings.Baseplate, extentSettings.Element, colors, mosaic);
			}
		}
	}
}
