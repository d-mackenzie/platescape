using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private const int BASEPLATE_32X32 = 3811;
        private const int PLATE_1X1 = 3024;
        
        private const int LDRAW_BLACK = 0;
        private const int LDRAW_DARK_BLEY = 72;
        private const int LDRAW_LIGHT_BLEY = 71;
        private const int LDRAW_WHITE = 15;


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

        public MosaicService()
        {
            var ldrawService = new LdrawService();

            Baseplate = ldrawService.GetPart(BASEPLATE_32X32);
            Part = ldrawService.GetPart(PLATE_1X1);
            BaseplateExtent = new Size(2, 2);
            AllowedColors = ldrawService.GetColors(new int[] { LDRAW_BLACK, LDRAW_DARK_BLEY, LDRAW_LIGHT_BLEY, LDRAW_WHITE }).ToList();
        }

        public Mosaic GetMosaic(Bitmap sourceImage, IMosaicAlgorithm mosaicAlgorithm)
        {
            var sw = Stopwatch.StartNew();

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

            Debug.WriteLine($"GetMosaic(): {sw.ElapsedMilliseconds}ms");

            return new Mosaic(Baseplate, Part, colors);
        }
    }
}
