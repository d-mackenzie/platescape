using SkiaSharp;
using System;
using System.Collections.Generic;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core.Services
{
    public class FilterService
    {        
        private ISource _source;

        public List<Filter> Filters { get; set; }

        public ISource Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public FilterService(ISource source)
        {
            Filters = new List<Filter>();
            Source = source;
        }

        public SKBitmap GetFilteredImage()
        {
            SKBitmap image = Source.Image.Copy();

            foreach (Filter filter in Filters)
            {
                filter.ApplyTo(image);
            }

            return image;
        }
    }
}
