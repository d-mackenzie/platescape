using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.Core.Services
{
	public static class LdrawService
	{
		private static List<LdColor> _colors;

		private static List<LdPart> _baseplates;
		private static List<LdPart> _elements;

		public static List<LdPart> Baseplates => _baseplates;
		public static List<LdPart> Elements => _elements;
		public static List<LdColor> Colors => _colors;

		static LdrawService()
		{
			_colors = new List<LdColor>()
			{
				new LdColor(0, "Black", "05131D", 0),
				new LdColor(72, "Dk Bluish Gray", "6C6E68", 1),
				new LdColor(71, "Lt Bluish Gray", "A0A5A9", 2),
				new LdColor(15, "White", "FFFFFF", 3),

				new LdColor(308, "Dk Brown", "352100", 4),
				new LdColor(70, "Reddish Brown", "582A12", 5),
				new LdColor(320, "Dk Red", "720E0F", 6),
				new LdColor(4, "Red", "C91A09", 7),

				new LdColor(484, "Dk Orange", "A95500", 8),
				new LdColor(25, "Orange", "FE8A18", 9),

				new LdColor(28, "Dk Tan", "958A73", 9),
				new LdColor(19, "Tan", "E4CD9E", 10),
				new LdColor(14, "Yellow", "F2CD37", 11),

				new LdColor(288, "Dk Green", "184632", 12),
				new LdColor(330, "Olive Green", "9B9A5A", 13),
				new LdColor(378, "Sand Green", "A0BCAC", 14),
				new LdColor(2, "Green", "257A3E", 15),
				new LdColor(10, "Br Green", "4B9F4A", 16),
				new LdColor(27, "Lime", "BBE90B", 17),

				new LdColor(272, "Dk Blue", "0D325B", 18),
				new LdColor(379, "Sand Blue", "597184", 19),
				new LdColor(73, "Md Blue", "5C9DD1", 20),
				new LdColor(1, "Blue", "0055BF", 21),
				new LdColor(9, "Lt Blue", "B4D2E3", 22),
				new LdColor(321, "Dk Azure", "1498D7", 23),
				new LdColor(322, "Md Azure", "3EC2DD", 24),

				new LdColor(22, "Purple", "81007B", 25),
				new LdColor(26, "Magenta", "923978", 26),
				new LdColor(29, "Bt Pink", "E4ADC8", 27),
			};

			_baseplates = new List<LdPart>()
			{
				new LdPart("4186", "Baseplate 48 x 48", new SKSizeI(48, 48), 4),
				new LdPart("3811", "Baseplate 32 x 32", new SKSizeI(32, 32), 4),
				new LdPart("3867", "Baseplate 16 x 16", new SKSizeI(16, 16), 4)
			};

			_elements = new List<LdPart>()
			{
				new LdPart("3024", "Plate 1 x 1", new SKSizeI(1, 1), 8),
				new LdPart("3022", "Plate 2 x 2", new SKSizeI(2, 2), 8),
				new LdPart("3005", "Brick 1 x 1", new SKSizeI(1, 1), 24),
				new LdPart("3004", "Brick 2 x 2", new SKSizeI(2, 2), 24)
			};
		}

		public static LdColor GetColor(int ldrawColorNumber)
		{
			return _colors.FirstOrDefault(x => x.Number == ldrawColorNumber);
		}

		public static LdPart GetPart(string ldrawElementNumber)
		{
			return _baseplates.Union(_elements).FirstOrDefault(x => x.Number == ldrawElementNumber);
		}

		public static IEnumerable<LdColor> GetColors(int[] ldrawColorNumbers)
		{
			return _colors.Where(x => ldrawColorNumbers.Contains(x.Number));
		}
	}
}
