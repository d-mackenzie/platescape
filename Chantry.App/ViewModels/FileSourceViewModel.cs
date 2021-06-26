using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Sources;

namespace Chantry.App.ViewModels
{
    public class FileSourceViewModel : INotifyPropertyChanged
    {
        private FileSource m_source;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Filename
        {
            get { return m_source.Filename; }
            set { m_source.Filename = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filename))); }
        }

        public FileSourceViewModel(FileSource source)
        {
            m_source = source;
        }
    }
}
