using SkiaSharp;

namespace TeethInc.Chantry.Core.Extensions
{
    public enum AspectRatioType
    {
        Portrait,
        Landscape
    }

    public static class SizeExtensions
    {
        public static bool IsSmallerThan(this SKSizeI me, SKSizeI size)
        {
            return (me.Width < size.Width || me.Height < size.Height);
        }

        public static bool IsLargerThan(this SKSizeI me, SKSizeI size)
        {
            return (me.Width > size.Width || me.Height > size.Height);
        }

        public static AspectRatioType AspectRatio(this SKSizeI me)
        {
            return (me.Width > me.Height) ? Extensions.AspectRatioType.Landscape : Extensions.AspectRatioType.Portrait;
        }

        public static SKSizeI GetSizeScaledBy(this SKSizeI me, double scale)
        {
            return new SKSizeI((int)(me.Width * scale), (int)(me.Height * scale));
        }

        public static SKSizeI GetSizeToFill(this SKSizeI me, SKSizeI target)
        {
            SKSizeI scaledByWidth = me.GetSizeScaledBy((double)target.Width / me.Width);
            SKSizeI scaledByHeight = me.GetSizeScaledBy((double)target.Height / me.Height);

            if (scaledByWidth.IsSmallerThan(target))
                return scaledByHeight;

            return scaledByWidth;
        }

        public static double GetScaleToFill(this SKSizeI me, SKSizeI target)
        {
            double widthScale = (double)target.Width / me.Width;
            double heightScale = (double)target.Height / me.Height;

            if (me.GetSizeScaledBy(widthScale).IsSmallerThan(target))
                return heightScale;

            return widthScale;
        }

        public static SKSizeI GetSizeToFit(this SKSizeI me, SKSizeI target)
        {
            SKSizeI scaledByWidth = me.GetSizeScaledBy((double)target.Width / me.Width);
            SKSizeI scaledByHeight = me.GetSizeScaledBy((double)target.Height / me.Height);

            if (scaledByWidth.IsLargerThan(target))
                return scaledByHeight;

            return scaledByWidth;
        }
    }
}
