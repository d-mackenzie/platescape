using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.Core.Services;

namespace Chantry.Cmd
{
	public class ExportWorker
	{
		public event EventHandler<ProgressEventArgs> Progress;

		public async void Export(Mosaic mosaic, ExportSettings exportSettings)
		{
			var exportService = new ExportService();

			exportService.Progress += ExportService_Progress;


			exportService.Export(mosaic, exportSettings);
		}

		private void ExportService_Progress(object? sender, ProgressEventArgs e)
		{
			Progress?.Invoke(this, e);
		}
	}

	public class ProgressEventArgs
	{
		public int Done { get; init; }

		public int Total { get; init; }
	}

}
