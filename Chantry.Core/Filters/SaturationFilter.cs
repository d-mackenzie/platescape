using SkiaSharp;
using System;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Helpers;

namespace TeethInc.Chantry.Core.Filters
{
    public class SaturationFilter : Filter
    {
        public override string DisplayName => "Saturation";

        public double Saturation { get; set; }

        private byte[] m_red;
        private byte[] m_green;
        private byte[] m_blue;

        public SaturationFilter() : base()
        {
            m_red = GetCalculatedArray(0.21f);
            m_green = GetCalculatedArray(0.71f);
            m_blue = GetCalculatedArray(0.07f);
        }

        private byte[] GetCalculatedArray(float factor)
        {
            var ret = new byte[256];

            for (int i = 0; i < 256; i++)
            {
                ret[i] = (byte)(i * factor);
            }

            return ret;
        }

        public override void ApplyFilter(SKBitmap image)
        {
            image.ApplyFilter(GetSaturationPixel);
        }

        private SKColor GetSaturationPixel(SKColor unfilteredPixel)
        {
            byte luminosity = (byte)(m_red[unfilteredPixel.Red] + m_green[unfilteredPixel.Green] + m_blue[unfilteredPixel.Blue]);

            byte red = MathHelper.Lerp(unfilteredPixel.Red, luminosity, Saturation);
            byte green = MathHelper.Lerp(unfilteredPixel.Green, luminosity, Saturation);
            byte blue = MathHelper.Lerp(unfilteredPixel.Blue, luminosity, Saturation);

            return new SKColor(red, green, blue);
        }
    }
}