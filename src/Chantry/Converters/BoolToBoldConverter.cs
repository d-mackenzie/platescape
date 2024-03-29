using Avalonia.Data.Converters;
using System;
using System.Globalization;
using SkiaSharp;
using Avalonia.Media;

namespace TeethInc.Chantry.Converters
{
    public class BoolToBoldConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null)
                return false;

            bool boolValue = (bool)value;

            return boolValue ? FontWeight.Bold : FontWeight.Normal;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
