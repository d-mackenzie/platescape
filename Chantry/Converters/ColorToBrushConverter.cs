using Avalonia.Data.Converters;
using System;
using System.Globalization;
using SkiaSharp;
using Avalonia.Media;

namespace TeethInc.Chantry.Converters
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null)
                return new SolidColorBrush(Colors.Transparent);

            SKColor skColor = (SKColor)value;
            return new SolidColorBrush(new Color(skColor.Alpha, skColor.Red, skColor.Green, skColor.Blue));
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
