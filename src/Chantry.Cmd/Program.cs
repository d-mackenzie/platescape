using SkiaSharp;
using System.Diagnostics;
using TeethInc.Chantry.Core.Exporters;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.Core.Services;

namespace Chantry.Cmd
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var project = ProjectService.Load("c:\\temp\\eric-avatar.jpg");

			project.ExtentSettings.BaseplateSize = new SKSizeI(16, 16);
			project.ExtentSettings.BaseplateExtent = new SKSizeI(1, 1);
			project.ExtentSettings.ElementSize = new SKSizeI(2, 2);

			var mosaic = project.Mosaic;

			var pngExporter = new PngExporter(new ExportPngSettings()
			{
				PixelsPerStud = 50,
				DrawOutlines = true,
				DrawStuds = true
			});

			using (var stream = new FileStream("c:\\temp\\eric-avatar-mosaic.png", FileMode.OpenOrCreate))
			{
				pngExporter.Export(mosaic, stream);
			}

			Process.Start(new ProcessStartInfo("c:\\temp\\eric-avatar-mosaic.png") { UseShellExecute = true });
		}
	}
}
