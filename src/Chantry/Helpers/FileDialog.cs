using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Helpers
{
	public class FileDialog : IFileDialog
	{
		public async Task<string?> ShowOpenDialog(List<FileDialogFilter> filters)
		{
			OpenFileDialog dialog = new OpenFileDialog()
			{
				Filters = filters
			};

			string[]? files = await dialog.ShowAsync(ApplicationHelper.GetMainWindow());
			return files?.FirstOrDefault();
		}

		public async Task<string?> ShowSaveDialog(string initialFileName, IReadOnlyList<FilePickerFileType> filters)
		{
			Window window = ApplicationHelper.GetMainWindow();

			var options = new FilePickerSaveOptions()
			{
				ShowOverwritePrompt = true,
				SuggestedFileName = initialFileName,
				DefaultExtension = filters.FirstOrDefault()?.Patterns?.FirstOrDefault(),
			};

			IStorageFile? file = await window.StorageProvider.SaveFilePickerAsync(options);

			if (file is null)
				return null;

			return file.Name;
		}

		public async Task<string?> ShowFolderDialog(string folder)
		{
			OpenFolderDialog dialog = new OpenFolderDialog()
			{
				Directory = folder
			};

			string? filename = await dialog.ShowAsync(ApplicationHelper.GetMainWindow());
			return filename;
		}
	}
}
