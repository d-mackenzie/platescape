using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.Core.Extensions
{
    public static class ColorExtensions
    {
        public static double DistanceFrom(this Color me, Color color)
        {
            return
                Math.Pow((me.R - color.R) * 0.30, 2) +
                Math.Pow((me.G - color.G) * 0.59, 2) +
                Math.Pow((me.B - color.B) * 0.11, 2);
        }

        public static LdColor ClosestLdColor(this Color me, IEnumerable<LdColor> allowedColors)
        {
            double minDistance = double.MaxValue;
            LdColor closestColor = null;

            foreach (LdColor ldrawColor in allowedColors)
            {
                // get distance.

                double distance = me.DistanceFrom(ldrawColor.Color);

                if (distance < minDistance)
                {
                    closestColor = ldrawColor;
                    minDistance = distance;
                }
            }

            return closestColor;
        }

        public static Color FromHsv(float h, float s, float v)
        {
            // ######################################################################
            // T. Nathan Mundhenk
            // mundhenk@usc.edu
            // C/C++ Macro HSV to RGB

            while (h < 0) { h += 360; };
            while (h >= 360) { h -= 360; };

            float r, g, b;

            if (v <= 0)
            {
                r = g = b = 0;
            }
            else if (s <= 0)
            {
                r = g = b = v;
            }
            else
            {
                float hf = h / 60.0f;
                int i = (int)Math.Floor(hf);
                float f = hf - i;
                float pv = v * (1 - s);
                float qv = v * (1 - s * f);
                float tv = v * (1 - s * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        r = v;
                        g = tv;
                        b = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        r = qv;
                        g = v;
                        b = pv;
                        break;
                    case 2:
                        r = pv;
                        g = v;
                        b = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        r = pv;
                        g = qv;
                        b = v;
                        break;
                    case 4:
                        r = tv;
                        g = pv;
                        b = v;
                        break;

                    // Red is the dominant color

                    case 5:
                        r = v;
                        g = pv;
                        b = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        r = v;
                        g = tv;
                        b = pv;
                        break;
                    case -1:
                        r = v;
                        g = pv;
                        b = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        r = g = b = v; // Just pretend its black/white
                        break;
                }
            }

            return Color.FromArgb(
                Math.Clamp((int)(r * 255.0), 0, 255),
                Math.Clamp((int)(g * 255.0), 0, 255),
                Math.Clamp((int)(b * 255.0), 0, 255));
        }
    }
}
