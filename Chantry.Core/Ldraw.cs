using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core
{
    public static class Ldraw
    {
        public static List<LdrawColor> Colors { get; }

        public static List<LdrawPart> Baseplates { get; }

        public static List<LdrawPart> Elements { get; }

        static Ldraw()
        {
            Colors = new List<LdrawColor>()
            {
                new LdrawColor(0, "Black", "05131D"),
                new LdrawColor(1, "Blue", "0055BF"),
                new LdrawColor(2, "Green", "257A3E"),
                new LdrawColor(4, "Red", "C91A09"),
                new LdrawColor(9, "Light Blue", "B4D2E3"),
                new LdrawColor(10, "Bright Green", "4B9F4A"),
                new LdrawColor(14, "Yellow", "F2CD37"),
                new LdrawColor(15, "White", "FFFFFF"),
                new LdrawColor(19, "Tan", "E4CD9E"),
                new LdrawColor(22, "Purple", "81007B"),
                new LdrawColor(25, "Orange", "FE8A18"),
                new LdrawColor(26, "Magenta", "923978"),
                new LdrawColor(27, "Lime", "BBE90B"),
                new LdrawColor(28, "Dark Tan", "958A73"),
                new LdrawColor(29, "Bright Pink", "E4ADC8"),
                new LdrawColor(70, "Reddish Brown", "582A12"),
                new LdrawColor(71, "Light Bluish Gray", "A0A5A9"),
                new LdrawColor(72, "Dark Bluish Gray", "6C6E68"),
                new LdrawColor(73, "Medium Blue", "5C9DD1"),
                new LdrawColor(118, "Aqua", "B3D7D1"),
                new LdrawColor(272, "Dark Blue", "0D325B"),
                new LdrawColor(288, "Dark Green", "184632"),
                new LdrawColor(308, "Dark Brown", "352100"),
                new LdrawColor(320, "Dark Red", "720E0F"),
                new LdrawColor(321, "Dark Azure", "1498D7"),
                new LdrawColor(322, "Medium Azure", "3EC2DD"),
                new LdrawColor(323, "Light Aqua", "BDDCD8"),
                new LdrawColor(330, "Olive Green", "9B9A5A"),
                new LdrawColor(378, "Sand Green", "A0BCAC"),
                new LdrawColor(379, "Sand Blue", "597184"),
                new LdrawColor(484, "Dark Orange", "A95500")
            };

            Baseplates = new List<LdrawPart>()
            {
                new LdrawPart(4186, "Baseplate 48 x 48", new Size(48, 48)),
                new LdrawPart(3811, "Baseplate 32 x 32", new Size(32, 32)),
                new LdrawPart(3867, "Baseplate 16 x 16", new Size(16, 16))
            };

            Elements = new List<LdrawPart>()
            {
                new LdrawPart(3024, "Plate 1 x 1", new Size(1, 1)),
                new LdrawPart(3022, "Plate 2 x 2", new Size(2, 2)),
                new LdrawPart(3005, "Brick 1 x 1", new Size(1, 1)),
                new LdrawPart(3004, "Brick 2 x 2", new Size(2, 2))
            };
        }
    }
}
