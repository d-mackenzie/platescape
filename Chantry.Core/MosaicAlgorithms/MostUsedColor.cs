using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Extensions;

namespace TeethInc.Chantry.Core.MosaicAlgorithms
{
    public class MostUsedColor : IMosaicAlgorithm
    {
        public LdrawColor GetColor(Color[] colors, LdrawColor[] allowedColors)
        {
            LdrawColor[] ldrawColors = colors.Select(x => x.ClosestLdrawColor(allowedColors)).ToArray();

            var grouping = ldrawColors.GroupBy(k => k.Number);

            int max = grouping.Max(x => x.Count());

            int mostUsedColorNumber = grouping.FirstOrDefault(x => x.Count() == max).Key;

            return allowedColors.FirstOrDefault(x => x.Number == mostUsedColorNumber);
        }
    }
}
