using Avalonia.Media;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Extensions
{
	internal static class SKColorExtensions
	{
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
