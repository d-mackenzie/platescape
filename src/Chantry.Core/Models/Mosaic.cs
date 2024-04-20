using SkiaSharp;
using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.Core.Models
{
	public class Mosaic
	{
		public SKSizeI BaseplateSize { get; }

		public SKSizeI ElementSize { get; }

		public LdColor[,] Colors { get; }

		public SKBitmap Image { get; }

		public SKSizeI ElementExtent => new SKSizeI(Colors.GetLength(0), Colors.GetLength(1));

		public SKSizeI StudExtent => new SKSizeI(ElementExtent.Width * ElementSize.Width, ElementExtent.Height * ElementSize.Height);

		public SKSizeI BaseplateExtent => new SKSizeI(StudExtent.Width / BaseplateSize.Width, StudExtent.Height / BaseplateSize.Height);

		public Mosaic(SKSizeI baseplateSize, SKSizeI elementSize, LdColor[,] colors, SKBitmap image)
		{
			BaseplateSize = baseplateSize;
			ElementSize = elementSize;
			Colors = colors;
			Image = image;
		}
	}
}
