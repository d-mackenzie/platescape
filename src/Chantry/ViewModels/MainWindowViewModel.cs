using System;
using System.IO;
using TeethInc.Chantry.Helpers;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		private ProjectViewModel? _projectViewModel;
		private IFileDialog _fileDialog;
		private bool _isLoading = false;

		public ProjectViewModel? ProjectViewModel
		{
			get { return _projectViewModel; }
			set { _projectViewModel = value; RaisePropertyChanged(); }
		}

		public bool IsLoading
		{
			get { return _isLoading; }
			set { _isLoading = value; RaisePropertyChanged(); }
		}

		public SplashViewModel SplashViewModel { get; set; }

		public MainWindowViewModel(ProjectViewModel? projectViewModel, SplashViewModel splashViewModel)
		{
			ProjectViewModel = projectViewModel;
			SplashViewModel = splashViewModel;

			SplashViewModel.OpenAnImage += SplashViewModel_OpenAnImage; ;
			SplashViewModel.OpenAProject += SplashViewModel_OpenAProject;

			_fileDialog = new FileDialog();
		}

		public void OpenAnImageCommand()
		{
			_fileDialog
				.ShowOpenDialog(FileFilters.Images)
				.ContinueWith(x => Open(x?.Result));
		}

		public void OpenAProjectCommand()
		{
			_fileDialog
				.ShowOpenDialog(FileFilters.Projects)
				.ContinueWith(x => Open(x.Result));
		}

		public void SaveProjectCommand()
		{
			if (_projectViewModel is not null)
				_fileDialog
					.ShowSaveDialog($"{_projectViewModel.Name}.json", FileFilters.Projects)
					.ContinueWith(x => SaveProject(x.Result));
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

			ProjectViewModel = new ProjectViewModel(ProjectService.Load(filename));

			IsLoading = false;
		}

		private void SaveProject(string? filename)
		{
			if (filename is null || _projectViewModel is null)
				return;

			_projectViewModel.SerializeProject(filename);

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
