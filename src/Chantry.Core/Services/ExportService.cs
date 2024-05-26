using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TeethInc.Chantry.Core.Exporters;
using TeethInc.Chantry.Core.Helpers;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.Core.Services
{
	public class ExportService<T> where T : IExporter
	{
		public OutputSettings OutputSettings { get; private set; }

		public T Exporter { get; private set; }

		public bool IsValid => OutputSettings.IsValid && Exporter.IsValid;

		public event EventHandler<ProgressEventArgs> Progress;
		public event EventHandler Completed;

		public ExportService(T exporter)
		{
			Exporter = exporter;
			OutputSettings = new OutputSettings();
		}

		public void Export(Mosaic mosaic)
		{
			if (!IsValid)
				throw new InvalidOperationException();

			if (!OutputSettings.OneFilePerBaseplate)
			{
				WriteMosaicToFile(Exporter, mosaic, OutputSettings.Filename);
				OnProgress(new ProgressEventArgs() { Done = 1, Total = 1 });
				OnCompleted(new EventArgs());
			}
			else
			{
				var mosaics = GetMosaicBaseplates(mosaic);
				int done = 0;

				foreach (KeyValuePair<(int Column, int Row), Mosaic> kvp in mosaics)
				{
					string filename = Path.Join(OutputSettings.ExportFolder, FilenamePatternHelper.BuildFilename(OutputSettings.FilenamePattern, kvp.Key.Row + 1, kvp.Key.Column + 1));
					WriteMosaicToFile(Exporter, kvp.Value, filename);
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

		private void WriteMosaicToFile(IExporter exporter, Mosaic mosaic, string filename)
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

	public class OutputSettings
	{
		public string Filename { get; set; } = "";

		public bool OneFilePerBaseplate { get; set; } = false;

		public string ExportFolder { get; set; } = "";

		public string FilenamePattern { get; set; } = "";

		public bool IsValid
		{
			get
			{
				if (OneFilePerBaseplate)
				{
					return !string.IsNullOrWhiteSpace(ExportFolder) &&
						   !string.IsNullOrWhiteSpace(FilenamePattern);
				}
				else
				{
					return !string.IsNullOrWhiteSpace(Filename);
				}
			}
		}
	}



}
