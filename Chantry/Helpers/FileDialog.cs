using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Helpers
{
    public class FileDialog : IFileDialog
    {
        public async Task<string?> ShowFileDialog(string[] extensions)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "Images", Extensions = new List<string>(extensions) },
                new FileDialogFilter { Name = "All Files", Extensions = new List<string> {"*" } }
            };

            string[]? files = await dialog.ShowAsync(ApplicationHelper.GetMainWindow());

            return files?.FirstOrDefault();
        }

        public async Task<string?> ShowSaveDialog(string initialFileName)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.InitialFileName = initialFileName;

            string? filename = await dialog.ShowAsync(ApplicationHelper.GetMainWindow());

            return filename;
        }
    }
}
