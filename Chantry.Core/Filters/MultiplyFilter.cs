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
    public class MultiplyFilter : Filter
    {
        public override string DisplayName => "Multiply";

        public float Factor { get; set; }

        public override void ApplyFilter(Bitmap image)
        {
            image.ApplyFilter(GetAdjustedPixel);
        }

        private Color GetAdjustedPixel(Color unfilteredPixel)
        {
            return Color.FromArgb(
                Math.Clamp((int)(unfilteredPixel.R * Factor), 0, 255),
                Math.Clamp((int)(unfilteredPixel.G * Factor), 0, 255),
                Math.Clamp((int)(unfilteredPixel.B * Factor), 0, 255));
        }
    }
}
