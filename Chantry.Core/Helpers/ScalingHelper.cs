using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Extensions;

namespace TeethInc.Chantry.Core.Helpers
{
    public static class ScalingHelper
    {
        public static Size GetBestFitSize(Size source, Size target)
        {
            float scalingFactor = 1f;

            if (source.AspectRatio() == AspectRatio.Landscape)
                scalingFactor = (float)target.Height / source.Height;
            else
                scalingFactor = (float)target.Width / source.Width;

            return new Size((int)(source.Width * scalingFactor), (int)(source.Height * scalingFactor));
        }
    }
}
