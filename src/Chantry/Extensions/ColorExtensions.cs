using Avalonia.Media;
using SkiaSharp;
using TeethInc.Chantry.Core.Extensions;

namespace TeethInc.Chantry.Extensions
{
	internal static class ColorExtensions
	{
		public static bool IsDarkColor(this SKColor skColor)
		{
			return skColor.GetValue() <= 75;
		}

		public static Color ToColor(this SKColor skColor)
		{
			return new Color(skColor.Alpha, skColor.Red, skColor.Green, skColor.Blue);
		}

		public static SolidColorBrush ToSolidColorBrush(this SKColor skColor)
		{
			return new SolidColorBrush(skColor.ToColor());
		}
	}
}
