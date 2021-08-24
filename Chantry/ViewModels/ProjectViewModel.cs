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

namespace TeethInc.Chantry.App.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {
        private Project m_project;
        private ObservableCollection<BaseViewModel> m_filters = new ObservableCollection<BaseViewModel>();

        public FileSource Source
        {
            get { return (FileSource)m_project.Source; }
        }

        public ObservableCollection<BaseViewModel> Filters => m_filters;

        public Bitmap UnfilteredImage => m_project.UnfilteredImage.AsAvaloniaMediaImagingBitmap();

        public Bitmap FilteredImage => m_project.FilteredImage.AsAvaloniaMediaImagingBitmap();

        public string Name => m_project.Name;

        public ProjectViewModel(Project project)
        {
            m_project = project;

            foreach (var filter in m_project.Filters)
            {
                if (filter is BrightnessContrastFilter brightnessContrastFilter)
                {
                    Filters.Add(new BrightnessContrastViewModel(brightnessContrastFilter));
                }
            }
        }
    }
}
