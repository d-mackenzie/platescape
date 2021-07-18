using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.App.Extensions;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.App.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public FileSourceViewModel FileSourceViewModel { get; set; }

        private FileSource m_fileSource;

        public Bitmap SourceImage
        {
            get { return m_fileSource.Image.AsAvaloniaMediaImagingBitmap(); }
        }

        public MainWindowViewModel()
        {
            m_fileSource = new FileSource()
            {
                Filename = @"C:\Users\david\Pictures\eric-avatar.jpg"
            };

            FileSourceViewModel = new FileSourceViewModel(m_fileSource);
            FileSourceViewModel.PropertyChanged += SourceChanged;
        }

        private void SourceChanged(object? sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(SourceImage));
        }
    }
}
