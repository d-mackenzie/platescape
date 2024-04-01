using Avalonia.Data.Converters;
using System;
using System.Globalization;
using SkiaSharp;
using Avalonia.Media;
using TeethInc.Chantry.Extensions;
using TeethInc.Chantry.Core.Ldraw;
using System.Collections.Generic;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Converters
{
	public class LdColorToColorConverter : IValueConverter
	{
		private Dictionary<int, SKColor> _mappings = new Dictionary<int, SKColor>();

		public LdColorToColorConverter()
		{
			_mappings[0] = new SKColor(32, 32, 32);
			_mappings[15] = new SKColor(224, 224, 224);
		}

		public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is null)
				return new SolidColorBrush(Colors.Transparent);

			LdColor ldColor = (LdColor)value;
			SKColor skColor = ldColor.Color;

			if (parameter != null && (bool)parameter && _mappings.ContainsKey(ldColor.Number))
			{
				skColor = _mappings[ldColor.Number];
			}

			return skColor.ToSolidColorBrush();
		}

		public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
