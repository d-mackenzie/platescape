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
    public class NearestColor : IMosaicAlgorithm
    {
        public LdColor GetColor(Point point, Color sourceColor, IEnumerable<LdColor> allowedColors)
        {
            return sourceColor.ClosestLdColor(allowedColors);
        }

        public void Reset()
        { }
    }
}
