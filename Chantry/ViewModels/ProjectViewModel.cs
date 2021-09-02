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
using TeethInc.Chantry.App.Filters.ViewModels;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Ldraw;
using System.Drawing;
using AmiBitmap = Avalonia.Media.Imaging.Bitmap;

namespace TeethInc.Chantry.App.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {
        private Project m_project;
        private ObservableCollection<BaseViewModel> m_filters = new ObservableCollection<BaseViewModel>();
        private LdrawService m_ldrawService;

        // project properties.

        public string Name => m_project.Name;

        // source properties.

        public FileSource Source
        {
            get { return (FileSource)m_project.Source; }
        }

        // filter properties.

        public ObservableCollection<BaseViewModel> Filters => m_filters;
        public AmiBitmap UnfilteredImage => m_project.UnfilteredImage.AsAvaloniaMediaImagingBitmap();
        public AmiBitmap FilteredImage => m_project.FilteredImage.AsAvaloniaMediaImagingBitmap();
        public AmiBitmap MosaicImage => m_project.Mosaic.Image.AsAvaloniaMediaImagingBitmap();

        // moasic properties.

        public List<LdPart> Baseplates => m_ldrawService.Baseplates;
        public List<LdPart> Elements => m_ldrawService.Elements;

        public LdPart Baseplate
        {
            get { return m_project.Baseplate; }
            set { m_project.Baseplate = value; RaisePropertyChanged(nameof(Mosaic)); }
        }

        public LdPart Element
        {
            get { return m_project.Element; }
            set { m_project.Element = value; RaisePropertyChanged(nameof(Mosaic)); }
        }

        public int BaseplateExtentWidth
        {
            get { return m_project.BaseplateExtent.Width; }
            set
            {
                m_project.BaseplateExtent = new Size(value, m_project.BaseplateExtent.Height);
                RaisePropertyChanged(nameof(Mosaic));
            }
        }

        public int BaseplateExtentHeight
        {
            get { return m_project.BaseplateExtent.Height; }
            set
            {
                m_project.BaseplateExtent = new Size(m_project.BaseplateExtent.Width, value);
                RaisePropertyChanged(nameof(Mosaic));
            }
        }

        public Mosaic Mosaic => m_project.Mosaic;


        // project properties.

        public ProjectViewModel(Project project)
        {
            m_ldrawService = new LdrawService();

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
