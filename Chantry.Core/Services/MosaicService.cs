using SkiaSharp;
using System.Diagnostics;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.MosaicAlgorithms;

namespace TeethInc.Chantry.Core.Services
{
    public class MosaicService
    {
        private const string BASEPLATE_32X32 = "3811";
        private const string PLATE_1X1 = "3024";
        
        private const int LDRAW_BLACK = 0;
        private const int LDRAW_BLUE = 1;
        private const int LDRAW_RED = 4;
        private const int LDRAW_YELLOW = 14;
        private const int LDRAW_WHITE = 15;

        private ColorService m_colorService;
        private LdrawService m_ldrawService;

        private LdPart m_baseplate;
        private LdPart m_element;

        public string Baseplate
        {
            get { return m_baseplate.Number; }
            set { m_baseplate = m_ldrawService.GetPart(value); }
        }

        public string Element
        {
            get { return m_element.Number; }
            set { m_element = m_ldrawService.GetPart(value); }
        }

        public SKSizeI BaseplateExtent { get; set; }

        public int[] AllowedColors
        {
            get { return m_colorService.AllowedColors; }
            set { m_colorService.AllowedColors = value; }
        }

        public SKSizeI ElementExtent
        {
            get
            {
                return new SKSizeI(
                    m_baseplate.Size.Width * BaseplateExtent.Width / m_element.Size.Width,
                    m_baseplate.Size.Height * BaseplateExtent.Height / m_element.Size.Height);
            }
        }

        public MosaicService(LdrawService ldrawService)
        {
            Debug.WriteLine("MosaicService c'tor.");

            m_ldrawService = ldrawService;
            m_colorService = new ColorService(m_ldrawService);

            Baseplate = BASEPLATE_32X32;
            Element = PLATE_1X1;
            BaseplateExtent = new SKSizeI(6, 6);
            AllowedColors = new int[]
            {
                LDRAW_BLACK,
                LDRAW_BLUE,
                LDRAW_RED,
                LDRAW_YELLOW,
                LDRAW_WHITE,
            };
        }

        public Mosaic GetMosaic(SKBitmap filteredImage, MosaicAlgorithm mosaicAlgorithm)
        {
            var sw = Stopwatch.StartNew();

            var colors = new LdColor[ElementExtent.Width, ElementExtent.Height];
            mosaicAlgorithm.Reset(ElementExtent);

            using (SKBitmap source = filteredImage.Resize(filteredImage.Info.Size.GetSizeToFill(ElementExtent), SKFilterQuality.High))
            {
                SKBitmap mosaic = new SKBitmap(ElementExtent.Width, ElementExtent.Height);

                SKPointI topLeft = new SKPointI(
                    (source.Width - mosaic.Width) / 2,
                    (source.Height - mosaic.Height) / 2);

                var sourcePixels = source.Pixels;
                var mosaicPixels = mosaic.Pixels;

                for (int y = 0; y < ElementExtent.Height; y++)
                {
                    for (int x = 0; x < ElementExtent.Width; x++)
                    {
                        int pixelIndex = ((y + topLeft.Y) * source.Width) + x + topLeft.X;

                        SKColor color = sourcePixels[pixelIndex];
                        LdColor ldColor = mosaicAlgorithm.GetColor(x, y, color, m_colorService);
                        colors[x, y] = ldColor;
                        mosaicPixels[pixelIndex] = ldColor.Color;
                    }
                }

                mosaic.Pixels = mosaicPixels;

                Debug.WriteLine($"GetMosaic() done: {sw.ElapsedMilliseconds}ms");

                return new Mosaic(m_baseplate, m_element, colors, mosaic);
            }
        }
    }
}
