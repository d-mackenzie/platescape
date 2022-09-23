using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TeethInc.Chantry.Core.Sources;
using TeethInc.Chantry.Extensions;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Extensions;
using System.Collections.ObjectModel;
using TeethInc.Chantry.Filters.ViewModels;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Ldraw;
using AmiBitmap = Avalonia.Media.Imaging.Bitmap;
using TeethInc.Chantry.Helpers;
using System.IO;
using SkiaSharp;
using Avalonia;
using System.Diagnostics;
using System;
using TeethInc.Chantry.Services;
using TeethInc.Chantry.Core.MosaicAlgorithms;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {
        private Project m_project;
        private ObservableCollection<IFilterViewModel> m_filters = new ObservableCollection<IFilterViewModel>();
        private List<MosaicAlgorithm> m_mosaicAlgorithms = new List<MosaicAlgorithm>()
        {
            new BayerMatrix(),
            new FloydSteinberg(),
            new NearestColor()
        };

        private LdrawService m_ldrawService;

        private Point m_pan;
        private double m_zoom = 1d;

        // project properties.

        public string Name => m_project.Name;

        // source properties.

        public FileSource Source
        {
            get { return (FileSource)m_project.Source; }
        }

        // filter properties.

        public ObservableCollection<IFilterViewModel> Filters => m_filters;
        public AmiBitmap UnfilteredImage => m_project.UnfilteredImage.AsAvaloniaMediaImagingBitmap();
        public AmiBitmap FilteredImage => m_project.FilteredImage.AsAvaloniaMediaImagingBitmap();
        public AmiBitmap MosaicImage => m_project.Mosaic.Image.AsAvaloniaMediaImagingBitmap();

        // moasic properties.

        public List<LdPart> Baseplates => m_ldrawService.Baseplates;
        public List<LdPart> Elements => m_ldrawService.Elements;
        public List<AllowedColorViewModel> Colors =>
            m_ldrawService.Colors.Select(x =>
                new AllowedColorViewModel(x, m_project.AllowedColors.Contains(x.Number))).OrderBy(x => x.LdColor.Hue).ToList();

        public LdPart Baseplate
        {
            get { return m_ldrawService.GetPart(m_project.BaseplatePartNumber); }
            set { m_project.BaseplatePartNumber = value.Number; RaiseMosaicPropertiesChanged(); }
        }

        public LdPart Element
        {
            get { return m_ldrawService.GetPart(m_project.ElementPartNumber); }
            set { m_project.ElementPartNumber = value.Number; RaiseMosaicPropertiesChanged(); }
        }

        public int BaseplateExtentWidth
        {
            get { return m_project.BaseplateExtent.Width; }
            set
            {
                m_project.BaseplateExtent = new SKSizeI(value, m_project.BaseplateExtent.Height);
                RaiseMosaicPropertiesChanged();
            }
        }

        public int BaseplateExtentHeight
        {
            get { return m_project.BaseplateExtent.Height; }
            set
            {
                m_project.BaseplateExtent = new SKSizeI(m_project.BaseplateExtent.Width, value);
                RaiseMosaicPropertiesChanged();
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
                ret += $"{MmToCentimetersOrMeters(width * 8)} by {MmToCentimetersOrMeters(height * 8)}\n";
                ret += $"{MmToFeetAndInches(width * 8)} by {MmToFeetAndInches(height * 8)}\n";

                return ret;
            }
        }

        public Mosaic Mosaic => m_project.Mosaic;

        public MosaicAlgorithm MosaicAlgorithm
        {
            get { return m_project.MosaicAlgorithm; }
            set { m_project.MosaicAlgorithm = value; RaiseMosaicPropertiesChanged(); }
        }

        public List<MosaicAlgorithm> MosaicAlgorithms => m_mosaicAlgorithms;

        // project properties.

        // view properties.

        public double Zoom
        {
            get { return m_zoom; }
            set { m_zoom = value; RaisePropertyChanged(); }
        }

        public Point Pan
        {
            get { return m_pan; }
            set { m_pan = value; RaisePropertyChanged(); }
        }

        public double ZoomMultiplier
        {
            get { return m_project.FilteredImage.Info.Size.GetScaleToFill(m_project.Mosaic.Image.Info.Size); }
        }

        public ProjectViewModel(Project project)
        {
            m_ldrawService = new LdrawService();

            m_project = project;
            m_project.Filters.ForEach(AddFilterViewModel);
            m_project.MosaicAlgorithm = m_mosaicAlgorithms.FirstOrDefault(x => x.DisplayName == m_project.MosaicAlgorithm.DisplayName);

            Zoom = 4;
        }

        public void AddNewFilterCommand(Type filterType)
        {
            Filter? filter = Activator.CreateInstance(filterType) as Filter;

            if (filter is null)
                throw new ArgumentException($"Could not create filter of type {filterType}");

            m_project.Filters.Add(filter);
            AddFilterViewModel(filter);
        }

        public void RemoveFilterCommand(IFilterViewModel filterViewModel)
        {
            filterViewModel.PropertyChanged -= FilterPropertyChanged;

            int index = Filters.IndexOf(filterViewModel);
            Filters.RemoveAt(index);
            m_project.Filters.RemoveAt(index);

            RaiseFilterPropertyChanged();
        }

        public void ToggleColorCommand(AllowedColorViewModel color)
        {
            int number = color.LdColor.Number;

            if (m_project.AllowedColors.Contains(number))
            {
                m_project.AllowedColors = m_project.AllowedColors.Where(x => x != number).ToArray();
            }
            else
            {
                m_project.AllowedColors = m_project.AllowedColors.Concat(new int[] { number }).ToArray();
            }
            
            RaisePropertyChanged(nameof(Colors));
            RaiseMosaicPropertiesChanged();
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

        private void AddFilterViewModel(Filter filter)
        {
            IFilterViewModel viewModel = FilterViewResolver.ConstructFilterViewModel(filter);
            viewModel.PropertyChanged += FilterPropertyChanged;
            Filters.Add(viewModel);
        }

        private void ExportLdrawHandler(string? filename)
        {
            if (string.IsNullOrEmpty(filename))
                return;

            m_project.ExportLdraw(filename);
        }

        private void RaiseFilterPropertyChanged()
        {
            RaisePropertyChanged(nameof(FilteredImage));
            RaiseMosaicPropertiesChanged();
        }

        private void FilterPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            RaiseFilterPropertyChanged();
        }

        private void RaiseMosaicPropertiesChanged()
        {
            RaisePropertyChanged(nameof(MosaicImage));
//            RaisePropertyChanged(nameof(Mosaic));
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

        private string MmToCentimetersOrMeters(int mm)
        {
            if (mm >= 1000)
                return string.Format("{0:F1}m", mm / 1000d);

            return string.Format("{0:F0}cm", mm / 10d);
        }
    }
}
