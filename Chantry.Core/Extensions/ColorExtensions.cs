using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Extensions
{
    public static class ColorExtensions
    {

        public static int DistanceFrom(this Color me, Color color)
        {
            return Math.Abs(me.R - color.R) +
                Math.Abs(me.G - color.G) +
                Math.Abs(me.B - color.B);
        }

        public static LdrawColor ClosestLdrawColor(this Color me, LdrawColor[] ldrawColors)
        {
            int minDistance = 255;
            LdrawColor closestColor = null;

            foreach (LdrawColor ldrawColor in ldrawColors)
            {
                // get distance.

                int distance = me.DistanceFrom(ldrawColor.Color);

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
