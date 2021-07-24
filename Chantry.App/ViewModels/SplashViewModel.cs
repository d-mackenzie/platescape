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

        public SplashViewModel(IFileDialog fileDialog)
        {
            m_fileDialog = fileDialog;
        }

        public void CreateANewProject()
        {
            m_fileDialog
                .ShowFileDialog(new string[] { "jpg", "png" })
                .ContinueWith(x => CreateANewProjectFileSelected(x.Result));
        }

        private void CreateANewProjectFileSelected(string? filename)
        {
            if (filename is not null)
            {
                Project project = new Project()
                {
                    Source = new FileSource()
                    {
                        Filename = filename
                    }
                };
            
            // raise event.
            
            }
        }

        public void OpenAnExistingProject()
        {



        }




    }
}
