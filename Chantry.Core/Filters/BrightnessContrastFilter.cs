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

        public override Bitmap GetFilteredImage(Bitmap unfilteredImage)
        {
            var filteredImage = unfilteredImage.Clone() as Bitmap;
            filteredImage.ApplyFilter(GetAdjustedPixel);
            return filteredImage;
        }

        private Color GetAdjustedPixel(Color unfilteredPixel)
        {
            return Color.FromArgb(
                Math.Clamp(unfilteredPixel.R + Brightness, 0, 255),
                Math.Clamp(unfilteredPixel.G + Brightness, 0, 255),
                Math.Clamp(unfilteredPixel.B + Brightness, 0, 255));
        }
    }
}
