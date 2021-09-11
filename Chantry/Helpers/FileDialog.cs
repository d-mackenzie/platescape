using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.App.Helpers
{
    public class FileDialog : IFileDialog
    {
        private Window m_window;

        public FileDialog()
        {
            m_window = ApplicationHelper.GetMainWindow();
        }

        public FileDialog(Window window)
        {
            m_window = window;
        }

        public async Task<string?> ShowFileDialog(string[] extensions)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "Images", Extensions = new List<string>(extensions) },
                new FileDialogFilter { Name = "All Files", Extensions = new List<string> {"*" } }
            };

            string[]? files = await dialog.ShowAsync(m_window);

            return files.FirstOrDefault();
        }

        public async Task<string> ShowSaveDialog(string initialFileName)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.InitialFileName = initialFileName;

            string filename = await dialog.ShowAsync(m_window);

            return filename;
        }
    }
}
