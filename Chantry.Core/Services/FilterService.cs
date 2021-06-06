using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core.Services
{
    public class FilterService
    {
        private const int RESIZE_THRESHOLD = 6;

        private Bitmap m_cachedSource;
        
        private ISource m_source;

        public List<Filter> Filters { get; set; }

        public ISource Source
        {
            get { return m_source; }
            set { m_source = value; m_cachedSource = null; }
        }

        public Size TargetElementExtent { get; set; }

        public FilterService(ISource source)
        {
            Filters = new List<Filter>();
            TargetElementExtent = new Size(32, 32);
            Source = source;
        }

        public Bitmap GetFilteredImage()
        {
            if (m_cachedSource == null)
            {
                m_cachedSource = GetResizedImage();
            }

            Bitmap image = m_cachedSource.Clone() as Bitmap;

            foreach (Filter filter in Filters)
            {
                if (filter.Enabled)
                    image = filter.GetFilteredImage(image);
            }

            return image;
        }

        private Bitmap GetResizedImage()
        {
            Bitmap sourceImage = Source.GetImage();

            Size maximumSize = TargetElementExtent * RESIZE_THRESHOLD;

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
    }
}
