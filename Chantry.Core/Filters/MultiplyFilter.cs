using SkiaSharp;
using System;
using TeethInc.Chantry.Core.Extensions;

namespace TeethInc.Chantry.Core.Filters
{
    public class MultiplyFilter : Filter
    {
        public override string DisplayName => "Multiply";

        public float Factor { get; set; }

        public override void ApplyFilter(SKBitmap image)
        {
            image.ApplyFilter(GetAdjustedPixel);
        }

        private SKColor GetAdjustedPixel(SKColor unfilteredPixel)
        {
            return new SKColor(
                (byte)Math.Clamp((int)(unfilteredPixel.Red * Factor), 0, 255),
                (byte)Math.Clamp((int)(unfilteredPixel.Green * Factor), 0, 255),
                (byte)Math.Clamp((int)(unfilteredPixel.Blue * Factor), 0, 255));
        }
    }
}
