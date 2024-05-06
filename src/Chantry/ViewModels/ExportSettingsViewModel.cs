using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Helpers;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.Helpers;

namespace TeethInc.Chantry.ViewModels
{
	internal class ExportSettingsViewModel : BaseViewModel
	{
		private readonly ExportSettings _exportSettings;
		private IFileDialog _fileDialog;

		public string Filename
		{
			get { return _exportSettings.Filename; }
			set { _exportSettings.Filename = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(IsValid)); }
		}

		public bool OneFilePerBaseplate
		{
			get { return _exportSettings.OneFilePerBaseplate; }
			set { _exportSettings.OneFilePerBaseplate = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(IsValid)); }
		}

		public string ExportFolder
		{
			get { return _exportSettings.ExportFolder; }
			set { _exportSettings.ExportFolder = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(IsValid)); }
		}

		public string FilenamePattern
		{
			get { return _exportSettings.FilenamePattern; }
			set
			{
				_exportSettings.FilenamePattern = value;
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
			get { return _exportSettings.IsValid; }
		}

		public ExportSettingsViewModel(ExportSettings exportSettings)
		{
			_exportSettings = exportSettings;
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
