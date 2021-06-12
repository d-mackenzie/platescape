using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Helpers;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.MosaicAlgorithms;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.Services
{
    public class MosaicService
    {
        public LdPart Baseplate { get; set; }

        public LdPart Part { get; set; }

        public Size BaseplateExtent { get; set; }

        public List<LdColor> AllowedColors { get; set; }

        public Size ElementExtent
        {
            get
            {
                return new Size(
                    Baseplate.Size.Width * BaseplateExtent.Width / Part.Size.Width,
                    Baseplate.Size.Height * BaseplateExtent.Height / Part.Size.Height);
            }
        }

        public Mosaic GetMosaic(Bitmap sourceImage, IMosaicAlgorithm mosaicAlgorithm)
        {
            var colors = new LdColor[ElementExtent.Width, ElementExtent.Height];

            Bitmap image = new Bitmap(sourceImage, ScalingHelper.GetBestFitSize(sourceImage.Size, ElementExtent));

            mosaicAlgorithm.Reset();

            for (int y = 0; y < ElementExtent.Height; y++)
            {
                for (int x = 0; x < ElementExtent.Width; x++)
                {
                    colors[x, y] = mosaicAlgorithm.GetColor(new Point(x, y), image.GetPixel(x, y), AllowedColors);
                }
            }

            return new Mosaic(Baseplate, Part, colors);
        }
    }
}
