using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.App.Extensions;
using TeethInc.Chantry.Core;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.App.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ProjectViewModel? m_projectViewModel;

        public ProjectViewModel? ProjectViewModel
        {
            get { return m_projectViewModel; }
            set { m_projectViewModel = value; RaisePropertyChanged(); }
        }

        public SplashViewModel SplashViewModel { get; set; }

        public MainWindowViewModel(ProjectViewModel? projectViewModel, SplashViewModel splashViewModel)
        {
            ProjectViewModel = projectViewModel;
            SplashViewModel = splashViewModel;

            SplashViewModel.FileSelected += SplashViewModel_FileSelected;


        }

        private void SplashViewModel_FileSelected(object? sender, string e)
        {
            ProjectViewModel = new ProjectViewModel(Project.CreateSimpleProject(e));
        }
    }
}
