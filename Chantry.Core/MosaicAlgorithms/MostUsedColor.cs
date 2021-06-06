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
    public class MostUsedColor : IMosaicAlgorithm
    {
        public LdColor GetColor(Point point, Color[] colors, LdColor[] allowedColors)
        {
            LdColor[] ldrawColors = colors.Select(x => x.ClosestLdColor(allowedColors)).ToArray();

            var grouping = ldrawColors.GroupBy(k => k.Number);

            int max = grouping.Max(x => x.Count());

            int mostUsedColorNumber = grouping.FirstOrDefault(x => x.Count() == max).Key;

            return allowedColors.FirstOrDefault(x => x.Number == mostUsedColorNumber);
        }

        public void Reset()
        { }

    }
}
