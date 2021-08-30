using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Helpers;

namespace TeethInc.Chantry.Core.Extensions
{
    public static class BitmapExtensions
    {
        public static void ApplyFilter(this Bitmap bitmap, Func<Color, Color> func)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ArgumentException("Pixel format must be 32bpp argb.");

            BitmapData bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite,
                bitmap.PixelFormat);

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bitmapData.Stride) * bitmap.Height;
            byte[] pixels = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(bitmapData.Scan0, pixels, 0, bytes);

            // apply filter to each pixel.
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    int index = (y * bitmapData.Stride) + (x * 4);
                    Color unfilteredPixel = Color.FromArgb(pixels[index + 2], pixels[index + 1], pixels[index + 0]);
                    Color filteredPixel = func.Invoke(unfilteredPixel);
                    pixels[index + 2] = filteredPixel.R;
                    pixels[index + 1] = filteredPixel.G;
                    pixels[index + 0] = filteredPixel.B;
                }
            }

            // copy the pixels onto the bitmap.
            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bitmapData.Scan0, bytes);

            // Unlock the bits.
            bitmap.UnlockBits(bitmapData);
        }

        public static PixelData ToPixelData(this Bitmap bitmap)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ArgumentException("Pixel format must be 32bpp argb.");

            BitmapData bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite,
                bitmap.PixelFormat);

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bitmapData.Stride) * bitmap.Height;
            byte[] pixels = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(bitmapData.Scan0, pixels, 0, bytes);

            // Unlock the bits.
            bitmap.UnlockBits(bitmapData);

            return new PixelData(bitmapData.Stride, pixels);
        }

        public static void SetPixelArray(this Bitmap bitmap, byte[] pixels)
        {
            BitmapData bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite,
                bitmap.PixelFormat);

            // copy the pixels onto the bitmap.
            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);

            // Unlock the bits.
            bitmap.UnlockBits(bitmapData);
        }
    }
}
