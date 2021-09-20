using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.MosaicAlgorithms
{
    public class FloydSteinberg : IMosaicAlgorithm
    {
        private Error[,] m_errors;
        private Size m_size;

        public FloydSteinberg()
        {
            Reset(new Size(1,1));
        }

        public LdColor GetColor(int x, int y, Color sourceColor, ColorService colorService)
        {
            // get average pixel color.

            double sourceRed = sourceColor.R;
            double sourceGreen = sourceColor.G;
            double sourceBlue = sourceColor.B;

            // apply the error.

            Error error = m_errors[x, y];

            sourceRed += error.RedError;
            sourceGreen += error.GreenError;
            sourceBlue += error.BlueError;

            // get closest color.

            Color colorWithErrorApplied = Color.FromArgb(
                Math.Clamp((int)sourceRed, 0, 255),
                Math.Clamp((int)sourceGreen, 0, 255),
                Math.Clamp((int)sourceBlue, 0, 255));

            LdColor newColor = colorService.GetClosestLdColor(colorWithErrorApplied);

            // calculate the error.

            Error calculatedError = new Error(
                colorWithErrorApplied.R - newColor.Color.R,
                colorWithErrorApplied.G - newColor.Color.G,
                colorWithErrorApplied.B - newColor.Color.B);

            // propagate the error.

            m_errors[x + 1, y + 0].Add(calculatedError.GetFraction(7));
            m_errors[x + 1, y + 1].Add(calculatedError.GetFraction(1));
            m_errors[x + 0, y + 1].Add(calculatedError.GetFraction(5));
            
            if (x != 0)
                m_errors[x - 1, y + 1].Add(calculatedError.GetFraction(3));

            return newColor;
        }

        public void Reset(Size size)
        {
            if (size != m_size)
                m_errors = new Error[size.Width + 1, size.Height + 1];

            for (int x = 0; x < size.Width + 1; x++)
            {
                for (int y = 0; y < size.Height + 1; y++)
                {
                    m_errors[x, y] = new Error();
                }
            }

            m_size = size;
        }

        private class Error
        {
            public double RedError = 0;
            public double GreenError = 0;
            public double BlueError = 0;

            public Error() { }

            public Error(double redError, double greenError, double blueError)
            {
                RedError = redError;
                GreenError = greenError;
                BlueError = blueError;
            }

            public void Reset()
            {
                RedError = 0;
                GreenError = 0;
                BlueError = 0;
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
                    RedError * fraction / 16,
                    GreenError * fraction / 16,
                    BlueError * fraction / 16);
            }
        }
    }
}
