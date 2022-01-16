using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TeethInc.Chantry.Core.Sources;
using TeethInc.Chantry.Extensions;
using TeethInc.Chantry.Core;
using TeethInc.Chantry.Core.Filters;
using System.Collections.ObjectModel;
using TeethInc.Chantry.Filters.ViewModels;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Ldraw;
using AmiBitmap = Avalonia.Media.Imaging.Bitmap;
using TeethInc.Chantry.Helpers;
using System.IO;
using SkiaSharp;

namespace TeethInc.Chantry.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {
        private Project m_project;
        private ObservableCollection<BaseViewModel> m_filters = new ObservableCollection<BaseViewModel>();
        private LdrawService m_ldrawService;
        private ObservableCollection<LdColor> m_allowedColors = new ObservableCollection<LdColor>();

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
        public List<LdColor> Colors => m_ldrawService.Colors.OrderBy(x => x.Name).ToList();
        public ObservableCollection<LdColor> AllowedColors => m_allowedColors;

        public LdPart Baseplate
        {
            get { return m_ldrawService.GetPart(m_project.BaseplatePartNumber); }
            set { m_project.BaseplatePartNumber = value.Number; RaiseMosaicPropertyChanged(); }
        }

        public LdPart Element
        {
            get { return m_ldrawService.GetPart(m_project.ElementPartNumber); }
            set { m_project.ElementPartNumber = value.Number; RaiseMosaicPropertyChanged(); }
        }

        public int BaseplateExtentWidth
        {
            get { return m_project.BaseplateExtent.Width; }
            set
            {
                m_project.BaseplateExtent = new SKSizeI(value, m_project.BaseplateExtent.Height);
                RaiseMosaicPropertyChanged();
            }
        }

        public int BaseplateExtentHeight
        {
            get { return m_project.BaseplateExtent.Height; }
            set
            {
                m_project.BaseplateExtent = new SKSizeI(m_project.BaseplateExtent.Width, value);
                RaiseMosaicPropertyChanged();
            }
        }

        public string SizeInfo
        {
            get
            {
                int width = m_project.BaseplateExtent.Width * Baseplate.Size.Width;
                int height = m_project.BaseplateExtent.Height * Baseplate.Size.Height;

                string ret = $"{m_project.ElementExtent.Width} elements by { m_project.ElementExtent.Height} elements\n";
                ret += $"{width} studs by {height} studs\n";
                ret += string.Format("{0:F0}cm by {1:F0}cm\n", width * 0.8, height * 0.8);
                ret += string.Format("{0} by {1}\n", MmToFeetAndInches(width * 8), MmToFeetAndInches(height * 8));

                return ret;
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

            var allowedColors = m_project.AllowedColors;

            foreach (var allowedColor in allowedColors)
            {
                AllowedColors.Add(m_ldrawService.GetColor(allowedColor));
            }

//            m_project.AllowedColors.ToList().ForEach(x => AllowedColors.Add(m_ldrawService.GetColor(x)));
            AllowedColors.CollectionChanged += AllowedColors_CollectionChanged;
        }

        public async void ExportLdraw()
        {
            IFileDialog fileDialog = new FileDialog();

            await fileDialog
                .ShowSaveDialog($"{m_project.Name}.ldr")
                .ContinueWith(x => ExportLdrawHandler(x?.Result));
        }

        public void SerializeProject(string filename)
        {
            string json = ProjectService.SerializeProject(m_project);
            File.WriteAllText(filename, json);
        }

        private void ExportLdrawHandler(string? filename)
        {
            if (string.IsNullOrEmpty(filename))
                return;

            m_project.ExportLdraw(filename);
        }

        private void AllowedColors_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            m_project.AllowedColors = AllowedColors.Select(x => x.Number).ToArray();
            RaiseMosaicPropertyChanged();
        }

        private void FilterPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(FilteredImage));
            RaiseMosaicPropertyChanged();
        }

        private void RaiseMosaicPropertyChanged()
        {
            RaisePropertyChanged(nameof(Mosaic));
            RaisePropertyChanged(nameof(SizeInfo));
        }

        private string MmToFeetAndInches(int mm)
        {
            int inches = (int)(mm / 25.4f);

            if (inches < 12)
                return $"{inches}in";

            if (inches % 12 == 0)
                return $"{inches / 12}ft";

            return $"{inches / 12}ft {inches % 12}in";
        }
    }
}
