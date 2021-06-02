using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core
{
    public class LdrawColor
    {
        public int Number { get; }
        public string Name { get; }
        public System.Drawing.Color Rgb { get; }

        public LdrawColor(int number, string name, string rgbHex)
        {
            Number = number;
            Name = name;

            int rgba = Int32.Parse("0x" + rgbHex);
            Rgb = System.Drawing.Color.FromArgb(rgba);
        }

    }
}
