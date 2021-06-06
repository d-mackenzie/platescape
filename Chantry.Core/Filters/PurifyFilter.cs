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
    public class PurifyFilter : Filter
    {
        public float Factor { get; set; }

        public override Bitmap GetFilteredImage(Bitmap unfilteredImage)
        {
            var filteredImage = unfilteredImage.Clone() as Bitmap;
            filteredImage.ApplyFilter(GetAdjustedPixel);
            return filteredImage;
        }

        private Color GetAdjustedPixel(Color unfilteredPixel)
        {
            float saturation = unfilteredPixel.GetSaturation();
            float brightness = unfilteredPixel.GetBrightness();

            saturation = Math.Clamp(saturation + Factor, 0, 1f);
//            brightness = Math.Clamp(brightness + Factor, 0, 1f);

            return ColorExtensions.FromHsv(unfilteredPixel.GetHue(), saturation, brightness);
        }
    }
}
