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
        private const int MINIMUM_SIZE = 192;
        
        private ISource _source;
        private SKBitmap _unfilteredImage;

        public List<Filter> Filters { get; set; }

        public ISource Source
        {
            get { return _source; }
            set { _source = value; _unfilteredImage = null; }
        }

        public SKSizeI TargetElementExtent { get; set; }

        public FilterService(ISource source)
        {
            Filters = new List<Filter>();
            TargetElementExtent = new SKSizeI(192, 128);
            Source = source;
        }

        public SKBitmap GetFilteredImage()
        {
            SKBitmap image = GetUnfilteredImage().Copy();

            foreach (Filter filter in Filters)
            {
                filter.ApplyTo(image);
            }

            return image;
        }

        public SKBitmap GetUnfilteredImage()
        {
            if (_unfilteredImage is null)
            {
                SKBitmap sourceImage = Source.Image;

                SKSizeI targetSize = GetTargetImageSize(sourceImage.Info.Size, TargetElementExtent, MINIMUM_SIZE);

                SKBitmap scaledBitmap = sourceImage.Resize(targetSize, SKFilterQuality.High);

                _unfilteredImage = new SKBitmap(targetSize.Width, targetSize.Height);

                for (int x = 0; x < targetSize.Width; x++)
                {
                    for (int y = 0; y < targetSize.Height; y++)
                    {
                        _unfilteredImage.SetPixel(x, y, scaledBitmap.GetPixel(x, y));
                    }
                }
            }

            return _unfilteredImage;
        }

        public SKSizeI GetTargetImageSize(SKSizeI sourceSize, SKSizeI targetSize, int minimumSize)
        {
            if (sourceSize.IsSmallerThan(targetSize))
            {
                return sourceSize.GetSizeToFill(targetSize);
            }

            if (targetSize.IsSmallerThan(new SKSizeI(minimumSize, minimumSize)))
            {
                targetSize = targetSize.GetSizeToFill(new SKSizeI(minimumSize, minimumSize));
            }

            return sourceSize.GetSizeToFill(targetSize);
        }
    }
}
