using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Extensions
{
	internal static class ArrayExtensions
	{
		public static T[,] GetRect<T>(this T[,] array, SKRectI rect)
		{
			var ret = new T[rect.Width, rect.Height];

			for (int x = 0; x < rect.Width; x++)
			{
				for (int y = 0; y < rect.Height; y++)
				{
					ret[x, y] = array[rect.Left + x, rect.Top + y];
				}
			}

			return ret;
		}
		public static IEnumerable<(int, int, SKColor)> As2dIEnumerable(this SKColor[] array, int width, int height)
		{
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					yield return (x, y, array[(y * width) + x]);
				}
			}
		}	}
}
