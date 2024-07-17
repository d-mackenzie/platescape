using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Helpers
{
	public class FileDialog : IFileDialog
	{
		public async Task<string?> ShowOpenDialog(IReadOnlyList<FilePickerFileType> filters)
		{
			var options = new FilePickerOpenOptions()
			{
				AllowMultiple = false,
				FileTypeFilter = filters
			};

			IReadOnlyList<IStorageFile> files = await ApplicationHelper.GetMainWindow().StorageProvider.OpenFilePickerAsync(options);

			return files?.FirstOrDefault()?.Path.LocalPath;
		}

		public async Task<string?> ShowSaveDialog(string initialFileName, IReadOnlyList<FilePickerFileType> filters)
		{
			var options = new FilePickerSaveOptions()
			{
				ShowOverwritePrompt = true,
				SuggestedFileName = initialFileName,
				DefaultExtension = filters.FirstOrDefault()?.Patterns?.FirstOrDefault(),
			};

			IStorageFile? file = await ApplicationHelper.GetMainWindow().StorageProvider.SaveFilePickerAsync(options);

			return file?.Path.LocalPath;
		}

		public async Task<string?> ShowFolderDialog(string initialFolder)
		{
			var options = new FolderPickerOpenOptions
			{
				AllowMultiple = false
			};

			IReadOnlyList<IStorageFolder>? folder = await ApplicationHelper.GetMainWindow().StorageProvider.OpenFolderPickerAsync(options);

			return folder?.FirstOrDefault()?.Name;
		}
	}
}
