using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeethInc.Chantry.App.Helpers;

namespace TeethInc.Chantry.App.ViewModels
{
    public class SplashViewModel : BaseViewModel
    {
        private IFileDialog m_fileDialog;
        private List<string> m_mru;

        public event EventHandler? OpenAnImage;
        public event EventHandler? OpenAProject;

        public List<string> Mru
        {
            get { return m_mru; }
        }

        public SplashViewModel()
        {
            m_fileDialog = new FileDialog();
            m_mru = ApplicationHelper.LoadConfiguration().Mru.ToList();
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
