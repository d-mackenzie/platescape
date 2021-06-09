using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Helpers
{
    public static class ScalingHelper
    {
        public static Size GetBestFitSize(Size source, Size target)
        {
            float widthDivisor = (float)source.Width / target.Width;
            float heightDivisor = (float)source.Height / target.Height;

            float divisor = Math.Min(widthDivisor, heightDivisor);

            return new Size((int)(source.Width / divisor), (int)(source.Height / divisor));
        }
    }
}
