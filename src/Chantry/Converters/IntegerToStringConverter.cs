using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Converters
{
	internal class IntegerToStringConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			int? valueAsInt = value as int?;

			return valueAsInt?.ToString("N0") ?? "";
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			string valueAsString = (value as string) ?? "";

			if (int.TryParse(valueAsString, out int intValue))
			{
				return intValue;
			}

			return null;
		}
	}
}
