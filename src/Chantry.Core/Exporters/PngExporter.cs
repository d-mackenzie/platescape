using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.Core.Exporters
{
	public class PngExporter : IExporter
	{
		public int PixelsPerStud { get; set; } = 1;

		public bool DrawStuds { get; set; }

		public bool DrawOutlines { get; set; }

		public string DefaultExtension => "png";

		public bool IsValid => (PixelsPerStud > 0);

		private float StrokeWidth => PixelsPerStud * 0.04f;

		public void Export(Mosaic mosaic, Stream stream)
		{
			var sw = Stopwatch.StartNew();

			var bitmap = new SKBitmap(
				mosaic.StudExtent.Width * PixelsPerStud,
				mosaic.StudExtent.Height * PixelsPerStud);

			var canvas = new SKCanvas(bitmap);

			PaintElements(mosaic, canvas);

			if (DrawOutlines)
			{
				PaintOutlines(mosaic, canvas, SKColors.Black, x => x.Number != 0);
				PaintOutlines(mosaic, canvas, SKColors.White, x => x.Number == 0);
			}

			if (DrawStuds)
			{
				PaintStuds(mosaic, canvas, SKColors.Black, x => x.Number != 0);
				PaintStuds(mosaic, canvas, SKColors.White, x => x.Number == 0);
			}

			// save.

			using (var pixmap = bitmap.PeekPixels())
			{
				pixmap.Encode(stream, new SKPngEncoderOptions()
				{
					FilterFlags = SKPngEncoderFilterFlags.None,
					ZLibLevel = 5
				});
			}

			Debug.WriteLine($"PngExporter.Export(): {sw.ElapsedMilliseconds}ms");
		}

		private void PaintElements(Mosaic mosaic, SKCanvas canvas)
		{
			for (int x = 0; x < mosaic.ElementExtent.Width; x++)
			{
				for (int y = 0; y < mosaic.ElementExtent.Height; y++)
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

		private void PaintOutlines(Mosaic mosaic, SKCanvas canvas, SKColor strokeColor, Func<LdColor, bool> filter)
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
								StrokeWidth = StrokeWidth,
								Style = SKPaintStyle.Stroke
							});
					}
				}
			}
		}

		private void PaintStuds(Mosaic mosaic, SKCanvas canvas, SKColor strokeColor, Func<LdColor, bool> filter)
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
							PixelsPerStud * 0.3f,
							new SKPaint()
							{
								IsAntialias = true,
								Color = strokeColor,
								StrokeWidth = StrokeWidth,
								Style = SKPaintStyle.Stroke
							});
					}
				}
			}
		}

		private SKRectI GetElementRect(Mosaic mosaic, int x, int y)
		{
			return new SKRectI(
				x * mosaic.ElementSize.Width * PixelsPerStud,
				y * mosaic.ElementSize.Height * PixelsPerStud,
				(x * mosaic.ElementSize.Width + mosaic.ElementSize.Width) * PixelsPerStud,
				(y * mosaic.ElementSize.Height + mosaic.ElementSize.Height) * PixelsPerStud);
		}
		private SKPoint GetStudCentre(Mosaic mosaic, int x, int y)
		{
			return new SKPoint(
				x * PixelsPerStud + (PixelsPerStud / 2),
				y * PixelsPerStud + (PixelsPerStud / 2));
		}
	}
}
