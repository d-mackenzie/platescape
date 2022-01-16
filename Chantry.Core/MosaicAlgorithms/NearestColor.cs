using SkiaSharp;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.MosaicAlgorithms
{
    public class NearestColor : MosaicAlgorithm
    {
        public override LdColor GetColor(int x, int y, SKColor sourceColor, ColorService colorService)
        {
            return colorService.GetClosestLdColor(sourceColor);
        }

        public override void Reset(SKSizeI size)
        { }
    }
}
