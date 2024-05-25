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
			project.ExtentSettings.BaseplateExtent = new SKSizeI(5, 5);
			project.ExtentSettings.ElementSize = new SKSizeI(2, 2);

			var mosaic = project.Mosaic;

			var exportSettings = new ExportSettings<PngExporter>(
				new PngExporter()
				{
					PixelsPerStud = 96,
					DrawOutlines = true,
					DrawStuds = true
				})
			{
				ExportFolder = "c:\\temp",
				FilenamePattern = "eric-avatar-{row}-{col}.png",
				OneFilePerBaseplate = true
			};

			var exportService = new ExportService();

			var sw = Stopwatch.StartNew();

			DoExport(mosaic, exportSettings);

			Debug.WriteLine(sw.ElapsedMilliseconds);
		}

		static async void DoExport(Mosaic mosaic, ExportSettings<PngExporter> exportSettings)
		{
			await ExportWorker(mosaic, exportSettings);
		}

		static Task ExportWorker(Mosaic mosaic, ExportSettings<PngExporter> exportSettings)
		{
			var exportService = new ExportService();
			exportService.Progress += ExportService_Progress;
			exportService.Export(mosaic, exportSettings);
			Task.WaitAll();
			return Task.CompletedTask;
		}

		private static void ExportService_Progress(object? sender, ProgressEventArgs e)
		{
			Debug.WriteLine($"{e.Done} of {e.Total}");
		}
	}
}
