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
        private Dictionary<Point, Error> m_errors;

        public FloydSteinberg()
        {
            Reset();
        }

        public LdColor GetColor(Point point, IEnumerable<Color> sourceColors, IEnumerable<LdColor> allowedColors)
        {
            // get average pixel color.

            double avgRed = sourceColors.Average(x => x.R);
            double avgGreen = sourceColors.Average(x => x.G);
            double avgBlue = sourceColors.Average(x => x.B);

            // apply the error.

            Error aggregatedError = GetError(point);

            avgRed -= aggregatedError.RedError;
            avgGreen -= aggregatedError.GreenError;
            avgBlue -= aggregatedError.BlueError;

            // get closest color.

            Color avgColor = Color.FromArgb(
                (int)Math.Clamp(avgRed, 0, 255),
                (int)Math.Clamp(avgGreen, 0, 255),
                (int)Math.Clamp(avgBlue, 0, 255));

            LdColor closestLdColor = avgColor.ClosestLdColor(allowedColors);

            // calculate the error.

            Error calculatederror = new Error(
                closestLdColor.Color.R - (int)avgRed,
                closestLdColor.Color.G - (int)avgGreen,
                closestLdColor.Color.B - (int)avgBlue);

            // propagate the error.

            AggregateError(point + new Size(1, 0), calculatederror.GetFraction(7));
            AggregateError(point + new Size(1, 1), calculatederror.GetFraction(1));
            AggregateError(point + new Size(0, 1), calculatederror.GetFraction(5));
            AggregateError(point + new Size(-1, 1), calculatederror.GetFraction(3));

            return closestLdColor;
        }

        private void AggregateError(Point point, Error error)
        {
            GetError(point).Add(error);
        }

        private Error GetError(Point point)
        {
            Error error = m_errors.GetValueOrDefault(point);

            if (error == null)
            {
                error = new Error(0, 0, 0);
                m_errors.Add(point, error);
            }

            return error;
        }

        public void Reset()
        {
            m_errors = new Dictionary<Point, Error>();
        }

        private class Error
        {
            public int RedError { get; set; }
            public int GreenError { get; set; }
            public int BlueError { get; set; }

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
