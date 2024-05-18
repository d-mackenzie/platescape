using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TeethInc.Chantry.Core.Exporters;
using TeethInc.Chantry.Core.Helpers;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.Core.Services
{
	public class ExportService
	{
		public event EventHandler<ProgressEventArgs> Progress;
		public event EventHandler Completed;

		public void Export(Mosaic mosaic, ExportSettings exportSettings)
		{
			IExporter exporter = GetExporter(exportSettings);

			if (!exportSettings.OneFilePerBaseplate)
			{
				WriteFile(exporter, mosaic, exportSettings.Filename);
				OnProgress(new ProgressEventArgs() { Done = 1, Total = 1 });
				OnCompleted(new EventArgs());
			}
			else
			{
				var mosaics = GetMosaicBaseplates(mosaic);
				int done = 0;

				foreach (KeyValuePair<(int Column, int Row), Mosaic> kvp in mosaics)
				{
					string filename = Path.Join(exportSettings.ExportFolder, FilenamePatternHelper.BuildFilename(exportSettings.FilenamePattern, kvp.Key.Row + 1, kvp.Key.Column + 1));
					WriteFile(exporter, kvp.Value, filename);
					done++;
					OnProgress(new ProgressEventArgs() { Done = done, Total = mosaics.Count });
				}

				OnCompleted(new EventArgs());
			}
		}

		private void OnProgress(ProgressEventArgs e)
		{
			Progress?.Invoke(this, e);
		}

		private void OnCompleted(EventArgs e)
		{
			Completed?.Invoke(this, e);
		}

		private Dictionary<(int, int), Mosaic> GetMosaicBaseplates(Mosaic mosaic)
		{
			var ret = new Dictionary<(int, int), Mosaic>();

			for (int x = 0; x < mosaic.BaseplateExtent.Width; x++)
			{
				for (int y = 0; y < mosaic.BaseplateExtent.Width; y++)
				{
					ret[(x, y)] = mosaic.GetBaseplateAsMosaic(x, y);
				}
			}

			return ret;
		}

		private IExporter GetExporter(ExportSettings exportSettings)
		{
			return exportSettings switch
			{
				LdrawExportSettings exportLdrawSettings => new LdrawExporter(exportLdrawSettings),
				PngExportSettings exportPngSettings => new PngExporter(exportPngSettings),
				_ => throw new Exception("Unhandled export settings.")
			};
		}

		private void WriteFile(IExporter exporter, Mosaic mosaic, string filename)
		{
			var sw = Stopwatch.StartNew();

			using (var fileStream = new FileStream(filename, FileMode.Create))
			{
				exporter.Export(mosaic, fileStream);
			}

			Debug.WriteLine($"Wrote '{filename}' in {sw.ElapsedMilliseconds}ms.");
		}
	}

	public class ProgressEventArgs
	{
		public int Done { get; init; }

		public int Total { get; init; }
	}
}
