using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Helpers
{
    public static class FilenamePatternHelper
    {
        public static string BuildFilename(string filenamePattern, int row, int column)
        {
            string filename = filenamePattern.Replace("{row}", row.ToString("00"), true, CultureInfo.InvariantCulture);
            filename = filename.Replace("{col}", column.ToString("00"), true, CultureInfo.InvariantCulture);

            return filename;
        }
    }
}
