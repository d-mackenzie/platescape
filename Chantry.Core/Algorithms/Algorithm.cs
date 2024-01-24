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

		public LdColor[,] GetMosaic(SKBitmap sourceBitmap, LdColor[] allowedColors)
		{
			_allowedColors = allowedColors;

			var mosaic = new LdColor[sourceBitmap.Width, sourceBitmap.Height];

			GetMosaic(sourceBitmap, mosaic);

			return mosaic;
		}

		protected abstract void GetMosaic(SKBitmap sourceBitmap, LdColor[,] mosaic);

		protected LdColor GetClosestLdColor(SKColor color)
		{
			return ColorService.GetClosestLdColor(color, _allowedColors);
		}
	}
}
