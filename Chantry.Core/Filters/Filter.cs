using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Filters
{
    public abstract class Filter
    {
        public bool Enabled { get; set; }

        public Filter()
        {
            Enabled = true;
        }

        public string Type
        {
            get { return this.GetType().Name; }
        }

        public abstract Bitmap GetFilteredImage(Bitmap unfilteredImage);
    }
}
