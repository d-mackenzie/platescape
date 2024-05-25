using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Exporters;

namespace TeethInc.Chantry.Core.Models
{
	public class ExportSettings<T> where T : IExporter
	{
		public string Filename { get; set; }

		public bool OneFilePerBaseplate { get; set; } = false;

		public string ExportFolder { get; set; }

		public string FilenamePattern { get; set; }

		public T Exporter { get; private set; }

		public ExportSettings(T exporter)
		{
			Exporter = exporter;
		}

		public bool IsValid
		{
			get
			{
				return IsFilenameSettingsValid() && Exporter.IsValid;
			}
		}

		private bool IsFilenameSettingsValid()
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
