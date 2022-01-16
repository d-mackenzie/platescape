using Newtonsoft.Json;
using SkiaSharp;

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

        public abstract void ApplyFilter(SKBitmap image);
    }
}
