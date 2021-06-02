using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core
{
    public class Mosaic
    {
        public int BaseplatePartNumber { get; set; }

        public int ElementPartNumber { get; set; }

        public Size BaseplateExtent { get; set; }

        public Size ElementExtent
        {
            get
            {
                Size baseplateSize = Ldraw.Baseplates.FirstOrDefault(x => x.Number == BaseplatePartNumber).Size;
                Size elementSize = Ldraw.Elements.FirstOrDefault(x => x.Number == ElementPartNumber).Size;

                return new Size(
                    baseplateSize.Width * BaseplateExtent.Width / elementSize.Width,
                    baseplateSize.Height * BaseplateExtent.Height / elementSize.Height);
            }
        }
    }
}
