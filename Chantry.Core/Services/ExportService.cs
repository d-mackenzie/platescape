using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Exporters;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.Core.Services
{
    public class ExportService
    {
        public void Export(Mosaic mosaic, ExportSettings settings, IExporter exporter)
        {
            if (!settings.OneFilePerBaseplate)
            {
                exporter.Export(mosaic, new FileStream(settings.Filename, FileMode.Create));
            }
            else
            {
                // split mosaic,

                // foreach baseplate...
            }
        }
    }
}
