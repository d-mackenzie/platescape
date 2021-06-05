using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.MosaicAlgorithms
{
    public class AverageColor : IMosaicAlgorithm
    {
        public LdrawColor GetColor(Color[] sourceColors, LdrawColor[] allowedColors)
        {
            // average the colors.

            double avgRed = sourceColors.Average(x => x.R);
            double avgGreen = sourceColors.Average(x => x.G);
            double avgBlue = sourceColors.Average(x => x.B);

            Color avgColor = Color.FromArgb((int)avgRed, (int)avgGreen, (int)avgBlue);

            // find nearest ldraw color.

            int minDistance = 255;
            LdrawColor closestColor = null;

            foreach (LdrawColor ldrawColor in allowedColors)
            {
                // get color value.

                Color candidateColor = ldrawColor.Color;

                // get distance.

                int distance = Math.Abs(avgColor.R - candidateColor.R) +
                    Math.Abs(avgColor.G - candidateColor.G) +
                    Math.Abs(avgColor.B - candidateColor.B);

                if (distance < minDistance)
                {
                    closestColor = ldrawColor;
                    minDistance = distance;
                }
            }

            return closestColor;
        }
    }
}
