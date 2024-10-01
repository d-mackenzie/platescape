using System;
using System.IO;
using TeethInc.Chantry.Helpers;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Logging;
using TeethInc.Chantry.Services;
using System.Threading.Tasks;
using System.Threading;
using Avalonia.Controls.Notifications;

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
			string? result = null;

			_fileDialog
				.ShowOpenDialog(FileFilters.Images)
				.ContinueWith(x => result = x?.Result)
				.GetAwaiter().OnCompleted(() =>
				{
					Open(result);
				});
		}

		public void OpenAProjectCommand()
		{
			//_fileDialog
			//	.ShowOpenDialog(FileFilters.Projects)
			//	.ContinueWith(x => Open(x.Result));
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

			Logger.Debug($"Loading {filename}");

			try
			{
				ProjectViewModel = new ProjectViewModel(ProjectService.Load(filename));
			}
			catch (Exception ex)
			{
				Logger.Exception(ex);
				ApplicationHelper.ShowToast("Cannot Open", ex.Message, NotificationType.Error);
			}
		}
	}
}
