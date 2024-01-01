using SkiaSharp;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.Algorithms
{
	public class NearestColor : IAlgorithm
	{
		public string DisplayName => "Nearest Colour";

		public LdColor GetColor(int x, int y, SKColor sourceColor, ColorService colorService)
		{
			return colorService.GetClosestLdColor(sourceColor);
		}

		public void Reset(SKSizeI size)
		{ }
	}
}
