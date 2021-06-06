using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            float scale = sourceImage.Width / ElementExtent.Width;

            mosaicAlgorithm.Reset();

            for (int y = 0; y < ElementExtent.Height; y++)
            {
                for (int x = 0; x < ElementExtent.Width; x++)
                {
                    List<Color> sourceColors = GetColorsInRegion(sourceImage, scale, new Point(x, y)).ToList();

                    colors[x, y] = mosaicAlgorithm.GetColor(new Point(x, y), sourceColors, AllowedColors);
                }
            }

            return new Mosaic(Baseplate, Part, colors);
        }

        private IEnumerable<Color> GetColorsInRegion(Bitmap sourceImage, float scale, Point offset)
        {
            int intScale = (int)scale;
            var ret = new List<Color>();

            Point p = new Point((int)(scale * offset.X), (int)(scale * offset.Y));

            for (int x = p.X; x < p.X + intScale; x++)
            {
                for (int y = p.Y; y < p.Y + intScale; y++)
                {
                    ret.Add(sourceImage.GetPixel(x, y));
                }
            }

            return ret;
        }
    }
}
