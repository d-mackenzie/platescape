using SkiaSharp;
using System;
using TeethInc.Chantry.Core.Extensions;

namespace TeethInc.Chantry.Core.Filters
{
    public class BrightnessContrastFilter : Filter
    {
        public override string DisplayName => "Brightness/Contrast";

        public int Brightness { get; set; }

        public int Contrast { get; set; }

        public TonalRange TonalRange { get; set; } = TonalRange.All;

        public override void ApplyFilter(SKBitmap image)
        {
            image.ApplyFilter(GetAdjustedPixel);
        }

        private SKColor GetAdjustedPixel(SKColor unfilteredPixel)
        {
            int red = unfilteredPixel.Red;
            int green = unfilteredPixel.Green;
            int blue = unfilteredPixel.Blue;

            // brightness.

            red += Brightness;
            green += Brightness;
            blue += Brightness;

            // contrast.

            float factor = 259f * (Contrast + 255f) / (255f * (259f - Contrast));

            red = Math.Clamp((int)(factor * (red - 128) + 128), 0, 255);
            green = Math.Clamp((int)(factor * (green - 128) + 128), 0, 255);
            blue = Math.Clamp((int)(factor * (blue - 128) + 128), 0, 255);

            return new SKColor(
                (byte)red,
                (byte)green,
                (byte)blue);
        }
    }
}
