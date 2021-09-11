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
                Math.Clamp((int)red, 0, 255),
                Math.Clamp((int)green, 0, 255),
                Math.Clamp((int)blue, 0, 255));

            LdColor closestLdColor = colorService.GetClosestLdColor(avgColor);

            // calculate the error.

            Error calculatedError = new Error();

            calculatedError.RedError = closestLdColor.Color.R - red;
            calculatedError.GreenError = closestLdColor.Color.G - green;
            calculatedError.BlueError = closestLdColor.Color.B - blue;

            // propagate the error.

            m_errors[x + 1, y + 0].Add(calculatedError.GetFraction(7));
            m_errors[x + 1, y + 1].Add(calculatedError.GetFraction(1));
            m_errors[x + 0, y + 1].Add(calculatedError.GetFraction(5));
            
            if (x != 0)
                m_errors[x - 1, y + 1].Add(calculatedError.GetFraction(3));

            return closestLdColor;
        }

        public void Reset(Size size)
        {
            if (size != m_size)
                m_errors = new Error[size.Width + 1, size.Height + 1];

            m_errors.Initialize();
            m_size = size;
        }

        private struct Error
        {
            public double RedError;
            public double GreenError;
            public double BlueError;

            public void Add(Error error)
            {
                RedError += error.RedError;
                GreenError += error.GreenError;
                BlueError += error.BlueError;
            }

            public Error GetFraction(int fraction)
            {
                Error ret = new Error();

                ret.RedError = RedError * fraction / 16;
                ret.GreenError = GreenError * fraction / 16;
                ret.BlueError = BlueError * fraction / 16;

                return ret;
            }
        }
    }
}
