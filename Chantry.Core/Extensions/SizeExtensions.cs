using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Extensions
{
    public enum AspectRatio
    {
        Portrait,
        Landscape
    }

    public static class SizeExtensions
    {
        public static bool IsSmallerThan(this Size me, Size size)
        {
            return (me.Width < size.Width || me.Height < size.Height);
        }

        public static bool IsLargerThan(this Size me, Size size)
        {
            return (me.Width > size.Width || me.Height > size.Height);
        }

        public static AspectRatio AspectRatio(this Size me)
        {
            return (me.Width > me.Height) ? Extensions.AspectRatio.Landscape : Extensions.AspectRatio.Portrait;
        }
    }
}
