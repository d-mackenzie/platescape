using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Extensions;

namespace TeethInc.Chantry.Core.Filters
{
    public class BrightnessContrastFilter : Filter
    {
        public int Brightness { get; set; }

        public int Contrast { get; set; }

        public TonalRange TonalRange { get; set; } = TonalRange.All;

        public override Bitmap GetFilteredImage(Bitmap unfilteredImage)
        {
            var filteredImage = unfilteredImage.Clone() as Bitmap;
            filteredImage.ApplyFilter(GetAdjustedPixel);
            return filteredImage;
        }

        private Color GetAdjustedPixel(Color unfilteredPixel)
        {
            int red = unfilteredPixel.R;
            int green = unfilteredPixel.G;
            int blue = unfilteredPixel.B;

            // brightness.

            red += Brightness;
            green += Brightness;
            blue += Brightness;

            // contrast.

            float factor = 259f * (Contrast + 255f) / (255f * (259f - Contrast));

            red = (int)(factor * (red - 128) + 128);
            green = (int)(factor * (green - 128) + 128);
            blue = (int)(factor * (blue - 128) + 128);

            return Color.FromArgb(
                Math.Clamp(red, 0, 255),
                Math.Clamp(green, 0, 255),
                Math.Clamp(blue, 0, 255));
        }
    }
}
