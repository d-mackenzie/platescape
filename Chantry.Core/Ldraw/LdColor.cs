using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Ldraw
{
    public class LdColor
    {
        public int Number { get; }
        public string Name { get; }
        public Color Color { get; }

        public LdColor(int number, string name, string rgbHex)
        {
            Number = number;
            Name = name;

            long rgba = Int32.Parse(rgbHex, System.Globalization.NumberStyles.HexNumber);
            rgba = rgba | 0xFF000000;
            Color = Color.FromArgb((int)rgba);
        }
    }
}
