using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.App.Extensions
{
    public static class LinqExtensions
    {
        public static bool EndsWithAny(this string str, IEnumerable<string> list)
        {
            return list.Any(x => str.EndsWith(x, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
