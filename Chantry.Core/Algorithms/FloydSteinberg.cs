using SkiaSharp;
using System;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.Algorithms
{
	public class FloydSteinberg : IAlgorithm
	{
		private Error[,] _errors;
		private SKSizeI _size;

		private const float EAST_ERROR = 7 / 16f;
		private const float SOUTHEAST_ERROR = 1 / 16f;
		private const float SOUTH_ERROR = 5 / 16f;
		private const float SOUTHWEST_ERROR = 3 / 16f;

		public FloydSteinberg()
		{
			Reset(new SKSizeI(1, 1));
		}

		public string DisplayName => "Floyd Steinberg";

		public LdColor GetColor(int x, int y, SKColor sourceColor, ColorService colorService)
		{
			// get average pixel color.

			float sourceRed = sourceColor.Red;
			float sourceGreen = sourceColor.Green;
			float sourceBlue = sourceColor.Blue;

			// apply the error.

			Error error = _errors[x, y];

			sourceRed += error.RedError;
			sourceGreen += error.GreenError;
			sourceBlue += error.BlueError;

			// get closest color.

			SKColor colorWithErrorApplied = new SKColor(
				(byte)Math.Clamp((int)sourceRed, 0, 255),
				(byte)Math.Clamp((int)sourceGreen, 0, 255),
				(byte)Math.Clamp((int)sourceBlue, 0, 255));

			LdColor newColor = colorService.GetClosestLdColor(colorWithErrorApplied);

			// calculate the error.

			Error calculatedError = new Error(
				colorWithErrorApplied.Red - newColor.Color.Red,
				colorWithErrorApplied.Green - newColor.Color.Green,
				colorWithErrorApplied.Blue - newColor.Color.Blue);

			// propagate the error.

			_errors[x + 1, y + 0].AddFraction(calculatedError, EAST_ERROR);
			_errors[x + 1, y + 1].AddFraction(calculatedError, SOUTHEAST_ERROR);
			_errors[x + 0, y + 1].AddFraction(calculatedError, SOUTH_ERROR);

			if (x != 0)
				_errors[x - 1, y + 1].AddFraction(calculatedError, SOUTHWEST_ERROR);

			return newColor;
		}

		public void Reset(SKSizeI size)
		{
			if (size != _size)
			{
				_errors = new Error[size.Width + 1, size.Height + 1];
				for (int x = 0; x < size.Width + 1; x++)
				{
					for (int y = 0; y < size.Height + 1; y++)
					{
						_errors[x, y] = new Error();
					}
				}
			}
			else
			{
				for (int x = 0; x < size.Width + 1; x++)
				{
					for (int y = 0; y < size.Height + 1; y++)
					{
						_errors[x, y].Reset();
					}
				}
			}

			_size = size;
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

			public void Reset()
			{
				RedError = 0;
				GreenError = 0;
				BlueError = 0;
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
