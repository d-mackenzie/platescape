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
        private LdrawService m_ldrawService;
        private int[] m_allowedColors;
        private LdColor[] m_allowedLdColors;
        private LdColor[,,] m_closestLdColorCache = new LdColor[64, 64, 64];
        private float[,,,] m_distanceCache;
        private int[] m_ldColorIndex;

        public int[] AllowedColors
        {
            get { return m_allowedColors; }
            set
            {
                m_allowedColors = value;

                if (m_allowedColors.Length == 0)
                    m_allowedColors = new int[] { 0 };

                m_allowedLdColors = m_ldrawService.GetColors(m_allowedColors).ToArray();

                BuildClosestLdColorCache();
            }
        }

        public ColorService(LdrawService ldrawService)
        {
            m_ldrawService = ldrawService;
            BuildDistanceCache();

            AllowedColors = new int[0];
        }

        public LdColor GetClosestLdColor(Color color)
        {
            return m_closestLdColorCache[color.R >> 2, color.G >> 2, color.B >> 2];
        }

        private void BuildDistanceCache()
        {
            var sw = Stopwatch.StartNew();

            var ldColors = m_ldrawService.Colors;
            m_distanceCache = new float[64, 64, 64, ldColors.Count];
            m_ldColorIndex = new int[ldColors.Select(x => x.Number).Max() + 1];

            for (int i = 0; i < ldColors.Count; i++)
            {
                m_ldColorIndex[ldColors[i].Number] = i;
            }

            foreach (LdColor ldColor in ldColors)
            {
                int ldColorIndex = m_ldColorIndex[ldColor.Number];

                for (int r = 0; r < 256; r += 4)
                {
                    for (int g = 0; g < 256; g += 4)
                    {
                        for (int b = 0; b < 256; b += 4)
                        {
                            Color color = Color.FromArgb(r, g, b);
                            m_distanceCache[r >> 2, g >> 2, b >> 2, ldColorIndex] = color.DistanceFrom(ldColor.Color);
                        }
                    }
                }
            }

            Debug.WriteLine($"BuildDistanceCache(): {sw.ElapsedMilliseconds}ms.");
        }

        private void BuildClosestLdColorCache()
        {
            var sw = Stopwatch.StartNew();

            for (int r = 0; r < 256; r += 4)
            {
                for (int g = 0; g < 256; g += 4)
                {
                    for (int b = 0; b < 256; b += 4)
                    {
                        m_closestLdColorCache[r >> 2, g >> 2, b >> 2] = CalculateClosestLdColor(Color.FromArgb(r, g, b));
                    }
                }
            }

            Debug.WriteLine($"BuildClosestLdColorCache(): {sw.ElapsedMilliseconds}ms.");
        }

        private LdColor CalculateClosestLdColor(Color color)
        {
            float minDistance = float.MaxValue;
            LdColor closestColor = null;

            foreach (LdColor ldColor in m_allowedLdColors)
            {
                // get distance.

                float distance = m_distanceCache[color.R >> 2, color.G >> 2, color.B >> 2, m_ldColorIndex[ldColor.Number]];

                if (distance < minDistance)
                {
                    closestColor = ldColor;
                    minDistance = distance;
                }
            }

            return closestColor;
        }
    }
}
