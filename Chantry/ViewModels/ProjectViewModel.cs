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
using TeethInc.Chantry.Core.Filters;
using System.Collections.ObjectModel;
using TeethInc.Chantry.App.FilterViewModels;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.App.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {
        private Project m_project;
        private ObservableCollection<BaseViewModel> m_filters = new ObservableCollection<BaseViewModel>();
        private MosaicService m_mosaicService = new MosaicService();

        public FileSource Source
        {
            get { return (FileSource)m_project.Source; }
        }

        // filter properties.

        public ObservableCollection<BaseViewModel> Filters => m_filters;
        public Bitmap UnfilteredImage => m_project.UnfilteredImage.AsAvaloniaMediaImagingBitmap();
        public Bitmap FilteredImage => m_project.FilteredImage.AsAvaloniaMediaImagingBitmap();

        // moasic properties.

        public MosaicService MosaicService => m_mosaicService;
        public Mosaic Mosaic => m_project.Mosaic;

        public string Name => m_project.Name;

        public ProjectViewModel(Project project)
        {
            m_project = project;

            foreach (var filter in m_project.Filters)
            {
                if (filter is BrightnessContrastFilter brightnessContrastFilter)
                {
                    var brightnessContrastViewModel = new BrightnessContrastViewModel(brightnessContrastFilter);
                    brightnessContrastViewModel.PropertyChanged += FilterPropertyChanged;

                    Filters.Add(brightnessContrastViewModel);
                }
            }
        }

        private void FilterPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(FilteredImage));
            RaisePropertyChanged(nameof(Mosaic));
        }
    }
}
