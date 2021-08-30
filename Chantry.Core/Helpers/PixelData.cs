using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Helpers
{
    public class PixelData
    {
        private byte[] m_pixels;
        private int m_stride;

        public Color this[int x, int y]
        {
            get
            {
                int index = ((y * m_stride) + (x * 4));
                return Color.FromArgb(m_pixels[index + 3], m_pixels[index + 2], m_pixels[index + 1], m_pixels[index + 0]);
            }

            set
            {
                int index = ((y * m_stride) + (x * 4));
                m_pixels[index + 3] = value.A;
                m_pixels[index + 2] = value.R;
                m_pixels[index + 1] = value.G;
                m_pixels[index + 0] = value.B;
            }
        }

        public byte[] PixelArray => m_pixels;

        public PixelData(int stride, byte[] pixels)
        {
            m_stride = stride;
            m_pixels = pixels;
        }
    }
}
