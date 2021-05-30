using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core
{
    public abstract class Filter
    {
        public bool Enabled { get; set; }

        public string Type
        {
            get { return this.GetType().Name; }
        }

        public abstract Bitmap GetFilteredImage(Bitmap unfilteredImage);
    }
}
