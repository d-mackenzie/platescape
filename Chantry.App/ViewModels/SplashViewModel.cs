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


namespace TeethInc.Chantry.App.ViewModels
{
    public class SplashViewModel : BaseViewModel
    {
        private Window m_window;

        public SplashViewModel(Window window)
        {
            m_window = window;
        }

        private async Task CreateANewProject()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "Images", Extensions = new List<string> {"jpg", "png" } },
                new FileDialogFilter { Name = "All Files", Extensions = new List<string> {"*" } }
            };

            var result = await dialog.ShowAsync(m_window);
        }

        public void DoOpenAnExistingProject()
        {



        }




    }
}
