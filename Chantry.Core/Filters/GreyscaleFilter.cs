using SkiaSharp;
using TeethInc.Chantry.Core.Extensions;

namespace TeethInc.Chantry.Core.Filters
{
    public class GreyscaleFilter : Filter
    {
        public override string DisplayName => "Greyscale";

        private byte[] m_red;
        private byte[] m_green;
        private byte[] m_blue;

        public GreyscaleFilter() : base()
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
            image.ApplyFilter(GetGreyscalePixel);
        }

        private SKColor GetGreyscalePixel(SKColor unfilteredPixel)
        {
            byte luminosity = (byte)(m_red[unfilteredPixel.Red] + m_green[unfilteredPixel.Green] + m_blue[unfilteredPixel.Blue]);

            return new SKColor(luminosity, luminosity, luminosity);
        }
    }
}
