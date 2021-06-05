using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Extensions;

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
            return avgColor.ClosestLdrawColor(allowedColors);
        }
    }
}
