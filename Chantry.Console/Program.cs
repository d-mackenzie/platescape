using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using TeethInc.Chantry.Core;
using TeethInc.Chantry.Core.Filters;

namespace TeethInc.Chantry.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Model model = new Model();

            model.Source.Filename = @"C:\Users\Dave\Pictures\eric-avatar.jpg";
            model.Filters.Add(new GreyscaleFilter());
            model.Mosaic.TargetSize = new Size(32, 32);

            System.Console.WriteLine("Delay...");
            Thread.Sleep(TimeSpan.FromMilliseconds(5000));

            System.Console.WriteLine("Filter image.");

            Bitmap filteredImage = null;

            var sw = Stopwatch.StartNew();

            for (int i = 0; i < 20; i++)
            {
                filteredImage = model.GetFilteredImage();
            }

            System.Console.WriteLine($"Filtering took {sw.ElapsedMilliseconds}ms.");
            System.Console.WriteLine($"Avg. filter time = {sw.ElapsedMilliseconds / 20}ms.");

            System.Console.WriteLine("Save image.");

            filteredImage.Save(@"c:\temp\eric-avatar-filtered.png", ImageFormat.Png);

            System.Console.WriteLine("Saved.");
            
            System.Console.ReadKey(true);
        }
    }
}
