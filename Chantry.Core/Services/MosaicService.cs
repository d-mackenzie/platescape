using SkiaSharp;
using System;
using System.Diagnostics;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.Core.MosaicAlgorithms;

namespace TeethInc.Chantry.Core.Services
{
    public class MosaicService
    {
        private const string BASEPLATE_32X32 = "3811";
        private const string PLATE_1X1 = "3024";
        
        private const int LDRAW_BLACK = 0;
        private const int LDRAW_BLUE = 1;
        private const int LDRAW_GREEN = 2;
        private const int LDRAW_RED = 4;
        private const int LDRAW_YELLOW = 14;
        private const int LDRAW_WHITE = 15;
        private const int LDRAW_TAN = 19;
        private const int LDRAW_DARK_TAN = 28;
        private const int LDRAW_LIGHT_BLUISH_GREY = 71;
        private const int LDRAW_DARK_BLUISH_GREY = 72;

        private ColorService _colorService;
        private LdrawService _ldrawService;

        private LdPart _baseplate;
        private LdPart _element;

        public string Baseplate
        {
            get { return _baseplate.Number; }
            set { _baseplate = _ldrawService.GetPart(value); }
        }

        public string Element
        {
            get { return _element.Number; }
            set { _element = _ldrawService.GetPart(value); }
        }

        public SKSizeI BaseplateExtent { get; set; }

        public int[] AllowedColors
        {
            get { return _colorService.AllowedColors; }
            set { _colorService.AllowedColors = value; }
        }

        public SKSizeI ElementExtent
        {
            get
            {
                return new SKSizeI(
                    _baseplate.Size.Width * BaseplateExtent.Width / _element.Size.Width,
                    _baseplate.Size.Height * BaseplateExtent.Height / _element.Size.Height);
            }
        }

        public SKSizeI StudExtent
        {
            get
            {
                return new SKSizeI(
                    _baseplate.Size.Width * BaseplateExtent.Width,
                    _baseplate.Size.Height * BaseplateExtent.Height);
            }
        }

        public MosaicService(LdrawService ldrawService)
        {
            _ldrawService = ldrawService;
            _colorService = new ColorService(_ldrawService);

            Baseplate = BASEPLATE_32X32;
            Element = PLATE_1X1;
            BaseplateExtent = new SKSizeI(4, 4);
            AllowedColors = new int[]
            {
                LDRAW_BLACK,
                LDRAW_BLUE,
                LDRAW_GREEN,
                LDRAW_RED,
                LDRAW_YELLOW,
                LDRAW_WHITE,
                LDRAW_TAN,
                LDRAW_DARK_TAN,
                LDRAW_LIGHT_BLUISH_GREY,
                LDRAW_DARK_BLUISH_GREY

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
                        SKColor color = sourcePixels[source.GetPixelIndex(topLeft.X + x, topLeft.Y + y)];
                        LdColor ldColor = mosaicAlgorithm.GetColor(x, y, color, _colorService);
                        colors[x, y] = ldColor;
                        mosaicPixels[mosaic.GetPixelIndex(x, y)] = ldColor.Color;
                    }
                }

                mosaic.Pixels = mosaicPixels;
                mosaic = mosaic.Resize(StudExtent, SKFilterQuality.None);

                Debug.WriteLine($"GetMosaic() done: {sw.ElapsedMilliseconds}ms");

                return new Mosaic(_baseplate, _element, colors, mosaic);
            }
        }
    }
}
