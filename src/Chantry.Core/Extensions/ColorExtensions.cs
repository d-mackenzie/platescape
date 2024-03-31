using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.Core.Extensions
{
	public static class ColorExtensions
	{
		public static float DistanceFrom(this SKColor me, SKColor color)
		{
			float deltaR = me.Red - color.Red;
			float deltaG = me.Green - color.Green;
			float deltaB = me.Blue - color.Blue;

			return
				(deltaR * 0.30f * deltaR * 0.30f) +
				(deltaG * 0.59f * deltaG * 0.59f) +
				(deltaB * 0.11f * deltaB * 0.11f);
		}

		public static LdColor ClosestLdColor(this SKColor me, IEnumerable<LdColor> allowedColors)
		{
			double minDistance = double.MaxValue;
			LdColor closestColor = null;

			foreach (LdColor ldrawColor in allowedColors)
			{
				// get distance.

				float distance = me.DistanceFrom(ldrawColor.Color);

				if (distance < minDistance)
				{
					closestColor = ldrawColor;
					minDistance = distance;
				}
			}

			return closestColor;
		}

		public static bool IsDarkColor(this SKColor skColor)
		{
			skColor.ToHsv(out float _, out float _, out float v);
			return (v <= 50);
		}
	}
}
