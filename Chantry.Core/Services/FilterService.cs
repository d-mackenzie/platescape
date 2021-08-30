using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Helpers;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core.Services
{
    public class FilterService
    {
        private const int MINIMUM_SIZE = 192;
        
        private ISource m_source;
        private Bitmap m_unfilteredImage;

        public List<Filter> Filters { get; set; }

        public ISource Source
        {
            get { return m_source; }
            set { m_source = value; m_unfilteredImage = null; }
        }

        public Size TargetElementExtent { get; set; }

        public FilterService(ISource source)
        {
            Filters = new List<Filter>();
            TargetElementExtent = new Size(192, 128);
            Source = source;
        }

        public Bitmap GetFilteredImage()
        {
            Bitmap image = GetUnfilteredImage().Clone() as Bitmap;

            foreach (Filter filter in Filters)
            {
                if (filter.Enabled)
                    filter.ApplyFilter(image);
            }

            return image;
        }

        public Bitmap GetUnfilteredImage()
        {
            if (m_unfilteredImage is null)
            {
                Bitmap sourceImage = Source.Image;

                Size targetSize = GetTargetImageSize(sourceImage.Size, TargetElementExtent, MINIMUM_SIZE);

                Bitmap scaledBitmap = new Bitmap(sourceImage, targetSize);

                m_unfilteredImage = new Bitmap(targetSize.Width, targetSize.Height, PixelFormat.Format32bppArgb);

                for (int x = 0; x < targetSize.Width; x++)
                {
                    for (int y = 0; y < targetSize.Height; y++)
                    {
                        m_unfilteredImage.SetPixel(x, y, scaledBitmap.GetPixel(x, y));
                    }
                }
            }

            return m_unfilteredImage;
        }

        public Size GetTargetImageSize(Size sourceSize, Size targetSize, int minimumSize)
        {
            if (sourceSize.IsSmallerThan(targetSize))
            {
                return sourceSize.GetSizeToFill(targetSize);
            }

            if (targetSize.IsSmallerThan(new Size(minimumSize, minimumSize)))
            {
                targetSize = targetSize.GetSizeToFill(new Size(minimumSize, minimumSize));
            }

            return sourceSize.GetSizeToFill(targetSize);
        }
    }
}
