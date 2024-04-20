using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.Models
{
	public class ExtentSettings
	{
		public SKSizeI BaseplateSize { get; set; }

		public SKSizeI ElementSize { get; set; }

		public SKSizeI BaseplateExtent { get; set; }

		[JsonIgnore]
		public SKSizeI ElementExtent =>
			new SKSizeI(
				BaseplateSize.Width * BaseplateExtent.Width / ElementSize.Width,
				BaseplateSize.Height * BaseplateExtent.Height / ElementSize.Height);

		[JsonIgnore]
		public SKSizeI StudExtent =>
			new SKSizeI(
				BaseplateSize.Width * BaseplateExtent.Width,
				BaseplateSize.Height * BaseplateExtent.Height);

		public string ToPhysicalSizeDisplayString()
		{
			int width = BaseplateExtent.Width * BaseplateSize.Width;
			int height = BaseplateExtent.Height * BaseplateSize.Height;

			return $"{ElementExtent.Width} elements by {ElementExtent.Height} elements\n"
				+ $"{width} studs by {height} studs\n"
				+ $"{MmToMetric(width * 8)} by {MmToMetric(height * 8)}\n"
				+ $"{MmToImperial(width * 8)} by {MmToImperial(height * 8)}\n";
		}

		private string MmToImperial(int mm)
		{
			int inches = (int)(mm / 25.4f);

			if (inches < 12)
				return $"{inches}in";

			if (inches % 12 == 0)
				return $"{inches / 12}ft";

			return $"{inches / 12}ft {inches % 12}in";
		}

		private string MmToMetric(int mm)
		{
			if (mm >= 1000)
				return string.Format("{0:F1}m", mm / 1000d);

			return string.Format("{0:F0}cm", mm / 10d);
		}
	}
}
