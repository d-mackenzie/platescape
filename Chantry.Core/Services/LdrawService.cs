using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.Core.Services
{
    public class LdrawService
    {
        private List<LdColor> m_colors;

        private List<LdPart> m_parts;

        public LdrawService()
        {
            m_colors = new List<LdColor>()
            {
                new LdColor(0, "Black", "05131D"),
                new LdColor(1, "Blue", "0055BF"),
                new LdColor(2, "Green", "257A3E"),
                new LdColor(4, "Red", "C91A09"),
                new LdColor(9, "Light Blue", "B4D2E3"),
                new LdColor(10, "Bright Green", "4B9F4A"),
                new LdColor(14, "Yellow", "F2CD37"),
                new LdColor(15, "White", "FFFFFF"),
                new LdColor(19, "Tan", "E4CD9E"),
                new LdColor(22, "Purple", "81007B"),
                new LdColor(25, "Orange", "FE8A18"),
                new LdColor(26, "Magenta", "923978"),
                new LdColor(27, "Lime", "BBE90B"),
                new LdColor(28, "Dark Tan", "958A73"),
                new LdColor(29, "Bright Pink", "E4ADC8"),
                new LdColor(70, "Reddish Brown", "582A12"),
                new LdColor(71, "Light Bluish Gray", "A0A5A9"),
                new LdColor(72, "Dark Bluish Gray", "6C6E68"),
                new LdColor(73, "Medium Blue", "5C9DD1"),
                new LdColor(118, "Aqua", "B3D7D1"),
                new LdColor(272, "Dark Blue", "0D325B"),
                new LdColor(288, "Dark Green", "184632"),
                new LdColor(308, "Dark Brown", "352100"),
                new LdColor(320, "Dark Red", "720E0F"),
                new LdColor(321, "Dark Azure", "1498D7"),
                new LdColor(322, "Medium Azure", "3EC2DD"),
                new LdColor(323, "Light Aqua", "BDDCD8"),
                new LdColor(330, "Olive Green", "9B9A5A"),
                new LdColor(378, "Sand Green", "A0BCAC"),
                new LdColor(379, "Sand Blue", "597184"),
                new LdColor(484, "Dark Orange", "A95500")
            };

            m_parts = new List<LdPart>()
            {
                new LdPart(4186, "Baseplate 48 x 48", new Size(48, 48)),
                new LdPart(3811, "Baseplate 32 x 32", new Size(32, 32)),
                new LdPart(3867, "Baseplate 16 x 16", new Size(16, 16)),
                new LdPart(3024, "Plate 1 x 1", new Size(1, 1)),
                new LdPart(3022, "Plate 2 x 2", new Size(2, 2)),
                new LdPart(3005, "Brick 1 x 1", new Size(1, 1)),
                new LdPart(3004, "Brick 2 x 2", new Size(2, 2))
            };
        }

        public LdColor GetColor(int ldrawColorNumber)
        {
            return m_colors.FirstOrDefault(x => x.Number == ldrawColorNumber);
        }

        public LdPart GetPart(int ldrawElementNumber)
        {
            return m_parts.FirstOrDefault(x => x.Number == ldrawElementNumber);
        }

        public LdColor[] GetColors(int[] ldrawColorNumbers)
        {
            return m_colors.Where(x => ldrawColorNumbers.Contains(x.Number)).ToArray();
        }
    }
}
