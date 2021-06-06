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

        public LdPart Element { get; set; }

        public Size BaseplateExtent { get; set; }

        public LdColor[] AllowedColors { get; set; }

        public Size ElementExtent
        {
            get
            {
                return new Size(
                    Baseplate.Size.Width * BaseplateExtent.Width / Element.Size.Width,
                    Baseplate.Size.Height * BaseplateExtent.Height / Element.Size.Height);
            }
        }

        public LdColor[,] GetMosaic(Bitmap sourceImage, IMosaicAlgorithm mosaicAlgorithm)
        {
            var ret = new LdColor[ElementExtent.Width, ElementExtent.Height];

            float scale = sourceImage.Width / ElementExtent.Width;

            mosaicAlgorithm.Reset();

            for (int y = 0; y < ElementExtent.Height; y++)
            {
                for (int x = 0; x < ElementExtent.Width; x++)
                {
                    Color[] colors = GetColorsInRegion(sourceImage, scale, new Point(x, y));

                    ret[x, y] = mosaicAlgorithm.GetColor(new Point(x, y), colors, AllowedColors);
                }
            }

            return ret;
        }

        private Color[] GetColorsInRegion(Bitmap sourceImage, float scale, Point offset)
        {
            int intScale = (int)scale;
            var ret = new Color[intScale * intScale];
            int index = 0;

            Point p = new Point((int)(scale * offset.X), (int)(scale * offset.Y));

            for (int x = p.X; x < p.X + intScale; x++)
            {
                for (int y = p.Y; y < p.Y + intScale; y++)
                {
                    ret[index++] = sourceImage.GetPixel(x, y);
                }
            }

            return ret;
        }
    }
}
