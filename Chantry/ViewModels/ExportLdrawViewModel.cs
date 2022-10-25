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
        private ExportSettingsViewModel m_exportSettingsViewModel;
        private Project m_project;

        public ExportLdrawViewModel(Project project)
        {
            m_project = project;
            m_exportSettingsViewModel = new ExportSettingsViewModel(m_project.ExportLdrawSettings);
        }

        public ExportSettingsViewModel ExportSettingsViewModel
        {
            get { return m_exportSettingsViewModel; }
            set { m_exportSettingsViewModel = value; RaisePropertyChanged(); }
        }

        public bool IncludeBaseplate
        {
            get { return m_project.ExportLdrawSettings.IncludeBaseplate; }
            set { m_project.ExportLdrawSettings.IncludeBaseplate = value; RaisePropertyChanged(); }
        }

        public void ExportCommand()
        {
            m_project.Export(m_project.ExportLdrawSettings);
        }
    }
}
