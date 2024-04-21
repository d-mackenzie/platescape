using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.Core.Exporters
{
	public class PngExporter : IExporter
	{
		private ExportPngSettings _settings;

		public PngExporter(ExportPngSettings settings)
		{
			_settings = settings;
		}

		public void Export(Mosaic mosaic, Stream stream)
		{
			// create png.

			var bitmap = new SKBitmap(
				mosaic.StudExtent.Width * _settings.PixelsPerStud,
				mosaic.StudExtent.Height * _settings.PixelsPerStud);

			var canvas = new SKCanvas(bitmap);

			for (int x = 0; x <= mosaic.Colors.GetUpperBound(0); x++)
			{
				for (int y = 0; y <= mosaic.Colors.GetUpperBound(1); y++)
				{
					canvas.DrawRect(
						GetElementRect(mosaic, x, y),
						new SKPaint()
						{
							Color = mosaic.Colors[x, y].Color,
							Style = SKPaintStyle.Fill
						});
				}
			}

			// stream out.

			bitmap.Encode(stream, SKEncodedImageFormat.Png, 1);
		}

		private SKRectI GetElementRect(Mosaic mosaic, int x, int y)
		{
			return new SKRectI(
				x * mosaic.ElementSize.Width * _settings.PixelsPerStud,
				y * mosaic.ElementSize.Height * _settings.PixelsPerStud,
				(x * mosaic.ElementSize.Width + mosaic.ElementSize.Width) * _settings.PixelsPerStud,
				(y * mosaic.ElementSize.Height + mosaic.ElementSize.Height) * _settings.PixelsPerStud);
		}
	}
}
