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
		private Lazy<Window> _window = new Lazy<Window>(() => ApplicationHelper.GetMainWindow());

		private Window Window => _window.Value;

		public async Task<string?> ShowOpenDialog(IReadOnlyList<FilePickerFileType> filters)
		{
			var options = new FilePickerOpenOptions()
			{
				AllowMultiple = false,
				FileTypeFilter = filters
			};

			IReadOnlyList<IStorageFile> files = await Window.StorageProvider.OpenFilePickerAsync(options);

			return files?.FirstOrDefault()?.Name;
		}

		public async Task<string?> ShowSaveDialog(string initialFileName, IReadOnlyList<FilePickerFileType> filters)
		{
			var options = new FilePickerSaveOptions()
			{
				ShowOverwritePrompt = true,
				SuggestedFileName = initialFileName,
				DefaultExtension = filters.FirstOrDefault()?.Patterns?.FirstOrDefault(),
			};

			IStorageFile? file = await Window.StorageProvider.SaveFilePickerAsync(options);

			return file?.Name;
		}

		public async Task<string?> ShowFolderDialog(string initialFolder)
		{
			var options = new FolderPickerOpenOptions
			{
				AllowMultiple = false
			};

			IReadOnlyList<IStorageFolder>? folder = await Window.StorageProvider.OpenFolderPickerAsync(options);

			return folder?.FirstOrDefault()?.Name;
		}
	}
}
