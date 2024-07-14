using SkiaSharp;
using System;
using System.Diagnostics;
using System.Linq;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Logging;

namespace TeethInc.Chantry.Core.Services
{
	public static class ColorService
	{
		private static float[,,,] _distanceCache;
		private static int[] _ldColorIndex;

		static ColorService()
		{
			BuildDistanceCache();
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

			Logger.Debug($"BuildDistanceCache(): {sw.ElapsedMilliseconds}ms.");
		}

		public static float GetDistanceBetween(SKColor color, LdColor ldColor)
		{
			return _distanceCache[color.Red >> 2, color.Green >> 2, color.Blue >> 2, _ldColorIndex[ldColor.Number]];
		}
	}
}
