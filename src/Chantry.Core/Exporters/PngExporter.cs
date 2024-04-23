using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		private float _strokeWidth;

		public PngExporter(ExportPngSettings settings)
		{
			_settings = settings;
			_strokeWidth = _settings.PixelsPerStud * 0.04f;

		}

		public void Export(Mosaic mosaic, Stream stream)
		{
			var sw = Stopwatch.StartNew();

			var bitmap = new SKBitmap(
				mosaic.StudExtent.Width * _settings.PixelsPerStud,
				mosaic.StudExtent.Height * _settings.PixelsPerStud);

			var canvas = new SKCanvas(bitmap);

			DrawElements(mosaic, canvas);

			if (_settings.DrawOutlines)
			{
				DrawOutlines(mosaic, canvas, SKColors.Black, x => x.Number != 0);
				DrawOutlines(mosaic, canvas, SKColors.White, x => x.Number == 0);
			}

			if (_settings.DrawStuds)
			{
				DrawStuds(mosaic, canvas, SKColors.Black, x => x.Number != 0);
				DrawStuds(mosaic, canvas, SKColors.White, x => x.Number == 0);
			}

			// save.

			bitmap.Encode(stream, SKEncodedImageFormat.Png, 1);

			Debug.WriteLine($"PngExporter.Export(): {sw.ElapsedMilliseconds}ms");
		}

		private void DrawElements(Mosaic mosaic, SKCanvas canvas)
		{
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
		}

		private void DrawOutlines(Mosaic mosaic, SKCanvas canvas, SKColor strokeColor, Func<LdColor, bool> filter)
		{
			for (int x = 0; x < mosaic.ElementExtent.Width; x++)
			{
				for (int y = 0; y < mosaic.ElementExtent.Height; y++)
				{
					if (filter.Invoke(mosaic.Colors[x, y]))
					{
						canvas.DrawRect(
							GetElementRect(mosaic, x, y),
							new SKPaint()
							{
								IsAntialias = true,
								Color = strokeColor,
								StrokeWidth = _strokeWidth,
								Style = SKPaintStyle.Stroke
							});
					}
				}
			}
		}

		private void DrawStuds(Mosaic mosaic, SKCanvas canvas, SKColor strokeColor, Func<LdColor, bool> filter)
		{
			for (int x = 0; x < mosaic.StudExtent.Width; x++)
			{
				for (int y = 0; y < mosaic.StudExtent.Height; y++)
				{
					int elementX = x / mosaic.ElementSize.Width;
					int elementY = y / mosaic.ElementSize.Height;

					if (filter.Invoke(mosaic.Colors[elementX, elementY]))
					{
						canvas.DrawCircle(
							GetStudCentre(mosaic, x, y),
							_settings.PixelsPerStud * 0.3f,
							new SKPaint()
							{
								IsAntialias = true,
								Color = strokeColor,
								StrokeWidth = _strokeWidth,
								Style = SKPaintStyle.Stroke
							});
					}
				}
			}
		}

		private SKRectI GetElementRect(Mosaic mosaic, int x, int y)
		{
			return new SKRectI(
				x * mosaic.ElementSize.Width * _settings.PixelsPerStud,
				y * mosaic.ElementSize.Height * _settings.PixelsPerStud,
				(x * mosaic.ElementSize.Width + mosaic.ElementSize.Width) * _settings.PixelsPerStud,
				(y * mosaic.ElementSize.Height + mosaic.ElementSize.Height) * _settings.PixelsPerStud);
		}
		private SKPoint GetStudCentre(Mosaic mosaic, int x, int y)
		{
			return new SKPoint(
				x * _settings.PixelsPerStud + (_settings.PixelsPerStud / 2),
				y * _settings.PixelsPerStud + (_settings.PixelsPerStud / 2));
		}
	}
}
