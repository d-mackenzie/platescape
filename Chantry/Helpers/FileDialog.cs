using Avalonia.Controls;
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

        public async Task<string?> ShowSaveDialog(string initialFileName, List<FileDialogFilter> filters)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Directory = Path.GetDirectoryName(initialFileName),
                DefaultExtension = filters.FirstOrDefault()?.Extensions.FirstOrDefault(),
                InitialFileName = Path.GetFileName(initialFileName),
                Filters = filters
            };

            string? filename = await dialog.ShowAsync(ApplicationHelper.GetMainWindow());
            return filename;
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
