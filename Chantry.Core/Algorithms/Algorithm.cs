using SkiaSharp;
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
			// convert source pixels into 2d array.

			SKColor[] sourceBitmapPixels = sourceBitmap.Pixels;
			SKColor[,] sourcePixels = new SKColor[targetSize.Width, targetSize.Height];

			SKPointI topLeft = new SKPointI(
				(sourceBitmap.Width - targetSize.Width) / 2,
				(sourceBitmap.Height - targetSize.Height) / 2);

			for (int y = 0; y < targetSize.Height; y++)
			{
				for (int x = 0; x < targetSize.Width; x++)
				{
					int sourcePixelIndex = ((topLeft.Y + y) * targetSize.Width) + topLeft.X + x;
					sourcePixels[x, y] = sourceBitmapPixels[sourcePixelIndex];
				}
			}

			_allowedColors = allowedColors;

			var mosaic = new LdColor[sourcePixels.GetLength(0), sourcePixels.GetLength(1)];

			GetMosaic(sourcePixels, mosaic);

			return mosaic;
		}

		protected abstract void GetMosaic(SKColor[,] sourcePixels, LdColor[,] mosaic);

		protected LdColor GetClosestLdColor(SKColor color)
		{
			return ColorService.GetClosestLdColor(color, _allowedColors);
		}
	}
}
