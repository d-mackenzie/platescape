using SkiaSharp;
using System;

namespace TeethInc.Chantry.Core.Ldraw
{
    public record LdColor
    {
        public int Number { get; }
        public string Name { get; }
        public SKColor Color { get; }

        public LdColor(int number, string name, string rgbHex)
        {
            Number = number;
            Name = name;

            uint rgba = uint.Parse(rgbHex, System.Globalization.NumberStyles.HexNumber);
            rgba = rgba | 0xFF000000;
            Color = new SKColor(rgba);
        }
    }
}
