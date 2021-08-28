using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Extensions
{
    public static class BitmapExtensions
    {
        public static void ApplyFilter(this Bitmap bitmap, Func<Color, Color> func)
        {
            if (bitmap.PixelFormat != PixelFormat.Format24bppRgb)
                throw new ArgumentException("Pixel format must be 24bpp rgb.");

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
            for (int i = 0; i < pixels.Length; i += 3)
            {
                Color unfilteredPixel = Color.FromArgb(pixels[i + 0], pixels[i + 1], pixels[i + 2]);
                Color filteredPixel = func.Invoke(unfilteredPixel);
                pixels[i + 0] = filteredPixel.R;
                pixels[i + 1] = filteredPixel.G;
                pixels[i + 2] = filteredPixel.B;
            }

            // copy the pixels onto the bitmap.
            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bitmapData.Scan0, bytes);

            // Unlock the bits.
            bitmap.UnlockBits(bitmapData);
        }

        public static (int, byte[]) ToPixelArray(this Bitmap bitmap)
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

            return (bitmapData.Stride, pixels);
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
