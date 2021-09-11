using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.App.Helpers
{
    public interface IFileDialog
    {
        public Task<string?> ShowFileDialog(string[] extensions);

        public Task<string> ShowSaveDialog(string initialFilename);
    }
}
