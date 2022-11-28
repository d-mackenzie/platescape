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
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filters = filters;

            string[]? files = await dialog.ShowAsync(ApplicationHelper.GetMainWindow());

            return files?.FirstOrDefault();
        }

        public async Task<string?> ShowSaveDialog(string initialFileName, string defaultExtension, List<FileDialogFilter> filters)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Directory = Path.GetDirectoryName(initialFileName);
            dialog.InitialFileName = Path.GetFileName(initialFileName);
            dialog.Filters = filters;

            string? filename = await dialog.ShowAsync(ApplicationHelper.GetMainWindow());

            return filename;
        }
    }
}
