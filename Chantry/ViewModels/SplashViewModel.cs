using Avalonia.Controls;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TeethInc.Chantry.App.Helpers;
using TeethInc.Chantry.Core;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.App.ViewModels
{
    public class SplashViewModel : BaseViewModel
    {
        private IFileDialog m_fileDialog;

        public event EventHandler<string>? FileSelected;
        public event EventHandler? CloseApplication;

        public SplashViewModel(IFileDialog fileDialog)
        {
            m_fileDialog = fileDialog;
        }

        public async Task CreateANewProject()
        {
            await m_fileDialog
                .ShowFileDialog(new string[] { "jpg", "png" })
                .ContinueWith(x => FileDialogFileSelectedHandler(x.Result));

            OnCloseApplication();
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

        protected virtual void OnCloseApplication()
        {
            if (CloseApplication is not null)
                CloseApplication(this, new EventArgs());
        }
    }
}
