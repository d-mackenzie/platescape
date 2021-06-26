using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Sources
{
    public class FileSource : ISource
    {
        public string Filename { get; set; }

        public Bitmap GetImage()
        {
            if (File.Exists(Filename))
            {
                return new Bitmap(Filename);
            }

            return new Bitmap(100, 100);
        }
    }
}
