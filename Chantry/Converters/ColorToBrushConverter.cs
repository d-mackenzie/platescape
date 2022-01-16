using Avalonia.Data.Converters;
using System;
using System.Globalization;
using AmSolidColorBrush = Avalonia.Media.SolidColorBrush;
using AmColor = Avalonia.Media.Color;
using SkiaSharp;

namespace TeethInc.Chantry.App.Converters
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SKColor skColor = (SKColor)value;
            return new AmSolidColorBrush(new AmColor(skColor.Alpha, skColor.Red, skColor.Green, skColor.Blue));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
