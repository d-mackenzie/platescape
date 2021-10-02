using System;
using System.Threading.Tasks;
using TeethInc.Chantry.App.Helpers;

namespace TeethInc.Chantry.App.ViewModels
{
    public class SplashViewModel : BaseViewModel
    {
        private IFileDialog m_fileDialog;

        public event EventHandler<string>? FileSelected;

        public SplashViewModel()
        {
            m_fileDialog = new FileDialog();
        }

        public SplashViewModel(IFileDialog fileDialog)
        {
            m_fileDialog = fileDialog;
        }

        public async Task OpenAnImage()
        {
            await m_fileDialog
                .ShowFileDialog(new string[] { "jpg", "png" })
                .ContinueWith(x => FileDialogFileSelectedHandler(x.Result));
        }

        private void FileDialogFileSelectedHandler(string? filename)
        {
            if (filename is not null)
                OnFileSelected(filename);
        }

        protected virtual void OnFileSelected(string filename)
        {
            if (FileSelected is not null)
                FileSelected(this, filename);
        }
    }
}
