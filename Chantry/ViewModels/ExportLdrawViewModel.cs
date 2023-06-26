using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.ViewModels
{
    internal class ExportLdrawViewModel : BaseViewModel
    {
        private ExportSettingsViewModel _exportSettingsViewModel;
        private Project _project;

        public ExportLdrawViewModel(Project project)
        {
            _project = project;
            _exportSettingsViewModel = new ExportSettingsViewModel(_project.ExportLdrawSettings);
        }

        public ExportSettingsViewModel ExportSettingsViewModel
        {
            get { return _exportSettingsViewModel; }
            set { _exportSettingsViewModel = value; RaisePropertyChanged(); }
        }

        public bool IncludeBaseplate
        {
            get { return _project.ExportLdrawSettings.IncludeBaseplate; }
            set { _project.ExportLdrawSettings.IncludeBaseplate = value; RaisePropertyChanged(); }
        }

        public void ExportCommand()
        {
            _project.Export(_project.ExportLdrawSettings);
        }
    }
}
