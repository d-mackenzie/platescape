using SkiaSharp;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.Algorithms
{
	public interface IAlgorithm
	{
		string DisplayName { get; }

		void Reset(SKSizeI size);

		LdColor GetColor(int x, int y, SKColor sourceColor, ColorService colorService);
	}
}
