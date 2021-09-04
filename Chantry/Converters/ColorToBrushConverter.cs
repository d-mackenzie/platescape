using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmSolidColorBrush = Avalonia.Media.SolidColorBrush;
using SdColor = System.Drawing.Color;
using AmColor = Avalonia.Media.Color;

namespace TeethInc.Chantry.App.Converters
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SdColor sdColor = (SdColor)value;
            return new AmSolidColorBrush(new AmColor(sdColor.A, sdColor.R, sdColor.G, sdColor.B));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
