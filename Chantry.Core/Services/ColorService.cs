using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.Core.Services
{
    public class ColorService
    {
        private LdColor[] m_allowedColors;
        private LdColor[,,] m_closestLdColors = new LdColor[64, 64, 64];

        public ColorService(LdColor[] allowedColors)
        {
            m_allowedColors = allowedColors;
            RebuildCache();
        }

        public LdColor GetClosestLdColor(Color color)
        {
            return m_closestLdColors[color.R >> 2, color.G >> 2, color.B >> 2];
        }

        private void RebuildCache()
        {
            var sw = Stopwatch.StartNew();

            for (int r = 0; r < 256; r += 4)
            {
                for (int g = 0; g < 256; g += 4)
                {
                    for (int b = 0; b < 256; b += 4)
                    {
                        m_closestLdColors[r >> 2, g >> 2, b >> 2] = CalculateClosestLdColor(Color.FromArgb(r, g, b));
                    }
                }
            }

            Debug.WriteLine($"RebuildCache(): {sw.ElapsedMilliseconds}ms.");
        }

        private LdColor CalculateClosestLdColor(Color color)
        {
            double minDistance = double.MaxValue;
            LdColor closestColor = null;

            foreach (LdColor ldrawColor in m_allowedColors)
            {
                // get distance.

                double distance = color.DistanceFrom(ldrawColor.Color);

                if (distance < minDistance)
                {
                    closestColor = ldrawColor;
                    minDistance = distance;
                }
            }

            return closestColor;
        }
    }
}
