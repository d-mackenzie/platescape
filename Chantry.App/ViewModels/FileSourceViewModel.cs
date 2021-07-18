using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Sources;
using TeethInc.Chantry.App.Extensions;
using TeethInc.Chantry.Core;

namespace TeethInc.Chantry.App.ViewModels
{
    public class FileSourceViewModel : BaseViewModel
    {
        private FileSource m_source;

        public string Filename
        {
            get { return m_source.Filename; }
            set { m_source.Filename = value; RaisePropertyChanged(); }
        }

        public FileSourceViewModel(FileSource source)
        {
            m_source = source;
        }
    }
}
