using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using TeethInc.Chantry.Core;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.MosaicAlgorithms;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            LdrawService ldrawService = new LdrawService();

            FilterService filterService = new FilterService(new FileSource(@"C:\Users\Dave\Pictures\eric-avatar.jpg"));
            filterService.TargetElementExtent = new Size(48, 48);
            filterService.Filters.Add(new GreyscaleFilter());

            MosaicService mosaicService = new MosaicService();

            mosaicService.Baseplate = ldrawService.GetPart(4186);
            mosaicService.Part = ldrawService.GetPart(3024);
            mosaicService.BaseplateExtent = new Size(1, 1);
            mosaicService.AllowedColors = ldrawService.GetColors(new int[] { 0, 15, 71, 72 }).ToList();

            System.Console.WriteLine("Filter image.");

            var sw = Stopwatch.StartNew();
            Bitmap filteredImage = filterService.GetFilteredImage();
            System.Console.WriteLine($"Filter took {sw.ElapsedMilliseconds}ms.\n");

            Dictionary<int, string> map = new Dictionary<int, string>()
            {
                { 0, " " },
                { 15, "\u2588" },
                { 71, "\u2593" },
                { 72, "\u2592" }
            };

            System.Console.WriteLine("Make mosaic.");

            sw.Restart();
            Mosaic mosaic = mosaicService.GetMosaic(filteredImage, new FloydSteinberg());
            System.Console.WriteLine($"Mosaic took {sw.ElapsedMilliseconds}ms.\n");

            System.Console.WriteLine("Output:\n\n");
            
            for (int y = 0; y < mosaic.Colors.GetUpperBound(1); y++)
            {
                for (int x = 0; x < mosaic.Colors.GetUpperBound(0); x++)
                {
                    System.Console.Write(map[mosaic.Colors[x, y].Number] + map[mosaic.Colors[x, y].Number]);
                }

                System.Console.WriteLine();
            }

            while (System.Console.ReadKey(true).Key != ConsoleKey.Escape)
            { }
        }
    }
}
