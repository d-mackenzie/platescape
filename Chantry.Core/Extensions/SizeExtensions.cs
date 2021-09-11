using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Extensions
{
    public enum AspectRatioType
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

        public static AspectRatioType AspectRatio(this Size me)
        {
            return (me.Width > me.Height) ? Extensions.AspectRatioType.Landscape : Extensions.AspectRatioType.Portrait;
        }

        public static Size GetSizeScaledBy(this Size me, float scale)
        {
            return new Size((int)(me.Width * scale), (int)(me.Height * scale));
        }

        public static Size GetSizeToFill(this Size me, Size target)
        {
            Size scaledByWidth = me.GetSizeScaledBy((float)target.Width / me.Width);
            Size scaledByHeight = me.GetSizeScaledBy((float)target.Height / me.Height);

            if (scaledByWidth.IsSmallerThan(target))
                return scaledByHeight;

            return scaledByWidth;
        }

        public static Size GetSizeToFit(this Size me, Size target)
        {
            Size scaledByWidth = me.GetSizeScaledBy((float)target.Width / me.Width);
            Size scaledByHeight = me.GetSizeScaledBy((float)target.Height / me.Height);

            if (scaledByWidth.IsLargerThan(target))
                return scaledByHeight;

            return scaledByWidth;
        }
    }
}
