using System;
using System.Threading.Tasks;
using TeethInc.Chantry.App.Helpers;

namespace TeethInc.Chantry.App.ViewModels
{
    public class SplashViewModel : BaseViewModel
    {
        private IFileDialog m_fileDialog;

        public event EventHandler? OpenAnImage;
        public event EventHandler? OpenAProject;

        public SplashViewModel()
        {
            m_fileDialog = new FileDialog();
        }

        public SplashViewModel(IFileDialog fileDialog)
        {
            m_fileDialog = fileDialog;
        }

        public void OpenAnImageCommand()
        {
            if (OpenAnImage is not null)
                OpenAnImage(this, new EventArgs());
        }

        public void OpenAProjectCommand()
        {
            if (OpenAProject is not null)
                OpenAProject(this, new EventArgs());
        }
    }
}
