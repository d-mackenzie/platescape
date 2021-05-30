using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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

            System.Console.WriteLine("Load image.");
            model.SourceImage = model.GetSourceImage();

            System.Console.ReadKey(true);

            System.Console.WriteLine("Filter image.");
            
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < 10; i++)
            {
                model.FilteredImage = model.GetFilteredImage();
            }

            System.Console.WriteLine($"Filtering took {sw.ElapsedMilliseconds}ms.");

            System.Console.WriteLine("Save image.");
            model.FilteredImage.Save(@"c:\temp\eric-avatar-filtered.png", ImageFormat.Png);

            System.Console.WriteLine("Saved.");
            
            System.Console.ReadKey(true);
        }
    }
}
