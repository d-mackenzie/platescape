using SkiaSharp;
using System;
using System.Linq;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.Algorithms
{
	public class BayerMatrix : Algorithm
	{
		private const int MATRIX_SIZE = 2;
		private float[,] _matrix = new float[MATRIX_SIZE, MATRIX_SIZE]
		{
			{ 0.25f, 0.75f },
			{ 1f,    0.5f }
		};

		public override string DisplayName => "Bayer Matrix";

		protected override void GetMosaic(SKBitmap sourceImage, LdColor[,] mosaic)
		{
			float spread = 255f / 8;

			foreach ((int x, int y, SKColor sourceColor) in sourceImage.Pixels.As2dIEnumerable(sourceImage.Width, sourceImage.Height))
			{
				float matrixValue = _matrix[x % MATRIX_SIZE, y % MATRIX_SIZE] - 0.5f;

				float red = sourceColor.Red + (spread * matrixValue);
				float green = sourceColor.Green + (spread * matrixValue);
				float blue = sourceColor.Blue + (spread * matrixValue);

				mosaic[x, y] = GetClosestLdColor(new SKColor(
					(byte)Math.Clamp(red, 0, 255),
					(byte)Math.Clamp(green, 0, 255),
					(byte)Math.Clamp(blue, 0, 255)));
			}
		}
	}
}
