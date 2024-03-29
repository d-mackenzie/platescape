using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Models
{
    public interface IExportSettings
    {
        string Filename { get; set; }

        bool OneFilePerBaseplate { get; set; }

        string ExportFolder { get; set; }

        string FilenamePattern { get; set; }

        bool IsValid { get; }
    }
}
