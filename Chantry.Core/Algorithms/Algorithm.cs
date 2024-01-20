using SkiaSharp;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.Algorithms
{
	public abstract class Algorithm
	{
		[JsonIgnore]
		public abstract string DisplayName { get; }

		private LdColor[] _allowedColors;

		public LdColor[,] GetMosaic(SKBitmap sourceBitmap, SKSizeI targetSize, LdColor[] allowedColors)
		{
			// generate cropped 2d pixel array.

			SKColor[] sourceBitmapPixels = sourceBitmap.Pixels;
			SKColor[,] croppedSourcePixels = new SKColor[targetSize.Width, targetSize.Height];

			SKPointI topLeft = new SKPointI(
				(sourceBitmap.Width - targetSize.Width) / 2,
				(sourceBitmap.Height - targetSize.Height) / 2);

			for (int y = 0; y < targetSize.Height; y++)
			{
				for (int x = 0; x < targetSize.Width; x++)
				{
					int sourcePixelIndex = ((topLeft.Y + y) * sourceBitmap.Width) + topLeft.X + x;
					croppedSourcePixels[x, y] = sourceBitmapPixels[sourcePixelIndex];
				}
			}

			_allowedColors = allowedColors;

			var mosaic = new LdColor[croppedSourcePixels.GetLength(0), croppedSourcePixels.GetLength(1)];

			GetMosaic(croppedSourcePixels, mosaic);

			return mosaic;
		}

		protected abstract void GetMosaic(SKColor[,] sourcePixels, LdColor[,] mosaic);

		protected LdColor GetClosestLdColor(SKColor color)
		{
			return ColorService.GetClosestLdColor(color, _allowedColors);
		}
	}
}
