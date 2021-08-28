using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.MosaicAlgorithms
{
    public interface IMosaicAlgorithm
    {
        public void Reset(Size size);

        public LdColor GetColor(int x, int y, Color sourceColor, ColorService colorService);
    }
}
