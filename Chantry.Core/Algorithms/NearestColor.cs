using SkiaSharp;
using System;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.Algorithms
{
	public class NearestColor : Algorithm
	{
		public override string DisplayName => "Nearest Colour";

		protected override void GetMosaic(SKBitmap sourceImage, LdColor[,] mosaic)
		{
			foreach ((int x, int y, SKColor sourceColor) in sourceImage.Pixels.As2dIEnumerable(sourceImage.Width, sourceImage.Height))
			{
				mosaic[x, y] = GetClosestLdColor(sourceColor);
			}
		}
	}
}
