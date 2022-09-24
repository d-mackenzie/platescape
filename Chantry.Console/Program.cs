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
            Project project = Project.CreateSimpleProject(@"c:\temp\eric-avatar.jpg");
            project.BaseplateExtent = new SKSizeI(2, 2);

            ExportSettings settings = new ExportSettings()
            {
                ExportFolder = @"c:\temp",
                FilenamePattern = "eric_row_{row}_col_{col}.ldr",
                OneFilePerBaseplate = true
            };

            project.ExportLdraw(settings);

            settings = new ExportSettings()
            {
                Filename = @"c:\temp\eric.ldr",
                OneFilePerBaseplate = false
            };

            project.ExportLdraw(settings);
        }
    }
}
