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
    public class NearestColor : IMosaicAlgorithm
    {
        private ColorService m_colorService;
        private IEnumerable<LdColor> m_allowedColors;

        public NearestColor(ColorService colorService)
        {
            m_colorService = colorService;
        }

        public LdColor GetColor(int x, int y, Color sourceColor)
        {
            //return sourceColor.ClosestLdColor(m_allowedColors);
            return m_colorService.GetClosestLdColor(sourceColor);
        }

        public void Reset(Size size, IEnumerable<LdColor> allowedColors)
        {
            m_allowedColors = allowedColors;
       
        }
    }
}
