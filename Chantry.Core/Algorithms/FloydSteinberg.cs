using SkiaSharp;
using System;
using System.Drawing;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.Algorithms
{
	public class FloydSteinberg : Algorithm
	{
		private const float EAST_ERROR = 7 / 16f;
		private const float SOUTHEAST_ERROR = 1 / 16f;
		private const float SOUTH_ERROR = 5 / 16f;
		private const float SOUTHWEST_ERROR = 3 / 16f;

		public FloydSteinberg(ColorService colorService) : base(colorService) { }

		public override string DisplayName => "Floyd Steinberg";

		protected override void GetMosaic(SKColor[,] sourcePixels, LdColor[,] mosaic)
		{
			int width = sourcePixels.GetLength(0);
			int height = sourcePixels.GetLength(1);

			var errors = new Error[width + 1, height + 1];

			for (int y = 0; y < height + 1; y++)
			{
				for (int x = 0; x < width + 1; x++)
				{
					errors[x, y] = new Error();
				}
			}

			foreach ((int x, int y, SKColor sourceColor) in sourcePixels.Each())
			{
				float sourceRed = sourceColor.Red;
				float sourceGreen = sourceColor.Green;
				float sourceBlue = sourceColor.Blue;

				// apply the error.

				Error error = errors[x, y];

				sourceRed += error.RedError;
				sourceGreen += error.GreenError;
				sourceBlue += error.BlueError;

				// get closest color.

				SKColor colorWithErrorApplied = new SKColor(
					(byte)Math.Clamp(sourceRed, 0, 255),
					(byte)Math.Clamp(sourceGreen, 0, 255),
					(byte)Math.Clamp(sourceBlue, 0, 255));

				LdColor newColor = GetClosestLdColor(colorWithErrorApplied);

				// calculate the error.

				Error calculatedError = new Error(
					colorWithErrorApplied.Red - newColor.Color.Red,
					colorWithErrorApplied.Green - newColor.Color.Green,
					colorWithErrorApplied.Blue - newColor.Color.Blue);

				// propagate the error.

				errors[x + 1, y + 0].AddFraction(calculatedError, EAST_ERROR);
				errors[x + 1, y + 1].AddFraction(calculatedError, SOUTHEAST_ERROR);
				errors[x + 0, y + 1].AddFraction(calculatedError, SOUTH_ERROR);

				if (x != 0)
					errors[x - 1, y + 1].AddFraction(calculatedError, SOUTHWEST_ERROR);

				mosaic[x, y] = newColor;
			}
		}

		private class Error
		{
			public float RedError = 0;
			public float GreenError = 0;
			public float BlueError = 0;

			public Error() { }

			public Error(float redError, float greenError, float blueError)
			{
				RedError = redError;
				GreenError = greenError;
				BlueError = blueError;
			}

			public void AddFraction(Error error, float fraction)
			{
				RedError += error.RedError * fraction;
				GreenError += error.GreenError * fraction;
				BlueError += error.BlueError * fraction;
			}
		}
	}
}
