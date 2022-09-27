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
        private ExportSettingsViewModel m_exportSettingsViewModel = new ExportSettingsViewModel();
        private bool m_includeBaseplate;

        public ExportSettingsViewModel ExportSettingsViewModel
        {
            get { return m_exportSettingsViewModel; }
            set { m_exportSettingsViewModel = value; RaisePropertyChanged(); }
        }

        public bool IncludeBaseplate
        {
            get { return m_includeBaseplate; }
            set { m_includeBaseplate = value; RaisePropertyChanged(); }
        }
    }
}
