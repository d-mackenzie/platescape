using System.IO;
using AmiBitmap = Avalonia.Media.Imaging.Bitmap;
using SkiaSharp;

namespace TeethInc.Chantry.Extensions
{
    public static class BitmapExtensions
    {
        public static AmiBitmap AsAvaloniaMediaImagingBitmap(this SKBitmap skBitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                skBitmap.Encode(memory, SKEncodedImageFormat.Png, 100);
                memory.Position = 0;

                return new AmiBitmap(memory);
            }
        }
    }
}
