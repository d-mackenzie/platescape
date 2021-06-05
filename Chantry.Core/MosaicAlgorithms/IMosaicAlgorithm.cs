using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.MosaicAlgorithms
{
    public interface IMosaicAlgorithm
    {
        public LdrawColor GetColor(Color[] colors, LdrawColor[] allowedColors);
    }
}
