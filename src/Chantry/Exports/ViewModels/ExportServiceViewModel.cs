using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Exporters;
using TeethInc.Chantry.Core.Helpers;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Helpers;
using TeethInc.Chantry.ViewModels;

namespace TeethInc.Chantry.Exports.ViewModels
{
	internal class ExportServiceViewModel<T> : BaseViewModel where T : IExporter
	{
		private readonly ExportService<T> _exportService;
		private IFileDialog _fileDialog;

		public string Filename
		{
			get { return _exportService.OutputSettings.Filename; }
			set { _exportService.OutputSettings.Filename = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(IsValid)); }
		}

		public bool OneFilePerBaseplate
		{
			get { return _exportService.OutputSettings.OneFilePerBaseplate; }
			set { _exportService.OutputSettings.OneFilePerBaseplate = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(IsValid)); }
		}

		public string ExportFolder
		{
			get { return _exportService.OutputSettings.ExportFolder; }
			set { _exportService.OutputSettings.ExportFolder = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(IsValid)); }
		}

		public string FilenamePattern
		{
			get { return _exportService.OutputSettings.FilenamePattern; }
			set
			{
				_exportService.OutputSettings.FilenamePattern = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(IsValid));
				RaisePropertyChanged(nameof(ExampleFilenames));
			}
		}

		public string ExampleFilenames
		{
			get
			{
				if (string.IsNullOrWhiteSpace(FilenamePattern))
					return "";

				var sb = new StringBuilder();

				sb.Append(FilenamePatternHelper.BuildFilename(FilenamePattern, 1, 1));
				sb.Append(Environment.NewLine);
				sb.Append(FilenamePatternHelper.BuildFilename(FilenamePattern, 1, 2));
				sb.Append(Environment.NewLine);
				sb.Append(FilenamePatternHelper.BuildFilename(FilenamePattern, 1, 3));
				sb.Append(Environment.NewLine);
				sb.Append("...");
				sb.Append(Environment.NewLine);
				sb.Append(FilenamePatternHelper.BuildFilename(FilenamePattern, 6, 4));
				sb.Append(Environment.NewLine);
				sb.Append(FilenamePatternHelper.BuildFilename(FilenamePattern, 6, 5));
				sb.Append(Environment.NewLine);
				sb.Append(FilenamePatternHelper.BuildFilename(FilenamePattern, 6, 6));

				return sb.ToString();
			}
		}

		public bool IsValid
		{
			get { return _exportService.IsValid; }
		}

		public ExportServiceViewModel(ExportService<T> exportService)
		{
			_exportService = exportService;
			_fileDialog = new FileDialog();
		}

		public void SelectFilenameCommand()
		{
			_fileDialog
				.ShowSaveDialog(Filename, FileFilters.Ldraw)
				.ContinueWith(x => Filename = x.Result ?? Filename);
		}

		public void SelectFolderCommand()
		{
			_fileDialog
				.ShowFolderDialog(Path.GetDirectoryName(Filename) ?? Environment.GetFolderPath(Environment.SpecialFolder.Personal))
				.ContinueWith(x => ExportFolder = x.Result ?? ExportFolder);
		}
	}
}
