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

        public static SKSizeI GetSizeScaledBy(this SKSizeI me, float scale)
        {
            return new SKSizeI((int)(me.Width * scale), (int)(me.Height * scale));
        }

        public static SKSizeI GetSizeToFill(this SKSizeI me, SKSizeI target)
        {
            SKSizeI scaledByWidth = me.GetSizeScaledBy((float)target.Width / me.Width);
            SKSizeI scaledByHeight = me.GetSizeScaledBy((float)target.Height / me.Height);

            if (scaledByWidth.IsSmallerThan(target))
                return scaledByHeight;

            return scaledByWidth;
        }

        public static SKSizeI GetSizeToFit(this SKSizeI me, SKSizeI target)
        {
            SKSizeI scaledByWidth = me.GetSizeScaledBy((float)target.Width / me.Width);
            SKSizeI scaledByHeight = me.GetSizeScaledBy((float)target.Height / me.Height);

            if (scaledByWidth.IsLargerThan(target))
                return scaledByHeight;

            return scaledByWidth;
        }
    }
}
