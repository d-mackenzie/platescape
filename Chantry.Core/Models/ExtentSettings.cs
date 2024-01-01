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
		public LdPart Baseplate { get; set; }

		public LdPart Element { get; set; }

		public SKSizeI BaseplateExtent { get; set; }

		[JsonIgnore]
		public SKSizeI ElementExtent =>
			new SKSizeI(
				Baseplate.Size.Width * BaseplateExtent.Width / Element.Size.Width,
				Baseplate.Size.Height * BaseplateExtent.Height / Element.Size.Height);

		[JsonIgnore]
		public SKSizeI StudExtent =>
			new SKSizeI(
				Baseplate.Size.Width * BaseplateExtent.Width,
				Baseplate.Size.Height * BaseplateExtent.Height);

		public string ToPhysicalSizeDisplayString()
		{
			int width = BaseplateExtent.Width * Baseplate.Size.Width;
			int height = BaseplateExtent.Height * Baseplate.Size.Height;

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
