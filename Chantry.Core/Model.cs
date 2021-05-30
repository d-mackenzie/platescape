using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace TeethInc.Chantry.Core
{
    public class Model : INotifyPropertyChanged
    {
        public Source Source { get; set; }

        public List<Filter> Filters { get; set; }

        public Mosaic Mosaic { get; set; }

        public Bitmap SourceImage { get; set; }

        public Bitmap FilteredImage { get; set; }

        public MosaicImage MosaicImage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Model()
        {
            Filters = new List<Filter>();
        }

        public Bitmap GetSourceImage()
        {
            return Source.GetImage();
        }

        public Bitmap GetFilteredImage()
        {
            var filteredImage = GetSourceImage();

            foreach (Filter filter in Filters)
            {
                if (filter.Enabled)
                    filteredImage = filter.GetFilteredImage(filteredImage);
            }

            return filteredImage;
        }
    }
}
