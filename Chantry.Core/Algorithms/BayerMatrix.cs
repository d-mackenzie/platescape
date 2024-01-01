using SkiaSharp;
using System;
using System.Linq;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.Algorithms
{
	public class BayerMatrix : IAlgorithm
	{
		private int _matrixSize = 2;

		private float[,] _matrix;

		public string DisplayName => "Bayer Matrix";

		public LdColor GetColor(int x, int y, SKColor sourceColor, ColorService colorService)
		{
			float spread = 255f / colorService.AllowedColors.ToList().Count();
			float matrixValue = _matrix[x % _matrixSize, y % _matrixSize];

			float red = sourceColor.Red + spread * (matrixValue - 0.5f);
			float green = sourceColor.Green + spread * (matrixValue - 0.5f);
			float blue = sourceColor.Blue + spread * (matrixValue - 0.5f);

			return colorService.GetClosestLdColor(new SKColor(
				(byte)Math.Clamp((int)(red), 0, 255),
				(byte)Math.Clamp((int)(green), 0, 255),
				(byte)Math.Clamp((int)(blue), 0, 255)));
		}

		public void Reset(SKSizeI size)
		{
			_matrix = new float[2, 2]
				{
					{ 0.25f, 0.75f },
					{ 1f,    0.5f }
				};
		}
	}
}
