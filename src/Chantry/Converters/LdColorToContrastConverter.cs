using Avalonia.Data.Converters;
using Avalonia.Media;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Extensions;

namespace TeethInc.Chantry.Converters
{
	public class LdColorToContrastConverter : IValueConverter
	{
		public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is null)
				return new SolidColorBrush(Colors.Transparent);

			SKColor skColor = ((LdColor)value).Color;
			return (skColor.IsDarkColor() ? SKColors.White : SKColors.Black).ToSolidColorBrush();
		}

		public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
