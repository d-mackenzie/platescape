using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Converters;

namespace TeethInc.Chantry.Core.Filters
{
    public abstract class Filter
    {
        public bool Enabled { get; set; }

        public Filter()
        {
            Enabled = true;
        }

        [JsonIgnore]
        public abstract string DisplayName { get; }

        public abstract void ApplyFilter(Bitmap image);
    }
}
