using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Logging;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.Models
{
	public class LdColorCache
	{
		private static LdColor[,,] _closestLdColorCache = new LdColor[64, 64, 64];
		private static LdColor[] _cachedAllowedColors;


		public LdColorCache()
		{
			_cachedAllowedColors = new LdColor[0];
		}

		public void BuildCache(LdColor[] allowedColors)
		{
			if (!isCacheInValid(allowedColors))
				return;

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

			Logger.Debug($"BuildClosestLdColorCache(): {sw.ElapsedMilliseconds}ms.");
		}

		public LdColor GetClosestAllowedLdColor(SKColor color)
		{
			return _closestLdColorCache[color.Red >> 2, color.Green >> 2, color.Blue >> 2];
		}

		private bool isCacheInValid(LdColor[] allowedColors)
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

		private static LdColor CalculateClosestLdColor(SKColor color, LdColor[] allowedColors)
		{
			float minDistance = float.MaxValue;
			LdColor closestColor = null;

			foreach (LdColor ldColor in allowedColors)
			{
				// get distance.

				float distance = ColorService.GetDistanceBetween(color, ldColor);

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
