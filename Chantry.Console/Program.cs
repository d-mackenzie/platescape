using System;
using System.Collections.Generic;
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

            model.Mosaic.BaseplatePartNumber = 3811;
            model.Mosaic.ElementPartNumber = 3024;
            model.Mosaic.BaseplateExtent = new Size(1, 1);
            model.Mosaic.LdrawColors = new int[] { 0, 15, 71, 72 };

            Bitmap bitmap = model.GetSourceImage();

            System.Console.WriteLine("Filter image.");
            Bitmap filteredImage = model.GetFilteredImage(bitmap);

            Dictionary<int, string> map = new Dictionary<int, string>()
            {
                { 0, "  " },
                { 15, "%%" },
                { 71, "//" },
                { 72, ".." }
            };

            System.Console.WriteLine("Make mosaic.");
            int[,] mosaic = model.Mosaic.GetMosaic(filteredImage);

            System.Console.WriteLine("Output:\n\n");
            
            for (int y = 0; y < mosaic.GetUpperBound(1); y++)
            {
                for (int x = 0; x < mosaic.GetUpperBound(0); x++)
                {
                    System.Console.Write(map[mosaic[x, y]]);
                }

                System.Console.WriteLine();
            }
            
            System.Console.ReadKey(true);
        }
    }
}
