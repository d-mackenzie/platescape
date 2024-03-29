using Avalonia.Controls;
using Avalonia.Skia.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Helpers
{
    internal static class FileFilters
    {
        public static List<FileDialogFilter> Images =>
            new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "Images", Extensions = new List<string> {"jpg", "png"} },
                AllFilesFilter()
            };

        public static List<FileDialogFilter> Projects =>
            new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "Chantry Project", Extensions = new List<string> {"json"} },
                AllFilesFilter()
            };

        public static List<FileDialogFilter> Ldraw =>
            new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "LDRAW", Extensions = new List<string> {"ldr"} },
                AllFilesFilter()
            };

        private static FileDialogFilter AllFilesFilter() => new FileDialogFilter
        {
            Name = "All Files",
            Extensions = new List<string> {"*"}
        };
    }
}
