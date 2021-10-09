using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.App.Extensions;
using TeethInc.Chantry.App.Helpers;
using TeethInc.Chantry.Core;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.App.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ProjectViewModel? m_projectViewModel;
        private IFileDialog m_fileDialog;
        private bool m_isLoading = false;

        public ProjectViewModel? ProjectViewModel
        {
            get { return m_projectViewModel; }
            set { m_projectViewModel = value; RaisePropertyChanged(); }
        }

        public bool IsLoading
        {
            get { return m_isLoading; }
            set { m_isLoading = value; RaisePropertyChanged(); }
        }

        public SplashViewModel SplashViewModel { get; set; }

        public MainWindowViewModel(ProjectViewModel? projectViewModel, SplashViewModel splashViewModel)
        {
            ProjectViewModel = projectViewModel;
            SplashViewModel = splashViewModel;

            SplashViewModel.OpenAnImage += SplashViewModel_OpenAnImage; ;
            SplashViewModel.OpenAProject += SplashViewModel_OpenAProject; ;

            m_fileDialog = new FileDialog();
        }

        public void OpenAnImageCommand()
        {
            m_fileDialog
                .ShowFileDialog(new string[] { "jpg", "png" })
                .ContinueWith(x => Open(x.Result));
        }

        public void OpenAProjectCommand()
        {
            m_fileDialog
                .ShowFileDialog(new string[] { "json" })
                .ContinueWith(x => Open(x.Result));
        }

        public void CloseCommand()
        {
            Close();
        }

        private void SplashViewModel_OpenAProject(object? sender, EventArgs e)
        {
            OpenAProjectCommand();
        }

        private void SplashViewModel_OpenAnImage(object? sender, EventArgs e)
        {
            OpenAnImageCommand();
        }

        private void Open(string? filename)
        {
            if (filename is not null)
            {
                IsLoading = true;

                if (Path.GetExtension(filename) == "json")
                {
                    string json = File.ReadAllText(filename);
                    ProjectViewModel = new ProjectViewModel(ProjectService.DeserializeProject(json));
                }
                else
                {
                    ProjectViewModel = new ProjectViewModel(Project.CreateSimpleProject(filename));
                }
            }

            IsLoading = false;
        }

        private void Close()
        {
            ProjectViewModel = null;
        }
    }
}
