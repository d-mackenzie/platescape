using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.MosaicAlgorithms;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Sources;
using SkiaSharp;
using TeethInc.Chantry.ViewModels;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Project project = ProjectService.CreateSimpleProject(@"c:\temp\eric-avatar.jpg");
            project.BaseplateExtent = new SKSizeI(2, 2);

            var json = project.Serialize();
            File.WriteAllText("c:\\temp\\eric-avatar.json", json);
        }
    }
}
