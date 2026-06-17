using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Helpers;

namespace TeethInc.Chantry.ViewModels
{
	public class SplashViewModel : BaseViewModel
	{
		private readonly IFileDialog _fileDialog;
		private readonly List<string> _mru = new List<string>();
		private readonly MainWindowViewModel _mwvm;

		public override string Header => "Welcome";

		public string ApplicationTitle
		{
			get
			{
				var assembly = System.Reflection.Assembly.GetExecutingAssembly();
				return $"{assembly.GetName().Name}";
			}
		}

		public int Version
		{
			get
			{
				var assembly = System.Reflection.Assembly.GetExecutingAssembly();
				return assembly.GetName().Version?.Major ?? 0;
			}
		}

		public List<string> Mru => _mru;

		public SplashViewModel(MainWindowViewModel mwvm)
		{
			_fileDialog = new FileDialog();
			_mwvm = mwvm;
			
			//_mru = ApplicationHelper.LoadConfiguration().Mru.ToList();
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
			string? result = null;
		
			_fileDialog
				.ShowOpenDialog(FileFilters.Projects)
				.ContinueWith(x => result = x?.Result)
				.GetAwaiter().OnCompleted(() =>
				{
					Open(result);
				});
		}
		
		private void Open(string? filename)
		{
			if (string.IsNullOrWhiteSpace(filename))
				return;
				
			var project = ProjectService.Load(filename);
			
			_mwvm.AddProjectTab(project);
		}
	}
}
