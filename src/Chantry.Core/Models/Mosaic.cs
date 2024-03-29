using SkiaSharp;
using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.Core.Models
{
    public class Mosaic
    {
        public LdPart Baseplate { get; }

        public LdPart Part { get; }

        public LdColor[,] Colors { get; }

        public SKBitmap Image { get; }

        public SKSizeI ElementExtent => new SKSizeI(Colors.GetLength(0), Colors.GetLength(1));

        public SKSizeI StudExtent => new SKSizeI(ElementExtent.Width * Part.Size.Width, ElementExtent.Height * Part.Size.Height);

        public SKSizeI BaseplateExtent => new SKSizeI(StudExtent.Width / Baseplate.Size.Width, StudExtent.Height / Baseplate.Size.Height);

        public Mosaic(LdPart baseplate, LdPart part, LdColor[,] colors, SKBitmap image)
        {
            Baseplate = baseplate;
            Part = part;
            Colors = colors;
            Image = image;
        }
    }
}
