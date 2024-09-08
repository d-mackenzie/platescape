using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TeethInc.Chantry.Core.Helpers;
using TeethInc.Chantry.Core.Logging;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core.Exporters
{
	public abstract class Exporter
	{
		public string Filename { get; set; } = "";

		public bool OneFilePerBaseplate { get; set; } = false;

		public string ExportFolder { get; set; } = "";

		public string FilenamePattern { get; set; } = "";

		public bool IsOutputValid
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

		public bool IsValid => IsOutputValid && IsExporterValid;

		public event EventHandler<ProgressEventArgs> Progress;
		public event EventHandler Completed;

		public abstract bool IsExporterValid { get; }

		public abstract string DefaultExtension { get; }

		public abstract void ToStream(Mosaic mosaic, Stream stream);

		public void PopulateOutputSettings(ISource source)
		{
			switch (source)
			{
				case ScaledImageSource scaledImageSource:
					string filename = scaledImageSource.OriginalFilename;
					ExportFolder = Path.GetDirectoryName(filename);
					Filename = Path.Combine(ExportFolder, $"{Path.GetFileNameWithoutExtension(filename)}_mosaic.{DefaultExtension}");
					FilenamePattern = Path.GetFileNameWithoutExtension(filename) + $"_mosaic_row_{{row}}_col_{{col}}.{DefaultExtension}";
					break;

				default:
					Filename = $"mosaic.{DefaultExtension}";
					ExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
					FilenamePattern = $"mosaic_row_{{row}}_col_{{col}}.{DefaultExtension}";
					break;
			}
		}

		public void ToFile(Mosaic mosaic)
		{
			if (!IsValid)
				throw new InvalidOperationException();

			if (!OneFilePerBaseplate)
			{
				ToFile(mosaic, Filename);
				OnProgress(new ProgressEventArgs() { Done = 1, Total = 1 });
				OnCompleted(new EventArgs());
			}
			else
			{
				var mosaics = GetMosaicBaseplates(mosaic);
				int done = 0;

				foreach (KeyValuePair<(int Column, int Row), Mosaic> kvp in mosaics)
				{
					string filename = Path.Join(ExportFolder, FilenamePatternHelper.BuildFilename(FilenamePattern, kvp.Key.Row + 1, kvp.Key.Column + 1));
					ToFile(kvp.Value, filename);
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

		private void ToFile(Mosaic mosaic, string filename)
		{
			var sw = Stopwatch.StartNew();

			using (var fileStream = new FileStream(filename, FileMode.Create))
			{
				ToStream(mosaic, fileStream);
			}

			Logger.Debug($"Wrote '{filename}' in {sw.ElapsedMilliseconds}ms.");
		}
	}

	public class ProgressEventArgs
	{
		public int Done { get; init; }

		public int Total { get; init; }
	}
}
