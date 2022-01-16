using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using TeethInc.Chantry.Core;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.MosaicAlgorithms;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Sources;
using SkiaSharp;
using TeethInc.Chantry.App.ViewModels;

namespace TeethInc.Chantry.Console
{
    class Program
    {

        private const int BASEPLATE_32 = 3811;
        private const int BASEPLATE_48 = 4186;

        static void Main(string[] args)
        {
            Project project = Project.CreateSimpleProject(@"c:\temp\eric.jpg");
            var projectViewModel = new ProjectViewModel(project);
        }

        private static void SerializeTest(Project project, string filename)
        {
            string json = ProjectService.SerializeProject(project);

            File.WriteAllText(filename, json);
        }

        private static Project DeserialiseTest(string filename)
        {
            return ProjectService.DeserializeProject(
                File.ReadAllText(filename));
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
                TargetElementExtent = new SKSizeI(320, 320)
            };

            MosaicService mosaicService = new MosaicService(ldrawService);
            mosaicService.BaseplateExtent = new SKSizeI(10, 10);

            System.Console.WriteLine("Filter image.");

            var sw = Stopwatch.StartNew();
            SKBitmap filteredImage = filterService.GetFilteredImage();
            System.Console.WriteLine($"Filter took {sw.ElapsedMilliseconds}ms.");

            System.Console.WriteLine("Wait 5 seconds...");
            Thread.Sleep(TimeSpan.FromMilliseconds(5000));

            System.Console.WriteLine("Go!");

            Mosaic mosaic = null;
            var algo = new FloydSteinberg();

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
