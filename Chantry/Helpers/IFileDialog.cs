using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Helpers
{
    public interface IFileDialog
    {
        public Task<string?> ShowOpenDialog(List<FileDialogFilter> filers);

        public Task<string?> ShowSaveDialog(string initialFilename, List<FileDialogFilter> filters);

        public Task<string?> ShowFolderDialog(string folder);
    }
}
