using System;
using System.IO;
using TeethInc.Chantry.Helpers;
using TeethInc.Chantry.Core;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Views;

namespace TeethInc.Chantry.ViewModels
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
            SplashViewModel.OpenAProject += SplashViewModel_OpenAProject;

            m_fileDialog = new FileDialog();
        }

        public void OpenAnImageCommand()
        {
            m_fileDialog
                .ShowFileDialog(new string[] { "jpg", "png" })
                .ContinueWith(x => Open(x?.Result));
        }

        public void OpenAProjectCommand()
        {
            m_fileDialog
                .ShowFileDialog(new string[] { "json" })
                .ContinueWith(x => Open(x.Result));
        }

        public void SaveProjectCommand()
        {
            if (m_projectViewModel is not null)
                m_fileDialog
                    .ShowSaveDialog($"{m_projectViewModel.Name}.json")
                    .ContinueWith(x => SaveProject(x.Result));
        }

        public void ExportLdrawCommand()
        {
            var exportLdrawView = new ExportLdrawView() { DataContext = new ExportLdrawViewModel() };

            exportLdrawView.ShowDialog(ApplicationHelper.GetMainWindow());
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
            if (filename is null)
                return;

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

            IsLoading = false;
        }

        private void SaveProject(string? filename)
        {
            if (filename is null || m_projectViewModel is null)
                return;

            m_projectViewModel.SerializeProject(filename);

            var config = ApplicationHelper.LoadConfiguration();
            config.Mru = new string[] { filename };
            ApplicationHelper.SaveConfiguration(config);
        }

        private void Close()
        {
            ProjectViewModel = null;
        }
    }
}
