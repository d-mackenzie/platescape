using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.Core.MosaicAlgorithms
{
    public class FloydSteinberg : IMosaicAlgorithm
    {
        private Error[,] m_errors;
        private Size m_size;
        private IEnumerable<LdColor> m_allowedColors;

        public FloydSteinberg()
        {
            Reset(new Size(1,1), new List<LdColor>());
        }

        public LdColor GetColor(int x, int y, Color sourceColor)
        {
            // get average pixel color.

            double red = sourceColor.R;
            double green = sourceColor.G;
            double blue = sourceColor.B;

            // apply the error.

            Error aggregatedError = m_errors[x, y];

            red -= aggregatedError.RedError;
            green -= aggregatedError.GreenError;
            blue -= aggregatedError.BlueError;

            // get closest color.

            Color avgColor = Color.FromArgb(
                (int)Math.Clamp(red, 0, 255),
                (int)Math.Clamp(green, 0, 255),
                (int)Math.Clamp(blue, 0, 255));

            LdColor closestLdColor = avgColor.ClosestLdColor(m_allowedColors);

            // calculate the error.

            Error calculatedError = new Error(
                closestLdColor.Color.R - (int)red,
                closestLdColor.Color.G - (int)green,
                closestLdColor.Color.B - (int)blue);

            // propagate the error.

            m_errors[x + 1, y + 0].Add(calculatedError.GetFraction(7));
            m_errors[x + 1, y + 1].Add(calculatedError.GetFraction(1));
            m_errors[x + 0, y + 1].Add(calculatedError.GetFraction(5));
            
            if (x != 0)
                m_errors[x - 1, y + 1].Add(calculatedError.GetFraction(3));

            return closestLdColor;
        }

        public void Reset(Size size, IEnumerable<LdColor> allowedColors)
        {
            if (size != m_size)
                m_errors = new Error[size.Width + 1, size.Height + 1];
            else
                m_errors.Initialize();

            m_size = size;
        }

        private struct Error
        {
            public int RedError;
            public int GreenError;
            public int BlueError;

            public Error(int redError, int greenError, int blueError)
            {
                RedError = redError;
                GreenError = greenError;
                BlueError = blueError;
            }

            public void Add(Error error)
            {
                RedError += error.RedError;
                GreenError += error.GreenError;
                BlueError += error.BlueError;
            }

            public Error GetFraction(int fraction)
            {
                return new Error(
                    (int)(RedError * fraction / 16f),
                    (int)(GreenError * fraction / 16f),
                    (int)(BlueError * fraction / 16f));
            }
        }
    }
}
