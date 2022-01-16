using SkiaSharp;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.MosaicAlgorithms
{
    public abstract class MosaicAlgorithm
    {
        public abstract void Reset(SKSizeI size);

        public abstract LdColor GetColor(int x, int y, SKColor sourceColor, ColorService colorService);
    }
}
