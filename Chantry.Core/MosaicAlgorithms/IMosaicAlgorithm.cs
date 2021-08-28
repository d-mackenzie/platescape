using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.Core.MosaicAlgorithms
{
    public interface IMosaicAlgorithm
    {
        public void Reset(Size size, IEnumerable<LdColor> allowedColors);

        public LdColor GetColor(int x, int y, Color sourceColor);
    }
}
