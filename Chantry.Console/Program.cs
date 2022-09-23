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

        private const int BASEPLATE_32 = 3811;
        private const int BASEPLATE_48 = 4186;

        static void Main(string[] args)
        {
            Project project = Project.CreateSimpleProject(@"c:\temp\eric-avatar.jpg");
            project.BaseplateExtent = new SKSizeI(1, 1);
            project.ExportLdraw(@"c:\temp\eric.ldr");
        }
    }
}
