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
    public class NearestColor : MosaicAlgorithm
    {
        public override LdColor GetColor(int x, int y, Color sourceColor, ColorService colorService)
        {
            return colorService.GetClosestLdColor(sourceColor);
        }

        public override void Reset(Size size)
        { }
    }
}
