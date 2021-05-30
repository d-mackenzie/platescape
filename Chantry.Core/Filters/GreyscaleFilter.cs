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
        public override Bitmap GetFilteredImage(Bitmap unfilteredImage)
        {
            var filteredImage = new Bitmap(unfilteredImage);

            for (int x = 0; x < filteredImage.Width; x++)
            {
                for (int y = 0; y < filteredImage.Height; y++)
                {
                    filteredImage.SetPixel(x, y, Color.FromArgb(
                        filteredImage.GetPixel(x, y).R,
                        filteredImage.GetPixel(x, y).G,
                        filteredImage.GetPixel(x, y).B));
                }
            }

            return filteredImage;
        }
    }
}
