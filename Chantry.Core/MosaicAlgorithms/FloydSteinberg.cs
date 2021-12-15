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
    public class FloydSteinberg : MosaicAlgorithm
    {
        private Error[,] m_errors;
        private Size m_size;

        private const float EAST_ERROR = 7 / 16f;
        private const float SOUTHEAST_ERROR = 1 / 16f;
        private const float SOUTH_ERROR = 5 / 16f;
        private const float SOUTHWEST_ERROR = 3 / 16f;

        public FloydSteinberg()
        {
            Reset(new Size(1,1));
        }

        public override LdColor GetColor(int x, int y, Color sourceColor, ColorService colorService)
        {
            // get average pixel color.

            float sourceRed = sourceColor.R;
            float sourceGreen = sourceColor.G;
            float sourceBlue = sourceColor.B;

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

            m_errors[x + 1, y + 0].AddFraction(calculatedError, EAST_ERROR);
            m_errors[x + 1, y + 1].AddFraction(calculatedError, SOUTHEAST_ERROR);
            m_errors[x + 0, y + 1].AddFraction(calculatedError, SOUTH_ERROR);
            
            if (x != 0)
                m_errors[x - 1, y + 1].AddFraction(calculatedError, SOUTHWEST_ERROR);

            return newColor;
        }

        public override void Reset(Size size)
        {
            if (size != m_size)
            {
                m_errors = new Error[size.Width + 1, size.Height + 1];
                for (int x = 0; x < size.Width + 1; x++)
                {
                    for (int y = 0; y < size.Height + 1; y++)
                    {
                        m_errors[x, y] = new Error();
                    }
                }
            }
            else
            {
                for (int x = 0; x < size.Width + 1; x++)
                {
                    for (int y = 0; y < size.Height + 1; y++)
                    {
                        m_errors[x, y].Reset();
                    }
                }
            }

            m_size = size;
        }

        private class Error
        {
            public float RedError = 0;
            public float GreenError = 0;
            public float BlueError = 0;

            public Error() { }

            public Error(float redError, float greenError, float blueError)
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

            public void AddFraction(Error error, float fraction)
            {
                RedError += error.RedError * fraction;
                GreenError += error.GreenError * fraction;
                BlueError += error.BlueError * fraction;
            }
        }
    }
}
