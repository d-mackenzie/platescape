using SkiaSharp;
using System;
using System.Diagnostics;
using System.Linq;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.Core.Services
{
	public static class ColorService
	{
		private static LdColor[] _cachedAllowedColors;
		private static LdColor[,,] _closestLdColorCache = new LdColor[64, 64, 64];
		private static float[,,,] _distanceCache;
		private static int[] _ldColorIndex;

		static ColorService()
		{
			BuildDistanceCache();
			_cachedAllowedColors = new LdColor[0];
		}

		public static LdColor GetClosestLdColor(SKColor color, LdColor[] allowedColors)
		{
			if (isCacheInvalid(allowedColors))
			{
				BuildClosestLdColorCache(allowedColors);
			}

			return _closestLdColorCache[color.Red >> 2, color.Green >> 2, color.Blue >> 2];
		}

		private static bool isCacheInvalid(LdColor[] allowedColors)
		{
			if (allowedColors.Length != _cachedAllowedColors.Length)
				return true;

			foreach (LdColor cachedAllowedColor in _cachedAllowedColors)
			{
				if (!allowedColors.Contains(cachedAllowedColor))
					return true;
			}

			return false;
		}

		private static void BuildDistanceCache()
		{
			var sw = Stopwatch.StartNew();

			_distanceCache = new float[64, 64, 64, LdrawService.Colors.Count];
			_ldColorIndex = new int[LdrawService.Colors.Select(x => x.Number).Max() + 1];

			for (int i = 0; i < LdrawService.Colors.Count; i++)
			{
				_ldColorIndex[LdrawService.Colors[i].Number] = i;
			}

			foreach (LdColor ldColor in LdrawService.Colors)
			{
				int ldColorIndex = _ldColorIndex[ldColor.Number];

				for (int r = 0; r < 256; r += 4)
				{
					for (int g = 0; g < 256; g += 4)
					{
						for (int b = 0; b < 256; b += 4)
						{
							SKColor color = new SKColor((byte)r, (byte)g, (byte)b);
							_distanceCache[r >> 2, g >> 2, b >> 2, ldColorIndex] = color.DistanceFrom(ldColor.Color);
						}
					}
				}
			}

			Debug.WriteLine($"BuildDistanceCache(): {sw.ElapsedMilliseconds}ms.");
		}

		private static void BuildClosestLdColorCache(LdColor[] allowedColors)
		{
			var sw = Stopwatch.StartNew();

			for (int r = 0; r < 256; r += 4)
			{
				for (int g = 0; g < 256; g += 4)
				{
					for (int b = 0; b < 256; b += 4)
					{
						_closestLdColorCache[r >> 2, g >> 2, b >> 2] = CalculateClosestLdColor(new SKColor((byte)r, (byte)g, (byte)b), allowedColors);
					}
				}
			}

			_cachedAllowedColors = allowedColors;

			Debug.WriteLine($"BuildClosestLdColorCache(): {sw.ElapsedMilliseconds}ms.");
		}

		private static LdColor CalculateClosestLdColor(SKColor color, LdColor[] allowedColors)
		{
			float minDistance = float.MaxValue;
			LdColor closestColor = null;

			foreach (LdColor ldColor in allowedColors)
			{
				// get distance.

				float distance = _distanceCache[color.Red >> 2, color.Green >> 2, color.Blue >> 2, _ldColorIndex[ldColor.Number]];

				if (distance < minDistance)
				{
					closestColor = ldColor;
					minDistance = distance;
				}
			}

			return closestColor;
		}
	}
}
