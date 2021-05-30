using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace TeethInc.Chantry.Core
{
    public class Model : INotifyPropertyChanged
    {
        private int m_resizeThreshold = 4;

        public Source Source { get; set; }

        public List<Filter> Filters { get; set; }

        public Mosaic Mosaic { get; set; }

        public Bitmap SourceImage { get; set; }

        public Bitmap FilteredImage { get; set; }

        public MosaicImage MosaicImage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Model()
        {
            Source = new Source();
            Filters = new List<Filter>();
            Mosaic = new Mosaic();
        }

        public Bitmap GetSourceImage()
        {
            Bitmap sourceImage = Source.GetImage();
            Size maximumSize = Mosaic.TargetSize * m_resizeThreshold;

            float widthScaleFactor = (float)maximumSize.Width / sourceImage.Width;
            float heightScaleFactor = (float)maximumSize.Height / sourceImage.Height;

            float scaleFactor = Math.Min(widthScaleFactor, heightScaleFactor);

            Size targetSize = new Size((int)(sourceImage.Width * scaleFactor), (int)(sourceImage.Height * scaleFactor));

            return new Bitmap(sourceImage, targetSize);
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
