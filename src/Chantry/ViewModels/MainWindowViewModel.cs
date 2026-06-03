using System;
using System.Collections.ObjectModel;
using System.Reflection;
using TeethInc.Chantry.Helpers;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Logging;
using Avalonia.Controls.Notifications;

namespace TeethInc.Chantry.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public ObservableCollection<BaseViewModel> Tabs { get; set; } = new ObservableCollection<BaseViewModel>();

		public string WindowTitle => Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? "";
		
		public MainWindowViewModel()
		{
			Tabs.Add(new SplashViewModel());
		}

		// public void OpenAnImageCommand()
		// {
		// 	string? result = null;
		//
		// 	_fileDialog
		// 		.ShowOpenDialog(FileFilters.Images)
		// 		.ContinueWith(x => result = x?.Result)
		// 		.GetAwaiter().OnCompleted(() =>
		// 		{
		// 			Open(result);
		// 		});
		// }

		// public void OpenAProjectCommand()
		// {
		// 	_fileDialog
		// 		.ShowOpenDialog(FileFilters.Projects)
		// 		.ContinueWith(x => Open(x.Result));
		// }


		// private void SplashViewModel_OpenAProject(object? sender, EventArgs e)
		// {
		// 	OpenAProjectCommand();
		// }
		//
		// private void SplashViewModel_OpenAnImage(object? sender, EventArgs e)
		// {
		// 	OpenAnImageCommand();
		// }
		//
		// private void Open(string? filename)
		// {
		// 	if (filename is null)
		// 		return;
		//
		// 	Logger.Debug($"Loading {filename}");
		//
		// 	try
		// 	{
		// 		ProjectViewModel = new ProjectViewModel(ProjectService.Load(filename));
		// 	}
		// 	catch (Exception ex)
		// 	{
		// 		Logger.Exception(ex);
		// 		ApplicationHelper.ShowToast("Cannot Open", ex.Message, NotificationType.Error);
		// 	}
		// }
	}
}
