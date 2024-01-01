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
			_colorService.AllowedColors = algorithmSettings.AllowedColors;

			var sw = Stopwatch.StartNew();

			SKSizeI elementExtent = extentSettings.ElementExtent;

			var colors = new LdColor[elementExtent.Width, elementExtent.Height];
			algorithmSettings.Algorithm.Reset(extentSettings.ElementExtent);

			using (SKBitmap source = filteredImage.Resize(filteredImage.Info.Size.GetSizeToFill(elementExtent), SKFilterQuality.High))
			{
				SKBitmap mosaic = new SKBitmap(elementExtent.Width, elementExtent.Height);

				SKPointI topLeft = new SKPointI(
					(source.Width - mosaic.Width) / 2,
					(source.Height - mosaic.Height) / 2);

				var sourcePixels = source.Pixels;
				var mosaicPixels = mosaic.Pixels;

				for (int y = 0; y < elementExtent.Height; y++)
				{
					for (int x = 0; x < elementExtent.Width; x++)
					{
						SKColor color = sourcePixels[source.GetPixelIndex(topLeft.X + x, topLeft.Y + y)];
						LdColor ldColor = algorithmSettings.Algorithm.GetColor(x, y, color, _colorService);
						colors[x, y] = ldColor;
						mosaicPixels[mosaic.GetPixelIndex(x, y)] = ldColor.Color;
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
