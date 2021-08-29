using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Extensions;
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
        private const int LDRAW_BLUE = 1;
        private const int LDRAW_RED = 4;
        private const int LDRAW_YELLOW = 14;
        private const int LDRAW_WHITE = 15;

        private ColorService m_colorService;

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
            BaseplateExtent = new Size(4, 2);
            AllowedColors = ldrawService.GetColors(new int[]
            {
                0,
                1,
                2,
                4,
                14,
                15,
                19,
                25,
                27,
                28,
                29,
                70,
                71,
                72,
                272,
                288,
                320,
                322
            }).ToList();

            m_colorService = new ColorService(AllowedColors);
        }

        public Mosaic GetMosaic(Bitmap filteredImage, IMosaicAlgorithm mosaicAlgorithm)
        {
            var sw = Stopwatch.StartNew();

            var colors = new LdColor[ElementExtent.Width, ElementExtent.Height];
            mosaicAlgorithm.Reset(ElementExtent);

            using (Bitmap source = new Bitmap(filteredImage, ScalingHelper.GetBestFitSize(filteredImage.Size, ElementExtent)))
            {
                Bitmap mosaic = new Bitmap(ElementExtent.Width, ElementExtent.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                Point topLeft = new Point(
                    (source.Width - mosaic.Width) / 2,
                    (source.Height - mosaic.Height) / 2);

                (int sourceStride, byte[] sourcePixels) = source.ToPixelArray();
                (int mosaicStride, byte[] mosaicPixels) = mosaic.ToPixelArray();

                for (int y = 0; y < ElementExtent.Height; y++)
                {
                    for (int x = 0; x < ElementExtent.Width; x++)
                    {
                        int sourceIndex = ((y + topLeft.Y) * sourceStride) + ((x + topLeft.X) * 4);
                        int mosaicIndex = (y * mosaicStride) + (x * 4);
                        Color color = Color.FromArgb(sourcePixels[sourceIndex + 2], sourcePixels[sourceIndex + 1], sourcePixels[sourceIndex + 0]);
                        LdColor ldColor = mosaicAlgorithm.GetColor(x, y, color, m_colorService);
                        colors[x, y] = ldColor;
                        mosaicPixels[mosaicIndex + 3] = 255; // alpha.
                        mosaicPixels[mosaicIndex + 2] = ldColor.Color.R;
                        mosaicPixels[mosaicIndex + 1] = ldColor.Color.G;
                        mosaicPixels[mosaicIndex + 0] = ldColor.Color.B;
                    }
                }

                mosaic.SetPixelArray(mosaicPixels);

                Debug.WriteLine($"GetMosaic() done: {sw.ElapsedMilliseconds}ms");

                return new Mosaic(Baseplate, Part, colors, mosaic);
            }
        }
    }
}
