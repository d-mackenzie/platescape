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
using TeethInc.Chantry.Extensions;

namespace TeethInc.Chantry.Converters
{
	public class ColorToContrastColorConverter : IValueConverter
	{
		public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is null)
				return new SolidColorBrush(Colors.Transparent);

			SKColor sourceColor = (SKColor)value;
			return (sourceColor.IsDarkColor() ? SKColors.White : SKColors.Black).ToSolidColorBrush();
		}

		public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
