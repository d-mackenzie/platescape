using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Models
{
    public abstract class ExportSettings : IExportSettings
    {
        public string Filename { get; set; }

        public bool OneFilePerBaseplate { get; set; }

        public string ExportFolder { get; set; }

        public string FilenamePattern { get; set; }

        public ExportSettings()
        {
            OneFilePerBaseplate = false;
        }

        public virtual bool IsValid
        {
            get
            {
                return IsFilenameSettingsValid() && IsSettingsValid();
            }
        }

        private bool IsFilenameSettingsValid()
        {
            if (OneFilePerBaseplate)
            {
                return !string.IsNullOrWhiteSpace(ExportFolder) &&
                       !string.IsNullOrWhiteSpace(FilenamePattern);
            }
            else
            {
                return !string.IsNullOrWhiteSpace(Filename);
            }
        }

        protected virtual bool IsSettingsValid()
        {
            return true;
        }
    }
}
