using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Models
{
    public class ExportSettings
    {
        public string Filename { get; set; }

        public bool OneFilePerBaseplate { get; set; }

        public string ExportFolder { get; set; }

        public string FilenamePattern { get; set; }
    }
}
