using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdBitmap = System.Drawing.Bitmap;
using AmiBitmap = Avalonia.Media.Imaging.Bitmap;
using System.Drawing;
using Avalonia;

namespace TeethInc.Chantry.App.Extensions
{
    public static class BitmapExtensions
    {
        public static AmiBitmap AsAvaloniaMediaImagingBitmap(this SdBitmap sdBitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                sdBitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                return new AmiBitmap(memory);

//                return new AmiBitmap(Avalonia.Platform.PixelFormat.Rgba8888, sdBitmap.GetHbitmap(), PixelSize.Empty, Vector.One, sdBitmap.str
            }
        }
    }
}
