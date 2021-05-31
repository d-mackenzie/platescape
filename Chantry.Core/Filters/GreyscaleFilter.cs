using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Extensions;

namespace TeethInc.Chantry.Core.Filters
{
    public class GreyscaleFilter : Filter
    {
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

        public override Bitmap GetFilteredImage(Bitmap unfilteredImage)
        {
            var filteredImage = new Bitmap(unfilteredImage);

            filteredImage.ApplyFilter(GetGreyscalePixel);

            return filteredImage;
        }

        private Color GetGreyscalePixel(Color unfilteredPixel)
        {
            byte luminosity = (byte)(m_red[unfilteredPixel.R] + m_green[unfilteredPixel.G] + m_blue[unfilteredPixel.B]);

            return Color.FromArgb(
                luminosity,
                luminosity,
                luminosity);
        }
    }
}
