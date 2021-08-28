using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using TeethInc.Chantry.Core;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.MosaicAlgorithms;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Sources;
using SdBitmap = System.Drawing.Bitmap;
using AmiBitmap = Avalonia.Media.Imaging.Bitmap;
using TeethInc.Chantry.App.Extensions;
using SConsole = System.Console;

namespace TeethInc.Chantry.Console
{
    class Program
    {

        private const int BASEPLATE_32 = 3811;
        private const int BASEPLATE_48 = 4186;

        static void Main(string[] args)
        {
            FullStackTest();

            while (System.Console.ReadKey(true).Key != ConsoleKey.Escape)
            { }
        }

        private static void BitmapTest()
        {
            SdBitmap sdBitmap = new SdBitmap(@"C:\Users\David\Pictures\eric-avatar.jpg");

            SConsole.WriteLine("Ready...");
            SConsole.ReadKey(true);

            var sw = Stopwatch.StartNew();

            AmiBitmap amiBitmap = new AmiBitmap(@"C:\Users\David\Pictures\eric-avatar.jpg");

//            AmiBitmap amiBitmap = sdBitmap.AsAvaloniaMediaImagingBitmap();

            SConsole.WriteLine(sw.ElapsedMilliseconds);
        }

        private static void DeserialiseTest()
        {
            Project project = ProjectService.DeserializeProject(
                File.ReadAllText(@"C:\Users\david\TeethInc\chantry\project.json"));
        }

        private static void FullStackTest()
        {
            LdrawService ldrawService = new LdrawService();

            FilterService filterService = new FilterService(
                new FileSource
                {
                    Filename = @"C:\Users\David\Pictures\eric-avatar.jpg"
                })
            {
                TargetElementExtent = new Size(160, 160)
            };

            MosaicService mosaicService = new MosaicService();

            System.Console.WriteLine("Filter image.");

            var sw = Stopwatch.StartNew();
            Bitmap filteredImage = filterService.GetFilteredImage();
            System.Console.WriteLine($"Filter took {sw.ElapsedMilliseconds}ms.");

            System.Console.WriteLine("Wait 5 seconds...");
            Thread.Sleep(TimeSpan.FromMilliseconds(5000));

            System.Console.WriteLine("Go!");

            Mosaic mosaic = null;
            var colorService = new ColorService(mosaicService.AllowedColors.ToArray());
            var algo = new NearestColor(colorService);

            sw.Restart();

            for (int i = 0; i < 100; i++)
            {
                mosaic = mosaicService.GetMosaic(filteredImage, algo);
            }

            System.Console.WriteLine($"Average: {sw.ElapsedMilliseconds / 100}ms.");

            //System.Console.WriteLine("\nSave ldr.");

            //sw.Restart();
            //File.WriteAllLines(@"c:\temp\eric-mosaic.ldr", ldrawService.GetLdrawFile(mosaic).ToList());
            //System.Console.WriteLine($"Save took {sw.ElapsedMilliseconds}ms.\n");

            System.Console.WriteLine("Done.");
        }
    }
}
