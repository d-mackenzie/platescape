using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.App.Extensions
{
    public static class BitmapExtensions
    {
        public static Avalonia.Media.Imaging.Bitmap AsAvaloniaMediaImagingBitmap(this System.Drawing.Bitmap sdBitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                sdBitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                return new Avalonia.Media.Imaging.Bitmap(memory);
            }
        }
    }
}
