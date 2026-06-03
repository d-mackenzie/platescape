using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeethInc.Chantry.Helpers;

namespace TeethInc.Chantry.ViewModels
{
	public class SplashViewModel : BaseViewModel
	{
		private IFileDialog _fileDialog;
		private List<string> _mru = new List<string>();

		public event EventHandler? OpenAnImage;
		public event EventHandler? OpenAProject;

		public override string Header => "Welcome";

		public string ApplicationTitle
		{
			get
			{
				var assembly = System.Reflection.Assembly.GetExecutingAssembly();
				return $"{assembly.GetName().Name}";
			}
		}

		public string Version
		{
			get
			{
				var assembly = System.Reflection.Assembly.GetExecutingAssembly();
				return $"Version {assembly.GetName().Version?.Major}";
			}
		}

		public List<string> Mru => _mru;

		public SplashViewModel()
		{
			_fileDialog = new FileDialog();
			//_mru = ApplicationHelper.LoadConfiguration().Mru.ToList();
		}

		public SplashViewModel(IFileDialog fileDialog)
		{
			_fileDialog = fileDialog;
			//_mru = ApplicationHelper.LoadConfiguration().Mru.ToList();
		}

		public void OpenAnImageCommand()
		{
			if (OpenAnImage is not null)
				OpenAnImage(this, EventArgs.Empty);
		}

		public void OpenAProjectCommand()
		{
			if (OpenAProject is not null)
				OpenAProject(this, new EventArgs());
		}
	}
}
