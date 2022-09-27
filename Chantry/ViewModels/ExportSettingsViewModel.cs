using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.ViewModels
{
    internal class ExportSettingsViewModel : BaseViewModel
    {
        private ExportSettings m_exportSettings = new ExportSettings();

        public string Filename
        {
            get { return m_exportSettings.Filename; }
            set { m_exportSettings.Filename = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(IsValid)); }
        }

        public bool OneFilePerBaseplate
        {
            get { return m_exportSettings.OneFilePerBaseplate;  }
            set { m_exportSettings.OneFilePerBaseplate = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(IsValid)); }
        }

        public string ExportFolder
        {
            get { return m_exportSettings.ExportFolder; }
            set { m_exportSettings.ExportFolder = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(IsValid)); }
        }

        public string FilenamePattern
        {
            get { return m_exportSettings.FilenamePattern; }
            set { m_exportSettings.FilenamePattern = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(IsValid)); }
        }

        public bool IsValid
        {
            get { return m_exportSettings.IsValid(); }
        }
    }
}
