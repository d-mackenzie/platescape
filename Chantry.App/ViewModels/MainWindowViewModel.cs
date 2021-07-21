using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.App.Extensions;
using TeethInc.Chantry.Core;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.App.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ProjectViewModel ProjectViewModel { get; set; }

        private FileSource m_fileSource;

        public Bitmap SourceImage
        {
            get { return m_fileSource.Image.AsAvaloniaMediaImagingBitmap(); }
        }

        public MainWindowViewModel(Project project)
        {
            m_fileSource = (FileSource)project.Source;
            ProjectViewModel = new ProjectViewModel(project);
        }

        private void SourceChanged(object? sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(SourceImage));
        }
    }
}
