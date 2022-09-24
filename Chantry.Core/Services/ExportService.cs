using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Exporters;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Helpers;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.Core.Services
{
    public class ExportService
    {
        public void Export(Mosaic mosaic, ExportSettings settings, IExporter exporter)
        {
            if (!settings.OneFilePerBaseplate)
            {
                var sw = Stopwatch.StartNew();
                exporter.Export(mosaic, new FileStream(settings.Filename, FileMode.Create));
                Debug.WriteLine($"Wrote '{settings.Filename}' in {sw.ElapsedMilliseconds}ms.");
            }
            else
            {
                foreach (KeyValuePair<(int Column, int Row), Mosaic> kvp in GetMosaicBaseplates(mosaic))
                {
                    var sw = Stopwatch.StartNew();
                    string filename = Path.Join(settings.ExportFolder, FilenamePatternHelper.BuildFilename(settings.FilenamePattern, kvp.Key.Row + 1, kvp.Key.Column + 1));
                    exporter.Export(kvp.Value, new FileStream(filename, FileMode.Create));
                    Debug.WriteLine($"Wrote '{filename}' in {sw.ElapsedMilliseconds}ms.");
                }
            }
        }

        private Dictionary<(int, int), Mosaic> GetMosaicBaseplates(Mosaic mosaic)
        {
            var ret = new Dictionary<(int, int), Mosaic>();

            for (int x = 0; x < mosaic.BaseplateExtent.Width; x++)
            {
                for (int y = 0; y < mosaic.BaseplateExtent.Width; y++)
                {
                    SKRectI baseplateBounds = GetBaseplateBounds(mosaic.Baseplate, x, y);

                    ret[(x, y)] = new Mosaic(
                        mosaic.Baseplate,
                        mosaic.Part,
                        mosaic.Colors.GetRect(baseplateBounds),
                        mosaic.Image.GetRect(baseplateBounds));
                }
            }

            return ret;
        }

        private SKRectI GetBaseplateBounds(LdPart baseplate, int x, int y)
        {
            return new SKRectI()
            {
                Left = x * baseplate.Size.Width,
                Top = y * baseplate.Size.Height,
                Size = baseplate.Size
            };
        }
    }
}
