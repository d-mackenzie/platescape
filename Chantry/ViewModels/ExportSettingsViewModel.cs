using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Helpers;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.Helpers;

namespace TeethInc.Chantry.ViewModels
{
    internal class ExportSettingsViewModel : BaseViewModel
    {
        private readonly IExportSettings m_exportSettings;
        private IFileDialog m_fileDialog;

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
            set
            { 
               m_exportSettings.FilenamePattern = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsValid));
                RaisePropertyChanged(nameof(ExampleFilenames));
            }
        }

        public string ExampleFilenames
        {
            get
            {
                if (string.IsNullOrWhiteSpace(FilenamePattern))
                    return "";

                var sb = new StringBuilder();

                sb.Append(FilenamePatternHelper.BuildFilename(FilenamePattern, 1, 1));
                sb.Append(Environment.NewLine);
                sb.Append(FilenamePatternHelper.BuildFilename(FilenamePattern, 1, 2));
                sb.Append(Environment.NewLine);
                sb.Append(FilenamePatternHelper.BuildFilename(FilenamePattern, 1, 3));
                sb.Append(Environment.NewLine);
                sb.Append("...");
                sb.Append(Environment.NewLine);
                sb.Append(FilenamePatternHelper.BuildFilename(FilenamePattern, 6, 4));
                sb.Append(Environment.NewLine);
                sb.Append(FilenamePatternHelper.BuildFilename(FilenamePattern, 6, 5));
                sb.Append(Environment.NewLine);
                sb.Append(FilenamePatternHelper.BuildFilename(FilenamePattern, 6, 6));

                return sb.ToString();
            }
        }

        public bool IsValid
        {
            get { return m_exportSettings.IsValid; }
        }

        public ExportSettingsViewModel(IExportSettings exportSettings)
        {
            m_exportSettings = exportSettings;
            m_fileDialog = new FileDialog();
        }

        public void SelectFilenameCommand()
        {
            m_fileDialog
                .ShowSaveDialog(Filename, "ldr", FileFilters.Ldraw)
                .ContinueWith(x => Filename = x.Result ?? Filename);
        }

        public void SelectFolderCommand()
        {

        }

    }
}
