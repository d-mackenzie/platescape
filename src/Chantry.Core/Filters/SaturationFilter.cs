using SkiaSharp;
using System;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Helpers;

namespace TeethInc.Chantry.Core.Filters
{
	public class SaturationFilter : Filter
	{
		public override string DisplayName => "Saturation";

		public double Saturation { get; set; }

		private byte[] _red;
		private byte[] _green;
		private byte[] _blue;

		public SaturationFilter() : base()
		{
			Saturation = 1d;

			_red = GetCalculatedArray(0.21f);
			_green = GetCalculatedArray(0.71f);
			_blue = GetCalculatedArray(0.07f);
		}

		private byte[] GetCalculatedArray(float factor)
		{
			var ret = new byte[256];

			for (int i = 0; i < 256; i++)
			{
				ret[i] = (byte)(i * factor);
			}

			return ret;
		}

		protected override void ApplyFilter(SKBitmap image)
		{
			if (Saturation != 1)
			{
				image.ApplyFilter(GetSaturationPixel);
			}
		}

		private SKColor GetSaturationPixel(SKColor unfilteredPixel)
		{
			byte luminosity = (byte)(_red[unfilteredPixel.Red] + _green[unfilteredPixel.Green] + _blue[unfilteredPixel.Blue]);

			byte red = MathHelper.Lerp(luminosity, unfilteredPixel.Red, Saturation);
			byte green = MathHelper.Lerp(luminosity, unfilteredPixel.Green, Saturation);
			byte blue = MathHelper.Lerp(luminosity, unfilteredPixel.Blue, Saturation);

			return new SKColor(red, green, blue);
		}
	}
}