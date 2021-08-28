using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.Core.Services
{
    public class Mosaic
    {
        public LdPart Baseplate { get; }

        public LdPart Part { get; }

        public LdColor[,] Colors { get; }

        public Bitmap Image { get; }

        public Mosaic(LdPart baseplate, LdPart part, LdColor[,] colors, Bitmap image)
        {
            Baseplate = baseplate;
            Part = part;
            Colors = colors;
            Image = image;
        }
    }
}
