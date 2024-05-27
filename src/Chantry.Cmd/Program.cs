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
			project.ExtentSettings.BaseplateExtent = new SKSizeI(2, 2);
			project.ExtentSettings.ElementSize = new SKSizeI(1, 1);

			var mosaic = project.Mosaic;

			var exporter = new PngExporter()
			{
				PixelsPerStud = 96,
				DrawOutlines = true,
				DrawStuds = true,
				ExportFolder = "c:\\temp",
				FilenamePattern = "eric-avatar-{row}-{col}.png",
				OneFilePerBaseplate = true
			};

			var sw = Stopwatch.StartNew();

			DoExport(mosaic, exporter);

			Debug.WriteLine(sw.ElapsedMilliseconds);
		}

		static async void DoExport(Mosaic mosaic, Exporter exporter)
		{
			await ExportWorker(mosaic, exporter);
		}

		static Task ExportWorker(Mosaic mosaic, Exporter exporter)
		{
			exporter.Progress += ExportService_Progress;
			exporter.ToFile(mosaic);
			Task.WaitAll();
			return Task.CompletedTask;
		}

		private static void ExportService_Progress(object? sender, ProgressEventArgs e)
		{
			Debug.WriteLine($"{e.Done} of {e.Total}");
		}
	}
}
