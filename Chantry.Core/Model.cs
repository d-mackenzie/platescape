using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace TeethInc.Chantry.Core
{
    public class Model : INotifyPropertyChanged
    {
        private int m_resizeThreshold = 6;

        public Source Source { get; set; }

        public List<Filter> Filters { get; set; }

        public Mosaic Mosaic { get; set; }

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

            Bitmap scaledBitmap = new Bitmap(sourceImage, targetSize);

            Bitmap ret = new Bitmap(targetSize.Width, targetSize.Height, PixelFormat.Format24bppRgb);

            for (int x = 0; x < targetSize.Width; x++)
            {
                for (int y = 0; y < targetSize.Height; y++)
                {
                    ret.SetPixel(x, y, scaledBitmap.GetPixel(x, y));
                }
            }

            return ret;
        }
   
        public Bitmap GetFilteredImage(Bitmap bitmap)
        {
            foreach (Filter filter in Filters)
            {
                if (filter.Enabled)
                    bitmap = filter.GetFilteredImage(bitmap);
            }

            return bitmap;
        }
    }
}
