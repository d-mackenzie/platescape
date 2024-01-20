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

		public NearestColor(ColorService colorService) : base(colorService) { }

		protected override void GetMosaic(SKColor[,] sourcePixels, LdColor[,] mosaic)
		{
			foreach ((int x, int y, SKColor sourceColor) in sourcePixels.Each())
			{
				mosaic[x, y] = GetClosestLdColor(sourceColor);
			}
		}
	}
}
