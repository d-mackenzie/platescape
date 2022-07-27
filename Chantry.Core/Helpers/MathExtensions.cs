using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Helpers
{
    public class MathHelper
    {
        public static byte Lerp(byte first, byte second, double by)
        {
            return (byte)((double)first * (1 - by) + (double)second * by);
        }
    }
}
