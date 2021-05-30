using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Filters
{
    public class GreyscaleFilter : Filter
    {
        public GreyscaleFilter() : base()
        {
        }

        public override Bitmap GetFilteredImage(Bitmap unfilteredImage)
        {
            var filteredImage = new Bitmap(unfilteredImage);
            
            for (int x = 0; x < filteredImage.Width; x++)
            {
                for (int y = 0; y < filteredImage.Height; y++)
                {
                    Color unfilteredPixel = filteredImage.GetPixel(x, y);
                    Color filteredPixel = Color.FromArgb(unfilteredPixel.R, unfilteredPixel.R, unfilteredPixel.R);

                    filteredImage.SetPixel(x, y, filteredPixel);
                }
            }

            return filteredImage;
        }
    }
}
