using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Filters;

namespace TeethInc.Chantry.Core.Extensions
{
    public static class EnumExtensions
    {
        public static float GetFactor(this TonalRange tonalRange, int value)
        {
            return 1f;
        }
    }
}
